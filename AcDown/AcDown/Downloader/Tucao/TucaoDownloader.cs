using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;
using System.IO;
using Kaedei.AcDown.Parser;
using Kaedei.AcDown.Interface.Forms;
using System.Net;

namespace Kaedei.AcDown.Downloader
{
	/// <summary>
	/// Tucao下载器
	/// </summary>
	public class TucaoDownloader : IDownloader
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

			string url = Info.Url;
			//视频地址数组
			string[] videos = null;

			try
			{
				//取得网页源文件
				string src = Network.GetHtmlSource(url, Encoding.UTF8, Info.Proxy);

				//视频id
				string id = "";
				//type值
				string type = "";
				//player id
				string playerId = "";
				//选择的视频（下拉列表）
				int selectedvideo = 0;

				//清空地址
				Info.FilePath.Clear();

				//取得视频标题
				Regex rTitle = new Regex(@"<title>(?<title>.*)</title>");
				Match mTitle = rTitle.Match(src);

				//分析id和视频存放站点(type)
				//取得"mplayer块的源代码
				Regex rEmbed = new Regex(@"<div id=""mplayer"">.+?</embed>", RegexOptions.Singleline);
				Match mEmbed = rEmbed.Match(src);
				string embedSrc = mEmbed.Value;

				//取得id值
				Regex rId = new Regex(@"\w+id=(?<id>\w+)");
				MatchCollection mIds = rId.Matches(embedSrc);
				//取得type值
				Regex rType = new Regex(@"type=(?<type>\w+)");
				MatchCollection mTypes = rType.Matches(embedSrc);
				//取得PlayerID值
				Regex rPlayerid = new Regex(@"<li>(?<playerid>content.+?)</li>");
				Match mPlayerid = rPlayerid.Match(embedSrc);
				playerId = mPlayerid.Groups["playerid"].Value;
				//取得所有子标题
				Regex rSubTitle = new Regex(@"\|(?<subtitle>.*?)(\*\*|</li>)");
				MatchCollection mSubTitles = rSubTitle.Matches(embedSrc);


				Match mId = null;
				Match mType = null;
				Match mSubTitle = null;
				if (mIds.Count > 1) //如果数量大于一个
				{
					List<string> texts = new List<string>();
					for (int i = 0; i < mIds.Count; i++)
					{
						texts.Add((i + 1).ToString() + "、" + mSubTitles[i].Groups["subtitle"].Value);
					}
					//用户选择下载哪一个视频
					selectedvideo = ToolForm.CreateSelectServerForm("请选择视频：", texts.ToArray(), 0);
					mId = mIds[selectedvideo];
					mType = mTypes[selectedvideo];
					mSubTitle = mSubTitles[selectedvideo];
				}
				else
				{
					mId = mIds[0];
					mType = mTypes[0];
					mSubTitle = mSubTitles[0];
				}

				//设置标题
				string title = mTitle.Groups["title"].Value.Replace("- tucao, 吐槽_弹幕", "");
				string subTitle = mSubTitle.Groups["subtitle"].Value;
				if (!string.IsNullOrEmpty(subTitle)) //如果存在子标题（视频为合集）
				{
					//更改标题
					title = title + " - " + subTitle;
					//更改URL防止hash时出错
					Info.Url = Info.Url + "#" + subTitle;

				}
				//过滤非法字符
				Info.Title = title;
				title = Tools.InvalidCharacterFilter(title, "");

				//取得ID
				id = mId.Groups["id"].Value;
				//取得type值
				type = mType.Groups["type"].Value;

				DownloadSubtitleType downsub = Info.DownSub;
				//如果不是“仅下载字幕”
				if (downsub != DownloadSubtitleType.DownloadSubtitleOnly)
				{
					//检查外链
					switch (type)
					{
						case "qq": //QQ视频
							//解析视频
							QQVideoParser parserQQ = new QQVideoParser();
							videos = parserQQ.Parse(new string[] { id }, Info.Proxy);
							break;
						case "youku": //优酷视频
							//解析视频
							YoukuParser parserYouKu = new YoukuParser();
							videos = parserYouKu.Parse(new string[] { id }, Info.Proxy);
							break;
						case "tudou": //土豆视频
							//解析视频
							TudouParser parserTudou = new TudouParser();
							videos = parserTudou.Parse(new string[] { id }, Info.Proxy);
							break;
						case "sina": //新浪视频
							SinaVideoParser parserSina = new SinaVideoParser();
							videos = parserSina.Parse(new string[] { id }, Info.Proxy);
							break;
					}

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
						//添加断点续传段
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

						//下载视频
						success = Network.DownloadFile(currentParameter, this.Info);

						if (!success) //未出现错误即用户手动停止
						{
							Info.Status = DownloadStatus.已经停止;
							delegates.Finish(new ParaFinish(this.Info, false));
							return;
						}
					} //end for
				}
				//下载弹幕
				if ((downsub != DownloadSubtitleType.DontDownloadSubtitle) && !string.IsNullOrEmpty(playerId))
				{
					//----------下载字幕-----------
					delegates.TipText(new ParaTipText(this.Info, "正在下载字幕文件"));
					//字幕文件(on)地址
					string subfile = Path.Combine(Info.SaveDirectory.ToString(), title + ".xml");
					Info.SubFilePath.Add(subfile);
					//取得字幕文件(on)地址
					string subUrl = "http://www.tucao.cc/index.php?m=comment&c=mukio&a=init&type=" + type + "&playerID=" + playerId + "~" + selectedvideo.ToString() + "&r=0.09502756828442216";
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
					catch { }
				}// end 下载弹幕xml
			} 
			catch (Exception ex)
			{
				Info.Status = DownloadStatus.出现错误;
				delegates.Error(new ParaError(this.Info, ex));
				return;
			}//end try
		
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
}
