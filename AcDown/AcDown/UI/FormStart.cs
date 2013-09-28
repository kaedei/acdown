using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using Kaedei.AcDown.Core;
using System.Diagnostics;
using System.IO;
using Kaedei.AcDown.Interface;
using Kaedei.AcDown.Downloader;
using System.Collections.ObjectModel;
using Kaedei.AcDown.Interface.UI;

namespace Kaedei.AcDown.UI
{
	public partial class FormStart : FormBase
	{
		DwmApi.MARGINS marg;

		public FormStart()
		{
			InitializeComponent();
		}


		private void FormStart_Load(object sender, EventArgs e)
		{
			//设置AERO效果
			marg = new DwmApi.MARGINS(this.Width, this.Height, this.Width, this.Height);
			if (DwmApi.IsWindowsVistaOrHigher()) //如果是vista以上
			{
				if (DwmApi.DwmIsCompositionEnabled()) //如果dwm被启用了（有AERO效果）
				{
					this.BackColor = Color.Black;
					this.picIcon.BackColor = Color.Black;
					DwmApi.DwmExtendFrameIntoClientArea(this.Handle, marg);
				}
			}

			//启动新窗体进行加载
			Thread t = new Thread(LoadData);
			t.Start();

		}

		//窗体重绘事件
		private void FormStart_Paint(object sender, PaintEventArgs e)
		{
			if (DwmApi.IsWindowsVistaOrHigher())
			{
				//定义重绘的矩形范围
				Rectangle rect = new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height);
				//如果操作系统dwm启用
				if (DwmApi.DwmIsCompositionEnabled())
				{
					//使用黑色画刷进行重绘
					using (SolidBrush blackbrush = new SolidBrush(Color.Black))
					{
						e.Graphics.FillRectangle(blackbrush, rect);//重绘玻璃部分
					}
				}
			}
		}

		//初始化数据
		private void LoadData()
		{
			//官方插件
			var plugins = new Collection<IPlugin>() 
				{ 
					new AcFunPlugin(), 
					new YoukuPlugin(),
					//new YouTubePlugin(),
					new NicoPlugin(),
					new BilibiliPlugin(),
					new TudouPlugin(),
					new ImanhuaPlugin(),
					//new TiebaAlbumPlugin(),
					new SfAcgPlugin(),
					new TucaoPlugin(),
					new XiamiPlugin(),
					new GoodmangaPlugin(),
					new FlvcdPlugin()
					//new AcDown.Downloader.AcDown.AcDownPlugin()
				};

			//初始化核心
			CoreManager.Initialize(
				 Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				 "Kaedei" + Path.DirectorySeparatorChar + "AcDown" + Path.DirectorySeparatorChar),
				 new UIDelegateContainer(null, null, null, null, null, null, null, null),
				 plugins);

			//检查是否是首次运行
			var firstrun = new FirstrunHandler();
			if (firstrun.IsFirstRun())
			{
				Process p = new Process();
				p.StartInfo = new ProcessStartInfo()
				{
					FileName = Application.ExecutablePath,
					Arguments = "firstrun",
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

			if (this.Disposing || this.IsDisposed) return;

			this.Invoke(new MethodInvoker(() =>
			{
				//加载主窗体
				Program.frmMain = new FormMain();
				Program.frmMain.Show();
				this.Hide();
			}));
		}
	}
}

