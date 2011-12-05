using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Component;

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

		private int _networkTimeout = 25000; //25秒
		/// <summary>
		/// 网络请求的超时值（以毫秒为单位）
		/// </summary>
		public int NetworkTimeout
		{
			get
			{
				return _networkTimeout;
			}
			set
			{
				if (value > 100000)
					_networkTimeout = 100000;
				else
					if (value < 15000)
						_networkTimeout = 15000;
					else
						_networkTimeout = value;
			}
		}

	}

	public enum DownloadSubtitleType
	{
		DownloadSubtitle,
		DontDownloadSubtitle,
		DownloadSubtitleOnly
	}

}
