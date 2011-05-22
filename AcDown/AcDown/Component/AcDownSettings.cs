/* AcFunSettings.cs
 * 
 * class AcFunSettings:
 * 定义AcDown程序设置的类
 * 
 * class Config:
 * 设置/保存AcDown程序设置的静态类
 */

using System;
using System.IO;
using System.Xml.Serialization;
using Kaedei.AcDown.Interface;
using System.Diagnostics;

namespace Kaedei.AcDown
{
	 [Serializable]
	 public class AcDownSettings
	 {
		  //程序设置
		 public bool WatchClipboardEnabled = true; //监视剪贴板
		  public bool DownSub=true  ; //下载字幕
		  public string SavePath=Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //保存文件夹
		  public bool AutoDownAllSection ; //自动下载所有分段
		  public Int32 CacheSize=1; //缓存文件
		  public bool OpenFolderAfterComplete; //完成后打开文件夹
		  public bool PlaySound=true; //播放声音
		  public string SoundFile = ""; //声音文件路径 (wav格式)
		  public bool EnableLog = false; //运行记录日志
		  public bool AutoCheckUrl = true; //自动检查URL
		  //public bool ShowTrayIcon = true; //显示托盘图标
		  public bool ShowBigStartButton = true; //显示大按钮
		  public Int32 MaxRunningTaskCount = 2; //最多同时运行任务数量
		  public bool SaveWhenAbort = true; //任务停止或错误退出时保存已经下载的部分
		  //public string SearchEngine = "Acfun站内搜索 - Google"; //默认的搜索引擎。还可以是Google或Baidu
		  public string SearchQuery = @""; //搜索url
		  public bool EnableWindows7Feature = true; //允许使用Windows7特性
		  public bool DeleteTaskAndFile = false; //删除任务的同时删除文件
		  

	  }

	 public static class Config
	 {
		  public static AcDownSettings setting { get; set; }
		  
		  /// <summary>
		  /// 保存设置
		  /// </summary>
		  public static void SaveSettings()
		  {
			  
					 //取得APPDATA路径名称
					 string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
					 path = Path.Combine(path, @"Kaedei\AcDown\");

					 if (!Directory.Exists(path))
					 {
						  //如果目录不存在则创建
						  Directory.CreateDirectory(path);
					 }

					 //序列化设置
					 using (FileStream fs = new FileStream(path + @"config.xml", FileMode.Create))
					 {
						  XmlSerializer formatter = new XmlSerializer(typeof(AcDownSettings));
						  formatter.Serialize(fs, setting);
					 }
				
		  }

		  /// <summary>
		  /// 读取设置
		  /// </summary>
		  /// <returns></returns>
		  public static AcDownSettings LoadSettings()
		  {
			  try
			  {
				  AcDownSettings s;
				  string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
				  path = Path.Combine(path, @"Kaedei\AcDown\config.xml");
				  using (FileStream fs = new FileStream(path, FileMode.Open))
				  {
					  XmlSerializer formatter = new XmlSerializer(typeof(AcDownSettings));
					  s = (AcDownSettings)formatter.Deserialize(fs);
				  }

				  if (s != null)
					  setting = s;
				  else
					  throw new Exception();
			  }
			  catch
			  {
				  setting = new AcDownSettings();
				  SaveSettings();
			  }
			  finally
			  {
				  GlobalSettings.GetSettings().CacheSizeMb = setting.CacheSize;
				  GlobalSettings.GetSettings().DownSub = setting.DownSub;
				  //GlobalSettings.GetSettings().SaveFileWhenAbort = setting.SaveWhenAbort;
			  }
				return setting; 
		  }

	 


				/// <summary>
		  /// 判断当前系统是否为Windows7
		  /// </summary>
		  /// <returns></returns>
		 [DebuggerNonUserCode()]
		 public static bool IsWindows7()
		  {
				if (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor >= 1)
					 return true;
				else
					 return false;
		  }
				[DebuggerNonUserCode()]
		  public static bool IsWindowsVista()
		  {
				if (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 0)
					 return true;
				else
					 return false;
		  }
				[DebuggerNonUserCode()]
		  public static bool IsWindowsVistaOr7()
		  {
				if (Environment.OSVersion.Version.Major == 6 )
					 return true;
				else
					 return false;
		  }
	 }
	 //[Serializable]
	 //class CheckNewXml
	 //{
	 //    public Version LastestVersion;
	 //    public DateTime PublishDate;
	 //    public string Describe;
	 //}
}
