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

		/// <summary>
		/// 创建一个新的自动应答设置
		/// </summary>
		public AutoAnswer() { }

		/// <summary>
		/// 创建一个新的自动应答设置
		/// </summary>
		/// <param name="prefix">前缀</param>
		/// <param name="identify">标识</param>
		/// <param name="description">描述</param>
		public AutoAnswer(string prefix, string identify, string description)
		{
			Prefix = prefix;
			Identify = identify;
			Description = description;
		}

		/// <summary>
		/// 检查指定前缀的标识是否存在于自动应答列表中
		/// </summary>
		/// <param name="autoAnswers">自动应答列表</param>
		/// <param name="prefix">需要检查的前缀</param>
		/// <param name="identify">需要对比的标识</param>
		/// <returns></returns>
		public static Boolean IsInAutoAnswers(List<AutoAnswer> autoAnswers, string prefix, string identify)
		{
			if (autoAnswers != null)
			{
				foreach (AutoAnswer item in autoAnswers)
				{
					if (item.Prefix.Equals(prefix, StringComparison.CurrentCultureIgnoreCase))
					{
						if (identify.Equals(item.Identify, StringComparison.CurrentCultureIgnoreCase))
						{
							return true;
						}
					}
				}
			}
			return false;
		}
	}
}
