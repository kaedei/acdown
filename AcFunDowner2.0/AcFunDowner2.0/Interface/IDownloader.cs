using System;
using System.Xml.Serialization;
using System.IO;

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
		long LastTick { get; set; }
		int PartCount { get; }

		string Url{ get; set; }
		string[] FilePath{ get; set; }
		string[] SubFilePath { get; set; }
		DirectoryInfo SaveDirectory { get; set; }

		void DownloadVideo();
		void DownloadSub();
		void StopDownloadVideo();

		string Info{ get; }
		DownloadStatus Status{ get; }
		string VideoTitle{ get; }
	}



}
