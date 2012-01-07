using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;
using System.IO;
using Kaedei.AcDown.Parser;
using Kaedei.AcDown.Interface.Forms;
using System.Net;
using System.Collections;

namespace Kaedei.AcDown.Downloader
{

	public class BilibiliPlugin : IAcdownPluginInfo
	{

		public string Name
		{
			get { return @"BilibiliDownloader"; }
		}

		public string Author
		{
			get { return "Kaedei Software"; }
		}

		public Version Version
		{
			get { return new Version(2, 1, 0, 0); }
		}

		public string Describe
		{
			get { return @"Bilibili.tv下载插件"; }
		}

		public string SupportUrl
		{
			get { return @"http://blog.sina.com.cn/kaedei"; }
		}

		public IDownloader CreateDownloader()
		{
			return new BilibiliDownloader();
		}

		public bool CheckUrl(string url)
		{
			Regex r = new Regex(@"^(http://(www\.|)bilibili\.(us|tv)/video/|)av(?<av>\d{2,6})");
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
		/// 规则为 bilibili + 视频ID + 下划线 + 子视频编号
		/// 如 "bilibili99999_2"或"bilibili99999_"
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public string GetHash(string url)
		{
			Regex r = new Regex(@"(http://(www\.|)bilibili\.(us|tv)/video/|)av(?<av>\d{2,6})(/index_(?<subav>\d+)\.html|)");
			Match m = r.Match(url);
			if (m.Success)
			{
				return "bilibili" + m.Groups["av"].Value + "_" + m.Groups["subav"].Value;
			}
			else
			{
				return null;
			}
		}

		public string[] GetUrlExample()
		{
			return new string[] { 
				"Bilibili下载插件:",
				"支持识别各Part名称、支持简写形式",
				"av97834",
				"http://www.bilibili.tv/video/av97834/",
				"http://www.bilibili.tv/video/av70229/index_20.html",
			};
		}

	} //end class

	/// <summary>
	/// Bilibili下载器
	/// </summary>
	public class BilibiliDownloader : IDownloader
	{

		public TaskInfo Info { get; set; }
		
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
			Info.Url = Info.Url.Replace("bilibili.us", "bilibili.tv");
			//修正URL2
			if (Regex.Match(Info.Url, @"^av\d{2,6}$").Success)
				Info.Url = "http://www.bilibili.tv/video/" + Info.Url + "/";
			//修正index_1.html
			if (!Info.Url.EndsWith(".html"))
			{
				if (Info.Url.EndsWith("/"))
					Info.Url += "index_1.html";
				else
					Info.Url += "/index_1.html";
			}


			string url = Info.Url;
			//取得子页面文件名（例如"/video/av12345/index_123.html"）
			string suburl = Regex.Match(Info.Url, @"bilibili\.tv(?<part>/video/av\d+/index_\d+\.html)").Groups["part"].Value;

			//视频地址数组
			string[] videos;

			try
			{
				//取得网页源文件
				string src = Network.GetHtmlSource2(url, Encoding.UTF8, Info.Proxy);
				//type值
				string type = "";
				#region 登录并重新获取网页源文件

				//检查是否需要登录
				if (src.Contains("你没有权限浏览")) //需要登录
				{
					CookieContainer cookies = new CookieContainer();
					//登录Bilibili
					var user = ToolForm.CreateLoginForm("https://secure.bilibili.tv/member/index_do.php?fmdo=user&dopost=regnew");
					//Post的数据
					string postdata = "fmdo=login&dopost=login&refurl=http%%3A%%2F%%2Fbilibili.tv%%2F&keeptime=604800&userid=" + user.Username + "&pwd=" + user.Password + "&keeptime=604800";
					byte[] data = Encoding.UTF8.GetBytes(postdata);
					//生成请求
					HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://secure.bilibili.tv/member/index_do.php");
					req.Method = "POST";
					req.Referer = "https://secure.bilibili.tv/login.php";
					req.ContentType = "application/x-www-form-urlencoded";
					req.ContentLength = data.Length;
					req.UserAgent= "Mozilla/5.0 (Windows NT 6.1; rv:6.0) Gecko/20100101 Firefox/6.0";
					req.CookieContainer = new CookieContainer();
					//发送POST数据
					using (var outstream = req.GetRequestStream())
					{
						outstream.Write(data, 0, data.Length);
						outstream.Flush();
					}
					//关闭请求
					req.GetResponse().Close();
					cookies = req.CookieContainer; //保存cookies
					string cookiesstr = req.CookieContainer.GetCookieHeader(req.RequestUri); //字符串形式的cookies

					//重新请求网页
					HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
					if (Info.Proxy != null)
						request.Proxy = Info.Proxy;
					//设置cookies
					request.CookieContainer = cookies;
					//获取网页源代码
					src = Network.GetHtmlSource(request, Encoding.UTF8);
				}

				#endregion

				//取得视频标题
				Regex rTitle = new Regex(@"<title>(?<title>.*)</title>");
				Match mTitle = rTitle.Match(src);
				//文件名称！
				string title = mTitle.Groups["title"].Value.Replace("- 嗶哩嗶哩", "").Replace("- ( ゜- ゜)つロ 乾杯~", "").Trim();

				//取得子标题
				Regex rSubTitle = new Regex(@"<option value='(?<part>.+?\.html)'(| selected)>(?<content>.+?)</option>");
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
							if (suburl == item.Groups["part"].Value)
							{
								urls.Add(url.Replace(suburl, item.Groups["part"].Value));
								titles.Add(item.Groups["content"].Value);
							}
						}
						//提供BitArray
						BitArray ba = new BitArray(mSubTitles.Count, false);
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

