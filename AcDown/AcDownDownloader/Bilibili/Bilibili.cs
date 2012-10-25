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

namespace Kaedei.AcDown.Downloader
{

	[AcDownPluginInformation("BilibiliDownloader", "Bilibili下载插件", "Kaedei", "4.2.2.1025", "BiliBili下载插件", "http://blog.sina.com.cn/kaedei")]
	public class BilibiliPlugin : IPlugin
	{

		public BilibiliPlugin()
		{
			Feature = new Dictionary<string, object>();
			//ExampleUrl
			Feature.Add("ExampleUrl", new string[] { 
				"Bilibili下载插件:",
				"支持识别各Part名称、支持简写形式",
				"av97834",
				"http://bilibili.kankanews.com//video/av97834/",
				"http://bilibili.kankanews.com/video/av70229/index_20.html",
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
			Regex r = new Regex(@"^(http://(www\.|)bilibili\.(us|tv|kankanews\.com)/video/|)av(?<av>\d{1,6})");
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
		/// 如 "bilibili99999_2"
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public string GetHash(string url)
		{
			Regex r = new Regex(@"(http://(www\.|)bilibili\.(us|tv|kankanews\.com)/video/|)av(?<av>\d{1,6})(/index_(?<subav>\d+)\.html|)");
			Match m = r.Match(url);
			if (m.Success)
			{
				return "bilibili" + m.Groups["av"].Value + "_" + (String.IsNullOrEmpty(m.Groups["subav"].Value) ? "1" : m.Groups["subav"].Value);
			}
			else
			{
				return null;
			}
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
			Info.Url = Info.Url.TrimEnd('#');
			//修正旧版URL(?)
			Info.Url = Info.Url.Replace("bilibili.us", "bilibili.tv");
			Info.Url = Info.Url.Replace("www.bilibili.tv", "bilibili.kankanews.com");
			Info.Url = Info.Url.Replace("bilibili.tv", "bilibili.kankanews.com");
			Info.Url = Info.Url.Replace("bilibili.kankanews.com", "www.bilibili.tv");

			//修正简写URL
			if (Regex.Match(Info.Url, @"^av\d{2,6}$").Success)
				//Info.Url = "http://bilibili.kankanews.com/video/" + Info.Url + "/";
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
			Match mAVNumber = Regex.Match(Info.Url, @"(?<av>av\d+)/index_(?<sub>\d+)\.html");
			Settings["AVNumber"] = mAVNumber.Groups["av"].Value;
			Settings["AVSubNumber"] = mAVNumber.Groups["sub"].Value;
			//设置自定义文件名
			Settings["CustomFileName"] = AcFunPlugin.DefaultFileNameFormat;
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
				//取得网页源文件
				string src = Network.GetHtmlSource(url, Encoding.UTF8, Info.Proxy);
				//type值
				string type = "";
				#region 登录并重新获取网页源文件

				//检查是否需要登录
				if (src.Contains("无权访问")) //需要登录
				{
					CookieContainer cookies = new CookieContainer();
					//登录Bilibili
					UserLoginInfo user;
					//检查插件配置
					try
					{
						user = new UserLoginInfo();
						
							user.Username = Encoding.UTF8.GetString(Convert.FromBase64String(Settings["user"]));
						
							user.Password = Encoding.UTF8.GetString(Convert.FromBase64String(Settings["password"]));
						if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
						{
							user.Username = Encoding.UTF8.GetString(Convert.FromBase64String(Info.BasePlugin.Configuration["Username"]));
							user.Password = Encoding.UTF8.GetString(Convert.FromBase64String(Info.BasePlugin.Configuration["Password"]));
							if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
								throw new Exception("需要登录");
						}
					}
					catch
					{
						user = ToolForm.CreateLoginForm("https://secure.bilibili.tv/member/index_do.php?fmdo=user&dopost=regnew");
						Settings["user"] = Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Username));
						Settings["password"] = Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Password));
					}
					//Post的数据
					string postdata = "act=login&gourl=http%%3A%%2F%%2Fbilibili.tv%%2F&userid=" + user.Username + "&pwd=" + user.Password + "&keeptime=604800";
					byte[] data = Encoding.UTF8.GetBytes(postdata);
					//生成请求
					//WorkItem #1441
					HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://secure.bilibili.tv/login");
					//修复不应用代理服务器的问题
					req.Proxy = Info.Proxy;
					req.Method = "POST";
					req.Referer = "https://secure.bilibili.tv/login.php";
					req.ContentType = "application/x-www-form-urlencoded";
					req.ContentLength = data.Length;
					req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:15.0) Gecko/20100101 Firefox/15.0.1";
					req.Referer = url;
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
					//string cookiesstr = req.CookieContainer.GetCookieHeader(req.RequestUri); //字符串形式的cookies

					//重新请求网页
					HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url.Replace("bilibili.kankanews.com", "www.bilibili.tv"));
					//设置代理服务器
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
				//文件名称
				string title = mTitle.Groups["title"].Value.Replace("- 嗶哩嗶哩", "")
					.Replace("- ( ゜- ゜)つロ", "").Replace("乾杯~", "").Replace("- bilibili.tv", "")
					.Replace("(" + Settings["AVSubNumber"] + ")", "").Trim();
				string subtitle = title;

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

