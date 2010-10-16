/*TaskManager.cs
 * 
 * class TaskManager:
 * 管理任务的类
 * 
 * Copyright 2010 Kaedei Software

	Licensed under the Apache License, Version 2.0 (the "License");
	you may not use this file except in compliance with the License.
	You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	Unless required by applicable law or agreed to in writing, software
	distributed under the License is distributed on an "AS IS" BASIS,
	WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	See the License for the specific language governing permissions and
	limitations under the License.
 * 
 * http://blog.sina.com.cn/kaedei
 * mailto:kaedei@foxmail.com
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using AcFunDowner;
using AcFunDownerLibrary;
using Kaedei.AcDown;
using Kaedei.AcDown.Interface;

namespace Kaedei.AcFunDowner
{
	
	public delegate void RefreshTaskListDelegate(AcDowner downer);
	/// <summary>
	/// 任务管理
	/// </summary>
	public class TaskManager
	{
		/// <summary>
		/// 实例TaskManager的引用
		/// </summary>
		public static TaskManager ObjectReference { get; set; }
		public Collection<AcDowner> Tasks { get; set; }
		public Int32 LimitedSpeed { get; set; }

		private DelegateContainer delegates;
		private RefreshTaskListDelegate refresh;
		public TaskManager(DelegateContainer delegatesCon,RefreshTaskListDelegate refreshDele)
		{
			delegates = delegatesCon ;
			refresh = refreshDele;
			Tasks=new Collection<AcDowner>();
		}//end TaskManager

		/// <summary>
		/// 添加任务
		/// </summary>
		/// <param name="url"></param>
		public void AddTask(string url,bool immediate)
		{
			AcDowner acd = null ;
			if (Config.setting.AutoDownAllSection)
			{
				//添加多段任务
				Dictionary<string, string> dict = AcDowner.GetVideoSections(url);
				if (dict.Count != 0)
				{
					foreach (string i in dict.Keys)
					{
						acd = new AcDowner(delegates, i, dict[i], Guid.NewGuid());
						Tasks.Add(acd);
						if (refresh != null)
							refresh.Invoke(acd);
					}
				}
				else
				{
					//取得当前URL的标题(和下面一段代码是一样的)
					string sectionTitle = AcDowner.GetVideoSectionName(url);
					acd = new AcDowner(delegates, url, sectionTitle, Guid.NewGuid());
					Tasks.Add(acd);
					if (refresh != null)
						refresh.Invoke(acd);
				}
			}
			else //如果不添加多段任务
			{
				//取得当前URL的标题
				string sectionTitle = AcDowner.GetVideoSectionName(url);
				acd = new AcDowner(delegates, url, sectionTitle, Guid.NewGuid());
				Tasks.Add(acd);
				if (refresh != null)
					refresh.Invoke(acd);
			}
			//开始所有可能开始的任务
			if (immediate)
				ContinueNext();
		}//end AddTask

		/// <summary>
		/// 删除任务
		/// </summary>
		/// <param name="task">需要删除的任务</param>
		/// <returns></returns>
		public bool DeleteTask(AcDowner task)
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
			//所有等待的任务尝试开始
			foreach (AcDowner item in Tasks)
			{
				if (item.Status == DownloadStatus.等待开始)
					delegates.Start(new ParaStart(item.TaskId));
			}

			//所有开始的任务刷新速度限制
			foreach (AcDowner item in Tasks)
			{
				if (item.Status == DownloadStatus.正在下载)
					item.SpeedLimit = LimitedSpeed / GetRunningCount();
			}
		}

	}//end class
}//end namespace
