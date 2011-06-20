using System;
using System.Windows.Forms;
using Kaedei.AcDown.Properties;
using System.IO;
using System.Diagnostics;

namespace Kaedei.AcDown.UI
{
   public partial class FormConfig : Form
   {
      public FormConfig()
      {
         InitializeComponent();
      }

      private void FormConfig_Load(object sender, EventArgs e)
      {
         if (Config.setting.DownSub)
            chkDownSub.Checked = true;
         if (Config.setting.OpenFolderAfterComplete)
            chkOpenFolder.Checked = true;
         if (Config.setting.PlaySound)
            chkPlaySound.Checked = true;
         chkDownAllSection.Checked = Config.setting.DownAllSection;
         numCacheSize.Value = Config.setting.CacheSize;
         lnkSavePath.Text = Config.setting.SavePath;
         chkEnableLog.Checked = Config.setting.EnableLog;
         chkCheckUrl.Checked = Config.setting.AutoCheckUrl;
         chkWatch.Checked = Config.setting.WatchClipboardEnabled;
         chkDeleteFile.Checked = Config.setting.DeleteTaskAndFile;
         chkEnableWin7.Checked = Config.setting.EnableWindows7Feature;
         chkShowBigButton.Checked = Config.setting.ShowBigStartButton;
         txtSearchText.Text = Config.setting.SearchQuery;
         //插件设置
         chkPluginAcfun.Checked = Config.setting.Plugin_Enable_Acfun;
         chkPluginTudou.Checked = Config.setting.Plugin_Enable_Tudou;
         chkPluginBilibili.Checked = Config.setting.Plugin_Enable_Bilibili;
         chkPluginYouku.Checked = Config.setting.Plugin_Enable_Youku;
         chkPluginImanhua.Checked = Config.setting.Plugin_Enable_Imanhua;
         chkPluginTiebaAlbum.Checked = Config.setting.Plugin_Enable_TiebaAlbum;

      }

      private void btnOK_Click(object sender, EventArgs e)
      {
         //保存设置
         Config.setting.DownSub = chkDownSub.Checked;
         Config.setting.OpenFolderAfterComplete = chkOpenFolder.Checked;
         Config.setting.PlaySound = chkPlaySound.Checked;
         Config.setting.DownAllSection = chkDownAllSection.Checked;
         Config.setting.CacheSize = (Int32)numCacheSize.Value;
         Config.setting.SavePath = lnkSavePath.Text;
         Config.setting.EnableLog = chkEnableLog.Checked;
         Config.setting.AutoCheckUrl = chkCheckUrl.Checked;
         Config.setting.WatchClipboardEnabled = chkWatch.Checked;
         Config.setting.DeleteTaskAndFile = chkDeleteFile.Checked;
         Config.setting.EnableWindows7Feature = chkEnableWin7.Checked;
         Config.setting.ShowBigStartButton = chkShowBigButton.Checked;
         Config.setting.SearchQuery = txtSearchText.Text;
         //插件设置
         Config.setting.Plugin_Enable_Acfun = chkPluginAcfun.Checked;
         Config.setting.Plugin_Enable_Tudou = chkPluginTudou.Checked;
         Config.setting.Plugin_Enable_Bilibili = chkPluginBilibili.Checked;
         Config.setting.Plugin_Enable_Youku = chkPluginYouku.Checked;
         Config.setting.Plugin_Enable_Imanhua = chkPluginImanhua.Checked;
         Config.setting.Plugin_Enable_TiebaAlbum = chkPluginTiebaAlbum.Checked;
         //保存设置
         Config.SaveSettings();
         this.Close();
      }

      private void btnCancel_Click(object sender, EventArgs e)
      {
         this.Close();
      }

      private void lnkSavePath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         //选择文件夹
         FolderBrowserDialog fbd = new FolderBrowserDialog();
         fbd.ShowNewFolderButton = true;
         fbd.Description = "请设置默认保存的文件夹：";
         fbd.SelectedPath = lnkSavePath.Text;
         if (fbd.ShowDialog() == DialogResult.OK)
            lnkSavePath.Text = fbd.SelectedPath;
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
   }
}
