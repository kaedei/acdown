using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;
using System.Net;

namespace Kaedei.AcDown.Parser
{
	public class QQVideoParser:IParser
	{
		/// <summary>
		/// 解析QQ视频源地址
		/// </summary>
		public ParseResult Parse(ParseRequest request)
		{
			ParseResult pr = new ParseResult();
			//完整url
			string url = @"http://vv.video.qq.com/geturl?ran=0.16436194255948067&otype=xml&vid=%ID%&platform=1&format=2".Replace(@"%ID%", request.Id);
			string xmldoc = Network.GetHtmlSource(url, Encoding.UTF8, request.Proxy);
			Regex r = new Regex(@"\<url\>(?<url>.*?)\</url\>");
			MatchCollection mc = r.Matches(xmldoc);
			if (mc.Count != 0)
			{
				foreach (Match item in mc)
				{
					pr.Items.Add(new ParseResultItem() { RealAddress = new Uri(item.Groups[@"url"].Value) });
				}
				return pr;
			}
			else
			{
				pr.Items.Add(new ParseResultItem() { RealAddress = new Uri(@"http://web.qqvideo.tc.qq.com/" + request.Id + ".flv") });
				return pr;
			}
			
		}
	}
}
