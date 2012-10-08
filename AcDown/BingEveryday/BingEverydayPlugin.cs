using Kaedei.AcDown.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaedei.BingEveryday
{
	[AcDownPluginInformation("BingEveryday", "Bing壁纸下载&更换插件", "Kaedei", "1.0.0.0", "每天换个好心情", "http://blog.sina.com.cn/kaedei")]
	public class BingEverydayPlugin : IPlugin
	{

		#region IPlugin 成员

		public IDownloader CreateDownloader()
		{
			return new BingEverydayDownloader();
		}

		public bool CheckUrl(string url)
		{
			if (url.Equals("bing", StringComparison.CurrentCultureIgnoreCase))
				return true;
			else
				return false;
		}

		/// <summary>
		/// Hash规则: bing+年月日
		/// 如: bing20120123
		/// </summary>
		public string GetHash(string url)
		{
			return "bing" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("D2")
				+ DateTime.Now.Day.ToString("D2");
		}

		public Dictionary<string, object> Feature { get; private set; }

		public SerializableDictionary<string, string> Configuration { get; set; }

		#endregion
	}
}
