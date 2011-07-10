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


		}

		//任务Id
		public Guid TaskId { get; set; }

		//包装的IDownloader对象
		private IDownloader Downloader { get; set; }

		//任务Uri
		public string Url { get; set; }

		//下载状态
		public DownloadStatus Status
		{
			get
			{
				return Downloader.Status;
			}
		}

		//视频标题
		public string Title { get; set; }

		//开始任务
		public void Start()
		{
			Downloader.Download();
		}

		//停止任务
		public void Stop()
		{
			Downloader.StopDownload();
		}

		//任务下载进度
		public double GetProcess()
		{
			return Downloader.DoneBytes / Downloader.TotalLength;
		}

		//任务下载速度差
		public long GetTickCount()
		{
			return Downloader.DoneBytes - Downloader.LastTick;
		}

		public DelegateContainer delegates
		{
			get
			{
				return Downloader.delegates;
			}
			set
			{
				Downloader.delegates = value;
			}
		}

		public IAcdownPluginInfo GetBasePlugin()
		{
			return Downloader.GetBasePlugin();
		}


		public int PartCount
		{
			get { return Downloader.PartCount; }
		}

		public int CurrentPart
		{
			get { return Downloader.CurrentPart; }
		}


		public System.IO.DirectoryInfo SaveDirectory
		{
			get
			{
				return Downloader.SaveDirectory;
			}
			set
			{
				Downloader.SaveDirectory = value;
			}
		}

		public List<string> FilePath
		{
			get { return Downloader.FilePath; }
		}

		public List<string> SubFilePath
		{
			get { return Downloader.SubFilePath; }
		}

		public string Info
		{
			get { throw new NotImplementedException(); }
		}

	}
}
