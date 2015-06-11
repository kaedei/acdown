using System.Collections.Generic;

namespace Kaedei.AcDown.Downloader.Tucao
{
	public class Video
	{
		public string type { get; set; }
		public string vid { get; set; }
		public string title { get; set; }
	}

	public class Result
	{
		public string hid { get; set; }
		public string typeid { get; set; }
		public string create { get; set; }
		public string mukio { get; set; }
		public string typename { get; set; }
		public string title { get; set; }
		public string play { get; set; }
		public string description { get; set; }
		public string keywords { get; set; }
		public string thumb { get; set; }
		public string user { get; set; }
		public string userid { get; set; }
		public string part { get; set; }
		public List<Video> video { get; set; }
	}

	public class TucaoVideoInfo
	{
		public string code { get; set; }
		public Result result { get; set; }
	}
}