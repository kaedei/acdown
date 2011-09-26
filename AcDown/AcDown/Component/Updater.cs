﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;

namespace Kaedei.AcDown.Component
{
   /// <summary>
   /// 控制应用程序升级过程的类
   /// </summary>
   public class Updater
   {
      private string tempFile = "";

      /// <summary>
      /// 临时文件路径
      /// </summary>
      public string TempFile
      {
         get { return tempFile; }
      }

      public Updater()
      {
         //取得临时文件的路径
         tempFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
         tempFile = Path.Combine(tempFile, @"Kaedei\AcDown\Update\AcDown.exe");
      }

      /// <summary>
      /// 检查程序是否有最新更新
      /// </summary>
      /// <returns></returns>
      public string CheckUpdate(Version oldVersion)
      {
         string src = Network.GetHtmlSource(@"http://acdown.codeplex.com/wikipage?title=AutoUpdate", Encoding.UTF8);
         Regex rVersion = new Regex(@"{updatestart}NEWVERSION=(?<major>\d+)\.(?<minor>\d+).(?<build>\d+)\.(?<revision>\d+){updateend}");
         Match mVersion = rVersion.Match(src);
         string verstring = mVersion.Groups["major"].ToString() + "." +
                           mVersion.Groups["minor"].ToString() + "." +
                           mVersion.Groups["build"].ToString() + "." +
                           mVersion.Groups["revision"].ToString();
         Version newVersion = new Version(verstring);
         if (newVersion > oldVersion)
         {
            Regex rUrl = new Regex(@"{urlstart}URL=(?<url>.+?){urlend}");
            Match mUrl = rUrl.Match(src);
            if (mUrl.Success)
            {
               string url = mUrl.Groups["url"].Value.Replace("&amp;", "&");
               return url;
            }
         }
         return "";

      }

      /// <summary>
      /// 下载最新更新至临时文件夹
      /// </summary>
      /// <param name="url">更新所在的Url</param>
      /// <returns></returns>
      public bool DownloadUpdate(string url)
      {
         try
         {
            //下载文件
            bool s = Network.DownloadFile(new DownloadParameter()
            {
               Url = url,
               FilePath = tempFile
            });
            return s;
         }
         catch
         {
            return false;
         }
      }

      /// <summary>
      /// 取得一个值，指示当前程序是否正在更新过程中（运行在临时文件夹中）
      /// </summary>
      /// <param name="path">程序的映像路径</param>
      /// <returns></returns>
      public bool CheckIfUpdating(string path)
      {
         if (path.ToUpper() == tempFile.ToUpper())
            return true;
         return false;
      }

      /// <summary>
      /// 将临时文件覆盖指定的文件
      /// </summary>
      /// <param name="filePath">覆盖到的文件完整路径</param>
      public void CopyTempFileToTargetFile(string filePath)
      {
         string file = filePath.Replace("\"", "");
         //拷贝并覆盖同名文件
         if (File.Exists(tempFile))
            File.Copy(tempFile, file, true);
      }

      /// <summary>
      /// 删除临时文件
      /// </summary>
      public void DeleteTempFile()
      {
         File.Delete(tempFile);
      }

   }
}
