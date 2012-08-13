﻿using System;
using Kaedei.AcDown.Interface;

namespace Kaedei.AcDown.Core
{
	/// <summary>
	/// UI委托包装类
	/// </summary>
	public class UIDelegateContainer : DelegateContainer
	{
		public UIDelegateContainer(AcTaskDelegate startDele,
							 AcTaskDelegate newPartDele,
							 AcTaskDelegate refreshDele,
							 AcTaskDelegate tipTextDele,
							 AcTaskDelegate finishDele,
							 AcTaskDelegate errorDele,
							 AcTaskDelegate newTaskDele,
							 AcTaskDelegate allFinishedDele)
			: base(newPartDele, refreshDele, tipTextDele, newTaskDele)
		{
			Start += startDele;
			Finish += finishDele;
			Error += errorDele;
			AllFinished += allFinishedDele;
		}

		/// <summary>
		/// 任务开始
		/// </summary>
		public AcTaskDelegate Start { get; set; }

		/// <summary>
		/// 任务完成
		/// </summary>
		public AcTaskDelegate Finish { get; set; }

		/// <summary>
		/// 任务出现错误
		/// </summary>
		public AcTaskDelegate Error { get; set; }

		/// <summary>
		/// 当前所有任务结束
		/// </summary>
		public AcTaskDelegate AllFinished { get; set; }

	}

	public class ParaFinish : DelegateParameter
	{
		public ParaFinish(TaskInfo task, bool isSuccess) { SourceTask = task; Successed = isSuccess; }
		public bool Successed { get; set; }
	}

	public class ParaError : DelegateParameter
	{
		public ParaError(TaskInfo task, Exception excp) { SourceTask = task; E = excp; }
		public Exception E { get; set; }
	}

	public class ParaStart : DelegateParameter
	{
		public ParaStart(TaskInfo task) { SourceTask = task; }
	}

}
