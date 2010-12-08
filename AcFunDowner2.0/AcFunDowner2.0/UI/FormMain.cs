using System;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Kaedei.AcDown;
using Kaedei.AcDown.Properties;
using System.Runtime.InteropServices;
using System.Threading;
using Kaedei.AcDown.Interface;


namespace AcDown
{

	 public partial class FormMain : Form
	 {

		 #region 外部调用

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
		 
		 /// <summary>
		 /// 委托的包装
		 /// </summary>
		 private DelegateContainer deles;
		 private TaskManager taskmanager;
		 private static ITaskbarList3 taskbarList;
		 private THUMBBUTTON newbtn;

#region 琐碎项目
		  

		  public FormMain()
		  {
			  //初始化静态类
			  Config.LoadSettings();
				Logging.Initialize();
			  //初始化窗体
				InitializeComponent();
			  //设置窗体标题和文字
				this.Icon = Resources.ac;
				this.Text = Application.ProductName;
				  //取消显示大按钮
				if(Config.setting.ShowBigStartButton==false)
				  if (btnClickNew != null)
						btnClickNew.Dispose();
				//显示托盘图标
				if (Config.setting.ShowTrayIcon)
				{
					notifyIcon.Icon = Resources.ac;
				}
				else
				{
					notifyIcon.Dispose();
				}
				//新建Container
				deles = new DelegateContainer(
					 new AcTaskDelegate(Start),
					 new AcTaskDelegate(NewPart),
					 new AcTaskDelegate(TipText),
					 new AcTaskDelegate(Finish),
					 new AcTaskDelegate(Error));
			  //初始化任务管理器
				taskmanager = new TaskManager(deles, new RefreshTaskListDelegate(RefreshTaskList));
				TaskManager.ObjectReference = taskmanager;

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
				  SendMessage(txtSearch.TextBox.Handle, 0x1501, IntPtr.Zero, System.Text.Encoding.Unicode.GetBytes(@"搜索视频"));
				  //设置listview效果
				  SetWindowTheme(this.lsv.Handle, "explorer", null); //Explorer style 
				  SendMessage(this.lsv.Handle, LVM_SETEXTENDEDLISTVIEWSTYLE, 0, LVS_EX_FULLROWSELECT+LVS_EX_DOUBLEBUFFER);  //Blue selection
			  }
			  //设置搜索框
			  switch (Config.setting.SearchEngine)
			  {
				  case "Google":
					  searchGoogle_Click(this, EventArgs.Empty);
					  break;
				  case "BiliBili":
					  searchBilibili_Click(this, EventArgs.Empty);
					  break;
				  case "Bing":
					  searchBing_Click(this, EventArgs.Empty);
					  break;
				  case "Baidu":
					  searchBaidu_Click(this, EventArgs.Empty);
					  break;
				  default:
					  searchGoogle.Checked = true;
					  searchBing.Checked = false;
					  searchBaidu.Checked = false;
					  searchCustom.Checked = true;
					  break;
			  }
			  //选中下拉列表框
			  cboAfterComplete.SelectedIndex = 0;
			  //设置更新文字
			  toolCheckNew.Text = "检查是否有新版本(当前版本:" + Application.ProductVersion.ToString() + ")";
		  }

		  private void btnAbout_Click(object sender, EventArgs e)
		  {
				//去除显示大按钮
				if (btnClickNew != null)
				{
					 btnClickNew.Dispose();
				}
				FormAbout about = new FormAbout();
				about.ShowDialog();
				about.Dispose();
		  }

		  private void btnConfig_Click(object sender, EventArgs e)
		  {
				//去除显示大按钮
				if (btnClickNew != null)
				{
					 btnClickNew.Dispose();
				}
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
				FormNew n = new FormNew();
				//禁用Win7缩略图按钮
				if (Config.IsWindows7() && Config.setting.EnableWindows7Feature)
				{
					newbtn.dwFlags = THBFLAGS.THBF_DISABLED;
					taskbarList.ThumbBarUpdateButtons(this.Handle, 1, new THUMBBUTTON[1] { newbtn });
				}
				watchClipboard = false;
			  //显示“新建”
				n.ShowDialog();
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
				if (txtSearch.Text == "搜索视频")
					 txtSearch.Text = "";
				else
					 txtSearch.SelectAll();
		  }

