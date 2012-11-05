using System;
using System.Windows.Forms;
using Kaedei.AcDown.Interface;
using Kaedei.AcDown.Core;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Kaedei.AcDown.Interface.UI;

namespace Kaedei.AcDown.UI
{
	public partial class FormNew : FormBase
	{

		private static FormNew instance;
		//外部Url
		private static string u;

		static FormNew()
		{
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
			if (instance != null && !string.IsNullOrEmpty(url))
			{
				foreach (var item in CoreManager.PluginManager.Plugins)
				{
					try
					{
						if (item.CheckUrl(url)) //检查成功
						{
							return true;
						}
					}
					catch { }
				}
			}
			return false;
		}

		private FormNew()
		{
			InitializeComponent();
			this.Size = new System.Drawing.Size(440, 230);
			this.Icon = Kaedei.AcDown.Properties.Resources.Ac;
		}

		private void FormNew_Load(object sender, EventArgs e)
		{
			//设置提示文字
			DwmApi.SetTextBoxTipText(txtInput.Handle, "将网页地址粘贴或填写到此处");
			//显示默认文字
			if (!string.IsNullOrEmpty(u))
			{
				txtInput.Text = u;
			}
			cboPath.Items.Add(CoreManager.ConfigManager.Settings.SavePath);
			cboPath.Items.AddRange(CoreManager.ConfigManager.Settings.SaveFolders.ToArray());
			cboPath.Items.Add(CLEAR_HISTORY);
			cboPath.SelectedIndex = 0;
			//填充代理服务器
			LoadProxys();

			//下载类型
			for (int i = 0; i < lstDownloadType.Items.Count; i++)
			{
				lstDownloadType.SetItemChecked(i, true);
			}

			//解析关联的下载项
			chkParseRelated.Checked = CoreManager.ConfigManager.Settings.ParseRelated;
			//自动应答
			chkAutoAnswer.Checked = CoreManager.ConfigManager.Settings.AutoAnswer;
			//缓存提取
			chkExtractCache.Checked = CoreManager.ConfigManager.Settings.ExtractCache;

			//设置焦点
			btnAdd.Focus();

		}

