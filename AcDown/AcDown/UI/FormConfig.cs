using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Kaedei.AcDown.Core;
using Kaedei.AcDown.Interface;
using Kaedei.AcDown.Interface.UI;

namespace Kaedei.AcDown.UI
{
	public partial class FormConfig : FormBase
	{

		public FormConfig()
		{
			InitializeComponent();
		}

		public FormConfig(string selectTabPage) :this()
		{
			try 
			{
				TabPage p = tab.TabPages[selectTabPage];
				tab.TabPages.Clear();
				tab.TabPages.Add(p);
				btnDefault.Visible = false;
			}
			catch { }
		}

		private void FormConfig_Load(object sender, EventArgs e)
		{
			//视觉效果
				DwmApi.SetListViewVisualEffect(this.lsvProxy);
			DwmApi.SetShieldIcon(btnSetFileAsso);

			//Mono
			if (Tools.IsRunningOnMono)
			{
				chkWatch.Enabled = false;
				chkWatchShortUrl.Enabled = false;
				chkEnableWin7.Enabled = false;
			}

			//设置控件状态
			chkOpenFolder.Checked = CoreManager.ConfigManager.Settings.OpenFolderAfterComplete;
			chkPlaySound.Checked = CoreManager.ConfigManager.Settings.PlaySound;
			if (!string.IsNullOrEmpty(CoreManager.ConfigManager.Settings.SoundFile))
			{
				chkCustomSound.Checked = true;
				txtCustomSound.Text = CoreManager.ConfigManager.Settings.SoundFile;
			}
			chkParseRelated.Checked = CoreManager.ConfigManager.Settings.ParseRelated;
			chkEnableExtractCache.Checked = CoreManager.ConfigManager.Settings.ExtractCache;
			numCacheSize.Value = CoreManager.ConfigManager.Settings.CacheSize;
			txtSavePath.Text = CoreManager.ConfigManager.Settings.SavePath;
			chkEnableLog.Checked = CoreManager.ConfigManager.Settings.Logging;
			chkWatch.Checked = CoreManager.ConfigManager.Settings.WatchClipboard;
			chkWatchShortUrl.Checked = CoreManager.ConfigManager.Settings.WatchClipboardShortUrl;
			chkDeleteFile.Checked = CoreManager.ConfigManager.Settings.DeleteTaskAndFile;
			if (!DwmApi.IsWindows7OrHigher()) chkEnableWin7.Enabled = false;
			chkEnableWin7.Checked = CoreManager.ConfigManager.Settings.Windows7Feature;
			chkHideWhenClose.Checked = CoreManager.ConfigManager.Settings.HideWhenClose;
			cboMaxRunningCount.SelectedIndex = CoreManager.ConfigManager.Settings.MaxRunningTaskCount - 1;
			txtSearchText.Text = CoreManager.ConfigManager.Settings.SearchQuery;
			udRefreshInfo.Value = CoreManager.ConfigManager.Settings.RefreshInfoInterval;
			udToolFormTimeout.Value = CoreManager.ConfigManager.Settings.ToolFormTimeout;
			chkEnableCheckUpdate.Checked = CoreManager.ConfigManager.Settings.CheckUpdate;
			udNetworkTimeout.Value = CoreManager.ConfigManager.Settings.NetworkTimeout;
			
			if (CoreManager.ConfigManager.Settings.CheckUpdateDocument == "stable")
				rdoChannelStable.Checked = true;
			else if (CoreManager.ConfigManager.Settings.CheckUpdateDocument == "develop")
				rdoChannelDevelop.Checked = true;
			else
			{
				rdoChannelCustom.Checked = true;
				txtUpdateDocument.Text = CoreManager.ConfigManager.Settings.CheckUpdateDocument;
			}
			udRetryTimes.Value = CoreManager.ConfigManager.Settings.RetryTimes;
			udRetryWatingTime.Value = CoreManager.ConfigManager.Settings.RetryWaitingTime / 1000;

			//代理服务器设置
			if (CoreManager.ConfigManager.Settings.Proxy_Settings != null)
			{
				foreach (AcDownProxy item in CoreManager.ConfigManager.Settings.Proxy_Settings)
				{
					lsvProxy.Items.Add(new ListViewItem(new string[] 
					{
						item.Name,
						item.Address,
						item.Port.ToString() ,
						item.Username,
						item.Password
					}));
				}
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			//保存设置
			//CoreManager.ConfigManager.Settings.DownSub = chkDownSub.Checked;
			CoreManager.ConfigManager.Settings.OpenFolderAfterComplete = chkOpenFolder.Checked;
			CoreManager.ConfigManager.Settings.PlaySound = chkPlaySound.Checked;
			if (chkCustomSound.Checked)
			{
				CoreManager.ConfigManager.Settings.SoundFile = txtCustomSound.Text;
			}
			else
			{
				CoreManager.ConfigManager.Settings.SoundFile = "";
			}
			CoreManager.ConfigManager.Settings.ParseRelated = chkParseRelated.Checked;
			CoreManager.ConfigManager.Settings.CacheSize = (Int32)numCacheSize.Value;
			CoreManager.ConfigManager.Settings.SavePath = txtSavePath.Text;
			CoreManager.ConfigManager.Settings.Logging = chkEnableLog.Checked;
			CoreManager.ConfigManager.Settings.WatchClipboard = chkWatch.Checked;
			CoreManager.ConfigManager.Settings.WatchClipboardShortUrl = chkWatchShortUrl.Checked;
			CoreManager.ConfigManager.Settings.DeleteTaskAndFile = chkDeleteFile.Checked;
			CoreManager.ConfigManager.Settings.ExtractCache = chkEnableExtractCache.Checked;
			CoreManager.ConfigManager.Settings.Windows7Feature = chkEnableWin7.Checked;
			CoreManager.ConfigManager.Settings.HideWhenClose = chkHideWhenClose.Checked;
			CoreManager.ConfigManager.Settings.MaxRunningTaskCount = cboMaxRunningCount.SelectedIndex + 1;
			CoreManager.ConfigManager.Settings.SearchQuery = txtSearchText.Text;
			CoreManager.ConfigManager.Settings.RefreshInfoInterval = (Int32)udRefreshInfo.Value;
			CoreManager.ConfigManager.Settings.ToolFormTimeout = (Int32)udToolFormTimeout.Value;
			CoreManager.ConfigManager.Settings.NetworkTimeout = (Int32)udNetworkTimeout.Value;
			CoreManager.ConfigManager.Settings.CheckUpdate = chkEnableCheckUpdate.Checked;
			if (rdoChannelStable.Checked)
				CoreManager.ConfigManager.Settings.CheckUpdateDocument = "stable";
			if (rdoChannelDevelop.Checked)
				CoreManager.ConfigManager.Settings.CheckUpdateDocument = "develop";
			if (rdoChannelCustom.Checked)
				CoreManager.ConfigManager.Settings.CheckUpdateDocument = txtUpdateDocument.Text;
			CoreManager.ConfigManager.Settings.RetryTimes = (Int32)udRetryTimes.Value;
			CoreManager.ConfigManager.Settings.RetryWaitingTime = (Int32)udRetryWatingTime.Value * 1000;
			

			//插件设置

			//代理服务器设置
			List<AcDownProxy> proxys = new List<AcDownProxy>();
			foreach (ListViewItem item in lsvProxy.Items)
			{
				AcDownProxy proxy = new AcDownProxy();
				proxy.Name = item.SubItems[0].Text;
				proxy.Address = item.SubItems[1].Text;
				proxy.Port = int.Parse(item.SubItems[2].Text);
				proxy.Username = item.SubItems[3].Text;
				proxy.Password = item.SubItems[4].Text;
				proxys.Add(proxy);
			}
			CoreManager.ConfigManager.Settings.Proxy_Settings = proxys.ToArray();
			//保存设置
			CoreManager.ConfigManager.SaveSettings();
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void chkPlaySound_CheckedChanged(object sender, EventArgs e)
		{
			chkCustomSound.Enabled = chkPlaySound.Checked;
			txtCustomSound.Enabled = chkCustomSound.Checked;
		}

		private void lnkSavePath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			
		}

		private void txtServerIP_KeyPress(object sender, KeyPressEventArgs e)
		{
			//e.Handled = true;
		}

		private void lnkLog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (CoreManager.ConfigManager.Settings.Logging)
			{
				if (File.Exists(Logging.LogFilePath))
				{
					System.Diagnostics.Process.Start(Logging.LogFilePath);
				}
				else
				{
					MessageBox.Show("当前日志文件为空", "日志文件", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			else
			{
				MessageBox.Show("当前禁止记录日志文件,请设置[启用错误日志]后重启下载器", "日志文件被禁止", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		//查看自定义搜索引擎示例
		private void lnkCustomSearchExample_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(@"http://acdown.codeplex.com/wikipage?title=UI%20-%20%E8%AE%BE%E7%BD%AE%E8%87%AA%E5%AE%9A%E4%B9%89%E6%90%9C%E7%B4%A2%E5%BC%95%E6%93%8E");
		}

		private void btnProxyAdd_Click(object sender, EventArgs e)
		{
			AcDownProxy proxy = new AcDownProxy();
			FormAddProxy frm = new FormAddProxy(proxy);
			frm.ShowDialog();
			if (!string.IsNullOrEmpty(proxy.Name))
			{
				lsvProxy.Items.Add(new ListViewItem(new string[] 
				{
					proxy.Name,
					proxy.Address,
					proxy.Port.ToString() ,
					proxy.Username,
					proxy.Password
				}));
			}
		}

		//修改代理服务器
		private void btnProxyModify_Click(object sender, EventArgs e)
		{
			if (lsvProxy.SelectedIndices.Count > 0)
			{
				//选择的代理服务器
				int selected = lsvProxy.SelectedIndices[0];
				//生成新的AcDownProxy对象
				AcDownProxy proxy = new AcDownProxy();
				proxy.Name = lsvProxy.Items[selected].SubItems[0].Text;
				proxy.Address = lsvProxy.Items[selected].SubItems[1].Text;
				proxy.Port = int.Parse(lsvProxy.Items[selected].SubItems[2].Text);
				proxy.Username = lsvProxy.Items[selected].SubItems[3].Text;
				proxy.Password = lsvProxy.Items[selected].SubItems[4].Text;
				//显示修改窗体
				FormAddProxy frm = new FormAddProxy(proxy);
				frm.ShowDialog();
				//重新加载
				if (!string.IsNullOrEmpty(proxy.Name))
				{
					lsvProxy.Items[selected] = new ListViewItem(new string[] 
					{
						proxy.Name,
						proxy.Address,
						proxy.Port.ToString() ,
						proxy.Username,
						proxy.Password
					});
				}
			}
		}

		private void btnProxyDelete_Click(object sender, EventArgs e)
		{
			if (lsvProxy.SelectedIndices.Count > 0)
			{
				lsvProxy.Items.RemoveAt(lsvProxy.SelectedIndices[0]);
			}
		}

		private void btnSavePath_Click(object sender, EventArgs e)
		{
			//选择文件夹
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			fbd.ShowNewFolderButton = true;
			fbd.Description = "请设置默认保存的文件夹：";
			fbd.SelectedPath = txtSavePath.Text;
			if (fbd.ShowDialog() == DialogResult.OK)
				txtSavePath.Text = fbd.SelectedPath;
		}

		//恢复默认设置
		private void btnDefault_Click(object sender, EventArgs e)
		{
			//询问是否恢复默认设置
			if (MessageBox.Show("恢复默认设置？当前未保存的设置将会丢失", "恢复默认设置", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Cancel)
			{
				return;
			}
			//保存新设置
			AcDownSettings s = new AcDownSettings();
			CoreManager.ConfigManager.Settings = s;
			CoreManager.ConfigManager.SaveSettings();
			//删除Firstrun文件
			FirstrunHandler fh = new FirstrunHandler();
			fh.DeleteFirstRunFile();

			//关闭窗体
			this.Close();
		}

		private void chkCustomSound_CheckedChanged(object sender, EventArgs e)
		{
			txtCustomSound.Enabled = chkCustomSound.Checked;
		}

		private void txtCustomSound_Click(object sender, EventArgs e)
		{
			//用户选择声音文件
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Title = "选择声音文件";
			ofd.Filter = "声音文件(*.wav)|*.wav";
			DialogResult r = ofd.ShowDialog();
			if (r != System.Windows.Forms.DialogResult.Cancel)
			{
				txtCustomSound.Text = ofd.FileName;
			}
		}

		private void lnkOpenConfig_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				Process.Start(CoreManager.StartupPath);
			}
			catch
			{
			}
		}

		private void rdoChannelCustom_CheckedChanged(object sender, EventArgs e)
		{
			txtUpdateDocument.Enabled = rdoChannelCustom.Checked;
		}

		//设置文件关联
		private void btnSetFileAsso_Click(object sender, EventArgs e)
		{
			Process p = new Process();
			p.StartInfo = new ProcessStartInfo()
			{
				FileName = Application.ExecutablePath,
				Arguments = "regasso",
				Verb = "runas"
			};
			//WinXP不使用Verb
			if (!DwmApi.IsWindowsVistaOrHigher())
				p.StartInfo.Verb = "";
			try
			{
				p.Start();
			}
			catch { }
		}

		private void chkWatch_CheckedChanged(object sender, EventArgs e)
		{
			chkWatchShortUrl.Enabled = chkWatch.Checked;
		}

	}
}
