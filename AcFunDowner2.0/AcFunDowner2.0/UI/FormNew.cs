using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Kaedei;
using Kaedei.AcFunDowner;
using AcFunDownerLibrary;

namespace Kaedei.AcFunDowner
{
    public partial class FormNew : Form
    {
		 //外部Url
		 private string u;

		 [DllImport("user32.dll", EntryPoint = "SendMessageA")]
		 public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, Byte[] lParam);

		 public FormNew(string url)
			 : this()
		 {
			 u = url;
		 }

       public FormNew()
       {
          InitializeComponent();
			 this.Icon = Kaedei.AcFunDowner.Properties.Resources.ac;
       }

		 private void FormNew_Load(object sender, EventArgs e)
		 {
			 if (Config.IsWindowsVistaOr7())
				 SendMessage(txtInput.Handle, 0x1501, IntPtr.Zero, System.Text.Encoding.Unicode.GetBytes(@"请输入或粘贴入网络地址"));
			 if (Config.setting.AutoDownAllSection)
				 chkDownAllSection.Checked = true;
			 if (Config.setting.DownSub)
				 chkDownSub.Checked = true;
			 if (!string.IsNullOrEmpty(u))
				 txtInput.Text = u;
			 this.Show();
			 this.TopMost = true;
			 this.TopMost = false;
		 }

		 
		 private void txtInput_TextChanged(object sender, EventArgs e)
		 {
			 if (Config.setting.AutoCheckUrl)
			 {
				 if (txtInput.Text.Length != 0)
				 {
					 picCheck.Visible = true;
					 if (AcFunDownerLibrary.AcDowner.CheckUrl(txtInput.Text))
						 picCheck.Image = Properties.Resources._1;
					 else
						 picCheck.Image = Properties.Resources._2;
				 }
				 else
				 {
					 picCheck.Visible = false;
				 }
			 }
		 }

		 /// <summary>
		 /// 添加任务
		 /// </summary>
		 /// <param name="sender"></param>
		 /// <param name="e"></param>
		 private void btnAdd_Click(object sender, EventArgs e)
		 {
			 string url = AcDowner.CombineUrl(txtInput.Text);
			 this.Cursor = Cursors.WaitCursor;
			 //检查是否有已经在进行的任务
			 foreach (AcDowner ac in TaskManager.ObjectReference.Tasks)
			 {
				 if (AcDowner.DepartUrl(url) == AcDowner.DepartUrl(ac.Url))
				 {
					 toolTip.Show("此任务已经存在", txtInput, 3000);
					 this.Cursor = Cursors.Default;
					 return;
				 }
			 }
			 //检查URL
			 if (Config.setting.AutoCheckUrl)
			 {
				 if (!AcDowner.CheckUrl(url))
				 {
					 toolTip.Show("网络地址(URL)不符合规则，请检查后重新输入", txtInput, 120 ,-60, 3000);
					 txtInput.SelectAll();
					 this.Cursor = Cursors.Default;
					 return;
				 }
			 }
			 //保存设置
			Config.setting.AutoDownAllSection = chkDownAllSection.Checked;
			Config.setting.DownSub = chkDownSub.Checked;
			Config.SaveSettings();
			try
			{
				TaskManager.ObjectReference.AddTask(url,chkImmediate.Checked);
			}
			catch (Exception ex)
			{
				Logging.Add(ex);
				toolTip.Show("网络连接出现错误，请检查相关设置或查看日志文件\n启用日志的方法请查看官方文档", btnAdd, 4000);
			}
			this.Cursor = Cursors.Default;
			this.Close();
		 }

		 //回车键的默认行为
		 private void txtInput_KeyDown(object sender, KeyEventArgs e)
		 {
			 if (e.KeyCode == Keys.Enter)
				 btnAdd_Click(this, EventArgs.Empty);
		 }



	}
}
