using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;
using System.IO;
using Kaedei.AcDown.Interface.Forms;
using System.Net;
using Kaedei.AcDown.Interface.Downloader;

namespace Kaedei.AcDown.Downloader
{
	/// <summary>
	/// Tucao下载器
	/// </summary>
	public class TucaoDownloader : CommonDownloader
	{
		//下载视频
		public override bool Download()
		{
			//开始下载
			delegates.TipText(new ParaTipText(this.Info, "正在分析视频地址"));

			string url = Info.Url;
			//视频地址数组
			string[] videos = null;

			//取得网页源文件
			string src = Network.GetHtmlSource(url, Encoding.UTF8, Info.Proxy);

			//视频id
			string id = "";
			//type值
			string type = "";
			//子标题
			string subtitle = "";
			//player id
			string playerId = "";
			//选择的视频（下拉列表）
			int selectedvideo = 0;

			//清空地址
			Info.FilePath.Clear();

			//取得视频标题
			Regex rTitle = new Regex(@"<title>(?<title>.*)</title>");
			Match mTitle = rTitle.Match(src);

			//取得PlayerID值
			Regex rPlayerid = new Regex(@"playerID=(?<playerid>[0-9-]+)");
			Match mPlayerid = rPlayerid.Match(src);
			playerId = mPlayerid.Groups["playerid"].Value;

			////分析id和视频存放站点(type)
			////取得"mplayer块的源代码
			//Regex rEmbed = new Regex(@"<div id=""mplayer"">.+?</embed>", RegexOptions.Singleline);
			//Match mEmbed = rEmbed.Match(src);
			//string embedSrc = mEmbed.Value;

			////取得id值
			//Regex rId = new Regex(@"\w+id=(?<id>\w+)");
			//MatchCollection mIds = rId.Matches(embedSrc);

			////取得type值
			//Regex rType = new Regex(@"type=(?<type>\w+)");
			//MatchCollection mTypes = rType.Matches(embedSrc);

			//取得所有子标题
			Regex rSubTitle = new Regex(@"type=(?<type>\w+)&vid=(?<vid>\d+)\|(?<subtitle>.*?)(\*\*|</li>)");
			MatchCollection mSubTitles = rSubTitle.Matches(src);

			if (mSubTitles.Count > 1) //如果数量大于一个
			{
				//定义字典
				var dict = new Dictionary<string, string>();
				for (int i = 0; i < mSubTitles.Count; i++)
				{
					dict.Add(i.ToString(), (i + 1).ToString() + "、" + mSubTitles[i].Groups["subtitle"].Value);
				}
				//用户选择下载哪一个视频
				selectedvideo = int.Parse(ToolForm.CreateSingleSelectForm("请选择视频：", dict, "0", Info.AutoAnswer, "tucao"));
				id = mSubTitles[selectedvideo].Groups["vid"].Value;
				type = mSubTitles[selectedvideo].Groups["type"].Value;
				subtitle = mSubTitles[selectedvideo].Groups["subtitle"].Value;
			}
			else
			{
				id = mSubTitles[0].Groups["vid"].Value;
				type = mSubTitles[0].Groups["type"].Value;
			}

			//设置标题
			string title = mTitle.Groups["title"].Value.Replace("- 吐槽 - tucao.cc", "");
			if (!string.IsNullOrEmpty(subtitle)) //如果存在子标题（视频为合集）
			{
				//更改标题
				title = title + " - " + subtitle;
				//更改URL防止hash时出错
				Info.Url = Info.Url + "#" + subtitle;

			}
			//过滤非法字符
			Info.Title = title;
			title = Tools.InvalidCharacterFilter(title, "");

			//如果允许下载视频
			if ((Info.DownloadTypes & DownloadType.Video) != 0)
			{
				//检查外链
				switch (type)
				{
					case "qq": //QQ视频
						//解析视频
						QQVideoParser parserQQ = new QQVideoParser();
						videos = parserQQ.Parse(new ParseRequest() { Id = id, Proxy = Info.Proxy, AutoAnswers = Info.AutoAnswer }).ToArray();
						break;
					case "youku": //优酷视频
						//解析视频
						YoukuParser parserYouKu = new YoukuParser();
						videos = parserYouKu.Parse(new ParseRequest() { Id = id, Proxy = Info.Proxy, AutoAnswers = Info.AutoAnswer }).ToArray();
						break;
					case "tudou": //土豆视频
						//解析视频
						TudouParser parserTudou = new TudouParser();
						videos = parserTudou.Parse(new ParseRequest() { Id = id, Proxy = Info.Proxy, AutoAnswers = Info.AutoAnswer }).ToArray();
						break;
					case "sina": //新浪视频
						SinaVideoParser parserSina = new SinaVideoParser();
						videos = parserSina.Parse(new ParseRequest() { Id = id, Proxy = Info.Proxy, AutoAnswers = Info.AutoAnswer }).ToArray();
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
			if (((Info.DownloadTypes & DownloadType.Subtitle) != 0) && !string.IsNullOrEmpty(playerId))
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
