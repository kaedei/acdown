using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
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
	[AcDownPluginInformation("AcfunDownloader", "Acfun.tv下载插件", "Kaedei", "4.5.2.103", "Acfun.tv下载插件",
		"http://blog.sina.com.cn/kaedei")]
	public class AcFunPlugin : IPlugin
	{
		public AcFunPlugin()
		{
			Feature = new Dictionary<string, object>();
			//GetExample
			Feature.Add("ExampleUrl", new string[]
			{
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
				new AutoAnswer("tudou", "4", "土豆 超清"),
				new AutoAnswer("youku", "mp4", "优酷 高清(Mp4)"),
				new AutoAnswer("tudou", "3", "土豆 高清"),
				new AutoAnswer("tudou", "99", "土豆 原画"),
				new AutoAnswer("youku", "hd2", "优酷 超清(HD)"),
				new AutoAnswer("youku", "flv", "优酷 标清(Flv)"),
				new AutoAnswer("tudou", "2", "土豆 清晰"),
				new AutoAnswer("tudou", "1", "土豆 流畅"),
				new AutoAnswer("acfun", "auto", "保留此项可以禁止Acfun插件显示任何对话框")
			});
			//ConfigForm 属性设置窗口
			Feature.Add("ConfigForm",
				new MethodInvoker(() => { new AcFun.AcfunDownloaderConfigurationForm(Configuration).ShowDialog(); }));
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
		private string m_currentPartVideoId;
		private string m_currentPartTitle;
		private string m_videoTitle;
		private string m_customFileName;
		private string m_acNumber;
		private string m_acSubNumber;
		private string m_danmakuId;
		private string m_sourceType;
		private string m_sourceId;

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
			Match mAcNumber = Regex.Match(Info.Url, @"(?<ac>ac\d+)_(?<sub>\d+)");
			m_acNumber = mAcNumber.Groups["ac"].Value;
			m_acSubNumber = mAcNumber.Groups["sub"].Value;
			//设置自定义文件名
			m_customFileName = AcFunPlugin.DefaultFileNameFormat;
			if (Info.BasePlugin.Configuration.ContainsKey("CustomFileName"))
			{
				m_customFileName = Info.BasePlugin.Configuration["CustomFileName"];
			}

			//是否通过【自动应答】禁用对话框
			bool disableDialog = false;
			disableDialog = AutoAnswer.IsInAutoAnswers(Info.AutoAnswer, "acfun", "auto");

			//当前播放器地址
			Settings["PlayerUrl"] = @"http://static.acfun.tv/player/ssl/ACFlashPlayerN0102.swf";

			//取得网页源文件
			string src = Network.GetHtmlSource(Info.Url, Encoding.UTF8, Info.Proxy);

			var relatedVideoList = new Dictionary<string, string>();


			TipText("正在获取视频详细信息");
			var videoIdCollection = Regex.Matches(src,
				@"<a .+? data-vid=""(?<vid>\d+)"" href=""(?<url>.+?)"".+?>(?<name>.+?)</a>",
				RegexOptions.IgnoreCase);
			foreach (Match mVideoId in videoIdCollection)
			{
				//所有子标题
				if (mVideoId.Groups["name"].Value.Contains("<i")) //当前标题
				{
					m_currentPartTitle = Regex.Replace(mVideoId.Groups["name"].Value, @"<i.+?i>", "", RegexOptions.IgnoreCase);
					m_currentPartVideoId = mVideoId.Groups["vid"].Value;
				}
				else //其他标题
				{
					relatedVideoList.Add("http://www.acfun.tv" + mVideoId.Groups["url"].Value, mVideoId.Groups["name"].Value);
				}
			}


			//取得视频标题
			m_videoTitle = Regex.Match(src, @"(?<=system\.title = \$\.parseSafe\(')(.+?)(?='\))", RegexOptions.IgnoreCase).Value;

			//取得当前视频完整标题
			Info.Title = m_videoTitle + " - " + m_currentPartTitle;
			m_videoTitle = Tools.InvalidCharacterFilter(m_videoTitle, "");
			m_currentPartTitle = Tools.InvalidCharacterFilter(m_currentPartTitle, "");

			//解析关联项需要同时满足的条件：
			//1.这个任务不是被其他任务所添加的
			//2.用户设置了“解析关联项”
			TipText("正在选择关联视频");
			if (!Info.IsBeAdded && Info.ParseRelated && relatedVideoList.Count > 0)
			{
				//用户选择任务
				var ba = new Collection<string>();
				if (!disableDialog)
					ba = ToolForm.CreateMultiSelectForm(relatedVideoList, Info.AutoAnswer, "acfun");
				//根据用户选择新建任务
				foreach (string u in ba)
				{
					//新建任务
					delegates.NewTask(new ParaNewTask(Info.BasePlugin, u, this.Info));
				}
			}


			//视频地址数组
			//清空地址
			Info.FilePath.Clear();
			Info.SubFilePath.Clear();


			//获取视频信息
			var videoInfo = Network.GetHtmlSource(@"http://www.acfun.tv/video/getVideo.aspx?id=" + m_currentPartVideoId,
				Encoding.UTF8, Info.Proxy);
			//视频源网站类型和Id
			m_sourceType = Regex.Match(videoInfo, @"(?<=""sourceType"":"")\w+", RegexOptions.IgnoreCase).Value;
			m_sourceId = Regex.Match(videoInfo, @"(?<=""sourceId"":"")\w+", RegexOptions.IgnoreCase).Value;
			//弹幕Id
			m_danmakuId = Regex.Match(videoInfo, @"(?<=""danmakuId"":"")\w+", RegexOptions.IgnoreCase).Value;

			//下载弹幕
			DownloadSubtitle();


			TipText("正在解析视频源地址");
			//解析器的解析结果
			ParseResult pr = null;

			//如果允许下载视频
			if ((Info.DownloadTypes & DownloadType.Video) != 0)
			{

				//检查外链
				switch (m_sourceType)
				{
					case "qq": //QQ视频
						//解析视频
						var parserQQ = new QQVideoParser();
						pr = parserQQ.Parse(new ParseRequest() {Id = m_sourceId, Proxy = Info.Proxy, AutoAnswers = Info.AutoAnswer});
						break;
					case "youku": //优酷视频
						//解析视频
						var parserYouKu = new YoukuParser();
						pr = parserYouKu.Parse(new ParseRequest() {Id = m_sourceId, Proxy = Info.Proxy, AutoAnswers = Info.AutoAnswer});
						break;
					case "tudou": //土豆视频
						//解析视频
						var parserTudou = new TudouParser();
						pr = parserTudou.Parse(new ParseRequest() {Id = m_sourceId, Proxy = Info.Proxy, AutoAnswers = Info.AutoAnswer});
						break;
					case "sina": //新浪视频
						var parserSina = new SinaVideoParser();
						pr = parserSina.Parse(new ParseRequest() {Id = m_sourceId, Proxy = Info.Proxy, AutoAnswers = Info.AutoAnswer});
						break;
				}

				//视频地址列表
				var videos = pr.ToArray();
				//支持导出列表
				if (videos != null)
				{
					var sb = new StringBuilder();
					foreach (string item in videos)
					{
						sb.Append(item);
						sb.Append("|");
					}
					Settings["ExportUrl"] = sb.ToString();
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
					string filename = renamehelper.CombineFileName(m_customFileName,
						m_videoTitle, m_currentPartTitle, Info.PartCount == 1 ? "" : Info.CurrentPart.ToString(),
						ext.Replace(".", ""), m_acNumber, m_acSubNumber);
					filename = Path.Combine(Info.SaveDirectory.ToString(), filename);

					//添加文件名到文件列表中
					Info.FilePath.Add(filename);

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
			} //end 判断是否下载视频


			//如果插件设置中没有GenerateAcPlay项，或此项设置为true则生成.acplay快捷方式
			if (!Info.BasePlugin.Configuration.ContainsKey("GenerateAcPlay") ||
			    Info.BasePlugin.Configuration["GenerateAcPlay"] == "true")
			{
				//生成AcPlay文件
				string acplay = GenerateAcplayConfig(pr);
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
				string filename = renamehelper.CombineFileName(m_customFileName,
					m_videoTitle, m_currentPartTitle, "",
					"mp4", m_acNumber, m_acSubNumber);
				filename = Path.Combine(Info.SaveDirectory.ToString(), filename);

				arg.Append(filename);
				Info.Settings["VideoCombine"] = arg.ToString();
			}

			//下载成功完成
			return true;
		}
		

		/// <summary>
		/// 下载弹幕
		/// </summary>
		/// <param name="title">文件名</param>
		/// <returns>是否下载成功</returns>
		private void DownloadSubtitle()
		{
			if ((Info.DownloadTypes & DownloadType.Subtitle) != 0)
			{
				//设置文件名
				var renamehelper = new CustomFileNameHelper();

				//----------下载字幕-----------
				TipText("正在下载弹幕文件");
				//字幕文件(on)位置
				string filename = renamehelper.CombineFileName(m_customFileName,
					m_videoTitle, m_currentPartTitle, "", "json", m_acNumber, m_acSubNumber);
				filename = Path.Combine(Info.SaveDirectory.ToString(), filename);
				//生成父文件夹
				if (!Directory.Exists(Path.GetDirectoryName(filename)))
					Directory.CreateDirectory(Path.GetDirectoryName(filename));
				Info.SubFilePath.Add(filename);
				//取得字幕文件(on)地址
				string subUrl = @"http://comment.acfun.tv/" + m_danmakuId + ".json?clientID=0.47080235205590725";

				try
				{
					//下载字幕文件
					string subcontent = Network.GetHtmlSource(subUrl, Encoding.UTF8, Info.Proxy);
					//保存文件
					File.WriteAllText(filename, subcontent);
				}
				catch
				{
					return;
				}

				//字幕文件(lock)地址
				filename = renamehelper.CombineFileName(m_customFileName,
					m_videoTitle, m_currentPartTitle, "", "[锁定].json", m_acNumber, m_acSubNumber);
				filename = Path.Combine(Info.SaveDirectory.ToString(), filename);
				//取得字幕文件(lock)地址
				subUrl = @"http://comment.acfun.tv/" + m_danmakuId + "_lock.json?clientID=0.47080235205590725";
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
				catch
				{
				}
			}
			return;
		}

		/// <summary>
		/// 生成acplay配置文件
		/// </summary>
		/// <param name="pr">Parser的解析结果</param>
		/// <param name="title">文件标题</param>
		private string GenerateAcplayConfig(ParseResult pr)
		{
			if (Tools.IsRunningOnMono)
				return "";
			try
			{
				//生成新的配置
				var c = new AcPlayConfiguration
				{
					PlayerName = "acfun",
					PlayerUrl = Info.Settings["PlayerUrl"],
					HttpServerPort = 7776,
					ProxyServerPort = 7777,
					Videos = new Video[Info.FilePath.Count],
					WebUrl = Info.Url
				};
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
				string filename = renamehelper.CombineFileName(m_customFileName,
					m_videoTitle, m_currentPartTitle, "", "acplay", m_acNumber, m_acSubNumber);
				filename = Path.Combine(Info.SaveDirectory.ToString(), filename);
				Info.FilePath.Add(filename);
				//string path = Path.Combine(Info.SaveDirectory.ToString(), title + ".acplay");
				//序列化到文件中
				using (var fs = new FileStream(filename, FileMode.Create))
				{
					var s = new XmlSerializer(typeof (AcPlayConfiguration));
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