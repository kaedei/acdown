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
	public class TudouPlugin : IAcdownPluginInfo
	{
		#region IAcdownPluginInfo 成员

		public string Name
		{
			get { return "TudouDownloader"; }
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
			get { return "土豆网下载插件"; }
		}

		public string SupportUrl
		{
			get { return @"http://blog.sina.com.cn/kaedei"; }
		}

		public IDownloader CreateDownloader()
		{
			return new TudouDownloader();
		}

		public bool CheckUrl(string url)
		{
			Regex r = new Regex(@"^http://www\.tudou\.com/(programs/view/(?<id1>.*)/|playlist/playindex.do\?lid=(?<id2>\d*)|playlist/p/(?<id3>\w+)\.html)");
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
			Regex r = new Regex(@"http://www\.tudou\.com/(programs/view/(?<id1>.*)/|playlist/playindex.do\?lid=(?<id2>\d*)|playlist/p/(?<id3>\w+)\.html)");
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
			}
			return null;

		}


		public string[] GetUrlExample()
		{
			return new string[] { 
				"土豆网(Tudou.com)下载插件:",
				"http://www.tudou.com/playlist/p/l12302995.html",
				"http://www.tudou.com/programs/view/scMdGug3bgY/","",
				"土豆网加密视频:(在地址后加“密码”字样)",
				"http://www.tudou.com/playlist/p/l12302995.html密码",
				"http://www.tudou.com/programs/view/JiTcS97DBHo/密码",
			};
		}

		#endregion
	}

	public class TudouDownloader : IDownloader
	{

		public TaskInfo Info { get; set; }

		//下载参数
		DownloadParameter currentParameter;

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
			delegates.TipText(new ParaTipText(this.Info, "正在分析视频地址"));
			Info.Status = DownloadStatus.正在下载;
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
				string src = Network.GetHtmlSource2(Info.Url, Encoding.GetEncoding("GBK"), Info.Proxy);

				//分析视频iid
				string iid = "";
				////确定URL类型
				//Regex r = new Regex(@"http://www\.tudou\.com/(programs/view/(?<id1>.*)/|playlist/playindex.do\?lid=(?<id2>\d*))");
				//Match m = r.Match(Url);

				//取得iid
				Regex rlist = new Regex(@"a(?<aid>\d+)i(?<iid>\d+)");
				Match mlist = rlist.Match(Info.Url);
				if (mlist.Success)
				{
					iid = mlist.Groups["iid"].Value;
				}
				else
				{
					Regex r1 = new Regex(@"(I|i)id = (?<iid>\d.*)");
					Match m1 = r1.Match(src);
					iid = m1.Groups["iid"].ToString();
				}

				//取得视频标题
				string title = "";
				
				if (mlist.Success)
				{
					string aid = mlist.Groups["aid"].Value;
					Regex rlisttitle = new Regex(@"aid:" + aid + @"\n,name:""(?<title>.+?)""", RegexOptions.Singleline);
					Match mlisttitle = rlisttitle.Match(src);
					Regex rsubtitle = new Regex(@"iid:" + iid + @"\n,cartoonType:\d+\n,title:""(?<subtitle>.+?)""", RegexOptions.Singleline);
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
				videos = parserTudou.Parse(new string[] { iid, password }, Info.Proxy);

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
					//土豆不支持断点续传
					////添加断点续传段
					//if (File.Exists(currentParameter.FilePath))
					//{
					//   //取得文件长度
					//   int len = int.Parse(new FileInfo(currentParameter.FilePath).Length.ToString());
					//   //设置RangeStart属性
					//   currentParameter.RangeStart = len;
					//   Info.Title = "[续传]" + Info.Title;
					//}
					//else
					//{
					//   Info.Title = Info.Title.Replace("[续传]", "");
					//}

					//提示更换新Part
					delegates.NewPart(new ParaNewPart(this.Info, i + 1));

					//下载视频文件
					success = Network.DownloadFile(currentParameter, this.Info);

					//未出现错误即用户手动停止
					if (!success)
					{
						Info.Status = DownloadStatus.已经停止;
						delegates.Finish(new ParaFinish(this.Info, false));
						return;
					}
				}
			}
			catch (Exception ex) //出现错误即下载失败
			{
				Info.Status = DownloadStatus.出现错误;
				delegates.Error(new ParaError(this.Info, ex));
				return;
			}
			//下载成功完成
			Info.Status = DownloadStatus.下载完成;
			delegates.Finish(new ParaFinish(this.Info, true));

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

}//end namespace
