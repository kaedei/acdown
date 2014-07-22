using System;
using System.Collections.Generic;
using System.Text;

namespace Kaedei.AcPlay.Combiner
{
	public class AcfunCombiner : ICombiner
	{

		public string Combine(List<StandardItem> addItems)
		{
			StringBuilder sb = new StringBuilder(addItems.Count * 10);
			sb.Append("[");

			for (int i = 0; i < addItems.Count; i++)
			{
				var item = addItems[i];
				sb.Append("{");
				sb.Append("\"c\": \"");
				sb.Append(item.time + ",");
				sb.Append(item.color + ",");
				sb.Append(item.mode + ",");
				sb.Append(item.size + ",");
				sb.Append(item.uid + ",");
				sb.Append(item.stamp);
				sb.Append("\", \"m\": \"");
				if (item.player.Equals("acfun", StringComparison.CurrentCultureIgnoreCase))
					sb.Append(item.message);
				else
					sb.Append(item.message.Replace('[', ' ').Replace(']', ' ').Replace('{', ' ').Replace('}', ' ').Replace('"', ' ').Replace('\\', ' '));
				sb.Append("\"}");
				if (i != addItems.Count - 1)
					sb.Append(", ");
			}

			sb.Append("]");
			return sb.ToString();
		}


	}
}
