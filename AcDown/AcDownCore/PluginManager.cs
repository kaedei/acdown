using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;

namespace Kaedei.AcDown.Core
{
	public class PluginManager
	{
		private string _startupPath;
		public PluginManager()
		{
			_startupPath = CoreManager.StartupPath;
		}

		private Collection<IPlugin> _plugins = new Collection<IPlugin>();

		public Collection<IPlugin> Plugins
		{
			get
			{
				return _plugins;
			}
		}
		/// <summary>
		/// 从指定的位置自动加载插件
		/// </summary>
		public void LoadPlugins()
		{
			LoadPlugins(Path.Combine(_startupPath, @"Plugins\"));
		}

		/// <summary>
		/// 加载指定的插件
		/// </summary>
		/// <param name="plugin"></param>
		public void LoadPlugin(IPlugin plugin)
		{
			if (plugin == null)
				return;
			//读取配置
			LoadConfiguration(plugin);
			//读取插件
			_plugins.Add(plugin);

		}

		/// <summary>
		/// 获取指定名称的插件
		/// </summary>
		/// <param name="name">插件名称</param>
		/// <returns></returns>
		public IPlugin GetPlugin(string name)
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
		/// 取得插件配置文件所在的地址
		/// </summary>
		/// <param name="plugin"></param>
		/// <returns></returns>
		private string GetSettingFilePath(IPlugin plugin)
		{
			//反射属性
			object[] types = plugin.GetType().GetCustomAttributes(typeof(AcDownPluginInformationAttribute), true);
			var attrib = (AcDownPluginInformationAttribute)types[0];
			//取得完整文件名
			//例如 %AppPath%\Plugin\Kaedei\AcfunDownloader\settings.xml
			string path = Path.Combine(_startupPath, @"Plugins\");
			path = Path.Combine(path, attrib.Author + @"\" + attrib.Name + @"\" + @"settings.xml");
			return path;
		}

		/// <summary>
		/// 保存插件配置
		/// </summary>
		/// <param name="plugin">需要保存配置的插件引用</param>
		/// <returns>如果保存成功返回true，失败为false</returns>
		public bool SaveConfiguration(IPlugin plugin)
		{
			try
			{
				string path = GetSettingFilePath(plugin);
				//建立文件夹
				if (!Directory.Exists(path)) Directory.CreateDirectory(Path.GetDirectoryName(path));

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

		/// <summary>
		/// 读取插件配置
		/// </summary>
		/// <param name="plugin"></param>
		/// <param name="startupPath"></param>
		private void LoadConfiguration(IPlugin plugin)
		{
			string path = GetSettingFilePath(plugin);
			//如果文件存在则反序列化设置
			if (File.Exists(path))
			{
				//反序列化插件设置
				XmlSerializer s = new XmlSerializer(typeof(SerializableDictionary<string, string>));
				using (FileStream fs = new FileStream(path, FileMode.Open))
				{
					try
					{
						plugin.Configuration = (SerializableDictionary<string, string>)s.Deserialize(fs);
					}
					catch { }
				}
			}
			plugin.Configuration = plugin.Configuration ?? new SerializableDictionary<string, string>();
			//设置启动路径
			plugin.Configuration["StartupPath"] = Path.GetDirectoryName(path);
		}


		/// <summary>
		/// 从指定位置加载插件
		/// </summary>
		/// <param name="appdir"></param>
		private void LoadPlugins(string appdir)
		{
			//Current AcDown Core Version
			//var currentver = Assembly.GetExecutingAssembly().GetName().Version;

			if (!Directory.Exists(appdir))
				return;
			//all plugin assembly
			var acdplugins = new List<string>();

			string[] companies = Directory.GetDirectories(appdir, "*", SearchOption.TopDirectoryOnly);
			foreach (string company in companies) //公司
			{
				string[] plugins = Directory.GetDirectories(company, "*", SearchOption.TopDirectoryOnly);
				foreach (string plugin in plugins) //插件
				{
					string[] dlls = Directory.GetFiles(plugin, "*.dll.acp", SearchOption.TopDirectoryOnly);
					acdplugins.AddRange(dlls);
				}//end foreach plugin in plugins
			}//end foreach company in companies

			//Load All Plugins
			foreach (string file in acdplugins)
			{
				try
				{
					var assembly = Assembly.LoadFrom(file);
					foreach (var t in assembly.GetExportedTypes())
					{
						//if it's inherted from IPlugin
						if (t.IsClass && typeof(IPlugin).IsAssignableFrom(t))
						{
							//and have a AcDownPluginInformation attribute
							if (t.GetCustomAttributes(typeof(AcDownPluginInformationAttribute), false).Length > 0)
							{
								//load the plugin
								var p = (IPlugin)Activator.CreateInstance(t);
								//Load Plugin Configuration
								LoadConfiguration(p);
								//add the plugin to Collection
								_plugins.Add(p);
							}
						}
					}
				}
				catch (Exception ex)
				{
					//log this exception
					Logging.Add(ex);
				}
			}//end load plugins
		}//end private void LoadPlugins


	}
}
