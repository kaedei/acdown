using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;

namespace Kaedei.AcDown.Downloader
{

	public class BilibiliPlugin : IAcdownPluginInfo
	{

		public string Name
		{
			get { return @"BilibiliDownloader"; }
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
			get { return @"Bilibili.us下载插件"; }
		}

		public string SupportUrl
		{
			get { return @"http://blog.sina.com.cn/kaedei"; }
		}

		public IDownloader CreateDownloader()
		{
			return null; // new Bilibili(this);
		}

		public bool CheckUrl(string url)
		{
			Regex r = new Regex(@"http://www\.bilibili\.us/video/av(?<av>\w+)");
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
		/// 规则为 bilibili + 视频ID
		/// 如 "bilibili99999"
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public string GetHash(string url)
		{
			Regex r = new Regex(@"http://www\.bilibili\.us/video/av(?<av>\w+)");
			Match m = r.Match(url);
			if (m.Success)
			{
				return "bilibili" + m.Groups["av"].Value;
			}
			else
			{
				return null;
			}
		}
	} //end class

	public class Bilibili //: IDownloader
	{

	}
}
