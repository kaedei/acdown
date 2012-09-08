using System;
using System.Windows.Forms;
using System.Diagnostics;
using Kaedei.AcDown.Interface.UI;

namespace Kaedei.AcDown.UI
{
	public partial class FormHelp : FormBase
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
			
		}

		private void lnkWeibo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://weibo.com/kaedei");
		}

		private void btnCopyEmail_Click(object sender, EventArgs e)
		{
			try
			{
				txtEmail.SelectAll();
				Clipboard.SetText(txtEmail.Text);
				MessageBox.Show("邮件地址已经复制到系统剪贴板", "帮助中心", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch { }
		}

		private void btnSendEmail_Click(object sender, EventArgs e)
		{
			Process.Start(@"http://mail.qq.com/cgi-bin/qm_share?t=qm_mailme&email=td7U0NHQ3PXD3MWbxMSb1trY");
		}

		private void lnkProjectDiscussion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(@"http://acdown.codeplex.com/discussions");
		}

		private void lnkProjectIssueTracker_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(@"http://acdown.codeplex.com/workitem/list/basic");
		}

		private void lnkProjectDocumentationUser_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(@"http://acdown.codeplex.com/wikipage?title=For%20User&referringTitle=Documentation");
		}

		private void lnkProjectDocumentationDeveloper_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(@"http://acdown.codeplex.com/wikipage?title=For%20Developer&referringTitle=Documentation");
		}

		private void lnkFeed_Click(object sender, EventArgs e)
		{
			Process.Start("http://list.qq.com/cgi-bin/qf_invite?id=5cab5a4e51e84cbb0f6ce0eaed69fb5fed194bc4e52ba3b9");
		}

		private void lnkQA_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(@"http://blog.sina.com.cn/s/blog_58c506600100z40t.html");
		}

		private void lnkQAAcplay_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(@"http://blog.sina.com.cn/s/blog_58c5066001012xsd.html");
		}
		

	}
}
