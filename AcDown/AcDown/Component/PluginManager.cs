using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Collections.ObjectModel;
using Kaedei.AcDown.Downloader;
using System.IO;
using System.Xml.Serialization;

namespace Kaedei.AcDown.Component
{
	public class PluginManager
	{
		private Collection<IAcdownPluginInfo> _plugins = new Collection<IAcdownPluginInfo>();

		public Collection<IAcdownPluginInfo > Plugins
		{
			get
			{
				return _plugins;
			}
		}
		/// <summary>
		/// 读取插件
		/// </summary>
		public void LoadPlugins()
		{
			//AddPlugins
			_plugins.Add(new YoukuPlugin());
			_plugins.Add(new YouTubePlugin());
			_plugins.Add(new AcFunPlugin());
			_plugins.Add(new BilibiliPlugin());
			_plugins.Add(new TudouPlugin());
			_plugins.Add(new ImanhuaPlugin());
			_plugins.Add(new TiebaAlbumPlugin());
			_plugins.Add(new SfAcgPlugin());
			_plugins.Add(new TucaoPlugin());
			_plugins.Add(new FlvcdPlugin());
			//load configuration
			foreach (var item in _plugins)
			{
				LoadConfiguration(item);
			}
		}

		public IAcdownPluginInfo GetPlugin(string name)
		{
			foreach (var item in _plugins)
			{
				object[] types = item.GetType().GetCustomAttributes(typeof(AcDownPluginInformationAttribute), true);
				if (types.Length > 0)
				{
					var attrib = (AcDownPluginInformationAttribute)types[0];
					if (attrib.Name == name)
						return item;
				}
			}
			return null;
		}

		/// <summary>
		/// 保存插件配置
		/// </summary>
		/// <param name="plugin">需要保存配置的插件引用</param>
		/// <returns>如果保存成功返回true，失败为false</returns>
		public bool SaveConfiguration(IAcdownPluginInfo plugin)
		{
			try
			{
				//取得APPDATA路径名称
				string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
				path = Path.Combine(path, @"Kaedei\AcDown\Plugins\");
				//建立文件夹
				if (!Directory.Exists(path)) Directory.CreateDirectory(path);
				//反射属性
				object[] types = plugin.GetType().GetCustomAttributes(typeof(AcDownPluginInformationAttribute), true);
				var attrib = (AcDownPluginInformationAttribute)types[0];
				//取得完整文件名
				path = Path.Combine(path, attrib.Name + ".xml"); //%appdata%\acdown\plugins\acfundownloader.xml

				//反序列化插件设置
				XmlSerializer s = new XmlSerializer(typeof(SerializableDictionary<string, string>));
				using (FileStream fs = new FileStream(path, FileMode.Create))
				{
					s.Serialize(fs, plugin.Configuration);
				}
				return true;
			}
			catch
			{
				return false;
			}

		}

		private void LoadConfiguration(IAcdownPluginInfo plugin)
		{
			try
			{
				//取得APPDATA路径名称
				string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
				path = Path.Combine(path, @"Kaedei\AcDown\Plugins\");

				//反射属性
				object[] types = plugin.GetType().GetCustomAttributes(typeof(AcDownPluginInformationAttribute), true);
				var attrib = (AcDownPluginInformationAttribute)types[0];
				//取得完整文件名
				path = Path.Combine(path, attrib.Name + ".xml"); //%appdata%\acdown\plugins\acfundownloader.xml

				//反序列化插件设置
				XmlSerializer s = new XmlSerializer(typeof(SerializableDictionary<string, string>));
				using (FileStream fs = new FileStream(path, FileMode.Open))
				{
					plugin.Configuration = (SerializableDictionary<string, string>)s.Deserialize(fs);
				}
			}
			catch
			{
			}
		}

	}
}
