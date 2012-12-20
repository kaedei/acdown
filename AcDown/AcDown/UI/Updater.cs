using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;
using Kaedei.AcDown.Core;
using System.Windows.Forms;
using System.Threading;

namespace Kaedei.AcDown.UI
{
	/// <summary>
	/// 控制应用程序升级过程的类
	/// </summary>
	public static class Updater
	{
		/// <summary>
		/// 检查程序是否有最新更新
		/// </summary>
		/// <returns></returns>
		public static UpdateInformation CheckUpdate()
		{
			try
			{
				string url = CoreManager.ConfigManager.Settings.CheckUpdateDocument;
				if (url == "stable")
					url = "http://acdown.codeplex.com/wikipage?title=AutoUpdate";
				if (url == "develop")
					url = "http://blog.sina.com.cn/s/blog_58c506600100ylrt.html";

				string src = Network.GetHtmlSource(url, Encoding.UTF8);
				Regex rVersion = new Regex(@"{updatestart}NEWVERSION=(?<major>\d+)\.(?<minor>\d+).(?<build>\d+)\.(?<revision>\d+){updateend}");
				Match mVersion = rVersion.Match(src);
				string verstring = mVersion.Groups["major"].ToString() + "." +
										mVersion.Groups["minor"].ToString() + "." +
										mVersion.Groups["build"].ToString() + "." +
										mVersion.Groups["revision"].ToString();
				Version newVersion = new Version(verstring);
				Regex rUrl = new Regex(@"{urlstart}URL=(?<url>.+?){urlend}");
				Match mUrl = rUrl.Match(src);
				string u = mUrl.Groups["url"].Value.Replace("&amp;", "&");
				return new UpdateInformation() { NewVersion = newVersion, Url = u };
			}
			catch
			{
				return new UpdateInformation() { NewVersion = new Version("0.0.0.1"), Url = "" };
			}

		}

		/// <summary>
		/// 下载最新更新至临时文件夹
		/// </summary>
		/// <returns>新文件的完整路径</returns>
		public static string DownloadUpdate(UpdateInformation updateInfo)
		{
			try
			{
				string filename = Path.Combine(
						CoreManager.StartupPath,
						"Update" + Path.DirectorySeparatorChar + "AcDown" + updateInfo.NewVersion.ToString() + ".exe");
				//下载文件
				Network.DownloadFile(new DownloadParameter()
				{
					Url = updateInfo.Url,
					FilePath = filename,
					Referer = @"http://acdown.codeplex.com/wikipage?title=AutoUpdate&referringTitle=For%20Developer",
					UserAgent = @"Mozilla/5.0 (Windows NT 6.1; rv:16.0) Gecko/20100101 Firefox/16.0"
				});
				return filename;
			}
			catch
			{
				return "";
			}
		}

		/// <summary>
		/// 取得一个值，指示当前程序是否正在更新过程中（运行在StartupPath文件夹中）
		/// </summary>
		/// <param name="path">程序的映像路径</param>
		/// <returns></returns>
		public static bool CheckIfUpdating(string path)
		{
			//兼容3.7-3.12旧版本
			if (path.ToUpper().StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData).ToUpper()))
				return true;
			if (path.ToUpper().StartsWith(CoreManager.StartupPath.ToUpper()))
				return true;
			else
				return false;
		}

		/// <summary>
		/// 拷贝自身覆盖指定的文件
		/// </summary>
		/// <param name="filePath">覆盖到的文件完整路径</param>
		public static bool CopyTempFileToTargetFile(string filePath)
		{
			for (int i = 0; i < 5; i++) //因为上一个实例可能并未退出所以尝试多次
			{
				try
				{
					string file = filePath.Replace("\"", "");
					//去除目标文件的各种属性
					File.SetAttributes(filePath, FileAttributes.Normal);
					//拷贝自身并覆盖目标文件
					File.Copy(Application.ExecutablePath, file, true);
					return true;
				}
				catch
				{
					Thread.Sleep(2000);
				}
			}
			return false;
		}

		/// <summary>
		/// 删除临时文件
		/// </summary>
		public static void DeleteTempFile()
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback((o) =>
				{
					DeleteTempFileAsync();
				}));
		}

		private static void DeleteTempFileAsync()
		{
			string updateFolder = Path.Combine(CoreManager.StartupPath, "Update");
			for (int i = 0; i < 5; i++)
			{
				if (Directory.Exists(updateFolder))
				{
					try
					{
						Directory.Delete(updateFolder, true);
						return;
					}
					catch
					{
						Thread.Sleep(1000);
					}
				}
				else
				{
					return;
				}
			}

		}
	}


	[Serializable]
	public class UpdateInformation
	{
		public Version NewVersion;
		public string Url;
	}
}
