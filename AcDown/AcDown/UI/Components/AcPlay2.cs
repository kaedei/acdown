using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Kaedei.AcDown.Component;
using System.IO;
using System.Threading;
using Kaedei.AcDown.Interface;

namespace Kaedei.AcDown.UI.Components
{
	public partial class AcPlay2 : UserControl
	{
		[DllImport("uxtheme", CharSet = CharSet.Unicode)]
		public extern static Int32 SetWindowTheme(IntPtr hWnd, String textSubAppName, String textSubIdList);

		public AcPlay2()
		{
			InitializeComponent();
		}

		private void AcPlay2_Load(object sender, EventArgs e)
		{
			//设置vista效果
			if (Config.IsWindowsVistaOrHigher())
			{
				SetWindowTheme(lsv.Handle, "explorer", null);
			}
		}

		#region 更新播放器缓存
		private void lnkPlayerCache_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			string appdata = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
			string file_acfun = Path.Combine(appdata, @"Kaedei\AcPlay\Acfun.swf");
			string file_bilibili = Path.Combine(appdata, @"Kaedei\AcPlay\Bilibili.swf");
			string swf_bilibili = "http://static.loli.my/play.swf";
			string swf_acfun = @"http://www.acfun.tv/newflvplayer/playert.swf";
			//建立文件夹
			string dir = Path.GetDirectoryName(file_acfun);
			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);

			bool r;
			string file = "";
			string swf = "";
			//更新播放器
			switch (cboPlayer.SelectedIndex)
			{
				case 0:
					file = file_acfun;
					swf = swf_acfun;
					break;
				case 1:
					file = file_bilibili;
					swf = swf_bilibili;
					break;
			}

			lnkPlayerCache.Enabled = false;
			lnkPlayerCache.Text = "正在更新播放器缓存";
			//启动新线程
			Thread t = new Thread(new ThreadStart(new MethodInvoker(() =>
			{
				r = Network.DownloadFile(new DownloadParameter()
				{
					Url = swf,
					FilePath = file
				});
				//如果下载失败则删除文件
				if (!r)
				{
					File.Delete(file);
				}
				this.Invoke(new MethodInvoker(() =>
				{
					lnkPlayerCache.Enabled = true;
					lnkPlayerCache.Text = "更新播放器缓存";
				}));
			})));
			t.IsBackground = true;
			t.Start();
		}
		#endregion

		private void lsv_DoubleClick(object sender, EventArgs e)
		{
			if (lsv.SelectedIndices.Count > 0)
			{
				int index = lsv.SelectedIndices[0];
				ListViewItem lvi = lsv.Items[index];
				Video v = new Video();
				var ms = lsv.Items[index].SubItems[1].Text.Split(':');
				v.Length = int.Parse(ms[0]) * 60 + int.Parse(ms[1]);
				v.Path = lsv.Items[index].SubItems[2].Text;
				AcPlayItem form = new AcPlayItem(v);
				lsv.Items[index].SubItems[1].Text = (v.Length / 60).ToString() + ":" + (v.Length % 60).ToString();
				lsv.Items[index].SubItems[2].Text = v.Path;
				form.ShowDialog();
			}
		}

	}
}
