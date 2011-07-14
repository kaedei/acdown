using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Kaedei.AcDown.Interface;
using Kaedei.AcDown.Component;
using System.Text;

namespace Kaedei.AcDown.UI
{
	 public partial class FormNew : Form
	 {

		 private static FormNew instance;
		 //外部Url
		 private static string u;
		 //插件
		 private static PluginManager _pluginMgr;
		 //任务
		 private static TaskManager _taskMgr;

		 [DllImport("user32.dll", EntryPoint = "SendMessageA")]
		 public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, Byte[] lParam);

		 public static void Initialize(PluginManager pluginMgr, TaskManager taskMgr)
		 {
			 _pluginMgr = pluginMgr;
			 _taskMgr = taskMgr;
			 instance = new FormNew();
		 }

		 /// <summary>
		 /// 显示窗体
		 /// </summary>
		 public static void ShowForm(string url)
		 {
			 u = url;
			 if (!string.IsNullOrEmpty(u))
			 {
				 instance.txtInput.Text = u;
			 }
			 instance.Show();
			 instance.TopMost = true;
			 instance.TopMost = false;
			 instance.txtInput.Focus();
		 }

		 private FormNew()
		 {
			 InitializeComponent();
			 this.Icon = Kaedei.AcDown.Properties.Resources.Ac;
		 }
		 
		 private void FormNew_Load(object sender, EventArgs e)
		 {
			 if (!string.IsNullOrEmpty(u))
				 txtInput.Text = u;
			 txtPath.Text = Config.setting.SavePath;
			 LoadProxys();
			 StringBuilder sb = new StringBuilder();
			 sb.AppendLine("当前支持的网站:(网址举例)");
			 foreach (var item in _pluginMgr.Plugins)
			 {
				 sb.AppendLine();
				 foreach (var i in item.GetUrlExample())
				 {
					 sb.AppendLine(i);
				 }
				 sb.AppendLine();
			 }
			 txtExample.Text = sb.ToString();
			 if (!Config.setting.Plugin_Enable_Flvcd)
				 lblFlvcdTip.Visible = false;
		 }

		 //读取代理服务器设置
		 private void LoadProxys()
		 {
			 cboProxy.Items.Clear();
			 cboProxy.Items.Add("不使用");
			 if (Config.setting.Proxy_Settings != null)
			 {
				 foreach (AcDownProxy item in Config.setting.Proxy_Settings)
				 {
					 cboProxy.Items.Add(item.Name);
				 }
			 }
			 cboProxy.SelectedIndex = 0;
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
							 return;
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
		 /// 添加任务
		 /// </summary>
		 /// <param name="sender"></param>
		 /// <param name="e"></param>
		 private void btnAdd_Click(object sender, EventArgs e)
		 {
			 string url = txtInput.Text;
			 this.Cursor = Cursors.WaitCursor;
			 
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
				 //取得此url的hash
				 string hash = p.GetHash(url);
				 //检查是否有已经在进行的相同任务
				 foreach (IDownloader downloader in _taskMgr.Tasks)
				 {
					 if (downloader.GetBasePlugin().GetHash(downloader.Url) == hash)
					 {
						 toolTip.Show("当前任务已经存在", txtInput, 4000);
						 this.Cursor = Cursors.Default;
						 return;
					 }
				 }
				 try
				 {
					 //取得代理设置
					 AcDownProxy selectedProxy = null;
					 foreach (AcDownProxy item in Config.setting.Proxy_Settings)
					 {
						 if (item.Name == cboProxy.SelectedItem.ToString())
							 selectedProxy = item;
					 }
					 //添加任务
					 IDownloader downloader = _taskMgr.AddTask(p.CreateDownloader(), url,
						 (selectedProxy == null) ? null : selectedProxy.ToWebProxy());
					 //开始下载
					 _taskMgr.StartTask(downloader);
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
				 toolTip.Show("网络地址(URL)不符合规则，请检查后重新输入", txtInput, 3000);
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

		 private void FormNew_FormClosing(object sender, FormClosingEventArgs e)
		 {
			 e.Cancel = true;
			 u = "";
			 txtInput.Text = "";
			 this.Hide();
		 }

		 private void lblShowConfig_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		 {
			 FormConfig fc = new FormConfig();
			 fc.ShowDialog();
			 this.txtPath.Text = Config.setting.SavePath;
		 }

		 private void btnExample_Click(object sender, EventArgs e)
		 {
			 if (this.Width == 842)
			 {
				 this.Width = 487;
				 btnExample.Text = "查看当前支持哪些网站 >>";
			 }
			 else
			 {
				 this.Width = 842;
				 btnExample.Text = "查看当前支持哪些网站 <<";
			 }
		 }

		 private void lnkPaste_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		 {
			 if (Clipboard.ContainsText())
			 {
				 txtInput.Text = Clipboard.GetText();
			 }
		 }

		 private void lnkSetProxy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		 {
			 FormConfig frm = new FormConfig("pageProxy");
			 frm.ShowDialog();
			 frm.Dispose();
			 LoadProxys();
		 }


	}
}
