using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml;

namespace Kaedei.AcDown.Interface
{
	[Serializable()]
	public class TaskInfo : IXmlSerializable
	{
		public TaskInfo()
		{
			//初始化
			FilePath = new List<string>();
			SubFilePath = new List<string>();
			Settings = new SerializableDictionary<string, string>();
		}

		private Guid _taskid;
		/// <summary>
		/// 任务Id
		/// </summary>
		public Guid TaskId
		{
			get
			{
				if (_taskid == null)
					_taskid = Guid.NewGuid();
				return _taskid;
			}
			set
			{
				_taskid = value;
			}
		}


		/// <summary>
		/// Downloader所属插件名称
		/// </summary>
		public string PluginName { get; set; }

		/// <summary>
		/// Downloader所属插件
		/// </summary>
		public IPlugin BasePlugin { get; set; }


		private IDownloader resourceDownloader;
		/// <summary>
		/// 包装的Downloader对象
		/// </summary>
		public IDownloader Downloader { get { return resourceDownloader; } }


		/// <summary>
		/// 任务名称
		/// </summary>
		public string Title { get; set; }


		/// <summary>
		/// 下载状态
		/// </summary>
		public DownloadStatus Status { get; set; }


		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime { get; set; }

		/// <summary>
		/// 完成时间
		/// </summary>
		public DateTime FinishTime { get; set; }


		/// <summary>
		/// 保存目录
		/// </summary>
		public DirectoryInfo SaveDirectory { get; set; }

		/// <summary>
		/// 分段总数
		/// </summary>
		public int PartCount { get; set; }

		/// <summary>
		/// 当前分段
		/// </summary>
		public int CurrentPart { get; set; }

		/// <summary>
		/// 文件列表
		/// </summary>
		public List<string> FilePath { get; set; }

		/// <summary>
		/// 子文件列表
		/// </summary>
		public List<string> SubFilePath { get; set; }


		/// <summary>
		/// 任务URL
		/// </summary>
		public string Url { get; set; }


		/// <summary>
		/// 下载类型
		/// </summary>
		public DownloadType DownloadTypes { get; set; }

		/// <summary>
		/// 是否解析关联的任务
		/// </summary>
		public bool ParseRelated { get; set; }

		/// <summary>
		/// 自动应答设置
		/// </summary>
		public List<AutoAnswer> AutoAnswer { get; set; }

		///是否启用缓存提取
		public bool ExtractCache { get; set; }

		/// <summary>
		/// 应用的代理服务器
		/// </summary>
		public WebProxy Proxy { get; set; }

		/// <summary>
		/// 速度限制
		/// </summary>
		public int SpeedLimit { get; set; }

		/// <summary>
		/// 最后一次错误
		/// </summary>
		public Exception LastError { get; set; }

		/// <summary>
		/// 引用页
		/// </summary>
		public string SourceUrl { get; set; }

		/// <summary>
		/// 注释
		/// </summary>
		public string Comment { get; set; }


		/// <summary>
		/// 读取或设置一个Boolean值，指示此任务是否由其他任务所添加
		/// </summary>
		public bool IsBeAdded { get; set; }

		/// <summary>
		/// 读取或设置一个Boolean值，指示此任务是否仅有部分完成
		/// </summary>
		public bool PartialFinished { get; set; }

		/// <summary>
		/// 读取或设置一个字符串，指示此任务"仅部分完成"的详细信息
		/// </summary>
		public string PartialFinishedDetail { get; set; }

		private string _hash = "";
		/// <summary>
		/// 任务的散列值
		/// </summary>
		public string Hash
		{
			get
			{
				if (BasePlugin != null)
				{
					_hash = BasePlugin.GetHash(Url);
				}
				return _hash;
			}
			set { _hash = value; }
		}

		/// <summary>
		/// 关联的UI Item
		/// </summary>
		public Object UIItem { get; set; }

		/// <summary>
		/// 插件存储的设置
		/// </summary>
		public SerializableDictionary<string, string> Settings { get; set; }

