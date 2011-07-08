using System;
using System.Collections.Generic;
using System.Text;

namespace Kaedei.AcDown.Interface
{
	/// <summary>
	/// 提供给插件的全局设置
	/// </summary>
	public class GlobalSettings
	{
		static GlobalSettings()
		{
			_settings = new GlobalSettings();
		}


		private static GlobalSettings _settings;
		/// <summary>
		/// 取得全局设置
		/// </summary>
		/// <returns></returns>
		public static GlobalSettings GetSettings()
		{
			return _settings;
		}

		/// <summary>
		/// 缓存
		/// </summary>
		public int CacheSizeMb { get; set; }

		/// <summary>
		/// 速度限制
		/// </summary>
		public int SpeedLimit { get; set; }

		/// <summary>
		/// 下载字幕
		/// </summary>
		public bool DownSub { get; set; }

      //代理服务器设置
      public bool Proxy_Enabled { get; set; }
      public string Proxy_Address { get; set; }
      public int Proxy_Port { get; set; }
      public string Proxy_Username { get; set; }
      public string Proxy_Password { get; set; }

	}
}
