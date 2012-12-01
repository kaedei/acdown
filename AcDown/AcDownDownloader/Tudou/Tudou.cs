using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;
using System.IO;
using Kaedei.AcDown.Interface.Forms;
using Kaedei.AcDown.Interface.Downloader;
using Kaedei.AcDown.Interface.AcPlay;
using System.Xml.Serialization;

namespace Kaedei.AcDown.Downloader
{
	[AcDownPluginInformation("TudouDownloader", "土豆网下载插件", "Kaedei", "4.3.1.1201", "土豆网下载插件", "http://blog.sina.com.cn/kaedei")]
	public class TudouPlugin : IPlugin
	{

		public TudouPlugin()
		{
			Feature = new Dictionary<string, object>();
			//GetExample
			Feature.Add("ExampleUrl", new string[] { 
				"土豆网(Tudou.com)下载插件:",
				"单视频:",
				"http://www.tudou.com/programs/view/3j7pjo3V9jg/",
				"列表视频:",
				"http://www.tudou.com/listplay/y7n9thjQ7xo.html",
				"http://www.tudou.com/listplay/y7n9thjQ7xo/8-qexU-mDnA.html",
				"专辑视频:",
				"http://www.tudou.com/albumplay/4EmiYFwoDyU.html",
				"http://www.tudou.com/albumplay/4EmiYFwoDyU/m0-LUVxLt10.html",
				"豆泡:",
				"http://dp.tudou.com/v/GaoNGAAJB9M.html",
				"http://dp.tudou.com/programs/view/E2d3ov3YD7Q/",
				"http://dp.tudou.com/l/y7n9thjQ7xo/8-qexU-mDnA.html",
				"http://dp.tudou.com/a/4EmiYFwoDyU/tkySSoG4aag.html",
				"专辑:",
				"http://www.tudou.com/albumcover/cghDyIIHl0M.html"
			});
			//AutoAnswer
			Feature.Add("AutoAnswer", new List<AutoAnswer>()
			{
				new AutoAnswer("tudou","4","土豆 超清"),
				new AutoAnswer("tudou","99","土豆 原画"),
				new AutoAnswer("tudou","3","土豆 高清"),
				new AutoAnswer("tudou","2","土豆 清晰"),
				new AutoAnswer("tudou","1","土豆 流畅")
			});
			//ConfigurationForm(不支持)
		}

		public IDownloader CreateDownloader()
		{
			return new TudouDownloader();
		}

		public const string regProgramView = @"http://www\.tudou\.com/programs/view/(?<icode>[\w\-]+)";
		public const string regListplay = @"http://www\.tudou\.com/listplay/(?<lcode>[\w\-]+)(/(?<icode>[\w\-]+)|)\.html";
		public const string regAlbumplay = @"http://www\.tudou\.com/albumplay/(?<acode>[\w\-]+)(/(?<icode>[\w\-]+)|)\.html";
		public const string regDpProgramview = @"http://dp\.tudou\.com/(programs/view|v)/(?<icode>[\w\-]+)";
		public const string regDpAlbumList = @"http://dp\.tudou\.com/(?<type>a|l|albumplay|listplay)/(?<alcode>[\w\-]+)(/(?<icode>[\w\-]+)|)\.html";
		public const string regAlbumcover = @"http://www\.tudou\.com/albumcover/(?<acode>[\w\-]+)";

		public bool CheckUrl(string url)
		{
			//单视频
			if (Regex.IsMatch(url, regProgramView, RegexOptions.IgnoreCase))
				return true;
			//列表视频
			if (Regex.IsMatch(url, regListplay, RegexOptions.IgnoreCase))
				return true;
			//专辑
			if (Regex.IsMatch(url, regAlbumplay, RegexOptions.IgnoreCase))
				return true;
			//豆泡(单视频)
			if (Regex.IsMatch(url, regDpProgramview, RegexOptions.IgnoreCase))
				return true;
			//豆泡(列表/专辑)
			if (Regex.IsMatch(url, regDpAlbumList, RegexOptions.IgnoreCase))
				return true;
			//专辑
			if (Regex.IsMatch(url, regAlbumcover, RegexOptions.IgnoreCase))
				return true;
			return false;
		}

