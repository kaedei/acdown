using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Kaedei.AcDown.Interface;
using Kaedei.AcDown.Component;
using Kaedei.AcDown.Interface.Forms;
using System.Collections.Generic;

namespace Kaedei.AcDown.UI
{
	 public partial class FormNew : Form
	 {

		 private static FormNew instance;
		 //外部Url
		 private static string u;
		 //插件管理器
		 private static PluginManager _pluginMgr;
		 //任务管理器
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
		 /// 显示新建窗体
		 /// </summary>
		 public static void ShowForm(string url)
		 {
			 u = url;
			 if (!string.IsNullOrEmpty(u))
			 {
				 instance.txtInput.Text = u;
			 }
			 instance.LoadProxys();
			 instance.Show();
			 instance.txtInput.Focus();
			 instance.Activate();
		 }

		 /// <summary>
		 /// 检查是否支持解析指定的URL
		 /// </summary>
		 /// <param name="url">需要检查的Url</param>
		 /// <returns></returns>
		 public static bool CheckUrl(string url)
		 {
			 if (instance != null)
			 {
				 foreach (var item in _pluginMgr.Plugins)
				 {
					 if (item.CheckUrl(url)) //检查成功
					 {
						 return true;
					 }
				 }
			 }
			 return false;
		 }

		 private FormNew()
		 {
			 InitializeComponent();
			 this.Icon = Kaedei.AcDown.Properties.Resources.Ac;
		 }
		 
