/*Logging.cs
 * 
 * class Logging:
 * 用来记录错误日志的静态类
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Kaedei.AcDown.Core;

namespace Kaedei.AcDown.Core
{
	 public static class Logging 
	 {
		  public static string LogFilePath { get; set; }
		  private static DateTime time = DateTime.Now;
		  private static StreamWriter writer;
		  public static void Initialize()
		  {
			  //如果禁止记录日志则返回
			  if (!Config.setting.EnableLog) 
				  return;
			  //日志文件路径
				string path=Path.Combine(
					 Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
					 @"Kaedei\AcDown\Log\");
				if (!Directory.Exists(path))
					 Directory.CreateDirectory(path);
				string t=string.Concat(time.Year.ToString(),time.Month.ToString(),time.Day.ToString(),
												"-",time.Hour.ToString(),time.Minute.ToString(),time.Second.ToString(),".log");
				path=Path.Combine(path,t);
				LogFilePath = path;
				writer = new StreamWriter (path ,true );
				writer.WriteLine("AcDown动漫下载器日志文件，生成于：");
				writer.WriteLine (time.ToString());
				writer.Flush();
			}

		 /// <summary>
		 /// 向日志中添加记录
		 /// </summary>
		 /// <param name="e"></param>
		  public static void Add(Exception e)
		  {
			  //如果禁止记录日志则返回
			  if (!Config.setting.EnableLog)
				  return;

			  writer.WriteLine();
			  writer.WriteLine("--------------------");
			  writer.WriteLine("New Exception: {0}", DateTime.Now.ToString());
			  if (e.Source != null)
				  writer.WriteLine("Source: {0}", e.Source);
			  if (e.TargetSite != null)
				  writer.WriteLine("Target Site: {0}", e.TargetSite.Name);
			  if (!string.IsNullOrEmpty(e.HelpLink))
				  writer.WriteLine("HelpLink: {0}", e.HelpLink);
			  if (!string.IsNullOrEmpty(e.Message))
				  writer.WriteLine("Exception: {0}", e.Message);
			  if (!string.IsNullOrEmpty(e.StackTrace))
				  writer.WriteLine("StackTrace：{0}", e.StackTrace);
			  if (e.InnerException != null)
				  writer.WriteLine("Inner Exception: {0}", e.InnerException.Message);
			  writer.Flush();
		  }

		 /// <summary>
		 /// 释放资源
		 /// </summary>
		  public static void Exit()
		  {
			  try
			  {
				  if (Config.setting.EnableLog)
				  {
					  writer.Close();
					  writer.Dispose();
				  }
			  }
			  catch
			  {
			  }
		  }

	 }
}
