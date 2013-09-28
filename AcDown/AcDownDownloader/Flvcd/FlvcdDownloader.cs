using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.IO;
using System.Text.RegularExpressions;
using Kaedei.AcDown.Interface.Forms;
using Kaedei.AcDown.Interface.Downloader;

namespace Kaedei.AcDown.Downloader
{
	public class FlvcdDownloader : CommonDownloader
	{

		//下载视频
		public override bool Download()
		{
			//开始下载
			TipText("正在分析视频地址");

			//原始Url
			Info.Url = Info.Url.TrimStart('+');
			//修正url
			string url = "http://www.flvcd.com/parse.php?kw=" + Tools.UrlEncode(Info.Url);

			try
			{
				//取得网页源文件
				string src = Network.GetHtmlSource(url, Encoding.GetEncoding("GB2312"), Info.Proxy);

				//检查是否需要密码
				if (src.Contains("请输入密码"))
				{
					TipText("等待输入密码");
					string pw = ToolForm.CreatePasswordForm(true, "", "");
					url = url + "&passwd=" + pw;
					src = Network.GetHtmlSource(url, Encoding.GetEncoding("GB2312"), Info.Proxy);
				}

				//获得所有清晰度
				//获取需要的源代码部分
				TipText("解析所有清晰度");
				Regex rMulti = new Regex(@"用硕鼠下载.*?赞助商链接", RegexOptions.Singleline);
				Match mMulti = rMulti.Match(src);

				string allResSrc = mMulti.Value;
				//获取url和名称
				Regex rGetAllRes = new Regex(@"<a href=""(?<url>.+?)"".+?<B>(?<mode>.+?)</B>");
				MatchCollection mGetAllRes = rGetAllRes.Matches(allResSrc);
				if (mGetAllRes.Count >= 1)
				{
					//将url和名称填入字典中
					var dict = new Dictionary<string, string>();
					dict.Add(url, "默认清晰度");
					foreach (Match item in mGetAllRes)
					{
						dict.Add("http://www.flvcd.com/" + item.Groups["url"].Value, item.Groups["mode"].Value);
					}
					//用户选择清晰度
					url = ToolForm.CreateSingleSelectForm("在线解析引擎可以解析此视频的多种清晰度模式，\n请选择您需要的视频清晰度：", dict, url, Info.AutoAnswer, "flvcd");
					//重新获取网页源文件
					src = Network.GetHtmlSource(url, Encoding.GetEncoding("GB2312"), Info.Proxy);

				}


				//取得视频标题
				TipText("正在解析视频");
				Regex rTitle = new Regex(@"<input type=$hidden$ name=$name$ value=$(?<title>.+?)$>".Replace("$", "\""));
				Match mTitle = rTitle.Match(src);
				string title = mTitle.Groups["title"].Value;

				Info.Title = title;
				//过滤非法字符
				title = Tools.InvalidCharacterFilter(title, "");

				//取得内容
				var urlMatch = Regex.Match(src, @"<input type=#hidden# name=#inf# value=#(?<urls>.+?)#".Replace('#', '"'));
				if (!urlMatch.Success)
				{
					throw new Exception("FLVCD插件暂时不支持此URL的解析" + Environment.NewLine + Info.Url);
				}
				string content = urlMatch.Groups["urls"].Value;
				List<string> partUrls = new List<string>(content.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries));

				//重新设置保存目录（生成子文件夹）
				if (!Info.SaveDirectory.ToString().EndsWith(title))
				{
					string newdir = Path.Combine(Info.SaveDirectory.ToString(), title);
					if (!Directory.Exists(newdir)) Directory.CreateDirectory(newdir);
					Info.SaveDirectory = new DirectoryInfo(newdir);
				}

				//清空地址
				Info.FilePath.Clear();

				//下载视频
				//确定视频共有几个段落
				Info.PartCount = partUrls.Count;

				//------------分段落下载------------
				for (int i = 0; i < Info.PartCount; i++)
				{
					Info.CurrentPart = i + 1;

					//取得文件后缀名
					string ext = Tools.GetExtension(partUrls[i]);
					if (string.IsNullOrEmpty(ext))
					{
						if (string.IsNullOrEmpty(Path.GetExtension(partUrls[i])))
							ext = ".flv";
						else
							ext = Path.GetExtension(partUrls[i]);
					}

					//设置当前DownloadParameter
					currentParameter = new DownloadParameter()
					{
						//文件名
						FilePath = Path.Combine(Info.SaveDirectory.ToString(),
							title + "-" + string.Format("{0:00}", i) + ext),
						//文件URL
						Url = partUrls[i],
						//代理服务器
						Proxy = Info.Proxy
					};

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
							throw;
						}
						else //否则继续下载，设置“部分失败”状态
						{
							Info.PartialFinished = true;
							Info.PartialFinishedDetail += "\r\n文件: " + currentParameter.Url + " 下载失败";
						}
					}
				} //end for
			}
			catch (Exception ex)
			{
				throw;
			}// end try

			//下载成功完成
			return true;
		}
	}
}
