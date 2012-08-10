using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using Kaedei.AcDown.Interface;

namespace Kaedei.AcDown.UI
{
	/// <summary>
	/// 短网址解析器
	/// </summary>
	public class ShortUrlParser
	{
		/// <summary>
		/// 获取指定的网址是否是短网址
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static bool CanParse(string url)
		{
			if (url.StartsWith("http://t.cn/") || url.StartsWith("http://url.cn/") ||
				url.StartsWith("http://goo.gl/") || url.StartsWith("http://is.gd/") ||
				url.StartsWith("http://126.fm/") || url.StartsWith("http://tinyurl.com/") ||
				url.StartsWith("http://dwz.cn/"))
				return true;
			return false;
		}

		/// <summary>
		/// 解析短网址
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static string Parse(string url)
		{
			try
			{
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
				req.Timeout = GlobalSettings.GetSettings().NetworkTimeout;
				req.AllowAutoRedirect = false;
				var res = req.GetResponse();
				string location = res.Headers["Location"];
				return location;
			}
			catch
			{
				return "";
			}
		}


	}
}
