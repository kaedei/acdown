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
			 //显示在线解析引擎选项
			 if (!Config.setting.Plugin_Enable_Flvcd)
			 {
				 chkFlvcd.Visible = false;
				 lblVideoType.Visible = false;
				 cboVideoType.Visible = false;
			 }
			 cboVideoType.SelectedIndex = 0;
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
				 foreach (TaskItem downloader in _taskMgr.Tasks)
				 {
					 if (downloader.GetBasePlugin().GetHash(downloader.Url) == hash)
					 {
						 tabNew.SelectTab(tabInput);
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
					 TaskItem downloader = _taskMgr.AddTask(new TaskItem(p.CreateDownloader(), null), url,
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
				 tabNew.SelectTab(tabInput);
				 toolTip.Show("网络地址(URL)不符合规则，请检查后重新输入。\n您也可以选择使用在线解析引擎解析此地址", txtInput, 3000);
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
			 fbd.Description = "请设置默认保存的文件夹：";
			 fbd.SelectedPath = Config.setting.SavePath;
			 if (fbd.ShowDialog() == DialogResult.OK)
				 Config.setting.SavePath = fbd.SelectedPath;
			 this.txtPath.Text = Config.setting.SavePath;
			 Config.SaveSettings();
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
					 lblVideoType.Visible = true;
					 cboVideoType.Visible = true;
				 }
			 }
			 else
			 {
				 if (txtInput.Text.StartsWith("+"))
					 txtInput.Text = txtInput.Text.TrimStart('+');
				 if (Config.setting.Plugin_Enable_Flvcd)
				 {
					 lblVideoType.Visible = false;
					 cboVideoType.Visible = false;
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
