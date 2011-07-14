using System;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Kaedei.AcDown;
using Kaedei.AcDown.Properties;
using System.Runtime.InteropServices;
using Kaedei.AcDown.Interface;
using Kaedei.AcDown.Component;
using Kaedei.AcDown.UI;


namespace AcDown.UI
{

	public partial class FormMain : Form
	{

		#region ——————外部调用——————
		private static ITaskbarList3 taskbarList;
		private THUMBBUTTON newbtn;

		[DllImport("user32.dll", EntryPoint = "RegisterWindowMessage", SetLastError = true, CharSet = CharSet.Unicode)]
		internal static extern uint RegisterWindowMessage([MarshalAs(UnmanagedType.LPWStr)] string lpString);
		//ITaskbarList接口
		[ComImportAttribute()]
		[GuidAttribute("ea1afb91-9e28-4b86-90e9-9e9f8a5eefaf")]
		[InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface ITaskbarList3
		{
			// ITaskbarList
			[PreserveSig]
			void HrInit();
			[PreserveSig]
			void AddTab(IntPtr hwnd);
			[PreserveSig]
			void DeleteTab(IntPtr hwnd);
			[PreserveSig]
			void ActivateTab(IntPtr hwnd);
			[PreserveSig]
			void SetActiveAlt(IntPtr hwnd);

			// ITaskbarList2
			[PreserveSig]
			void MarkFullscreenWindow(
				 IntPtr hwnd,
				 [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);

			// ITaskbarList3
			void SetProgressValue(IntPtr hwnd, UInt64 ullCompleted, UInt64 ullTotal);
			void SetProgressState(IntPtr hwnd, TBPFLAG tbpFlags);
			void RegisterTab(IntPtr hwndTab, IntPtr hwndMDI);
			void UnregisterTab(IntPtr hwndTab);
			void SetTabOrder(IntPtr hwndTab, IntPtr hwndInsertBefore);
			void SetTabActive(IntPtr hwndTab, IntPtr hwndMDI, TBATFLAG tbatFlags);
			void ThumbBarAddButtons(
				 IntPtr hwnd,
				 uint cButtons,
				 [MarshalAs(UnmanagedType.LPArray)] THUMBBUTTON[] pButtons);
			void ThumbBarUpdateButtons(
				 IntPtr hwnd,
				 uint cButtons,
				 [MarshalAs(UnmanagedType.LPArray)] THUMBBUTTON[] pButtons);
			void ThumbBarSetImageList(IntPtr hwnd, IntPtr himl);
			void SetOverlayIcon(
			  IntPtr hwnd,
			  IntPtr hIcon,
			  [MarshalAs(UnmanagedType.LPWStr)] string pszDescription);
			void SetThumbnailTooltip(
				 IntPtr hwnd,
				 [MarshalAs(UnmanagedType.LPWStr)] string pszTip);
			void SetThumbnailClip(
				 IntPtr hwnd,
				/*[MarshalAs(UnmanagedType.LPStruct)]*/ ref RECT prcClip);
		}

		[GuidAttribute("56FDF344-FD6D-11d0-958A-006097C9A090")]
		[ClassInterfaceAttribute(ClassInterfaceType.None)]
		[ComImportAttribute()]
		internal class CTaskbarList { }

		[StructLayout(LayoutKind.Sequential)]
		internal struct RECT
		{
			public int left;
			public int top;
			public int right;
			public int bottom;

			public RECT(int left, int top, int right, int bottom)
			{
				this.left = left;
				this.top = top;
				this.right = right;
				this.bottom = bottom;
			}
		}

		internal enum TBPFLAG
		{
			TBPF_NOPROGRESS = 0,
			TBPF_INDETERMINATE = 0x1,
			TBPF_NORMAL = 0x2,
			TBPF_ERROR = 0x4,
			TBPF_PAUSED = 0x8
		}

		internal enum TBATFLAG
		{
			TBATF_USEMDITHUMBNAIL = 0x1,
			TBATF_USEMDILIVEPREVIEW = 0x2
		}

		internal enum THBMASK
		{
			THB_BITMAP = 0x1,
			THB_ICON = 0x2,
			THB_TOOLTIP = 0x4,
			THB_FLAGS = 0x8
		}

		internal enum THBFLAGS
		{
			THBF_ENABLED = 0,
			THBF_DISABLED = 0x1,
			THBF_DISMISSONCLICK = 0x2,
			THBF_NOBACKGROUND = 0x4,
			THBF_HIDDEN = 0x8
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal struct THUMBBUTTON
		{
			[MarshalAs(UnmanagedType.U4)]
			public THBMASK dwMask;
			public uint iId;
			public uint iBitmap;
			public IntPtr hIcon;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string szTip;
			[MarshalAs(UnmanagedType.U4)]
			public THBFLAGS dwFlags;
		}

		[DllImport("user32.dll", EntryPoint = "SendMessageA")]
		public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, Byte[] lParam);
		public const int LVM_FIRST = 0x1000; //Value from http://www.winehq.org/pipermail/wine-devel/2002-October/009527.html
		public const int LVM_SETEXTENDEDLISTVIEWSTYLE = LVM_FIRST + 54;
		public const int LVS_EX_FULLROWSELECT = 0x00000020;
		public const int LVS_EX_DOUBLEBUFFER = 0x00010000;
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
		//Imports the UXTheme DLL
		[DllImport("uxtheme", CharSet = CharSet.Unicode)]
		public extern static Int32 SetWindowTheme(IntPtr hWnd, String textSubAppName, String textSubIdList);

		#endregion

		#region ——————管理器——————
		
		//任务管理器
		private TaskManager taskMgr;
		//插件管理器
		private PluginManager pluginMgr;
		//包装委托的类
		private DelegateContainer deles;

		//是否退出程序
		private bool exitapp = false;
		#endregion

		#region ——————初始化——————

		private void Initialize()
		{
			//全局设置
			Config.LoadSettings();
			//记录
			Logging.Initialize();
			//插件管理器
			pluginMgr = new PluginManager();
			pluginMgr.LoadPlugins();
			//委托
			deles = new DelegateContainer(
				new AcTaskDelegate(Start),
				new AcTaskDelegate(NewPart),
				new AcTaskDelegate(RefreshTask),
				new AcTaskDelegate(TipText),
				new AcTaskDelegate(Finish),
				new AcTaskDelegate(Error));
			//任务管理器
			taskMgr = new TaskManager(deles);
			//新建窗体
			FormNew.Initialize(pluginMgr, taskMgr);
		}

		#endregion //初始化

		#region ——————UI相关——————


		public FormMain()
		{
			//初始化数据
			Initialize();
			//初始化窗体
			InitializeComponent();
			//设置窗体标题和文字
			this.Icon = Resources.Ac;
			this.Text = Application.ProductName;
			//取消显示大按钮
			if (Config.setting.ShowBigStartButton == false)
				if (btnClickNew != null)
					btnClickNew.Dispose();
			//显示托盘图标
			notifyIcon.Icon = Resources.Ac;
			//设置刷新频率
			timer.Interval = Config.setting.RefreshInfoInterval;
			//设置是否监视剪贴板
			watchClipboard = Config.setting.WatchClipboardEnabled;

		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			if (Config.IsWindowsVistaOr7())
			{
				if (Config.IsWindows7())
				{
					//初始化Win7任务栏管理器
					taskbarList = (ITaskbarList3)new CTaskbarList();
					taskbarList.HrInit();
				}
				//设置提示文字
				SendMessage(txtSearch.TextBox.Handle, 0x1501, IntPtr.Zero, System.Text.Encoding.Unicode.GetBytes(@"快捷搜索"));
				//设置listview效果
				SetWindowTheme(this.lsv.Handle, "explorer", null); //Explorer style 
				SendMessage(this.lsv.Handle, LVM_SETEXTENDEDLISTVIEWSTYLE, 0, LVS_EX_FULLROWSELECT + LVS_EX_DOUBLEBUFFER);  //Blue selection
			}
			//选中下拉列表框
			cboAfterComplete.SelectedIndex = 0;			
		}

		private void btnAbout_Click(object sender, EventArgs e)
		{
			FormAbout about = new FormAbout();
			about.ShowDialog();
			about.Dispose();
		}

		private void btnConfig_Click(object sender, EventArgs e)
		{
			FormConfig config = new FormConfig();
			config.ShowDialog();
			config.Dispose();
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			//去除显示大按钮
			if (btnClickNew != null)
			{
				btnClickNew.Dispose();
			}
			//禁用Win7缩略图按钮
			if (Config.IsWindows7() && Config.setting.EnableWindows7Feature)
			{
				newbtn.dwFlags = THBFLAGS.THBF_DISABLED;
				taskbarList.ThumbBarUpdateButtons(this.Handle, 1, new THUMBBUTTON[1] { newbtn });
			}
			watchClipboard = false;
			//显示“新建”
			FormNew.ShowForm("");
			watchClipboard = true;
			//启用Win7缩略图按钮
			if (Config.IsWindows7() && Config.setting.EnableWindows7Feature)
			{
				newbtn.dwFlags = THBFLAGS.THBF_ENABLED;
				taskbarList.ThumbBarUpdateButtons(this.Handle, 1, new THUMBBUTTON[1] { newbtn });
			}
		}

		private void txtSearch_Click(object sender, EventArgs e)
		{
			if (txtSearch.Text == "快捷搜索")
				txtSearch.Text = "";
			else
				txtSearch.SelectAll();
		}

		//新建任务
		private void btnClickNew_Click(object sender, EventArgs e)
		{
			//显示动画提示效果
			Int32 width = btnClickNew.Size.Width;
			Int32 height = btnClickNew.Size.Height;
			Int32 x = btnClickNew.Location.X;
			Int32 y = btnClickNew.Location.Y;

			for (int i = 99; i >= 0; i--)
			{
				btnClickNew.Size = new Size(width * i / 100, height * i / 100);
				btnClickNew.Location = new Point(x * i / 100, y * i / 100);
				Application.DoEvents();
			}
			btnClickNew.Dispose();
			watchClipboard = false;
			//显示新建窗体
			FormNew.ShowForm("");
			watchClipboard = true;
		}

		//上一次取得的URL
		//private string lastUrl;
		//是否监视剪贴板
		private bool watchClipboard;

		//显示进度以及速度 & 监视剪贴板
		[DebuggerNonUserCode()]
		private void timer_Tick(object sender, EventArgs e)
		{
			//设置刷新频率
			if (Config.setting.RefreshInfoInterval != timer.Interval)
			{
				timer.Interval = Config.setting.RefreshInfoInterval;
			}
			//设置限速
			int sl = Convert.ToInt32(udSpeedLimit.Value);
			if (sl != 0)
			{
				int r = taskMgr.GetRunningCount();
				if (r != 0 )
				{
					GlobalSettings.GetSettings().SpeedLimit = sl / r;
				}
			}
			else
			{
				GlobalSettings.GetSettings().SpeedLimit = 0;
			}
			//全局速度
			double speed = 0;
			//取得所有正在进行中的任务
			foreach (IDownloader downloader in taskMgr.Tasks)
			{
				//如果是正在下载的任务
				if (downloader.Status == DownloadStatus.正在下载)
				{
					//显示进度
					ListViewItem item = GetLsvItem(downloader.TaskId);
					if (downloader.TotalLength != 0)
					{
						item.SubItems[3].Text = string.Format(@"{0:P}", (double)downloader.DoneBytes / (double)downloader.TotalLength);
					}
					else
					{
						item.SubItems[3].Text = "0.0%";
					}
					//显示速度
					double currentSpeed = 0;
					currentSpeed = (double)(downloader.DoneBytes - downloader.LastTick) / (timer.Interval *1024 / 1000);
					speed += currentSpeed;
					item.SubItems[4].Text = string.Format("{0:F1}",currentSpeed) + "KB/s";
				}
				//如果正在等待开始
				if (downloader.Status == DownloadStatus.等待开始)
				{
					ListViewItem item = GetLsvItem(downloader.TaskId);
					if (item != null)
					{
						item.SubItems[0].Text = "等待开始";
					}
					
				}
			}
			//显示全局速度
			if (speed != 0.0)
			{
				lblSpeed.Text = string.Format("{0:F1}", speed) + "KB/s";
			}
			else
			{
				lblSpeed.Text = "";
			}
			//显示Win7任务栏特性
			if (Config.IsWindows7())
			{
				if (Config.setting.EnableWindows7Feature)
				{
					IDownloader a = taskMgr.GetFirstRunning();
					if (a != null) //如果有任务正在运行
					{
						taskbarList.SetProgressState(this.Handle, TBPFLAG.TBPF_NORMAL);
						//显示此任务的进度
						taskbarList.SetProgressValue(this.Handle, (ulong)(((double)a.DoneBytes / (double)a.TotalLength) * 10000), 10000);
					}
					else
					{
						taskbarList.SetProgressState(this.Handle, TBPFLAG.TBPF_NOPROGRESS);
					}
					//设置win7任务栏小图标
					//taskbarList.SetOverlayIcon(this.Handle,this.Icon.Handle, "w");
					//设置缩略图
					if (this.WindowState != FormWindowState.Minimized)
					{
						RECT rect = new RECT(picLogo.Left, picLogo.Top, picLogo.Right, picLogo.Bottom);
						//RECT rect = new RECT(lsv.Left, lsv.Top, lsv.Right, lsv.Bottom);
						taskbarList.SetThumbnailClip(this.Handle, ref rect);
					}
				}
				else  //如果禁止Win7特性
				{
					taskbarList.SetProgressState(this.Handle, TBPFLAG.TBPF_NOPROGRESS);
				}
			}
		}

		//程序正在退出
		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = !exitapp;
			if (Config.setting.HideWhenClose)
			{		
				this.Hide();
			}
			else
			{
				mnuTrayExit_Click(sender, EventArgs.Empty);
			}
			
		}

		private bool alreayTipMinimize = false;
		//程序隐藏后提示
		private void FormMain_VisibleChanged(object sender, EventArgs e)
		{
			if (!alreayTipMinimize)
			{
				if (!this.Visible)
				{
					notifyIcon.ShowBalloonTip(1500, "AcDown动漫下载器已经最小化到系统托盘", "您可以双击此图标以重新显示下载器", ToolTipIcon.Info);
					alreayTipMinimize = true;
				}
			}
		}

		//单击弹出菜单
		private void lsv_MouseClick(object sender, MouseEventArgs e)
		{
			//如果按下右键
			if (e.Button == MouseButtons.Right)
			{
				////如果选择的项目大于0个
				//if (lsv.SelectedItems.Count > 0)
				//{
				//   mnuContext.Show(lsv, e.Location);
				//}
			}
		}

		//显示工具栏
		private void lsv_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lsv.SelectedItems.Count > 0)
			{
				//取得最后一项的索引值
				int i = lsv.SelectedIndices[lsv.SelectedIndices.Count - 1];
				//计算最后一项+1项的高度
				contextTool.Top = lsv.Top + lsv.Font.Height * (i + 3 + 1 / 2);
				//如果高度超过listview的范围
				if (contextTool.Top > lsv.Top + lsv.Height)
				{
					contextTool.Top = lsv.Top + lsv.Height - contextTool.Height * 2;
				}
				contextTool.Visible = true;
			}
			else
				contextTool.Visible = false;
		}

