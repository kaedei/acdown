using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;

namespace Kaedei.AcDown.Downloader
{
	/// <summary>
	/// SFACG漫画下载插件
	/// </summary>
	public class SfAcgPlugin : IAcdownPluginInfo
	{
		#region IAcdownPluginInfo 成员

		public string Name
		{
			get { return "SfAcgDownloader"; }
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
			get { return "SF互动传媒网下载插件"; }
		}

		public string SupportUrl
		{
			get { return @"http://blog.sina.com.cn/kaedei"; }
		}

		public IDownloader CreateDownloader()
		{
			return new SfAcgComicDownloader();
		}

		public bool CheckUrl(string url)
		{
			Regex r = new Regex(@"(^http://comic\.sfacg\.com/HTML/(?<t>.+?)/|^http://\w+\.sfacg\.com/AllComic/(?<t>.+))");
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
		/// 规则为 SfAcg + 漫画名 或 漫画ID
		/// 如 "SfAcgshiszqdzqy"或"SfAcg319/397/"
		/// </summary>
		public string GetHash(string url)
		{
			Regex r = new Regex(@"(^http://comic\.sfacg\.com/HTML/(?<t>.+?)/|^http://\w+\.sfacg\.com/AllComic/(?<t>.+))");
			Match m = r.Match(url);
			if (m.Success)
			{

				return "SfAcg" + m.Groups["t"].Value;
			}
			return null;
		}



		public string[] GetUrlExample()
		{
			return new string[] { 
				"SF互动传媒网(SfAcg.com)漫画下载插件:",
				"http://comic.sfacg.com/HTML/WXSN/",
				"http://coldpic.sfacg.com/AllComic/495/001/",
			};
		}
		#endregion
	}
}
