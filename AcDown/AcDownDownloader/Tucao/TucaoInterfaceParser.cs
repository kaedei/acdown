using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using Kaedei.AcDown.Interface;

namespace Kaedei.AcDown.Downloader.Tucao
{
	public class TucaoInterfaceParser
	{
		public ParseResult Parse(string type, string vid, WebProxy proxy)
		{
			ParseResult pr = new ParseResult();

			//合并完整url
			string url = string.Format(@"http://www.tucao.cc/api/videourl.php?type={0}&vid={1}", type, vid);

			string source = Network.GetHtmlSource(url, Encoding.UTF8, proxy).Trim();

			VideoDetails videoInfo;
			var serializer = new XmlSerializer(typeof(VideoDetails));
			using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(source)))
			{
				videoInfo = (VideoDetails)serializer.Deserialize(ms);
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
			pr.Items.Sort((p1, p2) => int.Parse(p1.Information["order"]) - int.Parse(p2.Information["order"]));
			//返回结果
			return pr;
		}
	}
}