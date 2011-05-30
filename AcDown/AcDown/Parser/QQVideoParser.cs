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
			string url = @"http://vv.video.qq.com/geturl?ran=0.16436194255948067&otype=xml&vid=%ID%&platform=1&format=2".Replace(@"%ID%", parameters[0]);
			string xmldoc = Network.GetHtmlSource(url, Encoding.UTF8);
			Regex r = new Regex(@"\<url\>(?<url>.*?)\</url\>");
			MatchCollection mc = r.Matches(xmldoc);
			if (mc.Count != 0)
			{
				foreach (Match item in mc)
				{
					returnarray.Add(item.Groups[@"url"].Value);
				}
				return returnarray.ToArray();
			}
			else
			{
				returnarray.Add(@"http://web.qqvideo.tc.qq.com/" + parameters[0] + ".flv");
				return returnarray.ToArray();
			}
			

		}

		#endregion
	}
}
