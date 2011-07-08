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
#region XML神马的太讨厌啦

			////取得xml配置
			//WebRequest wr = WebRequest.Create(url);
			//var response = wr.GetResponse();
			//SinaVideo v;
			//using (var sr = new StreamReader(response.GetResponseStream(),Encoding.UTF8))
			//{
			//   XmlSerializer s = new XmlSerializer(typeof(SinaVideo));
			//   //反序列化对象
			//   v = (SinaVideo)s.Deserialize(sr);
			//}

			////将URL复制制List<string>中
			//List<string> r = new List<string>(v.durl.Length);
			//for (int i = 0; i < v.durl.Length; i++)
			//{
			//   r[i] = v.durl[i].url;
			//}
#endregion
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


	[Serializable]
	public class SinaVideo
	{
		public string result = "";
		public Int32 timelength = 0;
		public Int32 framecount = 0;
		public string vname = "";
		public string src = "";
		[XmlElement()]
		public Part[] durl;
		public string vtags = "";
		public string ad = "";
		public string vstr = "";
		public string vip = "";
		public string vround = "";
		public Int32 type = 0;
		public string message = "";
	}

	[Serializable]
	public class Part
	{
		public Int32 order = 0;
		public Int32 length = 0;
		public string url = "";
	}

}
