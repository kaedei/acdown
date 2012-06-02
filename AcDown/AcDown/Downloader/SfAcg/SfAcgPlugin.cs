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
	[AcDownPluginInformation("SfAcgDownloader","SF互动传媒网下载插件","Kaedei","3.10.0.0","SF互动传媒网下载插件","http://blog.sina.com.cn/kaedei")]
	public class SfAcgPlugin : IPlugin
	{
		public SfAcgPlugin()
		{
			Feature = new Dictionary<string,object>();
			Feature.Add("ExampleUrl", new string[] { 
				"SF互动传媒网(SfAcg.com)漫画下载插件:",
				"http://comic.sfacg.com/HTML/WXSN/",
				"http://coldpic.sfacg.com/AllComic/495/001/",
			});
			//AutoAnswer(不支持)
			//ConfigurationForm(不支持)
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


		public Dictionary<string, object> Feature { get; private set; }

		public SerializableDictionary<string, string> Configuration { get; set; }
	}
}
