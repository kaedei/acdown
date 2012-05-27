using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;
using System.IO;
using Kaedei.AcDown.Parser;
using Kaedei.AcDown.Interface.Forms;

namespace Kaedei.AcDown.Downloader
{
	[AcDownPluginInformation("TudouDownloader", "土豆网下载插件", "Kaedei", "3.11.7.527", "土豆网下载插件", "http://blog.sina.com.cn/kaedei")]
	public class TudouPlugin : IAcdownPluginInfo
	{

		public TudouPlugin()
		{
			Feature = new Dictionary<string, object>();
			//GetExample
			Feature.Add("ExampleUrl", new string[] { 
				"土豆网(Tudou.com)下载插件:",
				"http://www.tudou.com/playlist/p/l12302995.html",
				"http://www.tudou.com/programs/view/scMdGug3bgY/","",
				"土豆网加密视频:(在地址后加“密码”字样)",
				"http://www.tudou.com/playlist/p/l12302995.html密码",
				"http://www.tudou.com/programs/view/JiTcS97DBHo/密码",
			});
			//AutoAnswer
			Feature.Add("AutoAnswer", new List<AutoAnswer>()
			{
					 new AutoAnswer("tudou","3","土豆 高清(720P)"),
				new AutoAnswer("tudou","99","土豆 原画"),
				new AutoAnswer("tudou","2","土豆 清晰(360P)"),
					 new AutoAnswer("tudou","1","土豆 流畅(256P)")
			});
			//ConfigurationForm(不支持)
		}

		public IDownloader CreateDownloader()
		{
			return new TudouDownloader();
		}

		public bool CheckUrl(string url)
		{
			Regex r = new Regex(@"^http://www\.tudou\.com/(playlist(/p/(?<id1>\w+)|/playindex\.do\?lid=(?<id2>\w+))|listplay/(.+?/|)(?<id3>.+?)(?=\.html)|programs/view/(?<id4>[\w+\-]+))");
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
		/// 规则为 tudou + 视频ID（或视频lid）
		/// 如 "tudouYDn_zTq_8gI"或"tudou608662"
		/// </summary>
		public string GetHash(string url)
		{
			Regex r = new Regex(@"(?<=http://www\.tudou\.com/)(playlist(/p/(?<id1>\w+)|/playindex\.do\?lid=(?<id2>\w+))|listplay/(.+?/|)(?<id3>.+?)(?=\.html)|programs/view/(?<id4>[\w+\-]+))");
			Match m = r.Match(url);
			if (m.Success)
			{
				if (!string.IsNullOrEmpty(m.Groups["id1"].ToString()))
					return "tudou" + m.Groups["id1"].ToString();
				else if (!string.IsNullOrEmpty(m.Groups["id2"].ToString()))
				{
					return "tudou" + m.Groups["id2"].ToString();
				}
				else if (!string.IsNullOrEmpty(m.Groups["id3"].ToString()))
				{
					return "tudou" + m.Groups["id3"].ToString();
				}
				else if (!string.IsNullOrEmpty(m.Groups["id4"].ToString()))
				{
					return "tudou" + m.Groups["id4"].ToString();
				}
			}
			return null;

		}

		public Dictionary<string, object> Feature { get; private set; }

		public SerializableDictionary<string, string> Configuration { get; set; }

	}

	public class TudouDownloader : IDownloader
	{

		public TaskInfo Info { get; set; }

		//下载参数
		DownloadParameter currentParameter;
		
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
			delegates.TipText(new ParaTipText(this.Info, "正在分析视频地址"));