		 private void FormNew_Load(object sender, EventArgs e)
		 {
			 //显示默认文字
			 if (!string.IsNullOrEmpty(u))
				 txtInput.Text = u;
			 txtPath.Text = Config.setting.SavePath;
			 //填充代理服务器
			 LoadProxys();

			 //显示在线解析引擎选项
			 if (!Config.setting.Plugin_Enable_Flvcd)
			 {
				 chkFlvcd.Visible = false;
				 lblVideoType.Visible = false;
				 cboVideoType.Visible = false;
			 }
			 cboVideoType.SelectedIndex = 0;
			 //显示下载弹幕/字幕选项
			 if (Config.setting.DownSub)
				 rdoDownSub.Checked = true;
			 else
				 rdoNotDownSub.Checked = true;
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
		 
		 //检查Url
		 private void txtInput_TextChanged(object sender, EventArgs e)
		 {
			 string t = txtInput.Text;
			 if (Config.setting.AutoCheckUrl)
			 {
				 if (t.Length != 0)
				 {
					 picCheck.Visible = true;
					 if (CheckUrl(t)) //检查url 设置图片
						 picCheck.Image = Properties.Resources._1;
					 else
						 picCheck.Image = Properties.Resources._2;
				 }
				 else
				 {
					 picCheck.Visible = false;
				 }
			 }
			 //设置checkbox
			 if (txtInput.Text.StartsWith("+"))
				 chkFlvcd.Checked = true;
			 else
				 chkFlvcd.Checked = false;
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
			 List<IAcdownPluginInfo> plugins = new List<IAcdownPluginInfo>();
			 IAcdownPluginInfo selectedPlugin = null;
			 foreach (var item in _pluginMgr.Plugins)
			 {
				 if (item.CheckUrl(url))
				 {
					 plugins.Add(item);
				 }
			 }
			 //如果有可用插件
			 if (plugins.Count > 0)
			 {
				 //有多个插件可供选择时，用户选择插件
				 if(plugins.Count >1)
				 {
					 List<string> pluginNames = new List<string>();
					 foreach (var item in plugins)
					 {
						 pluginNames.Add(item.Describe);
					 }
					 int selected = ToolForm.CreateSelectServerForm("请选择需要使用的下载插件", pluginNames.ToArray(), 0);
					 selectedPlugin = plugins[selected];
				 }
				 else
				 {
					 selectedPlugin = plugins[0];
				 }

				 //取得此url的hash
				 string hash = selectedPlugin.GetHash(url);
				 //检查是否有已经在进行的相同任务
				 foreach (TaskInfo task in _taskMgr.TaskInfos)
				 {
					 if (hash == task.Hash)
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
					 if (Config.setting.Proxy_Settings != null)
					 {
						 foreach (AcDownProxy item in Config.setting.Proxy_Settings)
						 {
							 if (item.Name == cboProxy.SelectedItem.ToString())
								 selectedProxy = item;
						 }
					 }
					 
					 //添加任务
					 TaskInfo task = _taskMgr.AddTask(selectedPlugin,
																 url,
																 (selectedProxy == null) ? null : selectedProxy.ToWebProxy());
					 
					 //设置字幕
					 DownloadSubtitleType ds = DownloadSubtitleType.DownloadSubtitle;
					 if (rdoNotDownSub.Checked) ds = DownloadSubtitleType.DontDownloadSubtitle;
					 if (rdoDownSubOnly.Checked) ds = DownloadSubtitleType.DownloadSubtitleOnly;
					 task.DownSub = ds;
					 //设置注释
					 task.Comment = txtComment.Text;

					 //设置ListView
					 //ListViewItem lsi = new ListViewItem();


					 //开始下载
					 _taskMgr.StartTask(task);
					 
					 this.Cursor = Cursors.Default;
					 this.Close();
				 }
				 catch (Exception ex)
				 {
					 Logging.Add(ex);
					 toolTip.Show("新建任务出现错误:\n" + ex.Message, btnAdd, 4000);
				 }
			 }
			 else
			 {
				 toolTip.Show("您所输入的网络地址(URL)不符合规则。\n没有支持解析此网址的插件，请您检查后重新输入", txtInput, 3000);
				 txtInput.SelectAll();
			 }
			 this.Cursor = Cursors.Default;
			
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
			 //选择文件夹
			 FolderBrowserDialog fbd = new FolderBrowserDialog();
			 fbd.ShowNewFolderButton = true;
			 fbd.Description = "为您的下载选择一个目标文件夹：";
			 fbd.SelectedPath = this.txtPath.Text;
			 if (fbd.ShowDialog() == DialogResult.OK)
				 this.txtPath.Text = fbd.SelectedPath;
		 }

		 private void lnkPaste_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		 {
			 if (Clipboard.ContainsText())
			 {
				 if (txtInput.Text.StartsWith("+"))
					 txtInput.Text = "+" + Clipboard.GetText();
				 else
					 txtInput.Text = Clipboard.GetText();
			 }
		 }

		 private void lnkSetProxy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		 {
			 FormConfig frm = new FormConfig("pageProxy");
			 this.TopMost = false;
			 frm.ShowDialog();
			 this.TopMost = true;
			 frm.Dispose();
			 LoadProxys();
		 }

		 private void chkFlvcd_CheckedChanged(object sender, EventArgs e)
		 {
			 if (chkFlvcd.Checked)
			 {
				 if (!txtInput.Text.StartsWith("+"))
					 txtInput.Text = "+" + txtInput.Text;
				 if (Config.setting.Plugin_Enable_Flvcd)
				 {
					 cboVideoType.Enabled = true;
				 }
				 tabNew.SelectTab(tabOnline);
			 }
			 else
			 {
				 if (txtInput.Text.StartsWith("+"))
					 txtInput.Text = txtInput.Text.TrimStart('+');
				 if (Config.setting.Plugin_Enable_Flvcd)
				 {
					 cboVideoType.SelectedIndex = 0;
					 cboVideoType.Enabled = false;
				 }
			 }
		 }

		 private void cboVideoType_SelectedIndexChanged(object sender, EventArgs e)
		 {
			 string end  = "";
			 switch(cboVideoType.SelectedIndex)
			 {
				 case 1: //高清(360P)
					 end = @"&format=high";
					 break;
				 case 2: //超清
					 end=@"&format=super";
					 break;
				 case 3: //原画
					 end = @"&format=real";
					 break;
				 default: //默认
					 end="";
					 break;
			 }
			 //替换Url
			 txtInput.Text = txtInput.Text.Replace(@"&format=high", "");
			 txtInput.Text = txtInput.Text.Replace(@"&format=super", "");
			 txtInput.Text = txtInput.Text.Replace(@"&format=real", "");
			 txtInput.Text += end;
			 //显示提示
			 if (end != "")
				 toolTip.Show("只有部分支持网站支持清晰度切换。\n选择视频未包含的清晰度可能会造成解析失败", this.cboVideoType, 2500);

		 }


	}
}
