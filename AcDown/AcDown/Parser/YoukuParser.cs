using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;
using System.Net;
using Kaedei.AcDown.Interface.Forms;

namespace Kaedei.AcDown.Parser
{
	public class YoukuParser : IParser
	{
		#region IParser 成员

		/// <summary>
		/// 解析优酷视频源文件地址
		/// </summary>
		///<remarks>自动应答会检查前缀为<i>youku</i>的所有设置</remarks>
		public ParseResult Parse(ParseRequest request)
		{
			ParseResult pr = new ParseResult();

			string url = @"http://v.youku.com/player/getPlayList/VideoIDS/%ID%/timezone/+08/version/5/source/video?n=3&ran=4656&password=%PW%".Replace(@"%ID%", request.Id).Replace(@"%PW%", request.Password);
			string xmldoc = Network.GetHtmlSource(url, Encoding.UTF8, request.Proxy);
			//正则表达式提取各个参数
			string regexstring = "\"seed\":(?<seed>\\w+),.+\"key1\":\"(?<key1>\\w+)\",\"key2\":\"(?<key2>\\w+)\".+\"streamfileids\":{\"(?<fileposfix>\\w+)\":\"(?<fileID>[0-9\\*]+)";
			Regex r = new Regex(regexstring);
			Match m = r.Match(xmldoc);
			//提取参数
			double seed = Double.Parse(m.Groups["seed"].Value);
			string key1 = m.Groups["key1"].Value;
			string key2 = m.Groups["key2"].Value;


			//提取各视频参数
			Regex rStreamFileIds = new Regex(@"""streamfileids"":{(?<fileids>.+?)}");
			Match mStreamFileIds = rStreamFileIds.Match(xmldoc);
			string fileIds = mStreamFileIds.Groups["fileids"].Value;

			var dict = new Dictionary<string, string>();
			string defaultres = "";
			//是否有超清模式
			if (fileIds.Contains("hd2"))
			{
				dict.Add("hd2", "超清(hd2)");
				defaultres = "hd2";
			}
			//是否有高清模式
			if (fileIds.Contains("mp4"))
			{
				dict.Add("mp4", "高清(mp4)");
				defaultres = "mp4";
			}
			//是否有普通清晰度
			if (fileIds.Contains("flv"))
			{
				dict.Add("flv", "标清(flv)");
				defaultres = "flv";
			}


			string fileposfix = null;
			string strSelect = null;

			//自动应答
			if (request.AutoAnswers.Count > 0)
			{
				foreach (var item in request.AutoAnswers)
				{
					if (item.Prefix == "youku")
					{
						if (fileIds.Contains(item.Identify))
						{
							strSelect = item.Identify;
							fileposfix = item.Identify;
							break;
						}
					}
				}
			}

			if (string.IsNullOrEmpty(fileposfix))
			{
				//如果多余一种清晰度
				if (dict.Count > 1)
				{
					fileposfix = ToolForm.CreateSingleSelectForm("您正在下载优酷视频，请选择视频清晰度:", dict, defaultres, request.AutoAnswers, "youku");
				}
				else
				{
					fileposfix = defaultres;
				}
			}

			//修正高清
			if (fileposfix == "hd2") fileposfix = "flv";

			//取得FileID
			Regex rFileID = new Regex(@"""" + strSelect + @""":""(?<fileid>.+?)""");
			Match mFileID = rFileID.Match(fileIds);
			string fileID = mFileID.Groups["fileid"].Value;

			//提取视频个数
			int flv_no = 0;
			string regexFlvNo = @"""segs"":{""\w+"":\[(?<content>.+?)\]";
			Regex rn = new Regex(regexFlvNo);
			Match mn = rn.Match(xmldoc);
			char[] tmp_content = mn.Groups["content"].Value.ToCharArray();
			foreach (char item in tmp_content)
			{
				if (item == '{') flv_no++;
			}

			//提取key
			Regex rSegs = new Regex(@"segs"":{(?<segs>.+?)},""streamsizes");
			Match mSegs = rSegs.Match(xmldoc);
			string segs = mSegs.Groups["segs"].Value;
			Regex rSegsSub = new Regex(@"""" + strSelect + @""":\[(?<segssub>.+?)\]");
			Match mSegsSub = rSegsSub.Match(segs);
			string segssub = mSegsSub.Groups["segssub"].Value;



			string regexKey = @"""k"":""(?<k>\w+)"",""k2"":""(?<k2>\w+)""";
			MatchCollection mcKey = Regex.Matches(segssub, regexKey);
			List<string> keys = new List<string>();
			foreach (Match mKey in mcKey)
			{
				keys.Add("?K=" + mKey.Groups["k"].Value + ",k2:" + mKey.Groups["k2"].Value);
			}

			//生成sid
			string sid = genSid();
			//生成fileid
			string fileid = getFileID(fileID, seed);
			//生成key
			//string key = genKey(key1, key2);
			//添加各个地址
			List<string> lst = new List<string>();
			for (int i = 0; i < flv_no; i++)
			{
				//得到地址
				string u = "http://f.youku.com/player/getFlvPath/sid/" + sid + "_" + string.Format("{0:D2}", i) +
					"/st/" + fileposfix + "/fileid/" + fileid.Substring(0, 8) + string.Format("{0:D2}", i)
					+ fileid.Substring(10) + keys[i];
				//添加地址
				pr.Items.Add(new ParseResultItem(new Uri(u)));
			}
			return pr;
		}
		#endregion

		private String genSid()
		{
			int i1 = (int)(1000 + Math.Floor((double)(new Random().Next(999))));
			int i2 = (int)(1000 + Math.Floor((double)(new Random().Next(9000))));
			TimeSpan ts = new TimeSpan(System.DateTime.UtcNow.Ticks - new DateTime(1970, 1, 1, 0, 0, 0).Ticks);
			return Convert.ToInt64(ts.TotalMilliseconds).ToString() + "" + i1 + "" + i2;
		}

		private String getFileID(String fileid, double seed)
		{
			String mixed = getFileIDMixString(seed);
			String[] ids = fileid.Split('*');
			StringBuilder realId = new StringBuilder();
			int idx;
			for (int i = 0; i < ids.Length - 1; i++)
			{
				idx = int.Parse(ids[i]);
				realId.Append(mixed[idx]);
			}
			return realId.ToString();
		}

		private String getFileIDMixString(double seed)
		{
			StringBuilder mixed = new StringBuilder();
			StringBuilder source = new StringBuilder("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ/\\:._-1234567890");
			int index, len = source.Length;
			for (int i = 0; i < len; ++i)
			{
				seed = (seed * 211 + 30031) % 65536;
				index = (int)Math.Floor(seed / 65536 * source.Length);
				mixed.Append(source[index]);
				source.Remove(index, 1);
			}
			return mixed.ToString();
		}

		private String genKey(String key1, String key2)
		{
			int key = Convert.ToInt32(key1, 16);
			var tempkey = key ^ 0xA55AA5A5;
			return key2 + Convert.ToString(tempkey, 16).Substring(8);
		}

	}
}
