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
			System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("视频", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("弹幕", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "【惨烈】灰飞烟灭的车祸瞬间【惨烈】灰飞烟灭的车祸瞬间【惨烈】灰飞烟灭的车祸瞬间【惨烈】灰飞烟灭的车祸瞬间【惨烈】灰飞烟灭的车祸瞬间【惨烈】灰飞烟灭的车祸瞬间.fl" +
                "v",
            "3:45",
            "C:\\Program Files\\Test(1).flv",
            "ABC"}, 0);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "TESTVIDEOFILE(2).mp4",
            "2:25",
            "D:\\VIDEO\\TESETVIDEOFILE(2).mp4"}, 0);
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "test",
            "Acfun弹幕",
            "C:\\VIDEO\\test.xml"}, 1);
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AcPlay2));
			this.lsv = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.lnkAdd = new System.Windows.Forms.LinkLabel();
			this.lnkDelete = new System.Windows.Forms.LinkLabel();
			this.lnkClear = new System.Windows.Forms.LinkLabel();
			this.btnStart = new System.Windows.Forms.Button();
			this.cboPlayer = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lnkPlayerCache = new System.Windows.Forms.LinkLabel();
			this.lnkImport = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// lsv
			// 
			this.lsv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.lsv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
			listViewGroup1.Header = "视频";
			listViewGroup1.Name = "VideoGroup";
			listViewGroup2.Header = "弹幕";
			listViewGroup2.Name = "SubtitleGroup";
			this.lsv.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
			listViewItem1.Group = listViewGroup1;
			listViewItem2.Group = listViewGroup1;
			listViewItem2.StateImageIndex = 0;
			listViewItem3.Group = listViewGroup2;
			this.lsv.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
			this.lsv.LargeImageList = this.imageList1;
			this.lsv.Location = new System.Drawing.Point(3, 41);
			this.lsv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.lsv.Name = "lsv";
			this.lsv.Size = new System.Drawing.Size(562, 358);
			this.lsv.SmallImageList = this.imageList1;
			this.lsv.TabIndex = 0;
			this.lsv.TileSize = new System.Drawing.Size(220, 56);
			this.lsv.UseCompatibleStateImageBehavior = false;
			this.lsv.View = System.Windows.Forms.View.Tile;
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
			// lnkAdd
			// 
			this.lnkAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lnkAdd.AutoSize = true;
			this.lnkAdd.Location = new System.Drawing.Point(3, 408);
			this.lnkAdd.Name = "lnkAdd";
			this.lnkAdd.Size = new System.Drawing.Size(32, 17);
			this.lnkAdd.TabIndex = 1;
			this.lnkAdd.TabStop = true;
			this.lnkAdd.Text = "添加";
			// 
			// lnkDelete
			// 
			this.lnkDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lnkDelete.AutoSize = true;
			this.lnkDelete.Location = new System.Drawing.Point(41, 408);
			this.lnkDelete.Name = "lnkDelete";
			this.lnkDelete.Size = new System.Drawing.Size(32, 17);
			this.lnkDelete.TabIndex = 2;
			this.lnkDelete.TabStop = true;
			this.lnkDelete.Text = "删除";
			// 
			// lnkClear
			// 
			this.lnkClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lnkClear.AutoSize = true;
			this.lnkClear.Location = new System.Drawing.Point(79, 408);
			this.lnkClear.Name = "lnkClear";
			this.lnkClear.Size = new System.Drawing.Size(32, 17);
			this.lnkClear.TabIndex = 3;
			this.lnkClear.TabStop = true;
			this.lnkClear.Text = "清空";
			// 
			// btnStart
			// 
			this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnStart.Location = new System.Drawing.Point(457, 407);
			this.btnStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(108, 41);
			this.btnStart.TabIndex = 4;
			this.btnStart.Text = "播放";
			this.btnStart.UseVisualStyleBackColor = true;
			// 
			// cboPlayer
			// 
			this.cboPlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPlayer.FormattingEnabled = true;
			this.cboPlayer.Items.AddRange(new object[] {
            "AcFun播放器",
            "Bilibili播放器"});
			this.cboPlayer.Location = new System.Drawing.Point(68, 8);
			this.cboPlayer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.cboPlayer.Name = "cboPlayer";
			this.cboPlayer.Size = new System.Drawing.Size(140, 25);
			this.cboPlayer.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 17);
			this.label1.TabIndex = 6;
			this.label1.Text = "播放器：";
			// 
			// lnkPlayerCache
			// 
			this.lnkPlayerCache.AutoSize = true;
			this.lnkPlayerCache.Location = new System.Drawing.Point(216, 12);
			this.lnkPlayerCache.Name = "lnkPlayerCache";
			this.lnkPlayerCache.Size = new System.Drawing.Size(92, 17);
			this.lnkPlayerCache.TabIndex = 7;
			this.lnkPlayerCache.TabStop = true;
			this.lnkPlayerCache.Text = "更新播放器缓存";
			this.lnkPlayerCache.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkPlayerCache_LinkClicked);
			// 
			// lnkImport
			// 
			this.lnkImport.AutoSize = true;
			this.lnkImport.Location = new System.Drawing.Point(117, 408);
			this.lnkImport.Name = "lnkImport";
			this.lnkImport.Size = new System.Drawing.Size(80, 17);
			this.lnkImport.TabIndex = 8;
			this.lnkImport.TabStop = true;
			this.lnkImport.Text = "导出配置文件";
			// 
			// AcPlay2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lnkImport);
			this.Controls.Add(this.lnkPlayerCache);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cboPlayer);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.lnkClear);
			this.Controls.Add(this.lnkDelete);
			this.Controls.Add(this.lnkAdd);
			this.Controls.Add(this.lsv);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "AcPlay2";
			this.Size = new System.Drawing.Size(569, 454);
			this.Load += new System.EventHandler(this.AcPlay2_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView lsv;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.LinkLabel lnkAdd;
		private System.Windows.Forms.LinkLabel lnkDelete;
		private System.Windows.Forms.LinkLabel lnkClear;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.ComboBox cboPlayer;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel lnkPlayerCache;
		private System.Windows.Forms.LinkLabel lnkImport;

	}
}
