using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Kaedei.AcPlay.Formatter
{
	public class DoupaoFormatter:IFormatter
	{

		public StandardItem[] Format(string input)
		{
			var items = new List<StandardItem>();
			Regex r = new Regex(@"{""group_item_count"":(\d+),""seq"":(\d+),""iid"":(\d+),""color"":(\d+),""altitude"":(\d+),""effect"":(\d+),""pos"":(\d+),""group_seq"":(\d+),""user_code"":""([\w\-\+]+)"",""replay_time"":(\d+),""ng"":(\d+),""data"":""(.+?)"",""commit_time"":(\d+),""size"":(\d+)}");
			MatchCollection mc = r.Matches(input);
			foreach (Match m in mc)
			{
				var item = new StandardItem();
				item.player = "doupao";

				item.time = Double.Parse(m.Groups[10].Value) / 1000;
				item.color = Int64.Parse(m.Groups[4].Value);
				item.mode = Int64.Parse(m.Groups[7].Value);
				item.size = Int64.Parse(m.Groups[14].Value);
				switch (item.size)
				{
					case 0:
						item.size = 18;
						break;
					case 1:
						item.size = 25;
						break;
					case 2:
						item.size = 32;
						break;
				}
				item.uid = m.Groups[8].Value;
				item.stamp = Double.Parse(m.Groups[13].Value);

				item.message = Convert(m.Groups[12].Value);

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
