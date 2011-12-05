using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Kaedei.AcDown.Interface;
using Kaedei.AcDown.Component;
using System.IO;

namespace Kaedei.AcDown.UI
{
   public partial class FormInfo : Form
   {
      TaskInfo _task;
      public FormInfo(TaskInfo task)
      {
         InitializeComponent();
         _task = task;
      }

      
      private void FormInfo_Load(object sender, EventArgs e)
      {
         //添加信息
         StringBuilder sb = new StringBuilder();
         sb.AppendLine("任务编号: " + _task.TaskId.ToString());
         sb.AppendLine("Url: " + _task.Url);
         sb.AppendLine("下载状态: " + _task.Status.ToString());
         sb.AppendLine("插件: " + _task.BasePlugin.Describe + " " + _task.BasePlugin.Version.ToString());
         sb.AppendLine("标题: " + _task.Title);
         sb.AppendLine("创建时间: " + _task.CreateTime.ToString());
         sb.AppendLine("引用页: " + _task.SourceUrl);
         sb.AppendLine("保存目录: " + _task.SaveDirectory.ToString());
         sb.AppendLine("任务散列值: " + _task.Hash);
         sb.AppendLine("注释: ");
         sb.AppendLine(_task.Comment);

         sb.AppendLine("文件: ");
         if (_task.FilePath.Count > 0)
         {
            foreach (string item in _task.FilePath)
            {
               if (File.Exists(item))
                  sb.AppendLine(item);
               else
                  sb.AppendLine("[文件不存在]" + item);
            }
         }
         else
         {
            sb.AppendLine("无");
         }

         sb.AppendLine("字幕文件: ");
         if (_task.FilePath.Count > 0)
         {
            foreach (string item in _task.SubFilePath)
            {
               if (File.Exists(item))
                  sb.AppendLine(item);
               else
                  sb.AppendLine("[文件不存在]" + item);
            }
         }
         else
         {
            sb.AppendLine("无");
         }


         sb.AppendLine("插件存储的设置: ");
         if (_task.Settings.Count > 0)
         {
            foreach (string key in _task.Settings.Keys)
            {

               sb.AppendLine(key + " = " + _task.Settings[key]);
            }
         }
         else
         {
            sb.AppendLine("无");
         }


         sb.AppendLine("索引信息: ");
         sb.AppendLine(_task.ToString());
         sb.AppendLine();

         txtInfo.Text = sb.ToString();

      }

      private void btnCopy_Click(object sender, EventArgs e)
      {
         if (!string.IsNullOrEmpty(txtInfo.Text))
         {
            Clipboard.SetText(txtInfo.Text);
         }
      }

      private void btnClose_Click(object sender, EventArgs e)
      {
         this.Close();
      }
   }
}
