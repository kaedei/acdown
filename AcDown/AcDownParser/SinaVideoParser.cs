using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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

			/*
			 * 参数   解释
			 * vid   和以前的vid一样
			 * ran   生成的一个随机数
			 * p     值为i，意义不明
			 * k     16+N长度的hash值
			 * 
			 * 把这个算法拿出去用的都要打PP！
			 */
			var ran = new Random(Environment.TickCount).Next(0, 1000);
			var key = WhereIsTheKey(request.Id, "0", ran);

			//合并完整url
			string url = @"http://v.iask.com/v_play.php?vid=" + request.Id + "&ran=" + ran + "&p=i&k=" + key;
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
				pri.RealAddress = item.Groups["url"].Value;
				pri.Information.Add("order", item.Groups["order"].Value);
				pri.Information.Add("length", item.Groups["length"].Value);
				pr.Items.Add(pri);
			}
			//返回结果
			return pr;
		}

		private string WhereIsTheKey(string vid, string time, int ran)
		{
			//前方神秘代码出没
			return GetStringHash(vid + "Z6prk18aWxP278cVAH" + time + ran).Substring(0, 16) + time;
		}


		/// <summary>
		/// 算出一个字符串的MD5
		/// </summary>
		string GetStringHash(string content)
		{
			var sb = new StringBuilder(32);
			var md5 = new MD5CryptoServiceProvider();

			var fileContent = Encoding.UTF8.GetBytes(content);

			byte[] hash = md5.ComputeHash(fileContent);

			foreach (byte b in hash)
			{
				int i = Convert.ToInt32(b);
				int j = i >> 4;
				sb.Append(Convert.ToString(j, 16));
				j = ((i << 4) & 0x00ff) >> 4;
				sb.Append(Convert.ToString(j, 16));
			}
			return sb.ToString();
		}
	}

}