		/// <summary>
		/// 规则为 tudou + URL后半部分
		/// </summary>
		public string GetHash(string url)
		{
			if (Regex.IsMatch(url, regAlbumcover))
			{
				return "tudou" + Regex.Match(url, regAlbumcover).Groups["acode"].Value;
			}
			else
			{
				return "tudou" +
					Regex.Match(url, @"(?<=http://(dp|www)\.tudou\.com/(a|l|programs/view|listplay|albumplay)/)[\w\-]+(/[\w\-]+|)").Value;
			}
		}

		public Dictionary<string, object> Feature { get; private set; }

		public SerializableDictionary<string, string> Configuration { get; set; }

	}

	public class TudouDownloader : CommonDownloader
	{

		//下载视频
		public override bool Download()
		{
			//开始下载
			TipText("正在解析视频地址");

			//解析专辑
			if (Regex.IsMatch(Info.Url, TudouPlugin.regAlbumcover, RegexOptions.IgnoreCase))
			{
				string source = Network.GetHtmlSource(Info.Url, Encoding.GetEncoding("GBK"), Info.Proxy);
				MatchCollection mcAlbumPlays = Regex.Matches(source.Replace(" ", "").Replace("\t", "").Replace("\r\n", ""),
					@"<h6class=""caption.+?(?<url>http://www\.tudou\.com/albumplay/[\w\-]+(/[\w\-]+|)\.html)"">(?<title>.+?)</a></h6>");
				var plays = new Dictionary<string, string>();
				foreach (Match mAlbumPlays in mcAlbumPlays)
				{
					string aUrl = mAlbumPlays.Groups["url"].Value;
					string aTitle = mAlbumPlays.Groups["title"].Value;
					plays.Add(aUrl, aTitle);
				}
				var chosen = ToolForm.CreateMultiSelectForm(plays, null, "tudou");
				if (chosen.Count == 0)
					throw new Exception("未从列表中选择任何视频");

				//修正当前url为用户选中的第一个url
				Info.Url = chosen[0];
				//添加其他任务
				for (int i = 1; i < chosen.Count; i++)
				{
					NewTask(chosen[i]);
				}
			}

			string url = Info.Url;
			//修正豆泡网址
			if (Regex.Match(Info.Url, TudouPlugin.regDpProgramview).Success)
			{
				url = @"http://www.tudou.com/programs/view/" + Regex.Match(Info.Url, TudouPlugin.regDpProgramview).Groups["icode"].Value;
			}
			else if (Regex.IsMatch(Info.Url, TudouPlugin.regDpAlbumList))
			{
				url = Info.Url.Replace("dp.tudou.com", "www.tudou.com");
				url = url.Replace("/l/", "/listplay/");
				url = url.Replace("/a/", "/albumplay/");
			}

			//Settings["icode"] = Regex.Match(Info.Url, TudouPlugin.regProgramView).Groups["icode"].Value;
			string src = Network.GetHtmlSource(url, Encoding.GetEncoding("GBK"), Info.Proxy);
			//取得iid
			Settings["iid"] = Regex.Match(src.Replace(" ", ""), @"(?<=iid:)\d+").Value;
			//取得专辑标题
			Settings["AlbumTitle"] = Regex.Match(src, @"(?<=atitle="").+?(?="")").Value ?? "";
			//视频标题
			Info.Title = Regex.Match(src, @"(?<=kw:( |)(""|')).+?(?=(""|'))").Value;
			Settings["title"] = Tools.InvalidCharacterFilter(Info.Title, "");
			if (!string.IsNullOrEmpty(Settings["AlbumTitle"]))
			{
				Settings["title"] = Tools.InvalidCharacterFilter(Settings["AlbumTitle"], "")
					+ Path.DirectorySeparatorChar + Settings["title"];
			}


			//视频地址数组
			string[] videos = null;
			//清空地址
			Info.FilePath.Clear();

			//调用土豆视频解析器
			TudouParser parserTudou = new TudouParser();
			var pr = parserTudou.Parse(new ParseRequest()
			{
				Id = Settings["iid"],
				Proxy = Info.Proxy,
				AutoAnswers = Info.AutoAnswer
			});
			videos = pr.ToArray();


			//下载弹幕
			if ((Info.DownloadTypes & DownloadType.Subtitle) != 0)
			{
				//如果视频地址属于豆泡
				if (Regex.IsMatch(Info.Url, TudouPlugin.regDpProgramview) ||
					Regex.IsMatch(Info.Url, TudouPlugin.regDpAlbumList))
				{
					TipText("正在下载弹幕文件");
					try
					{
						var subfile = Path.Combine(Info.SaveDirectory.ToString(), Settings["title"] + ".json");
						Info.SubFilePath.Clear();
						Info.SubFilePath.Add(subfile);
						Network.DownloadFile(new DownloadParameter()
						{
							FilePath = subfile,
							Url = "http://comment.dp.tudou.com/comment/get/" + Settings["iid"] + "/vdn12d/",
							Proxy = Info.Proxy
						});
					}
					catch { }
				}
			}

			//下载视频
			//确定视频共有几个段落
			Info.PartCount = videos.Length;

			TipText("正在开始下载视频文件");

			//分段落下载
			for (int i = 0; i < videos.Length; i++)
			{

				//取得文件后缀名
				string ext = Tools.GetExtension(videos[i]);
				if (ext == ".f4v") ext = ".flv";
				//设置当前DownloadParameter
				if (Info.PartCount == 1) //如果只有一段
				{
					currentParameter = new DownloadParameter()
					{
						//文件名 例: c:\123(1).flv
						FilePath = Path.Combine(Info.SaveDirectory.ToString(),
													  Settings["title"] + ext),
						//文件URL
						Url = videos[i],
						//代理服务器
						Proxy = Info.Proxy
					};
				}
				else //如果分段有多段
				{
					currentParameter = new DownloadParameter()
					{
						//文件名 例: c:\123(1).flv
						FilePath = Path.Combine(Info.SaveDirectory.ToString(),
													  Settings["title"] + "(" + (i + 1).ToString() + ")" + ext),
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
				NewPart(i + 1, videos.Length);

				//下载视频文件
				success = Network.DownloadFile(currentParameter, this.Info);

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
			}

			//生成.acplay文件
			if (File.Exists(Path.Combine(Info.SaveDirectory.ToString(), Settings["title"] + ".json")))
			{
				var acplay = 	GenerateAcplayConfig(pr, Settings["title"]);
				if (!string.IsNullOrEmpty(acplay))
					Settings["AcPlay"] = acplay;
			}


			//下载成功完成
			return true;

		}


		private string GenerateAcplayConfig(ParseResult pr, string title)
		{
			if (Tools.IsRunningOnMono)
				return "";
			try
			{
				//生成新的配置
				AcPlayConfiguration c = new AcPlayConfiguration();
				//播放器
				c.PlayerName = "acfun";
				//播放器地址 （使用acfun播放器）
				c.PlayerUrl = "http://static.acfun.tv/player/ACFlashPlayer.201209271950.swf";
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


				string path = Path.Combine(Info.SaveDirectory.ToString(), title + ".acplay");
				//序列化到文件中
				using (var fs = new FileStream(path, FileMode.Create))
				{
					XmlSerializer s = new XmlSerializer(typeof(AcPlayConfiguration));
					s.Serialize(fs, c);
				}
				return title;
			}
			catch
			{
				return "";
			}

		}
	}

}//end namespace
