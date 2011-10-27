using System;
using System.Windows.Forms;
using Kaedei.AcDown.Component;
using System.IO;
using System.Threading;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;

namespace Kaedei.AcDown.UI.Components
{
   public partial class AcPlayControl : UserControl
   {
      public AcPlayControl()
      {
         InitializeComponent();
      }

      //添加视频
      private void btnItemAdd_Click(object sender, EventArgs e)
      {
         OpenFileDialog ofd = new OpenFileDialog();
         ofd.Filter = "所有视频文件(*.mp4;*.flv;*.hlv;*.f4v)|*.mp4;*.flv;*.hlv;*.f4v|mp4视频(*.mp4)|*.mp4|flv视频(*.flv)|*.flv|高清flv视频(*.hlv)|*.hlv|f4v视频(*.f4v)|*.f4v";
         ofd.Title = "请选择视频文件";
         ofd.Multiselect = true;
         ofd.InitialDirectory = Config.setting.SavePath;
         if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
         {
            string[] files = ofd.FileNames;
            foreach (string item in files)
            {
               if (!lstVideo.Items.Contains(item))
                  lstVideo.Items.Add(item);
            }
         }
         CalculateTimeLength();
      }

      //删除项
      private void btnItemDelete_Click(object sender, EventArgs e)
      {
         while (lstVideo.SelectedIndices.Count != 0)
         {
            lstVideo.Items.RemoveAt(lstVideo.SelectedIndex);
         }
         CalculateTimeLength();
      }

      //启用/禁用 上移下移按钮
      private void lstVideo_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (lstVideo.SelectedIndices.Count == 1)
         {
            btnItemUp.Enabled = true;
            btnItemDown.Enabled = true;
         }
         else
         {
            btnItemUp.Enabled = false;
            btnItemDown.Enabled = false;
         }
      }

      //上移
      private void btnItemUp_Click(object sender, EventArgs e)
      {
         //选中项
         int selected = lstVideo.SelectedIndex;
         //如果是第一项则不做任何事
         if (selected == 0)
            return;
         //交换两项
         string tmp = lstVideo.Items[selected].ToString();
         lstVideo.Items[selected] = lstVideo.Items[selected - 1];
         lstVideo.Items[selected - 1] = tmp;
         //重新选中项
         lstVideo.ClearSelected();
         lstVideo.SelectedIndex = selected - 1;
      }

      //下移
      private void btnItemDown_Click(object sender, EventArgs e)
      {
         //选中项
         int selected = lstVideo.SelectedIndex;
         //如果是最后一项项则不做任何事
         if (selected == lstVideo.Items.Count - 1)
            return;
         //交换两项
         string tmp = lstVideo.Items[selected].ToString();
         lstVideo.Items[selected] = lstVideo.Items[selected + 1];
         lstVideo.Items[selected + 1] = tmp;
         //重新选中项
         lstVideo.ClearSelected();
         lstVideo.SelectedIndex = selected + 1;
      }

      //选择xml
      private void btnXml_Click(object sender, EventArgs e)
      {
         OpenFileDialog ofd = new OpenFileDialog();
         ofd.Filter = "XML弹幕(*.xml)|*.xml";
         ofd.Title = "请选择弹幕XML文件";
         ofd.Multiselect = false;
         if (lstVideo.Items.Count > 0)
         {
            string dir = Path.GetDirectoryName(lstVideo.Items[lstVideo.Items.Count - 1].ToString());
            ofd.InitialDirectory = dir;
         }
         else
         {
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
         }

         if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
         {
            txtXml.Text = ofd.FileName;
         }
      }

      //选择xml2
      private void btnXml2_Click(object sender, EventArgs e)
      {
         OpenFileDialog ofd = new OpenFileDialog();
         ofd.Filter = "XML弹幕(*.xml)|*.xml";
         ofd.Title = "请选择副弹幕文件";
         ofd.Multiselect = false;
         if (lstVideo.Items.Count > 0)
         {
            string dir = Path.GetDirectoryName(lstVideo.Items[lstVideo.Items.Count - 1].ToString());
            ofd.InitialDirectory = dir;
         }
         else
         {
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
         }
         if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
         {
            txtXml2.Text = ofd.FileName;
         }
      }

