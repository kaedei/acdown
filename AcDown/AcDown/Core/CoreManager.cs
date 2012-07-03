
using System.IO;
namespace Kaedei.AcDown.Core
{
	/// <summary>
	/// AcDown核心管理器
	/// </summary>
	public class CoreManager
	{
		private CoreManager() { }

		public static UIDelegateContainer UIDelegates { get; private set; }
		public static PluginManager PluginManager { get; private set; }
		public static TaskManager TaskManager { get; private set; }
		public static ConfigManager ConfigManager { get; private set; }

		/// <summary>
		/// 初始化AcDown核心
		/// </summary>
		/// <param name="startupFolderPath"></param>
		/// <param name="taskFilePath"></param>
		/// <param name="uiDelegates"></param>
		public static void Initialize(string startupFolderPath, UIDelegateContainer uiDelegates)
		{
			//如果目录不存在则创建
			if (!Directory.Exists(startupFolderPath))
			{
				Directory.CreateDirectory(startupFolderPath);
			}

			//全局设置
			ConfigManager = new ConfigManager(startupFolderPath);
			ConfigManager.LoadSettings();
			//记录
			Logging.Initialize();
			//插件管理器
			PluginManager = new PluginManager(startupFolderPath);
			PluginManager.LoadPlugins();
			//委托
			UIDelegates = uiDelegates;
			//任务管理器
			TaskManager = new TaskManager(UIDelegates, PluginManager, startupFolderPath);
			TaskManager.LoadAllTasks();
		}

	}
}