		/// <summary>
		/// 开始任务
		/// </summary>
		public bool Start(DelegateContainer delegates)
		{
			if (BasePlugin == null)
			{
				this.Status = DownloadStatus.出现错误;
				throw new Exception("Plugin Not Found");
			}
			resourceDownloader = BasePlugin.CreateDownloader();
			resourceDownloader.Info = this;
			resourceDownloader.delegates = delegates;
			this.PartialFinished = false;
			this.PartialFinishedDetail = "";
			return resourceDownloader.Download();
		}

		/// <summary>
		/// 停止任务
		/// </summary>
		public void Stop()
		{
			if (resourceDownloader != null)
				resourceDownloader.StopDownload();
		}


		/// <summary>
		/// 销毁关联的IDownloader对象
		/// </summary>
		public void DisposeDownloader()
		{
			resourceDownloader = null;
		}


		internal double _progress;
		/// <summary>
		/// 任务下载进度
		/// </summary>
		/// <returns></returns>
		public double GetProgress()
		{
			if (resourceDownloader != null)
			{
				_progress = (double)resourceDownloader.DoneBytes / (double)resourceDownloader.TotalLength;
				if (_progress < 0) _progress = 0.00;
				else if (_progress > 1.00) _progress = 1.00;
			}
			return _progress;
		}


		/// <summary>
		/// 下载速度之差
		/// </summary>
		/// <returns></returns>
		public long GetTickCount()
		{
			if (resourceDownloader != null)
				return resourceDownloader.DoneBytes - resourceDownloader.LastTick;
			return 0;
		}

		public override string ToString()
		{
			string template = "状态:{0} 标题:{1} 网址:{2} 创建时间:{3} 引用页:{4} 注释:{5}";
			return string.Format(template, Status.ToString(), Title, Url, CreateTime.ToShortDateString(), SourceUrl, Comment);
		}

		#region IXmlSerializable 成员

