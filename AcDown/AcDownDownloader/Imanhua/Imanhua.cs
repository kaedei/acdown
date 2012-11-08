using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Kaedei.AcDown.Interface;
using Kaedei.AcDown.Interface.Forms;

namespace Kaedei.AcDown.Downloader
{
	/// <summary>
	/// 爱漫画下载插件
	/// </summary>
	[AcDownPluginInformation("ImanhuaDownloader", "爱漫画下载插件", "Kaedei", "4.3.0.1106", "爱漫画网下载插件", "http://blog.sina.com.cn/kaedei")]
	public class ImanhuaPlugin : IPlugin
	{

		public ImanhuaPlugin()
		{
			Feature = new Dictionary<string, object>();
			//GetExample
			Feature.Add("ExampleUrl", new string[] { 
				"爱漫画(imanhua.com)下载插件:",
				"http://www.imanhua.com/comic/120/",
				"http://www.imanhua.com/comic/120/list_55010.html",
			});
			//AutoAnswer
			Feature.Add("AutoAnswer", new List<AutoAnswer>()
			{
				new AutoAnswer("imanhua","http://c3.imanhua.com","网通①"),
				new AutoAnswer("imanhua","http://t5.imanhua.com","电信①"),
				new AutoAnswer("imanhua","http://t4.imanhua.com","电信②"),
				new AutoAnswer("imanhua","http://t6.imanhua.com","电信③"),
				new AutoAnswer("imanhua","http://c4.imanhua.com","网通②"),
				new AutoAnswer("imanhua","http://c5.imanhua.com","网通③"),
			});
			//ConfigurationForm(不支持)

		}

		public IDownloader CreateDownloader()
		{
			return new ImanhuaDownloader();
		}

		public bool CheckUrl(string url)
		{
			Regex r = new Regex(@"^http://(www\.|)imanhua\.com/comic/(?<id>\d+)(/list_(?<lid>\d+)\.html|)");
			Match m = r.Match(url);
			if (m.Success)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 规则为 Imanhua+漫画ID+随机数字 或 漫画ID+漫画某一话ID(list后的数字)
		/// 如 "Imanhua120"或"Imanhua12048848"
		/// </summary>
		public string GetHash(string url)
		{
			Regex r = new Regex(@"http://(www\.|)imanhua\.com/comic/(?<id>\d+)(/list_(?<lid>\d+)\.html|)");
			Match m = r.Match(url);
			if (m.Success)
			{
				if (string.IsNullOrEmpty(m.Groups["lid"].Value))
				{
					Random ran = new Random();
					return "Imanhua" + m.Groups["id"].Value + (ran.NextDouble() * 1000).ToString();
				}
				else
				{
					return "Imanhua" + m.Groups["id"].Value + m.Groups["lid"].Value;
				}
			}
			return null;
		}

		public Dictionary<string, object> Feature { get; private set; }

		public SerializableDictionary<string, string> Configuration { get; set; }

	}

	/// <summary>
	/// 爱漫画下载器
	/// </summary>
	public class ImanhuaDownloader : IDownloader
	{
		public TaskInfo Info { get; set; }

		//下载参数
		DownloadParameter currentParameter = new DownloadParameter();

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
			delegates.TipText(new ParaTipText(this.Info, "正在分析漫画地址"));

			if (currentParameter != null)
			{
				//将停止flag设置为true
				currentParameter.IsStop = false;
			}

