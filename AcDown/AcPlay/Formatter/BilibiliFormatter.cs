using System;
using System.Collections.Generic;
using System.Xml;

namespace Kaedei.AcPlay.Formatter
{
	public class BilibiliFormatter : IFormatter
	{
		public StandardItem[] Format(string input)
		{
			List<StandardItem> items = new List<StandardItem>();
			var doc = new XmlDocument();
			doc.LoadXml(input);

			var children = doc.SelectSingleNode("i").ChildNodes;
			foreach (XmlNode node in children)
			{
				if (node.Name.Equals("d", StringComparison.CurrentCultureIgnoreCase))
				{
					var p = (node as XmlElement).GetAttribute("p");
					var attributes = p.Split(',');
					//<d p="2796.1999511719,1,25,16776960,1313163329,0,Dd613a91,43665165">内容</d>
					//第一个参数是弹幕出现的时间 以秒数为单位。
					//第二个参数是弹幕的模式1..3 滚动弹幕 4底端弹幕 5顶端弹幕 6.逆向弹幕 7精准定位 8高级弹幕
					//第三个参数是字号， 12非常小,16特小,18小,25中,36大,45很大,64特别大
					//第四个参数是字体的颜色 以HTML颜色的十位数为准
					//第五个参数是Unix格式的时间戳。基准时间为 1970-1-1 08:00:00
					//第六个参数是弹幕池 0普通池 1字幕池 2特殊池 【目前特殊池为高级弹幕专用】;
					//第七个参数是发送者的ID，用于“屏蔽此弹幕的发送者”功能
					//第八个参数是弹幕在弹幕数据库中rowID 用于“历史弹幕”功能。
					var item = new StandardItem();
					item.player = "bilibili";
					item.time = double.Parse(attributes[0]);
					item.mode = long.Parse(attributes[1]);
					item.size = long.Parse(attributes[2]);
					item.color = long.Parse(attributes[3]);
					item.stamp = double.Parse(attributes[4]);
					item.items["pool"] = attributes[5];
					item.uid = attributes[6];
					if (attributes.Length >= 8)
						item.id = long.Parse(attributes[7]);
					else
						item.id = 0;

					item.message = (node as XmlElement).InnerText;
					items.Add(item);
				}
			}
			return items.ToArray();
		}

	}
}
