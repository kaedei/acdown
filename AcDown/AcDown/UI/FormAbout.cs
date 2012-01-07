using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace Kaedei.AcDown.UI
{
   partial class FormAbout : Form
   {
      public FormAbout()
      {
         InitializeComponent();
      }

      private void FormAbout_Load(object sender, EventArgs e)
      {
         lblVersion.Text = String.Concat(new string[] { @"版本：", Application.ProductVersion.ToString() });
      }

      /// <summary>
      /// 点击制作者链接
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void lnkSupport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         Process.Start(@"http://blog.sina.com.cn/kaedei");
      }

      private void lblVersion_DoubleClick(object sender, EventArgs e)
      {
         pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
         pictureBox1.Image = AcDown.Properties.Resources.ICONICON_1;
      }

      private void lnkProject_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         Process.Start(@"http://acdown.codeplex.com/");
      }


   }
}
