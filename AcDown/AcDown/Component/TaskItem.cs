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
		public Uri Uri { get; set; }

		//下载状态
		public DownloadStatus Status
		{
			get
			{
				return Downloader.Status;
			}
		}

		//视频标题
		public string VideoTitle { get; set; }

		//开始任务
		public void Start()
		{
			Downloader.DownloadVideo();
		}

		//停止任务
		public void Stop()
		{
			Downloader.StopDownloadVideo();
		}

		//下载字幕&弹幕
		public void DownloadSub()
		{
			Downloader.DownloadSub();
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

	}
}
