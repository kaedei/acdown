using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Collections.ObjectModel;
using Kaedei.AcDown.Downloader;

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
			_plugins.Add(new AcFunPlugin());
			_plugins.Add(new TudouPlugin());
			_plugins.Add(new BilibiliPlugin());
		}

	}
}
