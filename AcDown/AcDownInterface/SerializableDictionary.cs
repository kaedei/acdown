using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.IO;

namespace Kaedei.AcDown.Interface
{
	/// <summary>
	/// 可序列化的字典
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	[Serializable]
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

		/// <summary>
		/// 将字典中的内容序列化到一个字符串对象中
		/// </summary>
		public static string WriteToString(SerializableDictionary<TKey, TValue> dict)
		{
			var t = typeof(SerializableDictionary<TKey, TValue>);
			var serializer = new XmlSerializer(t);
			using (var ms = new MemoryStream())
			{
				serializer.Serialize(ms, dict);
				return Encoding.UTF8.GetString(ms.ToArray());
			}
		}

		/// <summary>
		/// 将一个字符串对象反序列化为字典
		/// </summary>
		public static SerializableDictionary<TKey, TValue> LoadFromString(string content)
		{
			var t = typeof(SerializableDictionary<TKey, TValue>);
			var serializer = new XmlSerializer(t);
			using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(content)))
			{
				return (SerializableDictionary<TKey, TValue>)serializer.Deserialize(ms);
			}
		}
	}
}