		 //新建任务
		  private void btnClickNew_Click(object sender, EventArgs e)
		  {
			  //显示动画提示效果
				Int32 width = btnClickNew.Size.Width ;
				Int32 height = btnClickNew.Size.Height;
				Int32 x=btnClickNew.Location.X;
				Int32 y = btnClickNew.Location.Y;
				
				for (int i = 99; i >= 0; i--)
				{
					 btnClickNew.Size=new Size(width*i/100, height * i / 100);
					 btnClickNew.Location = new Point(x * i / 100,y * i / 100);
					 Application.DoEvents();
				}
				btnClickNew.Dispose();
				watchClipboard = false;
				FormNew n = new FormNew();
				n.ShowDialog();
				watchClipboard = true;
		  }

		 //上一次取得的URL
		  private string lastUrl;
		 //是否监视剪贴板
		  private bool watchClipboard;

		  //显示进度以及速度 & 监视剪贴板
		  private void timer_Tick(object sender, EventArgs e)
		  {
			  if (Config.setting.WatchClipboardEnabled)
			  {
				  if(watchClipboard)
				  {
					  if (Clipboard.ContainsText())
					  {
						  if (AcDowner.CheckUrl(Clipboard.GetText()) && Clipboard.GetText() != lastUrl)
						  {
							  watchClipboard = false;
							  lastUrl = Clipboard.GetText();
							  //去除显示大按钮
							  if (btnClickNew != null)
							  {
								  btnClickNew.Dispose();
							  }
							  FormNew n = new FormNew(Clipboard.GetText());
							  //禁用Win7缩略图按钮
							  if (Config.IsWindows7() && Config.setting.EnableWindows7Feature)
							  {
								  newbtn.dwFlags = THBFLAGS.THBF_ENABLED;
								  taskbarList.ThumbBarUpdateButtons(this.Handle, 1, new THUMBBUTTON[1] { newbtn });
							  }
							  n.ShowDialog();
							  //启用Win7缩略图按钮
							  if (Config.IsWindows7() && Config.setting.EnableWindows7Feature)
							  {
								  newbtn.dwFlags = THBFLAGS.THBF_ENABLED;
								  taskbarList.ThumbBarUpdateButtons(this.Handle, 1, new THUMBBUTTON[1] { newbtn });
							  }
							  watchClipboard = true;

						  }
					  }
				  }
			  }
			  long speed = 0;
			  //取得所有正在进行中的任务
			  foreach (AcDowner ac in taskmanager.Tasks)
			  {
				  //如果是正在下载的任务
				  if (ac.Status == DownloadStatus.正在下载)
				  {
					  //显示进度
					  ListViewItem item = GetLsvItem(ac.TaskId);
					  item.SubItems[3].Text = string.Format(@"{0:P}", ac.DownloadProcess);
					  //显示速度
					  long currentSpeed = 0;
					  currentSpeed = (ac.DoneBytes - ac.LastBytes) / 1000;
					  speed += currentSpeed;
					  ac.LastBytes = ac.DoneBytes;
					  item.SubItems[4].Text = currentSpeed.ToString() + "KB/s";
				  }
				  //如果正在等待开始
				  if (ac.Status == DownloadStatus.等待开始)
				  {
					  ListViewItem item = GetLsvItem(ac.TaskId);
					  item.SubItems[0].Text = "等待开始";
				  }
			  }
			  //显示全局速度
			  if (speed != 0)
				  lblSpeed.Text = speed.ToString() + "KB/s";
			  //显示Win7任务栏
			  if (Config.setting.EnableWindows7Feature)
			  {
				  if (Config.IsWindows7())
				  {
					  AcDowner a = taskmanager.GetFirstRunning();
					  if (a != null) //如果有任务正在运行
					  {
						  //显示此任务的进度
						  taskbarList.SetProgressValue(this.Handle, (ulong)(a.DownloadProcess * 10000), 10000);
					  }
					  else
					  {
						  taskbarList.SetProgressState(this.Handle, TBPFLAG.TBPF_INDETERMINATE);
					  }
					  //设置win7任务栏小图标
					  //taskbarList.SetOverlayIcon(this.Handle,this.Icon.Handle, "w");
					  //设置缩略图
					  if (this.WindowState != FormWindowState.Minimized)
					  {
						  RECT rect = new RECT(lsv.Left, lsv.Top, lsv.Right, lsv.Bottom);
						  taskbarList.SetThumbnailClip(this.Handle, ref rect);
					  }
				  }
			  }
		  }

