using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using Kaedei.AcDown.Core;

namespace Kaedei.AcDown.UI.Components
{
	public class VideoCombineHelper
	{
		/// <summary>
		/// 释放Flv合并组件
		/// </summary>
		public void ReleaseFlvCombineFile()
		{
			return;
			string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			string file_flvbind = Path.Combine(appdata, "Kaedei" + Path.DirectorySeparatorChar + "FlvCombine" + Path.DirectorySeparatorChar + "FlvBind.exe");
			string file_flvlib = Path.Combine(appdata, "Kaedei" + Path.DirectorySeparatorChar + "FlvCombine" + Path.DirectorySeparatorChar + "FLVLib.dll");

			Assembly assembly = GetType().Assembly;
			//释放FlvBind.exe文件
			var stream = assembly.GetManifestResourceStream("Kaedei.AcDown.UI.Components.FlvCombine.FlvBind.exe");

			//创建文件夹
			if (!Directory.Exists(Path.GetDirectoryName(file_flvbind)))
				Directory.CreateDirectory(Path.GetDirectoryName(file_flvbind));
			using (var fs = new FileStream(file_flvbind, FileMode.Create))
			{
				byte[] bf = new byte[100 * 1024]; //100kb buffer
				while (true)
				{
					int read = stream.Read(bf, 0, bf.Length);
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


			stream = assembly.GetManifestResourceStream("Kaedei.AcDown.UI.Components.FlvCombine.FLVLib.dll");

			//创建文件夹
			if (!Directory.Exists(Path.GetDirectoryName(file_flvlib)))
				Directory.CreateDirectory(Path.GetDirectoryName(file_flvlib));
			using (var fs = new FileStream(file_flvlib, FileMode.Create))
			{
				byte[] bf = new byte[100 * 1024]; //100kb buffer
				while (true)
				{
					int read = stream.Read(bf, 0, bf.Length);
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

		/// <summary>
		/// 检查视频合并所需文件是否都存在
		/// </summary>
		/// <returns></returns>
		public static bool CheckFileExists()
		{
			string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
				, "Kaedei" + Path.DirectorySeparatorChar + "FlvCombine" + Path.DirectorySeparatorChar);

			return File.Exists(Path.Combine(folder, "FlvBind.exe")) &&
				File.Exists(Path.Combine(folder, "FlvLib.dll")) &&
				File.Exists(Path.Combine(folder, "ffmpeg.exe")) &&
				File.Exists(Path.Combine(folder, "libfaac.dll"));
		}

		/// <summary>
		/// 合并视频
		/// </summary>
		/// <param name="inputFile">输入文件</param>
		/// <param name="outputFile">输出文件</param>
		/// <returns></returns>
		public bool Combine(string[] inputFile, string outputFile, Action<int> progress)
		{
			progress = progress ?? new Action<int>((o) => { });
			string exe = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
				, "Kaedei" + Path.DirectorySeparatorChar + "FlvCombine" + Path.DirectorySeparatorChar + "ffmpeg.exe");

			var tempfiles = new List<string>(inputFile.Length);
			int pg = 0; //当前进度
			progress(pg);
			//转码
			foreach (var file in inputFile)
			{
				pg += (30 / inputFile.Length / 2);
				var tempfile = file + ".actemp";
				tempfiles.Add(tempfile);
				//生成ProcessStartInfo
				ProcessStartInfo pinfo = new ProcessStartInfo(exe);
				//设置参数
				pinfo.Arguments = String.Format(CoreManager.ConfigManager.Settings.EncodeArg, "\"" + file + "\"", "\"" + tempfile + "\"");
				//隐藏窗口
				pinfo.WindowStyle = ProcessWindowStyle.Hidden;
				//启动程序
				Process p = Process.Start(pinfo);
				p.WaitForExit();

				pg += (30 / inputFile.Length / 2);
				progress(pg);
			}

			progress(30);

			//合并
			var outputtemp = outputFile + ".actemp";
			using (var fsOutput = new FileStream(outputtemp, FileMode.Create))
			{
				foreach (var tempfile in tempfiles)
				{
					pg += (30 / tempfiles.Count / 2);
					using (var fsInput = new FileStream(tempfile, FileMode.Open))
					{
						var buffer = new byte[10 * 1024 * 1024]; //10MB Buffer
						do
						{
							int read = fsInput.Read(buffer, 0, buffer.Length);
							if (read > 0)
								fsOutput.Write(buffer, 0, read);
							else
								break;
						} while (true);
					}
					pg += (30 / tempfiles.Count / 2);
					progress(pg);
				}
			}

			progress(60);

			//转码
			{
				//生成ProcessStartInfo
				ProcessStartInfo pinfo = new ProcessStartInfo(exe);
				//设置参数
				pinfo.Arguments = String.Format(CoreManager.ConfigManager.Settings.CombineArg, "\"" + outputtemp + "\"", "\"" + outputFile + "\"");
				//隐藏窗口
				pinfo.WindowStyle = ProcessWindowStyle.Hidden;
				//启动程序
				Process p = Process.Start(pinfo);
				p.WaitForExit();
			}

			progress(90);

			//删除临时文件
			try
			{
				File.Delete(outputtemp);
			}
			catch { }
			foreach (var tempfile in tempfiles)
			{
				try
				{
					File.Delete(tempfile);
				}
				catch { }
			}


			progress(100);
			return true;
		}


	}
}
