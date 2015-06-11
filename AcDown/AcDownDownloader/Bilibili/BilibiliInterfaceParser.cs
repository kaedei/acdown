using System.Net;
using Kaedei.AcDown.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

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
			var ts = Convert.ToInt64((DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds);
			string url = string.Format(@"http://interface.bilibili.com/playurl?appkey={0}&cid={1}&ts={2}&sign={3}",
							BilibiliPlugin.AppKey,
							request.Id,
							ts,
							Tools.GetStringHash("appkey=" + BilibiliPlugin.AppKey +
												"&cid=" + request.Id +
												"&ts=" + ts +
												BilibiliPlugin.AppSecret));
			var httpRequest = (HttpWebRequest)WebRequest.Create(url);
			httpRequest.Proxy = request.Proxy;
			httpRequest.CookieContainer = request.CookieContainer;

			string source = Network.GetHtmlSource(httpRequest, Encoding.UTF8);

			Video videoInfo;
			var serializer = new XmlSerializer(typeof(Video));
			using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(source)))
			{
				videoInfo = (Video) serializer.Deserialize(ms);
			}

			//视频总长度
			var totallength = videoInfo.Timelength;
			if (totallength != null) pr.SpecificResult.Add("totallength", totallength);
			//src
			var src = videoInfo.Src;
			if (src != null) pr.SpecificResult.Add("src", src);

			//视频信息
			foreach (var durl in videoInfo.Durl)
			{
				var pri = new ParseResultItem()
				{
					RealAddress = durl.Url
				};
				pri.Information.Add("order", durl.Order);
				pri.Information.Add("length", durl.Length);
				pr.Items.Add(pri);
			}
			//返回结果
			return pr;
		}
	}
}
