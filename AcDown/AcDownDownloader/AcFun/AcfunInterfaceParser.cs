using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Kaedei.AcDown.Interface;
using Kaedei.AcDown.Interface.Forms;
using Newtonsoft.Json;

namespace Kaedei.AcDown.Downloader.AcFun
{
	class AcfunInterfaceParser:IParser
	{
		public ParseResult Parse(ParseRequest request)
		{
			var httpRequest =(HttpWebRequest) WebRequest.Create(@"http://jiexi.acfun.info/index.php?type=mobileclient&vid=" + request.Id);
			httpRequest.Proxy = request.Proxy;
			httpRequest.UserAgent = "Dalvik/1.6.0 (Linux; U; Android 4.2.2; H30-U10 Build/HuaweiH30-U10)";
			httpRequest.Accept = "application/json";
			httpRequest.Headers.Add("Accept-Encoding", "gzip");

			var json = Network.GetHtmlSource(httpRequest, Encoding.UTF8);
			var acInfo = JsonConvert.DeserializeObject<AcInfo>(json);

			if (acInfo.code != 200)
			{
				throw new Exception("AcfunInterfaceParser: " + acInfo.message);
			}

			var pr = new ParseResult();

			var qualityDict = new Dictionary<string, string>();
			AddQualityDict(qualityDict, acInfo.result);
			string chosenQuality = ToolForm.CreateSingleSelectForm("请选择视频清晰度：", qualityDict, "", request.AutoAnswers, "acfun");
			var videoInfo = GetVideoInfoFromQuality(acInfo, chosenQuality);

			//视频总长度
			pr.SpecificResult.Add("totallength", videoInfo.totalseconds.ToString());
			//Framecount
			pr.SpecificResult.Add("framecount", "0");
			//src
			pr.SpecificResult.Add("src", "200");
			//vstr
			pr.SpecificResult.Add("vstr", "");

			videoInfo.files.ForEach(file =>
			{
				pr.Items.Add(new ParseResultItem
				{
					RealAddress = file.url,
					Information = new SerializableDictionary<string, string>()
					{
						{"length", (file.seconds*1000).ToString()},
						{"order", file.no.ToString()}
					}
				});
			});

			return pr;
		}

		private C GetVideoInfoFromQuality(AcInfo acInfo, string chosenQuality)
		{
			switch (chosenQuality)
			{
				case "C00":
					return acInfo.result.C00;
				case "C10":
					return acInfo.result.C10;
				case "C20":
					return acInfo.result.C20;
				case "C30":
					return acInfo.result.C30;
				case "C40":
					return acInfo.result.C40;
				case "C50":
					return acInfo.result.C50;
				case "C60":
					return acInfo.result.C60;
				case "C70":
					return acInfo.result.C70;
				case "C80":
					return acInfo.result.C80;
				case "C90":
					return acInfo.result.C90;
				default:
					return null;
			}
		}

		private void AddQualityDict(Dictionary<string, string> qualityDict, Result result)
		{
			if (result.C00 != null)
			{
				qualityDict.Add("C00", result.C00.quality);
			}
			if (result.C10 != null)
			{
				qualityDict.Add("C10", result.C10.quality);
			}
			if (result.C20 != null)
			{
				qualityDict.Add("C20", result.C20.quality);
			}
			if (result.C30 != null)
			{
				qualityDict.Add("C30", result.C30.quality);
			}
			if (result.C40 != null)
			{
				qualityDict.Add("C40", result.C40.quality);
			}
			if (result.C50 != null)
			{
				qualityDict.Add("C50", result.C50.quality);
			}
			if (result.C60 != null)
			{
				qualityDict.Add("C60", result.C60.quality);
			}
			if (result.C70 != null)
			{
				qualityDict.Add("C70", result.C70.quality);
			}
			if (result.C80 != null)
			{
				qualityDict.Add("C80", result.C80.quality);
			}
			if (result.C90 != null)
			{
				qualityDict.Add("C90", result.C90.quality);
			}
		}
	}
}