		//菜单中的开始
		private void mnuConStart_Click(object sender, EventArgs e)
		{
			//开始所有可能开始的任务
			foreach (ListViewItem item in lsv.SelectedItems)
			{
				IDownloader downloader = GetTask(new Guid((string)item.Tag));
				if (downloader.Status == DownloadStatus.出现错误 || downloader.Status == DownloadStatus.已经停止)
				{
					taskMgr.StartTask(downloader);
					Start(new ParaStart(downloader.TaskId));
				}
			}
		}

		//菜单中的停止
		private void mnuConStop_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("是否要停止选定的下载任务?","停止下载", 
				MessageBoxButtons.YesNo, MessageBoxIcon.Question , 
				MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
			{
				return;
			}
			//停止所有可能停止的任务
			foreach (ListViewItem item in lsv.SelectedItems)
			{
				IDownloader downloader = GetTask(new Guid((string)item.Tag));
				if (downloader.Status == DownloadStatus.正在下载 || downloader.Status == DownloadStatus.等待开始)
				{
					taskMgr.StopTask(downloader);
				}
			}
		}

		//打开视频所在文件夹
		private void mnuConOpenFolder_Click(object sender, EventArgs e)
		{
			ListViewItem item = lsv.SelectedItems[0];
			if (item != null)
			{
				IDownloader downloader = GetTask(new Guid((string)item.Tag));
				if (!string.IsNullOrEmpty(downloader.SaveDirectory.ToString()))
					Process.Start(downloader.SaveDirectory.ToString());
				else
					Process.Start(Config.setting.SavePath);
			}
		}

