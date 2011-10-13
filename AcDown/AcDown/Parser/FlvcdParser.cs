using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;

namespace Kaedei.AcDown.Parser
{
	public class FlvcdParser:IParser
	{
		#region IParser 成员

		/// <summary>
		/// 解析任意视频源地址
		/// </summary>
		/// <param name="parameters">维度为1、长度为3的字符串数组，1的内容为待分析的视频页面地址,2的内容为视频密码(如果有的话),3为清晰度设置</param>
		/// <param name="proxy"></param>
		/// <returns></returns>
		public string[] Parse(string[] parameters, System.Net.WebProxy proxy)
		{
			//原始Url
			string ourl = parameters[0];
			//密码
			string password = parameters[1];
			//清晰度
			string resolution = parameters[2];

			//修正url
			string url = "http://www.flvcd.com/parse.php?kw=" + Tools.UrlEncode(ourl) + resolution;


			//取得网页源文件
			string src = Network.GetHtmlSource(url, Encoding.GetEncoding("GB2312"), proxy);

			//检查是否需要密码
			if (src.Contains("请输入密码"))
			{
				url = url + "&passwd=" + password;
				src = Network.GetHtmlSource(url, Encoding.GetEncoding("GB2312"), proxy);
			}

			//取得内容
			Regex rContent = new Regex("<input type=\"hidden\" name=\"inf\".+\">", RegexOptions.Singleline);
			Match mContent = rContent.Match(src);
			string content = mContent.Value;

			//取得各Part下载地址
			List<string> partUrls = new List<string>();
			Regex rPartUrls = new Regex(@"<U>(?<url>.+)");
			MatchCollection mcPartUrls = rPartUrls.Matches(content);
			foreach (Match item in mcPartUrls)
			{
				partUrls.Add(item.Groups["url"].Value);
			}
			return partUrls.ToArray();
		}

		#endregion
	}
}
