using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;

namespace Kaedei.AcDown.Downloader
{
	/// <summary>
	/// 自定义文件名辅助类
	/// </summary>
	public class CustomFileNameHelper
	{
		public static string TITLE = "标题";
		public static string SUBTITLE = "子标题";
		public static string PART = "(分段)";
		public static string EXT = "扩展名";
		public static string NUM = "编号";
		public static string SUBNUM = "子编号";
		public static string AUTHOR = "作者";
		public static string URL = "网址";
		public static string DATE = "日期";
		public static string TIME = "时间";

		/// <summary>
		/// 生成文件名称
		/// </summary>
		public string CombineFileName(string input, string title = "", string subtitle = "",
			string part = "", string ext = "", string num = "", string subnum = "",
			string author = "", string url = "", string date = "", string time = "")
		{
			input = input.Replace(SUBTITLE, subtitle).Replace(TITLE, title)
				.Replace(PART, string.IsNullOrEmpty(part) ? "" : "(" + part + ")") //分段
				.Replace(EXT, ext)
				.Replace(SUBNUM, subnum).Replace(NUM, num)
				.Replace(AUTHOR, author)
				.Replace(URL, url)
				.Replace(DATE, date)
				.Replace(TIME, time)
				.Trim();
			return input;
		}

	}
}
