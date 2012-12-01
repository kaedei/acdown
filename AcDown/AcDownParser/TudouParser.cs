using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Kaedei.AcDown.Interface.Forms;
using System.Xml.Serialization;
using System.IO;

namespace Kaedei.AcDown.Interface
{
	public class TudouParser : IParser
	{
		#region IParser 成员
		/// <summary>
		/// 解析土豆视频
		/// </summary>
		/// <remarks>接受的AutoAnswer对象需要以<i>tudou</i>作为前缀</remarks>
		public ParseResult Parse(ParseRequest request)
		{
			ParseResult pr = new ParseResult();
			int t;
			if (!int.TryParse(request.Id, out t)) //测试iid是否是真实的(应该为数字)
			{
				//取得土豆网页面源代码
				string tudousource = Network.GetHtmlSource("http://www.tudou.com/programs/view/" + request.Id + "/", Encoding.GetEncoding("GB2312"), request.Proxy);
				//取得iid列表
				Regex rIid = new Regex(@"(?<=iid(:| = ))\d+", RegexOptions.Singleline);
				MatchCollection mIids = rIid.Matches(tudousource);
				List<string> iids = new List<string>();
				foreach (Match mIid in mIids)
				{
					iids.Add(mIid.Value);
				}
				//取得icode列表
				Regex rIcode = new Regex(@"(?<=icode(:| = )(""|'))[\w\-]+", RegexOptions.Singleline);
				MatchCollection mIcodes = rIcode.Matches(tudousource);
				List<string> icodes = new List<string>();
				foreach (Match mIcode in mIcodes)
				{
					icodes.Add(mIcode.Value);
				}
				//取得标题列表
				Regex rKw = new Regex(@"(?<=kw(:| = )(""|')).+?(?=(""|'))", RegexOptions.Singleline);
				MatchCollection mKws = rKw.Matches(tudousource);
				List<string> kws = new List<string>();
				foreach (Match mKw in mKws)
				{
					kws.Add(mKw.Value);
				}

				//检查需要的iid
				for (int i = 0; i < icodes.Count; i++)
				{
					if (request.Id.Equals(icodes[i]))
					{
						request.Id = iids[i];
						pr.SpecificResult["icode"] = icodes[i];
						pr.SpecificResult["title"] = kws[i];
						break;
					}
				}
			}
			pr.SpecificResult["iid"] = request.Id;

			string xmlurl = @"http://v2.tudou.com/v?st=1%2C2%2C3%2C4%2C99&it=" + request.Id + "&pw=" + request.Password;
			string xmldoc = Network.GetHtmlSource(xmlurl, Encoding.UTF8, request.Proxy);
			xmldoc = xmldoc.Replace("<a>", "").Replace("</a>", "").Replace("<b>", "").Replace("</b>", "");

			//反序列化XML文档
			TudouVideo tudou;
			var serializer = new XmlSerializer(typeof(TudouVideo));
			using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(xmldoc)))
			{
				tudou = (TudouVideo)serializer.Deserialize(ms);
			}

			//Regex rVideo = new Regex("<f [^>]+brt=\"(?<brt>\\d+)\">(?<url>[^<]+)</f>");
			//MatchCollection mcVideo = rVideo.Matches(xmldoc);

			//默认url
			string defaultUrl = "";

			//将 数字-清晰度 存入到字典中
			var resolutiondict = new Dictionary<string, string>();
			resolutiondict.Add("1", "流畅");
			resolutiondict.Add("2", "清晰");
			resolutiondict.Add("3", "高清");
			resolutiondict.Add("4", "超清");
			resolutiondict.Add("99", "原画");

			//将 清晰度-地址 存入到字典中
			var videodict = new Dictionary<string, string>();
			//foreach (Match item in mcVideo)
			foreach (var info in tudou.videos)
			{
				string brt = info.brt; // item.Groups["brt"].Value;
				string url = info.address; // item.Groups["url"].Value;
				if (string.IsNullOrEmpty(defaultUrl))
					defaultUrl = url;
				brt = resolutiondict[brt];
				if (!videodict.ContainsValue(brt)) //不覆盖已有的 清晰度-地址 对
					videodict.Add(url, brt);
			}

			//自动应答
			if (request.AutoAnswers != null && request.AutoAnswers.Count > 0)
			{
				string answer;
				foreach (var item in request.AutoAnswers)
				{
					if (item.Prefix == "tudou")
					{
						if (resolutiondict.ContainsKey(item.Identify)) //如果自动应答设置中有"1"/"2"/"3"/"99"
						{
							answer = resolutiondict[item.Identify]; //取得描述（如"流畅 256P"）
							foreach (var u in videodict.Keys) //从字典中查找描述信息所属的URL
							{
								if (videodict[u].Equals(answer))
								{
									pr.Items.Add(new ParseResultItem(u));
									return pr;
								}
							}
						}
					}
				}
			}

			string urladdress;
			//只在视频清晰度数量多余1个时进行选择
			if (videodict.Count > 1)
			{
				urladdress = ToolForm.CreateSingleSelectForm("请选择视频清晰度:", videodict, defaultUrl, request.AutoAnswers, "tudou");
			}
			else //如果只有一个清晰度，不进行选择
			{
				urladdress = defaultUrl;
			}

			pr.Items.Add(new ParseResultItem(urladdress));
			pr.Items[0].Information.Add("order", "1");
			//写入视频时长
			pr.Items[0].Information.Add("length", tudou.time);
			pr.SpecificResult.Add("totallength", tudou.time);
			return pr;
		}

		#endregion
	}

	[Serializable]
	[XmlRoot("v")]
	public class TudouVideo
	{
		[XmlAttribute("cd")]
		public string iid;

		[XmlAttribute("ch")]
		public string ch;

		[XmlAttribute("lg")]
		public string lg;

		[XmlAttribute("tm")]
		public string time;

		[XmlAttribute("tt")]
		public string title;

		[XmlAttribute("vi")]
		public string vi;

		[XmlAttribute("wt")]
		public string wt;

		[XmlElement("f")]
		public TudouVideoInfo[] videos;
	}

	[Serializable]
	public class TudouVideoInfo
	{
		[XmlAttribute("bc")]
		public string bc;
		[XmlAttribute("brt")]
		public string brt;
		[XmlAttribute("s1")]
		public string s1;
		[XmlAttribute("st")]
		public string st;
		[XmlText]
		public string address;
	}

}
