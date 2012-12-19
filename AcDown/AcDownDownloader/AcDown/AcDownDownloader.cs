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
			string FlvBind_exe = "http://download-codeplex.sec.s-msft.com/Download?ProjectName=acdown&DownloadId=367557";
			string FlvLib_dll = "http://download-codeplex.sec.s-msft.com/Download?ProjectName=acdown&DownloadId=367558";
			string FFMpeg_exe_1 = "http://download-codeplex.sec.s-msft.com/Download?ProjectName=acdown&DownloadId=579838";
			string FFMpeg_exe_2 = "http://download-codeplex.sec.s-msft.com/Download?ProjectName=acdown&DownloadId=579839";
			string Libfaac_dll = "http://download-codeplex.sec.s-msft.com/Download?ProjectName=acdown&DownloadId=579840";

			string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			string folder = Path.Combine(appdata, "Kaedei" + Path.DirectorySeparatorChar + "FlvCombine" + Path.DirectorySeparatorChar);

			Info.Title = "AcDown视频合并插件";

			NewPart(1, 5);
			TipText("正在下载Flv合并组件(1/2)");
			p = new DownloadParameter()
			{
				Url = FlvBind_exe,
				FilePath = Path.Combine(folder, "FlvBind.exe"),
				Proxy = Info.Proxy,
				Referer = "http://acdown.codeplex.com"
			};
			DownloadFile();

			NewPart(2, 5);
			TipText("正在下载Flv合并组件(2/2)");
			p = new DownloadParameter()
			{
				Url = FlvLib_dll,
				FilePath = Path.Combine(folder, "FlvLib.dll"),
				Proxy = Info.Proxy,
				Referer = "http://acdown.codeplex.com"
			};
			DownloadFile();

			NewPart(3, 5);
			TipText("正在下载高级视频合并组件(1/3)");
			p = new DownloadParameter()
			{
				Url = FFMpeg_exe_1,
				FilePath = Path.Combine(folder, "ffmpeg.exe.part01"),
				Proxy = Info.Proxy,
				Referer = "http://acdown.codeplex.com"
			};
			DownloadFile();

			NewPart(4, 5);
			TipText("正在下载高级视频合并组件(2/3)");
			p = new DownloadParameter()
			{
				Url = FFMpeg_exe_2,
				FilePath = Path.Combine(folder, "ffmpeg.exe.part02"),
				Proxy = Info.Proxy,
				Referer = "http://acdown.codeplex.com"
			};
			DownloadFile();

			TipText("正在合并FFMPEG.EXE");
			var part1 = File.ReadAllBytes(Path.Combine(folder, "ffmpeg.exe.part01"));
			var part2 = File.ReadAllBytes(Path.Combine(folder, "ffmpeg.exe.part02"));
			File.Open(Path.Combine(folder, "ffmpeg.exe"), FileMode.Open).Write(part1, 0, part1.Length);
			File.Open(Path.Combine(folder, "ffmpeg.exe"), FileMode.Append).Write(part2, 0, part2.Length);

			File.Delete(Path.Combine(folder, "ffmpeg.exe.part01"));
			File.Delete(Path.Combine(folder, "ffmpeg.exe.part02"));

			NewPart(5, 5);
			TipText("正在下载高级视频合并组件(3/3)");
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
