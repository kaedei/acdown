using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Kaedei.AcPlay.Formatter
{
	/// <summary>
	/// Acfun Subtitle file Formatter
	/// </summary>
	public class AcfunFormatter : IFormatter
	{
		public StandardItem[] Format(string input)
		{
			List<StandardItem> items = new List<StandardItem>();
			Regex r = new Regex(@"{""c"":( |)""(?<c>.+?)"",( |)""m"":( |)""(?<m>.+?)""}", RegexOptions.Singleline);
			MatchCollection mc = r.Matches(input);
			foreach (Match m in mc)
			{
				var item = new StandardItem();
				item.player = "acfun";

				string c = m.Groups["c"].Value;
				var cs = c.Split(',');
				item.time = Double.Parse(cs[0]);
				item.color = Int64.Parse(cs[1]);
				item.mode = Int64.Parse(cs[2]);
				item.size = Int64.Parse(cs[3]);
				item.uid = cs[4];
				item.stamp = Double.Parse(cs[5]);

				//转换unicode
				item.message = Convert(m.Groups["m"].Value);
				//修复最后一个特殊弹幕出错
				if (item.message.StartsWith("{") && !item.message.EndsWith("}"))
					item.message += "\"}";

				items.Add(item);
			}
			return items.ToArray();
		}

		string Convert(string input)
		{
			char[] o = input.ToCharArray();
			StringBuilder sb = new StringBuilder(o.Length);
			for (int i = 0; i < o.Length; i++)
			{
				if (i > o.Length - 6)
				{
					sb.Append(o[i]);
					continue;
				}
				if (o[i] == '\\' && o[i + 1] == 'u')
				{
					string temp = string.Concat(o[i + 2], o[i + 3], o[i + 4], o[i + 5]);
					char newchar = (char)int.Parse(temp, System.Globalization.NumberStyles.HexNumber);
					sb.Append(newchar);
					i += 5;
				}
				else
				{
					sb.Append(o[i]);
				}
			}
			return sb.ToString();
		}
	}
}
