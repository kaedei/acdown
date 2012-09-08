using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using Kaedei.AcDown.Interface.Downloader;
using System.Text.RegularExpressions;
using System.IO;

namespace Kaedei.AcDown.Downloader
{
	[AcDownPluginInformation("XiamiDownloader", "虾米音乐下载插件", "orzFly", "1.0.0.0", "虾米音乐", "http://orzfly.com/")]
	public class XiamiPlugin : IPlugin
	{
		public XiamiPlugin()
		{
			Feature = new Dictionary<string, object>();
			Feature.Add("ExampleUrl", GetUrlExample());
		}

		public IDownloader CreateDownloader()
		{
			return new XiamiDownloader();
		}

		public bool CheckUrl(string url)
		{
			return url.StartsWith("http://www.xiami.com/song/");
		}

		public string GetHash(string url)
		{
			return "xiami" + url;
		}

		private string[] GetUrlExample()
		{
			List<string> t = new List<string>() { 
				"虾米音乐:",
				"http://www.xiami.com/song/1771292328"
			};
			return t.ToArray();
		}


		public Dictionary<string, object> Feature { get; private set; }

		public SerializableDictionary<string, string> Configuration { get; set; }
	}
	
}
