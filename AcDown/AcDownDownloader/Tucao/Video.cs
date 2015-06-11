using System.Collections.Generic;
using System.Xml.Serialization;

namespace Kaedei.AcDown.Downloader.Tucao
{
	[XmlRoot(ElementName = "durl")]
	public class Durl
	{
		[XmlElement(ElementName = "order")]
		public string Order { get; set; }
		[XmlElement(ElementName = "length")]
		public string Length { get; set; }
		[XmlElement(ElementName = "url")]
		public string Url { get; set; }
	}

	[XmlRoot(ElementName = "video")]
	public class VideoDetails
	{
		[XmlElement(ElementName = "result")]
		public string Result { get; set; }
		[XmlElement(ElementName = "timelength")]
		public string Timelength { get; set; }
		[XmlElement(ElementName = "src")]
		public string Src { get; set; }
		[XmlElement(ElementName = "durl")]
		public List<Durl> Durl { get; set; }
	}

}
