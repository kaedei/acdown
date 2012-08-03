using System;
using System.Collections.Generic;
using System.Text;

namespace Kaedei.AcDown.Interface.Downloader
{
	/// <summary>
	/// 漫画下载器，此类继承自CommonDownloader类并实现了IDownloader接口
	/// </summary>
	public class ComicDownloader : CommonDownloader
	{
		public override bool Download()
		{
			throw new NotImplementedException();
		}
	}
}
