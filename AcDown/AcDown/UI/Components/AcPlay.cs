using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Kaedei.AcDown.Interface;
using System.IO;
using System.Windows.Forms;

namespace Kaedei.AcDown.UI.Components
{

   public class AcPlay
   {
      /// <summary>
      /// 检查相关文件是否存在
      /// </summary>
      /// <returns></returns>
      public static bool CheckExisted()
      {
         string appdata = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
         string acplay = Path.Combine(appdata, @"Kaedei\AcDown\UIComponents\AcPlay\1\AcPlay.exe");
         if (File.Exists(acplay))
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
         string file_acplay = Path.Combine(appdata, @"Kaedei\AcDown\UIComponents\AcPlay\1\AcPlay.exe");
         string exe_acplay = @"http://download.codeplex.com/Download?ProjectName=acdown&DownloadId=297021";


         //建立文件夹
         string dir = Path.GetDirectoryName(file_acplay);
         if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
         //下载exe文件
         try
         {
            bool r1 = Network.DownloadFile(new DownloadParameter()
            {
               Url = exe_acplay,
               FilePath = file_acplay,
            });
            return r1;
         }
         catch (Exception ex)
         {
            return false;
         }
      }

      /// <summary>
      /// 启动AcPlay
      /// </summary>
      /// <param name="fileName"></param>
      /// <param name="fileParts"></param>
      /// <returns></returns>
      public static void PlayVideo(string player, string[] videos, string xml1, string xml2, int timeLength)
      {
         string appdata = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
         string file_acplay = Path.Combine(appdata, @"Kaedei\AcDown\UIComponents\AcPlay\1\AcPlay.exe");
         //生成ProcessStartInfo
         ProcessStartInfo pinfo = new ProcessStartInfo(file_acplay);
         //设置参数
         StringBuilder sb = new StringBuilder();
         sb.Append(@"player=" + player + " ");
         sb.Append(@"video=""");
         for (int i = 0; i < videos.Length - 1; i++)
         {
            sb.Append(videos[i] + "|");
         }
         sb.Append(videos[videos.Length - 1] + @""" ");
         if (xml1 != "")
            sb.Append(@"xml1=""" + xml1 + @""" ");
         if (xml2 != "")
            sb.Append(@"xml2=""" + xml2 + @""" ");
         sb.Append(@"time=" + timeLength.ToString());

         pinfo.Arguments = sb.ToString();
         //启动程序
         try
         {
            Process p = Process.Start(pinfo);
            p.WaitForExit();
         }
         catch 
         {
            MessageBox.Show("启动AcPlay失败", "AcPlay弹幕播放器", MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }
   
   }
}