			try
			{
				//获取密码
				string password = "";
				if (Info.Url.EndsWith("密码"))
				{
					password = ToolForm.CreatePasswordForm(true, "", "");
					Info.Url.Replace("密码", "");
				}

				//取得网页源文件
				string src = Network.GetHtmlSource(Info.Url, Encoding.GetEncoding("GBK"), Info.Proxy);

				//分析视频iid
				string iid = "";

				//取得iid
				Regex rlist = new Regex(@"(a|l)(?<aid>\d+)(i(?<iid>\d+)|)(?=\.html)");
				Match mlist = rlist.Match(Info.Url);
				if (mlist.Success) //如果是列表中的视频
				{
					//尝试取得url中的iid
					if (!string.IsNullOrEmpty(mlist.Groups["iid"].Value))
						iid = mlist.Groups["iid"].Value;
					//否则取得源文件中的iid
					Regex r1 = new Regex(@"defaultIid = (?<iid>\d.*)");
					Match m1 = r1.Match(src);
					iid = m1.Groups["iid"].ToString();
				}
				else //如果是普通视频(或新列表视频)
				{
					//URL中获取id
					var mIdInUrl = Regex.Match(Info.Url, @"listplay/(?<l>.+?)/(?<i>.+?)(?=\.html)");
					if (mIdInUrl.Success)
					{
						iid = mIdInUrl.Groups["i"].Value;
					}
					else
					{
						
						var mIdInSrc = Regex.Match(src, @"(?<=listData = \[{\niid:)\w+");
						if (mIdInSrc.Success)
						{
							iid = mIdInSrc.Value;
						}
						else
						{
							Regex r1 = new Regex(@"(I|i)id = (?<iid>\d.*)");
							Match m1 = r1.Match(src);
							iid = m1.Groups["iid"].ToString();
						}
					}
				}

				//取得视频标题
				string title = "";

				if (mlist.Success)
				{
					//取得aid/lid标题
					string aid = mlist.Groups["aid"].Value;
					Regex rlisttitle = null;
					Match mlisttitle = null;
					if (mlist.ToString().StartsWith("a")) //如果是a开头的列表
					{
						rlisttitle = new Regex(@"aid:" + aid + @"\n,name:""(?<title>.+?)""", RegexOptions.Singleline);
						mlisttitle = rlisttitle.Match(src);
					}
					else if (mlist.ToString().StartsWith("l")) //如果是l开头的列表
					{
						rlisttitle = new Regex(@"ltitle = ""(?<title>.+?)""", RegexOptions.Singleline);
						mlisttitle = rlisttitle.Match(src);
					}
					//取得iid标题
					Regex rsubtitle = new Regex(@"iid:" + iid + @"\n(,cartoonType:\d+\n|),title:""(?<subtitle>.+?)""", RegexOptions.Singleline);
					Match msubtitle = rsubtitle.Match(src);
					title = mlisttitle.Groups["title"].Value + "-" + msubtitle.Groups["subtitle"].Value;
				}
				else
				{
					Regex rTitle = new Regex(@"\<h1\>(?<title>.*)\<\/h1\>");
					Match mTitle = rTitle.Match(src);
					title = mTitle.Groups["title"].Value;
				}
				Info.Title = title;
				//过滤非法字符
				title = Tools.InvalidCharacterFilter(title, "");

				//视频地址数组
				string[] videos = null;
				//清空地址
				Info.FilePath.Clear();

				//调用内建的土豆视频解析器
				TudouParser parserTudou = new TudouParser();
				videos = parserTudou.Parse(new ParseRequest() { Id = iid, Password = password, Proxy = Info.Proxy, AutoAnswers = Info.AutoAnswer }).ToArray();

				//下载视频
				//确定视频共有几个段落
				Info.PartCount = videos.Length;

				//分段落下载
				for (int i = 0; i < Info.PartCount; i++)
				{
					Info.CurrentPart = i + 1;

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
														  title + ext),
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
														  title + "(" + (i + 1).ToString() + ")" + ext),
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
					delegates.NewPart(new ParaNewPart(this.Info, i + 1));

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
							throw ex;
						}
						else //否则继续下载，设置“部分失败”状态
						{
							Info.PartialFinished = true;
							Info.PartialFinishedDetail += "\r\n文件: " + currentParameter.Url + " 下载失败";
						}
					}
				}
			}
			catch (Exception ex) //出现错误即下载失败
			{
				throw ex;
			}
			//下载成功完成
			return true;

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

	}

}//end namespace
