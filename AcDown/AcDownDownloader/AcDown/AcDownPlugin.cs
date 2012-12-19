using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;

namespace Kaedei.AcDown.Downloader.AcDown
{
	[AcDownPluginInformation("AcDownPlugin", "AcDown UI组件下载插件", "Kaedei", "4.4.0.1204", "AcDown UI插件(视频合并等)下载插件", "http://blog.sina.com.cn/kaedei")]
	public class AcDownPlugin : IPlugin
	{
		private string[] supportedUrl = { "视频合并插件" };
		public IDownloader CreateDownloader()
		{
			return new AcDownDownloader();
		}

		public bool CheckUrl(string url)
		{
			foreach (string item in supportedUrl)
			{
				if (item.Equals(url, StringComparison.CurrentCultureIgnoreCase))
					return true;
			}
			return false;
		}

		public string GetHash(string url)
		{
			return url;
		}

		public Dictionary<string, object> Feature { get; set; }

		public SerializableDictionary<string, string> Configuration { get; set; }

	}
}
