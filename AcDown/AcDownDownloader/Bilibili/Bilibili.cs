using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Kaedei.AcDown.Interface;
using Kaedei.AcDown.Interface.AcPlay;
using Kaedei.AcDown.Interface.Forms;
using System.Windows.Forms;
using Kaedei.AcDown.Interface.Downloader;
using Kaedei.AcDown.Downloader.Bilibili;
using System.Xml;

namespace Kaedei.AcDown.Downloader
{

	[AcDownPluginInformation("BilibiliDownloader", "Bilibili下载插件", "Kaedei", "4.5.0.928", "BiliBili下载插件", "http://blog.sina.com.cn/kaedei")]
	public class BilibiliPlugin : IPlugin
	{
		//地址解析正则表达式
		public static Regex RegexBili = new Regex(@"(http://www\.bilibili\.tv/video\/)?av(?<id>[0-9]+)(/index_(?<page>[0-9]+)\.html)?", RegexOptions.IgnoreCase);
		public static Regex RegexAcg = new Regex(@"(http://acg\.tv/)?av(?<id>[0-9]+)(,(?<page>[0-9]+))?", RegexOptions.IgnoreCase);
		public static String AppKey = @"876fe0ebd0e67a0f";

		public BilibiliPlugin()
		{
			Feature = new Dictionary<string, object>();
			//ExampleUrl
			Feature.Add("ExampleUrl", new string[] { 
				"Bilibili下载插件:",
				"支持识别各Part名称、支持简写形式",
				"av97834",
				"av97834,2",
				"http://www.bilibili.tv/video/av97834/",
				"http://www.bilibili.tv/video/av70229/index_20.html",
				"http://acg.tv/av97834/",
				"http://acg.tv/av70229/index_20.html"
			});
			//AutoAnswer
			Feature.Add("AutoAnswer", new List<AutoAnswer>()
			{
				new AutoAnswer("tudou","4","土豆 超清"),
				new AutoAnswer("youku","mp4","优酷 高清(Mp4)"),
				new AutoAnswer("tudou","3","土豆 高清"),
				new AutoAnswer("tudou","99","土豆 原画"),
				new AutoAnswer("youku","hd2","优酷 超清(HD)"),
				new AutoAnswer("youku","flv","优酷 标清(Flv)"),
				new AutoAnswer("tudou","2","土豆 清晰"),
				new AutoAnswer("tudou","1","土豆 流畅"),
				new AutoAnswer("bilibili","auto","保留此项可以禁止BiliBili插件显示任何对话框")
			});
			//ConfigForm 属性设置窗口
			Feature.Add("ConfigForm", new MethodInvoker(() =>
			{
				new Bilibili.BilibiliDownloaderConfigurationForm(Configuration).ShowDialog();
			}));
		}
		public IDownloader CreateDownloader()
		{
			return new BilibiliDownloader();
		}

		public bool CheckUrl(string url)
		{
			return RegexBili.IsMatch(url) || RegexAcg.IsMatch(url);
		}

		/// <summary>
		/// 规则为 bilibili + 视频ID + 下划线 + 子视频编号
		/// 如 "bilibili99999_2"
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public string GetHash(string url)
		{
			String id = "";
			String page = "";
			var b = RegexBili.Match(url);
			var acg = RegexAcg.Match(url);
			if (b.Success)
			{
				id = b.Groups["id"].Value;
				page = b.Groups["page"].Value;
			}
			else if (acg.Success)
			{
				id = acg.Groups["id"].Value;
				page = acg.Groups["page"].Value;
			}
			return "bilibili" + id + "_" + (String.IsNullOrEmpty(page) ? "1" : page);
		}

		public Dictionary<string, object> Feature
		{
			get;
			private set;
		}

		public SerializableDictionary<string, string> Configuration
		{
			get;
			set;
		}

		public static readonly string DefaultFileNameFormat = "标题" + Path.DirectorySeparatorChar + "子标题(分段).扩展名";

	} //end class

	/// <summary>
	/// Bilibili下载器
	/// </summary>
	public class BilibiliDownloader : CommonDownloader
	{

