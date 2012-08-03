using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;

namespace Kaedei.AcDown.Downloader
{
	[AcDownPluginInformation("NicoDownloader", "Nicovideo.jp下载插件", "Kaedei", "3.12.0.701", "NicoNico下载插件", "http://blog.sina.com.cn/kaedei")]
	public class NicoPlugin : IPlugin
	{
		public NicoPlugin()
		{
			Feature = new Dictionary<string, object>();
			//ExampleUrl
			Feature.Add("ExampleUrl", new string[] { 
				"NicoNico下载插件:",
				"支持简写形式",
				"sm18194913",
				"http://www.nicovideo.jp/watch/sm18194913"
			});
			//AutoAnswer(不支持)
			//ConfigurationForm(不支持)
		}

		public IDownloader CreateDownloader()
		{
			return new NicoDownloader();
		}

		public bool CheckUrl(string url)
		{
			if (Regex.IsMatch(url, @"(?<=(http://www\.nicovideo\.jp/watch/|^))sm\d+"))
				return true;
			else
				return false;
		}

		/// <summary>
		/// 规则为 sm+数字。例如sm1234567
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public string GetHash(string url)
		{
			Match m = Regex.Match(url, @"(?<=(http://www\.nicovideo\.jp/watch/|))sm\d+");
			if (m.Success)
				return m.Value;
			else
				return null;
		}

		public Dictionary<string, object> Feature { get; private set; }
		public SerializableDictionary<string, string> Configuration { get; set; }


	}
}
