namespace Kaedei.AcDown.UI.Components
{
	partial class AcPlay2
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("视频", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("弹幕", System.Windows.Forms.HorizontalAlignment.Left);
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AcPlay2));
			this.lsv = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.btnStart = new System.Windows.Forms.Button();
			this.cboPlayer = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lnkPlayerCache = new System.Windows.Forms.LinkLabel();
			this.lnkImport = new System.Windows.Forms.LinkLabel();
			this.lnkExport = new System.Windows.Forms.LinkLabel();
			this.label2 = new System.Windows.Forms.Label();
			this.lnkAbout = new System.Windows.Forms.LinkLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnDel = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.chkOptions = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.chkDebug = new System.Windows.Forms.CheckBox();
			this.cboProxy = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.udSpeedLimit = new System.Windows.Forms.NumericUpDown();
			this.lnkDandanplay = new System.Windows.Forms.LinkLabel();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.udSpeedLimit)).BeginInit();
			this.SuspendLayout();
			// 
			// lsv
			// 
			this.lsv.AllowDrop = true;
			this.lsv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lsv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
			listViewGroup3.Header = "视频";
			listViewGroup3.Name = "VideoGroup";
			listViewGroup4.Header = "弹幕";
			listViewGroup4.Name = "SubtitleGroup";
			this.lsv.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup3,
            listViewGroup4});
			this.lsv.LargeImageList = this.imageList1;
			this.lsv.Location = new System.Drawing.Point(121, 77);
			this.lsv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.lsv.Name = "lsv";
			this.lsv.Size = new System.Drawing.Size(536, 318);
			this.lsv.SmallImageList = this.imageList1;
			this.lsv.TabIndex = 3;
			this.lsv.TileSize = new System.Drawing.Size(220, 56);
			this.lsv.UseCompatibleStateImageBehavior = false;
			this.lsv.View = System.Windows.Forms.View.Tile;
			this.lsv.DragDrop += new System.Windows.Forms.DragEventHandler(this.lsv_DragDrop);
			this.lsv.DragEnter += new System.Windows.Forms.DragEventHandler(this.lsv_DragEnter);
			this.lsv.DoubleClick += new System.EventHandler(this.lsv_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "名称";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "长度";
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "文件路径";
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "Video.png");
			this.imageList1.Images.SetKeyName(1, "Xml.png");
			// 
			// btnStart
			// 
			this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnStart.Location = new System.Drawing.Point(544, 399);
			this.btnStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(105, 36);
			this.btnStart.TabIndex = 9;
			this.btnStart.Text = "播放";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// cboPlayer
			// 
			this.cboPlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPlayer.FormattingEnabled = true;
			this.cboPlayer.Items.AddRange(new object[] {
            "AcFun播放器",
            "BiliBili播放器"});
			this.cboPlayer.Location = new System.Drawing.Point(81, 5);
			this.cboPlayer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.cboPlayer.Name = "cboPlayer";
			this.cboPlayer.Size = new System.Drawing.Size(140, 25);
			this.cboPlayer.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "播放器：";
			// 
			// lnkPlayerCache
			// 
			this.lnkPlayerCache.AutoSize = true;
			this.lnkPlayerCache.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.lnkPlayerCache.Location = new System.Drawing.Point(227, 9);
			this.lnkPlayerCache.Name = "lnkPlayerCache";
			this.lnkPlayerCache.Size = new System.Drawing.Size(160, 17);
			this.lnkPlayerCache.TabIndex = 2;
			this.lnkPlayerCache.TabStop = true;
			this.lnkPlayerCache.Text = "(离线播放)更新此播放器缓存";
			this.lnkPlayerCache.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkPlayerCache_LinkClicked);
			// 
			// lnkImport
			// 
			this.lnkImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lnkImport.AutoSize = true;
			this.lnkImport.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.lnkImport.Location = new System.Drawing.Point(16, 409);
			this.lnkImport.Name = "lnkImport";
			this.lnkImport.Size = new System.Drawing.Size(115, 17);
			this.lnkImport.TabIndex = 7;
			this.lnkImport.TabStop = true;
			this.lnkImport.Text = "从.ACPLAY文件导入";
			this.lnkImport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkImport_LinkClicked);
			// 
			// lnkExport
			// 
			this.lnkExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lnkExport.AutoSize = true;
			this.lnkExport.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.lnkExport.Location = new System.Drawing.Point(137, 409);
			this.lnkExport.Name = "lnkExport";
			this.lnkExport.Size = new System.Drawing.Size(103, 17);
			this.lnkExport.TabIndex = 8;
			this.lnkExport.TabStop = true;
			this.lnkExport.Text = "导出.ACPLAY文件";
			this.lnkExport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkExport_LinkClicked);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 42);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(68, 17);
			this.label2.TabIndex = 10;
			this.label2.Text = "播放列表：";
			// 
			// lnkAbout
			// 
			this.lnkAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lnkAbout.AutoSize = true;
			this.lnkAbout.Location = new System.Drawing.Point(524, 9);
			this.lnkAbout.Name = "lnkAbout";
			this.lnkAbout.Size = new System.Drawing.Size(114, 17);
			this.lnkAbout.TabIndex = 11;
			this.lnkAbout.TabStop = true;
			this.lnkAbout.Text = "使用方法&&常见问题";
			this.lnkAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAbout_LinkClicked);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.lnkDandanplay);
			this.splitContainer1.Panel1.Controls.Add(this.btnClear);
			this.splitContainer1.Panel1.Controls.Add(this.btnDel);
			this.splitContainer1.Panel1.Controls.Add(this.btnAdd);
			this.splitContainer1.Panel1.Controls.Add(this.label1);
			this.splitContainer1.Panel1.Controls.Add(this.lsv);
			this.splitContainer1.Panel1.Controls.Add(this.lnkImport);
			this.splitContainer1.Panel1.Controls.Add(this.chkOptions);
			this.splitContainer1.Panel1.Controls.Add(this.cboPlayer);
			this.splitContainer1.Panel1.Controls.Add(this.btnStart);
			this.splitContainer1.Panel1.Controls.Add(this.lnkExport);
			this.splitContainer1.Panel1.Controls.Add(this.lnkPlayerCache);
			this.splitContainer1.Panel1.Controls.Add(this.lnkAbout);
			this.splitContainer1.Panel1.Controls.Add(this.label2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.label5);
			this.splitContainer1.Panel2.Controls.Add(this.chkDebug);
			this.splitContainer1.Panel2.Controls.Add(this.cboProxy);
			this.splitContainer1.Panel2.Controls.Add(this.label4);
			this.splitContainer1.Panel2.Controls.Add(this.label3);
			this.splitContainer1.Panel2.Controls.Add(this.udSpeedLimit);
			this.splitContainer1.Panel2Collapsed = true;
			this.splitContainer1.Size = new System.Drawing.Size(661, 442);
			this.splitContainer1.SplitterDistance = 338;
			this.splitContainer1.TabIndex = 12;
			// 
			// btnClear
			// 
			this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClear.Location = new System.Drawing.Point(3, 149);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(112, 30);
			this.btnClear.TabIndex = 14;
			this.btnClear.Text = "清空列表";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// btnDel
			// 
			this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDel.Location = new System.Drawing.Point(3, 113);
			this.btnDel.Name = "btnDel";
			this.btnDel.Size = new System.Drawing.Size(112, 30);
			this.btnDel.TabIndex = 13;
			this.btnDel.Text = "删除选中项";
			this.btnDel.UseVisualStyleBackColor = true;
			this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAdd.Location = new System.Drawing.Point(3, 77);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(112, 30);
			this.btnAdd.TabIndex = 12;
			this.btnAdd.Text = "添加视频/弹幕";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// chkOptions
			// 
			this.chkOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.chkOptions.AutoSize = true;
			this.chkOptions.Location = new System.Drawing.Point(463, 408);
			this.chkOptions.Name = "chkOptions";
			this.chkOptions.Size = new System.Drawing.Size(75, 21);
			this.chkOptions.TabIndex = 10;
			this.chkOptions.Text = "高级选项";
			this.chkOptions.UseVisualStyleBackColor = true;
			this.chkOptions.CheckedChanged += new System.EventHandler(this.chkOptions_CheckedChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(111, 70);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(59, 17);
			this.label5.TabIndex = 16;
			this.label5.Text = "调试模式:";
			// 
			// chkDebug
			// 
			this.chkDebug.AutoSize = true;
			this.chkDebug.Location = new System.Drawing.Point(176, 69);
			this.chkDebug.Name = "chkDebug";
			this.chkDebug.Size = new System.Drawing.Size(51, 21);
			this.chkDebug.TabIndex = 15;
			this.chkDebug.Text = "开启";
			this.chkDebug.UseVisualStyleBackColor = true;
			// 
			// cboProxy
			// 
			this.cboProxy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboProxy.FormattingEnabled = true;
			this.cboProxy.Location = new System.Drawing.Point(176, 38);
			this.cboProxy.Name = "cboProxy";
			this.cboProxy.Size = new System.Drawing.Size(155, 25);
			this.cboProxy.TabIndex = 14;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(75, 41);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(95, 17);
			this.label4.TabIndex = 13;
			this.label4.Text = "使用代理服务器:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(16, 11);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(154, 17);
			this.label3.TabIndex = 12;
			this.label3.Text = "缓冲本地文件时限速(KB/s):";
			// 
			// udSpeedLimit
			// 
			this.udSpeedLimit.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.udSpeedLimit.Location = new System.Drawing.Point(176, 9);
			this.udSpeedLimit.Maximum = new decimal(new int[] {
            10240,
            0,
            0,
            0});
			this.udSpeedLimit.Name = "udSpeedLimit";
			this.udSpeedLimit.Size = new System.Drawing.Size(75, 23);
			this.udSpeedLimit.TabIndex = 11;
			this.udSpeedLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lnkDandanplay
			// 
			this.lnkDandanplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lnkDandanplay.AutoSize = true;
			this.lnkDandanplay.Location = new System.Drawing.Point(474, 9);
			this.lnkDandanplay.Name = "lnkDandanplay";
			this.lnkDandanplay.Size = new System.Drawing.Size(44, 17);
			this.lnkDandanplay.TabIndex = 15;
			this.lnkDandanplay.TabStop = true;
			this.lnkDandanplay.Text = "高级版";
			this.lnkDandanplay.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDandanplay_LinkClicked);
			// 
			// AcPlay2
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "AcPlay2";
			this.Size = new System.Drawing.Size(661, 442);
			this.Load += new System.EventHandler(this.AcPlay2_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.udSpeedLimit)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lsv;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.ComboBox cboPlayer;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel lnkPlayerCache;
		private System.Windows.Forms.LinkLabel lnkImport;
		private System.Windows.Forms.LinkLabel lnkExport;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.LinkLabel lnkAbout;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.CheckBox chkOptions;
		private System.Windows.Forms.ComboBox cboProxy;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown udSpeedLimit;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox chkDebug;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnDel;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.LinkLabel lnkDandanplay;

	}
}
