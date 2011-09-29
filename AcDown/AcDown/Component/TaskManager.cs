/*TaskManager.cs
 * 
 * class TaskManager:
 * 管理任务的类
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using Kaedei.AcDown;
using Kaedei.AcDown.Interface;
using System.Threading;
using System.Net;
using Kaedei.AcDown.Component;

namespace Kaedei.AcDown
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
		public TaskManager(DelegateContainer delegatesCon)
		{
			delegates = delegatesCon;
		}

		//保存工作进程的弱引用,用于结束程序时强制结束下载线程
		private Collection<WeakReference> taskThreadReferenceCollection = new Collection<WeakReference>();

		//任务
		public Collection<TaskItem> Tasks = new Collection<TaskItem>();

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
				GlobalSettings.GetSettings().SpeedLimit = value / Tasks.Count;
			}
		}

		//委托
		private DelegateContainer delegates;

		/// <summary>
		/// 添加任务
		/// </summary>
		public TaskItem AddTask(TaskItem downloader, string url, WebProxy proxySetting)
		{
			//设置下载器
			downloader.delegates = delegates;
			//设置代理服务器
			downloader.delegates.Proxy = proxySetting;
			downloader.Url = url;
			downloader.SaveDirectory = new DirectoryInfo(Config.setting.SavePath);
			downloader.TaskId = Guid.NewGuid();
			//向集合中添加任务
			Tasks.Add(downloader);
			//提示UI刷新信息
			delegates.Refresh.Invoke(new ParaRefresh(downloader.TaskId));
			//返回新建的任务
			return downloader;
		}

		/// <summary>
		/// 开始任务
		/// </summary>
		public void StartTask(TaskItem downloader)
		{
			//如果队列未满则开始下载
			if (GetRunningCount() < Config.setting.MaxRunningTaskCount)
			{
				//启动新线程下载文件
				Thread t = new Thread(() =>
					{
						try
						{
							//下载视频
							downloader.Start();
						}
						catch (Exception ex) //如果出现错误
						{
							delegates.Error.Invoke(new ParaError(downloader.TaskId, ex));
						}

					});
				//开始下载
				t.Start();
				//添加到弱引用集合中
				WeakReference wr = new WeakReference(t);
				taskThreadReferenceCollection.Add(wr);
			}
		}

		/// <summary>
		/// 停止任务
		/// </summary>
		/// <param name="downloader"></param>
		public void StopTask(TaskItem downloader)
		{
			downloader.Stop();
			//刷新信息
			delegates.Refresh(new ParaRefresh(downloader.TaskId));
		}

		/// <summary>
		/// 删除任务(自动终止未停止的任务)
		/// </summary>
		/// <param name="downloader"></param>
		public void DeleteTask(TaskItem downloader, bool deleteFile)
		{

			//先停止任务
			downloader.Stop();

			while (downloader.Status == DownloadStatus.正在下载)
			{
				Thread.Sleep(50);
			}
			
			
			//是否删除文件
			if (deleteFile)
			{
				//删除所有视频文件
				foreach (var f in downloader.FilePath)
				{
					if (File.Exists(f))
					{
						File.Delete(f);
					}
				}
				//删除所有字幕文件
				foreach (var item in downloader.SubFilePath)
				{
					if (File.Exists(item))
					{
						File.Delete(item);
					}
				}
			}
			

			//从任务列表中删除任务
			if (Tasks.Contains(downloader))
			{
				Tasks.Remove(downloader);
			}
		}



		/// <summary>
		/// 取得下一个正在等待的任务
		/// </summary>
		/// <returns></returns>
		public TaskItem GetNextWaiting()
		{
			foreach (var item in Tasks)
			{
				if (item.Status == DownloadStatus.等待开始)
					return item;
			}
			return null;
		}

		/// <summary>
		/// 取得第一个正在下载的任务
		/// </summary>
		/// <returns></returns>
		public TaskItem GetFirstRunning()
		{
			foreach (var item in Tasks)
			{
				if (item.Status == DownloadStatus.正在下载)
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
			foreach (var i in this.Tasks)
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
			//在正在进行的任务的弱引用集合中
			foreach (var item in taskThreadReferenceCollection)
			{
				//如果该任务对象仍然存活（未被销毁）
				if (item.IsAlive)
				{
					//强制终止线程
					Thread t = (Thread)item.Target;
					t.Abort();
				}
			}

		}

		#region 参考

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
				foreach (var item in Tasks)
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
			}
		}
		#endregion

	}//end class
}//end namespace
