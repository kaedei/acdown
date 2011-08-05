using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Kaedei.AcDown.Interface;
using System.Diagnostics;

namespace Kaedei.AcDown.UI.Components.FlvCombine
{
   /// <summary>
   /// Flv合并
   /// </summary>
   public class FlvCombine
   {
      /// <summary>
      /// 检查相关文件是否存在
      /// </summary>
      /// <returns></returns>
      public static bool CheckExisted()
      {
         string appdata = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
         string bind = Path.Combine(appdata, @"Kaedei\AcDown\UIComponents\FlvCombine\1\FlvBind.exe");
         string dll = Path.Combine(appdata, @"Kaedei\AcDown\UIComponents\FlvCombine\1\FLVLib.dll");
         if (File.Exists(bind) && File.Exists(dll))
            return true;
         else
            return false;
      }

      /// <summary>
      /// 下载相关文件
      /// </summary>
      public static bool DownloadComponents()
      {
         string appdata = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
         string file_flvbind = Path.Combine(appdata, @"Kaedei\AcDown\UIComponents\FlvCombine\1\FlvBind.exe");
         string file_flvlib = Path.Combine(appdata, @"Kaedei\AcDown\UIComponents\FlvCombine\1\FLVLib.dll");
         //建立文件夹
         string dir = Path.GetDirectoryName(file_flvbind);
         if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
         //下载exe文件
         try
         {
            bool r1 = Network.DownloadFile(new DownloadParameter()
            {
               Url = @"http://download.codeplex.com/Project/Download/FileDownload.aspx?ProjectName=acdown&DownloadId=266608",
               FilePath = file_flvbind,
            });
            //下载dll文件
            bool r2 = Network.DownloadFile(new DownloadParameter()
            {
               Url = @"http://download.codeplex.com/Project/Download/FileDownload.aspx?ProjectName=acdown&DownloadId=266609",
               FilePath = file_flvlib,
            });
            return r1 && r2;
         }
         catch (Exception ex)
         {
            return false;
         }
      }

      /// <summary>
      /// 合并Flv视频
      /// </summary>
      /// <param name="fileName"></param>
      /// <param name="fileParts"></param>
      /// <returns></returns>
      public static bool CombineFlvFile(string fileName,string[] fileParts)
      {
         string appdata = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
         string file_flvbind = Path.Combine(appdata, @"Kaedei\AcDown\UIComponents\FlvCombine\1\FlvBind.exe");
         //生成ProcessStartInfo
         ProcessStartInfo pinfo = new ProcessStartInfo(file_flvbind);
         //设置参数
         StringBuilder sb = new StringBuilder();
         sb.Append("\"" + fileName + "\"");
         foreach (string item in fileParts)
         {
            sb.Append(" \"" + item + "\"");
         }
         pinfo.Arguments = sb.ToString();
         //隐藏窗口
         pinfo.WindowStyle = ProcessWindowStyle.Hidden;
         //启动程序
         Process p = Process.Start(pinfo);
         p.WaitForExit();
         if (p.ExitCode == 0)
            return true;
         else
            return false;
      }

   }
}
