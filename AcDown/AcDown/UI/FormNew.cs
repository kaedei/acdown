using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Kaedei.AcDown.Interface;
using Kaedei.AcDown.Core;
using Kaedei.AcDown.Interface.Forms;
using System.Collections.Generic;
using System.IO;

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
			instance.btnAdd.Focus();
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
			if (Config.IsWindowsVistaOrHigher())
			{
				//设置提示文字
				SendMessage(txtInput.Handle, 0x1501, IntPtr.Zero, System.Text.Encoding.Unicode.GetBytes(@"将网页地址粘贴或填写到此处"));
			}
			//显示默认文字
			if (!string.IsNullOrEmpty(u))
			{
				txtInput.Text = u;
			}
			txtPath.Text = Config.setting.SavePath;
			//填充代理服务器
			LoadProxys();

			//显示下载弹幕/字幕选项
			if (Config.setting.DownSub)
				cboDownSub.SelectedIndex = 0;
			else
				cboDownSub.SelectedIndex = 1;

			//解析关联的下载项
			chkParseRelated.Checked = Config.setting.ParseRelated;

			//自动应答
			chkAutoAnswer.Checked = Config.setting.EnableAutoAnswer;
			//缓存提取
			chkExtractCache.Checked = Config.setting.EnableExtractCache;

			//设置焦点
			btnAdd.Focus();
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


		//候选插件列表
		private List<IPlugin> supportedPlugins = new List<IPlugin>();
		//检查输入的Url
		private void txtInput_TextChanged(object sender, EventArgs e)
		{
			string t = txtInput.Text.Trim();  //去除空格

			if (t.Length > 0) //如果文本不为空
			{
				picCheck.Visible = true;
				//清除已有列表
				supportedPlugins.Clear();
				cboPlugins.Items.Clear();
				//查找所有候选插件
				foreach (var item in _pluginMgr.Plugins)
				{
					if (item.CheckUrl(t))
					{
						supportedPlugins.Add(item);
					}
				}
				//填充下拉列表
				foreach (var item in supportedPlugins)
				{
					object[] types = item.GetType().GetCustomAttributes(typeof(AcDownPluginInformationAttribute), true);
					var attrib = (AcDownPluginInformationAttribute)types[0];
					cboPlugins.Items.Add(attrib.FriendlyName);
				}
				//如果有插件支持
				if (supportedPlugins.Count > 0)
				{
					//设置笑脸图片
					picCheck.Image = Properties.Resources._1;
					//清除下拉列表的选择
					cboPlugins.SelectedIndex = 0;
					//支持的插件数量大于1个时才显示下拉列表
					if (supportedPlugins.Count > 1)
					{
						panelSelectPlugin.Visible = true;
					}
					else
					{
						panelSelectPlugin.Visible = false;
					}
					//按钮可以按下
					btnAdd.Enabled = true;
				}
				else
				{
					//设置哭脸图片
					picCheck.Image = Properties.Resources._2;
					//清除下拉列表的选择
					cboPlugins.SelectedIndex = -1;
					//显示下拉列表
					panelSelectPlugin.Visible = false;
					//按钮不可按下
					btnAdd.Enabled = false;
				}

				btnAdd.Text = "添加任务";
			}
			else
			{
				panelSelectPlugin.Visible = false;
				picCheck.Visible = false;
				btnAdd.Enabled = true;
				btnAdd.Text = "粘贴并添加";
			}
		}

		/// <summary>
		/// 添加任务
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAdd_Click(object sender, EventArgs e)
		{
			//判断是否是“粘贴并添加”
			if (txtInput.Text.Trim() == "" && Clipboard.ContainsText()) //如果文本框为空则为“粘贴并添加”
			{
				txtInput.Text = Clipboard.GetText();
			}


			string url = txtInput.Text;
			this.Cursor = Cursors.WaitCursor;

			IPlugin selectedPlugin = null;

			//如果有可用插件
			if (supportedPlugins.Count > 0)
			{

				selectedPlugin = supportedPlugins[cboPlugins.SelectedIndex];

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
					//取得[代理设置]
					AcDownProxy selectedProxy = null;
					if (Config.setting.Proxy_Settings != null)
					{
						foreach (AcDownProxy item in Config.setting.Proxy_Settings)
						{
							if (item.Name == cboProxy.SelectedItem.ToString())
								selectedProxy = item;
						}
					}

					//取得[AutoAnswer设置]
					List<AutoAnswer> aa = new List<AutoAnswer>();
					if (chkAutoAnswer.Checked)
					{
						if (selectedPlugin.Feature.ContainsKey("AutoAnswer"))
						{
							aa = (List<AutoAnswer>)selectedPlugin.Feature["AutoAnswer"];
							if (aa.Count > 0)
							{
								FormAutoAnswer faa = new FormAutoAnswer(aa);
                                faa.TopMost = this.TopMost;
								var result = faa.ShowDialog();
								if (result == System.Windows.Forms.DialogResult.Cancel)
								{
									this.Cursor = Cursors.Default;
									return;
								}
							}
						}
					}

					//添加任务
					TaskInfo task = _taskMgr.AddTask(selectedPlugin,
																url,
																(selectedProxy == null) ? null : selectedProxy.ToWebProxy());
					//设置[保存目录]
					task.SaveDirectory = new DirectoryInfo(txtPath.Text);
					//设置[字幕]
					DownloadSubtitleType ds = DownloadSubtitleType.DownloadSubtitle;
					if (cboDownSub.SelectedIndex == 1) ds = DownloadSubtitleType.DontDownloadSubtitle;
					if (cboDownSub.SelectedIndex == 2) ds = DownloadSubtitleType.DownloadSubtitleOnly;
					task.DownSub = ds;
					//设置[提取浏览器缓存]
					task.ExtractCache = chkExtractCache.Checked;
					//设置[解析关联视频]
					task.ParseRelated = chkParseRelated.Checked;
					//设置[自动应答]
					task.AutoAnswer = aa;
					//设置注释
					task.Comment = txtComment.Text;


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
			panelSelectPlugin.Hide();
			this.Hide();
		}

		//选择保存文件夹
		private void btnSelectDir_Click(object sender, EventArgs e)
		{
			//选择文件夹
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			fbd.ShowNewFolderButton = true;
			fbd.Description = "为您的下载选择一个目标文件夹：";
			fbd.SelectedPath = this.txtPath.Text;
			if (fbd.ShowDialog() == DialogResult.OK)
				this.txtPath.Text = fbd.SelectedPath;
		}

		//设置代理服务器
		private void btnSetProxy_Click(object sender, EventArgs e)
		{
			FormConfig frm = new FormConfig("pageProxy");
			this.TopMost = false;
			frm.ShowDialog();
			this.TopMost = true;
			frm.Dispose();
			LoadProxys();
		}


	}
}
