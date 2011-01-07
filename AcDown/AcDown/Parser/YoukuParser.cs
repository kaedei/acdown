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
		/// <param name="parameters">维度为1、长度为1的字符串数组，内容为待分析的视频ID（v=?）</param>
		/// <returns>各分段视频的真实地址数组(如果存在MP4格式则优先返回)</returns>
		public string[] Parse(string[] parameters)
		{
			List<string> returnarray = new List<string>();
			string url = @"http://v.youku.com/player/getPlayList/VideoIDS/%ID%/version/5/source/video?password=&ran=5505&n=3".Replace(@"%ID%", parameters[0]);
			string xmldoc = Network.GetHtmlSource(url, Encoding.UTF8);
			//正则表达式提取各个参数
			string regexstring = @"$seed$:(?<seed>\w+),.+key1$:$(?<key1>\w+)$,$key2$:$(?<key2>\w+).+$streamfileids$:{$(?<fileposfix>\w+)$:$(?<fileID>[0-9\*]+)$.+$:\[[^\]]*{$no$:$*(?<flv_no>\d+)$".Replace("$", "\"");
			Regex r = new Regex(regexstring);
			Match m = r.Match(xmldoc);
			//提取参数
			int seed = Int32.Parse(m.Groups["seed"].Value);
			string key1 = m.Groups["key1"].Value;
			string key2 = m.Groups["key2"].Value;
			string fileposfix = m.Groups["fileposfix"].Value;
			string fileID = m.Groups["fileID"].Value;
			int flv_no = Int32.Parse(m.Groups["flv_no"].Value);

			//byte bKey1str = Byte.Parse(Key1str,System.Globalization.NumberStyles.HexNumber);
			int bKey1str = Convert.ToInt32(key1, 16);
			string bbkey1 = Convert.ToString((bKey1str ^ 0XA55AA5A5), 16).Substring(8, 8);
			for (int q = 0; q < 8; q++)
			{
				if (bbkey1.StartsWith("0"))
					bbkey1 = bbkey1.Substring(1, bbkey1.Length - 1);
			}

			string TIMEstring;

			string KKstr = key2 + bbkey1;

			string IIstring2 = cg_fun(fileID, seed);


			System.Random random = new Random();
			int number = random.Next(9000);

			DateTime submittime = DateTime.Now;
			DateTime dt = new DateTime(1970, 1, 1);
			TimeSpan ts = DateTime.Now - dt;

			long YY = Convert.ToInt64(ts.TotalMilliseconds * 1000 / 1000);

			int kk = ts.Milliseconds;

			TIMEstring = YY.ToString() + (1000 + kk).ToString() + (number + 1000);

			string MYMY1 = "";

			string MYMY3 = "";

			MYMY1 = IIstring2.Substring(0, 8);

			MYMY3 = IIstring2.Substring(10, IIstring2.Length - 10);

			string tempurl = "http://f.youku.com/player/getFlvPath/sid/" + TIMEstring + "_00/st/" +  fileposfix + "/fileid/" + MYMY1 + "00" + MYMY3 + "?K=" + KKstr + "&myp=0&ts=425";
			returnarray.Add(tempurl);
			for (int i = 1; i < flv_no; i++)
			{
				string hex = "";
				if (i < 16) hex = "0";
				hex += ConvertHex(i.ToString());
				returnarray.Add("http://f.youku.com/player/getFlvPath/sid/" + TIMEstring + "_" + hex + "/st/" + fileposfix + "/fileid/" + MYMY1 + hex + MYMY3 + "?K=" + KKstr + "&myp=0&ts=425");
			}
			return returnarray.ToArray();

		}
		#endregion

		/// <summary>
		/// 返回１６进制
		/// </summary>
		/// <param name="value">value</param>
		/// <returns></returns>
		public static string ConvertHex(string value)
		{
			string sReturn = string.Empty;
			try
			{

				while (Convert.ToUInt32(value) > 16)
				{
					int v = int.Parse(value);
					sReturn = GetHexChar((v % 16).ToString()) + sReturn;
					value = Math.Floor(Convert.ToDouble(v / 16)).ToString();
				}
				sReturn = GetHexChar(value) + sReturn;
			}
			catch
			{
				sReturn = "###Valid Value!###";
			}
			return sReturn;
		}



		/// <summary>
		/// 返回１６进制字符
		/// </summary>
		/// <param name="value">value</param>
		/// <returns></returns>
		public static string GetHexChar(string value)
		{
			string sReturn = string.Empty;
			switch (value)
			{
				case "10":
					sReturn = "A";
					break;
				case "11":
					sReturn = "B";
					break;
				case "12":
					sReturn = "C";
					break;
				case "13":
					sReturn = "D";
					break;
				case "14":
					sReturn = "E";
					break;
				case "15":
					sReturn = "F";
					break;
				default:
					sReturn = value;
					break;
			}
			return sReturn;
		}


		int randomSeed = 0;
		private string cg_hun(int randomSeed)
		{
			string cg_str = "";
			string str1 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ/\\:._-1234567890";
			int MYLEN = str1.Length;
			int _loc3 = 0;
			double cccc = 0.0;

			for (int k = 0; k < MYLEN; ++k)
			{
				cccc = MYran(randomSeed);

				_loc3 = Convert.ToInt32(cccc * Convert.ToDouble(str1.Length) * 1000) / 1000;

				cg_str = cg_str + str1.Substring(_loc3, 1);
				if (_loc3 == 0)
					str1 = str1.Substring(1, str1.Length - 1);
				else if (_loc3 == str1.Length)
					str1 = str1.Substring(0, str1.Length - 1);
				else
					str1 = str1.Substring(0, _loc3) + str1.Substring(_loc3 + 1, str1.Length - _loc3 - 1);
			} // end of for.

			return cg_str;
		} // End of the functions

		private double MYran(int randomSeed)
		{
			double abc = 0.0;
			randomSeed = (randomSeed * 211 + 30031) % 65536;
			abc = randomSeed / 65536.0;

			return abc;
		} // End of the function

		private string cg_fun(string b,int randomSeed)
		{
			string mystring = cg_hun(randomSeed);
			string[] mysplit = b.Split('*');
			string _loc4 = "";
			for (int _loc2 = 0; _loc2 < mysplit.Length - 1; ++_loc2)
			{
				_loc4 = _loc4 + mystring.Substring(Convert.ToInt32(mysplit[_loc2]), 1);
			} // end of for
			return _loc4;
		} // End of the function



		
	}
}
