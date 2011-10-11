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
			get { return new Version(1,1,0,0); }
		}

		public string Describe
		{
			get { return @"Acfun.tv下载插件"; }
		}

		public string SupportUrl
		{
			get { return @"http://blog.sina.com.cn/kaedei"; }
		}

		public IDownloader CreateDownloader()
		{
			return new AcfunDownloader(this);
		}

		public bool CheckUrl(string url)
		{
			Regex r = new Regex(@"^http://((www\.|)acfun\.tv|.*?)/v/ac(?<id>\d+)");
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
		/// 规则为 acfun + 视频ID + 下划线 + 子视频编号
		/// 如 "acfun158539_"或"acfun123456_2"
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public string GetHash(string url)
		{
			Regex r = new Regex(@"^http://((www\.|)acfun\.tv|.*?)/v/ac(?<id>\d+)(/index_(?<subid>\d+)\.html|)");
			Match m = r.Match(url);
			if (m.Success)
			{
				return "acfun" + m.Groups["id"].Value + "_" + m.Groups["subid"].Value;
			}
			else
			{
				return null;
			}
		}

		public string[] GetUrlExample()
		{
			return new string[] { 
				"AcFun.tv下载插件:",
				"支持识别各Part名称",
				"http://acfun.tv/v/ac06020",
				"http://www.acfun.tv/v/ac206020",
				"http://124.228.254.229/v/ac206020 (IP地址形式)"
			};
		}



	}

	/// <summary>
	/// Acfun下载器
	/// </summary>
	public class AcfunDownloader : IDownloader
	{

		public AcfunDownloader(AcFunPlugin p)
		{
			_basePlugin = p;
		}
		//插件
		AcFunPlugin _basePlugin;
		public IAcdownPluginInfo GetBasePlugin() { return _basePlugin; }
		//服务器IP地址
		private const string ServerIP = "124.228.254.229";
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

		//下载视频
		public void Download()
		{ 
			//开始下载
			delegates.Start(new ParaStart(this.TaskId));
			delegates.TipText(new ParaTipText(this.TaskId, "正在分析视频地址"));
			_status = DownloadStatus.正在下载;

			////要分析的地址
			string url = Url.Replace("www.acfun.tv", ServerIP);
			url = url.Replace("acfun.tv", ServerIP);

			try
			{
				//取得网页源文件
				string src = Network.GetHtmlSource(url, Encoding.GetEncoding("GB2312"), delegates.Proxy);

				//分析id和视频存放站点(type)
				string type;
				string id = ""; //视频id
				string ot = ""; //视频子id

				//检查是否为Flash游戏
				Regex rFlash = new Regex(@"data=""(?<flash>.+?\.swf)""");
				Match mFlash = rFlash.Match(src);

				//如果是Flash游戏
				if (mFlash.Success)
				{
					type = "game";
				}
				else
				{
					//先取得embed块的源代码
					Regex rEmbed = new Regex(@"<embed (?<content>.*)>");
					Match mEmbed = rEmbed.Match(src);
					string embedSrc = mEmbed.Groups["content"].Value.Replace("type=\"application/x-shockwave-flash\"", "");

					//取得id值
					Regex rId = new Regex(@"id=(?<id>\w*)(?<ot>(-\w*|))");
					Match mId = rId.Match(embedSrc);
					id = mId.Groups["id"].Value;
					ot = mId.Groups["ot"].Value;

					//取得type值
					Regex rType = new Regex(@"type(|\w)=(?<type>\w*)");
					Match mType = rType.Match(embedSrc);
					type = mType.Groups["type"].Value;
				}

				//取得视频标题
				Regex rTitle = new Regex(@"<title>(?<title>.*)</title>");
				Match mTitle = rTitle.Match(src);
				string title = mTitle.Groups["title"].Value.Replace(" - AcFun.tv","");

				//取得子标题
				Regex rSubTitle = new Regex(@"<option value='\w+?\.html'(?<isselected>(| selected))>(?<content>.+)</option>");
				MatchCollection mSubTitles = rSubTitle.Matches(src);
				 //如果存在下拉列表框
				if (mSubTitles.Count > 0)
				{
					bool findSelected = false;
					foreach (Match item in mSubTitles)
					{
						if (!string.IsNullOrEmpty(item.Groups["isselected"].Value))
						{
							title = title + " - " + item.Groups["content"].Value;
							findSelected = true;
						}
					}
					if (!findSelected)
					{
						title = title + " - " + mSubTitles[0].Groups["content"].Value;
					}
				}

				_title = title;
				//过滤非法字符
				title = Tools.InvalidCharacterFilter(title, "");

				//视频地址数组
				string[] videos = null;
				//清空地址
				_filePath.Clear();

				if (GlobalSettings.GetSettings().TasksInfomation[this.TaskId].DownSub != DownloadSubtitleType.DownloadSubtitleOnly)
				{
					//检查type值
					switch (type)
					{
						case "video": //新浪视频
							//解析视频
							SinaVideoParser parserSina = new SinaVideoParser();
							videos = parserSina.Parse(new string[] { id }, delegates.Proxy);
							break;
						case "qq": //QQ视频
							//解析视频
							QQVideoParser parserQQ = new QQVideoParser();
							videos = parserQQ.Parse(new string[] { id }, delegates.Proxy);
							break;
						case "youku": //优酷视频
							//解析视频
							YoukuParser parserYouKu = new YoukuParser();
							videos = parserYouKu.Parse(new string[] { id }, delegates.Proxy);
							break;
						case "game": //flash游戏
							videos = new string[] { mFlash.Groups["flash"].Value };
							break;
					}

					//下载视频
					//确定视频共有几个段落
					_partCount = videos.Length;

					//------------分段落下载------------
					for (int i = 0; i < _partCount; i++)
					{
						_currentPart = i + 1;

						//取得文件后缀名
						string ext = Tools.GetExtension(videos[i]);
						if (string.IsNullOrEmpty(ext))
						{
							if (string.IsNullOrEmpty(Path.GetExtension(videos[i])))
								ext = ".flv";
							else
								ext = Path.GetExtension(videos[i]);
						}
						//设置当前DownloadParameter
						if (_partCount == 1)
						{
							currentParameter = new DownloadParameter()
							{
								//文件名 例: c:\123(1).flv
								FilePath = Path.Combine(SaveDirectory.ToString(),
											title + ext),
								//文件URL
								Url = videos[i]
							};
						}
						else
						{
							currentParameter = new DownloadParameter()
							{
								//文件名 例: c:\123(1).flv
								FilePath = Path.Combine(SaveDirectory.ToString(),
											title + "(" + (i + 1).ToString() + ")" + ext),
								//文件URL
								Url = videos[i]
							};
						}

						//添加断点续传段
						if (File.Exists(currentParameter.FilePath))
						{
							//取得文件长度
							int len = int.Parse(new FileInfo(currentParameter.FilePath).Length.ToString());
							//设置RangeStart属性
							currentParameter.RangeStart = len;
							_title = "[续传]" + title;
						}
						else
						{
							_title = _title.Replace("[续传]", "");
						}

						//设置代理服务器
						currentParameter.Proxy = delegates.Proxy;
						//添加文件路径到List<>中
						_filePath.Add(currentParameter.FilePath);
						//下载文件
						bool success;


						//提示更换新Part
						delegates.NewPart(new ParaNewPart(this.TaskId, i + 1));

						//下载视频
						success = Network.DownloadFile(currentParameter);

						if (!success) //未出现错误即用户手动停止
						{
							_status = DownloadStatus.已经停止;
							delegates.Finish(new ParaFinish(this.TaskId, false));
							return;
						}
					} //end for

				}

				if (GlobalSettings.GetSettings().TasksInfomation[TaskId].DownSub == DownloadSubtitleType.DownloadSubtitle)
				{
					//----------下载字幕-----------
					delegates.TipText(new ParaTipText(this.TaskId, "正在下载字幕文件"));
					//字幕文件(on)地址
					string subfile = Path.Combine(SaveDirectory.ToString(), title + "[未锁定].xml");
					_subFilePath.Add(subfile);
					//取得字幕文件(on)地址
					string subUrl = @"http://124.228.254.234/newflvplayer/xmldata/%VideoId%/comment_on.xml?r=0.9138414077460766".Replace(@"%VideoId%", id + (ot.Length > 2 ? ot : ""));
					//下载字幕文件
					try
					{
						Network.DownloadSub(new DownloadParameter()
							{
								Url = subUrl,
								FilePath = subfile,
								Proxy = delegates.Proxy
							});
					}
					catch { }
					//字幕文件(lock)地址
					subfile = Path.Combine(SaveDirectory.ToString(), title + "[锁定].xml");
					_subFilePath.Add(subfile);
					//取得字幕文件(lock)地址
					subUrl = @"http://124.228.254.234/newflvplayer/xmldata/%VideoId%/comment_lock.xml?r=0.5152998301200569".Replace(@"%VideoId%", id + (ot.Length > 2 ? ot : ""));
					//下载字幕文件
					try
					{
						Network.DownloadSub(new DownloadParameter()
						{
							Url = subUrl,
							FilePath = subfile,
							Proxy = delegates.Proxy
						});
					}
					catch { }
				}
			
			}
			catch(Exception ex)
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

		#endregion
	}
}
