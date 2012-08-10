using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Kaedei.AcDown.Interface;

namespace Kaedei.AcDown.Downloader
{
	[AcDownPluginInformation("TucaoDownloader","吐槽网下载插件","Kaedei","4.0.0.0","吐槽网下载插件","http://blog.sina.com.cn/kaedei")]
	public class TucaoPlugin : IPlugin
	{
		public TucaoPlugin()
		{
			Feature = new Dictionary<string, object>();
			//GetExample
			Feature.Add("ExampleUrl", new string[] { 
				"Tucao下载插件:",
				"支持识别各Part名称",
				"http://www.tucao.cc/play/114530/",
				"http://www.tucao.cc/play/811894/",
			});
			//AutoAnswer
            Feature.Add("AutoAnswer", new List<AutoAnswer>()
			{
                new AutoAnswer("tudou","3","土豆 高清(720P)"),
                new AutoAnswer("youku","mp4","优酷 高清(Mp4)"),
				new AutoAnswer("tudou","99","土豆 原画"),
                new AutoAnswer("youku","hd2","优酷 超清(HD)"),
				new AutoAnswer("youku","flv","优酷 标清(Flv)"),
                new AutoAnswer("tudou","2","土豆 清晰(360P)"),
                new AutoAnswer("tudou","1","土豆 流畅(256P)")
			});
			//ConfigurationForm(不支持)
		}

		public IDownloader CreateDownloader()
		{
			return new TucaoDownloader();
		}

		public bool CheckUrl(string url)
		{
			Regex r = new Regex(@"^http://www\.tucao\.cc/play/.+");
			if (r.Match(url).Success)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 规则为 tucao + 视频ID
		/// 如 "tucao99999"
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public string GetHash(string url)
		{
			Regex r = new Regex(@"http://www\.tucao\.cc/play/(?<id>.+)");
			Match m = r.Match(url);
			if (m.Success)
			{
				return "tucao" + m.Groups["id"].Value;
			}
			else
			{
				return null;
			}
		}

		public Dictionary<string, object> Feature { get; private set; }

		public SerializableDictionary<string, string> Configuration { get; set; }

	} //end class
}
