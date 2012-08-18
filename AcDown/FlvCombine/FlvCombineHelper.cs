using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace Kaedei.AcDown.UI.Components
{
	public class FlvCombineHelper
	{
		/// <summary>
		/// 释放Flv合并组件
		/// </summary>
		public void ReleaseFlvCombineFile()
		{
			string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			string file_flvbind = Path.Combine(appdata, @"Kaedei\AcDown\UIComponents\FlvCombine\FlvBind.exe");
			string file_flvlib = Path.Combine(appdata, @"Kaedei\AcDown\UIComponents\FlvCombine\FLVLib.dll");

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
	}
}
