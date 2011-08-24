using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;
using System.IO;
using Kaedei.AcDown.Parser;
using Kaedei.AcDown.Interface.Forms;
using System.Net;

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
			get { return new Version(1, 3, 0, 0); }
		}

		public string Describe
		{
			get { return @"Bilibili下载插件"; }
		}

		public string SupportUrl
		{
			get { return @"http://blog.sina.com.cn/kaedei"; }
		}

		public IDownloader CreateDownloader()
		{
			return new BilibiliDownloader(this);
		}

		public bool CheckUrl(string url)
		{
			Regex r = new Regex(@"^http://(www\.|)bilibili\.(us|tv)/video/av(?<av>\w+)");
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
			Regex r = new Regex(@"http://(www\.|)bilibili\.(us|tv)/video/av(?<av>.\w+)(/index_(?<subav>\d+)\.html|)");
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
				"支持识别各Part名称",
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
		public BilibiliDownloader(BilibiliPlugin p)
		{
			_basePlugin = p;
		}
		//插件
		BilibiliPlugin _basePlugin;
		public IAcdownPluginInfo GetBasePlugin() { return _basePlugin; }
		
		//下载参数
		DownloadParameter currentParameter;
		#region IDownloader 成员

		public Guid TaskId { get; set; }

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

		//分段数量
		private int _partCount;
		public int PartCount
		{
			get { return _partCount; }
		}

		//当前分段
		private int _currentPart;
		public int CurrentPart
		{
			get { return _currentPart; }
		}

		//下载地址
		public string Url { get; set; }


		//下载状态
		private DownloadStatus _status;
		public DownloadStatus Status
		{
			get
			{
				return _status;
			}
		}

		//视频标题
		private string _title;
		public string Title
		{
			get
			{
				return _title;
			}
		}

		//保存到的文件夹
		public DirectoryInfo SaveDirectory { get; set; }

		//下载文件地址
		private List<string> _filePath = new List<string>();
		public List<string> FilePath
		{
			get
			{
				return _filePath;
			}
		}

		//字幕文件地址
		private List<string> _subFilePath = new List<string>();
		public List<string> SubFilePath
		{
			get
			{
				return _subFilePath;
			}
		}

		//下载视频
		public void Download()
		{ 
			//开始下载
			delegates.Start(new ParaStart(this.TaskId));
			delegates.TipText(new ParaTipText(this.TaskId, "正在分析视频地址"));
			_status = DownloadStatus.正在下载;

			//修正URL
			Url = Url.Replace("bilibili.us", "bilibili.tv");
			string url = Url;
			//视频地址数组
			string[] videos;

			try
			{
				//取得网页源文件
				string src = Network.GetHtmlSource(url, Encoding.UTF8, delegates.Proxy);

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
               if (delegates.Proxy != null)
                  request.Proxy = delegates.Proxy;
               //设置cookies
               request.CookieContainer = cookies;
               HttpWebResponse response = (HttpWebResponse)request.GetResponse();
               using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
               {
                  src = reader.ReadToEnd();
               }
            }

            #endregion

            //取得视频标题
				Regex rTitle = new Regex(@"<title>(?<title>.*)</title>");
				Match mTitle = rTitle.Match(src);
				string title = mTitle.Groups["title"].Value.Replace(" _嗶哩嗶哩", "").Replace(" - 嗶哩嗶哩", "");

				//取得子标题
				Regex rSubTitle = new Regex(@"<option value='\w+?\.html'(?<isselected>(| selected))>(?<content>.+?)</option>");
				MatchCollection mSubTitles = rSubTitle.Matches(src);

				//如果存在下拉列表框
				if (mSubTitles.Count > 0)
				{
					bool findSelected = false;
					foreach (Match item in mSubTitles)
					{
						if (!string.IsNullOrEmpty(item.Groups["isselected"].Value))
						{
							title = title + " - " + item.Groups["content"].Value;
							findSelected = true;
						}
					}
					if (!findSelected)
					{
						title = title + " - " + mSubTitles[0].Groups["content"].Value;
					}
				}
				//过滤非法字符
				title = Tools.InvalidCharacterFilter(title, "");
				_title = title;

				//清空地址
				_filePath.Clear();

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
				Regex rId = new Regex(@"(?<idname>(\w{0,2}id|data))=(?<id>(\w+|$http://.+?$))".Replace("$", "\""));
				Match mId = rId.Match(embedSrc);

				if (mFile.Success) //如果有file参数
				{
					string fileurl = mFile.Groups["file"].Value;
					videos = new string[] { fileurl };
				}
				else if (mId.Success)//如果是普通的外链
				{
					//取得ID
					id = mId.Groups["id"].Value;
					//取得type值
					string type = mId.Groups["idname"].Value;
					//检查外链
					switch (type)
					{
						case "qid": //QQ视频
							//解析视频
							QQVideoParser parserQQ = new QQVideoParser();
							videos = parserQQ.Parse(new string[] { id }, delegates.Proxy);
							break;
						case "ykid": //优酷视频
							//解析视频
							YoukuParser parserYouKu = new YoukuParser();
							videos = parserYouKu.Parse(new string[] { id }, delegates.Proxy);
							break;
						case "uid": //土豆视频
							//解析视频
							TudouParser parserTudou = new TudouParser();
							videos = parserTudou.Parse(new string[] { id }, delegates.Proxy);
							break;
						case "rid": //6.cn视频
							SixcnParser parserSixcn = new SixcnParser();
							videos = parserSixcn.Parse(new string[] { id }, delegates.Proxy);
							break;
						case "data": //Flash游戏
							id = id.Replace("\"", "");
							videos = new string[] { id };
							break;
						default: //新浪视频
							SinaVideoParser parserSina = new SinaVideoParser();
							videos = parserSina.Parse(new string[] { id }, delegates.Proxy);
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
				_partCount = videos.Length;

				//------------分段落下载------------
				for (int i = 0; i < _partCount; i++)
				{
					_currentPart = i + 1;
					//提示更换新Part
					delegates.NewPart(new ParaNewPart(this.TaskId, i + 1));
					//取得文件后缀名
					string ext = Tools.GetExtension(videos[i]);
					if (string.IsNullOrEmpty(ext))
					{
						if (string.IsNullOrEmpty(Path.GetExtension(videos[i])))
							ext = ".flv";
						else
							ext = Path.GetExtension(videos[i]);
					}
					//设置当前DownloadParameter
					if (_partCount == 1)
					{
						currentParameter = new DownloadParameter()
						{
							//文件名 例: c:\123(1).flv
							FilePath = Path.Combine(SaveDirectory.ToString(),
										_title + ext),
							//文件URL
							Url = videos[i],
							Proxy = delegates.Proxy
						};
					}
					else
					{
						currentParameter = new DownloadParameter()
						{
							//文件名 例: c:\123(1).flv
							FilePath = Path.Combine(SaveDirectory.ToString(),
										_title + "(" + (i + 1).ToString() + ")" + ext),
							//文件URL
							Url = videos[i],
							//代理服务器
							Proxy = delegates.Proxy
						};
					}
					//添加文件路径到List<>中
					_filePath.Add(currentParameter.FilePath);
					//下载文件
					bool success;
					//下载视频
					success = Network.DownloadFile(currentParameter);

					if (!success) //未出现错误即用户手动停止
					{
						_status = DownloadStatus.已经停止;
						delegates.Finish(new ParaFinish(this.TaskId, false));
						return;
					}
				} //end for

				if (GlobalSettings.GetSettings().DownSub && !string.IsNullOrEmpty(id))
				{
					//----------下载字幕-----------
					delegates.TipText(new ParaTipText(this.TaskId, "正在下载字幕文件"));
					//字幕文件(on)地址
					string subfile = Path.Combine(SaveDirectory.ToString(), title + ".xml");
					//取得字幕文件(on)地址
					string subUrl = "http://www.bilibili.tv/dm," + id + "?r=155";
					//下载字幕文件
					try
					{
						Network.DownloadSub(new DownloadParameter()
							{
								Url = subUrl,
								FilePath = subfile,
								UseDeflate = true,
								Proxy = delegates.Proxy
							});
					}
					catch { }
				}
			}
			catch (Exception ex)
			{
				_status = DownloadStatus.出现错误;
				delegates.Error(new ParaError(this.TaskId, ex));
				return;
			}
		
			//下载成功完成
			_status = DownloadStatus.下载完成;
			delegates.Finish(new ParaFinish(this.TaskId, true));
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

		//下载信息（显示到UI上）
		public string Info
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendLine("TaskId: " + this.TaskId.ToString());
				sb.AppendLine("Url: " + this.Url);
				return sb.ToString();
			}
		}

		#endregion
	}
}
