using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;

namespace Kaedei.AcDown.Downloader
{
	public class Acfun : IDownloader
	{

		#region IDownloader 成员

		Guid IDownloader.TaskId
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

		DelegateContainer IDownloader.delegates
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

		long IDownloader.TotalLength
		{
			get { throw new NotImplementedException(); }
		}

		long IDownloader.DoneBytes
		{
			get { throw new NotImplementedException(); }
		}

		int IDownloader.LastTick
		{
			get { throw new NotImplementedException(); }
		}

		int IDownloader.PartCount
		{
			get { throw new NotImplementedException(); }
		}

		string IDownloader.Url
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

		string IDownloader.FilePath
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

		bool IDownloader.CheckUrl(string url)
		{
			throw new NotImplementedException();
		}

		void IDownloader.DownloadVideo()
		{
			throw new NotImplementedException();
		}

		bool IDownloader.DownloadSub()
		{
			throw new NotImplementedException();
		}

		void IDownloader.StopDownloadVideo()
		{
			throw new NotImplementedException();
		}

		Video IDownloader.Info
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

		DownloadStatus IDownloader.Status
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

		string IDownloader.VideoTitle
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
