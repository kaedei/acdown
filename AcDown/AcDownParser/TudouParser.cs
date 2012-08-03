using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;
using System.Net;
using System.Collections.Specialized;
using Kaedei.AcDown.Interface.Forms;

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
			if(!int.TryParse(request.Id,out t)) //测试iid是否是真实的(应该为数字)
			{
				//取得土豆网页面源代码
				string tudousource = Network.GetHtmlSource("http://www.tudou.com/programs/view/" + request.Id + "/", Encoding.GetEncoding("GB2312"), request.Proxy);
				//取得iid
				Regex r1 = new Regex(@"(I|i)id = (?<iid>\d.*)");
				Match m1 = r1.Match(tudousource);
				request.Id = m1.Groups["iid"].ToString();
			}
			
			string xmlurl = @"http://v2.tudou.com/v?st=1%2C2%2C3%2C4%2C99&it=" + request.Id + "&pw=" + request.Password;
			string xmldoc = Network.GetHtmlSource(xmlurl, Encoding.UTF8, request.Proxy);
			Regex rVideo = new Regex("<f [^>]+brt=\"(?<brt>\\d+)\">(?<url>[^<]+)</f>");
			MatchCollection mcVideo = rVideo.Matches(xmldoc);

			//默认url
			string defaultUrl = "";

			//将 数字-清晰度 存入到字典中
			var resolutiondict = new Dictionary<string, string>();
			resolutiondict.Add("1", "流畅 256P");
			resolutiondict.Add("2", "清晰 360P");
			resolutiondict.Add("3", "高清 720P");
			resolutiondict.Add("99", "原画");

			//将 清晰度-地址 存入到字典中
			var videodict = new Dictionary<string, string>();
			foreach (Match item in mcVideo)
			{
				string brt = item.Groups["brt"].Value;
				string url = item.Groups["url"].Value;
				if (string.IsNullOrEmpty(defaultUrl))
					defaultUrl = url;
				brt = resolutiondict[brt];
				if (!videodict.ContainsValue(brt)) //不覆盖已有的 清晰度-地址 对
					videodict.Add(url, brt);
			}

			//自动应答
			if (request.AutoAnswers.Count > 0)
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
									pr.Items.Add(new ParseResultItem(new Uri(u)));
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

			pr.Items.Add(new ParseResultItem(new Uri(urladdress)));
			return pr;
		}

		#endregion
	}
}
