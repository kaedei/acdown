using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;
using Kaedei.AcDown.Parser;
using System.IO;
using System.Collections;
using Kaedei.AcDown.Interface.Forms;

namespace Kaedei.AcDown.Downloader
{
	/// <summary>
	/// AcFun下载支持插件
	/// </summary>
	public class AcFunPlugin :IAcdownPluginInfo
	{

		public string Name
		{
			get { return @"AcFunDownloader"; }
		}

		public string Author
		{
			get { return "Kaedei Software"; }
		}

		public Version Version
		{
			get { return new Version(3, 1, 0, 0); }
		}

		public string Describe
		{
			get { return @"Acfun.tv下载插件"; }
		}

		public string SupportUrl
		{
			get { return @"http://blog.sina.com.cn/kaedei"; }
		}

		public IDownloader CreateDownloader()
		{
			return new AcfunDownloader();
		}

		public bool CheckUrl(string url)
		{
			Regex r = new Regex(@"^(http://((www\.|)acfun\.tv|.*?)/v/|)ac(?<id>\d+)");
			if (r.Match(url).Success)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 规则为 acfun + 视频ID + 下划线 + 子视频编号
		/// 如 "acfun158539_"或"acfun123456_2"
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public string GetHash(string url)
		{
			Regex r = new Regex(@"^(http://((www\.|)acfun\.tv|.*?)/v/|)ac(?<id>\d+)(/index_(?<subid>\d+)\.html|)");
			Match m = r.Match(url);
			if (m.Success)
			{
				return "acfun" + m.Groups["id"].Value + "_" + m.Groups["subid"].Value;
			}
			else
			{
				return null;
			}
		}

		public string[] GetUrlExample()
		{
			return new string[] { 
				"AcFun.tv下载插件:",
				"支持识别各Part名称、支持简写形式",
				"ac206020",
				"http://acfun.tv/v/ac206020",
				"http://www.acfun.tv/v/ac206020",
				"http://124.228.254.229/v/ac206020 (IP地址形式)"
			};
		}



	}

	/// <summary>
	/// Acfun下载器
	/// </summary>
	public class AcfunDownloader : IDownloader
	{

		public TaskInfo Info { get; set; }

		//服务器IP地址
		//private const string ServerIP = "124.228.254.229";
		//下载参数
		DownloadParameter currentParameter;

		#region IDownloader 成员

		public DelegateContainer delegates { get; set; }

		//文件总长度
		public long TotalLength
		{
			get
			{
				if (currentParameter != null)
				{
					return currentParameter.TotalLength;
				}
				else
				{
					return 0;
				}
			}
		}

		//已完成的长度
		public long DoneBytes
		{
			get 
			{
				if (currentParameter != null)
				{
					return currentParameter.DoneBytes;
				}
				else
				{
					return 0;
				}
			}
		}

		//最后一次Tick时的值
		public long LastTick
		{
			get 
			{
				if (currentParameter != null)
				{
					//将tick值更新为当前值
					long tmp = currentParameter.LastTick;
					currentParameter.LastTick = currentParameter.DoneBytes;
					return tmp;	
				}
				else
				{
					return 0;
				}
			}
		}


