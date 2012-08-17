using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Xml.Serialization;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Kaedei.AcDown.Interface
{
	/// <summary>
	/// 新浪视频分析器
	/// </summary>
	public class SinaVideoParser : IParser
	{
		/// <summary>
		/// 解析新浪视频视频文件源地址
		/// </summary>
		public ParseResult Parse(ParseRequest request)
		{
			ParseResult pr = new ParseResult();
			//合并完整url
			string url = @"http://v.iask.com/v_play.php?vid=" + request.Id;
			string source = Network.GetHtmlSource(url, Encoding.UTF8, request.Proxy);
			//视频总长度
			string totallength = Regex.Match(source, @"<timelength>(?<timelength>\d+)</timelength>").Groups["timelength"].Value;
			//Framecount
			string framecount = Regex.Match(source, @"<framecount>(?<framecount>\d+)</framecount>").Groups["framecount"].Value;
			//src
			string src = Regex.Match(source, @"<src>(?<src>\d+)</src>").Groups["src"].Value;
			
			pr.SpecificResult.Add("totallength", totallength);
			pr.SpecificResult.Add("framecount", framecount);
			pr.SpecificResult.Add("src", src);

			//vstr
			string vstr = Regex.Match(source, @"(?<=<vstr><!\[CDATA\[)\w+").Value;

			//视频信息
			Regex r = new Regex(@"<durl>.+?<order>(?<order>\d+)</order>.+?<length>(?<length>\d+)</length>.+?<url><!\[CDATA\[(?<url>.+?)\]\]></url>.+?</durl>", RegexOptions.Singleline);
			MatchCollection matches = r.Matches(source);
			foreach (Match item in matches)
			{
				var pri = new ParseResultItem();
				string real = item.Groups["url"].Value;
				if (real.EndsWith(".flv", StringComparison.CurrentCultureIgnoreCase) ||
					real.EndsWith(".hlv", StringComparison.CurrentCultureIgnoreCase) ||
					real.EndsWith(".mp4", StringComparison.CurrentCultureIgnoreCase))
				{
					if (!string.IsNullOrEmpty(vstr))
					{
						//real = real +"?vstr=" + vstr;
					}
				}
				pri.RealAddress = real;
				pri.Information.Add("order", item.Groups["order"].Value);
				pri.Information.Add("length", item.Groups["length"].Value);
				pr.Items.Add(pri);
			}
			//返回结果
			return pr;
		}
	}

}
