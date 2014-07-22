using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace Kaedei.AcPlay
{
	public class AcPlayHelper
	{
		/// <summary>
		/// 释放AcPlay.exe文件
		/// </summary>
		public void ReleaseAcPlayFile()
		{
			//释放AcPlay.exe文件
			string exeFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Kaedei" + Path.DirectorySeparatorChar + "AcPlay" + Path.DirectorySeparatorChar + "acplay.exe");
			Assembly assembly = GetType().Assembly;
			var stream = assembly.GetManifestResourceStream("Kaedei.AcPlay.acplay.exe");

			//创建文件夹
			if (!Directory.Exists(Path.GetDirectoryName(exeFile)))
				Directory.CreateDirectory(Path.GetDirectoryName(exeFile));

			//去除已有文件的只读属性
			if (File.Exists(exeFile))
			{
				try
				{
					File.SetAttributes(exeFile, FileAttributes.Normal);
				}
				catch { }
			}

			//删除位于CommonAppData目录的旧的Acplay.exe文件
			try
			{
				string oldfile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Kaedei" + Path.DirectorySeparatorChar + "acplay.exe");
				if (File.Exists(oldfile))
				{
					File.SetAttributes(oldfile, FileAttributes.Normal);
					File.Delete(oldfile);
				}
			}
			catch { }

			//释放文件
			using (var fs = new FileStream(exeFile, FileMode.Create))
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
	}
}
