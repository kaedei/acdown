using System;
using System.Collections.Generic;
using System.Text;

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
		public ParaStart(Guid task) { TaskId = task; }
		public Guid TaskId { get; set; }
	}
	public class ParaNewPart
	{
		public ParaNewPart(Guid task, Int32 partNum) { TaskId = task; PartNumber = partNum; }
		public Guid TaskId { get; set; }
		public Int32 PartNumber { get; set; }
	}
	public class ParaRefresh
	{
		public ParaRefresh(Guid task) { TaskId = task; }
		public Guid TaskId { get; set; }
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


}