		/// <summary>
		/// 下载视频
		/// </summary>
		public override bool Download()
		{
			//开始下载
			TipText("正在分析视频地址");

			//修正井号
			Info.Url = Info.Url.ToLower().TrimEnd('#');
			//修正旧版URL
			Info.Url = Info.Url.Replace("bilibili.us", "bilibili.tv");
			Info.Url = Info.Url.Replace("bilibili.smgbb.cn", "www.bilibili.tv");
			Info.Url = Info.Url.Replace("bilibili.kankanews.com", "www.bilibili.tv");

			//修正简写URL
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
			//string suburl = Regex.Match(Info.Url, @"bilibili\.kankanews\.com(?<part>/video/av\d+/index_\d+\.html)").Groups["part"].Value;
			string suburl = Regex.Match(Info.Url, @"www\.bilibili\.tv(?<part>/video/av\d+/index_\d+\.html)").Groups["part"].Value;
			//取得AV号和子编号
			//Match mAVNumber = Regex.Match(Info.Url, @"(?<av>av\d+)/index_(?<sub>\d+)\.html");
			Match mAVNumber = BilibiliPlugin.RegexBili.Match(Info.Url);
			if (!mAVNumber.Success) mAVNumber = BilibiliPlugin.RegexAcg.Match(Info.Url);
			Settings["AVNumber"] = mAVNumber.Groups["id"].Value;
			Settings["AVSubNumber"] = mAVNumber.Groups["page"].Value;
			//设置自定义文件名
			Settings["CustomFileName"] = BilibiliPlugin.DefaultFileNameFormat;
			if (Info.BasePlugin.Configuration.ContainsKey("CustomFileName"))
			{
				Settings["CustomFileName"] = Info.BasePlugin.Configuration["CustomFileName"];
			}

			//是否通过【自动应答】禁用对话框
			bool disableDialog = AutoAnswer.IsInAutoAnswers(Info.AutoAnswer, "bilibili", "auto");

			//视频地址数组
			string[] videos = null;

			try
			{
				//解析关联项需要同时满足的条件：
				//1.这个任务不是被其他任务所添加的
				//2.用户设置了“解析关联项”
				if (!Info.IsBeAdded || Info.ParseRelated)
				{

					//取得网页源文件
					string src = Network.GetHtmlSource(url, Encoding.UTF8, Info.Proxy);
					string subtitle = "";
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
								subtitle = item.Groups["content"].Value;
								break;
							}
						}

						//准备(地址-标题)字典
						var dict = new Dictionary<string, string>();
						foreach (Match item in mSubTitles)
						{
							if (suburl != item.Groups["part"].Value)
							{
								dict.Add(url.Replace(suburl, item.Groups["part"].Value),
											item.Groups["content"].Value);
							}
						}
						//用户选择任务
						var ba = new Collection<string>();
						if (!disableDialog)
							ba = ToolForm.CreateMultiSelectForm(dict, Info.AutoAnswer, "bilibili");
						//根据用户选择新建任务
						foreach (string u in ba)
						{
							NewTask(u);
						}

					}
				}

				//获取视频信息API
				var apiAddress = @"http://api.bilibili.tv/view?type=xml&appkey=" + BilibiliPlugin.AppKey + "&id=" +
				                 Settings["AVNumber"] + "&page=" + Settings["AVSubNumber"];
				var webrequest = (HttpWebRequest) WebRequest.Create(apiAddress);
				webrequest.UserAgent = "AcDown/" + Application.ProductVersion + " (kaedei@foxmail.com)";
				webrequest.Proxy = Info.Proxy;
				var viewSrc = Network.GetHtmlSource(webrequest, Encoding.UTF8);
				//登录获取API结果
				if (viewSrc.Contains("<code>-403</code>"))
				{
					viewSrc = LoginApi(url, apiAddress);
				}
				var viewDoc = new XmlDocument();
				viewDoc.LoadXml(viewSrc);
				//视频标题和子标题
				string title = viewDoc.SelectSingleNode(@"/info/title").InnerText.Replace("&amp;", "&");
				string stitle = viewDoc.SelectSingleNode(@"/info/partname").InnerText.Replace("&amp;", "&");

				if (String.IsNullOrEmpty(stitle))
				{
					Info.Title = title;
					stitle = title;
				}
				else
					Info.Title = title + " - " + stitle;
				//过滤非法字符
				title = Tools.InvalidCharacterFilter(title, "");
				stitle = Tools.InvalidCharacterFilter(stitle, "");

				//清空地址
				Info.FilePath.Clear();
				Info.SubFilePath.Clear();

