using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;
using Kaedei.AcDown.Core;

namespace Kaedei.AcDown.UI
{
	/// <summary>
	/// 控制应用程序升级过程的类
	/// </summary>
	public class Updater
	{
		private string tempFileInCommonAppData = "";

		/// <summary>
		/// 临时文件路径
		/// </summary>
		public string TempFileInCommonAppData
		{
			get { return tempFileInCommonAppData; }
		}

		private string tempFileInUserAppData = "";
		/// <summary>
		/// 临时文件路径2(兼容旧版本)
		/// </summary>
		public string TempFileInUserAppData
		{
			get { return tempFileInUserAppData; }
		}

		public Updater()
		{
			//取得临时文件的路径
			var c = System.IO.Path.DirectorySeparatorChar;
			tempFileInCommonAppData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
			tempFileInCommonAppData = Path.Combine(tempFileInCommonAppData, "Kaedei" + c + "AcDown" + c + "Update" + c + "AcDown.exe");
			tempFileInUserAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			tempFileInUserAppData = Path.Combine(tempFileInUserAppData, @"Kaedei" + c + "AcDown" + c + "Update" + c + "AcDown.exe");
		}

		/// <summary>
		/// 检查程序是否有最新更新
		/// </summary>
		/// <returns></returns>
		public string CheckUpdate(Version oldVersion)
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
				if (newVersion > oldVersion)
				{
					Regex rUrl = new Regex(@"{urlstart}URL=(?<url>.+?){urlend}");
					Match mUrl = rUrl.Match(src);
					if (mUrl.Success)
					{
						string u = mUrl.Groups["url"].Value.Replace("&amp;", "&");
						return u;
					}
				}
			}
			catch
			{ }
			return "";

		}

		/// <summary>
		/// 下载最新更新至临时文件夹
		/// </summary>
		/// <param name="url">更新所在的Url</param>
		/// <returns></returns>
		public bool DownloadUpdate(string url)
		{
			try
			{
				//下载文件
				bool s = Network.DownloadFile(new DownloadParameter()
				{
					Url = url,
					FilePath = tempFileInUserAppData
				});
				return s;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// 取得一个值，指示当前程序是否正在更新过程中（运行在临时文件夹中）
		/// </summary>
		/// <param name="path">程序的映像路径</param>
		/// <returns></returns>
		public bool CheckIfUpdating(string path)
		{
			if (path.ToUpper() == tempFileInCommonAppData.ToUpper())
				return true;
			if (path.ToUpper() == tempFileInUserAppData.ToUpper())
				return true;
			return false;
		}

		/// <summary>
		/// 将临时文件覆盖指定的文件
		/// </summary>
		/// <param name="filePath">覆盖到的文件完整路径</param>
		public void CopyTempFileToTargetFile(string filePath)
		{
			string file = filePath.Replace("\"", "");
			//去除目标文件的各种属性
			File.SetAttributes(filePath, FileAttributes.Normal);
			//拷贝并覆盖同名文件
			if (File.Exists(tempFileInUserAppData))
				File.Copy(tempFileInUserAppData, file, true);
			//拷贝并覆盖同名文件
			if (File.Exists(tempFileInCommonAppData))
				File.Copy(tempFileInCommonAppData, file, true);
		}

		/// <summary>
		/// 删除临时文件
		/// </summary>
		public void DeleteTempFile()
		{
			try
			{
				if (File.Exists(tempFileInCommonAppData))
				{
					File.SetAttributes(tempFileInCommonAppData, FileAttributes.Normal);
					File.Delete(tempFileInCommonAppData);
				}
				if (File.Exists(tempFileInUserAppData))
				{
					File.SetAttributes(tempFileInUserAppData, FileAttributes.Normal);
					File.Delete(tempFileInUserAppData);
				}
			}
			catch { }
		}

	}
}
