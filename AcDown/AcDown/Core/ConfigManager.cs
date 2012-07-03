using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Xml.Serialization;
using System.IO;

namespace Kaedei.AcDown.Core
{

	public class ConfigManager
	{
		public string _pluginsFolderPath;
		public ConfigManager(string pluginsFolderPath)
		{
			_pluginsFolderPath = pluginsFolderPath;
		}

		public AcDownSettings Settings
		{
			get
			{
				return Config.setting;
			}
		}

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
				formatter.Serialize(fs, Config.setting);
			}
			GlobalSettings.GetSettings().CacheSize = Config.setting.CacheSize;
			GlobalSettings.GetSettings().NetworkTimeout = Config.setting.NetworkTimeout;
			GlobalSettings.GetSettings().RetryTimes = Config.setting.RetryTimes;
			GlobalSettings.GetSettings().RetryWaitingTime = Config.setting.RetryWaitingTime;
			GlobalSettings.GetSettings().ToolFormTimeout = Config.setting.ToolFormTimeout;
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
					Config.setting = s;
				else
					throw new Exception();
			}
			catch
			{
				Config.setting = new AcDownSettings();
				SaveSettings();
			}
			finally
			{
				GlobalSettings.GetSettings().NetworkTimeout = Config.setting.NetworkTimeout;
				GlobalSettings.GetSettings().RetryTimes = Config.setting.RetryTimes;
				GlobalSettings.GetSettings().RetryWaitingTime = Config.setting.RetryWaitingTime;
				GlobalSettings.GetSettings().ToolFormTimeout = Config.setting.ToolFormTimeout;
			}
			return Config.setting;
		}
	}
}
