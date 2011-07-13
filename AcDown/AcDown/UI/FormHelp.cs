using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Kaedei.AcDown.UI
{
   public partial class FormHelp : Form
   {
      public FormHelp()
      {
         InitializeComponent();
      }

      private void btnClose_Click(object sender, EventArgs e)
      {
         this.Close();
      }

      private void FormHelp_Load(object sender, EventArgs e)
      {
         lblVersion.Text = Application.ProductVersion.ToString();
      }

      private void lnkCheckUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         Process.Start("http://acdown.codeplex.com/wikipage?title=Check%20Update");
      }

      private void lnkFAQ_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         Process.Start("http://acdown.codeplex.com/wikipage?title=FAQ");
      }

      private void lnkReportBug_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         Process.Start("http://blog.sina.com.cn/s/blog_58c506600100t7w4.html");
      }

      private void lnkAdvise_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         Process.Start("http://blog.sina.com.cn/s/blog_58c506600100t7w5.html");
      }

      private void lnkProject_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         Process.Start("http://acdown.codeplex.com/");
      }

      private void lnkBlog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         Process.Start("http://blog.sina.com.cn/kaedei");
      }

      private void lnkFeed_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         Process.Start("http://list.qq.com/cgi-bin/qf_invite?id=5cab5a4e51e84cbb0f6ce0eaed69fb5fed194bc4e52ba3b9");
      }

      private void lnkWeibo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         Process.Start("http://weibo.com/kaedei");
      }
   }
}
