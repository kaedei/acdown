using System;
using System.Xml.Serialization;
using Kaedei.AcDown.Interface;

namespace Kaedei.AcDown.Interface.AcPlay
{
	[Serializable()]
	public class AcPlayConfiguration
	{
		public static AcPlayConfiguration Config;
		[XmlIgnore()]
		public string StartupPath;

		public string PlayerName;
		public string PlayerUrl;
		public int HttpServerPort;
		public int ProxyServerPort;
		[XmlArray("Videos")]
		public Video[] Videos;
		[XmlArray("Subtitles")]
		public string[] Subtitles;
		public SerializableDictionary<string, string> ExtraConfig;
	}

	[Serializable()]
	public class Video
	{
		public int Order;
		public string FileName;
		public int Length;
	}
}
