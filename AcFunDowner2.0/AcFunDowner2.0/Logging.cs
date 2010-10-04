/*Logging.cs
 * 
 * class Logging:
 * 用来记录错误日志的静态类
 * 
 * 最后更新：2010-1-10
 * 
 * Copyright 2010 Kaedei Software

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 * 
 * http://blog.sina.com.cn/kaedei
 * mailto:kaedei@foxmail.com
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Kaedei.AcFunDowner
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
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                @"Kaedei\AcFunDowner\Log\");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string t=string.Concat(time.Year.ToString(),time.Month.ToString(),time.Day.ToString(),
                                    "-",time.Hour.ToString(),time.Minute.ToString(),time.Second.ToString(),".log");
            path=Path.Combine(path,t);
            LogFilePath = path;
            writer = new StreamWriter (path ,true );
            writer.WriteLine("AcFun视频下载器日志文件，生成于：");
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
			  if (e.InnerException != e.InnerException)
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
