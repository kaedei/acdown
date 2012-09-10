using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using Kaedei.AcDown.Interface.Downloader;
using System.Text.RegularExpressions;
using System.IO;

namespace Kaedei.AcDown.Downloader
{
	[AcDownPluginInformation("XiamiDownloader", "虾米音乐下载插件", "orzFly", "1.1.0.0", "虾米音乐", "http://orzfly.com/")]
	public class XiamiPlugin : IPlugin
	{
		public static string RegexPatternSong = @"http://www.xiami.com/song/(?<var>\d+)([^0-9].*)?";
		public static string RegexPatternArtist = @"http://www.xiami.com/artist/(?<var>\d+)([^0-9].*)?";
		public static string RegexPatternAlbum = @"http://www.xiami.com/album/(?<var>\d+)([^0-9].*)?";
		public static string RegexPatternCollect = @"http://www.xiami.com/song/showcollect/id/(?<var>\d+)([^0-9].*)?";
		
		private static string[] patterns = new string[] { RegexPatternSong, RegexPatternArtist, RegexPatternAlbum, RegexPatternCollect };
		
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
			Match match;
			foreach (string pattern in patterns) {
				match = Regex.Match(url, pattern);
				if (match.Groups["var"].Success)
					return true;
			}
			return false;
		}

		public string GetHash(string url)
		{
			return "xiami" + url;
		}

		private string[] GetUrlExample()
		{
			List<string> t = new List<string>() { 
				"虾米音乐:",
				"http://www.xiami.com/song/1770138847",
				"http://www.xiami.com/artist/86505",
				"http://www.xiami.com/album/434802",
				"http://www.xiami.com/song/showcollect/id/13008248",
			};
			return t.ToArray();
		}


		public Dictionary<string, object> Feature { get; private set; }

		public SerializableDictionary<string, string> Configuration { get; set; }
	}
	
}
