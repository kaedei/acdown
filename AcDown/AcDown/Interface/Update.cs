using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Kaedei.AcDown.Interface
{
	[Serializable()]
	public class Update
	{
		[XmlAttribute("Version")]
		public string Version;

		[XmlAttribute("MinSupportedVersion")]
		public string MinSupportedVersion;

		[XmlElement("CompanyName")]
		public string CompanyName;

		[XmlElement("PluginName")]
		public string PluginName;

		[XmlElement("UpdateXmlUrl")]
		public string UpdateXmlUrl;

		[XmlElement("EntryFile")]
		public string EntryFile;

		[XmlElement("FilePackage")]
		public String FilePackage;

	}
}
