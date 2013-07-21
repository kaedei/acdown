using System;
using System.Xml.Serialization;
using Kaedei.AcDown.Interface;

namespace Kaedei.AcDown.Interface.AcPlay
{
	/// <summary>
	/// AcPlay播放配置
	/// </summary>
	[Serializable()]
	public class AcPlayConfiguration
	{
		/// <summary>
		/// 播放器名称
		/// </summary>
		public string PlayerName;
		/// <summary>
		/// 播放器地址
		/// </summary>
		public string PlayerUrl;
		/// <summary>
		/// 原网页地址
		/// </summary>
		public string WebUrl;
		/// <summary>
		/// 本地HTTP服务器端口
		/// </summary>
		public int HttpServerPort;
		/// <summary>
		/// 本地代理服务器端口
		/// </summary>
		public int ProxyServerPort;
		/// <summary>
		/// 视频列表
		/// </summary>
		[XmlArray("Videos")]
		public Video[] Videos;
		/// <summary>
		/// 弹幕文件列表
		/// </summary>
		[XmlArray("Subtitles")]
		public string[] Subtitles;
		/// <summary>
		/// 加载本地文件时的读取速度限制
		/// </summary>
		public int SpeedLimit = 0;
		/// <summary>
		/// 连接互联网资源时使用的代理服务器
		/// </summary>
		public AcDownProxy Proxy;
		/// <summary>
		/// 额外信息
		/// </summary>
		public SerializableDictionary<string, string> ExtraConfig;

	}

	/// <summary>
	/// 播放配置中的视频信息
	/// </summary>
	[Serializable()]
	public class Video
	{
		/// <summary>
		/// 视频编号
		/// </summary>
		public int Order;
		/// <summary>
		/// 文件名称（支持相对路径、绝对路径和Internet路径）
		/// </summary>
		public string FileName;
		/// <summary>
		/// 视频长度(毫秒为单位)
		/// </summary>
		public int Length;
	}
}
