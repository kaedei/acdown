﻿/*TaskManager.cs
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
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Diagnostics;

namespace Kaedei.AcDown.Core
{

	/// <summary>
	/// 任务管理
	/// </summary>
	public class TaskManager : IDisposable
	{
		/// <summary>
		/// 新建TaskManager类的实例
		/// </summary>
		public TaskManager()
		{
			//初始化预处理委托
			preDelegates = new UIDelegateContainer(
				this.StartPreprocessor,
				this.NewPartPreprocessor,
				this.RefreshPreprocessor,
				this.TipTextPreprocessor,
				this.FinishPreprocessor,
				this.ErrorPreprocessor,
				this.NewTaskPreprocessor,
				this.AllFinishedPreprocessor);
		}

		//所有任务
		public List<TaskInfo> TaskInfos = new List<TaskInfo>();

		//TaskInfos对象的全局锁
		public object TaskInfosLock = new object();

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

		//UI委托的包装（预处理）
		private UIDelegateContainer preDelegates;
		//来自UI的委托
		//private UIDelegateContainer uiDelegates;

		/// <summary>
		/// 添加任务
		/// </summary>
		/// <param name="plugin">任务所属的插件引用</param>
		/// <param name="url">任务Url</param>
		/// <param name="proxySetting">代理服务器设置</param>
		/// <param name="downSub">下载字幕文件设置</param>
		/// <returns></returns>
		public TaskInfo AddTask(IPlugin plugin, string url, WebProxy proxySetting)
		{
			//新建TaskInfo对象
			TaskInfo task = new TaskInfo();
			task.Url = url;
			task.SourceUrl = url;
			task.BasePlugin = plugin;
			object[] types = plugin.GetType().GetCustomAttributes(typeof(AcDownPluginInformationAttribute), true);
			task.PluginName = (types[0] as AcDownPluginInformationAttribute).Name;
			task.TaskId = Guid.NewGuid();
			task.Proxy = proxySetting;
			task.CreateTime = DateTime.Now;
			task.Status = DownloadStatus.等待开始;
			if (task.SaveDirectory == null)
				task.SaveDirectory = new DirectoryInfo(CoreManager.ConfigManager.Settings.SavePath);
			//向集合中添加对象
			Monitor.Enter(TaskInfosLock);
			TaskInfos.Add(task);
			Monitor.Exit(TaskInfosLock);
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
			//如果正在停止则什么都不做(等待任务正常停止)
			if (task.Status == DownloadStatus.正在停止 || task.Status == DownloadStatus.正在下载)
				return;
			//如果队列未满则开始下载
			if (GetRunningCount() < CoreManager.ConfigManager.Settings.MaxRunningTaskCount)
			{
				//启动新线程下载文件
				Thread t = new Thread(() =>
				{
					try
					{
						//AcDown规范:仅有TaskManager及插件本身有权修改其所属TaskInfo对象的Status属性
						task.Status = DownloadStatus.正在下载;
						preDelegates.Start(new ParaStart(task));

						//下载视频
						bool finished = task.Start(preDelegates);

						if (finished)
						{
							//设置完成状态
							if (task.PartialFinished)
								task.Status = DownloadStatus.部分完成;
							else
								task.Status = DownloadStatus.下载完成;
						}
						else
						{
							task.Status = DownloadStatus.已经停止;
						}
						preDelegates.Finish(new ParaFinish(task, finished));
					}
					catch (Exception ex) //如果出现错误
					{
						task.Status = DownloadStatus.出现错误;
						preDelegates.Error(new ParaError(task, ex));
					}

				});
				t.IsBackground = true;
				//开始下载
				t.Start();
			}
			else //如果队列已满，则转换状态至“等待开始”
			{
				task.Status = DownloadStatus.等待开始;
			}
			//刷新UI
			preDelegates.Refresh(new ParaRefresh(task));
		}

		/// <summary>
		/// 停止任务
		/// </summary>
		/// <param name="task"></param>
		public void StopTask(TaskInfo task)
		{
			//只有已开始的任务才可停止
			switch (task.Status)
			{
				case DownloadStatus.等待开始: //尚未开始的任务直接停止
					task.Status = DownloadStatus.已经停止;
					break;
				case DownloadStatus.正在下载: //已经开始的任务启动新线程停止
					task.Status = DownloadStatus.正在停止;
					break;
				default:
					return;
			}

			//刷新信息
			preDelegates.Refresh(new ParaRefresh(task));
			//停止任务
			task.Stop();

			if (task.Status != DownloadStatus.已经停止)
			{
				//启动新线程等待任务完全停止
				Thread t = new Thread(new ThreadStart(() =>
				{
					//超时时长 (10秒钟)
					int timeout = 10000;
					//等待停止
					while (task.Status == DownloadStatus.正在停止)
					{
						Thread.Sleep(500);
						timeout -= 500;
						if (timeout < 0) //如果到时仍未停止
						{
							task.Status = DownloadStatus.已经停止;
							break;
						}
					}
					//刷新信息
					preDelegates.Refresh(new ParaRefresh(task));
				}));
				t.IsBackground = true;
				t.Start();
			}
			//销毁Downloader
			task.DisposeDownloader();
		}

		/// <summary>
		/// 删除任务(自动终止未停止的任务)
		/// </summary>
		/// <param name="task"></param>
		public void DeleteTask(TaskInfo task, bool deleteFile, bool removeToRecyclebin)
		{
			//停止任务
			StopTask(task);

			//启动新线程等待任务完全停止

			ThreadPool.QueueUserWorkItem(new WaitCallback((o) =>
			{
				while (task.Status == DownloadStatus.正在停止 || task.Status == DownloadStatus.正在下载)
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
							try
							{
								File.Delete(f);
							}
							catch (Exception ex)
							{
								Logging.Add(ex);
							}
						}
					}
					//删除所有字幕文件
					foreach (var item in task.SubFilePath)
					{
						if (File.Exists(item))
						{
							try
							{
								File.Delete(item);
							}
							catch (Exception ex)
							{
								Logging.Add(ex);
							}
						}
					}
				}

				//从任务列表中删除任务
				if (task.Status != DownloadStatus.已删除 && removeToRecyclebin)
				{
					//移动到回收站
					task.Status = DownloadStatus.已删除;
				}
				else //如果任务已经删除至回收站
				{
					//移除集合中的任务
					TaskInfos.Remove(task);
				}

				//刷新信息
				preDelegates.Refresh(new ParaRefresh(task));
			}));
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
		/// 执行下一个任务
		/// </summary>
		public void ContinueNext()
		{
			//计算可以开始的任务数量
			int canStart = CoreManager.ConfigManager.Settings.MaxRunningTaskCount - GetRunningCount();
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

		/// <summary>
		/// 取得任务队列的第一个运行中的任务
		/// </summary>
		public TaskInfo GetFirstRunning()
		{
			foreach (TaskInfo task in TaskInfos)
			{
				if (task.Status == DownloadStatus.正在下载)
				{
					return task;
				}
			}
			return null;
		}

		private object saveTaskLock = new object();
		/// <summary>
		/// 保存所有任务到文件中
		/// </summary>
		public void SaveAllTasks()
		{
			lock (saveTaskLock)
			{
				//序列化至内存流
				using (MemoryStream ms = new MemoryStream())
				{
					try
					{
						XmlSerializer formatter = new XmlSerializer(typeof(List<TaskInfo>));
						formatter.Serialize(ms, TaskInfos);
						//将内存流复制到文件
						using (FileStream fs = new FileStream(Path.Combine(CoreManager.StartupPath, "Task.xml"), FileMode.Create))
						{
							ms.Position = 0;
							byte[] buffer = new byte[500 * 1024];
							int read = 0;
							read = ms.Read(buffer, 0, buffer.Length);
							while (read > 0)
							{
								fs.Write(buffer, 0, read);
								read = ms.Read(buffer, 0, buffer.Length);
							}
						}
					}
					catch (Exception ex)
					{
						Logging.Add(ex);
					}
				}
				//保证TaskInfos对象不会被意外回收
				GC.KeepAlive(TaskInfos);
			}
		}


		/// <summary>
		/// 从文件中读取任务列表
		/// </summary>
		public void LoadAllTasks()
		{
			//取得文件路径名称
			string path = Path.Combine(CoreManager.StartupPath, "Task.xml");
			//如果文件存在
			if (File.Exists(path))
			{
				using (FileStream fs = new FileStream(path, FileMode.Open))
				{
					try
					{
						XmlSerializer formatter = new XmlSerializer(typeof(List<TaskInfo>));
						TaskInfos = (List<TaskInfo>)formatter.Deserialize(fs);
					}
					catch (Exception ex)
					{
						Logging.Add(ex);
					}
				}
			}

			foreach (TaskInfo task in TaskInfos)
			{
				//寻找所需插件
				if (task.BasePlugin == null)
				{
					task.BasePlugin = CoreManager.PluginManager.GetPlugin(task.PluginName);
				}
			}

		}

		/// <summary>
		/// 根据GUID值寻找对应的任务
		/// </summary>
		/// <param name="guid"></param>
		/// <returns></returns>
		[DebuggerNonUserCode()]
		public TaskInfo GetTask(Guid guid)
		{
			foreach (var i in CoreManager.TaskManager.TaskInfos)
			{
				if (i.TaskId == guid)
					return i;
			}
			return null;
		}


		private bool bgWorkerContinue = false;
		private Timer bgWorker;
		/// <summary>
		/// 启动后台自动保存任务的进程
		/// </summary>
		public void StartSaveBackgroundWorker()
		{
			bgWorkerContinue = true;
			if (bgWorker == null)
			{
				//每45秒自动保存一次任务状态信息
				bgWorker = new Timer(new TimerCallback(SaveBackgroundWorker), null, 45000, 45000);
			}
		}

		/// <summary>
		/// 结束侯台自动保存任务的进程
		/// </summary>
		public void EndSaveBackgroundWorker()
		{
			bgWorkerContinue = false;
		}

		private void SaveBackgroundWorker(object o)
		{
			if (bgWorkerContinue)
			{
				SaveAllTasks();
			}
			else
			{
				bgWorker.Dispose();
			}
		}

		#region 预处理


		/// <summary>
		/// Start委托的预处理
		/// </summary>
		private void StartPreprocessor(object e)
		{
			if (CoreManager.UIDelegates.Start != null)
				CoreManager.UIDelegates.Start((DelegateParameter)e);
		}

		/// <summary>
		/// NewPart委托的预处理
		/// </summary>
		private void NewPartPreprocessor(object e)
		{
			if (CoreManager.UIDelegates.NewPart != null)
				CoreManager.UIDelegates.NewPart((DelegateParameter)e);
		}

		/// <summary>
		/// Refresh委托的预处理
		/// </summary>
		private void RefreshPreprocessor(object e)
		{
			if (CoreManager.UIDelegates.Refresh != null)
				CoreManager.UIDelegates.Refresh((DelegateParameter)e);
		}

		/// <summary>
		/// TipText委托的预处理
		/// </summary>
		private void TipTextPreprocessor(object e)
		{
			if (CoreManager.UIDelegates.TipText != null)
				CoreManager.UIDelegates.TipText((DelegateParameter)e);
		}

		/// <summary>
		/// NewTask委托的预处理
		/// </summary>
		private void NewTaskPreprocessor(object e)
		{
			ParaNewTask p = (ParaNewTask)e;
			TaskInfo sourcetask = p.SourceTask;
			IPlugin plugin = p.Plugin;
			string url = p.Url;

			//检查参数有效性
			if (!plugin.CheckUrl(url) || sourcetask == null || plugin == null || string.IsNullOrEmpty(url))
				return;
			//取得此url的hash
			string hash = plugin.GetHash(url);
			//检查是否有已经在进行的相同任务
			foreach (TaskInfo t in CoreManager.TaskManager.TaskInfos)
			{
				if (hash == t.Hash)
				{
					//如果有则不新建此任务
					//将状态由停止或删除修改为开始
					if (t.Status == DownloadStatus.出现错误 ||
						 t.Status == DownloadStatus.已经停止 ||
						 t.Status == DownloadStatus.已删除)
						CoreManager.TaskManager.StartTask(t);
					return;
				}
			}

			//设置新任务
			TaskInfo task = CoreManager.TaskManager.AddTask(plugin, url, sourcetask.Proxy);
			task.Settings = CloneDictionary(sourcetask.Settings);
			task.DownloadTypes = sourcetask.DownloadTypes;
			task.Comment = sourcetask.Comment;
			task.SaveDirectory = sourcetask.SaveDirectory;
			task.AutoAnswer = sourcetask.AutoAnswer;
			task.ExtractCache = sourcetask.ExtractCache;
			//此任务由其他任务所添加
			task.IsBeAdded = true;
			//开始新任务
			CoreManager.TaskManager.StartTask(task);

			//调用UI层的NewTask委托
			if (CoreManager.UIDelegates.NewTask != null)
				CoreManager.UIDelegates.NewTask(p);
		}

		private SerializableDictionary<string, string> CloneDictionary(SerializableDictionary<string, string> settings)
		{
			var newDictionary = new SerializableDictionary<string, string>();
			foreach (var setting in settings)
			{
				newDictionary[setting.Key] = setting.Value;
			}
			return newDictionary;
		}

		/// <summary>
		/// Finish委托的预处理
		/// </summary>
		private void FinishPreprocessor(object e)
		{
			ParaFinish p = (ParaFinish)e;
			TaskInfo task = p.SourceTask;

			//设置完成时间
			task.FinishTime = DateTime.Now;

			//执行下一个可能开始的任务
			CoreManager.TaskManager.ContinueNext();

			//执行UI委托
			if (CoreManager.UIDelegates.Finish != null)
				CoreManager.UIDelegates.Finish(p);

			//检查是否全部完成
			AllFinishedPreprocessor(null);
		}

		/// <summary>
		/// Error委托的预处理
		/// </summary>
		private void ErrorPreprocessor(object e)
		{
			ParaError p = (ParaError)e;
			TaskInfo task = p.SourceTask;
			//添加到日志
			Logging.Add(p.E);
			if (task != null)
			{
				//记录最后一次错误
				task.LastError = p.E;
			}

			//执行UI委托
			if (CoreManager.UIDelegates.Error != null)
				CoreManager.UIDelegates.Error(p);

			//执行下一个可能开始的任务
			CoreManager.TaskManager.ContinueNext();

			//检查是否全部完成
			AllFinishedPreprocessor(null);
		}

		/// <summary>
		/// 检查当前下载任务是否全部结束
		/// </summary>
		private void AllFinishedPreprocessor(object e)
		{
			//如果没有正在等待的任务了且正在运行的任务为0
			if (GetNextWaiting() == null && GetRunningCount() == 0)
				if (CoreManager.UIDelegates.AllFinished != null)
					CoreManager.UIDelegates.AllFinished(null);
		}

		#endregion

		#region IDisposable 成员

		void IDisposable.Dispose()
		{
			bgWorkerContinue = false;
			if (bgWorker != null)
				bgWorker.Dispose();
		}

		#endregion
	}//end class
}//end namespace
