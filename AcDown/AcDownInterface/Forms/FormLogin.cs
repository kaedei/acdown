using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Kaedei.AcDown.Interface.UI;

namespace Kaedei.AcDown.Interface.Forms
{
	public partial class FormLogin : FormBase
	{
		readonly UserLoginInfo info;
		readonly string url;
		private readonly string m_captchaFile;

		public FormLogin(UserLoginInfo userLoginInfo, string regUrl, string captchaFile="")
		{
			InitializeComponent();
			formtitle = this.Text;
			info = userLoginInfo;
			url = regUrl;
			m_captchaFile = captchaFile;
		}

		private void FormLogin_Load(object sender, EventArgs e)
		{
			txtUserName.Text = info.Username;
			txtPassword.Text = info.Password;
			//隐藏"注册"按钮
			if (string.IsNullOrEmpty(url))
			{
				lnkRegister.Visible = false;
			}
			//隐藏验证码
			if (!string.IsNullOrEmpty(m_captchaFile) && File.Exists(m_captchaFile))
			{
				picCaptcha.Image = Image.FromFile(m_captchaFile);
			}
			else
			{
				lblCaptcha.Visible = false;
				picCaptcha.Visible = false;
				txtCaptcha.Visible = false;
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
			info.Captcha = txtCaptcha.Text;
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

		private void txtUserName_TextChanged(object sender, EventArgs e)
		{

		}

		private void picCaptcha_Click(object sender, EventArgs e)
		{

		}

	} // end class formlogin

	public class UserLoginInfo
	{
		public string Username = "";
		public string Password = "";
		public string Captcha = "";
	}
}
