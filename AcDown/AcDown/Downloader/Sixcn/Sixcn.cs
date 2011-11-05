using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;
using System.IO;
using Kaedei.AcDown.Interface.Forms;
using Kaedei.AcDown.Parser;

namespace Kaedei.AcDown.Downloader
{

	public class SixcnPlugin : IAcdownPluginInfo
	{
		
		#region IAcdownPluginInfo 成员

		public string Name
		{
			get { return "SixcnDownloader"; }
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
			get { return "6.cn下载插件"; }
		}

		public string SupportUrl
		{
			get { return @"http://blog.sina.com.cn/kaedei"; }
		}

		public IDownloader CreateDownloader()
		{
			return new SixcnDownloader(this);
		}

		public bool CheckUrl(string url)
		{
			Regex r = new Regex(@"^http://6\.cn/(watch|plist)/(?<id>(\d+|\d+/\d+))\.html");
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
		/// 规则为 6cn + url中的视频ID
		/// 如 "6cn229333/2"或"6cn15147433"
		/// </summary>
		public string GetHash(string url)
		{
			Regex r = new Regex(@"http://6\.cn/(watch|plist)/(?<id>(\d+|\d+/\d+))\.html");
			Match m = r.Match(url);
			if (m.Success)
			{
				return "6cn" + m.Groups["id"].ToString();
			}
			return null;

		}


		public string[] GetUrlExample()
		{
			return new string[] { 
				"六间房(6.cn)下载插件:",
				"http://6.cn/watch/15147433.html",
				"http://6.cn/plist/229333/2.html"
			};
		}

		#endregion
	} //end plugin class

	public class SixcnDownloader : IDownloader
	{

		public SixcnDownloader(SixcnPlugin p)
		{
			_basePlugin = p;
		}
		//插件
		SixcnPlugin _basePlugin;
		public IAcdownPluginInfo GetBasePlugin() { return _basePlugin; }

		//下载参数
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

		//下载视频
		public void Download()
		{
			//开始下载
			delegates.Start(new ParaStart(this.TaskId));
			delegates.TipText(new ParaTipText(this.TaskId, "正在分析视频地址"));
			_status = DownloadStatus.正在下载;
			try
			{
				//获取密码
				string password = "";
				if (Url.EndsWith("密码"))
					password = ToolForm.CreatePasswordForm(true, "", "");

				//取得网页源文件
				string src = Network.GetHtmlSource(Url.Replace("密码", ""), Encoding.UTF8, delegates.Proxy);

				//分析视频evid
				string evid = "";

				//取得evid
				Regex r1 = new Regex(@"evid = '(?<evid>.+)'");
				Match m1 = r1.Match(src);
				evid = m1.Groups["evid"].ToString();

				//取得视频标题
				Regex rTitle = new Regex(@"<h(1|2) class=$(vt|pvt)$>(?<title>.+?)<".Replace("$","\""));
				Match mTitle = rTitle.Match(src);
				string title = mTitle.Groups["title"].Value;
				//过滤非法字符
				title = Tools.InvalidCharacterFilter(title, "");
				_title = title;

				//视频地址数组
				string[] videos = null;
				//清空地址
				_filePath.Clear();

				//调用内建的土豆视频解析器
				SixcnParser parserSixcn = new SixcnParser();
				videos = parserSixcn.Parse(new string[] { evid, password }, delegates.Proxy);

				//下载视频
				//确定视频共有几个段落
				_partCount = videos.Length;

				//分段落下载
				for (int i = 0; i < _partCount; i++)
				{
					_currentPart = i + 1;
					//提示更换新Part
					delegates.NewPart(new ParaNewPart(this.TaskId, i + 1));
					//取得文件后缀名
					string ext = Tools.GetExtension(videos[i]);
					//设置当前DownloadParameter
					if (_partCount == 1) //如果只有一段
					{
						currentParameter = new DownloadParameter()
						{
							//文件名 例: c:\123(1).flv
							FilePath = Path.Combine(SaveDirectory.ToString(),
														  _title + "." + ext),
							//文件URL
							Url = videos[i]
						};
					}
					else //如果分段有多段
					{
						currentParameter = new DownloadParameter()
						{
							//文件名 例: c:\123(1).flv
							FilePath = Path.Combine(SaveDirectory.ToString(),
														  _title + "(" + (i + 1).ToString() + ")" + "." + ext),
							//文件URL
							Url = videos[i]
						};
					}

					//添加文件路径到List<>中
					_filePath.Add(currentParameter.FilePath);
					//下载文件
					bool success;
					//添加断点续传段
					if (File.Exists(currentParameter.FilePath))
					{
						//取得文件长度
						int len = int.Parse(new FileInfo(currentParameter.FilePath).Length.ToString());
						//设置RangeStart属性
						currentParameter.RangeStart = len;
					}
					//下载视频文件
					success = Network.DownloadFile(currentParameter);
					//未出现错误即用户手动停止
					if (!success)
					{
						_status = DownloadStatus.已经停止;
						delegates.Finish(new ParaFinish(this.TaskId, false));
						return;
					}
				}
			}
			catch (Exception ex) //出现错误即下载失败
			{
				_status = DownloadStatus.出现错误;
				delegates.Error(new ParaError(this.TaskId, ex));
				return;
			}
			//下载成功完成
			_status = DownloadStatus.下载完成;
			delegates.Finish(new ParaFinish(this.TaskId, true));

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
