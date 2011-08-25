using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Kaedei.AcDown.UI.Components
{
   public partial class FlvCombineControl : UserControl
   {
      public FlvCombineControl()
      {
         InitializeComponent();
      }

      private void btnCombineAdd_Click(object sender, EventArgs e)
      {

      }

      //添加文件
      private void btnCombineAdd_Click_1(object sender, EventArgs e)
      {
         //显示Open对话框
         OpenFileDialog ofd = new OpenFileDialog();
         ofd.DefaultExt = ".flv";
         ofd.Filter = "Flv视频文件(*.flv;*.hlv)|*.flv;*.hlv";
         ofd.Multiselect = true;

         if (lstCombine.Items.Count == 0)
            ofd.InitialDirectory = Config.setting.SavePath;
         else
         {
            ofd.InitialDirectory = Path.GetDirectoryName(lstCombine.Items[lstCombine.Items.Count - 1].ToString());
         }
         //选择文件
         if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
         {
            //去除重复文件
            foreach (string item in ofd.FileNames)
            {
               if (!lstCombine.Items.Contains(item))
                  lstCombine.Items.Add(item);
            }
            //设置"保存到"文本框
            txtCombineOutput.Text = Path.Combine(Path.GetDirectoryName(ofd.FileNames[0]), "合并.flv");
         }
         //如果视频多余一个才可以合并
         btnCombineStart.Enabled = (lstCombine.Items.Count > 1);

      }
      //删除选中的项
      private void btnCombineDelete_Click(object sender, EventArgs e)
      {
         while (lstCombine.SelectedIndices.Count != 0)
         {
            lstCombine.Items.RemoveAt(lstCombine.SelectedIndex);
         }
         btnCombineStart.Enabled = (lstCombine.Items.Count > 1);
      }


      //选择输出文件
      private void btnCombineChooseOutput_Click(object sender, EventArgs e)
      {
         //选择保存文件
         SaveFileDialog sfd = new SaveFileDialog();
         sfd.DefaultExt = ".flv";
         sfd.AddExtension = true;
         sfd.Title = "保存合并文件";
         //设置保存文件（对话框的默认路径）
         if (lstCombine.Items.Count > 0) //列表中最后一个视频所在的文件夹
            sfd.InitialDirectory = Path.GetDirectoryName(lstCombine.Items[lstCombine.Items.Count - 1].ToString());
         else if (!string.IsNullOrEmpty(txtCombineOutput.Text)) //以前设置的输出文件所在的文件夹
            sfd.InitialDirectory = Path.GetDirectoryName(txtCombineOutput.Text);
         else //默认保存的文件夹
            sfd.InitialDirectory = Config.setting.SavePath;

         sfd.Filter = "Flv视频文件(*.flv)|*.flv";
         DialogResult result = sfd.ShowDialog();
         if (result != System.Windows.Forms.DialogResult.Cancel)
         {
            txtCombineOutput.Text = sfd.FileName;
         }
      }


      //合并视频
      private void btnCombineStart_Click(object sender, EventArgs e)
      {
         panelCombine.Enabled = false;
         btnCombineStart.Text = "正在合并";
         //数组参数
         List<string> l = new List<string>();
         foreach (string item in lstCombine.Items)
         {
            l.Add(item);
         }
         //使用新线程合并视频
         Thread t = new Thread(() =>
         {
            bool result = FlvCombine.FlvCombine.CombineFlvFile(txtCombineOutput.Text, l.ToArray());
            if (result) //合并成功
            {
               MessageBox.Show("视频合并成功:\n" + txtCombineOutput.Text, "合并FLV视频", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
            else //合并失败
            {
               MessageBox.Show("视频合并失败。\n视频合并器暂时不能正确读取您所选择的文件。\n\n此问题可能是由于视频并未使用Flv编码所致", "合并FLV视频", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
            //恢复控件的状态
            this.Invoke(new MethodInvoker(() =>
            {
               panelCombine.Enabled = true;
               btnCombineStart.Text = "开始合并";

            }));
         });
         //启动线程
         t.Start();
      }


      //下载FLV合并器
      private void btnGetFlvCombine_Click(object sender, EventArgs e)
      {
         btnGetFlvCombine.Text = "正在下载FLV合并插件...";
         btnGetFlvCombine.Enabled = false;
         //下载程序组件
         Thread t = new Thread(() =>
         {
            bool s = FlvCombine.FlvCombine.DownloadComponents();
            //下载成功
            if (s)
            {
               this.Invoke(new MethodInvoker(() =>
               {
                  btnGetFlvCombine.Hide();
                  panelCombine.Show();
               }));
            }
            else //下载失败
            {
               this.Invoke(new MethodInvoker(() =>
               {
                  MessageBox.Show("下载FLV合并插件失败，请检查网络设置", "FLV视频合并", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  btnGetFlvCombine.Text = "下载FLV合并插件";
                  btnGetFlvCombine.Enabled = true;
               }));
            }
         });
         t.Start();
      }

      private void FlvCombineControl_Load(object sender, EventArgs e)
      {
         if (FlvCombine.FlvCombine.CheckExisted())
         {
            btnGetFlvCombine.Hide();
            panelCombine.Show();
         }
      }


   }
}