		  //程序正在退出
		  private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		  {
			  //正在进行的任务数量
			  Int32 c = taskmanager.GetRunningCount();
			  if (c > 0)
			  {
				  DialogResult r = MessageBox.Show("有" + c.ToString() + "个任务正在运行，是否退出？", "AcDown动漫下载器", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				  if (r == DialogResult.Yes) //确认关闭
				  {
					  this.Cursor = Cursors.WaitCursor;
					  foreach (AcDowner item in taskmanager.Tasks)
					  {
						  //所有设置为停止，等待其自动退出
						  item.Status = DownloadStatus.已经停止;
					  }
					  //执行十五次DoEvents，尽量使所有任务自动退出
					  for (int i = 0; i < 15; i++)
						  Application.DoEvents();
					  this.Cursor = Cursors.Default;
					  //释放托盘图标
					  notifyIcon.Dispose();
				  }
				  else //取消关闭
				  {
					  e.Cancel = true;
				  }

			  }
			  //关闭日志文件
			  Logging.Exit();
		  }

		 //单击弹出菜单
		  private void lsv_MouseClick(object sender, MouseEventArgs e)
		  {
			  //如果按下右键
			  if (e.Button == MouseButtons.Right)
			  {
				  //如果选择的项目大于0个
				  if (lsv.SelectedItems.Count > 0)
				  {
					  mnuContext.Show(lsv, e.Location);
				  }
			  }
		  }

		 //菜单中的开始
		  private void mnuConStart_Click(object sender, EventArgs e)
		  {
			  //开始所有可能开始的任务
			  foreach (ListViewItem item in lsv.SelectedItems)
			  {
				  AcDowner ac = GetTask(new Guid((string)item.Tag));
				  if (ac.Status == DownloadStatus.出现错误 || ac.Status == DownloadStatus.已经停止)
					  Start(new ParaStart(ac.TaskId));
			  }
		  }

		 //菜单中的停止
		  private void mnuConStop_Click(object sender, EventArgs e)
		  {
			  //停止所有可能停止的任务
			  foreach (ListViewItem item in lsv.SelectedItems)
			  {
				  AcDowner ac = GetTask(new Guid((string)item.Tag));
				  if (ac.Status == DownloadStatus.正在下载 || ac.Status == DownloadStatus.等待开始)
				  {
					  ac.Status = DownloadStatus.已经停止;
					  item.SubItems[0].Text = ac.Status.ToString();
				  }
			  }
		  }

		 //打开视频所在文件夹
		  private void mnuConOpenFolder_Click(object sender, EventArgs e)
		  {
			  ListViewItem item = lsv.SelectedItems[0];
			  if (item != null)
			  {
				  AcDowner ac = GetTask(new Guid((string)item.Tag));
				  if (!string.IsNullOrEmpty(ac.FilePath))
					  Process.Start(Path.GetDirectoryName(ac.FilePath));
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
				  AcDowner ac = GetTask(new Guid((string)item.Tag));
				  Process.Start(ac.Url);
			  }
		  }

		 //查看视频信息
		  private void mnuConInfo_Click(object sender, EventArgs e)
		  {
			  ListViewItem item = lsv.SelectedItems[0];
			  if (item != null)
			  {
				  AcDowner ac = GetTask(new Guid((string)item.Tag));
				  FormXml frmXml = new FormXml(ac);
				  frmXml.ShowDialog();
			  }
				
		  }

		 //链接到acfun
		  private void lnkAcfun_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		  {
			  Process.Start("http://acfun.cn/");
		  }

		 //链接到BiliBili
		  private void lnkBilibili_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		  {
			  Process.Start("http://bilibili.us");
		  }

		 //启动字幕下载器
		  private void lnkSubdowner_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		  {
			  string path = System.IO.Path.Combine(Application.StartupPath, "AcFunSubDowner.exe");
			  if (File.Exists(path))
				  Process.Start(path);
			  else
				  MessageBox.Show("未找到字幕下载器", "AcFunSubDowner.exe", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		  }

		 //使用Google搜索站内视频
		  private void searchGoogle_Click(object sender, EventArgs e)
		  {
			  searchGoogle.Checked = true;
			  Config.setting.SearchEngine = "Google";
			  Config.setting.SearchQuery = @"http://www.google.com/custom?domains=acfun.cn&q=%TEST%&sa=Google+%CB%D1%CB%F7&sitesearch=acfun.cn&client=pub-1186646738938701&forid=1&ie=GB2312&oe=GB2312&cof=GALT%3A%23008000%3BGL%3A1%3BDIV%3A%23336699%3BVLC%3A663399%3BAH%3Acenter%3BBGC%3AFFFFFF%3BLBGC%3A336699%3BALC%3A0000FF%3BLC%3A0000FF%3BT%3A000000%3BGFNT%3A0000FF%3BGIMP%3A0000FF%3BFORID%3A1&hl=zh-CN&ie=UTF-8";
			  Config.SaveSettings();
			  searchBing.Checked = false;
			  searchBaidu.Checked = false;
			  searchCustom.Checked = false;
			  searchBilibili.Checked = false;
		  }

		  private void searchBilibili_Click(object sender, EventArgs e)
		  {
			  searchBilibili.Checked = true;
			  Config.setting.SearchEngine = "BiliBili";
			  Config.setting.SearchQuery = @"http://bilibili.us/plus/search.php?keyword=%TEST%";
			  Config.SaveSettings();
			  searchBing.Checked = false;
			  searchBaidu.Checked = false;
			  searchCustom.Checked = false;
			  searchGoogle.Checked = false;
		  }

		  private void searchBing_Click(object sender, EventArgs e)
		  {
			  searchGoogle.Checked = false;
			  searchBing.Checked = true;
			  Config.setting.SearchEngine = "Bing";
			  Config.setting.SearchQuery = @"http://cn.bing.com/search?q=%TEST%+site%3Aacfun.cn";
			  Config.SaveSettings();
			  searchBaidu.Checked = false;
			  searchCustom.Checked = false;
			  searchBilibili.Checked = false;
		  }

		  private void searchBaidu_Click(object sender, EventArgs e)
		  {
			  searchBaidu.Checked = true;
			  Config.setting.SearchEngine = "Baidu";
			  Config.setting.SearchQuery = @"http://www.baidu.com/s?wd=%TEST%+site%3A%28acfun.cn%29";
			  Config.SaveSettings();
			  searchGoogle.Checked = false;
			  searchBilibili.Checked = false;
			  searchBing.Checked = false;
			  searchCustom.Checked = false;
		  }

		  private void searchCustom_Click(object sender, EventArgs e)
		  {
			  btnConfig_Click(this, EventArgs.Empty);
		  }
		 
		 //搜索视频
		  private void btnSearch_ButtonClick(object sender, EventArgs e)
		  {
			  if (txtSearch.Text.Length != 0)
			  {
				  string q = Config.setting.SearchQuery.Replace(@"%TEST%", txtSearch.Text);
				  Process.Start(q);
			  }
		  }

		 //按下回车搜索视频
		  private void txtSearch_KeyUp(object sender, KeyEventArgs e)
		  {
			  if (e.KeyData == Keys.Enter)
				  btnSearch_ButtonClick(this, EventArgs.Empty);
		  }


			//FLV合并器
		  private void lnkFLVConvert_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		  {
			  string path = Path.Combine(Application.StartupPath, "flvcomb.exe");
			  if (File.Exists(path))
			  {
				  Process.Start(path);
			  }
			  else
			  {
				  Process.Start(@"http://soft.pt42.com/flvcomb_index.htm");
			  }			  
		  }

		 //双击某一项目
		  private void lsv_DoubleClick(object sender, EventArgs e)
		  {
			  ListViewItem item = lsv.SelectedItems[0];
			  if (item != null)
			  {
				  AcDowner ac = GetTask(new Guid((string)item.Tag));
				  //根据状态执行不同的操作
				  switch (ac.Status)
				  {
					  case DownloadStatus.正在下载:
					  case DownloadStatus.等待开始:
						  ac.Status = DownloadStatus.已经停止;
						  break;
					  case DownloadStatus.已经停止:
					  case DownloadStatus.出现错误:
						  Start(new ParaStart(ac.TaskId));
						  break;
					  case DownloadStatus.下载完成:
						  try
						  {
							  Process.Start(ac.FilePath);
						  }
						  catch
						  {
							  MessageBox.Show("文件已删除或不存在", "打开文件", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						  }
						  break;
				  }
			  }
		  }

		 //双击托盘图标
		  private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		  {
			  if (this.WindowState == FormWindowState.Minimized)
			  {
				  this.Visible = true;
				  this.WindowState = FormWindowState.Normal;
			  }
			  else
				  this.WindowState = FormWindowState.Minimized;
		  }

		 //最小化
		  private void FormMain_Resize(object sender, EventArgs e)
		  {
			  if (this.WindowState == FormWindowState.Minimized)
				  if (Config.setting.ShowTrayIcon)
					  if (!Config.IsWindowsVistaOr7() || !Config.setting.EnableWindows7Feature)
						  this.Visible = false;
		  }

		  //检查新版本
		  private void toolCheckNew_Click(object sender, EventArgs e)
		  {
			  Process.Start(@"http://blog.sina.com.cn/s/blog_58c506600100h7np.html");
		  }

		 //使用交流
		  private void toolTieba_Click(object sender, EventArgs e)
		  {
			  Process.Start(@"http://tieba.baidu.com/f?kw=lavola");
		  }

		 //新功能建议
		  private void toolAdvise_Click(object sender, EventArgs e)
		  {
			  Process.Start(@"http://tieba.baidu.com/f?kz=690678202");
		  }

		 //错误提交
		  private void toolReportBug_Click(object sender, EventArgs e)
		  {
			  Process.Start(@"http://tieba.baidu.com/f?kz=690682848");
		  }

		 //设计错误
		  private void toolDesign_Click(object sender, EventArgs e)
		  {
			  Process.Start(@"http://tieba.baidu.com/f?kz=690684234");
		  }

		 //提交网址
		  private void toolReportUrl_Click(object sender, EventArgs e)
		  {
			  Process.Start(@"http://tieba.baidu.com/f?kz=690685789");
		  }

		 //xp下搜索框失去焦点
		  private void txtSearch_Leave(object sender, EventArgs e)
		  {
			  if (!Config.IsWindowsVistaOr7())
				  txtSearch.Text = "搜索AcFun视频";
		  }

		 //删除任务
		  private void mnuConDelete_Click(object sender, EventArgs e)
		  {
			  DeleteTask(Config.setting.DeleteTaskAndFile);
		  }

		 //按下delete键删除任务
		  private void lsv_KeyUp(object sender, KeyEventArgs e)
		  {
			  if (e.KeyCode == Keys.Delete)
			  {
				  if (e.Shift == true)
				  {
					  DeleteTask(!Config.setting.DeleteTaskAndFile);
				  }
				  else
				  {
					  DeleteTask(Config.setting.DeleteTaskAndFile);
				  }
			  }
		  }

		  //限速生效
		  private void btnLimitApply_Click(object sender, EventArgs e)
		  {
			  //每个任务的限速
			  taskmanager.LimitedSpeed = Convert.ToInt32(udSpeedLimit.Value);
			  //刷新限速
			  taskmanager.ContinueNext();
		  }

		 //获得win消息
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
							  btnNew_Click(this,EventArgs.Empty);
							  break;
						  default:
							  break;
					  }

				  }
			  }
		  }


