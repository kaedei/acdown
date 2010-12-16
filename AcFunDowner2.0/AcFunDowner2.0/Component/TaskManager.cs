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
		public Guid AddTask(string url, IDownloader downloader)
		{
			//设置下载器
			downloader.delegates = delegates;
			downloader.Url = url;
			downloader.SaveDirectory = new DirectoryInfo(Config.setting.SavePath);
			downloader.TaskId = Guid.NewGuid();
			//向集合中添加任务
			Tasks.Add(downloader);
			//返回新建的任务
			return downloader.TaskId;
		}

		/// <summary>
		/// 开始任务
		/// </summary>
		public void StartTask(IDownloader downloader)
		{
			//启动新线程下载文件
			Thread t = new Thread(() =>
				{
					//如果队列未满则开始下载
					if (GetRunningCount() < Config.setting.MaxRunningTaskCount)
					{
						downloader.DownloadVideo();
					}
				});
			//提示UI刷新信息
			delegates.Refresh.Invoke(new ParaRefresh(downloader.TaskId));
		}

		/// <summary>
		/// 停止任务
		/// </summary>
		/// <param name="downloader"></param>
		public void StopTask(IDownloader downloader)
		{
			//启动新线程停止任务
			Thread t = new Thread(() =>
				{
					downloader.StopDownloadVideo();
					//刷新信息
					delegates.Refresh(new ParaRefresh(downloader.TaskId));
				});
		}

		/// <summary>
		/// 删除任务(自动终止未停止的任务)
		/// </summary>
		/// <param name="downloader"></param>
		public void DeleteTask(IDownloader downloader, bool deleteFile)
		{

			//启动新线程结束任务，防止下载器阻止UI响应
			Thread t = new Thread(() =>
				{
					//UI中任务应被首先删除，所以不必有刷新UI的代码
					//先停止任务（线程阻塞的）
					downloader.StopDownloadVideo();
					//然后从任务列表中删除任务
					if (Tasks.Contains(downloader))
					{
						Tasks.Remove(downloader);
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
					
				});
		}



		/// <summary>
		/// 取得下一个正在等待的任务
		/// </summary>
		/// <returns></returns>
		public IDownloader GetNextWaiting()
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
		public IDownloader GetFirstRunning()
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

		#region 参考

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
					StartTask(item);
			}
		}
		#endregion
	}//end class
}//end namespace
