using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using Kaedei.AcDown.Core;
using System.Reflection;
using System.Diagnostics;
using System.Text;

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
			ofd.Filter = "Flv视频文件(*.flv;*.hlv;*.f4v)|*.flv;*.hlv;*.f4v";
			ofd.Multiselect = true;

			if (lstCombine.Items.Count == 0)
				ofd.InitialDirectory = CoreManager.ConfigManager.Settings.SavePath;
			else
			{
				ofd.InitialDirectory = Path.GetDirectoryName(lstCombine.Items[lstCombine.Items.Count - 1].ToString());
			}
			//选择文件
			if (ofd.ShowDialog() != DialogResult.Cancel)
			{
				//去除重复文件
				foreach (string item in ofd.FileNames)
				{
					if (!lstCombine.Items.Contains(item))
						lstCombine.Items.Add(item);
				}
				//设置"保存到"文本框
				txtCombineOutput.Text = Path.Combine(Path.GetDirectoryName(ofd.FileNames[0]),
					Path.GetFileNameWithoutExtension(ofd.FileNames[0]) + "_合并.flv");
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
				sfd.InitialDirectory = CoreManager.ConfigManager.Settings.SavePath;

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
			//检查文件是否存在
			if (!VideoCombineHelper.CheckFileExists())
			{
				if (MessageBox.Show("尚未安装视频合并所需要的插件，" +
					Environment.NewLine +
					"是否立即下载？", "视频合并插件"
					, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
					== DialogResult.Yes)
				{
					Process.Start(@"https://acdown.codeplex.com/wikipage?title=%e8%a7%86%e9%a2%91%e5%90%88%e5%b9%b6%e6%8f%92%e4%bb%b6");
				}
				//Clipboard.SetText("视频合并插件");
				return;
			}
			panelCombine.Enabled = false;
			btnCombineStart.Text = "视频正在合并中，过一会儿再回来看看吧";
			//数组参数
			List<string> l = new List<string>();
			foreach (string item in lstCombine.Items)
			{
				l.Add(item);
			}
			//使用新线程合并视频
			Thread t = new Thread(() =>
			{
				bool result = rdoFlv.Checked ?
					CombineFlvFile(txtCombineOutput.Text, l.ToArray()) :
					new VideoCombineHelper().Combine(l.ToArray(), txtCombineOutput.Text, null);

				if (result) //合并成功
				{
					MessageBox.Show("视频合并成功ヾ(●゜ⅴ゜)ﾉ" + Environment.NewLine + txtCombineOutput.Text, "合并视频", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
				}
				else //合并失败
				{
					MessageBox.Show("啊喔，视频合并失败了ヾ(´･ω･｀)ﾉ。", "合并视频", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
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

		private void btnCombineClear_Click(object sender, EventArgs e)
		{
			lstCombine.Items.Clear();
		}


		/// <summary>
		/// 合并Flv视频
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="fileParts"></param>
		/// <returns></returns>
		public bool CombineFlvFile(string fileName, string[] fileParts)
		{
			string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			string file_flvbind = Path.Combine(appdata, "Kaedei" + Path.DirectorySeparatorChar + "FlvCombine" + Path.DirectorySeparatorChar + "FlvBind.exe");
			string file_flvlib = Path.Combine(appdata, "Kaedei" + Path.DirectorySeparatorChar + "FlvCombine" + Path.DirectorySeparatorChar + "FLVLib.dll");

			//如果文件不存在则释放文件
			if (!File.Exists(file_flvbind) || !File.Exists(file_flvlib))
			{
				var helper = new VideoCombineHelper();
				helper.ReleaseFlvCombineFile();
			}

			//生成ProcessStartInfo
			ProcessStartInfo pinfo = new ProcessStartInfo(file_flvbind);
			//设置参数
			StringBuilder sb = new StringBuilder();
			sb.Append("\"" + fileName + "\"");
			foreach (string item in fileParts)
			{
				sb.Append(" \"" + item + "\"");
			}
			pinfo.Arguments = sb.ToString();
			//隐藏窗口
			pinfo.WindowStyle = ProcessWindowStyle.Hidden;
			//启动程序
			Process p = Process.Start(pinfo);
			p.WaitForExit();
			if (p.ExitCode == 0)
				return true;
			else
				return false;
		}


	}
}