				//CID
				Settings["chatid"] = Regex.Match(viewSrc, @"(?<=\<cid\>)\d+(?=\</cid\>)", RegexOptions.IgnoreCase).Value;
				
				//下载弹幕
				DownloadComment(title, stitle, Settings["chatid"]);

				//解析器的解析结果
				ParseResult pr = null;

				//如果允许下载视频
				if ((Info.DownloadTypes & DownloadType.Video) != 0)
				{
					//var playurlSrc = Network.GetHtmlSource(@"http://interface.bilibili.tv/playurl?otype=xml&cid=" + Settings["chatid"] + "&type=flv", Encoding.UTF8);
					//var playurlDoc = new XmlDocument();
					//playurlDoc.LoadXml(playurlSrc);

					//获得视频列表
					var prRequest = new ParseRequest()
						{
							Id = Settings["chatid"],
							Proxy = Info.Proxy,
							AutoAnswers = Info.AutoAnswer
						};
					pr = new BilibiliInterfaceParser().Parse(prRequest);
					videos = pr.ToArray();

					//支持导出列表
					if (videos != null)
					{
						StringBuilder sb = new StringBuilder(videos.Length * 2);
						foreach (string item in videos)
						{
							sb.Append(item);
							sb.Append("|");
						}
						if (Settings.ContainsKey("ExportUrl"))
							Settings["ExportUrl"] = sb.ToString();
						else
							Settings.Add("ExportUrl", sb.ToString());
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

						//设置文件名
						var renamehelper = new CustomFileNameHelper();
						string filename = renamehelper.CombineFileName(Settings["CustomFileName"],
										title, stitle, Info.PartCount == 1 ? "" : Info.CurrentPart.ToString(),
										ext.Replace(".", ""), Settings["AVNumber"], Settings["AVSubNumber"]);
						filename = Path.Combine(Info.SaveDirectory.ToString(), filename);

						//生成父文件夹
						if (!Directory.Exists(Path.GetDirectoryName(filename)))
							Directory.CreateDirectory(Path.GetDirectoryName(filename));

						//设置当前DownloadParameter
						currentParameter = new DownloadParameter()
						{
							//文件名 例: c:\123(1).flv
							FilePath = filename,
							//文件URL
							Url = videos[i],
							//代理服务器
							Proxy = Info.Proxy,
							//提取缓存
							ExtractCache = Info.ExtractCache,
							ExtractCachePattern = "fla*.tmp"
						};

						//添加文件路径到List<>中
						Info.FilePath.Add(currentParameter.FilePath);
						//下载文件
						bool success;

						//提示更换新Part
						delegates.NewPart(new ParaNewPart(this.Info, i + 1));

						//下载视频
						try
						{
							success = Network.DownloadFile(currentParameter, this.Info);
							if (!success) //未出现错误即用户手动停止
							{
								return false;
							}
						}
						catch //下载文件时出现错误
						{
							//如果此任务由一个视频组成,则报错（下载失败）
							if (Info.PartCount == 1)
							{
								throw;
							}
							else //否则继续下载，设置“部分失败”状态
							{
								Info.PartialFinished = true;
								Info.PartialFinishedDetail += "\r\n文件: " + currentParameter.Url + " 下载失败";
							}
						}

					} //end for
				}//end 判断是否下载视频


				//如果插件设置中没有GenerateAcPlay项，或此项设置为true则生成.acplay快捷方式
				if (!Info.BasePlugin.Configuration.ContainsKey("GenerateAcPlay") ||
					Info.BasePlugin.Configuration["GenerateAcPlay"] == "true")
				{
					//生成AcPlay文件
					string acplay = GenerateAcplayConfig(pr, title, stitle);
					//支持AcPlay直接播放
					Settings["AcPlay"] = acplay;
				}

