using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;

namespace Kaedei.AcDown.Downloader
{

	[AcDownPluginInformation("YouTubeDownloader", "YouTube下载插件", "Kaedei", "3.11.5.425", "YouTube下载插件", "http://blog.sina.com.cn/kaedei")]
	public class YouTubePlugin : IPlugin
	{
		public YouTubePlugin()
		{
			Feature = new Dictionary<string, object>();
			//GetExample
			Feature.Add("ExampleUrl", new string[] { 
				"YouTube.com下载插件:",
				"http://www.youtube.com/watch?v=HbVBdU88Sbw"
			});
			//AutoAnswer(不支持)
			//ConfigurationForm(不支持)
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

		public Dictionary<string, object> Feature { get; private set; }

		public SerializableDictionary<string, string> Configuration { get; set; }

	}
}
