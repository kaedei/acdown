using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using Kaedei.AcDown.Interface.Forms;
using System.Net;

namespace Kaedei.AcDown.Downloader
{
	/// <summary>
	/// 爱漫画下载器
	/// </summary>
	public class SfAcgComicDownloader : IDownloader
	{

		public TaskInfo Info { get; set; }

		//下载参数
		DownloadParameter currentParameter = new DownloadParameter();

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

		//下载漫画
		public bool Download()
		{
			//开始下载
			delegates.TipText(new ParaTipText(this.Info, "正在分析漫画地址"));

			try
			{
				//取得Url源文件
				string src = Network.GetHtmlSource(Info.Url, Encoding.UTF8, Info.Proxy);

				//要下载的Url列表（页面）
				List<string> subUrls = new List<string>();

				//漫画id
				int comicId;

				//是否为漫画的介绍页面
				bool isIntroPage = !Regex.IsMatch(Info.Url, @"/HTML/\w+/\w+", RegexOptions.IgnoreCase);

				#region 确定是整部漫画还是单独一话

				//如果为整部漫画
				if (isIntroPage)
				{
					//取得所有漫画的列表
					Regex rAllComics = new Regex(@"<li><a href=""(?<page>(http://\w+\.sfacg\.com/AllComic/)?.+?)"" target=""_blank"">(?<title>.+?)</a>");
					MatchCollection mcAllComics = rAllComics.Matches(src);

					//新建（url-标题）字典
					var dict = new Dictionary<string, string>();

					foreach (Match item in mcAllComics)
					{
						string comicPage = item.Groups["page"].Value;
						if (comicPage.StartsWith("/"))
							comicPage = "http://comic.sfacg.com" + comicPage;
						string comicTitle = item.Groups["title"].Value;
						if (Regex.IsMatch(comicTitle, @"(?<=<(\w+) .*?>).+?(?=</\1>)"))
							comicTitle = Regex.Match(comicTitle, @"(?<=<(\w+) .*?>).+?(?=</\1>)").Value;
						dict.Add(comicPage, comicTitle);
					}

					//选择下载哪部漫画
					var selected = ToolForm.CreateMultiSelectForm(dict, Info.AutoAnswer, "sfacgcomic");

					//将地址填充到下载列表中
					subUrls.AddRange(selected);

					//如果用户没有选择任何章节
					if (subUrls.Count == 0)
					{
						return false;
					}

					//取得漫画标题
					string title = Regex.Match(src, @"(?<=<title>).+?(?=,)").Value;
					//过滤标题中的非法字符
					title = Tools.InvalidCharacterFilter(title, "");
					Info.Title = title;
					//获取漫画ID
					comicId = int.Parse(Regex.Match(src, @"(?<=var comicCounterID = )\d+", RegexOptions.IgnoreCase).Value);
				}
				else //如果不是整部漫画则添加此单话url
				{
					subUrls.Add(Info.Url);
					//取得漫画标题
					//取得源代码并分析
					string pSrc = Network.GetHtmlSource(Info.Url, Encoding.UTF8, Info.Proxy);
					//取得漫画标题
					Regex rTitle = new Regex(@"> > <a href=""http://comic.sfacg.com/HTML/.+>(?<title>.+?)</a>");
					Match mTitle = rTitle.Match(pSrc);
					string title = mTitle.Groups["title"].Value;
					//过滤标题中的非法字符
					title = Tools.InvalidCharacterFilter(title, "");
					Info.Title = title;
					//获取漫画ID
					comicId = int.Parse(Regex.Match(pSrc, @"(?<=var c = )\d+", RegexOptions.IgnoreCase).Value);
				} //end if

				#endregion

				#region 下载漫画

				//建立文件夹
				string mainDir = Info.SaveDirectory + (Info.SaveDirectory.ToString().EndsWith(@"\") ? "" : @"\") + Info.Title;
				//确定漫画共有几个段落
				Info.PartCount = subUrls.Count;

				//分段落下载
				for (int i = 0; i < Info.PartCount; i++)
				{

					Info.CurrentPart = i + 1;
					//提示更换新Part
					delegates.NewPart(new ParaNewPart(this.Info, i + 1));

					//地址数组
					Dictionary<string, string> files = new Dictionary<string, string>();

					//分析源代码,取得下载地址
					WebClient wc = new WebClient();
					if (Info.Proxy != null)
						wc.Proxy = Info.Proxy;

					//取得源代码
					byte[] buff = wc.DownloadData(subUrls[i]);
					string cookie = wc.ResponseHeaders.Get("Set-Cookie");
					string source = Encoding.UTF8.GetString(buff);
					//取得标题
					Regex rTitle = new Regex(@"> > (?<title>\S+?)</div>");
					Match mTitle = rTitle.Match(source);
					string subTitle = mTitle.Groups["title"].Value;
					//过滤子标题中的非法字符
					subTitle = Tools.InvalidCharacterFilter(subTitle, "");
					//合并本地路径(文件夹)
					string subDir = mainDir + @"\" + subTitle;
					//创建文件夹
					Directory.CreateDirectory(subDir);


					#region 取得js文件


					//取得js文件路径
					string urljs = subUrls[i];
					//分析漫画id
					Regex rjs = new Regex(@"(?<server>http://\w+\.sfacg\.com/)HTML/\w+/(?<id>.+)");
					Match mjs = rjs.Match(urljs);
					string jsserver = mjs.Groups["server"].Value;
					string jsid = mjs.Groups["id"].Value.TrimEnd('/');
					urljs = jsserver + "Utility/" + comicId + "/" + jsid + ".js";

					#endregion


					//取得JS文件内容
					string jscontent = Network.GetHtmlSource(urljs, Encoding.UTF8);

					//添加到url数组
					Regex rFiles = new Regex(@"picAy\[(?<no>\d+)\] = ""(?<file>.+?(?<ext>\w{3}))""");
					MatchCollection mFiles = rFiles.Matches(jscontent);
					//添加url到数组
					foreach (Match item in mFiles)
					{
						//将 url - 本地文件 添加到字典中
						files.Add("http://comic.sfacg.com/" + item.Groups["file"].Value,
							Path.Combine(subDir, item.Groups["no"].Value + "." + item.Groups["ext"].Value));
					}

					//设置下载长度
					currentParameter.TotalLength = files.Count;
					currentParameter.DoneBytes = 0;

					//下载文件
					foreach (string key in files.Keys)
					{
						if (currentParameter.IsStop)
						{
							return false;
						}
						try
						{
							wc.Headers.Add("Referer", subUrls[i]);
							wc.Headers.Add("Cookie", cookie);
							byte[] content = wc.DownloadData(key);
							File.WriteAllBytes(files[key], content);
						}
						catch{ } //end try
						currentParameter.DoneBytes += 1;
					} // end foreach

				}//end for
				#endregion

			}//end try
			catch (Exception ex) //出现错误即下载失败
			{
				throw ex;
			}//end try


			//下载成功完成
			currentParameter.DoneBytes = currentParameter.TotalLength;
			return true;

		}//end DownloadVideo


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
