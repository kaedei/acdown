using System;
using System.Net;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Kaedei.AcPlay
{

	[Serializable()]
	public class AcPlayConfiguration
	{
		public AcPlayConfiguration()
		{
			//兼容2.2
			SpeedLimit = 0;
		}

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

		[OptionalField()]
		public int SpeedLimit = 0;

		[OptionalField()]
		public AcDownProxy Proxy;

		[XmlIgnore()]
		public WebProxy WebProxy
		{
			get
			{
				return Proxy != null ? Proxy.ToWebProxy() : null;
			}
		}

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
