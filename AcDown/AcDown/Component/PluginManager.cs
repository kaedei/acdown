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
			if (Config.setting.Plugin_Enable_Acfun)
				_plugins.Add(new AcFunPlugin());
			if (Config.setting.Plugin_Enable_Tudou)
				_plugins.Add(new TudouPlugin());
			if (Config.setting.Plugin_Enable_Bilibili)
				_plugins.Add(new BilibiliPlugin());
			if (Config.setting.Plugin_Enable_Youku)
				_plugins.Add(new YoukuPlugin());
			if (Config.setting.Plugin_Enable_Imanhua)
				_plugins.Add(new ImanhuaPlugin());
			if (Config.setting.Plugin_Enable_TiebaAlbum)
				_plugins.Add(new TiebaAlbumPlugin());
         if (Config.setting.Plugin_Enable_Tucao)
            _plugins.Add(new TucaoPlugin());
			if (Config.setting.Plugin_Enable_Flvcd)
				_plugins.Add(new FlvcdPlugin());
         if (Config.setting.Plugin_Enable_SfAcg)
            _plugins.Add(new SfAcgPlugin());
		}

	}
}
