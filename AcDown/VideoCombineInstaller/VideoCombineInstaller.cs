using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Kaedei.AcDown.VideoCombineInstaller
{
	public class VideoCombineInstaller
	{
		static void Main(string[] args)
		{
			try
			{
				string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
				string file_ffmpeg = Path.Combine(appdata, "Kaedei" + Path.DirectorySeparatorChar + "FlvCombine" + Path.DirectorySeparatorChar + "ffmpeg.exe");
				string file_libfaac = Path.Combine(appdata, "Kaedei" + Path.DirectorySeparatorChar + "FlvCombine" + Path.DirectorySeparatorChar + "libfaac.dll");
				string file_flvbind = Path.Combine(appdata, "Kaedei" + Path.DirectorySeparatorChar + "FlvCombine" + Path.DirectorySeparatorChar + "FlvBind.exe");
				string file_flvlib = Path.Combine(appdata, "Kaedei" + Path.DirectorySeparatorChar + "FlvCombine" + Path.DirectorySeparatorChar + "FLVLib.dll");

				Assembly assembly = Assembly.GetExecutingAssembly();
				//释放文件
				using (var stream = assembly.GetManifestResourceStream("Kaedei.AcDown.VideoCombineInstaller.FlvCombine.FlvBind.exe"))
				{
					Extract(stream, file_flvbind);
				}
				using (var stream = assembly.GetManifestResourceStream("Kaedei.AcDown.VideoCombineInstaller.FlvCombine.FLVLib.dll"))
				{
					Extract(stream, file_flvlib);
				}
				using (var stream = assembly.GetManifestResourceStream("Kaedei.AcDown.VideoCombineInstaller.FFMpeg.ffmpeg.exe"))
				{
					Extract(stream, file_ffmpeg);
				}
				using (var stream = assembly.GetManifestResourceStream("Kaedei.AcDown.VideoCombineInstaller.FFMpeg.libfaac.dll"))
				{
					Extract(stream, file_libfaac);
				}

				Console.WriteLine("AcDown视频合并插件安装完毕。按下任意键退出...");
				Console.ReadKey();
			}
			catch
			{
				Console.WriteLine("安装AcDown视频插件时发生了一些错误。请保证当前没有合并任务正在运行中，或尝试使用管理员权限重新启动安装程序。");
				Console.WriteLine("按下任意键退出...");
				Console.ReadKey();
			}
		}

		static void Extract(Stream inputStream,string outputFile)
		{
			//创建文件夹
			if (!Directory.Exists(Path.GetDirectoryName(outputFile)))
				Directory.CreateDirectory(Path.GetDirectoryName(outputFile));
			using (var fs = new FileStream(outputFile, FileMode.Create))
			{
				byte[] bf = new byte[100 * 1024]; //100kb buffer
				while (true)
				{
					int read = inputStream.Read(bf, 0, bf.Length);
					if (read > 0)
					{
						fs.Write(bf, 0, read);
					}
					else
					{
						break;
					}
				}
			}
		}
	}
}
