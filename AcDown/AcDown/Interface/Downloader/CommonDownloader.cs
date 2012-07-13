using System;
using System.Collections.Generic;
using System.Text;

namespace Kaedei.AcDown.Interface.Downloader
{
	/// <summary>
	/// 公共下载器，此类实现了IDownloader接口并提供了常用的方法与类成员实现
	/// </summary>
	public abstract class CommonDownloader : IDownloader
	{
		/// <summary>
		/// 与此任务相关联的信息
		/// </summary>
		public TaskInfo Info { get; set; }

		/// <summary>
		/// 与此任务相关联的设置
		/// </summary>
		protected IDictionary<string, string> Settings
		{
			get
			{
				return Info.Settings;
			}
		}

		/// <summary>
		/// 下载参数
		/// </summary>
		protected DownloadParameter currentParameter = new DownloadParameter();

		/// <summary>
		/// 更换为新Part
		/// </summary>
		protected void NewPart(Int32 partNumber,Int32 totalCount)
		{
			Info.PartCount = totalCount;
			Info.CurrentPart = partNumber;
			delegates.NewPart(new ParaNewPart(this.Info, partNumber));
		}
		/// <summary>
		/// 刷新UI
		/// </summary>
		protected void Refresh()
		{
			delegates.Refresh(new ParaRefresh(this.Info));
		}
		/// <summary>
		/// 创建新任务
		/// </summary>
		protected void NewTask(string newUrl)
		{
			delegates.Refresh(new ParaNewTask(this.Info.BasePlugin, newUrl, this.Info));
		}
		/// <summary>
		/// 更改提示信息
		/// </summary>
		protected void TipText(string text)
		{
			delegates.TipText(new ParaTipText(this.Info, text));
		}


		public DelegateContainer delegates { get; set; }

		/// <summary>
		/// 文件总长度
		/// </summary>
		public long TotalLength
		{
			get
			{
				if (currentParameter != null)
				{
					return currentParameter.TotalLength;
				}
				else
				{
					return 0;
				}
			}
		}

		/// <summary>
		/// 已完成的长度
		/// </summary>
		public long DoneBytes
		{
			get
			{
				if (currentParameter != null)
				{
					return currentParameter.DoneBytes;
				}
				else
				{
					return 0;
				}
			}
		}

		/// <summary>
		/// 最后一次Tick时的值
		/// </summary>
		public long LastTick
		{
			get
			{
				if (currentParameter != null)
				{
					//将tick值更新为当前值
					long tmp = currentParameter.LastTick;
					currentParameter.LastTick = currentParameter.DoneBytes;
					return tmp;
				}
				else
				{
					return 0;
				}
			}
		}

		public abstract bool Download();

		/// <summary>
		/// 停止下载
		/// </summary>
		public void StopDownload()
		{
			if (currentParameter != null)
			{
				//将停止flag设置为true
				currentParameter.IsStop = true;
			}
		}
	}
}
