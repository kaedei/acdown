using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;

namespace Kaedei.AcDown.Interface
{
	/// <summary>
	/// 可序列化的字典
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	public class SerializableDictionary<TKey, TValue>
			  : Dictionary<TKey, TValue>, IXmlSerializable
	{
		#region IXmlSerializable 成员

		public XmlSchema GetSchema()
		{
			return null;
		}

		private string _elementName = "AcDownPluginSettings";
		public SerializableDictionary() { }
		public SerializableDictionary(string elementName)
		{
			_elementName = elementName;
		}

		/// <summary>
		/// 反序列化
		/// </summary>
		/// <param name="reader"></param>
		public void ReadXml(XmlReader reader)
		{
			XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
			XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
			if (reader.IsEmptyElement || !reader.Read())
			{
				return;
			}
			while (reader.NodeType != XmlNodeType.EndElement)
			{
				reader.ReadStartElement(_elementName);

				reader.ReadStartElement("Key");
				TKey key = (TKey)keySerializer.Deserialize(reader);
				reader.ReadEndElement();
				reader.ReadStartElement("Value");
				TValue value = (TValue)valueSerializer.Deserialize(reader);
				reader.ReadEndElement();

				reader.ReadEndElement();
				reader.MoveToContent();
				this.Add(key, value);
			}
			reader.ReadEndElement();
		}

		/// <summary>
		/// 序列化
		/// </summary>
		/// <param name="writer"></param>
		public void WriteXml(XmlWriter writer)
		{
			XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
			XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

			foreach (TKey key in this.Keys)
			{
				writer.WriteStartElement(_elementName);

				writer.WriteStartElement("Key");
				keySerializer.Serialize(writer, key);
				writer.WriteEndElement();
				writer.WriteStartElement("Value");
				valueSerializer.Serialize(writer, this[key]);
				writer.WriteEndElement();

				writer.WriteEndElement();
			}
		}

		#endregion
	}
}
