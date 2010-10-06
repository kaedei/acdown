using System;
using System.Collections.Generic;
using System.Text;

namespace Kaedei.AcFunDowner.Interface
{
	/// <summary>
	/// 提供给插件的全局设置
	/// </summary>
	public static class GlobalSettings
	{
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
		public int CacheSizeMb;

		/// <summary>
		/// 中止时保存已经下载的部分
		/// </summary>
		public bool SaveFileWhenAbort;

		/// <summary>
		/// 速度限制
		/// </summary>
		public int SpeedLimit;
	}
}
