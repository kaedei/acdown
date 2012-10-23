using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;
using System.IO;
using Kaedei.AcDown.Interface.Forms;
using System.Net;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using Kaedei.AcDown.Interface.AcPlay;
using System.Windows.Forms;
using Kaedei.AcDown.Interface.Downloader;

namespace Kaedei.AcDown.Downloader
{
	/// <summary>
	/// AcFun下载支持插件
	/// </summary>
	[AcDownPluginInformation("AcfunDownloader", "Acfun.tv下载插件", "Kaedei", "4.2.1.1016", "Acfun.tv下载插件", "http://blog.sina.com.cn/kaedei")]
	public class AcFunPlugin : IPlugin
	{
		public AcFunPlugin()
		{
			Feature = new Dictionary<string, object>();
			//GetExample
			Feature.Add("ExampleUrl", new string[] { 
				"AcFun.tv下载插件:",
				"支持识别各Part名称、支持简写形式",
				"ac206020",
				"http://acfun.tv/v/ac206020",
				"http://www.acfun.tv/v/ac206020",
				"http://124.228.254.229/v/ac206020 (IP地址形式)"
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
				new AutoAnswer("acfun","auto","保留此项可以禁止Acfun插件显示任何对话框")
			});
			//ConfigForm 属性设置窗口
			Feature.Add("ConfigForm", new MethodInvoker(() =>
			{
				new AcFun.AcfunDownloaderConfigurationForm(Configuration).ShowDialog();
			}));
		}

		public IDownloader CreateDownloader()
		{
			return new AcfunDownloader();
		}

		public bool CheckUrl(string url)
		{
			Regex r = new Regex(@"^((http://|)(www\.|)acfun.tv/v/|)ac\d+(_\d+|/index(_\d+|)\.html|)");
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
		/// 规则为 acfun + 视频ID + 下划线 + 子视频编号(无子视频编号时默认为1)
		/// 如 "acfun158539_2"
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public string GetHash(string url)
		{
			Regex r = new Regex(@"ac(?<id>\d+)(_(?<subid>\d+)|/index(_(?<subid2>\d+)|)\.html|)");
			Match m = r.Match(url);
			if (m.Success)
			{
				if (m.Groups["subid"] != null)
					return "acfun" + m.Groups["id"].Value + "_" + m.Groups["subid"].Value;
				else if (m.Groups["subid2"] != null)
					return "acfun" + m.Groups["id"].Value + "_" + m.Groups["subid2"].Value;
				else
					return "acfun" + m.Groups["id"].Value + "_1";
			}
			else
			{
				return null;
			}
		}

		public Dictionary<string, object> Feature { get; private set; }

		public SerializableDictionary<string, string> Configuration { get; set; }

		public static readonly string DefaultFileNameFormat = "标题" + Path.DirectorySeparatorChar + "子标题(分段).扩展名";
	}

	/// <summary>
	/// Acfun下载器
	/// </summary>
	public class AcfunDownloader : CommonDownloader
	{

