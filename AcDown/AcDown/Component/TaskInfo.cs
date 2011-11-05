using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace Kaedei.AcDown.Component
{
   public class TaskInfo
   {
      /// <summary>
      /// 任务ID
      /// </summary>
      public Guid TaskId;

      /// <summary>
      /// TaskItem对象
      /// </summary>
      public TaskItem Task;

      /// <summary>
      /// 任务名称
      /// </summary>
      public string Title;
      /// <summary>
      /// 下载状态
      /// </summary>
      public DownloadStatus Status;
      /// <summary>
      /// 任务URL(真实的)
      /// </summary>
      public string Url; 
      /// <summary>
      /// 是否下载弹幕/字幕
      /// 0-下载 1-不下载 2-只下载
      /// </summary>
      public DownloadSubtitleType DownSub; 
      /// <summary>
      /// 应用的代理服务器
      /// </summary>
      public WebProxy Proxy; 
      /// <summary>
      /// 保存路径
      /// </summary>
      public DirectoryInfo SaveDirectory;
      /// <summary>
      /// 速度限制
      /// </summary>
      public int SpeedLimit;
      /// <summary>
      /// 任务创建时间
      /// </summary>
      public DateTime StartTime;

      /// <summary>
      /// 关联的UI Item(ListViewItem)
      /// </summary>
      public ListViewItem UIItem;

      
   }

}
