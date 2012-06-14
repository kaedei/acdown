using System;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Kaedei.AcDown.Properties;
using System.Runtime.InteropServices;
using Kaedei.AcDown.Interface;
using Kaedei.AcDown.Core;
using System.Threading;
using System.Text;
using System.Collections.ObjectModel;
using Kaedei.AcDown.Interface.Forms;
using System.Security.Principal;
using System.ComponentModel;


namespace Kaedei.AcDown.UI
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
		private UIDelegateContainer deles;

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
			pluginMgr = new PluginManager(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"Kaedei\AcDown\Plugin"));
			pluginMgr.LoadPlugins();
			//委托
			deles = new UIDelegateContainer(
				new AcTaskDelegate(Start),
				new AcTaskDelegate(NewPart),
				new AcTaskDelegate(RefreshTask),
				new AcTaskDelegate(TipText),
				new AcTaskDelegate(Finish),
				new AcTaskDelegate(Error),
				new AcTaskDelegate(NewTask));
			//任务管理器
			taskMgr = new TaskManager(deles, pluginMgr);
			taskMgr.LoadAllTasks();
			//"新建任务"窗体初始化
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
		}


		//窗体加载
		private void FormMain_Load(object sender, EventArgs e)
		{
			//设置窗口大小
			this.Size = Config.setting.WindowSize;
			//设置窗体标题和文字
			this.Icon = Resources.Ac;
			this.Text = Application.ProductName +
							" v" + new Version(Application.ProductVersion).Major + "." +
							new Version(Application.ProductVersion).Minor;
			//设置托盘文字
			notifyIcon.Text = this.Text;
			//显示托盘图标
			notifyIcon.Icon = Resources.Ac;
			//设置刷新频率
			timer.Interval = Config.setting.RefreshInfoInterval;
			//设置是否监视剪贴板
			watchClipboard = Config.setting.WatchClipboardEnabled;
			//显示网址示例
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("AcDown当前支持下载的网站:(网址举例)");
			foreach (var item in pluginMgr.Plugins)
			{
				if (item.Feature.ContainsKey("ExampleUrl"))
				{
					string[] tmp = (string[])item.Feature["ExampleUrl"];
					sb.AppendLine();
					foreach (var i in tmp)
					{
						sb.AppendLine(i);
					}
					sb.AppendLine();
				}
			}
			txtExample.Text = sb.ToString();
			//初始化AERO特效
			if (Config.IsWindowsVistaOrHigher())
			{
				if (Config.IsWindows7OrHigher())
				{
					//初始化Win7任务栏管理器
					taskbarList = (ITaskbarList3)new CTaskbarList();
					taskbarList.HrInit();
				}
				//设置提示文字
				SendMessage(txtSearch.TextBox.Handle, 0x1501, IntPtr.Zero, System.Text.Encoding.Unicode.GetBytes(@"即时搜索"));
				//设置listview效果
				SetWindowTheme(this.lsv.Handle, "explorer", null); //Explorer style 
				SendMessage(this.lsv.Handle, LVM_SETEXTENDEDLISTVIEWSTYLE, 0, LVS_EX_FULLROWSELECT + LVS_EX_DOUBLEBUFFER);  //Blue selection
			}
			else  //如果是XP系统
			{
				txtSearch.Text = "即时搜索";
			}
			//选中下拉列表框
			cboAfterComplete.SelectedIndex = 0;
			//检查更新
			if (Config.setting.EnableCheckUpdate)
				CheckUpdate();
			//启动自动保存线程
			taskMgr.StartSaveBackgroundWorker();
			//加载任务UI
			foreach (TaskInfo task in taskMgr.TaskInfos)
			{
				RefreshTask(new ParaRefresh(task));
			}
			//程序文件名中有acplay
			if (Path.GetFileNameWithoutExtension(Application.ExecutablePath)
				.IndexOf("acplay", StringComparison.CurrentCultureIgnoreCase) >= 0)
			{
				tabMain.SelectedTab = tabAcPlay;
			}
			//如果命令行中指定播放acplay
			ShowFormToFront();
		}

		//关于
		private void btnAbout_Click(object sender, EventArgs e)
		{
			FormAbout about = new FormAbout();
			about.ShowDialog();
			about.Dispose();
		}

		//设置
		private void btnConfig_Click(object sender, EventArgs e)
		{
			FormConfig config = new FormConfig(this.pluginMgr);
			config.ShowDialog();
			config.Dispose();
			//重新加载某些项目
			//检查更新
			if (Config.setting.EnableCheckUpdate)
				CheckUpdate();
			//刷新“同时进行的任务数”设置
			taskMgr.ContinueNext();
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			//禁用Win7缩略图按钮
			if (Config.IsWindows7OrHigher() && Config.setting.EnableWindows7Feature)
			{
				newbtn.dwFlags = THBFLAGS.THBF_DISABLED;
				taskbarList.ThumbBarUpdateButtons(this.Handle, 1, new THUMBBUTTON[1] { newbtn });
			}
			watchClipboard = false;
			//显示“新建”
			FormNew.ShowForm("");
			watchClipboard = true;
			//启用Win7缩略图按钮
			if (Config.IsWindows7OrHigher() && Config.setting.EnableWindows7Feature)
			{
				newbtn.dwFlags = THBFLAGS.THBF_ENABLED;
				taskbarList.ThumbBarUpdateButtons(this.Handle, 1, new THUMBBUTTON[1] { newbtn });
			}
		}

		private void txtSearch_Click(object sender, EventArgs e)
		{
			if (txtSearch.Text == "即时搜索")
				txtSearch.Text = "";
			else
				txtSearch.SelectAll();
		}

		//上一次取得的URL
		private string lastUrl;
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
			taskMgr.SetSpeedLimitKb(sl);

			//全局速度
			double speed = 0;
			//取得所有正在进行中的任务
			foreach (TaskInfo task in taskMgr.TaskInfos)
			{
				//如果是正在下载的任务
				if (task.Status == DownloadStatus.正在下载)
				{
					try
					{
						//显示进度
						ListViewItem item = (ListViewItem)task.UIItem;
						if (task.Downloader.TotalLength != 0)
						{
							item.SubItems[GetColumn("Progress")].Text = string.Format(@"{0:P}", task.GetProgress());
						}
						else
						{
							item.SubItems[GetColumn("Progress")].Text = "0.0%";
						}
						//显示速度
						double currentSpeed = 0;
						currentSpeed = (double)task.GetTickCount() / (timer.Interval * 1024 / 1000);
						if (currentSpeed < 0) currentSpeed = 0;
						speed += currentSpeed;
						if (currentSpeed > 1024)
							item.SubItems[GetColumn("Speed")].Text = string.Format("{0:F1}", currentSpeed / 1024) + "MB/s";
						else
							item.SubItems[GetColumn("Speed")].Text = string.Format("{0:F1}", currentSpeed) + "KB/s";
						//显示已用时间
						DateTime now = DateTime.Now;
						TimeSpan use = now - task.CreateTime;
						item.SubItems[GetColumn("PastTime")].Text = string.Format("{0:D2}:{1:D2}:{2:D2}", use.Hours, use.Minutes, use.Seconds);
						//显示剩余时间：剩余时间 = (总长度 - 已完成)/每秒速度
						double remain = (task.Downloader.TotalLength - task.Downloader.DoneBytes) / currentSpeed / 1024;
						if (remain > 0 && !double.IsInfinity(remain))
						{
							try
							{
								int hour = (int)(remain / 3600);
								int minute = (int)((remain % 3600) / 60);
								int second = (int)(remain % 3600 % 60);
								item.SubItems[GetColumn("RemainTime")].Text = string.Format("{0:D2}:{1:D2}:{2:D2}", hour, minute, second);
							}
							catch { }
						}
					}
					catch
					{ }
				}
				//如果正在等待开始
				if (task.Status == DownloadStatus.等待开始)
				{
					ListViewItem item = (ListViewItem)task.UIItem;
					if (item != null)
					{
						item.SubItems[GetColumn("Status")].Text = "等待开始";
					}

				}
			}
			//显示全局速度
			if (speed != 0.0)
			{
				if (speed > 1024)
					lblSpeed.Text = string.Format("当前速度: {0:F1}", speed / 1024) + "MB/s";
				else
					lblSpeed.Text = string.Format("当前速度: {0:F1}", speed) + "KB/s";
			}
			else
			{
				lblSpeed.Text = "";
			}
			//显示Win7任务栏特性
			if (Config.IsWindows7OrHigher())
			{
				if (Config.setting.EnableWindows7Feature)
				{
					TaskInfo a = taskMgr.GetFirstRunning();
					if (a != null) //如果有任务正在运行
					{
						taskbarList.SetProgressState(this.Handle, TBPFLAG.TBPF_NORMAL);
						//显示此任务的进度
						taskbarList.SetProgressValue(this.Handle, (ulong)(a.GetProgress() * 10000), 10000);
					}
					else
					{
						taskbarList.SetProgressState(this.Handle, TBPFLAG.TBPF_NOPROGRESS);
					}
					//设置win7任务栏小图标
					//taskbarList.SetOverlayIcon(this.Handle,this.Icon.Handle, "w");
					//设置缩略图
					//if (this.WindowState != FormWindowState.Minimized)
					//{
					//   if (Config.setting.ShowLogo)
					//   {
					//      RECT rect = new RECT(picLogo.Left, picLogo.Top, picLogo.Right, picLogo.Bottom);
					//      //RECT rect = new RECT(lsv.Left, lsv.Top, lsv.Right, lsv.Bottom);
					//      taskbarList.SetThumbnailClip(this.Handle, ref rect);
					//   }
					//}
				}
				else  //如果禁止Win7特性
				{
					taskbarList.SetProgressState(this.Handle, TBPFLAG.TBPF_NOPROGRESS);
				}
			}
		} // end Timer_Tick

		//监视剪贴板
		[DebuggerNonUserCode()]
		private void timerClipboard_Tick(object sender, EventArgs e)
		{
			//如果允许监视剪贴板
			if (Config.setting.WatchClipboardEnabled && watchClipboard)
			{
				if (Clipboard.ContainsText())
				{
					if (Clipboard.GetText() != lastUrl && FormNew.CheckUrl(Clipboard.GetText()))
					{
						watchClipboard = false;
						lastUrl = Clipboard.GetText();


						//禁用Win7缩略图按钮
						if (Config.IsWindows7OrHigher() && Config.setting.EnableWindows7Feature)
						{
							newbtn.dwFlags = THBFLAGS.THBF_ENABLED;
							taskbarList.ThumbBarUpdateButtons(this.Handle, 1, new THUMBBUTTON[1] { newbtn });
						}
						//显示“新建”窗口
						FormNew.ShowForm(Clipboard.GetText());

						//启用Win7缩略图按钮
						if (Config.IsWindows7OrHigher() && Config.setting.EnableWindows7Feature)
						{
							newbtn.dwFlags = THBFLAGS.THBF_ENABLED;
							taskbarList.ThumbBarUpdateButtons(this.Handle, 1, new THUMBBUTTON[1] { newbtn });
						}
						watchClipboard = true;

					}
				}
			}
		}// end timerClipboard_Tick



		//程序正在退出
		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = !exitapp;
			if (!exitapp && Config.setting.HideWhenClose)
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


		//显示工具栏
		private void lsv_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lsv.SelectedItems.Count > 0)
			{
				//取得最后一项
				ListViewItem sItem = lsv.SelectedItems[lsv.SelectedIndices.Count - 1];
				Point pos = sItem.Position;

				//计算最后一项+1项的高度
				contextTool.Top = lsv.Top + pos.Y + 45;
				//如果高度超过listview的范围
				if (contextTool.Top + contextTool.Height > lsv.Top + lsv.Height)
				{
					contextTool.Top = lsv.Top + lsv.Height - contextTool.Height * 3;
				}
				//显示"更多"菜单
				mnuConMore.Visible = false;
				if (lsv.SelectedItems.Count == 1)
				{
					TaskInfo downloader = GetTask(new Guid((string)sItem.Tag));
					if (downloader.Status == DownloadStatus.下载完成 || downloader.Status == DownloadStatus.部分完成)
					{
						mnuConMore.Visible = true;
					}
					//特性
					if (downloader.Settings != null)
					{
						//导出地址
						mnuConExportUrlList.Enabled = downloader.Settings.ContainsKey("ExportUrl");
						//AcPlay
						mnuConAcPlay.Enabled = downloader.Settings.ContainsKey("AcPlay");
					}
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
				TaskInfo downloader = GetTask(new Guid((string)item.Tag));
				taskMgr.StartTask(downloader);
			}
		}

		//菜单中的停止
		private void mnuConStop_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("是否要停止选定的下载任务?", "停止下载",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
			{
				return;
			}
			//停止所有可能停止的任务
			foreach (ListViewItem item in lsv.SelectedItems)
			{
				TaskInfo downloader = GetTask(new Guid((string)item.Tag));
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
				try
				{
					TaskInfo downloader = GetTask(new Guid((string)item.Tag));
					if (!string.IsNullOrEmpty(downloader.SaveDirectory.ToString()))
						Process.Start(downloader.SaveDirectory.ToString());
					else
						Process.Start(Config.setting.SavePath);
				}
				catch { }
			}
		}

		//打开视频页面
		private void mnuConOpenUrl_Click(object sender, EventArgs e)
		{
			ListViewItem item = lsv.SelectedItems[0];
			if (item != null)
			{
				TaskInfo downloader = GetTask(new Guid((string)item.Tag));
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

		//导出真实地址列表
		private void mnuConExportUrlList_Click(object sender, EventArgs e)
		{
			try
			{
				if (lsv.SelectedItems.Count == 1)
				{
					ListViewItem item = lsv.SelectedItems[0];
					TaskInfo downloader = GetTask(new Guid((string)item.Tag));
					//检查ExportUrl属性
					string[] urls = downloader.Settings["ExportUrl"].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
					//如果长度大于0 导出列表
					if (urls.Length > 0)
					{
						SaveFileDialog sfd = new SaveFileDialog();
						sfd.Filter = "列表文件(*.txt)|*.txt";
						sfd.InitialDirectory = Config.setting.SavePath;
						if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
						{
							File.WriteAllLines(sfd.FileName, urls, Encoding.UTF8);
						}
					}
				}
			}
			catch { }

		}


		//播放AcPlay
		private void mnuConAcPlay_Click(object sender, EventArgs e)
		{
			try
			{
				if (lsv.SelectedItems.Count == 1)
				{
					ListViewItem item = lsv.SelectedItems[0];
					TaskInfo downloader = GetTask(new Guid((string)item.Tag));
					tabMain.SelectedTab = tabAcPlay;
					acPlay.PlayConfig(downloader.Settings["AcPlay"]);
				}
			}
			catch { }
		}

		//自定义搜索引擎
		private void searchCustom_Click(object sender, EventArgs e)
		{
			FormConfig frm = new FormConfig("pageUI");
			frm.ShowDialog();
			frm.Dispose();
		}

		//搜索
		private void btnSearch_ButtonClick(object sender, EventArgs e)
		{
			if (txtSearch.Text == "即时搜索")
				return;
			if (txtSearch.Text.Length != 0)
			{
				string q;
				switch (Config.setting.SearchQuery)
				{
					case "Acfun站内搜索":
						q = @"http://www.acfun.tv/search.aspx?q=%TEST%".Replace("%TEST%", Tools.UrlEncode(txtSearch.Text));
						break;
					case "Bilibili站内搜索":
						q = @"http://www.bilibili.tv/search?keyword=%TEST%".Replace("%TEST%", Tools.UrlEncode(txtSearch.Text));
						break;
					case "土豆网":
						q = @"http://so.tudou.com/nisearch/%TEST%/".Replace("%TEST%", Tools.UrlEncode(txtSearch.Text));
						break;
					case "优酷搜索(搜酷)":
						q = @"http://www.soku.com/search_video/q_%TEST%".Replace("%TEST%", Tools.UrlEncode(txtSearch.Text));
						break;
					case "漫画搜索(爱漫画)":
						q = @"http://www.imanhua.com/v2/user/search.aspx?key=%TEST%".Replace("%TEST%", Tools.UrlEncode(txtSearch.Text));
						break;
					default:
						q = Config.setting.SearchQuery.Replace(@"%TEST%", Tools.UrlEncode(txtSearch.Text));
						break;
				}
				try
				{
					Process.Start(q);
				}
				catch { };
			}
		}

		//即时搜索
		private void txtSearch_KeyUp(object sender, KeyEventArgs e)
		{
			//回车搜索
			if (e.KeyData == Keys.Enter)
			{
				string t = txtSearch.Text.Trim();
				//if (t == "清除搜索结果") //清除搜索结果
				//{
				//   txtSearch.AutoCompleteCustomSource.Clear();
				//   txtSearch.AutoCompleteCustomSource.Add("清除搜索结果");
				//}
				//加入历史记录
				//else 
				if (t != "")
				{
					if (!this.txtSearch.AutoCompleteCustomSource.Contains(t)) //如果历史记录中没有
					{
						txtSearch.AutoCompleteCustomSource.Add(txtSearch.Text);
					}
					btnSearch_ButtonClick(this, EventArgs.Empty);
				}
			}
			else
			{
				SetTaskFilter(new string[] { txtSearch.Text.Trim() });
				rdoSearch.Checked = true;
			}
		}

		private void lblSpeed_Click(object sender, EventArgs e)
		{
			tabMain.SelectTab("tabConfig");
			udSpeedLimit.Select();
		}

		//点击"帮助中心"链接
		private void toolHelpCenter_Click(object sender, EventArgs e)
		{
			FormHelp frmHelp = new FormHelp();
			frmHelp.ShowDialog();
		}

		//点击“常见问题”链接
		private void toolQA_Click(object sender, EventArgs e)
		{
			ToolForm.CreateWebpageForm("http://blog.sina.com.cn/s/blog_58c506600100z40t.html", "常见问题", true);
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
				if (this.WindowState == FormWindowState.Minimized) //如果是最小化则恢复
				{
					this.WindowState = FormWindowState.Normal;
				}
				this.TopMost = true;
				this.Activate();
				this.TopMost = false;
			}

			//播放AcPlay配置文件
			if (!AcPlayStartup.IsHandled)
			{
				AcPlayStartup.IsHandled = true;
				tabMain.SelectedTab = tabAcPlay;
				acPlay.PlayConfig(AcPlayStartup.FilePath);
			}

		}

		//显示/隐藏窗口
		private void mnuTrayShowHide_Click(object sender, EventArgs e)
		{
			if (this.Visible)
			{
				if (this.WindowState == FormWindowState.Minimized) //如果最小化则恢复Normal
					this.WindowState = FormWindowState.Normal;
				else
					this.Visible = false;
			}
			else
			{
				this.Visible = true;
			}
			this.TopMost = true;
			this.Activate();
			this.TopMost = false;
		}

		//打开托盘菜单
		private void mnuTray_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (this.Visible)
			{
				if (this.WindowState == FormWindowState.Minimized) //如果最小化则恢复Normal
					mnuTrayShowHide.Text = "恢复AcDown动漫下载器";
				else
					mnuTrayShowHide.Text = "隐藏AcDown动漫下载器";
			}
			else
			{
				mnuTrayShowHide.Text = "显示AcDown动漫下载器";
			}
		}

		//退出程序
		private void mnuTrayExit_Click(object sender, EventArgs e)
		{
			//正在进行的任务数量
			Int32 c = taskMgr.GetRunningCount();
			if (c > 0)
			{
				DialogResult r = MessageBox.Show("有" + c.ToString() + "个任务正在运行，是否退出？", "AcDown动漫下载器", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if (r != DialogResult.Yes) //取消关闭
				{
					return;
				}
			}
			this.Cursor = Cursors.WaitCursor;
			//终止自动保存线程
			taskMgr.EndSaveBackgroundWorker();
			//保存所有任务
			Thread t = new Thread(new ThreadStart(new MethodInvoker(taskMgr.SaveAllTasks)));
			t.Start();
			this.Cursor = Cursors.Default;
			//释放托盘图标
			notifyIcon.Dispose();
			//关闭日志文件
			Logging.Exit();
			//退出程序
			exitapp = true;
			Program.frmStart.Close();
		}

		//xp下搜索框失去焦点
		private void txtSearch_Leave(object sender, EventArgs e)
		{
			if (!Config.IsWindowsVistaOrHigher() && txtSearch.Text == "")
				txtSearch.Text = "即时搜索";
		}

		//删除任务
		private void mnuConDelete_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("是否删除选定的下载任务？", "删除下载任务",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
			{
				return;
			}
			DeleteTask(Config.setting.DeleteTaskAndFile, true);
		}

		//彻底删除任务
		private void toolCompletelyDelete_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("是否彻底删除选定的下载任务？", "删除下载任务",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
			{
				return;
			}
			DeleteTask(Config.setting.DeleteTaskAndFile, false);
		}

		//彻底删除任务及文件
		private void mnuConDeleteAndFile_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("是否要彻底删除选定的下载任务，\n并移除及已下载的所有文件？", "彻底删除选定任务和文件",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
			{
				return;
			}
			DeleteTask(true, false);
		}

		private void DeleteTask(bool deletefile, bool removeToRecyclebin)
		{
			//隐藏浮动工具栏
			contextTool.Hide();

			Collection<TaskInfo> willbedeleted = new Collection<TaskInfo>();
			foreach (ListViewItem item in lsv.SelectedItems)
			{
				TaskInfo task = GetTask(new Guid((string)item.Tag));
				willbedeleted.Add(task);
			}

			//取消选中所有任务
			lsv.SelectedItems.Clear();

			foreach (TaskInfo item in willbedeleted)
			{
				taskMgr.DeleteTask(item, deletefile, removeToRecyclebin);
			}

		}

		//按下delete键删除任务
		private void lsv_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				mnuConDelete_Click(sender, EventArgs.Empty);
			}
		}

		//记录窗口大小
		private void FormMain_ResizeEnd(object sender, EventArgs e)
		{
			if (this.WindowState == FormWindowState.Normal)
			{
				Config.setting.WindowSize = this.Size;
				Config.SaveSettings();
			}
		}

		//调查问卷
		private void toolQuestionnaire_Click(object sender, EventArgs e)
		{
			Process.Start(@"http://www.sojump.com/jq/1055148.aspx");
			toolQuestionnaire.Visible = false;
		}

		//限速生效
		private void btnSpeedlimitApply_Click(object sender, EventArgs e)
		{
			taskMgr.SetSpeedLimitKb((int)udSpeedLimit.Value);
		}

		//获得win消息
		[DebuggerNonUserCode()]
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			if (m.Msg == (int)RegisterWindowMessage("TaskbarButtonCreated"))
				if (Config.IsWindows7OrHigher() && Config.setting.EnableWindows7Feature)
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
		public TaskInfo GetTask(Guid guid)
		{
			foreach (var i in taskMgr.TaskInfos)
			{
				if (i.TaskId == guid)
					return i;
			}
			return null;
		}

		/// <summary>
		/// 取得列所在位置
		/// </summary>
		/// <param name="columnName"></param>
		/// <returns></returns>
		private int GetColumn(string columnName)
		{
			foreach (ColumnHeader item in lsv.Columns)
			{
				if (item.Tag.ToString() == columnName)
					return item.Index;
			}
			return -1;
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
			ListViewItem item; //= GetLsvItem(r.TaskId);
			TaskInfo task = r.SourceTask;

			//如果任务被删除
			if (!taskMgr.TaskInfos.Contains(task))
			{
				//移除UI项
				if (lsv.Items.Contains((ListViewItem)task.UIItem))
				{
					lsv.Items.Remove((ListViewItem)task.UIItem);
				}
				return;
			}
			//如果存在此任务的UI
			if (task.UIItem != null)
			{
				item = (ListViewItem)task.UIItem;
				//刷新界面
				//状态
				item.SubItems[GetColumn("Status")].Text = task.Status.ToString();
				//视频名称
				item.SubItems[GetColumn("Name")].Text = task.Title;
				//分段
				item.SubItems[GetColumn("Part")].Text = task.CurrentPart.ToString() + "/" + task.PartCount.ToString();
				//下载进度
				item.SubItems[GetColumn("Progress")].Text = "";
				//下载速度
				item.SubItems[GetColumn("Speed")].Text = "";
				//剩余时间
				item.SubItems[GetColumn("RemainTime")].Text = "00:00:00";
				//已经过的时间
				item.SubItems[GetColumn("PastTime")].Text = "00:00:00";
				//源地址
				item.SubItems[GetColumn("SourceUrl")].Text = task.Url;
			}
			else  //如果UI不存在此任务
			{
				//新建ListViewItem
				ListViewItem lvi = new ListViewItem();

				for (int i = 0; i < 8; i++)
				{
					lvi.SubItems.Add("");
				}
				lvi.SubItems[GetColumn("Status")].Text = task.Status.ToString();//状态
				lvi.SubItems[GetColumn("Name")].Text = task.Title; //视频名称
				lvi.SubItems[GetColumn("Part")].Text = task.CurrentPart.ToString() + "/" + task.PartCount.ToString(); //分段
				lvi.SubItems[GetColumn("Progress")].Text = string.Format(@"{0:P}", task.GetProgress()); //下载进度
				lvi.SubItems[GetColumn("Speed")].Text = "0"; //下载速度
				lvi.SubItems[GetColumn("RemainTime")].Text = "00:00:00"; //剩余时间
				lvi.SubItems[GetColumn("PastTime")].Text = "00:00:00"; //已经过的时间
				lvi.SubItems[GetColumn("SourceUrl")].Text = task.Url; //源地址
				lvi.Tag = task.TaskId.ToString(); //设置TAG
				//添加到关联的UI中
				task.UIItem = lvi;
				//如果当前任务符合过滤器，则添加到UI中
				if (IsMatchCurrentFilter(task))
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
			TaskInfo task = p.SourceTask;
			if (task == null)
				return;

			//设置TaskItem
			ListViewItem item = (ListViewItem)task.UIItem;
			item.SubItems[GetColumn("Status")].Text = task.Status.ToString();//"正在下载";
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
			TaskInfo task = p.SourceTask;
			ListViewItem item = (ListViewItem)task.UIItem;
			//视频标题
			item.SubItems[GetColumn("Name")].Text = task.Title;
			try
			{
				item.SubItems[GetColumn("Part")].Text = p.PartNumber.ToString() + @"/" + task.PartCount.ToString();
			}
			catch
			{
				item.SubItems[GetColumn("Part")].Text = "1/1";
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
			TaskInfo ac = p.SourceTask;
			ListViewItem item = (ListViewItem)ac.UIItem;
			//设置提示信息
			item.SubItems[GetColumn("Name")].Text = p.TipText;
		}//end TipText

		/// <summary>
		/// 下载完成(需要判断下载完成还是用户手动停止)
		/// </summary>
		public void Finish(object e)
		{
			//如果需要在安全的线程上下文中执行
			if (this.InvokeRequired)
			{
				this.Invoke(new AcTaskDelegate(Finish), e);
				return;
			}

			ParaFinish p = (ParaFinish)e;
			TaskInfo task = p.SourceTask;
			ListViewItem item = (ListViewItem)task.UIItem;

			//设置完成时间
			task.FinishTime = DateTime.Now;

			//如果下载成功
			if (p.Successed)
			{
				//更新item
				item.SubItems[GetColumn("Status")].Text = task.Status.ToString();
				item.SubItems[GetColumn("Name")].Text = task.Title;
				item.SubItems[GetColumn("Progress")].Text = @"100%"; //下载进度
				item.SubItems[GetColumn("Speed")].Text = ""; //下载速度
				//打开文件夹
				if (Config.setting.OpenFolderAfterComplete)
					Process.Start(Config.setting.SavePath);
				//播放声音
				if (Config.setting.PlaySound)
				{
					try
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
								player.Stream = Resources.remind;
							}
						}
						player.Load();
						player.Play();
						player.Dispose();
					}
					catch { }
				}
			}
			else //如果用户取消下载
			{
				if (item != null)
				{
					//更新item
					item.SubItems[GetColumn("Status")].Text = task.Status.ToString();
					item.SubItems[GetColumn("Speed")].Text = ""; //下载速度
				}
			}
			//移除item
			if (lsv.Items.Contains(item))
				if (!IsMatchCurrentFilter(task))
					lsv.Items.Remove(item);

			//继续下一任务或关机
			ProcessNext();
		}

		bool errorTip = true;
		/// <summary>
		/// 出现错误下载失败
		/// </summary>
		public void Error(object e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new AcTaskDelegate(Error), e);
				return;
			}
			ParaError p = (ParaError)e;
			TaskInfo task = p.SourceTask;
			ListViewItem item = (ListViewItem)task.UIItem;
			//添加到日志
			Logging.Add(p.E);

			if (task != null)
			{
				//记录最后一次错误
				task.LastError = p.E;
				//更新item
				item.SubItems[GetColumn("Status")].Text = task.Status.ToString();
				item.SubItems[GetColumn("Name")].Text = task.Title;
				item.SubItems[GetColumn("Progress")].Text = @""; //下载进度
				item.SubItems[GetColumn("Speed")].Text = ""; //下载速度
			}
			if (p.E.Message == "Plugin Not Found")
			{
				MessageBox.Show("AcDown希望使用这个插件来下载此任务:\n" + task.PluginName +
										"\n遗憾的是，您好像并未启用它。", "未加载指定的插件",
										MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			//显示ToolTip
			if (errorTip)
			{
				toolTip.Show("下载出现问题了？点这里", this, this.Width - toolHelpCenter.Width + 10
					, this.Height - toolHelpCenter.Height + 5);
				errorTip = false;
			}
			//继续下一任务或关机
			ProcessNext();
		}

		/// <summary>
		/// 插件新建任务
		/// </summary>
		/// <param name="e"></param>
		public void NewTask(object e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new AcTaskDelegate(NewTask), e);
				return;
			}
			ParaNewTask p = (ParaNewTask)e;
			TaskInfo sourcetask = p.SourceTask;
			IPlugin plugin = p.Plugin;
			string url = p.Url;

			//检查参数有效性
			if (!plugin.CheckUrl(url) || sourcetask == null || plugin == null || string.IsNullOrEmpty(url))
				return;
			//取得此url的hash
			string hash = plugin.GetHash(url);
			//检查是否有已经在进行的相同任务
			foreach (TaskInfo t in taskMgr.TaskInfos)
			{
				if (hash == t.Hash)
				{
					//如果有则不新建此任务
					//将状态由停止或删除修改为开始
					if (t.Status == DownloadStatus.出现错误 ||
						t.Status == DownloadStatus.已经停止 ||
						t.Status == DownloadStatus.已删除)
						taskMgr.StartTask(t);
					return;
				}
			}

			//设置新任务
			TaskInfo task = taskMgr.AddTask(plugin, url, sourcetask.Proxy);
			task.Settings = sourcetask.Settings;
			task.DownSub = sourcetask.DownSub;
			task.Comment = sourcetask.Comment;
			task.SaveDirectory = sourcetask.SaveDirectory;
			task.AutoAnswer = sourcetask.AutoAnswer;
			task.ExtractCache = sourcetask.ExtractCache;
			//此任务由其他任务所添加
			task.IsBeAdded = true;
			//开始新任务
			taskMgr.StartTask(task);

		}

		/// <summary>
		/// 执行下一个任务，如果所有任务执行完毕则执行关机任务
		/// </summary>
		public void ProcessNext()
		{
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

		#endregion

		#region ——————自动更新——————

		string haveupdate = "";

		/// <summary>
		/// 检查是否有软件更新
		/// </summary>
		private void CheckUpdate()
		{
			toolUpdate.Visible = false;
			Thread t = new Thread(new ThreadStart(() =>
			{
				Updater upd = new Updater();
				haveupdate = upd.CheckUpdate(new Version(Application.ProductVersion));
				if (!string.IsNullOrEmpty(haveupdate))
				{
					this.Invoke(new MethodInvoker(() =>
					{
						toolUpdate.Visible = true;
						notifyIcon.ShowBalloonTip(10000, "保持AcDown在最新状态!", "AcDown有新版本了哦~\n使用最新版本有助于减少解析错误发生的概率\n请点击主界面上方的“更新AcDown”按钮进行更新", ToolTipIcon.Info);
					}));
				}
			}));
			t.IsBackground = true;
			t.Start();

		}


		//下载更新
		private void toolUpdate_Click(object sender, EventArgs e)
		{
			DialogResult r = MessageBox.Show("AcDown将在后台下载软件更新。\n下载成功后AcDown会自动关闭并重新启动，现有下载任务将被中断。\n\n是否继续？", "自动更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (r == System.Windows.Forms.DialogResult.Yes)
			{
				toolUpdate.Text = "正在下载";
				toolUpdate.Enabled = false;
				Thread t = new Thread(new ThreadStart(new MethodInvoker(() =>
				{
					//忽略任何未知的错误（如线程强制停止）
					try
					{
						Updater upd = new Updater();
						bool success = upd.DownloadUpdate(haveupdate);
						if (success) //下载更新成功
						{
							Application.DoEvents();
							bool isadmin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
							ProcessStartInfo startInfo = new ProcessStartInfo();
							startInfo.UseShellExecute = true;
							startInfo.WorkingDirectory = Path.GetDirectoryName(upd.TempFile);
							startInfo.FileName = upd.TempFile;
							startInfo.Arguments = "\"" + Application.ExecutablePath + "\""; ;
							if (!isadmin)
								startInfo.Verb = "runas";
							try
							{
								Process process = Process.Start(startInfo);
							}
							catch (Win32Exception ex) //提升权限失败
							{
								startInfo.Verb = "";
								Process process = Process.Start(startInfo);
							}
							this.Invoke(new MethodInvoker(() =>
							{
								Program.frmStart.Close();
							}));
						}
						else//下载失败
						{
							this.Invoke(new MethodInvoker(() =>
							{
								MessageBox.Show("因为网络原因下载更新失败，请稍候重试", "更新AcDown", MessageBoxButtons.OK, MessageBoxIcon.Stop);
								toolUpdate.Text = "更新AcDown";
								toolUpdate.Enabled = true;
							}));
						}
					}
					catch
					{
						this.Invoke(new MethodInvoker(() =>
						{
							MessageBox.Show("因为网络原因下载更新失败，请稍候重试", "更新AcDown", MessageBoxButtons.OK, MessageBoxIcon.Stop);
							toolUpdate.Text = "更新AcDown";
							toolUpdate.Enabled = true;
						}));
					}
				})));
				t.IsBackground = true;
				t.Start();
			}
		}

		#endregion

		#region ——————过滤器——————


		private string[] _filter = new string[] { "状态:正在下载", "状态:等待开始", "状态:正在停止", "状态:出现错误", "状态:已经停止" };
		/// <summary>
		/// 设置任务过滤器
		/// </summary>
		/// <param name="filter"></param>
		private void SetTaskFilter(string[] filter)
		{
			_filter = filter;
			//临时挂起listview布局
			lsv.SuspendLayout();
			//清除当前所有
			lsv.Items.Clear();
			//查找所有任务
			foreach (TaskInfo task in taskMgr.TaskInfos)
			{
				//如果符合过滤器
				if (IsMatchCurrentFilter(task))
				{
					lsv.Items.Add((ListViewItem)task.UIItem);
				}
			}
			//恢复listview布局
			lsv.ResumeLayout();
			//隐藏浮动工具栏
			contextTool.Hide();
			//取消选中所有任务
			lsv.SelectedItems.Clear();
		}

		/// <summary>
		/// 判断所提供的任务是否符合当前过滤器
		/// </summary>
		/// <param name="infoToString"></param>
		private bool IsMatchCurrentFilter(TaskInfo task)
		{
			string tmp = task.ToString();
			foreach (string f in _filter)
			{
				if (f.Trim() != "")
					if (tmp.IndexOf(f.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
						return true;
			}
			return false;
		}

		//点击Radiobutton设置过滤器
		private void rdo_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton rdo = (RadioButton)sender;
			//设置过滤器
			if (rdo.Checked)
			{
				if (rdo.Tag.ToString() == "CustomSearch")
				{
					//设置焦点并显示提示
					txtSearch.Focus();
					toolTip.Show("输入文字,搜索已有任务", this,
									this.ClientSize.Width - btnSearch.Width - 20,
									this.Size.Height - this.ClientSize.Height + txtSearch.Height, 3500);
				}
				else
					SetTaskFilter(rdo.Tag.ToString().Split('|'));
			}
			////隐藏浮动工具栏
			//contextTool.Hide();
			////取消选中所有任务
			//lsv.SelectedItems.Clear();
		}

		#endregion



	}//end class


}//end namespace
