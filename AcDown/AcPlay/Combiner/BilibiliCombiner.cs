using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Kaedei.AcPlay.Combiner
{
	public class BilibiliCombiner : ICombiner
	{
		#region ICombiner Members

		public string Combine(List<StandardItem> addItems)
		{
			string xml = @"<?xml version=""1.0"" encoding=""UTF-8""?><i><chatserver>chat.bilibili.com</chatserver><chatid>10000</chatid><source>k-v</source></i>";
			var doc = new XmlDocument();
			doc.LoadXml(xml);

			var node = doc.SelectSingleNode("i");
			

			for (int i = 0; i < addItems.Count; i++)
			{
				var newnode = doc.CreateElement("d");
				var item = addItems[i];
				newnode.InnerText = item.message;
				
				var sb = new StringBuilder();

				sb.Append(item.time + ",");
				sb.Append(item.mode + ",");
				sb.Append(item.size + ",");
				sb.Append(item.color + ",");
				sb.Append(item.stamp + ",");
				sb.Append(item.items.ContainsKey("pool") ? item.items["pool"] : "0");
				sb.Append(item.uid + ",");
				sb.Append(item.id);
				newnode.SetAttribute("p", sb.ToString());
				
				node.AppendChild(newnode);

			}

			var ms = new MemoryStream();
			var writer = XmlWriter.Create(ms);
			doc.Save(writer);
			ms.Position = 0;
			return Encoding.UTF8.GetString(ms.ToArray());
		}

		#endregion
	}
}
