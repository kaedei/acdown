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
			LoadPlugins(Path.Combine(CoreManager.StartupPath, "Plugins" + Path.DirectorySeparatorChar));
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
				try
				{
					var attrib = GetAttr(item);
					if (attrib.Name == name)
						return item;
				}
				catch { }
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
			//取得插件属性
			var attrib = GetAttr(plugin);
			//取得完整文件名
			//例如 %AppPath%\Plugin\Kaedei\AcfunDownloader\settings.xml
			string path = Path.Combine(CoreManager.StartupPath, "Plugins" + Path.DirectorySeparatorChar);
			path = Path.Combine(path, attrib.Author + Path.DirectorySeparatorChar + attrib.Name + Path.DirectorySeparatorChar + @"settings.xml");
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
					//搜索此文件夹下与此文件夹同名的.acp文件，如 ABCDownloader\ABCDownloader.acp
					string[] dlls = Directory.GetFiles(plugin, Path.GetFileName(plugin) + ".acp", SearchOption.TopDirectoryOnly);
					//删除已经被卸载的插件
					if (File.Exists(Path.Combine(plugin, "uninstall")))
					{
						Directory.Delete(plugin, true);
					}
					else
					{
						acdplugins.AddRange(dlls);
					}
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

		/// <summary>
		/// 安装插件
		/// </summary>
		/// <param name="pluginFile">插件Dll文件完整路径</param>
		/// <exception cref="FileNotFoundException" />
		/// <exception cref="PluginFileNotSupportedException" />
		/// <returns>已加载类型的AcDownPluginInformationAttribute属性</returns>
		public static AcDownPluginInformationAttribute InstallPlugin(string pluginFile)
		{
			//检查文件是否存在
			if (!File.Exists(pluginFile))
				throw new FileNotFoundException("File not found", pluginFile);
			//检查文件是否是有效的插件
			AcDownPluginInformationAttribute pluginInfo = null;
			var assembly = Assembly.LoadFrom(pluginFile);
			foreach (var t in assembly.GetExportedTypes())
			{
				//检查t类型是否实现了IPlugin
				if (t.IsClass && typeof(IPlugin).IsAssignableFrom(t))
				{
					//检查t类型是否有AcDownPluginInformationAttribute属性
					var attributes = t.GetCustomAttributes(typeof(AcDownPluginInformationAttribute), false);
					if (attributes.Length > 0)
					{
						pluginInfo = attributes[0] as AcDownPluginInformationAttribute;
						break;
					}
				}
			}

			//整个文件中都无法找到合适的插件类型
			if (pluginInfo == null)
				throw new PluginFileNotSupportedException();

			//复制插件文件
			string destinationFile = Path.Combine(Path.Combine(CoreManager.StartupPath, "Plugins"), pluginInfo.Author);
			destinationFile = Path.Combine(destinationFile, pluginInfo.Name);
			//建立目标文件夹
			if (!Directory.Exists(destinationFile))
				Directory.CreateDirectory(destinationFile);
			//目标文件。完整路径为 \[Author]\[PluginName]\[PluginName].acp
			destinationFile = Path.Combine(destinationFile, pluginInfo.Name + ".acp");
			File.Copy(pluginFile, destinationFile, true);

			return pluginInfo;
		}

		/// <summary>
		/// 卸载指定的插件。插件将会在下一次AcDown启动时删除
		/// </summary>
		/// <param name="plugin">插件引用</param>
		public void UninstallPlugin(IPlugin plugin)
		{
			var attrib = GetAttr(plugin);
			//取得完整文件名
			//例如 %AppPath%\Plugin\Kaedei\AcfunDownloader\uninstall
			string path = Path.Combine(CoreManager.StartupPath, @"Plugins" + Path.DirectorySeparatorChar);
			path = Path.Combine(path, attrib.Author + Path.DirectorySeparatorChar +
				attrib.Name + Path.DirectorySeparatorChar + "uninstall");
			//创建标识文件，下次启动acdown时会删除这个插件
			File.WriteAllText(path, attrib.Version.ToString());
		}

		/// <summary>
		/// 获取指定的插件是否将会在下次启动时被卸载
		/// </summary>
		/// <param name="plugin"></param>
		/// <returns></returns>
		public bool IsPluginWillBeUninstalled(IPlugin plugin)
		{
			if (File.Exists(Path.Combine(Path.GetDirectoryName(GetSettingFilePath(plugin)), "uninstall")))
				return true;
			else
				return false;
		}

		/// <summary>
		/// 判断指定的插件是否是内部插件
		/// </summary>
		/// <param name="plugin"></param>
		/// <returns></returns>
		public bool IsInnerPlugin(IPlugin plugin)
		{
			if (File.Exists(Path.Combine(Path.GetDirectoryName(GetSettingFilePath(plugin)), GetAttr(plugin).Name + ".acp")))
				return false;
			else
				return true;
		}

		/// <summary>
		/// 取得指定插件的AcDownPluginInformationAttribute属性
		/// </summary>
		/// <param name="plugin">插件引用</param>
		/// <exception cref="AcDownAttributeNotImplementedException" />
		/// <returns></returns>
		public AcDownPluginInformationAttribute GetAttr(IPlugin plugin)
		{
			try
			{
				object[] types = plugin.GetType().GetCustomAttributes(typeof(AcDownPluginInformationAttribute), true);
				var attrib = (AcDownPluginInformationAttribute)types[0];
				return attrib;
			}
			catch
			{
				throw new AcDownAttributeNotImplementedException(plugin);
			}
		}
	}

	[Serializable]
	public class PluginFileNotSupportedException : Exception
	{

	}

	[Serializable]
	public class AcDownAttributeNotImplementedException : Exception
	{
		public AcDownAttributeNotImplementedException(IPlugin plugin) { Plugin = plugin; }
		public IPlugin Plugin { get; set; }
	}
}
