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
		/// <summary>
		/// 无
		/// </summary>
		None = 0,
		/// <summary>
		/// 视频
		/// </summary>
		Video = 1,
		/// <summary>
		/// 音频/音乐
		/// </summary>
		Audio = 2,
		/// <summary>
		/// 图像/照片
		/// </summary>
		Picture = 4,
		/// <summary>
		/// 正文/文字
		/// </summary>
		Text = 8,
		/// <summary>
		/// 字幕/弹幕
		/// </summary>
		Subtitle = 16,
		/// <summary>
		/// 评论
		/// </summary>
		Comment = 32,
		/// <summary>
		/// 所有类型
		/// </summary>
		All = Video | Audio | Picture | Text | Subtitle | Comment
	}

}
