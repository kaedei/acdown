/*TaskManager.cs
 * 
 * class TaskManager:
 * 管理任务的类
 * 
 */

using System;
using System.IO;
using System.Collections.ObjectModel;
using Kaedei.AcDown.Interface;
using System.Threading;
using System.Net;

namespace Kaedei.AcDown.Component
{
	
	/// <summary>
	/// 任务管理
	/// </summary>
	public class TaskManager
	{
		/// <summary>
		/// 新建TaskManager类的实例
		/// </summary>
		/// <param name="delegatesCon"></param>
		public TaskManager(DelegateContainer delegatesCon,PluginManager pluginManager)
		{
			delegates = delegatesCon;
			_pluginMgr = pluginManager;
		}

		//保存工作进程的弱引用,用于结束程序时强制结束下载线程
		//private Collection<WeakReference> taskThreadReferenceCollection = new Collection<WeakReference>();

		//插件管理器
		PluginManager _pluginMgr;

		//所有任务
		public Collection<TaskInfo> TaskInfos = new Collection<TaskInfo>();

		//全局速度限制
		private int _speedLimitGlobal = 0;
		public int SpeedLimitGlobal
		{
			get 
			{ 
				return _speedLimitGlobal; 
			}
			set
			{
				_speedLimitGlobal = value;
				int c = GetRunningCount();
				if (c != 0)
				{
					int limit = value / c;
					foreach (TaskInfo info in TaskInfos)
					{
						if (info.Status == DownloadStatus.正在下载)
							info.SpeedLimit = limit;
					}
				}
			}
		}

		//委托
		private DelegateContainer delegates;

		/// <summary>
		/// 添加任务
		/// </summary>
		/// <param name="plugin">任务所属的插件引用</param>
		/// <param name="url">任务Url</param>
		/// <param name="proxySetting">代理服务器设置</param>
		/// <param name="downSub">下载字幕文件设置</param>
		/// <returns></returns>
		public TaskInfo AddTask(IAcdownPluginInfo plugin,string url,WebProxy proxySetting)
		{
			//新建TaskInfo对象
			TaskInfo task = new TaskInfo();
			task.Url = url;
			task.SourceUrl = url;
			task.BasePlugin = plugin;
			task.PluginName = plugin.Name;
			task.TaskId = Guid.NewGuid();
			task.Proxy = proxySetting;
			task.CreateTime = DateTime.Now;
			task.Status = DownloadStatus.等待开始;
			task.SaveDirectory = new DirectoryInfo(Config.setting.SavePath);
			//向集合中添加对象
			TaskInfos.Add(task);
			//提示UI刷新信息
			//if (delegates.Refresh != null)
			//	delegates.Refresh.Invoke(new ParaRefresh(task.TaskId));
			return task;
		}


		/// <summary>
		/// 开始任务
		/// </summary>
		public void StartTask(TaskInfo task)
		{
			//寻找所需插件
			if (task.BasePlugin == null)
			{
				task.BasePlugin = _pluginMgr.GetPlugin(task.PluginName);
			}

			//如果队列未满则开始下载
			if (GetRunningCount() < Config.setting.MaxRunningTaskCount)
			{
				//启动新线程下载文件
				Thread t = new Thread(() =>
					{
						try
						{
							//下载视频
							task.Start(delegates);
						}
						catch (Exception ex) //如果出现错误
						{
							delegates.Error.Invoke(new ParaError(task, ex));
						}

					});
				t.IsBackground = true;
				//开始下载
				t.Start();
			}
			delegates.Refresh(new ParaRefresh(task));
		}

		/// <summary>
		/// 停止任务
		/// </summary>
		/// <param name="task"></param>
		public void StopTask(TaskInfo task)
		{
			task.Status = DownloadStatus.正在停止;
			//刷新信息
			delegates.Refresh(new ParaRefresh(task));
			//停止任务
			task.Stop();
			
			//启动新线程等待任务完全停止
			Thread t = new Thread(new ThreadStart(() =>
			{
				//等待停止
				while (task.Status == DownloadStatus.正在停止)
				{
					Thread.Sleep(50);
				}
				//刷新信息
				delegates.Refresh(new ParaRefresh(task));
			}));
			t.IsBackground = true;
			t.Start();
		}

