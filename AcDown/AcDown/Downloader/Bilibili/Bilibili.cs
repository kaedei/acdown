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
using Kaedei.AcDown.Parser;

namespace Kaedei.AcDown.Downloader
{

	[AcDownPluginInformation("BilibiliDownloader", "Bilibili.tv下载插件", "Kaedei", "3.12.0.701", "Bilibili.tv下载插件", "http://blog.sina.com.cn/kaedei")]
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
				"http://www.bilibili.tv/video/av97834/",
				"http://www.bilibili.tv/video/av70229/index_20.html",
			});
			//AutoAnswer
			Feature.Add("AutoAnswer", new List<AutoAnswer>()
			{
				new AutoAnswer("tudou","3","土豆 高清(720P)"),
				new AutoAnswer("youku","mp4","优酷 高清(Mp4)"),
				new AutoAnswer("tudou","99","土豆 原画"),
				new AutoAnswer("youku","hd2","优酷 超清(HD)"),
				new AutoAnswer("youku","flv","优酷 标清(Flv)"),
				new AutoAnswer("tudou","2","土豆 清晰(360P)"),
				new AutoAnswer("tudou","1","土豆 流畅(256P)"),
				new AutoAnswer("bilibili","auto","保留此项可以禁止BiliBili插件显示任何对话框")
			});
			//ConfigurationForm(不支持)
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
		/// 如 "bilibili99999_2"
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public string GetHash(string url)
		{
			Regex r = new Regex(@"(http://(www\.|)bilibili\.(us|tv)/video/|)av(?<av>\d{2,6})(/index_(?<subav>\d+)\.html|)");
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
		public bool Download()
		{
			//开始下载
			delegates.TipText(new ParaTipText(this.Info, "正在分析视频地址"));

			//修正井号
			Info.Url = Info.Url.TrimEnd('#');
			//修正旧版URL
			Info.Url = Info.Url.Replace("bilibili.us", "bilibili.tv");
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
			string suburl = Regex.Match(Info.Url, @"bilibili\.tv(?<part>/video/av\d+/index_\d+\.html)").Groups["part"].Value;

			//是否通过【自动应答】禁用对话框
			bool disableDialog = false;
			if (Info.AutoAnswer != null)
			{
				foreach (var item in Info.AutoAnswer)
				{
					if (item.Prefix == "bilibili")
					{
						if (item.Identify == "auto")
							disableDialog = true;
						break;
					}
				}
			}

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
						user.Username = Encoding.UTF8.GetString(Convert.FromBase64String(Info.Settings["user"]));
						user.Password = Encoding.UTF8.GetString(Convert.FromBase64String(Info.Settings["password"]));
						if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
							throw new Exception();
					}
					catch
					{
						user = ToolForm.CreateLoginForm("https://secure.bilibili.tv/member/index_do.php?fmdo=user&dopost=regnew");
						Info.Settings["user"] = Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Username));
						Info.Settings["password"] = Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Password));
					}
					//Post的数据
					string postdata = "fmdo=login&dopost=login&refurl=http%%3A%%2F%%2Fbilibili.tv%%2F&keeptime=604800&userid=" + user.Username + "&pwd=" + user.Password + "&keeptime=604800";
					byte[] data = Encoding.UTF8.GetBytes(postdata);
					//生成请求
					HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://secure.bilibili.tv/member/index_do.php");
					req.Method = "POST";
					req.Referer = "https://secure.bilibili.tv/login.php";
					req.ContentType = "application/x-www-form-urlencoded";
					req.ContentLength = data.Length;
					req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:11.0) Gecko/20100101 Firefox/11.0";
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
				//文件名称
				string title = mTitle.Groups["title"].Value.Replace("- 嗶哩嗶哩", "").Replace("- ( ゜- ゜)つロ", "").Replace("乾杯~", "").Replace("- bilibili.tv", "").Trim();

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


				Info.Title = title;
				//过滤非法字符
				title = Tools.InvalidCharacterFilter(title, "");
				//重新设置保存目录（生成子文件夹）
				if (!Info.SaveDirectory.ToString().EndsWith(title))
				{
					string newdir = Path.Combine(Info.SaveDirectory.ToString(), title);
					if (!Directory.Exists(newdir)) Directory.CreateDirectory(newdir);
					Info.SaveDirectory = new DirectoryInfo(newdir);
				}


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

				//解析器的解析结果
				ParseResult pr = null;

				//如果不是“仅下载字幕”
				if (Info.DownSub != DownloadSubtitleType.DownloadSubtitleOnly)
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

					//下载弹幕
					bool comment = DownloadComment(title, id);
					if (!comment)
					{
						Info.PartialFinished = true;
						Info.PartialFinishedDetail += "\r\n弹幕文件文件下载失败";
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
								Proxy = Info.Proxy,
								//提取缓存
								ExtractCache = Info.ExtractCache,
								ExtractCachePattern = "fla*.tmp"
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
								Proxy = Info.Proxy,
								//提取缓存
								ExtractCache = Info.ExtractCache,
								ExtractCachePattern = "fla*.tmp"
							};
						}
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
								throw ex;
							}
							else //否则继续下载，设置“部分失败”状态
							{
								Info.PartialFinished = true;
								Info.PartialFinishedDetail += "\r\n文件: " + currentParameter.Url + " 下载失败";
							}
						}

					} //end for
				}//end 判断是否下载视频

				
				//生成AcPlay文件
				string acplay = GenerateAcplayConfig(pr, title);

				//支持导出列表
				StringBuilder sb = new StringBuilder(videos.Length * 2);
				foreach (string item in videos)
				{
					sb.Append(item);
					sb.Append("|");
				}
				if (Info.Settings.ContainsKey("ExportUrl"))
					Info.Settings["ExportUrl"] = sb.ToString();
				else
					Info.Settings.Add("ExportUrl", sb.ToString());
				//支持AcPlay
				if (Info.Settings.ContainsKey("AcPlay"))
					Info.Settings["AcPlay"] = acplay;
				else
					Info.Settings.Add("AcPlay", acplay);

			}
			catch (Exception ex)
			{
				Info.Settings["user"] = "";
				Info.Settings["password"] = "";
				throw ex;
			}

			return true;
		}


		/// <summary>
		/// 生成acplay配置文件
		/// </summary>
		/// <param name="pr">Parser的解析结果</param>
		/// <param name="title">文件标题</param>
		private string GenerateAcplayConfig(ParseResult pr, string title)
		{
			try
			{
				//生成新的配置
				AcPlayConfiguration c = new AcPlayConfiguration();
				//播放器
				c.PlayerName = "bilibili";
				//播放器地址
				c.PlayerUrl = "http://static.loli.my/play.swf";
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
				string path = Path.Combine(Info.SaveDirectory.ToString(), title + ".acplay");
				//序列化到文件中
				using (var fs = new FileStream(path, FileMode.Create))
				{
					XmlSerializer s = new XmlSerializer(typeof(AcPlayConfiguration));
					s.Serialize(fs, c);
				}
				return path;
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
		private bool DownloadComment(string title,string id)
		{
			//如果不是“不下载弹幕”且ID不为空
			if ((Info.DownSub != DownloadSubtitleType.DontDownloadSubtitle) && !string.IsNullOrEmpty(id))
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
				catch
				{
					return false;
				}
			}
			return true;
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
