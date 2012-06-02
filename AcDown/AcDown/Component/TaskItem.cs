using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;

namespace Kaedei.AcDown.Component
{
	public class TaskItem
	{
		public TaskItem(IDownloader downloader,DelegateContainer delegates )
		{
			resourceDownloader = downloader;
			downloader.delegates = delegates;
		}

		//任务Id
		public Guid TaskId {
			get
			{
				return this.resourceDownloader.TaskId;
			}
			set
			{
				if (value != null)
					this.resourceDownloader.TaskId = value;
			}
		}

		//包装的IDownloader对象
		private IDownloader resourceDownloader { get; set; }

		//任务Uri
		public string Url {
			get
			{
				return resourceDownloader.Url;
			}
			set
			{
				resourceDownloader.Url = value;
			}
		}

		//下载状态
		public DownloadStatus Status
		{
			get
			{
				return resourceDownloader.Status;
			}
		}

		//视频标题
		public string Title
		{
			get
			{
				return resourceDownloader.Title;
			}
		}

		//创建时间
		private DateTime createTime = DateTime.Now;
		public DateTime CreateTime { get { return createTime; } }

		/// <summary>
		/// 开始任务
		/// </summary>
		public void Start()
		{
			resourceDownloader.Download();
		}

		/// <summary>
		/// 停止任务
		/// </summary>
		public void Stop()
		{
			resourceDownloader.StopDownload();
		}

		/// <summary>
		/// 任务下载进度
		/// </summary>
		/// <returns></returns>
		public double GetProcess()
		{
			return (double)resourceDownloader.DoneBytes / (double)resourceDownloader.TotalLength;
		}

		/// <summary>
		/// 下载速度之差
		/// </summary>
		/// <returns></returns>
		public long GetTickCount()
		{
			return resourceDownloader.DoneBytes - resourceDownloader.LastTick;
		}

		/// <summary>
		/// 文件总长度
		/// </summary>
		public long TotalLength
		{ get { return resourceDownloader.TotalLength; } }

		/// <summary>
		/// 已完成的长度
		/// </summary>
		public long DoneBytes
		{ get { return resourceDownloader.DoneBytes; } }

		public DelegateContainer delegates
		{
			get
			{
				return resourceDownloader.delegates;
			}
			set
			{
				resourceDownloader.delegates = value;
			}
		}

		public IAcdownPluginInfo GetBasePlugin()
		{
			return resourceDownloader.GetBasePlugin();
		}


		public int PartCount
		{
			get { return resourceDownloader.PartCount; }
		}

		public int CurrentPart
		{
			get { return resourceDownloader.CurrentPart; }
		}


		public System.IO.DirectoryInfo SaveDirectory
		{
			get
			{
				return resourceDownloader.SaveDirectory;
			}
			set
			{
				resourceDownloader.SaveDirectory = value;
			}
		}

		public List<string> FilePath
		{
			get { return resourceDownloader.FilePath; }
		}

		public List<string> SubFilePath
		{
			get { return resourceDownloader.SubFilePath; }
		}

		public string Info
		{
			get { return resourceDownloader.Info; }
		}

	}
}
