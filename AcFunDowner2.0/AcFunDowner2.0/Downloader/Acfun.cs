using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;
using Kaedei.AcDown.Parser;
using System.IO;

namespace Kaedei.AcDown.Downloader
{
	/// <summary>
	/// AcFun下载支持插件
	/// </summary>
	public class AcFunPlugin :IAcdownPluginInfo
	{

		public string Name
		{
			get { return @"AcFunDownloader"; }
		}

		public string Author
		{
			get { return "Kaedei Software"; }
		}

		public Version Version
		{
			get { return new Version(1,0,0,0); }
		}

		public string Describe
		{
			get { return @"Acfun.cn下载插件"; }
		}

		public string SupportUrl
		{
			get { return @"http://blog.sina.com.cn/kaedei"; }
		}

		public IDownloader CreateDownloader()
		{
			return new Acfun();
		}

		public bool CheckUrl(string url)
		{
			Regex r = new Regex(@"http://(acfun\.cn|.*?)/html/(music|anime|game|ent|dy|zj)/\w+/\w+\.html");
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
		/// 规则为 acfun + 视频ID
		/// 如 "acfun158539"
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public string GetHash(string url)
		{
			Regex r = new Regex(@"http://(acfun\.cn|.*?)/html/(music|anime|game|ent|dy|zj)/\w+/(?<hash>\w+)\.html");
			Match m = r.Match(url);
			if (m.Success)
			{
				return "acfun" + m.Groups["hash"].Value;
			}
			else
			{
				return null;
			}
		}

	}

	/// <summary>
	/// Acfun下载器
	/// </summary>
	public class Acfun : IDownloader
	{
		DownloadParameter currentParameter;
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

		public string Url { get; set; }

		//下载视频
		public void DownloadVideo()
		{
			//TODO:各Delegates的调用
			//开始下载
			delegates.Start(new ParaStart(this.TaskId));
			delegates.TipText(new ParaTipText(this.TaskId, "正在分析视频地址"));

			//要分析的地址
			string url = Url;

			//取得网页源文件
			string src = Network.GetHtmlSource(url, Encoding.GetEncoding("GB2312"));

			//分析id和视频存放站点(type)
			Regex rId = new Regex(@"id=(?<id>\w*)&amp;type(|\w)=(?<type>\w*)");
			Match mId = rId.Match(src);
			//取得id和type值
			string id = mId.Groups["id"].Value;
			string type = mId.Groups["type"].Value;

			//取得视频标题
			Regex rTitle = new Regex(@"<title>(?<title>.*)</title>");
			Match mTitle = rTitle.Match(src);
			string title = mTitle.Groups["title"].Value;
			//过滤非法字符
			title = Tools.InvalidCharacterFilter(title, "");

			//视频地址数组
			string[] videos = null;
			//检查type值
			switch (type)
			{
				case "video": //新浪视频
					//解析视频
					SinaVideoParser parserSina = new SinaVideoParser();
					videos = parserSina.Parse(new string[] { id });
					break;
				case "qq": //QQ视频
					//解析视频
					QQVideoParser parserQQ = new QQVideoParser();
					videos = parserQQ.Parse(new string[] { id });
					break;
			}

			//下载视频
			//确定视频共有几个段落
			_partCount = videos.Length;

			//分段落下载
			for (int i = 0; i < _partCount; i++)
			{
				//提示更换新Part
				delegates.NewPart(new ParaNewPart(this.TaskId, i + 1));
				//设置当前DownloadParameter
				currentParameter = new DownloadParameter()
				{
					//文件名 例: c:\123(1).flv
					FilePath = Path.Combine(SaveDirectory.ToString(),
												  title + "(" + (i + 1).ToString() + ")" +
												  Path.GetExtension(videos[i])),
					//文件URL
					Url = url,				
				};
				//下载文件
				Network.DownloadFile(currentParameter);
			}
			delegates.Finish(new ParaFinish(this.TaskId, true));
		}

		//下载弹幕文件
		public void DownloadSub()
		{
			delegates.TipText(new ParaTipText(this.TaskId, "正在下载字幕文件"));
			//要分析的地址
			string url = Url;

			//取得网页源文件
			string src = Network.GetHtmlSource(url, Encoding.GetEncoding("GB2312"));

			//分析id和视频存放站点(type)
			Regex rId = new Regex(@"id=(?<id>\w*)&amp;type(|\w)=(?<type>\w*)");
			Match mId = rId.Match(src);
			//取得id和type值
			string id = mId.Groups["id"].Value;
			string type = mId.Groups["type"].Value;

			//取得视频标题
			Regex rTitle = new Regex(@"<title>(?<title>.*)</title>");
			Match mTitle = rTitle.Match(src);
			string title = mTitle.Groups["title"].Value;
			//过滤非法字符
			title = Tools.InvalidCharacterFilter(title, "");

			//取得字幕文件地址
			string subUrl = @"http://acfun.cn/newflvplayer/xmldata/%VideoId%/comment_on.xml?r=0.5446887564165".Replace(@"%VideoId%", id);
			//下载字幕文件
			Network.DownloadSub(new DownloadParameter()
			{
				Url = subUrl,
				FilePath = Path.Combine(SaveDirectory.ToString(), title + ".xml")
			});
			delegates.Finish(new ParaFinish(this.TaskId, true));
		}

		//停止下载
		public void StopDownloadVideo()
		{
			if (currentParameter != null)
			{
				//将停止flag设置为true
				currentParameter.IsStop = true;
			}
		}

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
		public string VideoTitle
		{
			get
			{
				return _title;
			}
		}


		string[] IDownloader.FilePath
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public string[] SubFilePath
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public DirectoryInfo SaveDirectory
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		#endregion
	}
}
