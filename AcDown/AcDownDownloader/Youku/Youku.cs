using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Kaedei.AcDown.Interface;
using Kaedei.AcDown.Interface.Forms;

namespace Kaedei.AcDown.Downloader
{
	[AcDownPluginInformation("YoukuDownloader", "优酷网下载插件", "Kaedei", "4.4.0.1220", "优酷网下载插件", "http://blog.sina.com.cn/kaedei")]
	public class YoukuPlugin : IPlugin
	{
		public YoukuPlugin()
		{
			Feature = new Dictionary<string, object>();
			//GetExample
			Feature.Add("ExampleUrl", new string[] { 
				"优酷网(Youku.com)下载插件:",
				"http://v.youku.com/vshow/idXMjY3ODgyNTAw.html",
				"http://v.youku.com/v_playlist/f5656465o1p0.html","",
				"优酷加密视频:(在地址后加“密码”字样)",
				"http://v.youku.com/vshow/idXMjY3ODgyNTAw.html密码",
				"http://v.youku.com/v_playlist/f5656465o1p0.html密码",
			});
			//AutoAnswer
			Feature.Add("AutoAnswer", new List<AutoAnswer>()
			{
					 new AutoAnswer("youku","mp4","优酷 高清(Mp4)"),
				new AutoAnswer("youku","hd2","优酷 超清(HD)"),
				new AutoAnswer("youku","flv","优酷 标清(Flv)")
			});
			//ConfigurationForm(不支持)
		}

		public IDownloader CreateDownloader()
		{
			return new YoukuDownloader();
		}

		public bool CheckUrl(string url)
		{
			Regex r = new Regex(@"^http://v\.youku\.com/\w+/(id_|id|)(?<id>\w+)");
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
		/// 规则为 Youku + 视频ID（字符或数字）
		/// 如 "YoukuXMjczNzUxMTYw"或"Youkuf5656465o1p0"
		/// </summary>
		public string GetHash(string url)
		{
			Regex r = new Regex(@"http://v\.youku\.com/v_\w+/(id_|)(?<id>\w+)");
			Match m = r.Match(url);
			if (m.Success)
			{
				if (!string.IsNullOrEmpty(m.Groups["id"].ToString()))
					return "Youku" + m.Groups["id"].ToString();
			}
			return null;

		}

		public Dictionary<string, object> Feature { get; private set; }

		public SerializableDictionary<string, string> Configuration { get; set; }

	}

	public class YoukuDownloader : IDownloader
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

			//获取密码
			string password = "";
			if (Info.Url.EndsWith("密码"))
				password = ToolForm.CreatePasswordForm(true, "", "");

			//取得网页源文件
			string src = Network.GetHtmlSource(Info.Url.Replace("密码", ""), Encoding.UTF8, Info.Proxy);

			//分析视频id
			Regex r1 = new Regex(@"videoId = '(?<vid>\w+)'");
			Match m1 = r1.Match(src);
			string vid = m1.Groups["vid"].ToString();

			//取得视频标题
			Regex rTitle = new Regex(@"<title>(?<title>.*)</title>");
			Match mTitle = rTitle.Match(src);
			string title = mTitle.Groups["title"].Value.Replace("—优酷网，视频高清在线观看", "").Replace("—在线播放", "").Replace("—电视剧", "").Replace("—电影", "").Replace("—综艺", "");

			Info.Title = title;
			//过滤非法字符
			title = Tools.InvalidCharacterFilter(title, "");

			//视频地址数组
			string[] videos = null;
			//清空地址
			Info.FilePath.Clear();

			//调用内建的优酷视频解析器
			YoukuParser parserYouku = new YoukuParser();
			videos = parserYouku.Parse(new ParseRequest() { Id = vid, Password = password, Proxy = Info.Proxy, AutoAnswers = Info.AutoAnswer }).ToArray();

			//下载视频
			//确定视频共有几个段落
			Info.PartCount = videos.Length;

			//分段落下载
			for (int i = 0; i < Info.PartCount; i++)
			{
				Info.CurrentPart = i + 1;

				//取得文件后缀名
				//string ext = Tools.GetExtension(videos[i]);
				//设置当前DownloadParameter
				if (Info.PartCount == 1) //如果只有一段
				{
					currentParameter = new DownloadParameter()
					{
						//文件名 例: c:\123(1).flv
						FilePath = Path.Combine(Info.SaveDirectory.ToString(),
													  title + ".flv"),
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
													  title + "(" + (i + 1).ToString() + ")" + ".flv"),
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
			}//end for

			//视频合并
			if (Info.FilePath.Count > 1 && !Info.PartialFinished)
			{
				Info.Settings.Remove("VideoCombine");
				var arg = new StringBuilder();
				foreach (var item in Info.FilePath)
				{
					arg.Append(item);
					arg.Append("|");
				}
				arg.Append(Path.Combine(Info.SaveDirectory.ToString(), title + ".mp4"));
				Info.Settings["VideoCombine"] = arg.ToString();
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
}
