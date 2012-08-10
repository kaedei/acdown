using System;
using System.Collections.Generic;
using System.Text;

namespace Kaedei.AcDown.Interface
{
	/// <summary>
	/// 下载类型
	/// </summary>
	[Flags(), Serializable()]
	public enum DownloadType
	{
		None = 0,
		Video = 1,
		Audio = 2,
		Picture = 4,
		Text = 8,
		Subtitle = 16,
		Comment = 32,
		All = Video | Audio | Picture | Text | Subtitle | Comment
	}

}
