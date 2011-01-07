using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;

namespace Kaedei.AcDown.Parser
{
	public class QQVideoParser:IParser
	{
		#region IParser 成员

		/// <summary>
		/// 解析QQ视频源地址
		/// </summary>
		/// <param name="parameters">维度为1、长度为1的字符串数组，内容为待分析的视频ID（v=?）</param>
		/// <returns>各分段视频的真实地址数组</returns>
		public string[] Parse(string[] parameters)
		{
			List<string> returnarray = new List<string>();		
			//完整url
			string url = @"http://video.qq.com/v1/vstatus/geturl?ran=0.32149567320163522&vid=%ID%&platform=1&otype=xml".Replace(@"%ID%", parameters[0]);
			string xmldoc = Network.GetHtmlSource(url, Encoding.UTF8);
			Regex r = new Regex(@"\<url\>(?<url>.*?)\</url\>");
			MatchCollection mc = r.Matches(xmldoc);
			foreach (Match item in mc)
			{
				returnarray.Add(item.Groups[@"url"].Value);
			}
			return returnarray.ToArray();

		}

		#endregion
	}
}
