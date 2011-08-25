using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;

namespace Kaedei.AcDown.Downloader
{
	/// <summary>
	/// FLVCD解析插件
	/// </summary>
	public class FlvcdPlugin : IAcdownPluginInfo
	{
		public string Name
		{
			get { return @"FlvcdDownloader"; }
		}

		public string Author
		{
			get { return "Kaedei Software"; }
		}

		public Version Version
		{
			get { return new Version(1, 1, 0, 0); }
		}

		public string Describe
		{
			get { return @"Flvcd解析插件"; }
		}

		public string SupportUrl
		{
			get { return @"http://blog.sina.com.cn/kaedei"; }
		}

		public IDownloader CreateDownloader()
		{
			return new FlvcdDownloader(this);
		}

		public bool CheckUrl(string url)
		{
			if (url.StartsWith("+http", StringComparison.CurrentCultureIgnoreCase))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 规则为 flvcd + url地址
		/// 如 "flvcdhttp://v.youku.com/v_show/id_XMjE1MTYyNzAw.html"
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public string GetHash(string url)
		{
			Regex r = new Regex(@"^\+(?<url>http.+)");
			Match m = r.Match(url);
			if (m.Success)
			{
				return "flvcd" + m.Groups["url"].Value;
			}
			else
			{
				return null;
			}
		}

		public string[] GetUrlExample()
		{
			return new string[] { 
				"FlvCD解析插件:",
				"任意地址前加 + ",
				"如:   +http://v.youku.com/v_show/id_XMjE1MTYyNzAw.html "
			};
		}
	}
}
