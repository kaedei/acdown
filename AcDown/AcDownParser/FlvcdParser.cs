using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;

namespace Kaedei.AcDown.Interface
{
	public class FlvcdParser : IParser
	{

		/// <summary>
		/// 解析任意视频源地址
		/// </summary>
		public ParseResult Parse(ParseRequest request)
		{
			//返回值
			ParseResult pr = new ParseResult();
			//分辨率
			string resolution = request.SpecificConfiguration.ContainsKey("resolution") ? request.SpecificConfiguration["resolution"] : "";
			//修正url
			string url = "http://www.flvcd.com/parse.php?kw=" + Tools.UrlEncode(request.Id) + resolution;


			//取得网页源文件
			string src = Network.GetHtmlSource(url, Encoding.GetEncoding("GB2312"), request.Proxy);

			//检查是否需要密码
			if (src.Contains("请输入密码"))
			{
				url = url + "&passwd=" + request.Password;
				src = Network.GetHtmlSource(url, Encoding.GetEncoding("GB2312"), request.Proxy);
			}
			


			var urlMatch = Regex.Match(src, @"<input type=#hidden# name=#inf# value=#(?<urls>.+?)#".Replace('#', '"'));
			string content = urlMatch.Groups["urls"].Value;
			List<string> partUrls = new List<string>(content.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries));

			foreach (string partUrl in partUrls)
			{
				pr.Items.Add(new ParseResultItem() { RealAddress = partUrl });
			}
			return pr;
		}
	}
}
