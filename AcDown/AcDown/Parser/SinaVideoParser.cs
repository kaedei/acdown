using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Xml.Serialization;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Kaedei.AcDown.Parser
{
	/// <summary>
	/// 新浪视频分析器
	/// </summary>
	public class SinaVideoParser:IParser 
	{

		#region IParser 成员
		/// <summary>
		/// 解析新浪视频视频文件源地址
		/// </summary>
		/// <param name="parameters">单视频的ID</param>
		/// <returns>所有分段视频地址的数组</returns>
		public string[] Parse(string[] parameters, WebProxy proxy)
		{
			//返回的数组
			List<string> address = new List<string>();
			//合并完整url
			string url = @"http://v.iask.com/v_play.php?vid=" + parameters[0];
			string source = Network.GetHtmlSource(url, Encoding.UTF8, proxy);
			Regex r = new Regex(@"http://.*(.flv|.f4v|.mp4|.hlv)");
			MatchCollection matches = r.Matches(source);
			foreach (var item in matches)
			{
				address.Add(item.ToString());
			}
			
			//返回数组
			return address.ToArray();

		}
		#endregion
	}

}
