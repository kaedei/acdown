using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;

namespace Kaedei.AcDown.Downloader
{
	public class YouTubePlugin : IAcdownPluginInfo
	{

		#region IAcdownPluginInfo 成员

		public string Name
		{
			get { return "YouTubeDownloader"; }
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
			get { return "YouTube下载插件"; }
		}

		public string SupportUrl
		{
			get { return @"http://blog.sina.com.cn/kaedei"; }
		}

		public IDownloader CreateDownloader()
		{
			return new YouTubeDownloader();
		}

		public bool CheckUrl(string url)
		{
			Regex r = new Regex(@"^http://www\.youtube\.com/watch\?v=(?<id>[\w\-]+)");
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
		/// 规则为 YouTube + 视频ID
		/// 如 "YouTubeHbVBdU88Sbw"
		/// </summary>
		public string GetHash(string url)
		{
			Regex r = new Regex(@"http://www\.youtube\.com/watch\?v=(?<id>[\w\-]+)");
			Match m = r.Match(url);
			if (m.Success)
			{
				if (!string.IsNullOrEmpty(m.Groups["id"].ToString()))
					return "YouTube" + m.Groups["id"].ToString();
			}
			return null;

		}


		public string[] GetUrlExample()
		{
			return new string[] { 
				"YouTube.com下载插件:",
				"http://www.youtube.com/watch?v=HbVBdU88Sbw"
			};
		}

		#endregion
	}
}
