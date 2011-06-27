using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;

namespace Kaedei.AcDown.Parser
{
	public class YoukuParser : IParser
	{
		#region IParser 成员

		/// <summary>
		/// 解析优酷视频源文件地址
		/// </summary>
		/// <param name="parameters">维度为1、长度为2的字符串数组，1的内容为待分析的视频ID（v=?）,2的内容为视频密码(如果有的话)</param>
		/// <returns>各分段视频的真实地址数组(如果存在MP4格式则优先返回)</returns>
		public string[] Parse(string[] parameters)
		{
			List<string> returnarray = new List<string>();
			string pw = (parameters.Length > 1) ? parameters[1] : "";

			string url = @"http://v.youku.com/player/getPlayList/VideoIDS/%ID%/version/5/source/video?password=%PW%&ran=5505&n=3".Replace(@"%ID%", parameters[0]).Replace(@"%PW%", pw);
			string xmldoc = Network.GetHtmlSource(url, Encoding.UTF8);
			//正则表达式提取各个参数
			string regexstring = "\"seed\":(?<seed>\\w+),.+\"key1\":\"(?<key1>\\w+)\",\"key2\":\"(?<key2>\\w+)\".+\"streamfileids\":{\"(?<fileposfix>\\w+)\":\"(?<fileID>[0-9\\*]+)";
			Regex r = new Regex(regexstring);
			Match m = r.Match(xmldoc);
			//提取参数
			double seed = Double.Parse(m.Groups["seed"].Value);
			string key1 = m.Groups["key1"].Value;
			string key2 = m.Groups["key2"].Value;
			string fileposfix = m.Groups["fileposfix"].Value;
			if (fileposfix == "hd2") fileposfix = "flv";
			string fileID = m.Groups["fileID"].Value;
			//提取视频个数
			string regexFlvNo = @"(#{$no$:($|)(?<flvno>#d+)($|),$size$:$#d+$,$seconds$:$#d+$(,$#w+$:$#w+$|)#}#])".Replace("#", @"\").Replace("$", "\"");
			Regex rn = new Regex(regexFlvNo);
			Match mn = rn.Match(xmldoc);
			int flv_no = Int32.Parse(mn.Groups["flvno"].Value);
			//生成sid
			string sid = genSid();
			//生成fileid
			string fileid = getFileID(fileID, seed);
			//生成key
			string key = genKey(key1, key2);
			//添加各个地址
			List<string> lst = new List<string>();
			for (int i = 0; i < flv_no + 1; i++)
			{
				lst.Add("http://f.youku.com/player/getFlvPath/sid/" + sid + "_" + string.Format("{0:D2}", flv_no) +
				"/st/" + fileposfix + "/fileid/" + fileid + "?K=" + key);
			}
			return lst.ToArray();
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