		//下载视频
		public void Download()
		{ 
			//开始下载
			delegates.Start(new ParaStart(this.Info));
			delegates.TipText(new ParaTipText(this.Info, "正在分析视频地址"));
			Info.Status = DownloadStatus.正在下载;

			//修正URL
			if (Regex.Match(Info.Url, @"^ac\d+$").Success)
				Info.Url = "http://www.acfun.tv/v/" + Info.Url;

			//修正index.html
			if (!Info.Url.EndsWith(".html"))
			{
				if (Info.Url.EndsWith("/"))
					Info.Url += "index.html";
				else
					Info.Url += "/index.html";
			}

			string url = Info.Url;
			//取得子页面文件名（例如"index_123.html"）
			string suburl = Regex.Match(Info.Url, @"ac\d+/(?<part>index\.html|index_\d+\.html)").Groups["part"].Value;

			try
			{
				//取得网页源文件
				string src = Network.GetHtmlSource(url, Encoding.GetEncoding("GB2312"), Info.Proxy);

				//分析id和视频存放站点(type)
				string type;
				string id = ""; //视频id
				string ot = ""; //视频子id

				//取得embed块的源代码
				Regex rEmbed = new Regex(@"<embed .+?>");
				Match mEmbed = rEmbed.Match(src);
				string embedSrc = mEmbed.ToString().Replace("type=\"application/x-shockwave-flash\"", "");

				string flashsrc = "";
				//检查是否为Flash游戏
				Regex rFlash2 = new Regex(@"src=""(?<player>.+?)""");
				flashsrc = rFlash2.Match(embedSrc).Groups["player"].Value;

				Regex rFlash = new Regex(@"data=""(?<flash>.+?\.swf)""");
				Match mFlash = rFlash.Match(src);
				if (mFlash.Success)
					flashsrc = mFlash.Groups["flash"].Value;

				//如果是Flash游戏
				if (!flashsrc.Contains("newflvplayer"))
				{
					type = "game";
				}
				else
				{
					
					//取得id值
					Regex rId = new Regex(@"(\?|amp;)id=(?<id>\w+)(?<ot>(-\w*|))");
					Match mId = rId.Match(embedSrc);
					id = mId.Groups["id"].Value;
					ot = mId.Groups["ot"].Value;

					//取得type值
					Regex rType = new Regex(@"type(|\w)=(?<type>\w*)");
					Match mType = rType.Match(embedSrc);
					type = mType.Groups["type"].Value;
				}

				//取得视频标题
				Regex rTitle = new Regex(@"<title>(?<title>.*)</title>");
				Match mTitle = rTitle.Match(src);
				string title = mTitle.Groups["title"].Value.Replace(" - AcFun.tv","");

				//取得子标题
				Regex rSubTitle = new Regex(@"<option value='(?<part>\w+?\.html)'(| selected)>(?<content>.+?)</option>");
				MatchCollection mSubTitles = rSubTitle.Matches(src);
				 //如果存在下拉列表框
				if (mSubTitles.Count > 0)
				{
					//确定当前视频的子标题
					foreach (Match item in mSubTitles)
					{
						if (suburl == item.Groups["part"].Value)
						{
							title = title + " - " + item.Groups["content"].Value;
							break;
						}
					}
					//如果需要解析关联下载项
					if (Info.ParseRelated)
					{
						//准备地址列表
						List<string> urls = new List<string>();
						//准备标题列表
						List<string> titles = new List<string>();
						//填充两个列表
						foreach (Match item in mSubTitles)
						{
							if (suburl != item.Groups["part"].Value)
							{
								urls.Add(url.Replace(suburl, item.Groups["part"].Value));
								titles.Add(item.Groups["content"].Value);
							}
						}
						//提供BitArray
						BitArray ba = new BitArray(urls.Count, false);
						//用户选择任务
						ba = ToolForm.CreateSelctionForm(titles.ToArray(), ba);
						//根据用户选择新建任务
						for (int i = 0; i < ba.Count; i++)
						{
							if (ba[i]) //如果选中了某项
							{
								//新建任务
								delegates.NewTask(new ParaNewTask(Info.BasePlugin, urls[i], this.Info));
							}
						}
					}
				}

				Info.Title = title;
				//过滤非法字符
				title = Tools.InvalidCharacterFilter(title, "");

				//视频地址数组
				string[] videos = null;
				//清空地址
				Info.FilePath.Clear();

				DownloadSubtitleType downsub = Info.DownSub;
				//如果不是“仅下载字幕”
				if (downsub != DownloadSubtitleType.DownloadSubtitleOnly)
				{
					//检查type值
					switch (type)
					{
						case "video": //新浪视频
							//解析视频
							SinaVideoParser parserSina = new SinaVideoParser();
							videos = parserSina.Parse(new string[] { id }, Info.Proxy);
							break;
						case "qq": //QQ视频
							//解析视频
							QQVideoParser parserQQ = new QQVideoParser();
							videos = parserQQ.Parse(new string[] { id }, Info.Proxy);
							break;
						case "youku": //优酷视频
							//解析视频
							YoukuParser parserYouKu = new YoukuParser();
							videos = parserYouKu.Parse(new string[] { id }, Info.Proxy);
							break;
						case "game": //flash游戏
							videos = new string[] { flashsrc };
							break;
					}

					//下载视频
					//确定视频共有几个段落
					Info.PartCount = videos.Length;

					//------------分段落下载------------
					for (int i = 0; i < Info.PartCount; i++)
					{
						Info.CurrentPart = i + 1;

						//取得文件后缀名
						string ext = Tools.GetExtension(videos[i]);
						if (string.IsNullOrEmpty(ext))
						{
							if (string.IsNullOrEmpty(Path.GetExtension(videos[i])))
								ext = ".flv";
							else
								ext = Path.GetExtension(videos[i]);
						}
						if (ext == ".hlv") ext = ".flv";
						//设置当前DownloadParameter
						if (Info.PartCount == 1)
						{
							currentParameter = new DownloadParameter()
							{
								//文件名 例: c:\123(1).flv
								FilePath = Path.Combine(Info.SaveDirectory.ToString(),
											title + ext),
								//文件URL
								Url = videos[i],
								//代理服务器
								Proxy = Info.Proxy
							};
						}
						else
						{
							currentParameter = new DownloadParameter()
							{
								//文件名 例: c:\123(1).flv
								FilePath = Path.Combine(Info.SaveDirectory.ToString(),
											title + "(" + (i + 1).ToString() + ")" + ext),
								//文件URL
								Url = videos[i],
								//代理服务器
								Proxy = Info.Proxy
							};
						}

						//设置代理服务器
						currentParameter.Proxy = Info.Proxy;
						//添加文件路径到List<>中
						Info.FilePath.Add(currentParameter.FilePath);
						//下载文件
						bool success;


						//提示更换新Part
						delegates.NewPart(new ParaNewPart(this.Info, i + 1));

						//下载视频
						success = Network.DownloadFile(currentParameter, this.Info);

						if (!success) //未出现错误即用户手动停止
						{
							Info.Status = DownloadStatus.已经停止;
							delegates.Finish(new ParaFinish(this.Info, false));
							return;
						}
					} //end for

				}

				if (downsub != DownloadSubtitleType.DontDownloadSubtitle)
				{
					//----------下载字幕-----------
					delegates.TipText(new ParaTipText(this.Info, "正在下载字幕文件"));
					//字幕文件(on)地址
					string subfile = Path.Combine(Info.SaveDirectory.ToString(), title + ".xml");
					Info.SubFilePath.Add(subfile);
					//取得字幕文件(on)地址
					string subUrl = @"http://comment.acfun.tv/%VideoId%.json?clientID=0.17456858092918992".Replace(@"%VideoId%", id + (ot.Length > 2 ? ot : ""));

					try
					{
						//下载字幕文件
						string subcontent = Network.GetHtmlSource(subUrl, Encoding.GetEncoding("gb2312"), Info.Proxy);
						//下面这行代码可以将json文件解码
						//subcontent = Tools.ReplaceUnicode2Str(subcontent);
						//保存文件
						File.WriteAllText(subfile, subcontent);
					}
					catch { }

					////字幕文件(lock)地址
					subfile = Path.Combine(Info.SaveDirectory.ToString(), title + "[锁定].xml");
					Info.SubFilePath.Add(subfile);
					//取得字幕文件(lock)地址
					subUrl = @"http://comment.acfun.tv/%VideoId%_lock.json?clientID=0.17456858092918992".Replace(@"%VideoId%", id + (ot.Length > 2 ? ot : ""));
					try
					{
						//下载字幕文件
						string subcontent = Network.GetHtmlSource(subUrl, Encoding.GetEncoding("gb2312"), Info.Proxy);
						//下面这行代码可以将json文件解码
						//subcontent = Tools.ReplaceUnicode2Str(subcontent);
						//保存文件
						File.WriteAllText(subfile, subcontent);
					}
					catch { }
				}
			
			}
			catch(Exception ex)
			{
				Info.Status = DownloadStatus.出现错误;
				delegates.Error(new ParaError(this.Info, ex));
				return;
			}
			//下载成功完成
			Info.Status = DownloadStatus.下载完成;
			delegates.Finish(new ParaFinish(this.Info, true));
		}


		//停止下载
		public void StopDownload()
		{
			if (currentParameter != null)
			{
				//将停止flag设置为true
				currentParameter.IsStop = true;
			}
		}


		#endregion

		

	}
}
