
namespace Kaedei.AcDown.Core
{
	/// <summary>
	/// AcDown核心管理器
	/// </summary>
	public class CoreManager
	{
		public UIDelegateContainer UIDelegates { get; private set; }
		public PluginManager PluginManager { get; private set; }
		public TaskManager TaskManager { get; private set; }

		/// <summary>
		/// 初始化AcDown核心
		/// </summary>
		/// <param name="pluginsFolderPath"></param>
		/// <param name="taskFilePath"></param>
		/// <param name="uiDelegates"></param>
		public void Initialize(string pluginsFolderPath, string taskFilePath, UIDelegateContainer uiDelegates)
		{
			//全局设置
			Config.LoadSettings();
			//记录
			Logging.Initialize();
			//插件管理器
			PluginManager = new PluginManager(pluginsFolderPath);
			PluginManager.LoadPlugins();
			//委托
			UIDelegates = uiDelegates;
			//任务管理器
			TaskManager = new TaskManager(UIDelegates, PluginManager);
			TaskManager.LoadAllTasks();
		}

	}
}
