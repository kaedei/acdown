using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

namespace Kaedei.AcDown.Interface
{

	/// <summary>
	/// 下载状态
	/// </summary>
	public enum DownloadStatus
	{
		等待开始 = 0,
		正在下载 = 1,
		已经停止 = 2,
		下载完成 = 3,
		出现错误 = 4,
		正在停止 = 5,
		已删除 = 6,
		部分完成 = 7
	}

	/// <summary>
	/// 下载适配器接口
	/// </summary>
	public interface IDownloader
	{
		DelegateContainer delegates{ get; set; }
		TaskInfo Info { get; set; }

		long TotalLength{ get; }
		long DoneBytes{ get; }
		long LastTick { get; }

		bool Download();
		void StopDownload();
	}



}
