using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Core;

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

		private int _cacheSize = 1;
		/// <summary>
		/// 网络缓存大小，单位为兆字节(MB)，取值范围为1~16
		/// </summary>
		public int CacheSize
		{
			get
			{
				return _cacheSize;
			}
			set
			{
				_cacheSize = (value > 16 || value < 1) ? 1 : value;
			}
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


		private int _retryTimes = 3;
		/// <summary>
		/// 下载失败时的重试次数
		/// </summary>
		public int RetryTimes
		{
			get { return _retryTimes; }
			set 
			{
				if (value > 99 || value < 0) _retryTimes = 3;
				else _retryTimes = value;
			}
		}

		private int _retryWaitingTime = 5000;
		/// <summary>
		/// 下载重试前的等待时间(毫秒)
		/// </summary>
		public int RetryWaitingTime
		{
			get { return _retryWaitingTime; }
			set
			{
				if (value > 300000 || value < 0) _retryWaitingTime = 5000; //默认值5秒钟
				else _retryWaitingTime = value;
			}
		}

		private int toolFormTimeout = 150; //默认值150秒

		/// <summary>
		/// 工具窗口的超时时间(秒)
		/// </summary>
		public int ToolFormTimeout
		{
			get { return toolFormTimeout; }
			set 
			{
				if (value < 10)
					toolFormTimeout = 10;
				else if (value > 600)
					toolFormTimeout = 600;
				else
					toolFormTimeout = value;
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
