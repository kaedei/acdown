using System;
using System.Xml.Serialization;

namespace Kaedei.Lavola
{

	#region 委托参数
	public delegate void LvlTaskDelegate(object para);
	public class ParaStart
	{
		public ParaStart(Guid task) { TaskId = task; }
		public Guid TaskId { get; set; }
	}
	public class ParaNewPart
	{
		public ParaNewPart(Guid task, Int32 partNum) { TaskId = task; PartNumber = partNum; }
		public Guid TaskId { get; set; }
		public Int32 PartNumber { get; set; }
	}
	public class ParaTipProcess
	{
		public ParaTipProcess(Guid task, double process) { TaskId = task; Process = process; }
		public Guid TaskId { get; set; }
		public double Process { get; set; }
	}
	public class ParaTipText
	{
		public ParaTipText(Guid task, string tip) { TaskId = task; TipText = tip; }
		public Guid TaskId { get; set; }
		public string TipText { get; set; }
	}
	public class ParaFinish
	{
		public ParaFinish(Guid task, bool isSuccess) { TaskId = task; Successed = isSuccess; }
		public Guid TaskId { get; set; }
		public bool Successed { get; set; }
	}
	public class ParaError
	{
		public ParaError(Guid task, Exception excp) { TaskId = task; E = excp; }
		public Guid TaskId { get; set; }
		public Exception E { get; set; }
	}

	#endregion

	/// <summary>
	/// 包装委托类
	/// </summary>
	public class DelegateContainer
	{
		public DelegateContainer() { }
		public DelegateContainer(LvlTaskDelegate startDele,
							 LvlTaskDelegate newPartDele,
							 LvlTaskDelegate tipProcessDele,
							 LvlTaskDelegate tipTextDele,
							 LvlTaskDelegate finishDele,
							 LvlTaskDelegate errorDele)
		{
			Start += startDele;
			NewPart += newPartDele;
			TipProcess += tipProcessDele;
			TipText += tipTextDele;
			Finish += finishDele;
			Error += errorDele;
		}
		public LvlTaskDelegate Start { get; set; }
		public LvlTaskDelegate NewPart { get; set; }
		public LvlTaskDelegate TipProcess { get; set; }
		public LvlTaskDelegate TipText { get; set; }
		public LvlTaskDelegate Finish { get; set; }
		public LvlTaskDelegate Error { get; set; }
	}

	public enum DownloadStatus
	{
		等待开始 = 0,
		正在下载 = 1,
		已经停止 = 2,
		下载完成 = 3,
		出现错误 = 4
		//正在停止=5
	}

	interface IDownloader
	{
		DelegateContainer delegates { get; set; }
		long DoneBytes { get; set; }
		bool DownloadSub(string id, string title);
		string FilePathString { get; set; }
		string GetVideoId(string source);
		video GetVideoInfo(string videoId);
		string GetVideoTitle(string source);
		video Info { get; set; }
		long LastBytes { get; set; }
		int PartCount { get; }
		string PartName { get; set; }
		void Run();
		DownloadStatus Status { get; set; }
		void StopDownload();
		string SubfilePathString { get; set; }
		Guid TaskId { get; set; }
		long TotalBytes { get; set; }
		string Url { get; set; }
		string VideoTitle { get; }
	}

	[Serializable]
	public abstract class video
	{
		
	}

	[Serializable]
	public class part
	{
		public Int32 order = 0;
		public Int32 length = 0;
		public string url = "";
	}
}
