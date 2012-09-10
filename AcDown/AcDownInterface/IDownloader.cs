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
		/// <summary>
		/// 任务管理器委托
		/// </summary>
		DelegateContainer delegates{ get; set; }
		/// <summary>
		/// 获取或设置与此任务相关联的信息
		/// </summary>
		TaskInfo Info { get; set; }
		/// <summary>
		/// 此任务总长度
		/// </summary>
		long TotalLength{ get; }
		/// <summary>
		/// 此任务已经完成的长度
		/// </summary>
		long DoneBytes{ get; }
		/// <summary>
		/// 最后一次获取时的Tick值
		/// </summary>
		long LastTick { get; }

		/// <summary>
		/// 开始下载
		/// </summary>
		/// <returns></returns>
		bool Download();
		/// <summary>
		/// 停止下载
		/// </summary>
		void StopDownload();
	}



}
