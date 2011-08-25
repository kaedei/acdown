using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Kaedei.AcDown.Interface;
using Kaedei.AcDown.Component;

namespace Kaedei.AcDown.UI
{
   public partial class FormInfo : Form
   {
      TaskItem _downloader;
      public FormInfo(TaskItem downloader)
      {
         InitializeComponent();
         _downloader = downloader;
      }

      private void FormInfo_Load(object sender, EventArgs e)
      {
         txtInfo.Text = _downloader.Info;
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
