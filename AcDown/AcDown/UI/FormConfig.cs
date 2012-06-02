using System;
using System.Windows.Forms;
using Kaedei.AcDown.Properties;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Kaedei.AcDown.Core;
using Kaedei.AcDown.Interface;

namespace Kaedei.AcDown.UI
{
   public partial class FormConfig : Form
   {
      private PluginManager pMgr;

      public FormConfig()
      {
         InitializeComponent();
      }

      public FormConfig(PluginManager pluginManager):this()
      {
         pMgr = pluginManager;
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
         chkDownSub.Checked = Config.setting.DownSub;
         chkOpenFolder.Checked = Config.setting.OpenFolderAfterComplete;
         chkPlaySound.Checked = Config.setting.PlaySound;
         if (!string.IsNullOrEmpty(Config.setting.SoundFile))
         {
            chkCustomSound.Checked = true;
            txtCustomSound.Text = Config.setting.SoundFile;
         }
         chkParseRelated.Checked = Config.setting.ParseRelated;
			chkEnableExtractCache.Checked = Config.setting.EnableExtractCache;
         numCacheSize.Value = Config.setting.CacheSize;
         txtSavePath.Text = Config.setting.SavePath;
         chkEnableLog.Checked = Config.setting.EnableLog;
         chkWatch.Checked = Config.setting.WatchClipboardEnabled;
         chkDeleteFile.Checked = Config.setting.DeleteTaskAndFile;
         if (!Config.IsWindows7OrHigher()) chkEnableWin7.Enabled = false;
         chkEnableWin7.Checked = Config.setting.EnableWindows7Feature;
         chkHideWhenClose.Checked = Config.setting.HideWhenClose;
         cboMaxRunningCount.SelectedIndex = Config.setting.MaxRunningTaskCount - 1;
         txtSearchText.Text = Config.setting.SearchQuery;
         udRefreshInfo.Value = Config.setting.RefreshInfoInterval;
			udToolFormTimeout.Value = Config.setting.ToolFormTimeout;
         chkEnableCheckUpdate.Checked = Config.setting.EnableCheckUpdate;
         udNetworkTimeout.Value = Config.setting.NetworkTimeout;
			
         if (Config.setting.CheckUpdateDocument == "stable")
            rdoChannelStable.Checked = true;
         else if (Config.setting.CheckUpdateDocument == "develop")
            rdoChannelDevelop.Checked = true;
         else
         {
            rdoChannelCustom.Checked = true;
            txtUpdateDocument.Text = Config.setting.CheckUpdateDocument;
         }
         udRetryTimes.Value = Config.setting.RetryTimes;
         udRetryWatingTime.Value = Config.setting.RetryWaitingTime / 1000;

         //插件设置
         pluginSettings1.SetPluginManager(pMgr);

         //代理服务器设置
         if (Config.setting.Proxy_Settings != null)
         {
            foreach (AcDownProxy item in Config.setting.Proxy_Settings)
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
         Config.setting.DownSub = chkDownSub.Checked;
         Config.setting.OpenFolderAfterComplete = chkOpenFolder.Checked;
         Config.setting.PlaySound = chkPlaySound.Checked;
         if (chkCustomSound.Checked)
         {
            Config.setting.SoundFile = txtCustomSound.Text;
         }
         else
         {
            Config.setting.SoundFile = "";
         }
         Config.setting.ParseRelated = chkParseRelated.Checked;
         Config.setting.CacheSize = (Int32)numCacheSize.Value;
         Config.setting.SavePath = txtSavePath.Text;
         Config.setting.EnableLog = chkEnableLog.Checked;
         Config.setting.WatchClipboardEnabled = chkWatch.Checked;
         Config.setting.DeleteTaskAndFile = chkDeleteFile.Checked;
			Config.setting.EnableExtractCache = chkEnableExtractCache.Checked;
         Config.setting.EnableWindows7Feature = chkEnableWin7.Checked;
         Config.setting.HideWhenClose = chkHideWhenClose.Checked;
         Config.setting.MaxRunningTaskCount = cboMaxRunningCount.SelectedIndex + 1;
         Config.setting.SearchQuery = txtSearchText.Text;
         Config.setting.RefreshInfoInterval = (Int32)udRefreshInfo.Value;
			Config.setting.ToolFormTimeout = (Int32)udToolFormTimeout.Value;
         Config.setting.NetworkTimeout = (Int32)udNetworkTimeout.Value;
         Config.setting.EnableCheckUpdate = chkEnableCheckUpdate.Checked;
         if (rdoChannelStable.Checked)
            Config.setting.CheckUpdateDocument = "stable";
         if (rdoChannelDevelop.Checked)
            Config.setting.CheckUpdateDocument = "develop";
         if (rdoChannelCustom.Checked)
            Config.setting.CheckUpdateDocument = txtUpdateDocument.Text;
         Config.setting.RetryTimes = (Int32)udRetryTimes.Value;
         Config.setting.RetryWaitingTime = (Int32)udRetryWatingTime.Value * 1000;
			

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
         Config.setting.Proxy_Settings = proxys.ToArray();
         //保存设置
         Config.SaveSettings();
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
         if (Config.setting.EnableLog)
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
         Config.setting = s;
         Config.SaveSettings();
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
         //取得APPDATA路径名称
         string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
         path = Path.Combine(path, @"Kaedei\AcDown\");

         try
         {
            Process.Start(path);
         }
         catch
         {
         }
      }

      private void rdoChannelCustom_CheckedChanged(object sender, EventArgs e)
      {
         txtUpdateDocument.Enabled = rdoChannelCustom.Checked;
      }

   }
}
