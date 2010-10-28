using System;
using System.Xml.Serialization;

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
		出现错误 = 4
		//正在停止=5
	}

	/// <summary>
	/// 下载适配器接口
	/// </summary>
	public interface IDownloader
	{
		Guid TaskId { get; set; }
		DelegateContainer delegates{ get; set; }

		long TotalLength{ get; }
		long DoneBytes{ get; }
		long LastTick { get; }
		int PartCount { get; }

		string Url{ get; set; }
		string[] FilePath{ get; set; }
		string FolderPath { get; set; }

		void DownloadVideo();
		bool DownloadSub();
		void StopDownloadVideo();

		string Info{ get; set; }
		DownloadStatus Status{ get; set; }
		string VideoTitle{ get; set; }
	}



}
