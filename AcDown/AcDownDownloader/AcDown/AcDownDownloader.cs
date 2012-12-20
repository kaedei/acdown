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
			string FlvBind_exe = "http://storage.live.com/items/4C4A61560B6A6DB5!6016";
			string FlvLib_dll = "http://storage.live.com/items/4C4A61560B6A6DB5!6017";
			string FFMpeg_exe = "http://storage.live.com/items/4C4A61560B6A6DB5!5795";
			string FFMpeg_exe_1 = "http://download-codeplex.sec.s-msft.com/Download?ProjectName=acdown&DownloadId=579838";
			string FFMpeg_exe_2 = "http://download-codeplex.sec.s-msft.com/Download?ProjectName=acdown&DownloadId=579839";
			string Libfaac_dll = "http://storage.live.com/items/4C4A61560B6A6DB5!5794";

			string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			string folder = Path.Combine(appdata, "Kaedei" + Path.DirectorySeparatorChar + "FlvCombine" + Path.DirectorySeparatorChar);

			Info.Title = "AcDown视频合并插件";

			NewPart(1, 4);
			TipText("正在下载Flv合并组件(1/2)");
			p = new DownloadParameter()
			{
				Url = FlvBind_exe,
				FilePath = Path.Combine(folder, "FlvBind.exe"),
				Proxy = Info.Proxy
			};
			if (!DownloadFile())
				return false;

			NewPart(2, 4);
			TipText("正在下载Flv合并组件(2/2)");
			p = new DownloadParameter()
			{
				Url = FlvLib_dll,
				FilePath = Path.Combine(folder, "FlvLib.dll"),
				Proxy = Info.Proxy
			};
			if (!DownloadFile())
				return false;

			NewPart(3, 4);
			TipText("正在下载高级视频合并组件(1/2)");
			p = new DownloadParameter()
			{
				Url = FFMpeg_exe,
				FilePath = Path.Combine(folder, "ffmpeg.exe"),
				Proxy = Info.Proxy
			};
			if (!DownloadFile())
				return false;

			NewPart(4, 4);
			TipText("正在下载高级视频合并组件(2/2)");
			p = new DownloadParameter()
			{
				Url = Libfaac_dll,
				FilePath = Path.Combine(folder, "libfaac.dll"),
				Proxy = Info.Proxy
			};
			if (!DownloadFile())
				return false;

			return true;
		}
	}
}