			try
			{
				//取得Url源文件
				string src = Network.GetHtmlSource(Info.Url, Encoding.GetEncoding("GB2312"), Info.Proxy);

				//要下载的Url列表
				var subUrls = new Collection<string>();

				//分析漫画id和lid
				Regex r = new Regex(@"http://(www\.|)imanhua\.com/comic/(?<id>\d+)(/list_(?<lid>\d+)\.html|)");
				Match m = r.Match(Info.Url);
				string id = m.Groups["id"].Value;
				string lid = m.Groups["lid"].Value;

				#region 确定是整部漫画还是单独一话

				//lid为空 则为整部漫画
				if (string.IsNullOrEmpty(lid))
				{
					//取得所有漫画的列表
					Regex rAllComics = new Regex(@"<a href=$(?<suburl>/comic/\d+/list_\d+.html)$ title=$(?<title>.*?)$".Replace("$", "\""));
					MatchCollection mcAllComics = rAllComics.Matches(src);

					//填充字典
					var dict = new Dictionary<string, string>();

					foreach (Match item in mcAllComics)
					{
						dict.Add("http://www.imanhua.com" + item.Groups["suburl"].Value,
									item.Groups["title"].Value);
					}

					//选择下载哪部漫画
					//提取用户上次选择的章节 如果配置中有Chosen项
					if (Info.Settings.ContainsKey("Chosen") && !string.IsNullOrEmpty(Info.Settings["Chosen"]))
					{
						foreach (var u in Info.Settings["Chosen"].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
						{
							subUrls.Add(u);
						}
					}
					else
					{
						subUrls = ToolForm.CreateMultiSelectForm(dict, Info.AutoAnswer, "imanhua");
					}

					//如果用户没有选择任何章节
					if (subUrls.Count == 0)
					{
						return false;
					}

					//取得漫画标题
					Regex rTitle = new Regex(@"\<h1\>(?<title>.*)\<\/h1\>");
					Match mTitle = rTitle.Match(src);
					string title = mTitle.Groups["title"].Value;
					//过滤标题中的非法字符
					title = Tools.InvalidCharacterFilter(title, "");
					Info.Title = title;
				}
				else //如果不是整部漫画则添加此单话url
				{
					subUrls.Add(Info.Url);
					//取得漫画标题
					//取得上级页面的url
					Regex rGetParent = new Regex(@"http://www\.imanhua\.com/comic/\d+/");
					Match mGetParent = rGetParent.Match(Info.Url);
					string parentUrl = mGetParent.ToString();
					//取得源代码并分析
					string pSrc = Network.GetHtmlSource(parentUrl, Encoding.GetEncoding("GB2312"), Info.Proxy);
					//取得漫画标题
					Regex rTitle = new Regex(@"\<h1\>(?<title>.*)\<\/h1\>");
					Match mTitle = rTitle.Match(pSrc);
					string title = mTitle.Groups["title"].Value;
					//过滤标题中的非法字符
					title = Tools.InvalidCharacterFilter(title, "");
					Info.Title = title;
				} //end if

				#endregion

				//将用户选择的漫画章节存入配置
				var sbChosen = new StringBuilder();
				foreach (var suburl in subUrls)
				{
					sbChosen.Append(suburl + "|");
				}
				Info.Settings["Chosen"] = sbChosen.ToString();

				#region 选择服务器


				//取得服务器配置文件
				string serverjs = Network.GetHtmlSource(@"http://www.imanhua.com/v2/config/config.js", Encoding.GetEncoding("GB2312"), Info.Proxy);
				Regex rServer = new Regex(@"arrHost\[\d+\][ =""'\[]+(?<server>.+?)[""', ]+(?<ip>.+?)[""']\]");
				MatchCollection mServers = rServer.Matches(serverjs);

				//添加到数组中
				Dictionary<string, string> servers = new Dictionary<string, string>();
				foreach (Match item in mServers)
				{
					if (!servers.ContainsKey(item.Groups["ip"].Value))
						servers.Add(item.Groups["ip"].Value, item.Groups["server"].Value);
				}

				//选择服务器
				string serverName = ToolForm.CreateSingleSelectForm("", servers, "", Info.AutoAnswer, "imanhua");

				#endregion

				#region 下载漫画

				//建立文件夹
				string mainDir = Info.SaveDirectory + (Info.SaveDirectory.ToString().EndsWith(@"\") ? "" : @"\") + Info.Title;
				//确定漫画共有几个段落
				Info.PartCount = subUrls.Count;
				int i = 0;

				//分段落下载
				foreach (string surl in subUrls)
				{
					Info.CurrentPart = i + 1;
					//提示更换新Part
					delegates.NewPart(new ParaNewPart(this.Info, i + 1));

					//分析漫画id和lid
					Regex rSubUrl = new Regex(@"http://(www\.|)imanhua\.com/comic/(?<bid>\d+)(/list_(?<cid>\d+)\.html|)");
					Match mSubUrl = rSubUrl.Match(surl);
					string bookId = mSubUrl.Groups["bid"].Value;
					string chapterId = mSubUrl.Groups["cid"].Value;

					//地址数组
					List<string> fileUrls = new List<string>();

					//分析源代码,取得下载地址
					WebClient wc = new WebClient();
					//if (Info.Proxy != null)
					wc.Proxy = Info.Proxy;

					//取得源代码
					byte[] buff = wc.DownloadData(surl);
					string cookie = wc.ResponseHeaders.Get("Set-Cookie");
					string source = Encoding.GetEncoding("GB2312").GetString(buff);
					//取得标题
					//Regex rTitle = new Regex(@"<span id=""position"">.+?>> <a href="".+?"">(?<title>.+?)</a> >> <a href="".+?"">(?<subtitle>.+?)</a></span>");
					//Match mTitle = rTitle.Match(source);
					//string subTitle = mTitle.Groups["subtitle"].Value;
					string subTitle = Regex.Match(source, @"(?<=<h2>).+?(?=</h2>)").Value;
					//过滤子标题中的非法字符
					subTitle = Tools.InvalidCharacterFilter(subTitle, "");
					//合并本地路径(文件夹)
					string subDir = mainDir + @"\" + subTitle;
					//创建文件夹
					Directory.CreateDirectory(subDir);


					////检查是否是老版本
					//if (int.Parse(chapterId) > 7910) //7910之后为新版本
					//{
					//如果使用动态生成
					if (source.Contains(@"var cInfo={"))
					{
						//获取所有文件名
						Regex rFileName = new Regex(@"(?<="")[^,]+(?="")");
						MatchCollection mcFileNames = rFileName.Matches(Regex.Match(source, @"(?<=""files"":\[).+?(?=\])").Value);
						foreach (Match file in mcFileNames)
						{
							if (file.Value.StartsWith("/"))
							{
								fileUrls.Add(serverName + file.Value);
							}
							else
							{
								fileUrls.Add(serverName + "/Files/Images/" + bookId + "/" + chapterId + "/" + file.Value);
							}
						}
					}
					else
					{
						//获取所有文件名
						//获取页面HTML中的js段
						Regex rJsFiles = new Regex(@"(?<=var pic=\[).+?(?=\])");
						string jsFiles = rJsFiles.Match(source).Value;

						//获取所有图片文件
						Regex rFileName = new Regex(@"(?<="")[^,]+(?="")");
						MatchCollection mcFileNames = rFileName.Matches(jsFiles);
						foreach (Match file in mcFileNames)
						{
							fileUrls.Add(serverName + (file.Value.StartsWith("/") ? file.Value : "/" + file.Value));
						}
					}

					//输出真实地址
					StringBuilder sb = new StringBuilder(fileUrls.Count);
					foreach (var file in fileUrls)
					{
						sb.Append(file + "|");
					}
					Info.Settings["ExportUrl"] = sb.ToString();
					sb = null;


					//设置下载长度
					currentParameter.TotalLength = fileUrls.Count;

					//下载文件!

					for (int j = 0; j < fileUrls.Count; j++)
					{
						if (currentParameter.IsStop)
						{
							return false;
						}
						try
						{
							wc.Headers.Add("Referer", subUrls[i]);
							wc.Headers.Add("Cookie", cookie);
							byte[] content = wc.DownloadData(fileUrls[j]);
							string fn = Path.GetFileName(fileUrls[j]);
							File.WriteAllBytes(Path.Combine(subDir, fn), content);
						}
						catch { } //end try
						currentParameter.DoneBytes = j;
					} // end for

					i++;
				}//end foreach
			}//end try
			catch (Exception ex) //出现错误即下载失败
			{
				throw ex;
			}//end try


				#endregion



			//下载成功完成
			currentParameter.DoneBytes = currentParameter.TotalLength;
			return true;

		}//end DownloadVideo


		//停止下载
		public void StopDownload()
		{
			if (currentParameter != null)
			{
				//将停止flag设置为true
				currentParameter.IsStop = true;
			}
		}

	}
}