					//如果需要解析关联下载项
					//解析关联项需要同时满足的条件：
					//1.这个任务不是被其他任务所添加的
					//2.用户设置了“解析关联项”
					if (!Info.IsBeAdded)
					{
						if (Info.ParseRelated)
						{
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
								//新建任务
								delegates.NewTask(new ParaNewTask(Info.BasePlugin, u, this.Info));
							}
						}
					}
				}


				Info.Title = title + " - " + subtitle;
				//过滤非法字符
				title = Tools.InvalidCharacterFilter(title, "");
				subtitle = Tools.InvalidCharacterFilter(subtitle, "");

				//清空地址
				Info.FilePath.Clear();
				Info.SubFilePath.Clear();

				//视频id
				string id = "";

				//分析id和视频存放站点(type)
				//取得"bofqi块的源代码
				Regex rEmbed = new Regex("<div class=\"scontent\" id=\"bofqi\">(?<content>.*?)</div>", RegexOptions.Singleline);
				Match mEmbed = rEmbed.Match(src);
				string embedSrc = mEmbed.Groups["content"].Value.Replace("type=\"application/x-shockwave-flash\"", "");

				//检查"file"参数
				Regex rFile = new Regex("file=(\"|)(?<file>.+?)(\"|&)");
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


				//解析Bilibili接口设置
				string interfacexml =
					type.Equals("cid") ?
					Network.GetHtmlSource("http://interface.bilibili.tv/player?id=cid:" + id, Encoding.UTF8, Info.Proxy) :
					Network.GetHtmlSource("http://interface.bilibili.tv/player?id=" + id, Encoding.UTF8, Info.Proxy);
				MatchCollection mcInterfaceSettings = Regex.Matches(interfacexml, @"\<(?<key>\w+)>(?<value>.+?)\</\1\>");
				foreach (Match mInterfaceSetting in mcInterfaceSettings)
				{
					Settings[mInterfaceSetting.Groups["key"].Value] = mInterfaceSetting.Groups["value"].Value;
				}

				//下载弹幕
				bool comment = DownloadComment(title, subtitle, Settings["chatid"]);
				if (!comment)
				{
					Info.PartialFinished = true;
					Info.PartialFinishedDetail += "\r\n弹幕文件文件下载失败";
				}

				//解析器的解析结果
				ParseResult pr = null;

				//如果允许下载视频
				if ((Info.DownloadTypes & DownloadType.Video) != 0)
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
							case "cid": //Bilibili接口 WorkItem #1412
								BilibiliInterfaceParser parserBili = new BilibiliInterfaceParser();
								pr = parserBili.Parse(new ParseRequest() { Id = id, Proxy = Info.Proxy, AutoAnswers = Info.AutoAnswer });
								videos = pr.ToArray();
								break;
							case "qid": //QQ视频
								//解析视频
								QQVideoParser parserQQ = new QQVideoParser();
								pr = parserQQ.Parse(new ParseRequest() { Id = id, Proxy = Info.Proxy, AutoAnswers = Info.AutoAnswer });
								videos = pr.ToArray();
								break;
							case "ykid": //优酷视频
								//解析视频
								YoukuParser parserYouKu = new YoukuParser();
								pr = parserYouKu.Parse(new ParseRequest() { Id = id, Proxy = Info.Proxy, AutoAnswers = Info.AutoAnswer });
								videos = pr.ToArray();
								break;
							case "uid": //土豆视频
								//解析视频
								TudouParser parserTudou = new TudouParser();
								pr = parserTudou.Parse(new ParseRequest() { Id = id, Proxy = Info.Proxy, AutoAnswers = Info.AutoAnswer });
								videos = pr.ToArray();
								break;
							case "data": //Flash游戏
								id = id.Replace("\"", "");
								videos = new string[] { id };
								break;
							case "rid": //六间房
								//不支持
								break;
							case "id": //Levelup视频 WorkItem #1442
								string levelupUrl = @"http://pl.bilibili.tv/" + id.Replace("levelup", "/") + ".flv";
								videos = new string[] { levelupUrl };
								break;
							default: //新浪视频
								SinaVideoParser parserSina = new SinaVideoParser();
								pr = parserSina.Parse(new ParseRequest() { Id = id, Proxy = Info.Proxy, AutoAnswers = Info.AutoAnswer });
								videos = pr.ToArray();
								break;
						}
					}
					else //如果是游戏
					{
						string flashurl = mFlash.Groups["flash"].Value;
						videos = new string[] { flashurl };
					}

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
										title, subtitle, Info.PartCount == 1 ? "" : Info.CurrentPart.ToString(),
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
						catch (Exception ex) //下载文件时出现错误
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
					string acplay = GenerateAcplayConfig(pr, title, subtitle);
					//支持AcPlay直接播放
					Settings["AcPlay"] = acplay;
				}

			}
			catch (Exception ex)
			{
				Settings["user"] = "";
				Settings["password"] = "";
				throw;
			}

			return true;
		}


		/// <summary>
		/// 生成acplay配置文件
		/// </summary>
		/// <param name="pr">Parser的解析结果</param>
		/// <param name="title">文件标题</param>
		private string GenerateAcplayConfig(ParseResult pr, string title, string subtitle)
		{
			try
			{
				//生成新的配置
				AcPlayConfiguration c = new AcPlayConfiguration();
				//播放器
				c.PlayerName = "bilibili";
				//播放器地址
				c.PlayerUrl = "http://static.hdslb.com/play.swf";
				//端口
				c.HttpServerPort = 7776;
				c.ProxyServerPort = 7777;
				//视频
				c.Videos = new Video[Info.FilePath.Count];
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