				//生成视频自动合并参数
				if (Info.FilePath.Count > 1 && !Info.PartialFinished)
				{
					Info.Settings.Remove("VideoCombine");
					var arg = new StringBuilder();
					foreach (var item in Info.FilePath)
					{
						arg.Append(item);
						arg.Append("|");
					}

					var renamehelper = new CustomFileNameHelper();
					string filename = renamehelper.CombineFileName(Settings["CustomFileName"],
									title, stitle, "",
									"mp4", Info.Settings["AVNumber"], Info.Settings["AVSubNumber"]);
					filename = Path.Combine(Info.SaveDirectory.ToString(), filename);

					arg.Append(filename);
					Info.Settings["VideoCombine"] = arg.ToString();
				}

			}
			catch
			{
				Settings["user"] = "";
				Settings["password"] = "";
				throw;
			}

			return true;
		}

		private string LoginApi(string url, string apiAddress)
		{
			string xmlSrc;
			var LOGIN_PAGE = "https://secure.bilibili.tv/login";
			//获取登录页Cookie
			var loginPageRequest = (HttpWebRequest) WebRequest.Create(LOGIN_PAGE);
			loginPageRequest.Proxy = Info.Proxy;
			loginPageRequest.Referer = @"http://www.bilibili.tv/";
			loginPageRequest.UserAgent = @"Mozilla/5.0 (Windows NT 6.1; rv:21.0) Gecko/20100101 Firefox/21.0";
			loginPageRequest.Accept = @"text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
			CookieContainer loginPageCookieContainer;
			string loginPageCookie;
			using (var resp = loginPageRequest.GetResponse())
			{
				loginPageCookieContainer = new CookieContainer();
				var sid = Regex.Match(resp.Headers["Set-Cookie"], @"(?<=sid=)\w+").Value;
				loginPageCookieContainer.Add(new Cookie("sid", sid, "/", ".bilibili.tv"));
				loginPageCookie = loginPageCookieContainer.GetCookieHeader(new Uri(LOGIN_PAGE));
			}
			//获取验证码图片
			var loginInfo = new UserLoginInfo();
			if (Info.BasePlugin.Configuration.ContainsKey("Username"))
				loginInfo.Username = Encoding.UTF8.GetString(Convert.FromBase64String(Info.BasePlugin.Configuration["Username"]));
			if (Info.BasePlugin.Configuration.ContainsKey("Password"))
				loginInfo.Password = Encoding.UTF8.GetString(Convert.FromBase64String(Info.BasePlugin.Configuration["Password"]));
			if (Settings.ContainsKey("Username"))
				loginInfo.Username = Settings["Username"];
			if (Settings.ContainsKey("Password"))
				loginInfo.Password = Settings["Password"];

			var captchaUrl = @"https://secure.bilibili.tv/captcha?r=" +
			                 new Random(Environment.TickCount).NextDouble().ToString();
			var captchaFile = Path.GetTempFileName() + ".png";
			var captchaClient = new WebClient {Proxy = Info.Proxy};
			captchaClient.Headers.Add(HttpRequestHeader.Cookie, loginPageCookie);
			captchaClient.DownloadFile(captchaUrl, captchaFile);
			loginInfo = ToolForm.CreateLoginForm(loginInfo, @"https://secure.bilibili.tv/register", captchaFile);

			//保存到设置
			Settings["Username"] = loginInfo.Username;
			Settings["Password"] = loginInfo.Password;


			string postString = @"act=login&gourl=http%%3A%%2F%%2Fbilibili.tv%%2F&userid=" + loginInfo.Username + "&pwd=" +
			                    loginInfo.Password +
			                    "&vdcode=" + loginInfo.Captcha.ToUpper() + "&keeptime=2592000";
			byte[] data = Encoding.UTF8.GetBytes(postString);

			var loginRequest = (HttpWebRequest) WebRequest.Create(@"https://secure.bilibili.tv/login");
			loginRequest.Proxy = Info.Proxy;
			loginRequest.Method = "POST";
			loginRequest.Referer = "https://secure.bilibili.tv/login";
			loginRequest.ContentType = "application/x-www-form-urlencoded";
			loginRequest.ContentLength = data.Length;
			loginRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:21.0) Gecko/20100101 Firefox/21.0";
			loginRequest.Referer = url;
			loginRequest.CookieContainer = loginPageCookieContainer;

			//发送POST数据
			using (var outstream = loginRequest.GetRequestStream())
			{
				outstream.Write(data, 0, data.Length);
				outstream.Flush();
			}
			//关闭请求
			loginRequest.GetResponse().Close();
			var cookies = loginRequest.CookieContainer.GetCookieHeader(new Uri(LOGIN_PAGE));

			var client = new WebClient {Proxy = Info.Proxy};
			client.Headers.Add(HttpRequestHeader.Cookie, cookies);

			var apiRequest = (HttpWebRequest) WebRequest.Create(apiAddress);
			apiRequest.Proxy = Info.Proxy;
			apiRequest.Headers.Add(HttpRequestHeader.Cookie, cookies);
			xmlSrc = Network.GetHtmlSource(apiRequest, Encoding.UTF8);
			return xmlSrc;
		}


		/// <summary>
		/// 生成acplay配置文件
		/// </summary>
		/// <param name="pr">Parser的解析结果</param>
		/// <param name="title">文件标题</param>
		private string GenerateAcplayConfig(ParseResult pr, string title, string subtitle)
		{
			if (Tools.IsRunningOnMono)
				return "";
			try
			{
				//生成新的配置
				var c = new AcPlayConfiguration
					{
						PlayerName = "bilibili",
						PlayerUrl = "http://static.hdslb.com/play.swf",
						HttpServerPort = 7776,
						ProxyServerPort = 7777,
						Videos = new Video[Info.FilePath.Count],
					    WebUrl = Info.Url
					};
				//视频
				for (int i = 0; i < Info.FilePath.Count; i++)
				{
					c.Videos[i] = new Video();
					c.Videos[i].FileName = Path.GetFileName(Info.FilePath[i]);
					if (pr != null)
						if (pr.Items[i].Information.ContainsKey("length"))
							c.Videos[i].Length = int.Parse(pr.Items[i].Information["length"]);
					if (pr != null)
						if (pr.Items[i].Information.ContainsKey("order"))
							c.Videos[i].Order = int.Parse(pr.Items[i].Information["order"]);
				}
				//弹幕
				c.Subtitles = new string[Info.SubFilePath.Count];
				for (int i = 0; i < Info.SubFilePath.Count; i++)
				{
					c.Subtitles[i] = Path.GetFileName(Info.SubFilePath[i]);
				}
				//其他
				c.ExtraConfig = new SerializableDictionary<string, string>();
				if (pr != null)
					if (pr.SpecificResult.ContainsKey("totallength")) //totallength
						c.ExtraConfig.Add("totallength", pr.SpecificResult["totallength"]);
				if (pr != null)
					if (pr.SpecificResult.ContainsKey("src")) //src
						c.ExtraConfig.Add("src", pr.SpecificResult["src"]);
				if (pr != null)
					if (pr.SpecificResult.ContainsKey("framecount")) //framecount
						c.ExtraConfig.Add("framecount", pr.SpecificResult["framecount"]);

				//配置文件的生成地址
				var renamehelper = new CustomFileNameHelper();
				string filename = renamehelper.CombineFileName(Settings["CustomFileName"],
								title, subtitle, "", "acplay", Settings["AVNumber"], Settings["AVSubNumber"]);
				filename = Path.Combine(Info.SaveDirectory.ToString(), filename);
				Info.FilePath.Add(filename);
				//序列化到文件中
				using (var fs = new FileStream(filename, FileMode.Create))
				{
					XmlSerializer s = new XmlSerializer(typeof(AcPlayConfiguration));
					s.Serialize(fs, c);
				}
				return filename;
			}
			catch
			{
				return "";
			}
		}


		/// <summary>
		/// 下载弹幕
		/// </summary>
		/// <param name="title">文件名</param>
		/// <returns>是否下载成功</returns>
		private bool DownloadComment(string title, string subtitle, string id)
		{
			//如果不是“不下载弹幕”且ID不为空
			if (((Info.DownloadTypes & DownloadType.Subtitle) != 0) && (!string.IsNullOrEmpty(id)))
			{
				//设置文件名
				var renamehelper = new CustomFileNameHelper();

				//----------下载字幕-----------
				TipText("正在下载字幕文件");
				//字幕文件(on)地址
				string filename = renamehelper.CombineFileName(Settings["CustomFileName"],
								title, subtitle, "", "xml", Settings["AVNumber"], Settings["AVSubNumber"]);
				filename = Path.Combine(Info.SaveDirectory.ToString(), filename);
				//生成父文件夹
				if (!Directory.Exists(Path.GetDirectoryName(filename)))
					Directory.CreateDirectory(Path.GetDirectoryName(filename));
				Info.SubFilePath.Add(filename);
				//取得字幕文件地址
				string subUrl = "http://comment.bilibili.tv/" + id + ".xml"; //WorkItem #1410

				//下载字幕文件
				try
				{
					Network.DownloadFile(new DownloadParameter()
					{
						Url = subUrl,
						FilePath = filename,
						Proxy = Info.Proxy
					});
				}
				catch
				{
					return false;
				}
			}
			return true;
		}

	}
}