				//过滤非法字符
				Info.Title = title;
				title = Tools.InvalidCharacterFilter(title, "");

				//清空地址
				Info.FilePath.Clear();

				//视频id
				string id = "";

				//分析id和视频存放站点(type)
				//取得"bofqi块的源代码
				Regex rEmbed = new Regex("<div class=\"scontent\" id=\"bofqi\">(?<content>.*?)</div>", RegexOptions.Singleline);
				Match mEmbed = rEmbed.Match(src);
				string embedSrc = mEmbed.Groups["content"].Value.Replace("type=\"application/x-shockwave-flash\"", "");

				//检查"file"参数
				Regex rFile = new Regex("file=\"(?<file>.+?)\"");
				Match mFile = rFile.Match(embedSrc);
				//取得Flash地址
				Regex rFlash = new Regex("src=\"(?<flash>.*?\\.swf)\"");
				Match mFlash = rFlash.Match(embedSrc);
				//取得id值
				Regex rId = new Regex(@"(?<idname>(\w{0,2}id|data))=(?<id>([\w\-]+|$http://.+?$))".Replace("$", "\""));
				Match mId = rId.Match(embedSrc);
				//取得ID
				id = mId.Groups["id"].Value;
				//取得type值
				type = mId.Groups["idname"].Value;

				DownloadSubtitleType downsub = Info.DownSub;
				//如果不是“仅下载字幕”
				if (downsub != DownloadSubtitleType.DownloadSubtitleOnly)
				{
					if (mFile.Success) //如果有file参数
					{
						string fileurl = mFile.Groups["file"].Value;
						videos = new string[] { fileurl };
					}
					else if (mId.Success)//如果是普通的外链
					{
						//检查外链
						switch (type)
						{
							case "qid": //QQ视频
								//解析视频
								QQVideoParser parserQQ = new QQVideoParser();
								videos = parserQQ.Parse(new string[] { id }, Info.Proxy);
								break;
							case "ykid": //优酷视频
								//解析视频
								YoukuParser parserYouKu = new YoukuParser();
								videos = parserYouKu.Parse(new string[] { id }, Info.Proxy);
								break;
							case "uid": //土豆视频
								//解析视频
								TudouParser parserTudou = new TudouParser();
								videos = parserTudou.Parse(new string[] { id }, Info.Proxy);
								break;
							case "data": //Flash游戏
								id = id.Replace("\"", "");
								videos = new string[] { id };
								break;
							default: //新浪视频
								SinaVideoParser parserSina = new SinaVideoParser();
								videos = parserSina.Parse(new string[] { id }, Info.Proxy);
								break;
						}
					}
					else //如果是游戏
					{
						string flashurl = mFlash.Groups["flash"].Value;
						videos = new string[] { flashurl };
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
				}//end 判断是否下载视频

				//如果不是“不下载弹幕”且ID不为空
				if ((downsub != DownloadSubtitleType.DontDownloadSubtitle) && !string.IsNullOrEmpty(id))
				{
					//----------下载字幕-----------
					delegates.TipText(new ParaTipText(this.Info, "正在下载字幕文件"));
					//字幕文件(on)地址
					string subfile = Path.Combine(Info.SaveDirectory.ToString(), title + ".xml");
					Info.SubFilePath.Add(subfile);
					//取得字幕文件(on)地址
					string subUrl = "http://comment.bilibili.tv/dm," + id;
					//下载字幕文件
					try
					{
						Network.DownloadFile(new DownloadParameter()
							{
								Url = subUrl,
								FilePath = subfile,
								Proxy = Info.Proxy
							});
					}
					catch { }
				}
			}
			catch (Exception ex)
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
