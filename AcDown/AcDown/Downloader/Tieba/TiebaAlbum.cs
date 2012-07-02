using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Kaedei.AcDown.Interface;
using System.IO;
using System.Net;
using System.Collections.ObjectModel;

namespace Kaedei.AcDown.Downloader
{

	/// <summary>
	/// 贴吧相册下载插件
	/// </summary>
	[AcDownPluginInformation("TiebaAlbumDownloader", "百度贴吧相册下载插件", "Kaedei", "3.12.0.701", "百度贴吧相册下载插件", "http://blog.sina.com.cn/kaedei")]
	public class TiebaAlbumPlugin : IPlugin
	{


		public TiebaAlbumPlugin()
		{
			Feature = new Dictionary<string, object>();
			//GetExample
			Feature.Add("ExampleUrl", new string[] { 
				"百度贴吧相册下载插件:",
				"http://tieba.baidu.com/f/tupian/album?kw=windows7&an=win7%D7%D4%B4%F8%B1%DA%D6%BD",
			});
			//AutoAnswer(不支持)
			//ConfigurationForm(不支持)
		}

		public IDownloader CreateDownloader()
		{
			return new TiebaAlbumDownloader();
		}

		public bool CheckUrl(string url)
		{
			Regex r = new Regex(@"^http://tieba\.baidu\.com/f/tupian/album\?kw=(?<kw>.+)&an=(?<an>.+)");
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

		public Dictionary<string, object> Feature { get; private set; }

		public SerializableDictionary<string, string> Configuration { get; set; }
	}


	/// <summary>
	/// 贴吧相册下载器
	/// </summary>
	public class TiebaAlbumDownloader : IDownloader
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


		//开始下载
		public bool Download()
		{
			//开始下载
			delegates.TipText(new ParaTipText(this.Info, "正在分析图片地址"));

			if (currentParameter != null)
			{
				//将停止flag设置为true
				currentParameter.IsStop = false;
			}
			try
			{
				//取得首个Url源文件
				string src1 = Network.GetHtmlSource(Info.Url, Encoding.GetEncoding("GBK"), Info.Proxy);
				Collection<string> subUrls = new Collection<string>();
				subUrls.Add(Info.Url);
				//要下载的源文件列表
				Dictionary<string, string> fileUrls = new Dictionary<string, string>();

				//取得标题(文件夹名称)
				Regex r = new Regex(@"http://tieba\.baidu\.com/f/tupian/album\?kw=(?<kw>.+)&an=(?<an>.+)");
				Match m = r.Match(Info.Url);
				string title = Tools.UrlDecode(m.Groups["kw"].Value) + "吧 - " + Tools.UrlDecode(m.Groups["an"].Value);
				//过滤无效字符
				Info.Title = Tools.InvalidCharacterFilter(title, "");

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
				var sb = new StringBuilder();
				foreach (string item in subUrls)
				{
					//取得源代码
					string src2 = Network.GetHtmlSource(item, Encoding.GetEncoding("GBK"), Info.Proxy);
					//解析所有图片
					Regex rAllPic = new Regex(@"<div class=$j_showtip pic_box$ id=$(?<id>\w+).+?<p class=$pic_des$>(?<des>.+?)</p>".Replace("$", "\""), RegexOptions.Singleline);
					MatchCollection mAllPics = rAllPic.Matches(src2);
					foreach (Match item2 in mAllPics)
					{
						//string fName = Tools.InvalidCharacterFilter(item2.Groups["des"].Value + " [" + rnd.Next(1000).ToString() + "]", "");
						string fName = item2.Groups["id"].Value + ".jpg";
						//支持导出地址列表
						sb.Append("http://imgsrc.baidu.com/forum/pic/item/" + fName + "|");
						fileUrls.Add("http://imgsrc.baidu.com/forum/pic/item/" + fName, fName);
					}
				}
				Info.Settings["ExportUrl"] = sb.ToString();




				#region 下载图片

				//建立文件夹
				string mainDir = Info.SaveDirectory + (Info.SaveDirectory.ToString().EndsWith(@"\") ? "" : @"\") + Info.Title;

				//确定下载任务共有几个Part
				Info.PartCount = 1;
				Info.CurrentPart = 1;
				delegates.NewPart(new ParaNewPart(this.Info, 1));

				//分析源代码,取得下载地址
				WebClient wc = new WebClient();
				if (Info.Proxy != null)
					wc.Proxy = Info.Proxy;

				//创建文件夹
				Directory.CreateDirectory(mainDir);

				//设置下载长度
				currentParameter.TotalLength = fileUrls.Count;

				int j = 0;
				//下载文件
				foreach (string item in fileUrls.Keys)
				{
					if (currentParameter.IsStop)
					{
						return false;
					}
					try
					{
						byte[] content = wc.DownloadData(item);
						File.WriteAllBytes(Path.Combine(mainDir, fileUrls[item] + ".jpg"), content);
					}
					catch { } //end try
					j++;
					currentParameter.DoneBytes = j;
				} // end for
				#endregion

			}//end try
			catch (Exception ex) //出现错误即下载失败
			{
				throw ex;
			}//end try



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
