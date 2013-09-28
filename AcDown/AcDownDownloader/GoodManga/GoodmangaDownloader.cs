using Kaedei.AcDown.Interface;
using Kaedei.AcDown.Interface.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Kaedei.AcDown.Downloader
{
	public class GoodmangaDownloader : IDownloader
	{
		public DelegateContainer delegates { get; set; }

		public TaskInfo Info { get; set; }

		//下载参数
		DownloadParameter currentParameter = new DownloadParameter();

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

		//获取漫画ID
		private string GetComicID(string Url)
		{
			Regex r = new Regex(@"^http://www\.goodmanga\.net/(?<id>[^/]+)");
			return r.Match(Url).Groups["id"].Value;
		}

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
				string src = Network.GetHtmlSource(Info.Url, Encoding.GetEncoding("UTF-8"), Info.Proxy);
				//要下载的Url列表
				var subUrls = new Collection<string>();
				Regex regChapter = new Regex("\"chapters\">[\\s|\\S]*?<\\/div>"); // 确定是整部漫画还是单独一话
				if (regChapter.Match(src).Success)
				{
					//尼玛是整部漫画..
					Match value = regChapter.Match(src);
					src = value.Value;
					regChapter = new Regex("href=\"(?'link'[^>]+)\">(?'title'[^<]+)");
					value = regChapter.Match(src);
					string title = value.Groups["title"].Value.Trim();
					int MaxCount = ((int)float.Parse(Path.GetFileName(value.Groups["link"].Value)));

					string baseLink = value.Groups["link"].Value;
					baseLink = baseLink.Substring(0, baseLink.LastIndexOf('/') + 1);
					title = title.Replace(float.Parse(Path.GetFileName(value.Groups["link"].Value)).ToString(), "");
					//填充字典
					var dict = new Dictionary<string, string>();

					while (MaxCount > 0)
					{
						dict.Add(baseLink + MaxCount.ToString(), title + MaxCount.ToString());
						MaxCount--;
					}
					//选择下载哪部漫画
					subUrls = ToolForm.CreateMultiSelectForm(dict, Info.AutoAnswer, "Goodmanga");
					//如果用户没有选择任何章节
					if (subUrls.Count == 0)
					{
						return false;
					}
				}
				else
				{
					subUrls.Add(Info.Url);
				}
				Info.PartCount = subUrls.Count;
				Info.CurrentPart = 0;
				foreach (string url in subUrls)
				{
					Info.CurrentPart ++ ;
					//提示更换新Part
					delegates.NewPart(new ParaNewPart(this.Info, Info.CurrentPart));
					if (DownloadChapterComic(url) == false)
					{
						return false;
					}
				}
			}
			catch
			{
			}
			//下载成功完成
			currentParameter.DoneBytes = currentParameter.TotalLength;
			return true;
		}

		//下载某一部漫画
		private bool DownloadChapterComic(string url)
		{
			List<string> fileUrls = new List<string>();
			//取得Url源文件
			string src = Network.GetHtmlSource(url, Encoding.GetEncoding("UTF-8"), Info.Proxy);
			Regex regChapter = new Regex("<span>of(?'page'[\\s|\\S]*?)</span>[\\s|\\S]*?src=\"(?'src'[^\"]+)[\\s|\\S]*?<\\/a>");
			Match value = regChapter.Match(src);

			if (value.Groups.Count > 2)
			{
				string id = GetComicID(Info.Url);
				int page = int.Parse(value.Groups["page"].Value);

				string baseLink = value.Groups["src"].Value;
				baseLink = baseLink.Substring(0, baseLink.LastIndexOf('/') + 1);

				//取得漫画标题
				string title = Regex.Match(src, @"(?<=<h2>).+?(?=</h2>)").Value;
				//过滤标题中的非法字符
				title = Tools.InvalidCharacterFilter(title, "");
				Info.Title = title;
				delegates.Refresh(new ParaRefresh(Info));
				for (int i = 1; i <= page; i++)
				{
					fileUrls.Add(baseLink + i + ".jpg");
				}

				//建立文件夹
				string mainDir = Info.SaveDirectory + (Info.SaveDirectory.ToString().EndsWith(@"\") ? "" : @"\") + Info.Title;
				//设置下载长度
				currentParameter.TotalLength = fileUrls.Count;
				//合并本地路径(文件夹)
				string subDir = mainDir + @"\" + Info.Title;
				Directory.CreateDirectory(subDir);
				//分析源代码,取得下载地址
				WebClient wc = new WebClient();
				//if (Info.Proxy != null)
				wc.Proxy = Info.Proxy;

				//取得源代码
				for (int j = 0; j < fileUrls.Count; j++)
				{
					if (currentParameter.IsStop)
					{
						return false;
					}
					try
					{
						byte[] content = wc.DownloadData(fileUrls[j]);
						string fn = Path.GetFileName(fileUrls[j]);
						File.WriteAllBytes(Path.Combine(subDir, fn), content);
					}
					catch { } //end try
					currentParameter.DoneBytes = j;
				} // end for
			}
			return true;
		}

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
