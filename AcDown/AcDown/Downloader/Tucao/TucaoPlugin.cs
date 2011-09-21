using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Kaedei.AcDown.Interface;

namespace Kaedei.AcDown.Downloader
{

	public class TucaoPlugin : IAcdownPluginInfo
	{

		public string Name
		{
			get { return @"TucaoDownloader"; }
		}

		public string Author
		{
			get { return "Kaedei Software"; }
		}

		public Version Version
		{
			get { return new Version(1, 0, 0, 0); }
		}

		public string Describe
		{
			get { return @"吐槽弹幕网下载插件"; }
		}

		public string SupportUrl
		{
			get { return @"http://blog.sina.com.cn/kaedei"; }
		}

		public IDownloader CreateDownloader()
		{
			return new TucaoDownloader(this);
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

		public string[] GetUrlExample()
		{
			return new string[] { 
				"Tucao下载插件:",
				"支持识别各Part名称",
				"http://www.Tucao.tv/video/av97834/",
				"http://www.Tucao.tv/video/av70229/index_20.html",
			};
		}

	} //end class
}
