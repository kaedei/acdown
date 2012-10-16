using Kaedei.AcDown.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Kaedei.BingEveryday
{
	[AcDownPluginInformation("BingEveryday", "Bing壁纸下载&更换插件", "Kaedei", "1.2.0.0", "每天换个好心情", "http://blog.sina.com.cn/kaedei")]
	public class BingEverydayPlugin : IPlugin
	{

		public BingEverydayPlugin()
		{
			Feature = new Dictionary<string, object>();
			//GetExample
			Feature.Add("ExampleUrl", new string[] { 
				"Bing.com壁纸下载&更换插件:",
				@"在地址栏中直接填写""bing""即可"
			});
		}

		#region IPlugin 成员

		public IDownloader CreateDownloader()
		{
			return new BingEverydayDownloader();
		}

		/// <summary>
		/// 检查url是否符合规则。
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public bool CheckUrl(string url)
		{
			if (Regex.IsMatch(url.Trim(), @"^bing(\d{8}|)$", RegexOptions.IgnoreCase))
				return true;
			else
				return false;
		}

		/// <summary>
		/// Hash规则: bing+年月日。
		/// 如: bing20120123
		/// </summary>
		public string GetHash(string url)
		{
			if (url.Equals("bing", StringComparison.CurrentCultureIgnoreCase))
			{
				var now = DateTime.Now;
				return "bing" + now.Year.ToString() + now.Month.ToString("D2") + now.Day.ToString("D2");
			}
			else
				return url;
		}

		public Dictionary<string, object> Feature { get; private set; }

		public SerializableDictionary<string, string> Configuration { get; set; }

		#endregion
	}
}
