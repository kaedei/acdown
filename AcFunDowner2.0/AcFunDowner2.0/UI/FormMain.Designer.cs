namespace AcFunDowner
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
			  this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			  this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			  this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			  this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			  this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			  this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			  this.picLogo = new System.Windows.Forms.PictureBox();
			  this.statusStrip = new System.Windows.Forms.StatusStrip();
			  this.lblSpeed = new System.Windows.Forms.ToolStripStatusLabel();
			  this.lblBlank = new System.Windows.Forms.ToolStripStatusLabel();
			  this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			  this.toolCheckNew = new System.Windows.Forms.ToolStripMenuItem();
			  this.toolTieba = new System.Windows.Forms.ToolStripMenuItem();
			  this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			  this.toolAdvise = new System.Windows.Forms.ToolStripMenuItem();
			  this.toolReportBug = new System.Windows.Forms.ToolStripMenuItem();
			  this.toolDesign = new System.Windows.Forms.ToolStripMenuItem();
			  this.toolReportUrl = new System.Windows.Forms.ToolStripMenuItem();
			  this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			  this.toolStrip = new System.Windows.Forms.ToolStrip();
			  this.btnNew = new System.Windows.Forms.ToolStripButton();
			  this.btnConfig = new System.Windows.Forms.ToolStripButton();
			  this.btnAbout = new System.Windows.Forms.ToolStripButton();
			  this.btnSearch = new System.Windows.Forms.ToolStripSplitButton();
			  this.searchGoogle = new System.Windows.Forms.ToolStripMenuItem();
			  this.searchBilibili = new System.Windows.Forms.ToolStripMenuItem();
			  this.searchBing = new System.Windows.Forms.ToolStripMenuItem();
			  this.searchBaidu = new System.Windows.Forms.ToolStripMenuItem();
			  this.searchCustom = new System.Windows.Forms.ToolStripMenuItem();
			  this.txtSearch = new System.Windows.Forms.ToolStripTextBox();
			  this.lnkAcfun = new System.Windows.Forms.LinkLabel();
			  this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			  this.btnClickNew = new System.Windows.Forms.Button();
			  this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			  this.mnuConCancel = new System.Windows.Forms.ToolStripMenuItem();
			  this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			  this.mnuConStart = new System.Windows.Forms.ToolStripMenuItem();
			  this.mnuConStop = new System.Windows.Forms.ToolStripMenuItem();
			  this.mnuConDelete = new System.Windows.Forms.ToolStripMenuItem();
			  this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			  this.mnuConOpenFolder = new System.Windows.Forms.ToolStripMenuItem();
			  this.mnuConOpenUrl = new System.Windows.Forms.ToolStripMenuItem();
			  this.mnuConInfo = new System.Windows.Forms.ToolStripMenuItem();
			  this.timer = new System.Windows.Forms.Timer(this.components);
			  this.cboAfterComplete = new System.Windows.Forms.ComboBox();
			  this.label1 = new System.Windows.Forms.Label();
			  this.udSpeedLimit = new System.Windows.Forms.NumericUpDown();
			  this.label2 = new System.Windows.Forms.Label();
			  this.btnLimitApply = new System.Windows.Forms.Button();
			  this.lnkBilibili = new System.Windows.Forms.LinkLabel();
			  ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
			  this.statusStrip.SuspendLayout();
			  this.toolStrip.SuspendLayout();
			  this.mnuContext.SuspendLayout();
			  ((System.ComponentModel.ISupportInitialize)(this.udSpeedLimit)).BeginInit();
			  this.SuspendLayout();
			  // 
			  // lsv
			  // 
			  this.lsv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							  | System.Windows.Forms.AnchorStyles.Left)
							  | System.Windows.Forms.AnchorStyles.Right)));
			  this.lsv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			  this.lsv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
			  this.lsv.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			  this.lsv.FullRowSelect = true;
			  this.lsv.Location = new System.Drawing.Point(12, 87);
			  this.lsv.Name = "lsv";
			  this.lsv.ShowItemToolTips = true;
			  this.lsv.Size = new System.Drawing.Size(423, 294);
			  this.lsv.TabIndex = 0;
			  this.lsv.UseCompatibleStateImageBehavior = false;
			  this.lsv.View = System.Windows.Forms.View.Details;
			  this.lsv.DoubleClick += new System.EventHandler(this.lsv_DoubleClick);
			  this.lsv.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lsv_KeyUp);
			  this.lsv.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lsv_MouseClick);
			  // 
			  // columnHeader1
			  // 
			  this.columnHeader1.Text = "状态";
			  this.columnHeader1.Width = 80;
			  // 
			  // columnHeader2
			  // 
			  this.columnHeader2.Text = "视频名称";
			  this.columnHeader2.Width = 120;
			  // 
			  // columnHeader3
			  // 
			  this.columnHeader3.Text = "分段";
			  this.columnHeader3.Width = 87;
			  // 
			  // columnHeader4
			  // 
			  this.columnHeader4.Text = "下载进度";
			  this.columnHeader4.Width = 80;
			  // 
			  // columnHeader5
			  // 
			  this.columnHeader5.Text = "下载速度";
			  this.columnHeader5.Width = 80;
			  // 
			  // columnHeader6
			  // 
			  this.columnHeader6.Text = "源地址";
			  this.columnHeader6.Width = 300;
			  // 
			  // picLogo
			  // 
			  this.picLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			  this.picLogo.Image = global::Kaedei.AcFunDowner.Properties.Resources.Logo;
			  this.picLogo.Location = new System.Drawing.Point(185, 30);
			  this.picLogo.Name = "picLogo";
			  this.picLogo.Size = new System.Drawing.Size(250, 53);
			  this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			  this.picLogo.TabIndex = 1;
			  this.picLogo.TabStop = false;
			  // 
			  // statusStrip
			  // 
			  this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblSpeed,
            this.lblBlank,
            this.toolStripDropDownButton1});
			  this.statusStrip.Location = new System.Drawing.Point(0, 431);
			  this.statusStrip.Name = "statusStrip";
			  this.statusStrip.Size = new System.Drawing.Size(447, 23);
			  this.statusStrip.TabIndex = 6;
			  // 
			  // lblSpeed
			  // 
			  this.lblSpeed.Name = "lblSpeed";
			  this.lblSpeed.Size = new System.Drawing.Size(42, 18);
			  this.lblSpeed.Text = "0KB/s";
			  // 
			  // lblBlank
			  // 
			  this.lblBlank.Name = "lblBlank";
			  this.lblBlank.Size = new System.Drawing.Size(281, 18);
			  this.lblBlank.Spring = true;
			  // 
			  // toolStripDropDownButton1
			  // 
			  this.toolStripDropDownButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			  this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolCheckNew,
            this.toolTieba,
            this.toolStripMenuItem1,
            this.toolAdvise,
            this.toolReportBug,
            this.toolDesign,
            this.toolReportUrl});
			  this.toolStripDropDownButton1.Image = global::Kaedei.AcFunDowner.Properties.Resources._1;
			  this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			  this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			  this.toolStripDropDownButton1.Size = new System.Drawing.Size(109, 21);
			  this.toolStripDropDownButton1.Text = "反馈您的意见";
			  // 
			  // toolCheckNew
			  // 
			  this.toolCheckNew.Name = "toolCheckNew";
			  this.toolCheckNew.Size = new System.Drawing.Size(248, 22);
			  this.toolCheckNew.Text = "检查是否有新版本(当前版本:2.0)";
			  this.toolCheckNew.Click += new System.EventHandler(this.toolCheckNew_Click);
			  // 
			  // toolTieba
			  // 
			  this.toolTieba.Name = "toolTieba";
			  this.toolTieba.Size = new System.Drawing.Size(248, 22);
			  this.toolTieba.Text = "与其他使用者交流";
			  this.toolTieba.Click += new System.EventHandler(this.toolTieba_Click);
			  // 
			  // toolStripMenuItem1
			  // 
			  this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			  this.toolStripMenuItem1.Size = new System.Drawing.Size(245, 6);
			  // 
			  // toolAdvise
			  // 
			  this.toolAdvise.Name = "toolAdvise";
			  this.toolAdvise.Size = new System.Drawing.Size(248, 22);
			  this.toolAdvise.Text = "建议添加新功能";
			  this.toolAdvise.Click += new System.EventHandler(this.toolAdvise_Click);
			  // 
			  // toolReportBug
			  // 
			  this.toolReportBug.Name = "toolReportBug";
			  this.toolReportBug.Size = new System.Drawing.Size(248, 22);
			  this.toolReportBug.Text = "程序出现错误";
			  this.toolReportBug.Click += new System.EventHandler(this.toolReportBug_Click);
			  // 
			  // toolDesign
			  // 
			  this.toolDesign.Name = "toolDesign";
			  this.toolDesign.Size = new System.Drawing.Size(248, 22);
			  this.toolDesign.Text = "功能设计不完善";
			  this.toolDesign.Click += new System.EventHandler(this.toolDesign_Click);
			  // 
			  // toolReportUrl
			  // 
			  this.toolReportUrl.Name = "toolReportUrl";
			  this.toolReportUrl.Size = new System.Drawing.Size(248, 22);
			  this.toolReportUrl.Text = "报告无法下载的地址";
			  this.toolReportUrl.Click += new System.EventHandler(this.toolReportUrl_Click);
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
			  this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
			  this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnConfig,
            this.btnAbout,
            this.btnSearch,
            this.txtSearch});
			  this.toolStrip.Location = new System.Drawing.Point(0, 0);
			  this.toolStrip.Name = "toolStrip";
			  this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			  this.toolStrip.Size = new System.Drawing.Size(447, 27);
			  this.toolStrip.TabIndex = 7;
			  this.toolStrip.Text = "toolStrip1";
			  // 
			  // btnNew
			  // 
			  this.btnNew.Image = global::Kaedei.AcFunDowner.Properties.Resources.Add;
			  this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			  this.btnNew.Name = "btnNew";
			  this.btnNew.Size = new System.Drawing.Size(74, 24);
			  this.btnNew.Text = "新建(&N)";
			  this.btnNew.ToolTipText = "新建下载任务";
			  this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			  // 
			  // btnConfig
			  // 
			  this.btnConfig.Image = global::Kaedei.AcFunDowner.Properties.Resources.Settings;
			  this.btnConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
			  this.btnConfig.Name = "btnConfig";
			  this.btnConfig.Size = new System.Drawing.Size(71, 24);
			  this.btnConfig.Text = "设置(&T)";
			  this.btnConfig.ToolTipText = "下载设置";
			  this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
			  // 
			  // btnAbout
			  // 
			  this.btnAbout.Image = global::Kaedei.AcFunDowner.Properties.Resources.About;
			  this.btnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
			  this.btnAbout.Name = "btnAbout";
			  this.btnAbout.Size = new System.Drawing.Size(72, 24);
			  this.btnAbout.Text = "关于(&A)";
			  this.btnAbout.ToolTipText = "关于AcFun视频下载器";
			  this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
			  // 
			  // btnSearch
			  // 
			  this.btnSearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			  this.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			  this.btnSearch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchGoogle,
            this.searchBilibili,
            this.searchBing,
            this.searchBaidu,
            this.searchCustom});
			  this.btnSearch.Image = global::Kaedei.AcFunDowner.Properties.Resources.bing;
			  this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
			  this.btnSearch.Name = "btnSearch";
			  this.btnSearch.Size = new System.Drawing.Size(36, 24);
			  this.btnSearch.Text = "搜索AcFun.cn站内视频";
			  this.btnSearch.ButtonClick += new System.EventHandler(this.btnSearch_ButtonClick);
			  // 
			  // searchGoogle
			  // 
			  this.searchGoogle.Name = "searchGoogle";
			  this.searchGoogle.Size = new System.Drawing.Size(209, 22);
			  this.searchGoogle.Text = "AcFun站内搜索(Google)";
			  this.searchGoogle.Click += new System.EventHandler(this.searchGoogle_Click);
			  // 
			  // searchBilibili
			  // 
			  this.searchBilibili.Name = "searchBilibili";
			  this.searchBilibili.Size = new System.Drawing.Size(209, 22);
			  this.searchBilibili.Text = "BiliBili站内搜索";
			  this.searchBilibili.Click += new System.EventHandler(this.searchBilibili_Click);
			  // 
			  // searchBing
			  // 
			  this.searchBing.Name = "searchBing";
			  this.searchBing.Size = new System.Drawing.Size(209, 22);
			  this.searchBing.Text = "AcFun视频 - 必应(Bing)";
			  this.searchBing.Click += new System.EventHandler(this.searchBing_Click);
			  // 
			  // searchBaidu
			  // 
			  this.searchBaidu.Name = "searchBaidu";
			  this.searchBaidu.Size = new System.Drawing.Size(209, 22);
			  this.searchBaidu.Text = "AcFun视频 - 百度";
			  this.searchBaidu.Click += new System.EventHandler(this.searchBaidu_Click);
			  // 
			  // searchCustom
			  // 
			  this.searchCustom.Name = "searchCustom";
			  this.searchCustom.Size = new System.Drawing.Size(209, 22);
			  this.searchCustom.Text = "自定义...";
			  this.searchCustom.Click += new System.EventHandler(this.searchCustom_Click);
			  // 
			  // txtSearch
			  // 
			  this.txtSearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			  this.txtSearch.AutoToolTip = true;
			  this.txtSearch.MaxLength = 20;
			  this.txtSearch.Name = "txtSearch";
			  this.txtSearch.Size = new System.Drawing.Size(130, 27);
			  this.txtSearch.Text = "搜索视频";
			  this.txtSearch.ToolTipText = "请键入搜索关键词";
			  this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
			  this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyUp);
			  this.txtSearch.Click += new System.EventHandler(this.txtSearch_Click);
			  // 
			  // lnkAcfun
			  // 
			  this.lnkAcfun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			  this.lnkAcfun.AutoSize = true;
			  this.lnkAcfun.Location = new System.Drawing.Point(12, 410);
			  this.lnkAcfun.Name = "lnkAcfun";
			  this.lnkAcfun.Size = new System.Drawing.Size(53, 12);
			  this.lnkAcfun.TabIndex = 9;
			  this.lnkAcfun.TabStop = true;
			  this.lnkAcfun.Text = "AcFun.cn";
			  this.lnkAcfun.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAcfun_LinkClicked);
			  // 
			  // notifyIcon
			  // 
			  this.notifyIcon.Text = "AcFun视频下载器 v2.0";
			  this.notifyIcon.Visible = true;
			  this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
			  // 
			  // btnClickNew
			  // 
			  this.btnClickNew.FlatStyle = System.Windows.Forms.FlatStyle.System;
			  this.btnClickNew.Location = new System.Drawing.Point(116, 141);
			  this.btnClickNew.Name = "btnClickNew";
			  this.btnClickNew.Size = new System.Drawing.Size(223, 161);
			  this.btnClickNew.TabIndex = 0;
			  this.btnClickNew.Text = "新建下载任务";
			  this.btnClickNew.UseVisualStyleBackColor = true;
			  this.btnClickNew.Click += new System.EventHandler(this.btnClickNew_Click);
			  // 
			  // mnuContext
			  // 
			  this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuConCancel,
            this.toolStripMenuItem2,
            this.mnuConStart,
            this.mnuConStop,
            this.mnuConDelete,
            this.toolStripMenuItem3,
            this.mnuConOpenFolder,
            this.mnuConOpenUrl,
            this.mnuConInfo});
			  this.mnuContext.Name = "mnuContext";
			  this.mnuContext.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			  this.mnuContext.Size = new System.Drawing.Size(185, 170);
			  // 
			  // mnuConCancel
			  // 
			  this.mnuConCancel.Enabled = false;
			  this.mnuConCancel.Name = "mnuConCancel";
			  this.mnuConCancel.Size = new System.Drawing.Size(184, 22);
			  this.mnuConCancel.Text = "AcFun视频下载器";
			  // 
			  // toolStripMenuItem2
			  // 
			  this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			  this.toolStripMenuItem2.Size = new System.Drawing.Size(181, 6);
			  // 
			  // mnuConStart
			  // 
			  this.mnuConStart.Name = "mnuConStart";
			  this.mnuConStart.Size = new System.Drawing.Size(184, 22);
			  this.mnuConStart.Text = "开始(&S)";
			  this.mnuConStart.Click += new System.EventHandler(this.mnuConStart_Click);
			  // 
			  // mnuConStop
			  // 
			  this.mnuConStop.Name = "mnuConStop";
			  this.mnuConStop.Size = new System.Drawing.Size(184, 22);
			  this.mnuConStop.Text = "停止";
			  this.mnuConStop.Click += new System.EventHandler(this.mnuConStop_Click);
			  // 
			  // mnuConDelete
			  // 
			  this.mnuConDelete.Name = "mnuConDelete";
			  this.mnuConDelete.Size = new System.Drawing.Size(184, 22);
			  this.mnuConDelete.Text = "删除任务";
			  this.mnuConDelete.Click += new System.EventHandler(this.mnuConDelete_Click);
			  // 
			  // toolStripMenuItem3
			  // 
			  this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			  this.toolStripMenuItem3.Size = new System.Drawing.Size(181, 6);
			  // 
			  // mnuConOpenFolder
			  // 
			  this.mnuConOpenFolder.Name = "mnuConOpenFolder";
			  this.mnuConOpenFolder.Size = new System.Drawing.Size(184, 22);
			  this.mnuConOpenFolder.Text = "打开视频所在文件夹";
			  this.mnuConOpenFolder.Click += new System.EventHandler(this.mnuConOpenFolder_Click);
			  // 
			  // mnuConOpenUrl
			  // 
			  this.mnuConOpenUrl.Name = "mnuConOpenUrl";
			  this.mnuConOpenUrl.Size = new System.Drawing.Size(184, 22);
			  this.mnuConOpenUrl.Text = "打开视频页面";
			  this.mnuConOpenUrl.Click += new System.EventHandler(this.mnuConOpenUrl_Click);
			  // 
			  // mnuConInfo
			  // 
			  this.mnuConInfo.Name = "mnuConInfo";
			  this.mnuConInfo.Size = new System.Drawing.Size(184, 22);
			  this.mnuConInfo.Text = "查看视频信息";
			  this.mnuConInfo.Click += new System.EventHandler(this.mnuConInfo_Click);
			  // 
			  // timer
			  // 
			  this.timer.Enabled = true;
			  this.timer.Interval = 1000;
			  this.timer.Tick += new System.EventHandler(this.timer_Tick);
			  // 
			  // cboAfterComplete
			  // 
			  this.cboAfterComplete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			  this.cboAfterComplete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			  this.cboAfterComplete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			  this.cboAfterComplete.FormattingEnabled = true;
			  this.cboAfterComplete.Items.AddRange(new object[] {
            "无动作",
            "关机",
            "待机",
            "休眠",
            "注销",
            "重新启动"});
			  this.cboAfterComplete.Location = new System.Drawing.Point(360, 387);
			  this.cboAfterComplete.Name = "cboAfterComplete";
			  this.cboAfterComplete.Size = new System.Drawing.Size(75, 20);
			  this.cboAfterComplete.TabIndex = 13;
			  // 
			  // label1
			  // 
			  this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			  this.label1.AutoSize = true;
			  this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			  this.label1.Location = new System.Drawing.Point(283, 390);
			  this.label1.Name = "label1";
			  this.label1.Size = new System.Drawing.Size(71, 12);
			  this.label1.TabIndex = 14;
			  this.label1.Text = "下载完成后:";
			  // 
			  // udSpeedLimit
			  // 
			  this.udSpeedLimit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			  this.udSpeedLimit.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			  this.udSpeedLimit.Location = new System.Drawing.Point(107, 386);
			  this.udSpeedLimit.Maximum = new decimal(new int[] {
            40960,
            0,
            0,
            0});
			  this.udSpeedLimit.Name = "udSpeedLimit";
			  this.udSpeedLimit.Size = new System.Drawing.Size(49, 21);
			  this.udSpeedLimit.TabIndex = 16;
			  this.udSpeedLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			  // 
			  // label2
			  // 
			  this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			  this.label2.AutoSize = true;
			  this.label2.Location = new System.Drawing.Point(12, 390);
			  this.label2.Name = "label2";
			  this.label2.Size = new System.Drawing.Size(89, 12);
			  this.label2.TabIndex = 17;
			  this.label2.Text = "速度限制(KB/s)";
			  // 
			  // btnLimitApply
			  // 
			  this.btnLimitApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			  this.btnLimitApply.FlatStyle = System.Windows.Forms.FlatStyle.System;
			  this.btnLimitApply.Location = new System.Drawing.Point(162, 386);
			  this.btnLimitApply.Name = "btnLimitApply";
			  this.btnLimitApply.Size = new System.Drawing.Size(53, 20);
			  this.btnLimitApply.TabIndex = 18;
			  this.btnLimitApply.Text = "生效";
			  this.btnLimitApply.UseVisualStyleBackColor = true;
			  this.btnLimitApply.Click += new System.EventHandler(this.btnLimitApply_Click);
			  // 
			  // lnkBilibili
			  // 
			  this.lnkBilibili.AutoSize = true;
			  this.lnkBilibili.Location = new System.Drawing.Point(71, 411);
			  this.lnkBilibili.Name = "lnkBilibili";
			  this.lnkBilibili.Size = new System.Drawing.Size(71, 12);
			  this.lnkBilibili.TabIndex = 19;
			  this.lnkBilibili.TabStop = true;
			  this.lnkBilibili.Text = "BiliBili.us";
			  this.lnkBilibili.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkBilibili_LinkClicked);
			  // 
			  // FormMain
			  // 
			  this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			  this.ClientSize = new System.Drawing.Size(447, 454);
			  this.Controls.Add(this.lnkBilibili);
			  this.Controls.Add(this.label2);
			  this.Controls.Add(this.lnkAcfun);
			  this.Controls.Add(this.udSpeedLimit);
			  this.Controls.Add(this.btnLimitApply);
			  this.Controls.Add(this.label1);
			  this.Controls.Add(this.btnClickNew);
			  this.Controls.Add(this.cboAfterComplete);
			  this.Controls.Add(this.toolStrip);
			  this.Controls.Add(this.statusStrip);
			  this.Controls.Add(this.picLogo);
			  this.Controls.Add(this.lsv);
			  this.MinimumSize = new System.Drawing.Size(410, 425);
			  this.Name = "FormMain";
			  this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			  this.Text = "AcFun视频下载器 v2.0";
			  this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
			  this.Load += new System.EventHandler(this.FormMain_Load);
			  this.Resize += new System.EventHandler(this.FormMain_Resize);
			  ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
			  this.statusStrip.ResumeLayout(false);
			  this.statusStrip.PerformLayout();
			  this.toolStrip.ResumeLayout(false);
			  this.toolStrip.PerformLayout();
			  this.mnuContext.ResumeLayout(false);
			  ((System.ComponentModel.ISupportInitialize)(this.udSpeedLimit)).EndInit();
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
		  private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripTextBox txtSearch;
        private System.Windows.Forms.ToolStripButton btnConfig;
		  private System.Windows.Forms.ToolStripButton btnAbout;
		  private System.Windows.Forms.LinkLabel lnkAcfun;
		  private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ColumnHeader columnHeader1;
		  private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
		  private System.Windows.Forms.Button btnClickNew;
		  private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
		  private System.Windows.Forms.ToolStripMenuItem toolAdvise;
		  private System.Windows.Forms.ToolStripMenuItem toolReportBug;
		  private System.Windows.Forms.ToolStripMenuItem toolDesign;
		  private System.Windows.Forms.ToolStripMenuItem toolReportUrl;
		  private System.Windows.Forms.ToolStripMenuItem toolCheckNew;
		  private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		  private System.Windows.Forms.ContextMenuStrip mnuContext;
		  private System.Windows.Forms.ToolStripMenuItem mnuConCancel;
		  private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		  private System.Windows.Forms.ToolStripMenuItem mnuConStart;
		  private System.Windows.Forms.ToolStripMenuItem mnuConStop;
		  private System.Windows.Forms.ToolStripMenuItem mnuConInfo;
		  private System.Windows.Forms.ColumnHeader columnHeader3;
		  private System.Windows.Forms.Timer timer;
		  private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		  private System.Windows.Forms.ToolStripMenuItem mnuConOpenFolder;
		  private System.Windows.Forms.ToolStripMenuItem mnuConOpenUrl;
		  private System.Windows.Forms.ToolStripSplitButton btnSearch;
		  private System.Windows.Forms.ToolStripMenuItem searchGoogle;
		  private System.Windows.Forms.ToolStripMenuItem searchBing;
		  private System.Windows.Forms.ToolStripMenuItem searchBaidu;
		  private System.Windows.Forms.ToolStripMenuItem searchCustom;
		  private System.Windows.Forms.ToolStripMenuItem toolTieba;
		  private System.Windows.Forms.ToolStripMenuItem mnuConDelete;
		  private System.Windows.Forms.ComboBox cboAfterComplete;
		  private System.Windows.Forms.Label label1;
		  private System.Windows.Forms.NumericUpDown udSpeedLimit;
		  private System.Windows.Forms.Label label2;
		  private System.Windows.Forms.Button btnLimitApply;
		  private System.Windows.Forms.ToolStripMenuItem searchBilibili;
		  private System.Windows.Forms.LinkLabel lnkBilibili;

    }
}