		public System.Xml.Schema.XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(System.Xml.XmlReader reader)
		{
			var s = new XmlSerializer(typeof(string));
			var b = new XmlSerializer(typeof(bool));
			var l = new XmlSerializer(typeof(List<AutoAnswer>));

			if (reader.IsEmptyElement || !reader.Read())
			{
				return;
			}
			while (reader.NodeType != XmlNodeType.EndElement)
			{
				reader.ReadStartElement("TaskInfo");

				//taskid
				reader.ReadStartElement("TaskId");
				TaskId = new Guid((string)s.Deserialize(reader));
				reader.ReadEndElement();

				//plugin name
				reader.ReadStartElement("PluginName");
				PluginName = (string)s.Deserialize(reader);
				reader.ReadEndElement();

				//url
				reader.ReadStartElement("Url");
				Url = (string)s.Deserialize(reader);
				reader.ReadEndElement();

				//title
				reader.ReadStartElement("Title");
				Title = (string)s.Deserialize(reader);
				reader.ReadEndElement();

				//Status
				reader.ReadStartElement("Status");
				Status = (DownloadStatus)Enum.Parse(typeof(DownloadStatus), (string)s.Deserialize(reader), true);
				reader.ReadEndElement();

				//createtime
				reader.ReadStartElement("CreateTime");
				CreateTime = DateTime.Parse((string)s.Deserialize(reader));
				reader.ReadEndElement();

				//finishtime
				if (reader.IsStartElement("FinishTime"))
				{
					reader.ReadStartElement("FinishTime");
					FinishTime = DateTime.Parse((string)s.Deserialize(reader));
					reader.ReadEndElement();
				}

				//savedirectory
				reader.ReadStartElement("SaveDirectory");
				SaveDirectory = new DirectoryInfo((string)s.Deserialize(reader));
				reader.ReadEndElement();

				//partcount
				reader.ReadStartElement("PartCount");
				PartCount = Int32.Parse((string)s.Deserialize(reader));
				reader.ReadEndElement();

				//currentpart
				reader.ReadStartElement("CurrentPart");
				CurrentPart = Int32.Parse((string)s.Deserialize(reader));
				reader.ReadEndElement();

				//is be added
				reader.ReadStartElement("IsBeAdded");
				IsBeAdded = (bool)b.Deserialize(reader);
				reader.ReadEndElement();

				//partialfinished
				reader.ReadStartElement("PartialFinished");
				PartialFinished = (bool)b.Deserialize(reader);
				reader.ReadEndElement();

				//partialfinished detail
				reader.ReadStartElement("PartialFinishedDetail");
				PartialFinishedDetail = (string)s.Deserialize(reader);
				reader.ReadEndElement();

				//autoanswer
				reader.ReadStartElement("AutoAnswers");
				this.AutoAnswer = (List<AutoAnswer>)l.Deserialize(reader);
				reader.ReadEndElement();

				//extract cache
				reader.ReadStartElement("ExtractCache");
				ExtractCache = (bool)b.Deserialize(reader);
				reader.ReadEndElement();

				//filepath
				reader.ReadStartElement("Files");
				FilePath = new List<string>();
				while (reader.IsStartElement("File"))
				{
					reader.ReadStartElement("File");
					FilePath.Add((string)s.Deserialize(reader));
					reader.ReadEndElement();
				}
				if (reader.NodeType == XmlNodeType.EndElement)
					reader.ReadEndElement();

				//subfilepath
				SubFilePath = new List<string>();
				reader.ReadStartElement("SubFiles");

				while (reader.IsStartElement("SubFile"))
				{
					reader.ReadStartElement("SubFile");
					SubFilePath.Add((string)s.Deserialize(reader));
					reader.ReadEndElement();
				}
				if (reader.NodeType == XmlNodeType.EndElement)
					reader.ReadEndElement();




				//DownloadType
				reader.ReadStartElement("DownloadType");
				DownloadTypes = (DownloadType)Enum.Parse(typeof(DownloadType), (string)s.Deserialize(reader), true);
				reader.ReadEndElement();

				//proxy
				XmlSerializer sProxy = new XmlSerializer(typeof(AcDownProxy));
				reader.ReadStartElement("Proxy");
				AcDownProxy pxy = (AcDownProxy)sProxy.Deserialize(reader);
				if (pxy.Address == "" && pxy.Port == 0)
					Proxy = null;
				else
					Proxy = pxy.ToWebProxy();
				reader.ReadEndElement();

				//sourceUrl
				reader.ReadStartElement("SourceUrl");
				SourceUrl = (string)s.Deserialize(reader);
				reader.ReadEndElement();

				//Comment
				reader.ReadStartElement("Comment");
				Comment = (string)s.Deserialize(reader);
				reader.ReadEndElement();

				//Hash
				reader.ReadStartElement("Hash");
				_hash = (string)s.Deserialize(reader);
				reader.ReadEndElement();

				//Progress
				reader.ReadStartElement("Progress");

				if (!double.TryParse((string)s.Deserialize(reader), out _progress))
					_progress = 0;
				if (_progress > 1.0) _progress = 1.0;
				if (_progress < 0.0) _progress = 0;
				if (double.IsNaN(_progress)) _progress = 0;
				reader.ReadEndElement();

				//settings
				reader.ReadStartElement("Settings");
				if (!reader.IsEmptyElement)
				{
					XmlSerializer sSettings = new XmlSerializer(typeof(SerializableDictionary<string, string>));
					Settings = (SerializableDictionary<string, string>)sSettings.Deserialize(reader);
				}
				else
				{
					Settings = new SerializableDictionary<string, string>();
					reader.Read();
				}
				reader.ReadEndElement();

				//结束读取
				reader.ReadEndElement();
				reader.MoveToContent();
			}
			reader.ReadEndElement();
		}

