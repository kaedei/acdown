using System;
using System.Collections.Generic;
using System.Text;

namespace Kaedei.AcDown.Downloader.AcFun
{
	public class File
	{
		public string type { get; set; }
		public int seconds { get; set; }
		public int bytes { get; set; }
		public string url { get; set; }
		public int no { get; set; }
	}

	public class C
	{
		public string quality { get; set; }
		public int totalseconds { get; set; }
		public int totalbytes { get; set; }
		public List<File> files { get; set; }
	}
	
	public class Result
	{
		public C C00 { get; set; }
		public C C10 { get; set; }
		public C C20 { get; set; }
		public C C80 { get; set; }
		public C C40 { get; set; }
		public C C30 { get; set; }
		public C C50 { get; set; }
		public C C60 { get; set; }
		public C C70 { get; set; }
		public C C90 { get; set; }
	}

	public class AcInfo
	{
		public int code { get; set; }
		public string message { get; set; }
		public bool success { get; set; }
		public Result result { get; set; }
	}
}
