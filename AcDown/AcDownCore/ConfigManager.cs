using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Xml.Serialization;
using System.IO;

namespace Kaedei.AcDown.Core
{
	/// <summary>
	/// 配置管理器
	/// </summary>
	public class ConfigManager
	{
		private string _pluginsFolderPath;

		/// <summary>
		/// 初始化配置管理器的实例
		/// </summary>
		/// <param name="pluginsFolderPath"></param>
		public ConfigManager()
		{
			_pluginsFolderPath = CoreManager.StartupPath;
		}

		public AcDownSettings Settings { get; set; }

		/// <summary>
		/// 保存设置
		/// </summary>
		public void SaveSettings()
		{
			if (!Directory.Exists(_pluginsFolderPath))
			{
				//如果目录不存在则创建
				Directory.CreateDirectory(_pluginsFolderPath);
			}

			//序列化设置
			using (FileStream fs = new FileStream(Path.Combine(_pluginsFolderPath, "config.xml"), FileMode.Create))
			{
				XmlSerializer formatter = new XmlSerializer(typeof(AcDownSettings));
				formatter.Serialize(fs, CoreManager.ConfigManager.Settings);
			}
			GlobalSettings.GetSettings().CacheSize = CoreManager.ConfigManager.Settings.CacheSize;
			GlobalSettings.GetSettings().NetworkTimeout = CoreManager.ConfigManager.Settings.NetworkTimeout;
			GlobalSettings.GetSettings().RetryTimes = CoreManager.ConfigManager.Settings.RetryTimes;
			GlobalSettings.GetSettings().RetryWaitingTime = CoreManager.ConfigManager.Settings.RetryWaitingTime;
			GlobalSettings.GetSettings().ToolFormTimeout = CoreManager.ConfigManager.Settings.ToolFormTimeout;
		}

		/// <summary>
		/// 读取设置
		/// </summary>
		/// <returns></returns>
		public AcDownSettings LoadSettings()
		{
			try
			{
				AcDownSettings s;
				string path = Path.Combine(_pluginsFolderPath, "config.xml");
				using (FileStream fs = new FileStream(path, FileMode.Open))
				{
					XmlSerializer formatter = new XmlSerializer(typeof(AcDownSettings));
					s = (AcDownSettings)formatter.Deserialize(fs);
				}

				if (s != null)
					CoreManager.ConfigManager.Settings = s;
				else
					throw new Exception();
			}
			catch
			{
				CoreManager.ConfigManager.Settings = new AcDownSettings();
				SaveSettings();
			}
			finally
			{
				GlobalSettings.GetSettings().CacheSize = CoreManager.ConfigManager.Settings.CacheSize;
				GlobalSettings.GetSettings().NetworkTimeout = CoreManager.ConfigManager.Settings.NetworkTimeout;
				GlobalSettings.GetSettings().RetryTimes = CoreManager.ConfigManager.Settings.RetryTimes;
				GlobalSettings.GetSettings().RetryWaitingTime = CoreManager.ConfigManager.Settings.RetryWaitingTime;
				GlobalSettings.GetSettings().ToolFormTimeout = CoreManager.ConfigManager.Settings.ToolFormTimeout;
			}
			return CoreManager.ConfigManager.Settings;
		}
	}
}
