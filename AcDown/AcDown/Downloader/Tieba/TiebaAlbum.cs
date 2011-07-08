using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using Kaedei.AcDown.Interface;
using System.IO;
using System.Net;
using System.Collections.ObjectModel;

namespace Kaedei.AcDown.Downloader
{

	/// <summary>
	/// 爱漫画下载插件
	/// </summary>
	public class TiebaAlbumPlugin : IAcdownPluginInfo
	{
		#region IAcdownPluginInfo 成员

		public string Name
		{
			get { return "TiebaAlbumDownloader"; }
		}

		public string Author
		{
			get { return "Kaedei Software"; }
		}

		public Version Version
		{
			get { return new Version(1, 0, 0, 0); }
		}

		public string Describe
		{
			get { return "百度贴吧相册下载插件"; }
		}

		public string SupportUrl
		{
			get { return @"http://blog.sina.com.cn/kaedei"; }
		}

		public IDownloader CreateDownloader()
		{
			return new TiebaAlbumDownloader(this);
		}

		public bool CheckUrl(string url)
		{
			Regex r = new Regex(@"http://tieba\.baidu\.com/f/tupian/album\?kw=(?<kw>.+)&an=(?<an>.+)");
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
		/// 规则为 TiebaAlbum + kw + an
		/// 如 "TiebaAlbum%C1%FA%D6%AE%B9%C8%CD%E6%BC%D2%D4%AD%B4%B4%CD%BC"
		/// </summary>
		public string GetHash(string url)
		{
			Regex r = new Regex(@"http://tieba\.baidu\.com/f/tupian/album\?kw=(?<kw>.+)&an=(?<an>.+)");
			Match m = r.Match(url);
			if (m.Success)
			{
				return "TiebaAlbum" + m.Groups["kw"].Value + m.Groups["an"].Value;
			}
			return null;
		}



		public string[] GetUrlExample()
		{
			return new string[] { 
				"百度贴吧相册下载插件:",
				"http://tieba.baidu.com/f/tupian/album?kw=windows7&an=win7%D7%D4%B4%F8%B1%DA%D6%BD",
			};
		}
		#endregion
	}


	/// <summary>
	/// 爱漫画下载器
	/// </summary>
	public class TiebaAlbumDownloader : IDownloader
	{

		public TiebaAlbumDownloader(TiebaAlbumPlugin p)
		{
			_basePlugin = p;
		}
		//插件
		TiebaAlbumPlugin _basePlugin;
		public IAcdownPluginInfo GetBasePlugin() { return _basePlugin; }

		//下载参数
		DownloadParameter currentParameter = new DownloadParameter();

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

		//开始下载
		public void Download()
		{
			//开始下载
			delegates.Start(new ParaStart(this.TaskId));
			delegates.TipText(new ParaTipText(this.TaskId, "正在分析图片地址"));
			_status = DownloadStatus.正在下载;
			try
			{
				//取得首个Url源文件
				string src1 = Network.GetHtmlSource(Url, Encoding.GetEncoding("GBK"),delegates.Proxy);
				Collection<string> subUrls = new Collection<string>();
				subUrls.Add(Url);
				//要下载的源文件列表
				Dictionary<string, string> fileUrls = new Dictionary<string, string>();

				//取得标题(文件夹名称)
				Regex r = new Regex(@"http://tieba\.baidu\.com/f/tupian/album\?kw=(?<kw>.+)&an=(?<an>.+)");
				Match m = r.Match(Url);
				string title = Tools.DecodeString(m.Groups["kw"].Value) + "吧 - " + Tools.DecodeString(m.Groups["an"].Value);
				//过滤无效字符
				_title = Tools.InvalidCharacterFilter(title, "");

				//分析页面
				Regex rSubPage = new Regex(@"a href=$(?<suburl>/f/tupian/album\?kw=.+?&an=.+?&pn=\d+)$>\d+<".Replace("$", "\""));
				MatchCollection mSubPages = rSubPage.Matches(src1);
				foreach (Match item in mSubPages)
				{
					string surl = item.Groups["suburl"].Value;
					subUrls.Add("http://tieba.baidu.com" + surl);
				}

				Random rnd = new Random();

				//分析各个子页面的源文件
				foreach (string item in subUrls)
				{
					//取得源代码
					string src2 = Network.GetHtmlSource(item, Encoding.GetEncoding("GBK"), delegates.Proxy);
					//解析所有图片
					Regex rAllPic = new Regex(@"<div class=$j_showtip pic_box$ id=$(?<id>\w+).+?<p class=$pic_des$>(?<des>.+?)</p>".Replace("$", "\""), RegexOptions.Singleline);
					MatchCollection mAllPics = rAllPic.Matches(src2);
					foreach (Match item2 in mAllPics)
					{
						string fName = Tools.InvalidCharacterFilter(item2.Groups["des"].Value + " [" + rnd.Next(1000).ToString() + "]", "");
						fileUrls.Add("http://imgsrc.baidu.com/forum/pic/item/" + item2.Groups["id"].Value + ".jpg",
							fName);
					}
				}

				#region 下载图片

				//建立文件夹
				string mainDir = SaveDirectory + (SaveDirectory.ToString().EndsWith(@"\") ? "" : @"\") + _title;
				
				//确定下载任务共有几个Part
				_partCount = 1;
				_currentPart = 1;
				delegates.NewPart(new ParaNewPart(this.TaskId, 1));

				//分析源代码,取得下载地址
				WebClient wc = new WebClient();
				if (delegates.Proxy != null)
					wc.Proxy = delegates.Proxy;

				//创建文件夹
				Directory.CreateDirectory(mainDir);

				//设置下载长度
				currentParameter.TotalLength = fileUrls.Count;

				int j = 0;
				//下载文件
				foreach(string item in fileUrls.Keys)
				{
					if (currentParameter.IsStop)
					{
						_status = DownloadStatus.已经停止;
						delegates.Finish(new ParaFinish(this.TaskId, false));
						return;
					}
					try
					{
						byte[] content = wc.DownloadData(item);
						File.WriteAllBytes(Path.Combine(mainDir, fileUrls[item] + ".jpg"), content);
					}
					catch (Exception ex) { } //end try
					j++;
					currentParameter.DoneBytes = j;
				} // end for


			}//end try
			catch (Exception ex) //出现错误即下载失败
			{
				_status = DownloadStatus.出现错误;
				delegates.Error(new ParaError(this.TaskId, ex));
				return;
			}//end try
			//下载成功完成
			_status = DownloadStatus.下载完成;
			delegates.Finish(new ParaFinish(this.TaskId, true));

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