#endregion

#region 任务管理
		 /// <summary>
		 /// 根据GUID值寻找对应的任务
		 /// </summary>
		 /// <param name="guid"></param>
		 /// <returns></returns>
		 public AcDowner GetTask(Guid guid)
		 {
			 foreach (var i in taskmanager.Tasks)
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
		 public ListViewItem GetLsvItem(Guid guid)
		 {
			 for(int i = 0;i < lsv.Items.Count;i++)
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

		 /// <summary>
		 /// 根据GUID更新ListView
		 /// </summary>
		 /// <param name="guid"></param>
		 public void RefreshTaskItem(Guid guid)
		 {
			 ListViewItem item = GetLsvItem(guid);
			 AcDowner ac = GetTask(guid);
			 //刷新listviewitem
			 if ((item != null) && (ac != null))
			 {
				 item.SubItems[0].Text  = ac.Status.ToString();
				 //视频名称
				 item.SubItems[1].Text  = ac.VideoTitle;
				 //分段
				 item.SubItems[2].Text = "等待开始";
				 //下载进度
				 item.SubItems[3].Text  = "等待开始";
				 //下载速度
				 item.SubItems[4].Text  = "等待开始";
				 //源地址(无需改变)
				 item.SubItems[5].Text  = ac.Url;
			 }

		 }

		 /// <summary>
		 /// 任务开始
		 /// </summary>
		 /// <param name="task">任务的TaskId属性</param>
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
			 AcDowner ac = GetTask(p.TaskId);
			 if (ac == null)
				 return;

			 //设置下载状态
			 //如果已达到最大运行数目
			 if (taskmanager.GetRunningCount() >= Config.setting.MaxRunningTaskCount)
			 {
				 ac.Status = DownloadStatus.等待开始;
				 return;
			 }
			//抢占开始的位置
			 ac.Status = DownloadStatus.正在下载;

			//设置TaskItem
			ListViewItem item =  GetLsvItem(p.TaskId);
			item.SubItems[0].Text = "正在开始";
			 //调用新线程进行下载
			 Thread t = new Thread(ac.Run);
			 t.Start();
			//刷新listviewitem
			//RefreshTaskItem(p.TaskId);
		} //end Start

		 /// <summary>
		 /// 进入到新的分段
		 /// </summary>
		 /// <param name="e"></param>
		public void NewPart(object e)
		{
			//如果需要在安全的线程上下文中执行
			if (this.InvokeRequired)
			{
				this.Invoke(new AcTaskDelegate(NewPart), e);
				return;
			}
			ParaNewPart p = (ParaNewPart)e;
			AcDowner ac = GetTask(p.TaskId);
			ListViewItem item = GetLsvItem(p.TaskId);
			//设置提示信息
			item.SubItems[0].Text = ac.Status.ToString();
			item.SubItems[1].Text = ac.VideoTitle;
			try
			{
				item.SubItems[2].Text = p.PartNumber.ToString() + @"/" + ac.PartCount.ToString();
			}
			catch
			{
				item.SubItems[2].Text = "1/1";
			}
		}//end NewPart


		/// <summary>
		/// 提示下载进度
		/// </summary>
		/// <param name="e"></param>
		public void TipProcess(object e)
		{
			//如果需要在安全的线程上下文中执行
			if (this.InvokeRequired)
			{
				this.Invoke(new AcTaskDelegate(TipProcess), e);
				return;
			}
			ParaTipProcess p = (ParaTipProcess)e;
			AcDowner ac = GetTask(p.TaskId);
			ListViewItem item = GetLsvItem(p.TaskId);
			//设置进度
			item.SubItems[3].Text = string.Format(@"{0:P}", p.Process);
		}//end TipProcess

		 /// <summary>
		 /// 提示下载信息
		 /// </summary>
		 /// <param name="e"></param>
		public void TipText(object e)
		{
			//如果需要在安全的线程上下文中执行
			if (this.InvokeRequired)
			{
				this.Invoke(new AcTaskDelegate(TipText ), e);
				return;
			}
			ParaTipText p = (ParaTipText)e;
			AcDowner ac = GetTask(p.TaskId);
			ListViewItem item = GetLsvItem(p.TaskId);
			//设置提示信息
			item.SubItems[3].Text = p.TipText;
		}//end TipText

		 /// <summary>
		 /// 下载完成
		 /// </summary>
		 /// <param name="e"></param>
		public void Finish(object e)
		{
			//如果需要在安全的线程上下文中执行
			if (this.InvokeRequired)
			{
				this.Invoke(new AcTaskDelegate(Finish), e);
				return;
			}
			
			ParaFinish p = (ParaFinish)e;
			AcDowner ac = GetTask(p.TaskId);
			
			//如果下载成功
			if (p.Successed) 
			{
				ListViewItem item = GetLsvItem(p.TaskId);
				//设置为停止
				ac.Status = DownloadStatus.下载完成;
				//更新item
				item.SubItems[0].Text  = ac.Status.ToString();
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
				//执行下一个可能开始的任务
				taskmanager.ContinueNext();
			}
			else //如果下载失败(用户取消)
			{
				if (ac == null)
					Error(new ParaError(Guid.NewGuid(), new Exception("下载失败")));
				else
					Error(new ParaError(ac.TaskId, new Exception("用户取消下载")));
			}

			//如果没有正在等待的任务了且正在运行的任务为0
			if (taskmanager.GetNextWaiting() == null && taskmanager.GetRunningCount() == 0)
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
				}
				if (action != ShutdownType.None)
				{
					FormShutdown frm = new FormShutdown(action);
					frm.ShowDialog();
				}
			}

		}

		 //出现错误
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

			AcDowner ac = GetTask(p.TaskId);
			if (ac != null)
			{
				//设置为下载失败
				//设置为停止或错误
				if (p.E.Message == "用户取消下载")
					ac.Status = DownloadStatus.已经停止;
				else
					ac.Status = DownloadStatus.出现错误;
				//更新item
				item.SubItems[0].Text = ac.Status.ToString();
				item.SubItems[3].Text = @"下载失败"; //下载进度
				item.SubItems[4].Text = ""; //下载速度

				//如果文件大小为0则删除文件
				if (File.Exists(ac.FilePath))
				{
					FileInfo fi = new FileInfo(ac.FilePath);
					if (fi.Length == 0)
						File.Delete(ac.FilePath);
				}
			}
			taskmanager.ContinueNext();
		}


		 /// <summary>
		 /// 刷新任务列表(功能为新建LSVITEM并更新)
		 /// </summary>
		public void RefreshTaskList(AcDowner ac)
		{
			//AcDowner ac = taskmanager.Tasks[taskmanager.Tasks.Count - 1];
			ListViewItem lsvItem = new ListViewItem();
			//添加SubItem
			lsvItem.SubItems.Add(ac.Status.ToString());
			lsvItem.SubItems.Add(ac.VideoTitle);
			lsvItem.SubItems.Add("");
			lsvItem.SubItems.Add("");
			lsvItem.SubItems.Add("");
			lsvItem.SubItems.Add(ac.Url);
			lsvItem.Tag = ac.TaskId.ToString();
			lsv.Items.Add(lsvItem);
			RefreshTaskItem(ac.TaskId);
		}

		 /// <summary>
		 /// 删除任务
		 /// </summary>
		 /// <param name="deleteFile">是否删除下载的文件</param>
		private void DeleteTask(bool deleteFile)
		{
			DialogResult dr;
			if (!deleteFile)
			{
				dr = MessageBox.Show("是否删除所选任务?", "删除任务", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			}
			else
			{
				dr = MessageBox.Show("是否删除所选任务并删除相应的视频以及字幕文件?", "删除任务", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			}
			if (dr == DialogResult.Yes)
			{
				//设置鼠标为忙碌
				this.Cursor = Cursors.WaitCursor;
				//删除所有任务
				foreach (ListViewItem item in lsv.SelectedItems)
				{
					Guid g = new Guid((string)item.Tag);
					AcDowner ac = GetTask(g);
					//停止任务
					ac.StopDownloadVideo();
					
					//删除lsv列表项
					lsv.Items.Remove(item);
					//删除文件
					try
					{
						if (deleteFile)
						{
							//删除视频文件
							if (File.Exists(ac.FilePath))
								File.Delete(ac.FilePath);
							//删除字幕文件
							if (File.Exists(ac.SubfilePathString))
								File.Delete(ac.SubfilePathString);
						}
					}
					catch { }

					//删除任务
					taskmanager.Tasks.Remove(ac);
				}
				//恢复鼠标指针
				this.Cursor = Cursors.Default;

			}



			
		}

#endregion























































		 //private void button1_Click(object sender, EventArgs e)
		  //{
		  //    using(BingService svr = new BingService())
		  //    {
		  //        SearchRequest r = new SearchRequest();
		  //        r.AppId = APP_ID;
		  //        r.Query=textBox2.Text ;
		  //        r.Sources = new SourceType[] { SourceType.Web };

		  //        r.UILanguage = "zh-CN";
		  //        r.Version = "2.0";
		  //        r.Market = "zh-CN";

		  //        SearchResponse res = svr.Search(r);
		  //        StringBuilder sb = new StringBuilder();
		  //        foreach (WebResult w in res.Web.Results)
		  //        {
		  //            sb.AppendLine(w.Title);
		  //            sb.AppendLine(w.Description);
		  //            sb.AppendLine(w.Url);
		  //            sb.AppendLine();
		  //        };
		  //        textBox1.Text = sb.ToString();
		  //     };




		
	 }


}
