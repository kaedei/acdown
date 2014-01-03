namespace Kaedei.AcDown.UI
{
	 partial class FormMain
	 {
		  /// <summary>
		  /// 必需的设计器变量。
		  /// </summary>
		  private System.ComponentModel.IContainer components = null;

		  /// <summary>
		  /// 清理所有正在使用的资源。
		  /// </summary>
		  /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		  protected override void Dispose(bool disposing)
		  {
				if (disposing && (components != null))
				{
					 components.Dispose();
				}
				base.Dispose(disposing);
		  }

		  #region Windows 窗体设计器生成的代码

		  /// <summary>
		  /// 设计器支持所需的方法 - 不要
		  /// 使用代码编辑器修改此方法的内容。
		  /// </summary>
		  private void InitializeComponent()
		  {
			this.components = new System.ComponentModel.Container();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.lblSpeed = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblBlank = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolQuestionnaire = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolHelpCenter = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.btnNew = new System.Windows.Forms.ToolStripButton();
			this.btnConfig = new System.Windows.Forms.ToolStripButton();
			this.btnPlugin = new System.Windows.Forms.ToolStripButton();
			this.btnAbout = new System.Windows.Forms.ToolStripButton();
			this.btnSearch = new System.Windows.Forms.ToolStripSplitButton();
			this.searchCustom = new System.Windows.Forms.ToolStripMenuItem();
			this.txtSearch = new System.Windows.Forms.ToolStripTextBox();
			this.toolUpdate = new System.Windows.Forms.ToolStripButton();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.mnuTray = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuTrayShowHide = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuTrayLine1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuTrayExit = new System.Windows.Forms.ToolStripMenuItem();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.btnSpeedlimitApply = new System.Windows.Forms.Button();
			this.cboAfterComplete = new System.Windows.Forms.ComboBox();
			this.udSpeedLimit = new System.Windows.Forms.NumericUpDown();
			this.contextTool = new System.Windows.Forms.ToolStrip();
			this.toolStart = new System.Windows.Forms.ToolStripButton();
			this.toolStop = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolDelete = new System.Windows.Forms.ToolStripSplitButton();
			this.toolCompletelyDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolCompetelyDeleteAndFile = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolOpenFolder = new System.Windows.Forms.ToolStripSplitButton();
			this.toolOpenUrl = new System.Windows.Forms.ToolStripMenuItem();
			this.toolAcPlay = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolMore = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolInfo = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolExportUrlList = new System.Windows.Forms.ToolStripMenuItem();
			this.toolCombineVideo = new System.Windows.Forms.ToolStripMenuItem();
			this.tabMain = new System.Windows.Forms.TabControl();
			this.tabDownload = new System.Windows.Forms.TabPage();
			this.splitContainerMain = new System.Windows.Forms.SplitContainer();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.rdoAll = new System.Windows.Forms.RadioButton();
			this.rdoDownloading = new System.Windows.Forms.RadioButton();
			this.rdoFinished = new System.Windows.Forms.RadioButton();
			this.rdoDeleted = new System.Windows.Forms.RadioButton();
			this.rdoSearch = new System.Windows.Forms.RadioButton();
			this.splitContainerTask = new System.Windows.Forms.SplitContainer();
			this.lsv = new System.Windows.Forms.ListView();
			this.headerStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.headerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.headerPart = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.headerProgress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.headerSpeed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.headerRemainTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.headerPastTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.headerSourceUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lnkAutoCombineSettings = new System.Windows.Forms.LinkLabel();
			this.chkAutoCombine = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tabFlvCombine = new System.Windows.Forms.TabPage();
			this.flvCombineControl1 = new Kaedei.AcDown.UI.Components.FlvCombineControl();
			this.tabAcPlay = new System.Windows.Forms.TabPage();
			this.acPlay = new Kaedei.AcDown.UI.Components.AcPlay2();
			this.tabFeedback = new System.Windows.Forms.TabPage();
			this.tabExample = new System.Windows.Forms.TabPage();
			this.txtExample = new System.Windows.Forms.TextBox();
			this.toolXml2Ass = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolDandanplay = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusStrip.SuspendLayout();
			this.toolStrip.SuspendLayout();
			this.mnuTray.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.udSpeedLimit)).BeginInit();
			this.contextTool.SuspendLayout();
			this.tabMain.SuspendLayout();
			this.tabDownload.SuspendLayout();
			this.splitContainerMain.Panel1.SuspendLayout();
			this.splitContainerMain.Panel2.SuspendLayout();
			this.splitContainerMain.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.splitContainerTask.Panel1.SuspendLayout();
			this.splitContainerTask.Panel2.SuspendLayout();
			this.splitContainerTask.SuspendLayout();
			this.tabFlvCombine.SuspendLayout();
			this.tabAcPlay.SuspendLayout();
			this.tabExample.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip
			// 
			this.statusStrip.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblSpeed,
            this.lblBlank,
            this.toolQuestionnaire,
            this.toolXml2Ass,
            this.toolDandanplay,
            this.toolHelpCenter});
			this.statusStrip.Location = new System.Drawing.Point(0, 515);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(772, 25);
			this.statusStrip.TabIndex = 6;
			// 
			// lblSpeed
			// 
			this.lblSpeed.Name = "lblSpeed";
			this.lblSpeed.Size = new System.Drawing.Size(47, 20);
			this.lblSpeed.Text = "0KB/s";
			// 
			// lblBlank
			// 
			this.lblBlank.Name = "lblBlank";
			this.lblBlank.Size = new System.Drawing.Size(174, 20);
			this.lblBlank.Spring = true;
			// 
			// toolQuestionnaire
			// 
			this.toolQuestionnaire.Image = global::Kaedei.AcDown.Properties.Resources.feedback;
			this.toolQuestionnaire.IsLink = true;
			this.toolQuestionnaire.Name = "toolQuestionnaire";
			this.toolQuestionnaire.Size = new System.Drawing.Size(151, 20);
			this.toolQuestionnaire.Text = "参与用户满意度调查";
			this.toolQuestionnaire.ToolTipText = "点击参与AcDown用户满意度及需求调查";
			this.toolQuestionnaire.Click += new System.EventHandler(this.toolQuestionnaire_Click);
			// 
			// toolHelpCenter
			// 
			this.toolHelpCenter.Image = global::Kaedei.AcDown.Properties.Resources.Help;
			this.toolHelpCenter.IsLink = true;
			this.toolHelpCenter.Name = "toolHelpCenter";
			this.toolHelpCenter.Size = new System.Drawing.Size(95, 20);
			this.toolHelpCenter.Text = "问题与反馈";
			this.toolHelpCenter.ToolTipText = "打开帮助中心";
			this.toolHelpCenter.Click += new System.EventHandler(this.toolHelpCenter_Click);
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(68, 18);
			this.toolStripStatusLabel1.Text = "全局速度：";
			// 
			// toolStrip
			// 
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnConfig,
            this.btnPlugin,
            this.btnAbout,
            this.btnSearch,
            this.txtSearch,
            this.toolUpdate});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(772, 31);
			this.toolStrip.TabIndex = 7;
			// 
			// btnNew
			// 
			this.btnNew.Image = global::Kaedei.AcDown.Properties.Resources.Add;
			this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(76, 28);
			this.btnNew.Text = "新建(&N)";
			this.btnNew.ToolTipText = "新建下载任务";
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// btnConfig
			// 
			this.btnConfig.Image = global::Kaedei.AcDown.Properties.Resources.Settings;
			this.btnConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnConfig.Name = "btnConfig";
			this.btnConfig.Size = new System.Drawing.Size(75, 28);
			this.btnConfig.Text = "设置(&C)";
			this.btnConfig.ToolTipText = "调整下载器设置";
			this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
			// 
			// btnPlugin
			// 
			this.btnPlugin.Image = global::Kaedei.AcDown.Properties.Resources.Plugin;
			this.btnPlugin.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnPlugin.Name = "btnPlugin";
			this.btnPlugin.Size = new System.Drawing.Size(74, 28);
			this.btnPlugin.Text = "插件(&P)";
			this.btnPlugin.Click += new System.EventHandler(this.btnPlugin_Click);
			// 
			// btnAbout
			// 
			this.btnAbout.Image = global::Kaedei.AcDown.Properties.Resources.About;
			this.btnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnAbout.Name = "btnAbout";
			this.btnAbout.Size = new System.Drawing.Size(75, 28);
			this.btnAbout.Text = "关于(&A)";
			this.btnAbout.ToolTipText = "关于AcDown动漫下载器";
			this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
			// 
			// btnSearch
			// 
			this.btnSearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnSearch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchCustom});
			this.btnSearch.Image = global::Kaedei.AcDown.Properties.Resources.bing;
			this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(40, 28);
			this.btnSearch.Text = "点击搜索";
			this.btnSearch.ButtonClick += new System.EventHandler(this.btnSearch_ButtonClick);
			// 
			// searchCustom
			// 
			this.searchCustom.Name = "searchCustom";
			this.searchCustom.Size = new System.Drawing.Size(167, 22);
			this.searchCustom.Text = "自定义搜索引擎...";
			this.searchCustom.Click += new System.EventHandler(this.searchCustom_Click);
			// 
			// txtSearch
			// 
			this.txtSearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.txtSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.txtSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.txtSearch.AutoToolTip = true;
			this.txtSearch.MaxLength = 20;
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(120, 31);
			this.txtSearch.ToolTipText = "请键入搜索关键词";
			this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
			this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyUp);
			this.txtSearch.Click += new System.EventHandler(this.txtSearch_Click);
			// 
			// toolUpdate
			// 
			this.toolUpdate.ForeColor = System.Drawing.Color.Crimson;
			this.toolUpdate.Image = global::Kaedei.AcDown.Properties.Resources.Update;
			this.toolUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolUpdate.Name = "toolUpdate";
			this.toolUpdate.Size = new System.Drawing.Size(104, 28);
			this.toolUpdate.Text = "更新AcDown";
			this.toolUpdate.ToolTipText = "您使用的版本并非最新版本，\r\n点击此按钮进行自动更新";
			this.toolUpdate.Click += new System.EventHandler(this.toolUpdate_Click);
			// 
			// notifyIcon
			// 
			this.notifyIcon.ContextMenuStrip = this.mnuTray;
			this.notifyIcon.Text = "AcDown动漫下载器";
			this.notifyIcon.Visible = true;
			this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
			// 
			// mnuTray
			// 
			this.mnuTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTrayShowHide,
            this.mnuTrayLine1,
            this.mnuTrayExit});
			this.mnuTray.Name = "mnuTray";
			this.mnuTray.Size = new System.Drawing.Size(172, 54);
			this.mnuTray.Opening += new System.ComponentModel.CancelEventHandler(this.mnuTray_Opening);
			// 
			// mnuTrayShowHide
			// 
			this.mnuTrayShowHide.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.mnuTrayShowHide.Name = "mnuTrayShowHide";
			this.mnuTrayShowHide.Size = new System.Drawing.Size(171, 22);
			this.mnuTrayShowHide.Text = "显示/隐藏主窗口";
			this.mnuTrayShowHide.Click += new System.EventHandler(this.mnuTrayShowHide_Click);
			// 
			// mnuTrayLine1
			// 
			this.mnuTrayLine1.Name = "mnuTrayLine1";
			this.mnuTrayLine1.Size = new System.Drawing.Size(168, 6);
			// 
			// mnuTrayExit
			// 
			this.mnuTrayExit.Name = "mnuTrayExit";
			this.mnuTrayExit.Size = new System.Drawing.Size(171, 22);
			this.mnuTrayExit.Text = "退出";
			this.mnuTrayExit.Click += new System.EventHandler(this.mnuTrayExit_Click);
			// 
			// timer
			// 
			this.timer.Enabled = true;
			this.timer.Interval = 2000;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(24, 24);
			this.toolStripButton1.Text = "toolStripButton1";
			// 
			// toolTip
			// 
			this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.toolTip.ToolTipTitle = "提示";
			// 
			// btnSpeedlimitApply
			// 
			this.btnSpeedlimitApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSpeedlimitApply.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSpeedlimitApply.Location = new System.Drawing.Point(188, 12);
			this.btnSpeedlimitApply.Name = "btnSpeedlimitApply";
			this.btnSpeedlimitApply.Size = new System.Drawing.Size(64, 26);
			this.btnSpeedlimitApply.TabIndex = 31;
			this.btnSpeedlimitApply.Text = "生效";
			this.toolTip.SetToolTip(this.btnSpeedlimitApply, "点击使速度限制生效，设置为0可以取消限速。");
			this.btnSpeedlimitApply.UseVisualStyleBackColor = true;
			this.btnSpeedlimitApply.Click += new System.EventHandler(this.btnSpeedlimitApply_Click);
			// 
			// cboAfterComplete
			// 
			this.cboAfterComplete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cboAfterComplete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAfterComplete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cboAfterComplete.FormattingEnabled = true;
			this.cboAfterComplete.Items.AddRange(new object[] {
            "无动作",
            "关机",
            "待机",
            "休眠",
            "注销",
            "重新启动",
            "关闭程序"});
			this.cboAfterComplete.Location = new System.Drawing.Point(400, 11);
			this.cboAfterComplete.Name = "cboAfterComplete";
			this.cboAfterComplete.Size = new System.Drawing.Size(116, 28);
			this.cboAfterComplete.TabIndex = 27;
			this.toolTip.SetToolTip(this.cboAfterComplete, "全部下载完成后执行的动作");
			// 
			// udSpeedLimit
			// 
			this.udSpeedLimit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.udSpeedLimit.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.udSpeedLimit.Location = new System.Drawing.Point(114, 12);
			this.udSpeedLimit.Maximum = new decimal(new int[] {
            40960,
            0,
            0,
            0});
			this.udSpeedLimit.Name = "udSpeedLimit";
			this.udSpeedLimit.Size = new System.Drawing.Size(68, 26);
			this.udSpeedLimit.TabIndex = 29;
			this.udSpeedLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.toolTip.SetToolTip(this.udSpeedLimit, "设置速度限制");
			// 
			// contextTool
			// 
			this.contextTool.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.contextTool.Dock = System.Windows.Forms.DockStyle.None;
			this.contextTool.GripMargin = new System.Windows.Forms.Padding(4, 2, 2, 2);
			this.contextTool.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.contextTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStart,
            this.toolStop,
            this.toolStripSeparator1,
            this.toolDelete,
            this.toolStripSeparator2,
            this.toolOpenFolder,
            this.toolAcPlay,
            this.toolStripSeparator3,
            this.toolMore});
			this.contextTool.Location = new System.Drawing.Point(32, 68);
			this.contextTool.Name = "contextTool";
			this.contextTool.Size = new System.Drawing.Size(419, 46);
			this.contextTool.TabIndex = 22;
			this.contextTool.Visible = false;
			// 
			// toolStart
			// 
			this.toolStart.Image = global::Kaedei.AcDown.Properties.Resources.ToolstripStart;
			this.toolStart.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStart.Name = "toolStart";
			this.toolStart.Size = new System.Drawing.Size(59, 43);
			this.toolStart.Text = "开始下载";
			this.toolStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolStart.ToolTipText = "开始/重新下载选定的任务";
			this.toolStart.Click += new System.EventHandler(this.mnuConStart_Click);
			// 
			// toolStop
			// 
			this.toolStop.Image = global::Kaedei.AcDown.Properties.Resources.ToolstripStop;
			this.toolStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStop.Name = "toolStop";
			this.toolStop.Size = new System.Drawing.Size(59, 43);
			this.toolStop.Text = "停止下载";
			this.toolStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolStop.ToolTipText = "停止下载选定的任务";
			this.toolStop.Click += new System.EventHandler(this.mnuConStop_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 46);
			// 
			// toolDelete
			// 
			this.toolDelete.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolCompletelyDelete,
            this.toolCompetelyDeleteAndFile});
			this.toolDelete.Image = global::Kaedei.AcDown.Properties.Resources.ToolstripDelete;
			this.toolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolDelete.Name = "toolDelete";
			this.toolDelete.Size = new System.Drawing.Size(71, 43);
			this.toolDelete.Text = "删除任务";
			this.toolDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolDelete.ToolTipText = "删除选定的任务至回收站";
			this.toolDelete.ButtonClick += new System.EventHandler(this.mnuConDelete_Click);
			// 
			// toolCompletelyDelete
			// 
			this.toolCompletelyDelete.Name = "toolCompletelyDelete";
			this.toolCompletelyDelete.Size = new System.Drawing.Size(206, 22);
			this.toolCompletelyDelete.Text = "彻底删除任务";
			this.toolCompletelyDelete.Click += new System.EventHandler(this.toolCompletelyDelete_Click);
			// 
			// toolCompetelyDeleteAndFile
			// 
			this.toolCompetelyDeleteAndFile.Name = "toolCompetelyDeleteAndFile";
			this.toolCompetelyDeleteAndFile.Size = new System.Drawing.Size(206, 22);
			this.toolCompetelyDeleteAndFile.Text = "彻底删除任务并删除文件";
			this.toolCompetelyDeleteAndFile.Click += new System.EventHandler(this.mnuConDeleteAndFile_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 46);
			// 
			// toolOpenFolder
			// 
			this.toolOpenFolder.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolOpenUrl});
			this.toolOpenFolder.Image = global::Kaedei.AcDown.Properties.Resources.ToolstripOpenFolder;
			this.toolOpenFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolOpenFolder.Name = "toolOpenFolder";
			this.toolOpenFolder.Size = new System.Drawing.Size(71, 43);
			this.toolOpenFolder.Text = "打开目录";
			this.toolOpenFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolOpenFolder.ToolTipText = "打开下载文件所在文件夹";
			this.toolOpenFolder.ButtonClick += new System.EventHandler(this.mnuConOpenFolder_Click);
			// 
			// toolOpenUrl
			// 
			this.toolOpenUrl.Image = global::Kaedei.AcDown.Properties.Resources.ToolstripOpenWebpage;
			this.toolOpenUrl.Name = "toolOpenUrl";
			this.toolOpenUrl.Size = new System.Drawing.Size(154, 30);
			this.toolOpenUrl.Text = "打开原始网页";
			this.toolOpenUrl.ToolTipText = "打开任务所引用的网页";
			this.toolOpenUrl.Click += new System.EventHandler(this.mnuConOpenUrl_Click);
			// 
			// toolAcPlay
			// 
			this.toolAcPlay.Image = global::Kaedei.AcDown.Properties.Resources.ToolstripAcPlay;
			this.toolAcPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolAcPlay.Name = "toolAcPlay";
			this.toolAcPlay.Size = new System.Drawing.Size(59, 43);
			this.toolAcPlay.Text = "弹幕播放";
			this.toolAcPlay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolAcPlay.ToolTipText = "使用AcPlay播放视频和弹幕";
			this.toolAcPlay.Click += new System.EventHandler(this.mnuConAcPlay_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 46);
			// 
			// toolMore
			// 
			this.toolMore.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolInfo,
            this.toolStripMenuItem1,
            this.toolExportUrlList,
            this.toolCombineVideo});
			this.toolMore.Image = global::Kaedei.AcDown.Properties.Resources.ToolStripControl;
			this.toolMore.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolMore.Name = "toolMore";
			this.toolMore.Size = new System.Drawing.Size(68, 43);
			this.toolMore.Text = "更多操作";
			this.toolMore.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			// 
			// toolInfo
			// 
			this.toolInfo.Image = global::Kaedei.AcDown.Properties.Resources.ToolstripInfo;
			this.toolInfo.Name = "toolInfo";
			this.toolInfo.Size = new System.Drawing.Size(187, 30);
			this.toolInfo.Text = "查看任务详细信息...";
			this.toolInfo.Click += new System.EventHandler(this.mnuConInfo_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(184, 6);
			// 
			// toolExportUrlList
			// 
			this.toolExportUrlList.Name = "toolExportUrlList";
			this.toolExportUrlList.Size = new System.Drawing.Size(187, 30);
			this.toolExportUrlList.Text = "导出地址列表...";
			this.toolExportUrlList.Click += new System.EventHandler(this.mnuConExportUrlList_Click);
			// 
			// toolCombineVideo
			// 
			this.toolCombineVideo.Name = "toolCombineVideo";
			this.toolCombineVideo.Size = new System.Drawing.Size(187, 30);
			this.toolCombineVideo.Text = "合并分段视频";
			this.toolCombineVideo.Click += new System.EventHandler(this.toolCombineVideo_Click);
			// 
			// tabMain
			// 
			this.tabMain.AllowDrop = true;
			this.tabMain.Controls.Add(this.tabDownload);
			this.tabMain.Controls.Add(this.tabFlvCombine);
			this.tabMain.Controls.Add(this.tabAcPlay);
			this.tabMain.Controls.Add(this.tabFeedback);
			this.tabMain.Controls.Add(this.tabExample);
			this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabMain.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tabMain.HotTrack = true;
			this.tabMain.Location = new System.Drawing.Point(0, 31);
			this.tabMain.Multiline = true;
			this.tabMain.Name = "tabMain";
			this.tabMain.SelectedIndex = 0;
			this.tabMain.Size = new System.Drawing.Size(772, 484);
			this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tabMain.TabIndex = 23;
			// 
			// tabDownload
			// 
			this.tabDownload.Controls.Add(this.splitContainerMain);
			this.tabDownload.Location = new System.Drawing.Point(4, 29);
			this.tabDownload.Name = "tabDownload";
			this.tabDownload.Padding = new System.Windows.Forms.Padding(3);
			this.tabDownload.Size = new System.Drawing.Size(764, 451);
			this.tabDownload.TabIndex = 0;
			this.tabDownload.Text = "下载";
			this.tabDownload.UseVisualStyleBackColor = true;
			// 
			// splitContainerMain
			// 
			this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainerMain.IsSplitterFixed = true;
			this.splitContainerMain.Location = new System.Drawing.Point(3, 3);
			this.splitContainerMain.Name = "splitContainerMain";
			// 
			// splitContainerMain.Panel1
			// 
			this.splitContainerMain.Panel1.Controls.Add(this.flowLayoutPanel1);
			// 
			// splitContainerMain.Panel2
			// 
			this.splitContainerMain.Panel2.Controls.Add(this.splitContainerTask);
			this.splitContainerMain.Size = new System.Drawing.Size(758, 445);
			this.splitContainerMain.SplitterDistance = 91;
			this.splitContainerMain.TabIndex = 35;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.rdoAll);
			this.flowLayoutPanel1.Controls.Add(this.rdoDownloading);
			this.flowLayoutPanel1.Controls.Add(this.rdoFinished);
			this.flowLayoutPanel1.Controls.Add(this.rdoDeleted);
			this.flowLayoutPanel1.Controls.Add(this.rdoSearch);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(91, 445);
			this.flowLayoutPanel1.TabIndex = 32;
			// 
			// rdoAll
			// 
			this.rdoAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rdoAll.Appearance = System.Windows.Forms.Appearance.Button;
			this.rdoAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.rdoAll.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkGray;
			this.rdoAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
			this.rdoAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
			this.rdoAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rdoAll.Location = new System.Drawing.Point(3, 3);
			this.rdoAll.Name = "rdoAll";
			this.rdoAll.Size = new System.Drawing.Size(82, 33);
			this.rdoAll.TabIndex = 27;
			this.rdoAll.Tag = "状态:";
			this.rdoAll.Text = "所有任务";
			this.rdoAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.rdoAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.rdoAll.UseVisualStyleBackColor = true;
			this.rdoAll.CheckedChanged += new System.EventHandler(this.rdo_CheckedChanged);
			// 
			// rdoDownloading
			// 
			this.rdoDownloading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rdoDownloading.Appearance = System.Windows.Forms.Appearance.Button;
			this.rdoDownloading.Checked = true;
			this.rdoDownloading.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.rdoDownloading.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkGray;
			this.rdoDownloading.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
			this.rdoDownloading.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
			this.rdoDownloading.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rdoDownloading.Location = new System.Drawing.Point(3, 42);
			this.rdoDownloading.Name = "rdoDownloading";
			this.rdoDownloading.Size = new System.Drawing.Size(82, 33);
			this.rdoDownloading.TabIndex = 23;
			this.rdoDownloading.TabStop = true;
			this.rdoDownloading.Tag = "状态:正在下载|状态:等待开始|状态:正在停止|状态:出现错误|状态:已经停止";
			this.rdoDownloading.Text = "正在下载";
			this.rdoDownloading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.rdoDownloading.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.rdoDownloading.UseVisualStyleBackColor = true;
			this.rdoDownloading.CheckedChanged += new System.EventHandler(this.rdo_CheckedChanged);
			// 
			// rdoFinished
			// 
			this.rdoFinished.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rdoFinished.Appearance = System.Windows.Forms.Appearance.Button;
			this.rdoFinished.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.rdoFinished.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkGray;
			this.rdoFinished.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
			this.rdoFinished.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
			this.rdoFinished.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rdoFinished.Location = new System.Drawing.Point(3, 81);
			this.rdoFinished.Name = "rdoFinished";
			this.rdoFinished.Size = new System.Drawing.Size(82, 33);
			this.rdoFinished.TabIndex = 24;
			this.rdoFinished.Tag = "状态:下载完成|状态:部分完成";
			this.rdoFinished.Text = "已完成";
			this.rdoFinished.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.rdoFinished.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.rdoFinished.UseVisualStyleBackColor = true;
			this.rdoFinished.CheckedChanged += new System.EventHandler(this.rdo_CheckedChanged);
			// 
			// rdoDeleted
			// 
			this.rdoDeleted.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rdoDeleted.Appearance = System.Windows.Forms.Appearance.Button;
			this.rdoDeleted.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.rdoDeleted.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkGray;
			this.rdoDeleted.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
			this.rdoDeleted.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
			this.rdoDeleted.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rdoDeleted.Location = new System.Drawing.Point(3, 120);
			this.rdoDeleted.Name = "rdoDeleted";
			this.rdoDeleted.Size = new System.Drawing.Size(82, 33);
			this.rdoDeleted.TabIndex = 25;
			this.rdoDeleted.Tag = "状态:已删除";
			this.rdoDeleted.Text = "回收站";
			this.rdoDeleted.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.rdoDeleted.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.rdoDeleted.UseVisualStyleBackColor = true;
			this.rdoDeleted.CheckedChanged += new System.EventHandler(this.rdo_CheckedChanged);
			// 
			// rdoSearch
			// 
			this.rdoSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rdoSearch.Appearance = System.Windows.Forms.Appearance.Button;
			this.rdoSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.rdoSearch.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkGray;
			this.rdoSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
			this.rdoSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
			this.rdoSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.flowLayoutPanel1.SetFlowBreak(this.rdoSearch, true);
			this.rdoSearch.Image = global::Kaedei.AcDown.Properties.Resources.bing;
			this.rdoSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.rdoSearch.Location = new System.Drawing.Point(3, 159);
			this.rdoSearch.Name = "rdoSearch";
			this.rdoSearch.Size = new System.Drawing.Size(82, 33);
			this.rdoSearch.TabIndex = 26;
			this.rdoSearch.Tag = "CustomSearch";
			this.rdoSearch.Text = "搜索";
			this.rdoSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.rdoSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.rdoSearch.UseVisualStyleBackColor = true;
			this.rdoSearch.CheckedChanged += new System.EventHandler(this.rdo_CheckedChanged);
			// 
			// splitContainerTask
			// 
			this.splitContainerTask.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerTask.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainerTask.Location = new System.Drawing.Point(0, 0);
			this.splitContainerTask.Name = "splitContainerTask";
			this.splitContainerTask.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerTask.Panel1
			// 
			this.splitContainerTask.Panel1.Controls.Add(this.contextTool);
			this.splitContainerTask.Panel1.Controls.Add(this.lsv);
			// 
			// splitContainerTask.Panel2
			// 
			this.splitContainerTask.Panel2.Controls.Add(this.lnkAutoCombineSettings);
			this.splitContainerTask.Panel2.Controls.Add(this.chkAutoCombine);
			this.splitContainerTask.Panel2.Controls.Add(this.udSpeedLimit);
			this.splitContainerTask.Panel2.Controls.Add(this.label2);
			this.splitContainerTask.Panel2.Controls.Add(this.btnSpeedlimitApply);
			this.splitContainerTask.Panel2.Controls.Add(this.label1);
			this.splitContainerTask.Panel2.Controls.Add(this.cboAfterComplete);
			this.splitContainerTask.Size = new System.Drawing.Size(663, 445);
			this.splitContainerTask.SplitterDistance = 364;
			this.splitContainerTask.TabIndex = 0;
			// 
			// lsv
			// 
			this.lsv.AllowColumnReorder = true;
			this.lsv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.headerStatus,
            this.headerName,
            this.headerPart,
            this.headerProgress,
            this.headerSpeed,
            this.headerRemainTime,
            this.headerPastTime,
            this.headerSourceUrl});
			this.lsv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lsv.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lsv.FullRowSelect = true;
			this.lsv.Location = new System.Drawing.Point(0, 0);
			this.lsv.Name = "lsv";
			this.lsv.ShowItemToolTips = true;
			this.lsv.Size = new System.Drawing.Size(663, 364);
			this.lsv.TabIndex = 0;
			this.lsv.UseCompatibleStateImageBehavior = false;
			this.lsv.View = System.Windows.Forms.View.Details;
			this.lsv.SelectedIndexChanged += new System.EventHandler(this.lsv_SelectedIndexChanged);
			this.lsv.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lsv_KeyUp);
			// 
			// headerStatus
			// 
			this.headerStatus.Tag = "Status";
			this.headerStatus.Text = "状态";
			this.headerStatus.Width = 80;
			// 
			// headerName
			// 
			this.headerName.Tag = "Name";
			this.headerName.Text = "名称";
			this.headerName.Width = 275;
			// 
			// headerPart
			// 
			this.headerPart.Tag = "Part";
			this.headerPart.Text = "分段";
			this.headerPart.Width = 69;
			// 
			// headerProgress
			// 
			this.headerProgress.Tag = "Progress";
			this.headerProgress.Text = "下载进度(当前分段)";
			this.headerProgress.Width = 85;
			// 
			// headerSpeed
			// 
			this.headerSpeed.Tag = "Speed";
			this.headerSpeed.Text = "下载速度";
			this.headerSpeed.Width = 80;
			// 
			// headerRemainTime
			// 
			this.headerRemainTime.Tag = "RemainTime";
			this.headerRemainTime.Text = "剩余时间(当前分段)";
			this.headerRemainTime.Width = 160;
			// 
			// headerPastTime
			// 
			this.headerPastTime.Tag = "PastTime";
			this.headerPastTime.Text = "已用时间";
			this.headerPastTime.Width = 160;
			// 
			// headerSourceUrl
			// 
			this.headerSourceUrl.Tag = "SourceUrl";
			this.headerSourceUrl.Text = "源地址";
			this.headerSourceUrl.Width = 500;
			// 
			// lnkAutoCombineSettings
			// 
			this.lnkAutoCombineSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lnkAutoCombineSettings.AutoSize = true;
			this.lnkAutoCombineSettings.Location = new System.Drawing.Point(275, 46);
			this.lnkAutoCombineSettings.Name = "lnkAutoCombineSettings";
			this.lnkAutoCombineSettings.Size = new System.Drawing.Size(47, 20);
			this.lnkAutoCombineSettings.TabIndex = 33;
			this.lnkAutoCombineSettings.TabStop = true;
			this.lnkAutoCombineSettings.Text = "[设置]";
			this.lnkAutoCombineSettings.Visible = false;
			// 
			// chkAutoCombine
			// 
			this.chkAutoCombine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chkAutoCombine.AutoSize = true;
			this.chkAutoCombine.Location = new System.Drawing.Point(7, 45);
			this.chkAutoCombine.Name = "chkAutoCombine";
			this.chkAutoCombine.Size = new System.Drawing.Size(262, 24);
			this.chkAutoCombine.TabIndex = 32;
			this.chkAutoCombine.Text = "下载成功后自动合并分段视频(测试版)";
			this.chkAutoCombine.UseVisualStyleBackColor = true;
			this.chkAutoCombine.CheckedChanged += new System.EventHandler(this.chkAutoCombine_CheckedChanged);
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(105, 20);
			this.label2.TabIndex = 30;
			this.label2.Text = "速度限制(KB/s)";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(284, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(110, 20);
			this.label1.TabIndex = 28;
			this.label1.Text = "全部下载完成后:";
			// 
			// tabFlvCombine
			// 
			this.tabFlvCombine.Controls.Add(this.flvCombineControl1);
			this.tabFlvCombine.Location = new System.Drawing.Point(4, 29);
			this.tabFlvCombine.Name = "tabFlvCombine";
			this.tabFlvCombine.Padding = new System.Windows.Forms.Padding(3);
			this.tabFlvCombine.Size = new System.Drawing.Size(764, 451);
			this.tabFlvCombine.TabIndex = 3;
			this.tabFlvCombine.Text = "视频合并";
			this.tabFlvCombine.UseVisualStyleBackColor = true;
			// 
			// flvCombineControl1
			// 
			this.flvCombineControl1.AutoScroll = true;
			this.flvCombineControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flvCombineControl1.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.flvCombineControl1.Location = new System.Drawing.Point(3, 3);
			this.flvCombineControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.flvCombineControl1.Name = "flvCombineControl1";
			this.flvCombineControl1.Size = new System.Drawing.Size(758, 445);
			this.flvCombineControl1.TabIndex = 0;
			// 
			// tabAcPlay
			// 
			this.tabAcPlay.AllowDrop = true;
			this.tabAcPlay.Controls.Add(this.acPlay);
			this.tabAcPlay.Location = new System.Drawing.Point(4, 29);
			this.tabAcPlay.Name = "tabAcPlay";
			this.tabAcPlay.Padding = new System.Windows.Forms.Padding(3);
			this.tabAcPlay.Size = new System.Drawing.Size(764, 451);
			this.tabAcPlay.TabIndex = 4;
			this.tabAcPlay.Text = "弹幕播放";
			this.tabAcPlay.UseVisualStyleBackColor = true;
			// 
			// acPlay
			// 
			this.acPlay.AllowDrop = true;
			this.acPlay.Dock = System.Windows.Forms.DockStyle.Fill;
			this.acPlay.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.acPlay.Location = new System.Drawing.Point(3, 3);
			this.acPlay.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.acPlay.Name = "acPlay";
			this.acPlay.Size = new System.Drawing.Size(758, 445);
			this.acPlay.TabIndex = 0;
			// 
			// tabFeedback
			// 
			this.tabFeedback.Location = new System.Drawing.Point(4, 29);
			this.tabFeedback.Name = "tabFeedback";
			this.tabFeedback.Padding = new System.Windows.Forms.Padding(3);
			this.tabFeedback.Size = new System.Drawing.Size(764, 451);
			this.tabFeedback.TabIndex = 6;
			this.tabFeedback.Text = "新鲜事";
			this.tabFeedback.UseVisualStyleBackColor = true;
			// 
			// tabExample
			// 
			this.tabExample.Controls.Add(this.txtExample);
			this.tabExample.Location = new System.Drawing.Point(4, 29);
			this.tabExample.Name = "tabExample";
			this.tabExample.Padding = new System.Windows.Forms.Padding(3);
			this.tabExample.Size = new System.Drawing.Size(764, 451);
			this.tabExample.TabIndex = 5;
			this.tabExample.Text = "示例网址";
			this.tabExample.UseVisualStyleBackColor = true;
			// 
			// txtExample
			// 
			this.txtExample.BackColor = System.Drawing.SystemColors.Window;
			this.txtExample.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtExample.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtExample.Location = new System.Drawing.Point(3, 3);
			this.txtExample.Multiline = true;
			this.txtExample.Name = "txtExample";
			this.txtExample.ReadOnly = true;
			this.txtExample.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtExample.Size = new System.Drawing.Size(758, 445);
			this.txtExample.TabIndex = 0;
			// 
			// toolXml2Ass
			// 
			this.toolXml2Ass.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolXml2Ass.IsLink = true;
			this.toolXml2Ass.Name = "toolXml2Ass";
			this.toolXml2Ass.Size = new System.Drawing.Size(140, 20);
			this.toolXml2Ass.Text = "★弹幕转Ass字幕工具";
			this.toolXml2Ass.Click += new System.EventHandler(this.toolXml2Ass_Click);
			// 
			// toolDandanplay
			// 
			this.toolDandanplay.IsLink = true;
			this.toolDandanplay.Name = "toolDandanplay";
			this.toolDandanplay.Size = new System.Drawing.Size(133, 20);
			this.toolDandanplay.Text = "☆全格式弹幕播放器";
			this.toolDandanplay.Click += new System.EventHandler(this.toolDandanplay_Click);
			// 
			// FormMain
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(772, 540);
			this.Controls.Add(this.tabMain);
			this.Controls.Add(this.toolStrip);
			this.Controls.Add(this.statusStrip);
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(666, 333);
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AcDown动漫下载器";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.ResizeEnd += new System.EventHandler(this.FormMain_ResizeEnd);
			this.VisibleChanged += new System.EventHandler(this.FormMain_VisibleChanged);
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.mnuTray.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.udSpeedLimit)).EndInit();
			this.contextTool.ResumeLayout(false);
			this.contextTool.PerformLayout();
			this.tabMain.ResumeLayout(false);
			this.tabDownload.ResumeLayout(false);
			this.splitContainerMain.Panel1.ResumeLayout(false);
			this.splitContainerMain.Panel2.ResumeLayout(false);
			this.splitContainerMain.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.splitContainerTask.Panel1.ResumeLayout(false);
			this.splitContainerTask.Panel1.PerformLayout();
			this.splitContainerTask.Panel2.ResumeLayout(false);
			this.splitContainerTask.Panel2.PerformLayout();
			this.splitContainerTask.ResumeLayout(false);
			this.tabFlvCombine.ResumeLayout(false);
			this.tabAcPlay.ResumeLayout(false);
			this.tabExample.ResumeLayout(false);
			this.tabExample.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		  }

		  #endregion

		  private System.Windows.Forms.StatusStrip statusStrip;
		  private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		  private System.Windows.Forms.ToolStripStatusLabel lblSpeed;
		  private System.Windows.Forms.ToolStripStatusLabel lblBlank;
		  private System.Windows.Forms.ToolStrip toolStrip;
		  private System.Windows.Forms.ToolStripTextBox txtSearch;
		  private System.Windows.Forms.NotifyIcon notifyIcon;
		  private System.Windows.Forms.Timer timer;
		  private System.Windows.Forms.ToolStripSplitButton btnSearch;
		  private System.Windows.Forms.ToolStripMenuItem searchCustom;
		  private System.Windows.Forms.ToolStripButton toolStripButton1;
		  private System.Windows.Forms.ContextMenuStrip mnuTray;
		  private System.Windows.Forms.ToolStripMenuItem mnuTrayShowHide;
		  private System.Windows.Forms.ToolStripSeparator mnuTrayLine1;
		  private System.Windows.Forms.ToolStripMenuItem mnuTrayExit;
		  private System.Windows.Forms.ToolTip toolTip;
		  private System.Windows.Forms.ToolStrip contextTool;
		  private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		  private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		  private System.Windows.Forms.TabControl tabMain;
		  private System.Windows.Forms.TabPage tabDownload;
		  private System.Windows.Forms.TabPage tabFlvCombine;
		  private Kaedei.AcDown.UI.Components.FlvCombineControl flvCombineControl1;
		  private System.Windows.Forms.ToolStripButton btnNew;
		  private System.Windows.Forms.ToolStripButton btnConfig;
		  private System.Windows.Forms.ToolStripButton btnAbout;
		  private System.Windows.Forms.ToolStripSplitButton toolDelete;
		  private System.Windows.Forms.ToolStripMenuItem toolCompetelyDeleteAndFile;
		  private System.Windows.Forms.ToolStripButton toolUpdate;
		  private System.Windows.Forms.ToolStripSplitButton toolOpenFolder;
		  private System.Windows.Forms.ToolStripMenuItem toolOpenUrl;
		  private System.Windows.Forms.TabPage tabAcPlay;
		  private System.Windows.Forms.ToolStripStatusLabel toolQuestionnaire;
		  private System.Windows.Forms.TabPage tabExample;
		  private System.Windows.Forms.TextBox txtExample;
		  private System.Windows.Forms.ToolStripButton toolStart;
		  private System.Windows.Forms.ToolStripButton toolStop;
		  private Components.AcPlay2 acPlay;
		  private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		  private System.Windows.Forms.ToolStripDropDownButton toolMore;
		  private System.Windows.Forms.ToolStripMenuItem toolExportUrlList;
		  private System.Windows.Forms.ToolStripMenuItem toolCompletelyDelete;
		  private System.Windows.Forms.ToolStripButton btnPlugin;
		  private System.Windows.Forms.ToolStripStatusLabel toolHelpCenter;
		  private System.Windows.Forms.ToolStripButton toolAcPlay;
		  private System.Windows.Forms.ToolStripMenuItem toolInfo;
		  private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		  private System.Windows.Forms.TabPage tabFeedback;
		  private System.Windows.Forms.SplitContainer splitContainerMain;
		  private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		  private System.Windows.Forms.RadioButton rdoAll;
		  private System.Windows.Forms.RadioButton rdoDownloading;
		  private System.Windows.Forms.RadioButton rdoFinished;
		  private System.Windows.Forms.RadioButton rdoDeleted;
		  private System.Windows.Forms.RadioButton rdoSearch;
		  private System.Windows.Forms.SplitContainer splitContainerTask;
		  private System.Windows.Forms.ListView lsv;
		  private System.Windows.Forms.ColumnHeader headerStatus;
		  private System.Windows.Forms.ColumnHeader headerName;
		  private System.Windows.Forms.ColumnHeader headerPart;
		  private System.Windows.Forms.ColumnHeader headerProgress;
		  private System.Windows.Forms.ColumnHeader headerSpeed;
		  private System.Windows.Forms.ColumnHeader headerRemainTime;
		  private System.Windows.Forms.ColumnHeader headerPastTime;
		  private System.Windows.Forms.ColumnHeader headerSourceUrl;
		  private System.Windows.Forms.NumericUpDown udSpeedLimit;
		  private System.Windows.Forms.Label label2;
		  private System.Windows.Forms.Button btnSpeedlimitApply;
		  private System.Windows.Forms.Label label1;
		  private System.Windows.Forms.ComboBox cboAfterComplete;
		  private System.Windows.Forms.LinkLabel lnkAutoCombineSettings;
		  private System.Windows.Forms.CheckBox chkAutoCombine;
		  private System.Windows.Forms.ToolStripMenuItem toolCombineVideo;
		  private System.Windows.Forms.ToolStripStatusLabel toolXml2Ass;
		  private System.Windows.Forms.ToolStripStatusLabel toolDandanplay;

	 }
}

