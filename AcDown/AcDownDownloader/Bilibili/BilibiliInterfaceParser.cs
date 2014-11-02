using System.Net;
using Kaedei.AcDown.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Kaedei.AcDown.Downloader.Bilibili
{
	/// <summary>
	/// Bilibili解析器
	/// </summary>
	public class BilibiliInterfaceParser : IParser
	{
		/// <summary>
		/// 通过Bilibili接口解析视频文件源地址
		/// </summary>
		public ParseResult Parse(ParseRequest request)
		{
			ParseResult pr = new ParseResult();

			//合并完整url
			//这里使用标准方法调用Bilibili API
			string url = @"http://interface.bilibili.com/playurl?appkey=876fe0ebd0e67a0f&cid=" + request.Id +
						"&ts=1406821992&sign=" +
						Tools.GetStringHash("appkey=876fe0ebd0e67a0f&cid=" + request.Id + "&ts=1406821992f487b808dc82abb7464a00935d4bb247");
			var httpRequest = (HttpWebRequest) WebRequest.Create(url);
			httpRequest.Proxy = request.Proxy;
			httpRequest.CookieContainer = request.CookieContainer;

			string source = Network.GetHtmlSource(httpRequest, Encoding.UTF8);
			//视频总长度
			var totallength = Regex.Match(source, @"<timelength>(?<timelength>\d+)</timelength>").Groups["timelength"];
			if (totallength != null) pr.SpecificResult.Add("totallength", totallength.Value);
			//Framecount
			var framecount = Regex.Match(source, @"<framecount>(?<framecount>\d+)</framecount>").Groups["framecount"];
			if (framecount != null) pr.SpecificResult.Add("framecount", framecount.Value);
			//src
			var src = Regex.Match(source, @"<src>(?<src>\d+)</src>").Groups["src"];
			if (src != null) pr.SpecificResult.Add("src", src.Value);
			//vstr
			var vstr = Regex.Match(source, @"(?<=<vstr><!\[CDATA\[)\w+");
			if (vstr.Success) pr.SpecificResult.Add("vstr", vstr.Value);

			//视频信息
			Regex r = new Regex(@"<durl>.+?<order>(?<order>\d+)</order>.+?<length>(?<length>\d+)</length>.+?<url><!\[CDATA\[(?<url>.+?)\]\]></url>.+?</durl>", RegexOptions.Singleline);
			MatchCollection matches = r.Matches(source);
			if (matches.Count <= 0)
			{
				throw new Exception("BiliBili Interface Parser Error: " + url);
			}
			foreach (Match item in matches)
			{
				var pri = new ParseResultItem();
				string real = item.Groups["url"].Value;
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
