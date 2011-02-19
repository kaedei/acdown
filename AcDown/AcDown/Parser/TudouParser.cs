using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;

namespace Kaedei.AcDown.Parser
{
	class TudouParser:IParser
	{
		#region IParser 成员
		/// <summary>
		/// 解析土豆视频
		/// </summary>
		/// <param name="parameters">维度为1、长度为1的字符串数组，内容为待分析的视频ID（iid）</param>
		/// <returns>各备用视频的真实地址数组</returns>
		public string[] Parse(string[] parameters)
		{
			List<string> returnarray = new List<string>();
			string iid = parameters[0];
			string xmlurl = @"http://v2.tudou.com/v?it=" + iid;
			string xmldoc = Network.GetHtmlSource(xmlurl, Encoding.UTF8);
			Regex r = new Regex(@"brt=""(?<brt>\d)""\>(?<url>http://.*?)\</f\>");
			MatchCollection mc = r.Matches(xmldoc);
			for (int i = 4; i > 0; i--)
			{
				foreach (Match item in mc)
				{
					if (item.Groups["brt"].ToString() == i.ToString())
					{
						return new string[] { item.Groups["url"].ToString() };
					}
				}
			}
			return null;
			//foreach (Match item in mc)
			//{
			//   returnarray.Add(item.Groups["url"].Value);
			//}
			//return returnarray.ToArray();
		}

		#endregion
	}
}
