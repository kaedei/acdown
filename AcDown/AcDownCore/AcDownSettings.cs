/* AcDownSettings.cs
 * 
 * class AcFunSettings:
 * 定义AcDown程序设置的类
 */

using System;
using System.Xml.Serialization;
using Kaedei.AcDown.Interface;
using System.Drawing;

namespace Kaedei.AcDown.Core
{
	[Serializable]
	public class AcDownSettings
	{
		//程序设置
		public Size WindowSize = new Size(634, 513);
		public bool WatchClipboard = true; //监视剪贴板
		public bool WatchClipboardShortUrl = true; //监视剪贴板中的短网址
		public string SavePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //保存文件夹
		public bool DownAllSection; //自动下载所有分段
		public bool ParseRelated = true; //自动解析关联的下载项
		public Int32 CacheSize = 1; //缓存文件
		public bool OpenFolderAfterComplete; //完成后打开文件夹
		public bool PlaySound = true; //播放声音
		public string SoundFile = ""; //声音文件路径 (wav格式)
		public bool Logging = false; //运行记录日志
		public bool CheckUpdate = true; //允许检查更新
		public Int32 MaxRunningTaskCount = 2; //最多同时运行任务数量
		public bool SaveWhenAbort = true; //任务停止或错误退出时保存已经下载的部分
		public string SearchQuery = @"Acfun站内搜索"; //搜索url
		public bool Windows7Feature = true; //允许使用Windows7特性
		public bool DeleteTaskAndFile = false; //删除任务的同时删除文件
		public bool HideWhenClose = true; //点击关闭按钮时最小化
		public int RefreshInfoInterval = 2000; //下载信息刷新频率(毫秒)
		public int NetworkTimeout = 25000; //发送请求的默认超时时间（毫秒）
		public string CheckUpdateDocument = @"stable"; //自动更新通道
		public int RetryTimes = 3; //重试次数
		public int RetryWaitingTime = 5000; //重试前的等待时间（毫秒）
		public bool ExtractCache = true; //默认启用缓存提取
		public bool AutoAnswer = false; //默认启用自动应答
		public int ToolFormTimeout = 90; //工具窗口默认超时时间
		//public List<string> RecentSearch = new List<string>();

		//代理设置
		//[XmlElement(IsNullable = true)]
		[XmlArray("Proxies")]
		public AcDownProxy[] Proxy_Settings;
	}
}