		//读取代理服务器设置
		private void LoadProxys()
		{
			cboProxy.Items.Clear();
			if (!AcDown.Interface.Tools.IsRunningOnMono)
				cboProxy.Items.Add("使用IE设置");
			else
				cboProxy.Items.Add("使用系统设置");
			cboProxy.Items.Add("直接连接");
			if (CoreManager.ConfigManager.Settings.Proxy_Settings != null)
			{
				foreach (AcDownProxy item in CoreManager.ConfigManager.Settings.Proxy_Settings)
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
			//清除已有列表
			supportedPlugins.Clear();
			cboPlugins.Items.Clear();
			cboPlugins_SelectedIndexChanged(sender, EventArgs.Empty);

			if (t.Length > 0) //如果文本不为空
			{
				picCheck.Visible = true;
				//查找所有候选插件
				foreach (var item in CoreManager.PluginManager.Plugins)
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
					if (!AcDown.Interface.Tools.IsRunningOnMono)
						picCheck.Image = Properties.Resources._1;
					//清除下拉列表的选择
					cboPlugins.SelectedIndex = 0;
					//按钮可以按下
					btnAdd.Enabled = true;
				}
				else
				{
					//设置哭脸图片
					if (!AcDown.Interface.Tools.IsRunningOnMono)
						picCheck.Image = Properties.Resources._2;
					//清除下拉列表的选择
					//cboPlugins.SelectedIndex = -1;
					//按钮不可按下
					btnAdd.Enabled = false;
				}
			}
			else
			{
				picCheck.Visible = false;
				btnAdd.Enabled = true;
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

			IPlugin selectedPlugin = null;

			//如果有可用插件
			if (supportedPlugins.Count > 0)
			{

				selectedPlugin = supportedPlugins[cboPlugins.SelectedIndex];

				//取得此url的hash
				string hash = selectedPlugin.GetHash(url);
				//检查是否有已经在进行的相同任务
				foreach (TaskInfo task in CoreManager.TaskManager.TaskInfos)
				{
					if (hash.Equals(task.Hash, StringComparison.InvariantCultureIgnoreCase))
					{
						toolTip.Show("当前任务已经存在", txtInput, 4000);
						this.Cursor = Cursors.Default;
						return;
					}
				}
				try
				{
					//取得[代理设置]
					WebProxy selectedProxy = null;
					if (cboProxy.SelectedIndex == 0) //IE设置
						//将WebProxy设置为null可以使用默认IE设置
						//与WebRequest.DefaultWebProxy的区别在于设置为null时不会自动检测代理设置
						//详情请见 http://msdn.microsoft.com/en-us/library/fze2ytx2.aspx
						selectedProxy = null;
					else if (cboProxy.SelectedIndex == 1) //直接连接
						selectedProxy = new WebProxy();
					else if (CoreManager.ConfigManager.Settings.Proxy_Settings != null)
					{
						foreach (AcDownProxy item in CoreManager.ConfigManager.Settings.Proxy_Settings)
						{
							if (item.Name == cboProxy.SelectedItem.ToString())
							{
								selectedProxy = item.ToWebProxy();
								break;
							}
						}
					}

					//取得[AutoAnswer设置]
					List<AutoAnswer> aa = new List<AutoAnswer>();
					if (chkAutoAnswer.Checked)
					{
						if (selectedPlugin.Feature != null)
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
					}

					//添加任务
					TaskInfo task = CoreManager.TaskManager.AddTask(selectedPlugin, url.Trim(), selectedProxy);
					//设置[保存目录]
					task.SaveDirectory = new DirectoryInfo(cboPath.Text);
					//设置[下载类型]
					DownloadType ds = DownloadType.None;
					if (lstDownloadType.GetItemChecked(0))
						ds = ds | DownloadType.Video;
					if (lstDownloadType.GetItemChecked(1))
						ds = ds | DownloadType.Audio;
					if (lstDownloadType.GetItemChecked(2))
						ds = ds | DownloadType.Picture;
					if (lstDownloadType.GetItemChecked(3))
						ds = ds | DownloadType.Text;
					if (lstDownloadType.GetItemChecked(4))
						ds = ds | DownloadType.Subtitle;
					if (lstDownloadType.GetItemChecked(5))
						ds = ds | DownloadType.Comment;
					task.DownloadTypes = ds;
					//设置[提取浏览器缓存]
					task.ExtractCache = chkExtractCache.Checked;
					//设置[解析关联视频]
					task.ParseRelated = chkParseRelated.Checked;
					//设置[自动应答]
					task.AutoAnswer = aa;
					//设置注释
					task.Comment = txtComment.Text;


					//开始下载
					CoreManager.TaskManager.StartTask(task);


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

		/// <summary>
		/// 选择保存文件夹
		/// </summary>
		private void lnkBrowseDir_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			//选择文件夹
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			fbd.ShowNewFolderButton = true;
			fbd.Description = "为您的下载选择一个目标文件夹：";
			fbd.SelectedPath = this.cboPath.Text;
			if (fbd.ShowDialog() == DialogResult.OK)
			{
				if(fbd.SelectedPath.Equals(CoreManager.ConfigManager.Settings.SavePath))
				{
					cboPath.SelectedIndex = 0;
				}
				else if (CoreManager.ConfigManager.Settings.SaveFolders.Contains(fbd.SelectedPath))
				{
					cboPath.SelectedIndex = CoreManager.ConfigManager.Settings.SaveFolders.IndexOf(fbd.SelectedPath) + 1;
				}
				else
				{
					CoreManager.ConfigManager.Settings.SaveFolders.Add(fbd.SelectedPath);
					CoreManager.ConfigManager.SaveSettings();
					cboPath.Items.Clear();
					cboPath.Items.Add(CoreManager.ConfigManager.Settings.SavePath);
					cboPath.Items.AddRange(CoreManager.ConfigManager.Settings.SaveFolders.ToArray());
					cboPath.Items.Add(CLEAR_HISTORY);
					cboPath.SelectedIndex = cboPath.Items.Count - 2;
				}
			}
		}

		//切换状态
		private void chkAdvance_CheckedChanged(object sender, EventArgs e)
		{
			if (chkAdvance.Checked)
			{
				this.Size = new System.Drawing.Size(440, 480);
				chkAdvance.Text = "更多选项 <<";
			}
			else
			{
				this.Size = new System.Drawing.Size(440, 230);
				chkAdvance.Text = "更多选项 >>";
			}
		}

		private void cboPlugins_SelectedIndexChanged(object sender, EventArgs e)
		{
			lnkPluginConfig.Visible = false;
			if (cboPlugins.Items.Count == 0)
				return;
			int index = cboPlugins.SelectedIndex;
			var plugin = supportedPlugins[index];
			if (plugin.Feature == null)
				return;
			if (plugin.Feature.ContainsKey("ConfigForm"))
			{
				var t = plugin.Feature["ConfigForm"].GetType();
				if (t.IsSubclassOf(typeof(Delegate)) || t.IsSubclassOf(typeof(MulticastDelegate)))
					lnkPluginConfig.Visible = true;
			}
		}

		//插件配置
		private void lnkPluginConfig_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			int index = cboPlugins.SelectedIndex;
			var plugin = supportedPlugins[index];
			var method = (Delegate)plugin.Feature["ConfigForm"];
			this.TopMost = false;
			try
			{
				this.Invoke(method);
			}
			catch
			{
			}
			this.TopMost = true;
			//保存设置
			CoreManager.PluginManager.SaveConfiguration(plugin);
		}

		//从剪贴板粘贴
		private void lnkPaste_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				if (Clipboard.ContainsText())
					txtInput.Text = Clipboard.GetText().Trim();
			}
			catch { }
		}

		//清除历史纪录
		private const string CLEAR_HISTORY = "清除历史记录";
		private void cboPath_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cboPath.Text.Equals(CLEAR_HISTORY))
			{
				CoreManager.ConfigManager.Settings.SaveFolders.Clear();
				CoreManager.ConfigManager.SaveSettings();
				cboPath.Items.Clear();
				cboPath.Items.Add(CoreManager.ConfigManager.Settings.SavePath);
				cboPath.Items.AddRange(CoreManager.ConfigManager.Settings.SaveFolders.ToArray());
				cboPath.Items.Add(CLEAR_HISTORY);
				cboPath.SelectedIndex = 0;
			}
		}





	}
}
