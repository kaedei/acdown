using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Net;
using System.IO;

namespace Kaedei.AcDown.Component
{
   public class TaskInfo
   {
      public Guid TaskId;
      public string Title;
      public DownloadStatus Status;
      public string Url;
      public DownloadSubtitleType DownSub; //0-下载 1-不下载 2-只下载
      public WebProxy Proxy;
      public DirectoryInfo SaveDirectory;
      public int SpeedLimit;
   }

}
