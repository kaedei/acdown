using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Collections.ObjectModel;
using Kaedei.AcDown.Downloader;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;

namespace Kaedei.AcDown.Core
{
	public class PluginManager
	{

		private string _startupPath;
		public PluginManager(string startupPath)
		{
			_startupPath = startupPath;
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
			
			//add extenal plugins
			//LoadPlugins(_startupPath);

		}

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
		/// 保存插件配置
		/// </summary>
		/// <param name="plugin">需要保存配置的插件引用</param>
		/// <returns>如果保存成功返回true，失败为false</returns>
		public bool SaveConfiguration(IPlugin plugin)
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

		private void LoadConfiguration(IPlugin plugin, string startupPath)
		{
			try
			{
				//取得完整文件名（4.0后生效）
				//例如 C:\xxxx\acdown\Plugin\Kaedei\AcfunDownloader\4.0\settings.xml
				string path = Path.Combine(startupPath, "settings.xml");

				//反序列化插件设置
				XmlSerializer s = new XmlSerializer(typeof(SerializableDictionary<string, string>));
				using (FileStream fs = new FileStream(path, FileMode.Open))
				{
					plugin.Configuration = (SerializableDictionary<string, string>)s.Deserialize(fs);
				}
			}
			catch { }
			finally
			{
				if (plugin.Configuration == null)
					plugin.Configuration = new SerializableDictionary<string, string>();
				//设置启动路径
				plugin.Configuration["StartupPath"] = startupPath;
			}
		}


		private void LoadPlugins(string appdir)
		{
			//Current AcDown Core Version
			var currentver = Assembly.GetExecutingAssembly().GetName().Version;
			//all plugin assembly
			var acdplugins = new List<string>();

			string[] companies = Directory.GetDirectories(appdir, "*", SearchOption.TopDirectoryOnly);
			foreach (string company in companies) //公司
			{
				string[] plugins = Directory.GetDirectories(company, "*", SearchOption.TopDirectoryOnly);
				foreach (string plugin in plugins) //插件
				{
					string[] versions = Directory.GetDirectories(plugin, "*.*", SearchOption.TopDirectoryOnly);
					Version chosenVer = null; //已选择的版本
					string chosenEntryFile = ""; //已选择的文件
					foreach (string version in versions) //版本号
					{
						//如果update.XML文件存在
						string xmlfile = Path.Combine(version, "update.xml");
						if (File.Exists(xmlfile))
						{
							try
							{
								Update u;
								//解析Update.XML文件
								using (var fs = new FileStream(xmlfile, FileMode.Open))
								{
									XmlSerializer s = new XmlSerializer(typeof(Update));
									u = (Update)s.Deserialize(fs);
								}
								//插件支持的最小版本和插件当前版本
								var minver = new Version(u.MinSupportedVersion);
								var pluginver = new Version(u.Version);

								if (currentver > minver)
								{
									if (chosenVer == null || pluginver > chosenVer)
									{
										string entryfile = Path.Combine(version, u.EntryFile);
										//Assembly.ReflectionOnlyLoadFrom(entryfile);
										chosenVer = pluginver;
										chosenEntryFile = entryfile;
									}
								}
							}
							catch
							{

							}//end try
						}//end if Update.xml existed	
					}//end foreach version in versions
					//if doesn't have a chosen file
					if (!string.IsNullOrEmpty(chosenEntryFile))
					{
						acdplugins.Add(chosenEntryFile);
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
								LoadConfiguration(p, Path.GetDirectoryName(file));
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
