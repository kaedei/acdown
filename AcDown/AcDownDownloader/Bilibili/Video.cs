using System.Collections.Generic;
using System.Xml.Serialization;

namespace Kaedei.AcDown.Downloader.Bilibili
{
	[XmlRoot(ElementName = "backup_url")]
	public class Backup_url
	{
		[XmlElement(ElementName = "url")]
		public string Url { get; set; }
	}

	[XmlRoot(ElementName = "durl")]
	public class Durl
	{
		[XmlElement(ElementName = "order")]
		public string Order { get; set; }
		[XmlElement(ElementName = "length")]
		public string Length { get; set; }
		[XmlElement(ElementName = "size")]
		public string Size { get; set; }
		[XmlElement(ElementName = "url")]
		public string Url { get; set; }
		[XmlElement(ElementName = "backup_url")]
		public Backup_url Backup_url { get; set; }
	}

	[XmlRoot(ElementName = "video")]
	public class Video
	{
		[XmlElement(ElementName = "result")]
		public string Result { get; set; }
		[XmlElement(ElementName = "timelength")]
		public string Timelength { get; set; }
		[XmlElement(ElementName = "format")]
		public string Format { get; set; }
		[XmlElement(ElementName = "accept_format")]
		public string Accept_format { get; set; }
		[XmlElement(ElementName = "accept_quality")]
		public string Accept_quality { get; set; }
		[XmlElement(ElementName = "from")]
		public string From { get; set; }
		[XmlElement(ElementName = "seek_param")]
		public string Seek_param { get; set; }
		[XmlElement(ElementName = "seek_type")]
		public string Seek_type { get; set; }
		[XmlElement(ElementName = "src")]
		public string Src { get; set; }
		[XmlElement(ElementName = "durl")]
		public List<Durl> Durl { get; set; }
	}

}