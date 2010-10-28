/*TaskManager.cs
 * 
 * class TaskManager:
 * 管理任务的类
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Kaedei.AcDown;
using Kaedei.AcDown.Interface;

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

		//任务
		public Collection<IDownloader> Tasks = new Collection<IDownloader>();

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
		public void AddTask(string url, IDownloader downloader)
		{
			//设置下载器
			downloader.delegates = delegates;
			downloader.Url = url;
			downloader.FolderPath = Config.setting.SavePath;
			downloader.Status = DownloadStatus.等待开始;
			downloader.TaskId = Guid.NewGuid();
			//如果队列未满则开始下载
			if (GetRunningCount() < Config.setting.MaxRunningTaskCount)
			{
				downloader.DownloadVideo();	
			}
			//提示UI刷新信息
			delegates.Refresh.Invoke(new ParaRefresh(downloader.TaskId));
		}

		/// <summary>
		/// 停止任务
		/// </summary>
		/// <param name="downloader"></param>
		public void StopTask(IDownloader downloader)
		{


		}

		/// <summary>
		/// 删除任务(自动终止未停止的任务)
		/// </summary>
		/// <param name="downloader"></param>
		public void DeleteTask(IDownloader downloader)
		{


		}


		#region 参考

		/// <summary>
		/// 删除任务
		/// </summary>
		/// <param name="task">需要删除的任务</param>
		/// <returns></returns>
		public bool DeleteTask(IDownloader task)
		{
			try
			{
				if (Tasks.IndexOf(task) >= 0)
				{
					Tasks.Remove(task);
					return true;
				}
			}
			catch (Exception ex)
			{
				//记录日志
				Logging.Add(ex);
			}
			return false;
		
		}

		/// <summary>
		/// 取得下一个正在等待的任务
		/// </summary>
		/// <returns></returns>
		public AcDowner GetNextWaiting()
		{
			foreach (AcDowner item in Tasks)
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
		public AcDowner GetFirstRunning()
		{
			foreach (AcDowner item in Tasks)
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
			int count=0;
			foreach (AcDowner i in this.Tasks)
			{
				if (i.Status == DownloadStatus.正在下载)
					count++;
			}
			return count;
		}


		/// <summary>
		/// 执行下一个任务
		/// </summary>
		public void ContinueNext()
		{
			//如果当前正在进行的任务队列已满
			if (GetRunningCount() >= Config.setting.MaxRunningTaskCount)
			{
				return;
			}
			//所有等待的任务尝试开始
			foreach (var item in Tasks)
			{
				if (item.Status == DownloadStatus.等待开始)
					delegates.Start(new ParaStart(item.TaskId));
			}
		}
		#endregion
	}//end class
}//end namespace
