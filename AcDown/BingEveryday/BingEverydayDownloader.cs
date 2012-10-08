using Kaedei.AcDown.Interface;
using Kaedei.AcDown.Interface.Downloader;
using Kaedei.AcDown.Interface.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Net;

namespace Kaedei.BingEveryday
{
	public class BingEverydayDownloader : CommonDownloader
	{
		//更换壁纸的API
		[DllImport("user32.dll", EntryPoint = "SystemParametersInfoA")]
		static extern Int32 SystemParametersInfo(Int32 uAction, Int32 uParam, string lpvParam, Int32 fuWinIni);

		/// <summary>
		/// 下载并更换当前壁纸
		/// </summary>
		public override bool Download()
		{
			TipText("正在选择区域");

			//获取所有区域
			var worldwidesrc = Network.GetHtmlSource("http://www.bing.com/account/worldwide", Encoding.UTF8, Info.Proxy);
			var mcMarkets = Regex.Matches(worldwidesrc, @"<li><a href=""http://bing\.com/\?scope=web&amp;setmkt=(?<mkt>.+?)&.+?>(?<region>.+?)</a></li>");
			//用户选择区域
			var dict = new Dictionary<string, string>();
			foreach (Match mMarket in mcMarkets)
				dict.Add(mMarket.Groups["mkt"].Value, mMarket.Groups["region"].Value);
			Settings["Market"] = ToolForm.CreateSingleSelectForm("请选择区域", dict, "zh-CN", null, "bing");
			Settings["Description"] = dict[Settings["Market"]];

			//合并目标区域的网址&获取源代码
			TipText("正在获取图片");
			var marketsrc = Network.GetHtmlSource("http://www.bing.com/?scope=web&setmkt=" + Settings["Market"], Encoding.UTF8);
			//获取背景图真实地址
			var image = "http://www.bing.com" + Regex.Match(marketsrc, @"(?<=g_img={url:').+?(?=')").Value;
			//设置文件名 如 zh-CN-20120123.jpg
			var filename = Settings["Market"] + "-" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("D2")
				+ DateTime.Now.Day.ToString("D2") + Path.GetExtension(image);
			filename = Path.Combine(Info.SaveDirectory.ToString(), filename);
			Info.FilePath.Add(filename);

			//设置任务标题
			Info.Title = string.Format("Bing每日壁纸: {0}年{1}月{2}日 - {3}", DateTime.Now.Year,
				DateTime.Now.Month.ToString("D2"), DateTime.Now.Day.ToString("D2"), Settings["Description"]);

			//下载图片
			NewPart(1, 1);

			//设置下载参数
			p.FilePath = filename;
			p.Url = image;
			p.Proxy = Info.Proxy;

			//下载图片
			DownloadFile();

			//设置壁纸
			if (!Tools.IsRunningOnMono)
			{
				SystemParametersInfo(20, 0, filename, 0x01 | 0x02);
			}

			return true;
		}


	}
}
