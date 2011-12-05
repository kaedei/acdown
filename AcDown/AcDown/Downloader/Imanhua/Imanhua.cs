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
	/// <summary>
	/// 爱漫画下载插件
	/// </summary>
	public class ImanhuaPlugin : IAcdownPluginInfo
	{
		#region IAcdownPluginInfo 成员

		public string Name
		{
			get { return "ImanhuaDownloader"; }
		}

		public string Author
		{
			get { return "Kaedei Software"; }
		}

		public Version Version
		{
			get { return new Version(2, 0, 0, 0); }
		}

		public string Describe
		{
			get { return "爱漫画网下载插件"; }
		}

		public string SupportUrl
		{
			get { return @"http://blog.sina.com.cn/kaedei"; }
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
		/// 规则为 Imanhua + 漫画ID 或 漫画ID+漫画某一话ID(list后的数字)
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
					return "Imanhua" + m.Groups["id"].Value;
				}
				else
				{
					return "Imanhua" + m.Groups["id"].Value + m.Groups["lid"].Value;
				}
			}
			return null;
		}



		public string[] GetUrlExample()
		{
			return new string[] { 
				"爱漫画(imanhua.com)下载插件:",
				"http://www.imanhua.com/comic/120/",
				"http://www.imanhua.com/comic/120/list_55010.html",
			};
		}
		#endregion
	}

	/// <summary>
	/// 爱漫画下载器
	/// </summary>
	public class ImanhuaDownloader : IDownloader
	{
		public TaskInfo Info { get; set; }
		
		//下载参数
		DownloadParameter currentParameter = new DownloadParameter();

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
			delegates.TipText(new ParaTipText(this.Info, "正在分析漫画地址"));
			Info.Status = DownloadStatus.正在下载;

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
				List<string> subUrls = new List<string>();

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

					//新建数组
					BitArray selected = new BitArray(mcAllComics.Count);

					//suburl数组
					List<string> urls = new List<string>();
					foreach (Match item in mcAllComics)
					{
						urls.Add("http://www.imanhua.com" + item.Groups["suburl"].Value);
					}

					//各话标题数组
					List<string> titles = new List<string>();
					foreach (Match item in mcAllComics)
					{
						titles.Add(item.Groups["title"].Value);
					}

					//选择下载哪部漫画
					selected = ToolForm.CreateSelctionForm(titles.ToArray());

					//将地址填充到下载列表中
					for (int i = 0; i < mcAllComics.Count; i++)
					{
						if (selected[i])
						{
							subUrls.Add(urls[i]);
						}
					}

					//如果用户没有选择任何章节
					if (subUrls.Count == 0)
					{
						Info.Status = DownloadStatus.已经停止;
						delegates.Finish(new ParaFinish(this.Info, false));
						return;
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

#region 选择服务器
				
				string serverName;
				//取得配置文件
				string serverjs = Network.GetHtmlSource(@"http://www.imanhua.com/v2/config/config.js", Encoding.GetEncoding("GB2312"), Info.Proxy);
				Regex rServer = new Regex("\"(?<sname>.+?)\" , \"(?<surl>.+?)\"");
				MatchCollection mServers = rServer.Matches(serverjs);

				//添加到数组中
				List<string> servers = new List<string>();
				foreach (Match item in mServers)
				{
					if (servers.Count < 5)
						servers.Add(item.Groups["sname"].Value);
				}

				//选择服务器
				int svr = ToolForm.CreateSelectServerForm("", servers.ToArray(), 0);
				serverName = mServers[svr].Groups["surl"].Value;


#endregion
				
#region 下载漫画

				//建立文件夹
				string mainDir = Info.SaveDirectory + (Info.SaveDirectory.ToString().EndsWith(@"\") ? "" : @"\") + Info.Title;
				//确定漫画共有几个段落
				Info.PartCount = subUrls.Count;

				//分段落下载
				for (int i = 0; i < Info.PartCount; i++)
				{
					Info.CurrentPart = i + 1;
					//提示更换新Part
					delegates.NewPart(new ParaNewPart(this.Info, i + 1));

					//地址数组
					List<string> fileUrls = new List<string>();

					//分析源代码,取得下载地址
					WebClient wc = new WebClient();
					if (Info.Proxy != null)
						wc.Proxy = Info.Proxy;

					//取得源代码
					byte[] buff = wc.DownloadData(subUrls[i]);
					string cookie = wc.ResponseHeaders.Get("Set-Cookie");
					string source = Encoding.GetEncoding("GB2312").GetString(buff);
					//取得标题
					Regex rTitle = new Regex(@">> <a href=$.+?$>(?<title>\w+)</a></span>".Replace("$", "\""));
					Match mTitle = rTitle.Match(source);
					string subTitle = mTitle.Groups["title"].Value;
					//过滤子标题中的非法字符
					subTitle = Tools.InvalidCharacterFilter(subTitle, "");
					//合并本地路径(文件夹)
					string subDir = mainDir + @"\" + subTitle;
					//创建文件夹
					Directory.CreateDirectory(subDir);

					//检查是否是老版本
					Regex rOld = new Regex(@"/Files/Images/\d+/\d+/\w+\.\w+");
					MatchCollection mOlds = rOld.Matches(source);
					if (mOlds.Count > 0) //老版本
					{
						//添加url到数组
						foreach (Match item in mOlds)
						{
							fileUrls.Add(serverName + item.ToString());
						}
					}
					else   //新版本
					{
						Regex rNewId = new Regex(@"'(?<id1>\d+)\|");
						Match mNewId = rNewId.Match(source);
						string id1 = mNewId.Groups["id1"].Value;
						//取得var段
						Regex rSubSource = new Regex(@"var.*split");
						Match mSubSource = rSubSource.Match(source);
						string subsource = mSubSource.ToString();
						Regex rNewFile = new Regex(@"\|(?<file>\w+[^pic,sid,var,len,\|,'])");
						MatchCollection mNewFiles = rNewFile.Matches(subsource);
						
						//添加url到数组
						foreach (Match item in mNewFiles)
						{
							fileUrls.Add(serverName + "/Pictures/" + id + "/" + id1 + "/" + item.Groups["file"].Value + ".jpg");
							fileUrls.Add(serverName + "/Pictures/" + id + "/" + id1 + "/" + item.Groups["file"].Value + ".png");
							fileUrls.Add(serverName + "/Files/Images/" + id + "/" + id1 + "/" + item.Groups["file"].Value + ".jpg");
							fileUrls.Add(serverName + "/Files/Images/" + id + "/" + id1 + "/" + item.Groups["file"].Value + ".png");
						}
					}

					//设置下载长度
					currentParameter.TotalLength = fileUrls.Count;

					//下载文件!

					for (int j = 0; j < fileUrls.Count; j++)
					{
						if (currentParameter.IsStop)
						{
							Info.Status = DownloadStatus.已经停止;
							delegates.Finish(new ParaFinish(this.Info, false));
							return;
						}
						try
						{
							wc.Headers.Add("Referer", subUrls[i]);
							wc.Headers.Add("Cookie", cookie);
							byte[] content = wc.DownloadData(fileUrls[j]);
							string fn = Path.GetFileName(fileUrls[j]);
							File.WriteAllBytes(Path.Combine(subDir, fn), content);
						}
						catch(Exception ex) { } //end try
						currentParameter.DoneBytes = j;
					} // end for

				}//end for
			}//end try
			catch (Exception ex) //出现错误即下载失败
			{
				Info.Status = DownloadStatus.出现错误;
				delegates.Error(new ParaError(this.Info, ex));
				return;
			}//end try
			//下载成功完成
			Info.Status = DownloadStatus.下载完成;
			delegates.Finish(new ParaFinish(this.Info, true));

#endregion

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

		#endregion
	}
}