		/// <summary>
		/// 下载视频
		/// </summary>
		/// <returns></returns>
		public override bool Download()
		{
			//开始下载
			delegates.TipText(new ParaTipText(this.Info, "正在分析视频地址"));

			//修正井号
			Info.Url = Info.Url.TrimEnd('#');

			//修正简写URL
			if (Regex.Match(Info.Url, @"^ac\d+$").Success)
				Info.Url = "http://www.acfun.tv/v/" + Info.Url;
			else if (!Info.Url.StartsWith("http://", StringComparison.CurrentCultureIgnoreCase))
				Info.Url = "http://" + Info.Url;

			//修正URL为 http://www.acfun.tv/v/ac12345_67 形式
			Info.Url = Info.Url.Replace(".html", "").Replace("/index", "");
			if (!Info.Url.Contains("_"))
			{
				Info.Url += "_1";
			}

			//取得AC号和子编号
			Match mACNumber = Regex.Match(Info.Url, @"(?<ac>ac\d+)_(?<sub>\d+)");
			Settings["ACNumber"] = mACNumber.Groups["ac"].Value;
			Settings["ACSubNumber"] = mACNumber.Groups["sub"].Value;
			//设置自定义文件名
			Settings["CustomFileName"] = AcFunPlugin.DefaultFileNameFormat;
			if (Info.BasePlugin.Configuration.ContainsKey("CustomFileName"))
			{
				Settings["CustomFileName"] = Info.BasePlugin.Configuration["CustomFileName"];
			}

			//是否通过【自动应答】禁用对话框
			bool disableDialog = false;
			disableDialog = AutoAnswer.IsInAutoAnswers(Info.AutoAnswer, "acfun", "auto");

			try
			{
				//取得网页源文件
				string src = Network.GetHtmlSource(Info.Url, Encoding.UTF8, Info.Proxy);

				//取得embed块的源代码
				Regex rEmbed = new Regex(@"\<div id=""area-player"".+?\</div\>", RegexOptions.Singleline);
				Match mEmbed = rEmbed.Match(src);
				string embedSrc = mEmbed.ToString().Replace("type=\"application/x-shockwave-flash\"", "");

				//检查是否为Flash游戏
				Regex rFlash = new Regex(@"src=""(?<player>.+?)\.swf""");
				Match mFlash = rFlash.Match(embedSrc);

				//#region 取得当前Flash播放器地址
				//Match mFlashPlayer = Regex.Match(src, @"http://static.acfun.tv/player/.+?\.swf");
				//if (mFlashPlayer.Success)
				//	Settings["PlayerUrl"] = mFlashPlayer.Value;
				//else
				Settings["PlayerUrl"] = @"http://static.acfun.tv/player/ACFlashPlayer.201209271950.swf";
				
				//#endregion

				TipText("正在获取视频详细信息");
				//如果是Flash游戏
				if (mFlash.Success && !mFlash.Value.Contains("newflvplayer"))
				{
					Settings["vtype"] = "game";
				}
				else
				{
					//取得acfun id值
					Regex rAcfunId = new Regex(@"(?<=\[video\])\d+(?=\[/video\])", RegexOptions.IgnoreCase);
					Match mAcfunId = rAcfunId.Match(embedSrc);
					if (mAcfunId.Success)
					{
						Settings["acfunid"] = mAcfunId.Value;
						//获取跳转
						string getvideobyid = Network.GetHtmlSource("http://www.acfun.tv/api/getVideoByID.aspx?vid=" + Settings["acfunid"], Encoding.UTF8);

						//将信息添加到Setting中
						//Regex rVideoInfo = new Regex(@"""(?<key>.+?)"":(""|)(?<value>.+?)(""|)[,|}]");
						//MatchCollection mcVideoInfo = rVideoInfo.Matches(getvideobyid);
						//foreach (Match mVideoInfo in mcVideoInfo)
						//{
						//	string key = mVideoInfo.Groups["key"].Value;
						//	string value = mVideoInfo.Groups["value"].Value;
						//	if (Info.Settings.ContainsKey(key))
						//		Info.Settings[key] = value;
						//	else
						//		Info.Settings.Add(key, value);
						//}

						Settings["uid"] = Regex.Match(getvideobyid, @"(?<=""uid"":)\d+").Value;
						Settings["id"] = Regex.Match(getvideobyid, @"(?<=""id"":)\d+").Value;
						Settings["vtype"] = Regex.Match(getvideobyid, @"(?<=""vtype"":"")\w+").Value;
						Settings["aid"] = Regex.Match(getvideobyid, @"(?<=""aid"":)\d+").Value;
						Settings["vid"] = Regex.Match(getvideobyid, @"(?<=""vid"":"")\w+").Value;
						Settings["cid"] = Regex.Match(getvideobyid, @"(?<=""cid"":"")\w+").Value;
					}
					else
					{
						string vid = Regex.Match(embedSrc, @"(?<=id=)\w+").Value;
						Settings["id"] = vid;
						Settings["vid"] = vid;
						Settings["cid"] = vid;
						Settings["vtype"] = "sina";
					}

				}

				//取得视频标题
				//Regex rTitle = new Regex(@"<title>(?<title>.*)</title>");
				Regex rTitle = new Regex(@"(?<=system.title = "").+?(?="";)");
				Match mTitle = rTitle.Match(src);
				//Settings["Title"] = mTitle.Groups["title"].Value.Replace(" - acfun", "");
				Settings["Title"] = mTitle.Value;

				//过滤非法字符
				Settings["Title"] = Tools.InvalidCharacterFilter(Settings["Title"], "");

				//取得当前视频子标题
				Match mSubtitle = Regex.Match(src, @"<a class=""pager active"" href=""(?<part>.+?)""><i.+?</i>(?<content>.+?)</a>");
				if (mSubtitle.Success)
				{
					Settings["Subtitle"] = mSubtitle.Groups["content"].Value;
					Settings["Subtitle"] = Tools.InvalidCharacterFilter(Settings["Subtitle"], "");
					Info.Title = Settings["Title"] + " - " + Settings["Subtitle"];
				}
				else
				{
					Settings["Subtitle"] = Settings["Title"];
					Info.Title = Settings["Title"];
				}

				TipText("正在分析关联视频");

				//取得所有子标题
				Regex rSubTitles = new Regex(@"<a class=""pager.*?"" href=""(?<part>.+?)""><i.+?</i>(?<content>.+?)</a>");
				MatchCollection mSubTitles = rSubTitles.Matches(src);

				//如果存在下拉列表框
				if (mSubTitles.Count > 0)
				{
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
								dict.Add("http://www.acfun.tv" + item.Groups["part"].Value,
											item.Groups["content"].Value);
							}
							//用户选择任务
							var ba = new Collection<string>();
							if (!disableDialog)
								ba = ToolForm.CreateMultiSelectForm(dict, Info.AutoAnswer, "acfun");
							//根据用户选择新建任务
							foreach (string u in ba)
							{
								//新建任务
								delegates.NewTask(new ParaNewTask(Info.BasePlugin, u, this.Info));
							}
						}
					}
				}