		public void WriteXml(System.Xml.XmlWriter writer)
		{
			var s = new XmlSerializer(typeof(string));
			var b = new XmlSerializer(typeof(bool));
			var l = new XmlSerializer(typeof(List<AutoAnswer>));

			writer.WriteStartElement("TaskInfo");

			//taskid
			writer.WriteStartElement("TaskId");
			s.Serialize(writer, TaskId.ToString());
			writer.WriteEndElement();

			//pluginname
			writer.WriteStartElement("PluginName");
			s.Serialize(writer, PluginName);
			writer.WriteEndElement();

			//url
			writer.WriteStartElement("Url");
			s.Serialize(writer, Url);
			writer.WriteEndElement();

			//title
			writer.WriteStartElement("Title");
			s.Serialize(writer, Title);
			writer.WriteEndElement();

			//status
			writer.WriteStartElement("Status");
			DownloadStatus tmpds = Status;
			if (tmpds == DownloadStatus.正在下载 || tmpds == DownloadStatus.正在停止 || tmpds == DownloadStatus.等待开始)
				tmpds = DownloadStatus.已经停止;
			s.Serialize(writer, tmpds.ToString());
			writer.WriteEndElement();

			//createtime
			writer.WriteStartElement("CreateTime");
			s.Serialize(writer, CreateTime.ToString());
			writer.WriteEndElement();

			//finishtime
			if (FinishTime != null)
			{
				writer.WriteStartElement("FinishTime");
				s.Serialize(writer, FinishTime.ToString());
				writer.WriteEndElement();
			}

			//savedirectory
			writer.WriteStartElement("SaveDirectory");
			s.Serialize(writer, SaveDirectory.ToString());
			writer.WriteEndElement();

			//PartCount
			writer.WriteStartElement("PartCount");
			s.Serialize(writer, PartCount.ToString());
			writer.WriteEndElement();

			//CurrentPart
			writer.WriteStartElement("CurrentPart");
			s.Serialize(writer, CurrentPart.ToString());
			writer.WriteEndElement();

			//is be added
			writer.WriteStartElement("IsBeAdded");
			b.Serialize(writer, IsBeAdded);
			writer.WriteEndElement();

			//partialfinished
			writer.WriteStartElement("PartialFinished");
			b.Serialize(writer, PartialFinished);
			writer.WriteEndElement();

			//partialfinished detail
			writer.WriteStartElement("PartialFinishedDetail");
			s.Serialize(writer, PartialFinishedDetail);
			writer.WriteEndElement();

			//autoanswer
			writer.WriteStartElement("AutoAnswers");
			l.Serialize(writer, AutoAnswer);
			writer.WriteEndElement();

			//extract cache
			writer.WriteStartElement("ExtractCache");
			b.Serialize(writer, ExtractCache);
			writer.WriteEndElement();

			//FilePath
			writer.WriteStartElement("Files");
			foreach (string item in FilePath)
			{
				writer.WriteStartElement("File");
				s.Serialize(writer, item);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();

			//SubFilePath
			writer.WriteStartElement("SubFiles");
			foreach (string item in SubFilePath)
			{
				writer.WriteStartElement("SubFile");
				s.Serialize(writer, item);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();

			//DownloadType
			writer.WriteStartElement("DownloadType");
			s.Serialize(writer, DownloadTypes.ToString("D"));
			writer.WriteEndElement();

			//proxy
			XmlSerializer sProxy = new XmlSerializer(typeof(AcDownProxy));
			writer.WriteStartElement("Proxy");
			sProxy.Serialize(writer, new AcDownProxy().FromWebProxy(Proxy));
			writer.WriteEndElement();

			//source url
			writer.WriteStartElement("SourceUrl");
			s.Serialize(writer, SourceUrl);
			writer.WriteEndElement();

			//comment
			writer.WriteStartElement("Comment");
			s.Serialize(writer, Comment);
			writer.WriteEndElement();

			//hash
			writer.WriteStartElement("Hash");
			s.Serialize(writer, Hash);
			writer.WriteEndElement();

			//Progress
			writer.WriteStartElement("Progress");
			s.Serialize(writer, GetProgress().ToString());
			writer.WriteEndElement();

			//settings
			XmlSerializer sSettings = new XmlSerializer(typeof(SerializableDictionary<string, string>));
			writer.WriteStartElement("Settings");
			sSettings.Serialize(writer, Settings);
			writer.WriteEndElement();

			writer.WriteEndElement();

		}

		#endregion
	}

}
