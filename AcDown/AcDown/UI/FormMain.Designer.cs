namespace AcDown.UI
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
           this.lsv = new System.Windows.Forms.ListView();
           this.headerStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
           this.headerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
           this.headerPart = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
           this.headerProgress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
           this.headerSpeed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
           this.headerRemainTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
           this.headerPastTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
           this.headerSourceUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
           this.picLogo = new System.Windows.Forms.PictureBox();
           this.statusStrip = new System.Windows.Forms.StatusStrip();
           this.lblSpeed = new System.Windows.Forms.ToolStripStatusLabel();
           this.lblBlank = new System.Windows.Forms.ToolStripStatusLabel();
           this.toolHelpCenter = new System.Windows.Forms.ToolStripStatusLabel();
           this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
           this.toolStrip = new System.Windows.Forms.ToolStrip();
           this.btnSearch = new System.Windows.Forms.ToolStripSplitButton();
           this.searchCustom = new System.Windows.Forms.ToolStripMenuItem();
           this.txtSearch = new System.Windows.Forms.ToolStripTextBox();
           this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
           this.mnuTray = new System.Windows.Forms.ContextMenuStrip(this.components);
           this.mnuTrayShowHide = new System.Windows.Forms.ToolStripMenuItem();
           this.mnuTrayLine1 = new System.Windows.Forms.ToolStripSeparator();
           this.mnuTrayExit = new System.Windows.Forms.ToolStripMenuItem();
           this.btnClickNew = new System.Windows.Forms.Button();
           this.timer = new System.Windows.Forms.Timer(this.components);
           this.cboAfterComplete = new System.Windows.Forms.ComboBox();
           this.label1 = new System.Windows.Forms.Label();
           this.udSpeedLimit = new System.Windows.Forms.NumericUpDown();
           this.label2 = new System.Windows.Forms.Label();
           this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
           this.toolTip = new System.Windows.Forms.ToolTip(this.components);
           this.contextTool = new System.Windows.Forms.ToolStrip();
           this.toolStart = new System.Windows.Forms.ToolStripButton();
           this.toolStop = new System.Windows.Forms.ToolStripButton();
           this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
           this.toolDelete = new System.Windows.Forms.ToolStripButton();
           this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
           this.toolOpenFolder = new System.Windows.Forms.ToolStripButton();
           this.toolOpenWebpage = new System.Windows.Forms.ToolStripButton();
           this.toolInfo = new System.Windows.Forms.ToolStripButton();
           this.tabMain = new System.Windows.Forms.TabControl();
           this.tabDownload = new System.Windows.Forms.TabPage();
           this.tabConfig = new System.Windows.Forms.TabPage();
           this.tabFlvCombine = new System.Windows.Forms.TabPage();
           this.btnNew = new System.Windows.Forms.ToolStripButton();
           this.btnConfig = new System.Windows.Forms.ToolStripButton();
           this.btnAbout = new System.Windows.Forms.ToolStripButton();
           this.flvCombineControl1 = new Kaedei.AcDown.UI.Components.FlvCombineControl();
           ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
           this.statusStrip.SuspendLayout();
           this.toolStrip.SuspendLayout();
           this.mnuTray.SuspendLayout();
           ((System.ComponentModel.ISupportInitialize)(this.udSpeedLimit)).BeginInit();
           this.contextTool.SuspendLayout();
           this.tabMain.SuspendLayout();
           this.tabDownload.SuspendLayout();
           this.tabConfig.SuspendLayout();
           this.tabFlvCombine.SuspendLayout();
           this.SuspendLayout();
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
           this.lsv.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
           this.lsv.FullRowSelect = true;
           this.lsv.Location = new System.Drawing.Point(3, 3);
           this.lsv.Name = "lsv";
           this.lsv.ShowItemToolTips = true;
           this.lsv.Size = new System.Drawing.Size(508, 303);
           this.lsv.TabIndex = 0;
           this.lsv.UseCompatibleStateImageBehavior = false;
           this.lsv.View = System.Windows.Forms.View.Details;
           this.lsv.SelectedIndexChanged += new System.EventHandler(this.lsv_SelectedIndexChanged);
           this.lsv.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lsv_KeyUp);
           this.lsv.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lsv_MouseClick);
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
           this.headerName.Width = 144;
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
           // picLogo
           // 
           this.picLogo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                       | System.Windows.Forms.AnchorStyles.Right)));
           this.picLogo.Image = global::Kaedei.AcDown.Properties.Resources.Logo;
           this.picLogo.Location = new System.Drawing.Point(0, 34);
           this.picLogo.Name = "picLogo";
           this.picLogo.Size = new System.Drawing.Size(522, 82);
           this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
           this.picLogo.TabIndex = 1;
           this.picLogo.TabStop = false;
           // 
           // statusStrip
           // 
           this.statusStrip.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
           this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblSpeed,
            this.lblBlank,
            this.toolHelpCenter});
           this.statusStrip.Location = new System.Drawing.Point(0, 465);
           this.statusStrip.Name = "statusStrip";
           this.statusStrip.Size = new System.Drawing.Size(522, 25);
           this.statusStrip.TabIndex = 6;
           // 
           // lblSpeed
           // 
           this.lblSpeed.IsLink = true;
           this.lblSpeed.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
           this.lblSpeed.Name = "lblSpeed";
           this.lblSpeed.Size = new System.Drawing.Size(47, 20);
           this.lblSpeed.Text = "0KB/s";
           this.lblSpeed.Click += new System.EventHandler(this.lblSpeed_Click);
           // 
           // lblBlank
           // 
           this.lblBlank.Name = "lblBlank";
           this.lblBlank.Size = new System.Drawing.Size(379, 20);
           this.lblBlank.Spring = true;
           // 
           // toolHelpCenter
           // 
           this.toolHelpCenter.Image = global::Kaedei.AcDown.Properties.Resources.Help;
           this.toolHelpCenter.IsLink = true;
           this.toolHelpCenter.Name = "toolHelpCenter";
           this.toolHelpCenter.Size = new System.Drawing.Size(81, 20);
           this.toolHelpCenter.Text = "帮助中心";
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
            this.btnAbout,
            this.btnSearch,
            this.txtSearch});
           this.toolStrip.Location = new System.Drawing.Point(0, 0);
           this.toolStrip.Name = "toolStrip";
           this.toolStrip.Size = new System.Drawing.Size(522, 31);
           this.toolStrip.TabIndex = 7;
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
           this.searchCustom.Size = new System.Drawing.Size(121, 22);
           this.searchCustom.Text = "自定义...";
           this.searchCustom.Click += new System.EventHandler(this.searchCustom_Click);
           // 
           // txtSearch
           // 
           this.txtSearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
           this.txtSearch.AutoToolTip = true;
           this.txtSearch.MaxLength = 20;
           this.txtSearch.Name = "txtSearch";
           this.txtSearch.Size = new System.Drawing.Size(130, 31);
           this.txtSearch.Text = "快捷搜索";
           this.txtSearch.ToolTipText = "请键入搜索关键词";
           this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
           this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyUp);
           this.txtSearch.Click += new System.EventHandler(this.txtSearch_Click);
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
           this.mnuTray.Size = new System.Drawing.Size(173, 54);
           this.mnuTray.Opening += new System.ComponentModel.CancelEventHandler(this.mnuTray_Opening);
           // 
           // mnuTrayShowHide
           // 
           this.mnuTrayShowHide.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
           this.mnuTrayShowHide.Name = "mnuTrayShowHide";
           this.mnuTrayShowHide.Size = new System.Drawing.Size(172, 22);
           this.mnuTrayShowHide.Text = "显示/隐藏主窗口";
           this.mnuTrayShowHide.Click += new System.EventHandler(this.mnuTrayShowHide_Click);
           // 
           // mnuTrayLine1
           // 
           this.mnuTrayLine1.Name = "mnuTrayLine1";
           this.mnuTrayLine1.Size = new System.Drawing.Size(169, 6);
           // 
           // mnuTrayExit
           // 
           this.mnuTrayExit.Name = "mnuTrayExit";
           this.mnuTrayExit.Size = new System.Drawing.Size(172, 22);
           this.mnuTrayExit.Text = "退出";
           this.mnuTrayExit.Click += new System.EventHandler(this.mnuTrayExit_Click);
           // 
           // btnClickNew
           // 
           this.btnClickNew.Cursor = System.Windows.Forms.Cursors.Hand;
           this.btnClickNew.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
           this.btnClickNew.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
           this.btnClickNew.Image = global::Kaedei.AcDown.Properties.Resources.Add;
           this.btnClickNew.Location = new System.Drawing.Point(130, 80);
           this.btnClickNew.MaximumSize = new System.Drawing.Size(254, 148);
           this.btnClickNew.Name = "btnClickNew";
           this.btnClickNew.Size = new System.Drawing.Size(254, 148);
           this.btnClickNew.TabIndex = 0;
           this.btnClickNew.Text = "新建下载任务";
           this.btnClickNew.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
           this.btnClickNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
           this.toolTip.SetToolTip(this.btnClickNew, "新建一个下载任务");
           this.btnClickNew.UseVisualStyleBackColor = true;
           this.btnClickNew.Click += new System.EventHandler(this.btnClickNew_Click);
           // 
           // timer
           // 
           this.timer.Enabled = true;
           this.timer.Interval = 2000;
           this.timer.Tick += new System.EventHandler(this.timer_Tick);
           // 
           // cboAfterComplete
           // 
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
           this.cboAfterComplete.Location = new System.Drawing.Point(141, 75);
           this.cboAfterComplete.Name = "cboAfterComplete";
           this.cboAfterComplete.Size = new System.Drawing.Size(116, 28);
           this.cboAfterComplete.TabIndex = 13;
           this.toolTip.SetToolTip(this.cboAfterComplete, "全部下载完成后执行的动作");
           // 
           // label1
           // 
           this.label1.AutoSize = true;
           this.label1.Location = new System.Drawing.Point(25, 78);
           this.label1.Name = "label1";
           this.label1.Size = new System.Drawing.Size(110, 20);
           this.label1.TabIndex = 14;
           this.label1.Text = "全部下载完成后:";
           // 
           // udSpeedLimit
           // 
           this.udSpeedLimit.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
           this.udSpeedLimit.Location = new System.Drawing.Point(141, 30);
           this.udSpeedLimit.Maximum = new decimal(new int[] {
            40960,
            0,
            0,
            0});
           this.udSpeedLimit.Name = "udSpeedLimit";
           this.udSpeedLimit.Size = new System.Drawing.Size(68, 26);
           this.udSpeedLimit.TabIndex = 16;
           this.udSpeedLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
           this.toolTip.SetToolTip(this.udSpeedLimit, "限速设置即时生效。设置为0可以取消限速");
           // 
           // label2
           // 
           this.label2.AutoSize = true;
           this.label2.Location = new System.Drawing.Point(25, 32);
           this.label2.Name = "label2";
           this.label2.Size = new System.Drawing.Size(105, 20);
           this.label2.TabIndex = 17;
           this.label2.Text = "速度限制(KB/s)";
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
            this.toolOpenWebpage,
            this.toolInfo});
           this.contextTool.Location = new System.Drawing.Point(42, 113);
           this.contextTool.Name = "contextTool";
           this.contextTool.Size = new System.Drawing.Size(386, 48);
           this.contextTool.TabIndex = 22;
           this.contextTool.Visible = false;
           // 
           // toolStart
           // 
           this.toolStart.Image = global::Kaedei.AcDown.Properties.Resources.ToolstripStart;
           this.toolStart.ImageTransparentColor = System.Drawing.Color.Magenta;
           this.toolStart.Name = "toolStart";
           this.toolStart.Size = new System.Drawing.Size(60, 45);
           this.toolStart.Text = "开始下载";
           this.toolStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
           this.toolStart.Click += new System.EventHandler(this.mnuConStart_Click);
           // 
           // toolStop
           // 
           this.toolStop.Image = global::Kaedei.AcDown.Properties.Resources.ToolstripStop;
           this.toolStop.ImageTransparentColor = System.Drawing.Color.Magenta;
           this.toolStop.Name = "toolStop";
           this.toolStop.Size = new System.Drawing.Size(60, 45);
           this.toolStop.Text = "停止下载";
           this.toolStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
           this.toolStop.Click += new System.EventHandler(this.mnuConStop_Click);
           // 
           // toolStripSeparator1
           // 
           this.toolStripSeparator1.Name = "toolStripSeparator1";
           this.toolStripSeparator1.Size = new System.Drawing.Size(6, 48);
           // 
           // toolDelete
           // 
           this.toolDelete.Image = global::Kaedei.AcDown.Properties.Resources.ToolstripDelete;
           this.toolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
           this.toolDelete.Name = "toolDelete";
           this.toolDelete.Size = new System.Drawing.Size(60, 45);
           this.toolDelete.Text = "删除任务";
           this.toolDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
           this.toolDelete.Click += new System.EventHandler(this.mnuConDelete_Click);
           // 
           // toolStripSeparator2
           // 
           this.toolStripSeparator2.Name = "toolStripSeparator2";
           this.toolStripSeparator2.Size = new System.Drawing.Size(6, 48);
           // 
           // toolOpenFolder
           // 
           this.toolOpenFolder.Image = global::Kaedei.AcDown.Properties.Resources.ToolstripOpenFolder;
           this.toolOpenFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
           this.toolOpenFolder.Name = "toolOpenFolder";
           this.toolOpenFolder.Size = new System.Drawing.Size(60, 45);
           this.toolOpenFolder.Text = "打开目录";
           this.toolOpenFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
           this.toolOpenFolder.ToolTipText = "打开下载文件所在文件夹";
           this.toolOpenFolder.Click += new System.EventHandler(this.mnuConOpenFolder_Click);
           // 
           // toolOpenWebpage
           // 
           this.toolOpenWebpage.Image = global::Kaedei.AcDown.Properties.Resources.ToolstripOpenWebpage;
           this.toolOpenWebpage.ImageTransparentColor = System.Drawing.Color.Magenta;
           this.toolOpenWebpage.Name = "toolOpenWebpage";
           this.toolOpenWebpage.Size = new System.Drawing.Size(60, 45);
           this.toolOpenWebpage.Text = "打开网页";
           this.toolOpenWebpage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
           this.toolOpenWebpage.ToolTipText = "打开任务源网页";
           this.toolOpenWebpage.Click += new System.EventHandler(this.mnuConOpenUrl_Click);
           // 
           // toolInfo
           // 
           this.toolInfo.Image = global::Kaedei.AcDown.Properties.Resources.ToolstripInfo;
           this.toolInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
           this.toolInfo.Name = "toolInfo";
           this.toolInfo.Size = new System.Drawing.Size(60, 45);
           this.toolInfo.Text = "扩展信息";
           this.toolInfo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
           this.toolInfo.ToolTipText = "查看任务信息";
           this.toolInfo.Click += new System.EventHandler(this.mnuConInfo_Click);
           // 
           // tabMain
           // 
           this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                       | System.Windows.Forms.AnchorStyles.Left)
                       | System.Windows.Forms.AnchorStyles.Right)));
           this.tabMain.Controls.Add(this.tabDownload);
           this.tabMain.Controls.Add(this.tabConfig);
           this.tabMain.Controls.Add(this.tabFlvCombine);
           this.tabMain.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
           this.tabMain.Location = new System.Drawing.Point(0, 122);
           this.tabMain.Name = "tabMain";
           this.tabMain.SelectedIndex = 0;
           this.tabMain.Size = new System.Drawing.Size(522, 342);
           this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
           this.tabMain.TabIndex = 23;
           // 
           // tabDownload
           // 
           this.tabDownload.Controls.Add(this.contextTool);
           this.tabDownload.Controls.Add(this.btnClickNew);
           this.tabDownload.Controls.Add(this.lsv);
           this.tabDownload.Location = new System.Drawing.Point(4, 29);
           this.tabDownload.Name = "tabDownload";
           this.tabDownload.Padding = new System.Windows.Forms.Padding(3);
           this.tabDownload.Size = new System.Drawing.Size(514, 309);
           this.tabDownload.TabIndex = 0;
           this.tabDownload.Text = "任务";
           this.tabDownload.UseVisualStyleBackColor = true;
           // 
           // tabConfig
           // 
           this.tabConfig.Controls.Add(this.label2);
           this.tabConfig.Controls.Add(this.udSpeedLimit);
           this.tabConfig.Controls.Add(this.cboAfterComplete);
           this.tabConfig.Controls.Add(this.label1);
           this.tabConfig.Location = new System.Drawing.Point(4, 29);
           this.tabConfig.Name = "tabConfig";
           this.tabConfig.Padding = new System.Windows.Forms.Padding(3);
           this.tabConfig.Size = new System.Drawing.Size(514, 309);
           this.tabConfig.TabIndex = 1;
           this.tabConfig.Text = "选项";
           this.tabConfig.UseVisualStyleBackColor = true;
           // 
           // tabFlvCombine
           // 
           this.tabFlvCombine.Controls.Add(this.flvCombineControl1);
           this.tabFlvCombine.Location = new System.Drawing.Point(4, 29);
           this.tabFlvCombine.Name = "tabFlvCombine";
           this.tabFlvCombine.Padding = new System.Windows.Forms.Padding(3);
           this.tabFlvCombine.Size = new System.Drawing.Size(514, 309);
           this.tabFlvCombine.TabIndex = 3;
           this.tabFlvCombine.Text = "视频合并";
           this.tabFlvCombine.UseVisualStyleBackColor = true;
           // 
           // btnNew
           // 
           this.btnNew.Image = global::Kaedei.AcDown.Properties.Resources.Add;
           this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
           this.btnNew.Name = "btnNew";
           this.btnNew.Size = new System.Drawing.Size(78, 28);
           this.btnNew.Text = "新建(&N)";
           this.btnNew.ToolTipText = "新建下载任务";
           this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
           // 
           // btnConfig
           // 
           this.btnConfig.Image = global::Kaedei.AcDown.Properties.Resources.Settings;
           this.btnConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
           this.btnConfig.Name = "btnConfig";
           this.btnConfig.Size = new System.Drawing.Size(76, 28);
           this.btnConfig.Text = "设置(&C)";
           this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
           // 
           // btnAbout
           // 
           this.btnAbout.Image = global::Kaedei.AcDown.Properties.Resources.About;
           this.btnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
           this.btnAbout.Name = "btnAbout";
           this.btnAbout.Size = new System.Drawing.Size(76, 28);
           this.btnAbout.Text = "关于(&A)";
           this.btnAbout.ToolTipText = "关于AcDown动漫下载器";
           this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
           // 
           // flvCombineControl1
           // 
           this.flvCombineControl1.Dock = System.Windows.Forms.DockStyle.Fill;
           this.flvCombineControl1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
           this.flvCombineControl1.Location = new System.Drawing.Point(3, 3);
           this.flvCombineControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
           this.flvCombineControl1.Name = "flvCombineControl1";
           this.flvCombineControl1.Size = new System.Drawing.Size(508, 303);
           this.flvCombineControl1.TabIndex = 0;
           // 
           // FormMain
           // 
           this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
           this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           this.ClientSize = new System.Drawing.Size(522, 490);
           this.Controls.Add(this.tabMain);
           this.Controls.Add(this.toolStrip);
           this.Controls.Add(this.statusStrip);
           this.Controls.Add(this.picLogo);
           this.MinimumSize = new System.Drawing.Size(444, 377);
           this.Name = "FormMain";
           this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
           this.Text = "AcDown动漫下载器";
           this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
           this.Load += new System.EventHandler(this.FormMain_Load);
           this.VisibleChanged += new System.EventHandler(this.FormMain_VisibleChanged);
           ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
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
           this.tabDownload.PerformLayout();
           this.tabConfig.ResumeLayout(false);
           this.tabConfig.PerformLayout();
           this.tabFlvCombine.ResumeLayout(false);
           this.ResumeLayout(false);
           this.PerformLayout();

		  }

		  #endregion

		  private System.Windows.Forms.ListView lsv;
		  private System.Windows.Forms.PictureBox picLogo;
		  private System.Windows.Forms.StatusStrip statusStrip;
		  private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		  private System.Windows.Forms.ToolStripStatusLabel lblSpeed;
		  private System.Windows.Forms.ToolStripStatusLabel lblBlank;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripTextBox txtSearch;
		  private System.Windows.Forms.NotifyIcon notifyIcon;
		  private System.Windows.Forms.ColumnHeader headerStatus;
		  private System.Windows.Forms.ColumnHeader headerName;
		  private System.Windows.Forms.ColumnHeader headerProgress;
		  private System.Windows.Forms.ColumnHeader headerSpeed;
		  private System.Windows.Forms.ColumnHeader headerSourceUrl;
        private System.Windows.Forms.Button btnClickNew;
		  private System.Windows.Forms.ColumnHeader headerPart;
        private System.Windows.Forms.Timer timer;
		  private System.Windows.Forms.ToolStripSplitButton btnSearch;
        private System.Windows.Forms.ToolStripMenuItem searchCustom;
		  private System.Windows.Forms.ComboBox cboAfterComplete;
		  private System.Windows.Forms.Label label1;
		  private System.Windows.Forms.NumericUpDown udSpeedLimit;
		  private System.Windows.Forms.Label label2;
		  private System.Windows.Forms.ToolStripButton toolStripButton1;
		  private System.Windows.Forms.ContextMenuStrip mnuTray;
		  private System.Windows.Forms.ToolStripMenuItem mnuTrayShowHide;
		  private System.Windows.Forms.ToolStripSeparator mnuTrayLine1;
		  private System.Windows.Forms.ToolStripMenuItem mnuTrayExit;
		  private System.Windows.Forms.ToolStripStatusLabel toolHelpCenter;
		  private System.Windows.Forms.ToolTip toolTip;
		  private System.Windows.Forms.ToolStrip contextTool;
		  private System.Windows.Forms.ToolStripButton toolStart;
		  private System.Windows.Forms.ToolStripButton toolStop;
		  private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		  private System.Windows.Forms.ToolStripButton toolDelete;
		  private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		  private System.Windows.Forms.ToolStripButton toolOpenFolder;
		  private System.Windows.Forms.ToolStripButton toolOpenWebpage;
        private System.Windows.Forms.ToolStripButton toolInfo;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabDownload;
        private System.Windows.Forms.TabPage tabConfig;
        private System.Windows.Forms.TabPage tabFlvCombine;
        private Kaedei.AcDown.UI.Components.FlvCombineControl flvCombineControl1;
        private System.Windows.Forms.ColumnHeader headerRemainTime;
        private System.Windows.Forms.ColumnHeader headerPastTime;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnConfig;
        private System.Windows.Forms.ToolStripButton btnAbout;

	 }
}

