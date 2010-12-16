using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Kaedei;
using Kaedei.AcDown;
using Kaedei.AcDown.Interface;
using System.Collections.ObjectModel;
using Kaedei.AcDown.Component;

namespace Kaedei.AcDown
{
	 public partial class FormNew : Form
	 {

		 private static FormNew instance;
		 //外部Url
		 private string u;
		 //插件
		 private PluginManager _pluginMgr;
		 //任务
		 private TaskManager _taskMgr;

		 [DllImport("user32.dll", EntryPoint = "SendMessageA")]
		 public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, Byte[] lParam);

		 public FormNew(string url, PluginManager pluginMgr, TaskManager taskMgr)
		 {
			 InitializeComponent();
			 this.Icon = Kaedei.AcDown.Properties.Resources.Ac;

			 if (instance != null)
			 {
				 //将窗体置顶一次
				 instance.TopMost = true;
				 instance.TopMost = false;
				 this.Dispose();
			 }
			 else
			 {
				 _pluginMgr = pluginMgr;
				 _taskMgr = taskMgr;
				 u = url;
				 instance = this;
			 }
		 }
		 
		 private void FormNew_Load(object sender, EventArgs e)
		 {
			 if (Config.IsWindowsVistaOr7())
				 SendMessage(txtInput.Handle, 0x1501, IntPtr.Zero, System.Text.Encoding.Unicode.GetBytes(@"请输入或粘贴入网络地址"));
			 if (!string.IsNullOrEmpty(u))
				 txtInput.Text = u;
			 lblPath.Text = Config.setting.SavePath;
			 this.Show();
			 this.TopMost = true;
			 this.TopMost = false;
		 }

		 
		 private void txtInput_TextChanged(object sender, EventArgs e)
		 {
			 string t = txtInput.Text;
			 if (Config.setting.AutoCheckUrl)
			 {
				 if (t.Length != 0)
				 {
					 picCheck.Visible = true;
					 foreach (var item in _pluginMgr.Plugins)
					 {
						 if (item.CheckUrl(t))
						 {
							 picCheck.Image = Properties.Resources._1;
						 }
						 else
						 {
							 picCheck.Image = Properties.Resources._2;
						 }
					 }
				 }
				 else
				 {
					 picCheck.Visible = false;
				 }
			 }
		 }

		 /// <summary>
		 /// TODO:添加任务
		 /// </summary>
		 /// <param name="sender"></param>
		 /// <param name="e"></param>
		 private void btnAdd_Click(object sender, EventArgs e)
		 {
			 string url = txtInput.Text;
			 this.Cursor = Cursors.WaitCursor;
			 ////检查是否有已经在进行的任务
			 //foreach (AcDowner ac in TaskManager.ObjectReference.Tasks)
			 //{
			 //   if (AcDowner.DepartUrl(url) == AcDowner.DepartUrl(ac.Url))
			 //   {
			 //      toolTip.Show("此任务已经存在", txtInput, 3000);
			 //      this.Cursor = Cursors.Default;
			 //      return;
			 //   }
			 //}
			 //取得可以下载的插件
			 IAcdownPluginInfo p = null;
			 foreach (var item in _pluginMgr.Plugins)
			 {
				 if (item.CheckUrl(url))
				 {
					 p = item;
					 break;
				 }
			 }
			 //如果有可用插件
			 if (p != null)
			 {
				 try
				 {
					 _taskMgr.AddTask(url, p.CreateDownloader());
					 this.Cursor = Cursors.Default;
					 this.Close();
				 }
				 catch (Exception ex)
				 {
					 Logging.Add(ex);
					 toolTip.Show("新建任务出现错误，请检查相关设置或查看日志文件", btnAdd, 4000);
				 }
			 }
			 else
			 {
				 toolTip.Show("网络地址(URL)不符合规则，请检查后重新输入", txtInput, 120, -60, 3000);
				 txtInput.SelectAll();
				 this.Cursor = Cursors.Default;
			 }
			
			
		 }

		 //回车键的默认行为
		 private void txtInput_KeyDown(object sender, KeyEventArgs e)
		 {
			 if (e.KeyCode == Keys.Enter)
				 btnAdd_Click(this, EventArgs.Empty);
		 }



	}
}