				//视频地址数组
				string[] videos = null;
				//清空地址
				Info.FilePath.Clear();
				Info.SubFilePath.Clear();


				//下载弹幕
				bool comment = DownloadSubtitle();
				if (!comment)
				{
					Info.PartialFinished = true;
					Info.PartialFinishedDetail += "\r\n弹幕文件文件下载失败";
				}

				TipText("正在解析视频源地址");
				//解析器的解析结果
				ParseResult pr = null;

				//如果允许下载视频
				if ((Info.DownloadTypes & DownloadType.Video) != 0)
				{
					//检查type值
					switch (Settings["vtype"])
					{
						case "sina": //新浪视频
							//解析视频
							SinaVideoParser parserSina = new SinaVideoParser();
							pr = parserSina.Parse(new ParseRequest() { Id = Info.Settings["vid"], Proxy = Info.Proxy, AutoAnswers = Info.AutoAnswer });
							videos = pr.ToArray();
							break;
						case "qq": //QQ视频
							//解析视频
							QQVideoParser parserQQ = new QQVideoParser();
							pr = parserQQ.Parse(new ParseRequest() { Id = Info.Settings["vid"], Proxy = Info.Proxy, AutoAnswers = Info.AutoAnswer });
							videos = pr.ToArray();
							break;
						case "youku": //优酷视频
							//解析视频
							YoukuParser parserYouKu = new YoukuParser();
							pr = parserYouKu.Parse(new ParseRequest() { Id = Info.Settings["vid"], Proxy = Info.Proxy, AutoAnswers = Info.AutoAnswer });
							videos = pr.ToArray();
							break;
						case "tudou": //土豆视频
							TudouParser parserTudou = new TudouParser();
							pr = parserTudou.Parse(new ParseRequest() { Id = Info.Settings["vid"], Proxy = Info.Proxy, AutoAnswers = Info.AutoAnswer });
							videos = pr.ToArray();
							break;
						case "game": //flash游戏
							videos = new string[] { mFlash.Groups["player"].Value };
							break;
					}

					//下载视频
					TipText("正在开始下载视频文件");
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
										Settings["Title"], Settings["Subtitle"], Info.PartCount == 1 ? "" : Info.CurrentPart.ToString(),
										ext.Replace(".", ""), Info.Settings["ACNumber"], Info.Settings["ACSubNumber"]);
						filename = Path.Combine(Info.SaveDirectory.ToString(), filename);

