using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Kaedei.AcDown.Interface
{

	/// <summary>
	/// 包装委托类
	/// </summary>
	public class DelegateContainer
	{
		public DelegateContainer() { }
		public DelegateContainer(AcTaskDelegate startDele,
							 AcTaskDelegate newPartDele,
							 AcTaskDelegate refreshDele,
							 AcTaskDelegate tipTextDele,
							 AcTaskDelegate finishDele,
							 AcTaskDelegate errorDele)
		{
			Start += startDele;
			NewPart += newPartDele;
			Refresh += refreshDele;
			TipText += tipTextDele;
			Finish += finishDele;
			Error += errorDele;
		}
		public AcTaskDelegate Start { get; set; }
		public AcTaskDelegate NewPart { get; set; }
		public AcTaskDelegate Refresh { get; set; }
		public AcTaskDelegate TipText { get; set; }
		public AcTaskDelegate Finish { get; set; }
		public AcTaskDelegate Error { get; set; }
	}
 

	#region 委托参数

	public delegate void AcTaskDelegate(object para);
	public class ParaStart
	{
		public ParaStart(TaskInfo task) { Task = task; }
		public TaskInfo Task { get; set; }
	}
	public class ParaNewPart
	{
		public ParaNewPart(TaskInfo task, Int32 partNum) { Task = task; PartNumber = partNum; }
		public TaskInfo Task { get; set; }
		public Int32 PartNumber { get; set; }
	}
	public class ParaRefresh
	{
		public ParaRefresh(TaskInfo task) { Task = task; }
		public TaskInfo Task { get; set; }
	}
	public class ParaTipText
	{
		public ParaTipText(TaskInfo task, string tip) { Task = task; TipText = tip; }
		public TaskInfo Task { get; set; }
		public string TipText { get; set; }
	}
	public class ParaFinish
	{
		public ParaFinish(TaskInfo task, bool isSuccess) { Task = task; Successed = isSuccess; }
		public TaskInfo Task { get; set; }
		public bool Successed { get; set; }
	}
	public class ParaError
	{
		public ParaError(TaskInfo task, Exception excp) { Task = task; E = excp; }
		public TaskInfo Task { get; set; }
		public Exception E { get; set; }
	}

	#endregion


}