		/// <summary>
		/// 删除任务(自动终止未停止的任务)
		/// </summary>
		/// <param name="task"></param>
		public void DeleteTask(TaskInfo task, bool deleteFile)
		{
			//停止任务
			task.Stop();

			//启动新线程等待任务完全停止
			Thread t = new Thread(new ThreadStart(() =>
			{
				while (task.Status == DownloadStatus.正在停止)
				{
					Thread.Sleep(50);
				}

				//是否删除文件
				if (deleteFile)
				{
					//删除所有视频文件
					foreach (var f in task.FilePath)
					{
						if (File.Exists(f))
						{
							File.Delete(f);
						}
					}
					//删除所有字幕文件
					foreach (var item in task.SubFilePath)
					{
						if (File.Exists(item))
						{
							File.Delete(item);
						}
					}
				}

				//从任务列表中删除任务
				//如果任务正在运行
				if (task.Status != DownloadStatus.已删除)
				{
					//移动到已删除
					task.Status = DownloadStatus.已删除;
				}
				else //如果任务已经删除至回收站
				{
					//移除集合中的任务
					TaskInfos.Remove(task);
				}

				//刷新信息
				delegates.Refresh(new ParaRefresh(task));
			}));
			t.IsBackground = true;
			t.Start();
		}


		/// <summary>
		/// 返回与指定的ID相关联的任务
		/// </summary>
		/// <param name="taskId">任务ID</param>
		/// <returns></returns>
		public TaskInfo GetTaskInfo(Guid taskId)
		{
			foreach (TaskInfo item in TaskInfos)
			{
				if (item.TaskId == taskId)
					return item;
			}
			return null;
		}

		/// <summary>
		/// 取得下一个正在等待的任务
		/// </summary>
		/// <returns></returns>
		public TaskInfo GetNextWaiting()
		{
			foreach (var item in TaskInfos)
			{
				if (item.Status == DownloadStatus.等待开始)
					return item;
			}
			return null;
		}

		/// <summary>
		/// 取得当前正在下载的任务数量
		/// </summary>
		/// <returns></returns>
		public Int32 GetRunningCount()
		{
			int count = 0;
			foreach (var i in this.TaskInfos)
			{
				if (i.Status == DownloadStatus.正在下载)
					count++;
			}
			return count;
		}

		/// <summary>
		/// 停止所有任务
		/// </summary>
		public void StopAllTasks()
		{
			////在正在进行的任务的弱引用集合中
			//foreach (var item in taskThreadReferenceCollection)
			//{
			//   //如果该任务对象仍然存活（未被销毁）
			//   if (item.IsAlive)
			//   {
			//      //强制终止线程
			//      Thread t = (Thread)item.Target;
			//      t.Abort();
			//   }
			//}
		}//end StopAllTasks()

		/// <summary>
		/// 执行下一个任务
		/// </summary>
		public void ContinueNext()
		{
			//计算可以开始的任务数量
			int canStart = Config.setting.MaxRunningTaskCount - GetRunningCount();
			if (canStart > 0)
			{
				//所有等待的任务尝试开始
				foreach (var item in TaskInfos)
				{
					if (item.Status == DownloadStatus.等待开始)
					{
						if (canStart > 0)
						{
							StartTask(item);
							canStart--;
						}
					}
				}
				//设置限速
				Thread.Sleep(250);
				SpeedLimitGlobal = _speedLimitGlobal;
			}
		}//end ContinueNext


		/// <summary>
		/// 为所有正在运行的任务设置限速
		/// </summary>
		/// <param name="limit"></param>
		public void SetSpeedLimitKb(int limit)
		{
			int running = GetRunningCount();
			int speed = 0;
			if (running != 0)
			{
				speed = limit / running;
			}
			foreach (TaskInfo task in TaskInfos)
			{
				if (task.Status == DownloadStatus.正在下载)
					task.SpeedLimit = speed;
			}
		}
	}//end class
}//end namespace