		//打开视频页面
		private void mnuConOpenUrl_Click(object sender, EventArgs e)
		{
			ListViewItem item = lsv.SelectedItems[0];
			if (item != null)
			{
				IDownloader downloader = GetTask(new Guid((string)item.Tag));
				Process.Start(downloader.Url);
			}
		}

		//查看视频信息
		private void mnuConInfo_Click(object sender, EventArgs e)
		{
			ListViewItem item = lsv.SelectedItems[0];
			if (item != null)
			{
				if (!string.IsNullOrEmpty(item.Tag.ToString()))
				{
					FormInfo fi = new FormInfo(GetTask(new Guid(item.Tag.ToString())));
					fi.ShowDialog();
				}
			}
		}
		
		private void acFuncnToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("http://www.acfun.tv/");
		}

		private void bilibiliToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("http://www.bilibili.tv/");
		}

		private void 土豆网ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("http://www.tudou.com");
		}

		private void 优酷网ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("http://www.youku.com/");
		}

		private void 贴吧相册ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("http://tieba.baidu.com/");
		}

		private void 爱漫画ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("http://www.imanhua.com/");
		}

		private void 更多ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("http://acdown.codeplex.com/wikipage?title=%E5%85%B3%E4%BA%8EFLVCD%E6%8F%92%E4%BB%B6");
		}


		//自定义搜索引擎
		private void searchCustom_Click(object sender, EventArgs e)
		{
			btnConfig_Click(this, EventArgs.Empty);
		}

		//搜索
		private void btnSearch_ButtonClick(object sender, EventArgs e)
		{
			if (txtSearch.Text == "快捷搜索")
				return;
			if (txtSearch.Text.Length != 0)
			{
				string q;
				switch (Config.setting.SearchQuery)
				{
					case "Acfun站内搜索 - Google":
						q = @"http://www.google.com.hk/cse?cx=partner-pub-9798548458573283%3Aq7uq7u33ezy&ie=GB2312&q=%TEST%&sa=%CB%D1%CB%F7&siteurl=www.acfun.tv%2F".Replace("%TEST%", txtSearch.Text);
						break;
					case "土豆网":
						q = @"http://so.tudou.com/nisearch/%TEST%/".Replace("%TEST%", txtSearch.Text);
						break;
					case "Acfun站内搜索 - 百度":
						q = @"http://www.baidu.com/s?wd=%TEST%+site%3A%28acfun.tv%29".Replace("%TEST%", txtSearch.Text);
						break;
					case "优酷搜索(搜酷)":
						q = @"http://www.soku.com/search_video/q_%TEST%".Replace("%TEST%", txtSearch.Text);
						break;
					case "漫画搜索(爱漫画)":
						q = @"http://www.imanhua.com/v2/user/search.aspx?key=%TEST%".Replace("%TEST%", txtSearch.Text);
						break;
					default:
						q = Config.setting.SearchQuery.Replace(@"%TEST%", txtSearch.Text);
						break;
				}
				Process.Start(q);
			}
		}

		//按下回车搜索
		private void txtSearch_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
				btnSearch_ButtonClick(this, EventArgs.Empty);
		}

		//双击某一项目
		private void lsv_DoubleClick(object sender, EventArgs e)
		{
		//   ListViewItem item = lsv.SelectedItems[0];
		//   if (item != null)
		//   {
		//      IDownloader downloader = GetTask(new Guid((string)item.Tag));
		//      //根据状态执行不同的操作
		//      switch (downloader.Status)
		//      {
		//            //开始状态则停止
		//         case DownloadStatus.正在下载:
		//         case DownloadStatus.等待开始:
		//            taskMgr.StopTask(downloader);
		//            break;
		//            //停止状态则开始
		//         case DownloadStatus.已经停止:
		//         case DownloadStatus.出现错误:
		//            taskMgr.StartTask(downloader);
		//            break;
		//         case DownloadStatus.下载完成:
		//            try
		//            {
		//               Process.Start(downloader.SaveDirectory.ToString());
		//            }
		//            catch
		//            {
		//               MessageBox.Show("文件已删除或不存在", "打开文件", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		//            }
		//            break;
		//      }
		//   }
		}

		//点击"下载中心"链接
		private void toolHelpCenter_Click(object sender, EventArgs e)
		{
			FormHelp frmHelp = new FormHelp();
			frmHelp.ShowDialog();
		}

		//双击托盘图标
		private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			mnuTrayShowHide_Click(sender, EventArgs.Empty);
		}

		//将程序显示到前台
		public void ShowFormToFront()
		{
			if (this.Visible == false)
			{
				mnuTrayShowHide_Click(this, EventArgs.Empty);
			}
			else
			{
				this.TopMost = true;
				this.TopMost = false;
			}
		}

		//显示/隐藏
		private void mnuTrayShowHide_Click(object sender, EventArgs e)
		{
			this.Visible = !this.Visible;
			this.TopMost = true;
			this.TopMost = false;
		}

		//退出程序
		private void mnuTrayExit_Click(object sender, EventArgs e)
		{
			//正在进行的任务数量
			Int32 c = taskMgr.GetRunningCount();
			if (c > 0)
			{
				DialogResult r = MessageBox.Show("有" + c.ToString() + "个任务正在运行，是否退出？", "AcDown动漫下载器", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if (r == DialogResult.Yes) //确认关闭
				{
					this.Cursor = Cursors.WaitCursor;
					//结束所有任务
					taskMgr.StopAllTasks();
					this.Cursor = Cursors.Default;
					//释放托盘图标
					notifyIcon.Dispose();
				}
				else //取消关闭
				{
					return;
				}
			}
			//关闭日志文件
			Logging.Exit();
			//退出程序
			exitapp = true;
			this.Dispose();
			Application.Exit();
		}
		
		//xp下搜索框失去焦点
		private void txtSearch_Leave(object sender, EventArgs e)
		{
			if (!Config.IsWindowsVistaOr7() && txtSearch.Text == "")
				txtSearch.Text = "快捷搜索";
		}

		//删除任务
		private void mnuConDelete_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("是否要删除选定的下载任务?", "删除下载任务",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
			{
				return;
			}

			foreach (ListViewItem item in lsv.SelectedItems)
			{
				IDownloader downloader = GetTask(new Guid((string)item.Tag));
				taskMgr.DeleteTask(downloader, Config.setting.DeleteTaskAndFile);
				//删除UI
				lsv.Items.Remove(item);
			}
		}

		//按下delete键删除任务
		private void lsv_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				ListViewItem item = lsv.SelectedItems[0];
				if (item != null)
				{
					IDownloader downloader = GetTask(new Guid((string)item.Tag));
					taskMgr.DeleteTask(downloader, e.Shift | Config.setting.DeleteTaskAndFile);
				}
			}
		}

		//获得win消息
		[DebuggerNonUserCode()]
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			if (m.Msg == (int)RegisterWindowMessage("TaskbarButtonCreated"))
				if (Config.IsWindows7() && Config.setting.EnableWindows7Feature)
				{
					newbtn = new THUMBBUTTON()
						 {
							 iId = 1001,
							 szTip = "新建下载任务",
							 dwFlags = THBFLAGS.THBF_ENABLED,
							 dwMask = THBMASK.THB_FLAGS | THBMASK.THB_ICON | THBMASK.THB_TOOLTIP,
							 hIcon = ((Bitmap)btnNew.Image).GetHicon()
						 };
					taskbarList.ThumbBarAddButtons(this.Handle, 1, new THUMBBUTTON[1] { newbtn });
				}

			if (m.Msg == 0x0111) //thumbbutton clicked
			{
				if ((short)(m.WParam.ToInt64() >> 16) == 0x1800)
				{
					int buttonId = (short)(m.WParam.ToInt64() & 0xFFFF);
					switch (buttonId)
					{
						case 1001:
							btnNew_Click(this, EventArgs.Empty);
							break;
						default:
							break;
					}

				}
			}
		}


		#endregion

		#region ——————任务管理——————

		/// <summary>
		/// 根据GUID值寻找对应的任务
		/// </summary>
		/// <param name="guid"></param>
		/// <returns></returns>
		[DebuggerNonUserCode()]
		public IDownloader GetTask(Guid guid)
		{
			foreach (var i in taskMgr.Tasks)
			{
				if (i.TaskId == guid)
					return i;
			}
			return null;
		}

		/// <summary>
		/// 根据GUID值寻找对应的ListViewItem
		/// </summary>
		/// <param name="guid"></param>
		/// <returns></returns>
		[DebuggerNonUserCode()]
		public ListViewItem GetLsvItem(Guid guid)
		{
			for (int i = 0; i < lsv.Items.Count; i++)
			{
				if (lsv.Items[i].Tag != null)
				{
					if (new Guid((string)lsv.Items[i].Tag) == guid)
					{
						return lsv.Items[i];
					}
				}
			}
			return null;
		}

		//刷新任务
		private void RefreshTask(object e)
		{
			//如果需要在安全的线程上下文中执行
			if (this.InvokeRequired)
			{
				this.Invoke(new AcTaskDelegate(RefreshTask), e);
				return;
			}

			ParaRefresh r = (ParaRefresh)e;
			ListViewItem item = GetLsvItem(r.TaskId);
			IDownloader downloader = GetTask(r.TaskId);

			//如果UI存在此任务
			if (item != null) 
			{
				//UI存在并且任务存在
				if (downloader != null)
				{
					//刷新界面
					//状态
					item.SubItems[0].Text = downloader.Status.ToString();
					//视频名称
					item.SubItems[1].Text = downloader.Title;
					//分段
					item.SubItems[2].Text = downloader.CurrentPart.ToString() + "/" + downloader.PartCount.ToString();
					//下载进度
					item.SubItems[3].Text = "";
					//下载速度
					item.SubItems[4].Text = "";
					//源地址
					item.SubItems[5].Text = downloader.Url;
				}
				else //UI存在但是任务不存在
				{
					//清除UI上的此任务
					lsv.Items.Remove(item);
				}
			}
			else  //如果UI不存在此任务
			{
				//新建ListViewItem
				ListViewItem lvi = new ListViewItem();
				//lvi.SubItems.Add(downloader.Status.ToString()); //状态
				lvi.SubItems.Add("正在解析,请稍候"); //视频名称
				lvi.SubItems.Add("0/0"); //分段
				lvi.SubItems.Add("0.0%"); //下载进度
				lvi.SubItems.Add("0"); //下载速度
				lvi.SubItems.Add(downloader.Url); //源地址
				lvi.Tag = downloader.TaskId.ToString(); //设置TAG
				lsv.Items.Add(lvi);
			}

		}

		//任务开始
		public void Start(object e)
		{
			//如果需要在安全的线程上下文中执行
			if (this.InvokeRequired)
			{
				this.Invoke(new AcTaskDelegate(Start), e);
				return;
			}
			//转换参数
			ParaStart p = (ParaStart)e;
			//取得指定任务
			IDownloader downloader = GetTask(p.TaskId);
			if (downloader == null)
				return;

			//设置TaskItem
			ListViewItem item = GetLsvItem(p.TaskId);
			item.SubItems[0].Text = "正在下载";
		} //end Start

		// 进入到新的分段
		public void NewPart(object e)
		{
			//如果需要在安全的线程上下文中执行
			if (this.InvokeRequired)
			{
				this.Invoke(new AcTaskDelegate(NewPart), e);
				return;
			}
			ParaNewPart p = (ParaNewPart)e;
			IDownloader downloader = GetTask(p.TaskId);
			ListViewItem item = GetLsvItem(p.TaskId);
			//设置提示信息
			//item.SubItems[0].Text = downloader.Status.ToString();
			//视频标题
			item.SubItems[1].Text = downloader.Title;
			try
			{
				item.SubItems[2].Text = p.PartNumber.ToString() + @"/" + downloader.PartCount.ToString();
			}
			catch
			{
				item.SubItems[2].Text = "1/1";
			}
		}//end NewPart


		/// <summary>
		/// 提示下载信息
		/// </summary>
		/// <param name="e"></param>
		public void TipText(object e)
		{
			//如果需要在安全的线程上下文中执行
			if (this.InvokeRequired)
			{
				this.Invoke(new AcTaskDelegate(TipText), e);
				return;
			}
			ParaTipText p = (ParaTipText)e;
			IDownloader ac = GetTask(p.TaskId);
			ListViewItem item = GetLsvItem(p.TaskId);
			//设置提示信息
			item.SubItems[3].Text = p.TipText;
		}//end TipText

		//下载完成(需要判断下载完成还是用户手动停止)
		public void Finish(object e)
		{
			//如果需要在安全的线程上下文中执行
			if (this.InvokeRequired)
			{
				this.Invoke(new AcTaskDelegate(Finish), e);
				return;
			}

			ParaFinish p = (ParaFinish)e;
			IDownloader downloader = GetTask(p.TaskId);
			ListViewItem item = GetLsvItem(p.TaskId);

			//如果下载成功
			if (p.Successed)
			{
				//更新item
				item.SubItems[0].Text = downloader.Status.ToString();
				item.SubItems[3].Text = @"100%"; //下载进度
				item.SubItems[4].Text = ""; //下载速度
				//打开文件夹
				if (Config.setting.OpenFolderAfterComplete)
					Process.Start(Config.setting.SavePath);
				//播放声音
				if (Config.setting.PlaySound)
				{
					System.Media.SoundPlayer player = new System.Media.SoundPlayer();
					//优先播放设置文件中的声音(必须是wav格式&忽略大小写)
					if (File.Exists(Config.setting.SoundFile) && Config.setting.SoundFile.EndsWith(".wav", StringComparison.CurrentCultureIgnoreCase))
					{
						player.SoundLocation = Config.setting.SoundFile;
					}
					else
					{
						//然后播放程序目录下的msg.wav文件
						if (File.Exists(Path.Combine(Application.StartupPath, "msg.wav")))
						{
							player.SoundLocation = Path.Combine(Application.StartupPath, "msg.wav");
						}
						else //如果都没有则播放资源文件中的声音文件
						{
							player.Stream = Resources.finish;
						}
					}
					player.Load();
					player.Play();
					player.Dispose();
				}
			}
			else //如果用户取消下载
			{
				if (item != null)
				{
					//更新item
					item.SubItems[0].Text = downloader.Status.ToString();
					item.SubItems[4].Text = ""; //下载速度
				}
			}

			//执行下一个可能开始的任务
			taskMgr.ContinueNext();

			//如果没有正在等待的任务了且正在运行的任务为0
			if (taskMgr.GetNextWaiting() == null && taskMgr.GetRunningCount() == 0)
			{
				ShutdownType action = ShutdownType.None;
				//执行关机任务
				switch (cboAfterComplete.SelectedIndex)
				{
					case 0: //无动作
						action = ShutdownType.None;
						break;
					case 1: //关机
						action = ShutdownType.Shutdown;
						break;
					case 2: //待机
						action = ShutdownType.Suspend;
						break;
					case 3: //休眠
						action = ShutdownType.Hibernate;
						break;
					case 4: //注销
						action = ShutdownType.Logoff;
						break;
					case 5: //重启
						action = ShutdownType.Reboot;
						break;
					case 6: //退出程序
						action = ShutdownType.ExitProgram;
						break;
				}
				if (action == ShutdownType.ExitProgram)
				{
					mnuTrayExit_Click(this, EventArgs.Empty);
				}
				if (action != ShutdownType.None)
				{
					FormShutdown frm = new FormShutdown(action);
					frm.ShowDialog();
				}
			}

		}

		bool errorTip = true;
		//出现错误下载失败
		public void Error(object e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new AcTaskDelegate(Error), e);
				return;
			}
			ParaError p = (ParaError)e;

			ListViewItem item = GetLsvItem(p.TaskId);
			//添加到日志
			Logging.Add(p.E);

			IDownloader downloader = GetTask(p.TaskId);
			if (downloader != null)
			{
				//更新item
				item.SubItems[0].Text = downloader.Status.ToString();
				item.SubItems[3].Text = @"下载出错"; //下载进度
				item.SubItems[4].Text = ""; //下载速度
			}
			//显示ToolTip
			if (errorTip)
			{
				toolTip.Show("下载出现问题了？点这里", this, cboAfterComplete.Left, cboAfterComplete.Top);
				errorTip = false;
			}
		}

#endregion









	}//end class


}//end namespace
