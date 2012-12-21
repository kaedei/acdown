using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using Kaedei.AcDown.Core;
using System.IO;
using Kaedei.AcDown.UI.Components;

namespace Kaedei.AcDown.UI
{
	public class FirstrunHandler
	{
		private string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Kaedei" + Path.DirectorySeparatorChar + "AcDown" + Path.DirectorySeparatorChar + "firstrun");
		/// <summary>
		/// 判断此版本的程序是否是第一次运行
		/// </summary>
		public bool IsFirstRun()
		{
			if (Interface.Tools.IsRunningOnMono)
				return false;
			try
			{
				if (File.Exists(file))
				{
					string content = File.ReadAllText(file, Encoding.UTF8);
					var ver = new Version(content);
					if (ver.CompareTo(new Version(Application.ProductVersion)) >= 0)
					{
						return false;
					}
				}
			}
			catch { }
			return true;
		}

		/// <summary>
		/// 执行首次启动所需要做的操作
		/// </summary>
		/// <returns></returns>
		public void RunFirstRun()
		{
			//释放AcPlay文件
			try
			{
				var acplayhelper = new AcPlay.AcPlayHelper();
				acplayhelper.ReleaseAcPlayFile();
			}
			catch { }

			try
			{
				//注册.acplay关联
				AssociateRegistrar.CreateAssociate(
					 Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
					 "Kaedei" + Path.DirectorySeparatorChar + "AcPlay" + Path.DirectorySeparatorChar + "acplay.exe"),
					 ".acplay", "AcPlayFile", "弹幕播放快捷方式", "");
				//注册.acp关联
				AssociateRegistrar.CreateAssociate(Application.ExecutablePath,
					 ".acp", "AcDownPlugin", "AcDown插件", "");
			}
			catch { }

			//写入Firstrun文件
			try
			{
				if (File.Exists(file))
				{
					File.SetAttributes(file, FileAttributes.Normal);
					File.Delete(file);
				}
				File.WriteAllText(file, Application.ProductVersion.ToString(), Encoding.UTF8);
			}
			catch { }

		}


		public void DeleteFirstRunFile()
		{
			if (File.Exists(file))
			{
				try
				{
					FileInfo fi = new FileInfo(file);
					fi.Attributes = FileAttributes.Normal;
					File.Delete(file);
				}
				catch { }
			}
		}
	}
}
