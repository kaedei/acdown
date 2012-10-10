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
using System.Xml.Serialization;

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

			var now = DateTime.Now;

			//修正URL
			if (Info.Url.Equals("bing", StringComparison.CurrentCultureIgnoreCase))
			{
				Info.Url = "bing" + now.Year.ToString() + now.Month.ToString("D2")
				+ now.Day.ToString("D2");
			}
			else
			{
				var m = Regex.Match(Info.Url, @"(?<y>\d{4})(?<m>\d{2})(?<d>\d{2})");
				now = new DateTime(int.Parse(m.Groups["y"].Value), int.Parse(m.Groups["m"].Value), int.Parse(m.Groups["d"].Value));
			}

			//mkt代码-国家 字典
			var dictMarketRegion = new SerializableDictionary<string, string>();
			//mkt代码-图片URL 字典
			var dictMarketPic = new SerializableDictionary<string, string>();
			//mkt代码-图片本地位置 字典
			var dictMarketFile = new Dictionary<string, string>();

			//获取所有区域的背景图片地址
			//如果之前没有缓存
			if (!Settings.ContainsKey("dictMarketRegion") || !Settings.ContainsKey("dictMarketPic"))
			{
				var worldwidesrc = Network.GetHtmlSource("http://www.bing.com/account/worldwide", Encoding.UTF8, Info.Proxy);
				var mcMarkets = Regex.Matches(worldwidesrc, @"scope=web&amp;setmkt=(?<mkt>.+?)&.+?>(?<region>.+?)</a>");

				foreach (Match mMarket in mcMarkets)
					dictMarketRegion.Add(mMarket.Groups["mkt"].Value, mMarket.Groups["region"].Value);

				
				//获取每个区域的背景图
				foreach (string key in dictMarketRegion.Keys)
				{
					TipText("正在获取图片地址: " + dictMarketRegion[key]);
					var marketsrc = Network.GetHtmlSource("http://www.bing.com/?scope=web&setmkt=" + key, Encoding.UTF8);
					//获取背景图真实地址
					var image = "http://www.bing.com" + Regex.Match(marketsrc, @"(?<=g_img={url:').+?(?=')").Value;
					//添加到字典
					dictMarketPic.Add(key, image);
				}
				//序列化
				Settings["dictMarketRegion"] = SerializableDictionary<string, string>.WriteToString(dictMarketRegion);
				Settings["dictMarketPic"] = SerializableDictionary<string, string>.WriteToString(dictMarketPic);
			}
			else
			{
				//反序列化
				dictMarketRegion = SerializableDictionary<string, string>.LoadFromString(Settings["dictMarketRegion"]);
				dictMarketPic = SerializableDictionary<string, string>.LoadFromString(Settings["dictMarketPic"]);
			}

			//设置任务标题
			Info.Title = string.Format("Bing每日壁纸: {0}年{1}月{2}日", now.Year,
				now.Month.ToString("D2"), now.Day.ToString("D2"));

			//下载所有图片
			int i = 1;
			Info.FilePath.Clear();
			foreach (string key in dictMarketPic.Keys)
			{
				//新分段
				NewPart(i++, dictMarketPic.Count);
				//设置文件名 如 C:\Bing-20120123\china.jpg
				var ext = Path.GetExtension(dictMarketPic[key]);
				if (ext != ".png" || ext != ".jpg") ext = ".jpg";
				var filename = "Bing" + "-" + now.Year.ToString() + now.Month.ToString("D2")
					+ now.Day.ToString("D2") + @"\" + dictMarketRegion[key] + ext;
				filename = Path.Combine(Info.SaveDirectory.ToString(), filename);
				Info.FilePath.Add(filename);
				dictMarketFile.Add(key, filename);

				//设置下载参数
				p.FilePath = filename;
				p.Url = dictMarketPic[key];
				p.Proxy = Info.Proxy;

				//下载图片
				DownloadFile();
			}

			//设置壁纸
			if (!Tools.IsRunningOnMono)
			{
				//选择地区
				var regionMkt = ToolForm.CreateSingleSelectForm("请选择需要设为壁纸的图片:", dictMarketRegion, "zh-CN", null, "bing");
				var file = dictMarketFile[regionMkt];
				SystemParametersInfo(20, 0, file, 0x01 | 0x02);
			}

			return true;
		}


	}
}
