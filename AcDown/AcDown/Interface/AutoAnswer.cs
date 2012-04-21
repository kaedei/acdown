using System;
using System.Collections.Generic;
using System.Text;

namespace Kaedei.AcDown.Interface
{
	/// <summary>
	/// 自动应答设置
	/// </summary>
	[Serializable()]
	public class AutoAnswer
	{
		/// <summary>
		/// 前缀
		/// </summary>
		public string Prefix { get; set; }
		/// <summary>
		/// 自动应答标识
		/// </summary>
		public string Identify { get; set; }
		/// <summary>
		/// 自动应答描述
		/// </summary>
		public string Description { get; set; }

		  public AutoAnswer() { }
		public AutoAnswer(string prefix, string identify, string description)
		{
			Prefix = prefix;
			Identify = identify;
			Description = description;
		}
	}
}
