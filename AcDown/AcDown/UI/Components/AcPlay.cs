using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Kaedei.AcDown.UI.Components
{
   public partial class AcPlay : UserControl
   {
      public AcPlay()
      {
         InitializeComponent();
      }

      private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         Process.Start(@"http://blog.sina.com.cn/s/blog_58c506600100vf4x.html");
      }
   }
}
