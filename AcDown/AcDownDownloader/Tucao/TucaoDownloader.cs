using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;
using System.IO;
using Kaedei.AcDown.Interface.Forms;
using System.Net;
using System.Security.Cryptography;
using Kaedei.AcDown.Downloader.Tucao;
using Kaedei.AcDown.Interface.Downloader;
using Newtonsoft.Json;

namespace Kaedei.AcDown.Downloader
{
	/// <summary>
	/// Tucao下载器
	/// </summary>
	public class TucaoDownloader : CommonDownloader
	{
		private const string API_KEY = "25tids8f1ew1821ed";
		//下载视频
		public override bool Download()
		{
			//开始下载
			TipText("正在分析视频地址");
			
			//清空地址
			Info.FilePath.Clear();

			//从url中获得hid和partId
			var urlMatch = Regex.Match(Info.Url, @"tucao\.cc/play/h(?<hid>\d+)(?:/#(?<partId>\d+))?", RegexOptions.IgnoreCase);
			int hid = int.Parse(urlMatch.Groups["hid"].Value);
			int partId = int.Parse(urlMatch.Groups["partId"].Success ? urlMatch.Groups["partId"].Value : "1");

			//取得视频信息
			var videoInfo = JsonConvert.DeserializeObject<TucaoVideoInfo>(
				Network.GetHtmlSource(string.Format(@"http://www.tucao.cc/api_v2/view.php?hid={0}&apikey={1}", hid, API_KEY),
					Encoding.UTF8));

			var currentPart = videoInfo.result.video[partId - 1];
			string title = videoInfo.result.title;
			string subtitle = currentPart.title;
			if (title.Equals(subtitle))
				subtitle = "";

			if (!string.IsNullOrEmpty(subtitle)) //如果存在子标题（视频为合集）
			{
				//更改标题
				title = title + " - " + subtitle;
				//更改URL防止hash时出错
				Info.Url = Info.Url + "#" + partId;

			}

			//过滤非法字符
			Info.Title = title;
			title = Tools.InvalidCharacterFilter(title, "");


			//视频地址数组
			string[] videos = null;

			//如果允许下载视频
			if ((Info.DownloadTypes & DownloadType.Video) != 0)
			{
				//获取视频地址
				var video = new TucaoInterfaceParser().Parse(currentPart.type, currentPart.vid, Info.Proxy);
				videos = video.ToArray();

				//下载视频
				//确定视频共有几个段落
				Info.PartCount = videos.Length;

				//------------分段落下载------------
				for (int i = 0; i < Info.PartCount; i++)
				{
					Info.CurrentPart = i + 1;

					//取得文件后缀名
					string ext = Tools.GetExtension(videos[i]);
					if (string.IsNullOrEmpty(ext))
					{
						if (string.IsNullOrEmpty(Path.GetExtension(videos[i])))
							ext = ".flv";
						else
							ext = Path.GetExtension(videos[i]);
					}
					if (ext == ".hlv") ext = ".flv";
					//设置当前DownloadParameter
					if (Info.PartCount == 1)
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
					else
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

				} //end for
			}
			//下载弹幕
			if (((Info.DownloadTypes & DownloadType.Subtitle) != 0))
			{
				//----------下载字幕-----------
				delegates.TipText(new ParaTipText(this.Info, "正在下载字幕文件"));
				//字幕文件(on)地址
				string subfile = Path.Combine(Info.SaveDirectory.ToString(), title + ".xml");
				Info.SubFilePath.Add(subfile);
				//取得字幕文件(on)地址
				string subUrl =
					string.Format("http://www.tucao.cc/index.php?m=mukio&c=index&a=init&playerID=11-{0}-1-{1}&r=0.5134681154294", hid,
						partId);
				//下载字幕文件
				try
				{
					Network.DownloadFile(new DownloadParameter()
						{
							Url = subUrl,
							FilePath = subfile,
							Proxy = Info.Proxy
						});
				}
				catch
				{
					//Info.PartialFinished = true;
					//Info.PartialFinishedDetail += "\r\n弹幕文件文件下载失败";
				}
			}// end 下载弹幕xml

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

	}
}
