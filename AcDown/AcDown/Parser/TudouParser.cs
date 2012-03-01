using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;
using System.Net;
using System.Collections.Specialized;
using Kaedei.AcDown.Interface.Forms;

namespace Kaedei.AcDown.Parser
{
	public class TudouParser:IParser
	{
		#region IParser 成员
		/// <summary>
		/// 解析土豆视频
		/// </summary>
		/// <param name="parameters">维度为1、长度为2的字符串数组，1内容为待分析的视频ID（iid）,2为视频密码</param>
		/// <returns>视频指定清晰度(用户选择)的真实地址数组</returns>
		public string[] Parse(string[] parameters, WebProxy proxy)
		{
			//密码
			string pw = "";
			if (parameters.Length > 1) pw = parameters[1];
			
			List<string> returnarray = new List<string>();
			string iid = parameters[0];
			try //测试iid是否是真实的(应该为数字)
			{
				int t = int.Parse(iid);
			}
			catch
			{
				//取得土豆网页面源代码
				string tudousource = Network.GetHtmlSource("http://www.tudou.com/programs/view/" + iid + "/", Encoding.GetEncoding("GB2312"), proxy);
				//取得iid
				Regex r1 = new Regex(@"(I|i)id = (?<iid>\d.*)");
				Match m1 = r1.Match(tudousource);
				iid = m1.Groups["iid"].ToString();
			}
			string xmlurl = @"http://v2.tudou.com/v?st=1%2C2%2C3%2C4%2C99&it=" + iid + "&pw=" + pw;
			string xmldoc = Network.GetHtmlSource(xmlurl, Encoding.UTF8, proxy);
			Regex rVideo = new Regex("<f [^>]+brt=\"(?<brt>\\d+)\">(?<url>[^<]+)</f>");
			MatchCollection mcVideo = rVideo.Matches(xmldoc);

			//将 清晰度-地址 存入到Dictionary中
			StringDictionary videodict = new StringDictionary();
			foreach (Match item in mcVideo)
			{
				string brt = item.Groups["brt"].Value;
				string url = item.Groups["url"].Value;
				switch (brt)
				{
					case "1":
						brt = "流畅 256P";
						break;
					case "2":
						brt = "清晰 360P";
						break;
					case "3":
						brt = "高清 720P";
						break;
					case "99":
						brt = "原画";
						break;
					default:
						brt = "未知清晰度";
						break;
				}
				if (!videodict.ContainsKey(brt)) //不覆盖已有的 清晰度-地址 对
					videodict.Add(brt, url);
			}

			//设置 索引-清晰度 字典
			Dictionary<int,string> paradict = new Dictionary<int,string>();
			int i = 0;
			foreach (string item in videodict.Keys)
			{
				paradict.Add(i, item);
				i++;
			}
			//生成 索引 数组
			List<string> index = new List<string>();
			foreach (string item in paradict.Values)
			{
				index.Add(item);
			}
			//用户选择清晰度
			int selected;
			
			//只在视频清晰度数量多余1个时进行选择
			if (index.Count != 1)
			{
				selected = ToolForm.CreateSelectServerForm("请选择视频清晰度:", index.ToArray(), 0);
			}
			else //如果只有一个清晰度，不进行选择
			{
				selected = 0;
			}
			//取得清晰度
			string resolution = paradict[selected];
			//取得地址
			string urladdress = videodict[resolution];

			return new string[] { urladdress };
			//foreach (Match item in mc)
			//{
			//   returnarray.Add(item.Groups["url"].Value);
			//}
			//return returnarray.ToArray();
		}

		#endregion
	}
}
