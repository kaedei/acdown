using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Kaedei.AcDown.Interface.UI;

namespace Kaedei.AcDown.Interface.Forms
{
	public partial class FormLogin : FormBase
	{
		UserLoginInfo info;
		string url;
		public FormLogin(UserLoginInfo userLoginInfo, string regUrl)
		{
			InitializeComponent();
			formtitle = this.Text;
			info = userLoginInfo;
			url = regUrl;
		}

		private void FormLogin_Load(object sender, EventArgs e)
		{
			//隐藏"注册"按钮
			if (string.IsNullOrEmpty(url))
			{
				lnkRegister.Visible = false;
			}
		}

		private void lnkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(url);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			info.Username = txtUserName.Text;
			info.Password = txtPassword.Text;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private string formtitle;
		private int countdown = GlobalSettings.GetSettings().ToolFormTimeout;
		private void tmr_Tick(object sender, EventArgs e)
		{
			countdown--;
			this.Text = "[" + countdown.ToString() + "]" + formtitle;
			if (countdown == 0)
			{
				btnOK_Click(sender, EventArgs.Empty);
			}
		}

	} // end class formlogin

	public class UserLoginInfo
	{
		public string Username = "";
		public string Password = "";
	}
}
