using System;
using System.Xml.Serialization;

namespace Kaedei.AcDown.Interface
{

	#region 委托参数
	public delegate void AcTaskDelegate(object para);
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
		public DelegateContainer(AcTaskDelegate startDele,
							 AcTaskDelegate newPartDele,
							 AcTaskDelegate tipProcessDele,
							 AcTaskDelegate tipTextDele,
							 AcTaskDelegate finishDele,
							 AcTaskDelegate errorDele)
		{
			Start += startDele;
			NewPart += newPartDele;
			TipProcess += tipProcessDele;
			TipText += tipTextDele;
			Finish += finishDele;
			Error += errorDele;
		}
		public AcTaskDelegate Start { get; set; }
		public AcTaskDelegate NewPart { get; set; }
		public AcTaskDelegate TipProcess { get; set; }
		public AcTaskDelegate TipText { get; set; }
		public AcTaskDelegate Finish { get; set; }
		public AcTaskDelegate Error { get; set; }
	}

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
		int LastTick { get; }
		int PartCount { get; }

		string Url{ get; set; }
		string FilePath{ get; set; }

		bool CheckUrl(string url);
		void DownloadVideo();
		bool DownloadSub();
		void StopDownloadVideo();

		Video Info{ get; set; }
		DownloadStatus Status{ get; set; }
		string VideoTitle{ get; set; }
	}

	[Serializable]
	public class Video
	{
		[XmlIgnore()]
		public string Identify;
	}

	[Serializable]
	public class Part
	{
		public Int32 order = 0;
		public Int32 length = 0;
		public string url = "";
	}
}
