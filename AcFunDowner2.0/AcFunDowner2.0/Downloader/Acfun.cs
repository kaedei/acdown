using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;

namespace Kaedei.AcDown.Downloader
{
	/// <summary>
	/// AcFun下载支持插件
	/// </summary>
	public class AcFunPlugin :IAcdownPluginInfo
	{
		#region IAcdownPluginInfo 成员

		public string Name
		{
			get { return @"AcFunDownloader"; }
		}

		public string Author
		{
			get { return "Kaedei Software"; }
		}

		public Version Version
		{
			get { return new Version(1,0,0,0); }
		}

		public string Describe
		{
			get { return @"Acfun.cn下载插件"; }
		}

		public string SupportUrl
		{
			get { return @"http://blog.sina.com.cn/kaedei"; }
		}

		public IDownloader CreateDownloader()
		{
			return new Acfun();
		}

		public bool CheckUrl(string url)
		{
			Regex r = new Regex(@"http://(acfun\.cn|.*?)/html/(music|anime|game|ent|dy|zj)/\w+/\w+\.html");
			if (r.Match(url).Success)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion
	}

	/// <summary>
	/// Acfun下载器
	/// </summary>
	public class Acfun : IDownloader
	{

		#region IDownloader 成员

		public Guid TaskId
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public DelegateContainer delegates
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public long TotalLength
		{
			get { throw new NotImplementedException(); }
		}

		public long DoneBytes
		{
			get { throw new NotImplementedException(); }
		}

		public long LastTick
		{
			get { throw new NotImplementedException(); }
		}

		public int PartCount
		{
			get { throw new NotImplementedException(); }
		}

		public string Url
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public string FilePath
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public void DownloadVideo()
		{
			throw new NotImplementedException();
		}

		public bool DownloadSub()
		{
			throw new NotImplementedException();
		}

		public void StopDownloadVideo()
		{
			throw new NotImplementedException();
		}

		public string Info
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public DownloadStatus Status
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public string VideoTitle
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		#endregion
	}
}