      private void cboPlayer_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (cboPlayer.SelectedIndex != 1) //不是acfun
         {
            lblXml2.Visible = false;
            txtXml2.Visible = false;
            btnXml2.Visible = false;
         }
         else
         {
            lblXml2.Visible = true;
            txtXml2.Visible = true;
            btnXml2.Visible = true;
         }
         btnLaunchAcPlay.Enabled = true;
      }


      //估算所选视频的长度
      private void CalculateTimeLength()
      {
         try
         {
            long filesize = 0;
            foreach (string item in lstVideo.Items)
            {
               FileInfo info = new FileInfo(item);
               filesize += info.Length;
            }
            //每秒钟100KB
            long secs = filesize / 100000;
            udMin.Value = (int)(secs / 60);
            udSec.Value = (int)(secs % 60);
         }
         catch { }
      }

      private void AcPlay_Load(object sender, EventArgs e)
      {
         if (AcPlay.CheckExisted())
         {
            panelPlayer.Visible = true;
            btnStart.Hide();
         }
      }

      private void btnLaunchAcPlay_Click(object sender, EventArgs e)
      {
         string tip = CheckInput();
         //提示输入错误
         if (tip != "")
         {
            MessageBox.Show(tip, "AcPlay弹幕播放", MessageBoxButtons.OK, MessageBoxIcon.Information);
         }
         else
         {
            string player = "bilibili";
            //播放器
            switch (cboPlayer.SelectedIndex)
            {
               case 0:
                  player = "bilibili";
                  break;
               case 1:
                  player = "acfun";
                  break;
            }
            List<string> v = new List<string>();
            foreach (string item in lstVideo.Items)
            {
               v.Add(item);
            }
            int length = (int)udMin.Value * 60000 + (int)udSec.Value * 1000;
            panelPlayer.Enabled = false;
            //启动新线程启动AcPlay.exe
            Thread t = new Thread(new ThreadStart(new MethodInvoker(() =>
               {
                  AcPlay.PlayVideo(player, v.ToArray(), txtXml.Text, txtXml2.Text, length);
                  //播放完成后重新启用Panel
                  this.Invoke(new MethodInvoker(() => { panelPlayer.Enabled = true; }));
               })));
            t.IsBackground = true;
            t.Start();
         }
      }

      //启动acplay前检查用户输入的值
      private string CheckInput()
      {
         if (lstVideo.Items.Count == 0)
            return "请选择一个或多个视频";
         if (udMin.Value == 0 && udSec.Value == 0)
            return "请填写视频长度";
         return "";
      }

      private void btnStart_Click(object sender, EventArgs e)
      {
        
         btnStart.Enabled = false;
         //下载程序组件
         Thread t = new Thread(() =>
         {
            bool s = AcPlay.DownloadComponents();
            //下载成功
            if (s)
            {
               this.Invoke(new MethodInvoker(() =>
               {
                  panelPlayer.Show();
                  btnStart.Hide();
               }));
            }
            else //下载失败
            {
               this.Invoke(new MethodInvoker(() =>
               {
                  DialogResult r =  MessageBox.Show("下载AcPlay弹幕播放插件失败\r\n是否下载离线安装包？", "AcPlay弹幕播放器", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                  if (r == DialogResult.Yes)
                  {
                     Process.Start(@"http://blog.sina.com.cn/s/blog_58c506600100vf4x.html");
                  }

                  btnStart.Enabled = true;
               }));
            }
         });
         t.Start();
      }

      private void lnkQA_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         Process.Start(@"http://blog.sina.com.cn/s/blog_58c506600100vf4x.html");
      }




   }
}
