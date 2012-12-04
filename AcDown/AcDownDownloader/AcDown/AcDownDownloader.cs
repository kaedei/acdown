using Kaedei.AcDown.Interface;
using Kaedei.AcDown.Interface.Downloader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kaedei.AcDown.Downloader.AcDown
{
	public class AcDownDownloader : CommonDownloader
	{
		public override bool Download()
		{
			string FlvBind_exe = "https://acdown.svn.codeplex.com/svn/AcDown/FlvCombine/FlvCombine/FlvBind.exe";
			string FlvLib_dll = "https://acdown.svn.codeplex.com/svn/AcDown/FlvCombine/FlvCombine/FLVLib.dll";
			string FFMpeg_exe = "";
			string Libfaac_dll = "";

			string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			string folder = Path.Combine(appdata, "Kaedei" + Path.DirectorySeparatorChar + "FlvCombine" + Path.DirectorySeparatorChar);

			Info.Title = "AcDown UI组件";

			NewPart(1, 4);
			TipText("正在下载Flv合并组件(1/2)");
			p = new DownloadParameter()
			{
				 Url = FlvBind_exe,
				 FilePath = Path.Combine(folder,"FlvBind.exe"),
				 Proxy = Info.Proxy,
				 Referer = "http://acdown.codeplex.com"
			};
			DownloadFile();

			NewPart(2, 4);
			TipText("正在下载Flv合并组件(2/2)");
			p = new DownloadParameter()
			{
				Url = FlvLib_dll,
				FilePath = Path.Combine(folder, "FlvLib.dll"),
				Proxy = Info.Proxy,
				Referer = "http://acdown.codeplex.com"
			};
			DownloadFile();

			NewPart(3, 4);
			TipText("正在下载高级视频合并组件(1/2)");
			p = new DownloadParameter()
			{
				Url = FFMpeg_exe,
				FilePath = Path.Combine(folder, "ffmpeg.exe"),
				Proxy = Info.Proxy,
				Referer = "http://acdown.codeplex.com"
			};
			DownloadFile();

			NewPart(4, 4);
			TipText("正在下载高级视频合并组件(2/2)");
			p = new DownloadParameter()
			{
				Url = Libfaac_dll,
				FilePath = Path.Combine(folder, "libfaac.dll"),
				Proxy = Info.Proxy,
				Referer = "http://acdown.codeplex.com"
			};
			DownloadFile();

			return true;
		}
	}
}