						//生成父文件夹
						if (!Directory.Exists(Path.GetDirectoryName(filename)))
							Directory.CreateDirectory(Path.GetDirectoryName(filename));

						//设置当前DownloadParameter
						currentParameter = new DownloadParameter()
						{
							//文件名
							FilePath = filename,
							//文件URL
							Url = videos[i],
							//代理服务器
							Proxy = Info.Proxy,
							//提取缓存
							ExtractCache = Info.ExtractCache,
							ExtractCachePattern = "fla*.tmp"
						};


						//设置代理服务器
						currentParameter.Proxy = Info.Proxy;
						//添加文件路径到List<>中
						Info.FilePath.Add(currentParameter.FilePath);
						//下载文件
						bool success = false;

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
					string acplay = GenerateAcplayConfig(pr);
					//支持AcPlay直接播放
					Settings["AcPlay"] = acplay;
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
					Settings["ExportUrl"] = sb.ToString();
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			//下载成功完成
			return true;
		}


		/// <summary>
		/// 下载弹幕
		/// </summary>
		/// <param name="title">文件名</param>
		/// <returns>是否下载成功</returns>
		private bool DownloadSubtitle()
		{
			if ((Info.DownloadTypes & DownloadType.Subtitle) != 0)
			{
				//设置文件名
				var renamehelper = new CustomFileNameHelper();

				//----------下载字幕-----------
				TipText("正在下载弹幕文件");
				//字幕文件(on)位置
				string filename = renamehelper.CombineFileName(Settings["CustomFileName"],
								Settings["Title"], Settings["Subtitle"], "", "json", Info.Settings["ACNumber"], Info.Settings["ACSubNumber"]);
				filename = Path.Combine(Info.SaveDirectory.ToString(), filename);
				//生成父文件夹
				if (!Directory.Exists(Path.GetDirectoryName(filename)))
					Directory.CreateDirectory(Path.GetDirectoryName(filename));
				Info.SubFilePath.Add(filename);
				//取得字幕文件(on)地址
				string subUrl = @"http://comment.acfun.tv/" + Info.Settings["cid"] + ".json?clientID=0.46080235205590725";

				try
				{
					//下载字幕文件
					string subcontent = Network.GetHtmlSource(subUrl, Encoding.UTF8, Info.Proxy);
					//保存文件
					File.WriteAllText(filename, subcontent);
				}
				catch
				{
					return false;
				}

				//字幕文件(lock)地址
				filename = renamehelper.CombineFileName(Settings["CustomFileName"],
								Settings["Title"], Settings["Subtitle"], "", "[锁定].json", Info.Settings["ACNumber"], Info.Settings["ACSubNumber"]);
				filename = Path.Combine(Info.SaveDirectory.ToString(), filename);
				//取得字幕文件(lock)地址
				subUrl = @"http://comment.acfun.tv/" + Info.Settings["cid"] + "_lock.json?clientID=0.46080235205590725";
				try
				{
					//下载字幕文件
					WebClient wc = new WebClient();
					wc.Proxy = Info.Proxy;
					byte[] data = wc.DownloadData(subUrl);
					string subcontent = Encoding.UTF8.GetString(data);
					//检测【锁定】弹幕文件是否是正确的JSON格式
					if (subcontent.StartsWith("[{"))
					{
						Info.SubFilePath.Add(filename);
						//保存文件
						File.WriteAllText(filename, subcontent);
					}
				}
				catch { }
			}
			return true;
		}

		/// <summary>
		/// 生成acplay配置文件
		/// </summary>
		/// <param name="pr">Parser的解析结果</param>
		/// <param name="title">文件标题</param>
		private string GenerateAcplayConfig(ParseResult pr)
		{
			try
			{
				//生成新的配置
				AcPlayConfiguration c = new AcPlayConfiguration();
				//播放器
				c.PlayerName = "acfun";
				//播放器地址
				c.PlayerUrl = Info.Settings["PlayerUrl"];
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
								Settings["Title"], Settings["Subtitle"], "", "acplay", Info.Settings["ACNumber"], Info.Settings["ACSubNumber"]);
				filename = Path.Combine(Info.SaveDirectory.ToString(), filename);
				//string path = Path.Combine(Info.SaveDirectory.ToString(), title + ".acplay");
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

	}
}
