namespace Kaedei.AcDown.UI
{
	 partial class FormConfig
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

		  #region Windows Form Designer generated code

		  /// <summary>
		  /// Required method for Designer support - do not modify
		  /// the contents of this method with the code editor.
		  /// </summary>
		  private void InitializeComponent()
		  {
           this.btnCancel = new System.Windows.Forms.Button();
           this.btnOK = new System.Windows.Forms.Button();
           this.tab = new System.Windows.Forms.TabControl();
           this.pageGeneral = new System.Windows.Forms.TabPage();
           this.label2 = new System.Windows.Forms.Label();
           this.lnkSavePath = new System.Windows.Forms.LinkLabel();
           this.lnkLog = new System.Windows.Forms.LinkLabel();
           this.chkEnableLog = new System.Windows.Forms.CheckBox();
           this.pageDownload = new System.Windows.Forms.TabPage();
           this.label1 = new System.Windows.Forms.Label();
           this.numCacheSize = new System.Windows.Forms.NumericUpDown();
           this.chkDeleteFile = new System.Windows.Forms.CheckBox();
           this.chkPlaySound = new System.Windows.Forms.CheckBox();
           this.chkOpenFolder = new System.Windows.Forms.CheckBox();
           this.chkDownAllParts = new System.Windows.Forms.CheckBox();
           this.chkDownSub = new System.Windows.Forms.CheckBox();
           this.pageUI = new System.Windows.Forms.TabPage();
           this.txtSearchText = new System.Windows.Forms.TextBox();
           this.label4 = new System.Windows.Forms.Label();
           this.chkShowBigButton = new System.Windows.Forms.CheckBox();
           this.chkEnableWin7 = new System.Windows.Forms.CheckBox();
           this.chkCheckUrl = new System.Windows.Forms.CheckBox();
           this.chkWatch = new System.Windows.Forms.CheckBox();
           this.pagePlugin = new System.Windows.Forms.TabPage();
           this.tab.SuspendLayout();
           this.pageGeneral.SuspendLayout();
           this.pageDownload.SuspendLayout();
           ((System.ComponentModel.ISupportInitialize)(this.numCacheSize)).BeginInit();
           this.pageUI.SuspendLayout();
           this.SuspendLayout();
           // 
           // btnCancel
           // 
           this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
           this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
           this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.btnCancel.Location = new System.Drawing.Point(325, 352);
           this.btnCancel.Name = "btnCancel";
           this.btnCancel.Size = new System.Drawing.Size(75, 23);
           this.btnCancel.TabIndex = 2;
           this.btnCancel.Text = "取消";
           this.btnCancel.UseVisualStyleBackColor = true;
           this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
           // 
           // btnOK
           // 
           this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
           this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.btnOK.Location = new System.Drawing.Point(244, 352);
           this.btnOK.Name = "btnOK";
           this.btnOK.Size = new System.Drawing.Size(75, 23);
           this.btnOK.TabIndex = 3;
           this.btnOK.Text = "确认";
           this.btnOK.UseVisualStyleBackColor = true;
           this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
           // 
           // tab
           // 
           this.tab.Controls.Add(this.pageGeneral);
           this.tab.Controls.Add(this.pageDownload);
           this.tab.Controls.Add(this.pageUI);
           this.tab.Controls.Add(this.pagePlugin);
           this.tab.Dock = System.Windows.Forms.DockStyle.Top;
           this.tab.Location = new System.Drawing.Point(0, 0);
           this.tab.Name = "tab";
           this.tab.SelectedIndex = 0;
           this.tab.ShowToolTips = true;
           this.tab.Size = new System.Drawing.Size(406, 344);
           this.tab.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
           this.tab.TabIndex = 4;
           this.tab.TabStop = false;
           // 
           // pageGeneral
           // 
           this.pageGeneral.Controls.Add(this.label2);
           this.pageGeneral.Controls.Add(this.lnkSavePath);
           this.pageGeneral.Controls.Add(this.lnkLog);
           this.pageGeneral.Controls.Add(this.chkEnableLog);
           this.pageGeneral.Location = new System.Drawing.Point(4, 22);
           this.pageGeneral.Name = "pageGeneral";
           this.pageGeneral.Padding = new System.Windows.Forms.Padding(3);
           this.pageGeneral.Size = new System.Drawing.Size(398, 318);
           this.pageGeneral.TabIndex = 1;
           this.pageGeneral.Text = "常规";
           this.pageGeneral.UseVisualStyleBackColor = true;
           // 
           // label2
           // 
           this.label2.AutoSize = true;
           this.label2.Location = new System.Drawing.Point(30, 81);
           this.label2.Name = "label2";
           this.label2.Size = new System.Drawing.Size(161, 12);
           this.label2.TabIndex = 8;
           this.label2.Text = "默认保存文件夹：(点击更改)";
           // 
           // lnkSavePath
           // 
           this.lnkSavePath.AutoSize = true;
           this.lnkSavePath.Location = new System.Drawing.Point(30, 105);
           this.lnkSavePath.Name = "lnkSavePath";
           this.lnkSavePath.Size = new System.Drawing.Size(95, 12);
           this.lnkSavePath.TabIndex = 7;
           this.lnkSavePath.TabStop = true;
           this.lnkSavePath.Text = "D:\\My Documents";
           this.lnkSavePath.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSavePath_LinkClicked);
           // 
           // lnkLog
           // 
           this.lnkLog.AutoSize = true;
           this.lnkLog.Location = new System.Drawing.Point(43, 49);
           this.lnkLog.Name = "lnkLog";
           this.lnkLog.Size = new System.Drawing.Size(77, 12);
           this.lnkLog.TabIndex = 5;
           this.lnkLog.TabStop = true;
           this.lnkLog.Text = "查看错误日志";
           this.lnkLog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLog_LinkClicked);
           // 
           // chkEnableLog
           // 
           this.chkEnableLog.AutoSize = true;
           this.chkEnableLog.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.chkEnableLog.Location = new System.Drawing.Point(32, 25);
           this.chkEnableLog.Name = "chkEnableLog";
           this.chkEnableLog.Size = new System.Drawing.Size(102, 17);
           this.chkEnableLog.TabIndex = 6;
           this.chkEnableLog.Text = "启用错误日志";
           this.chkEnableLog.UseVisualStyleBackColor = true;
           // 
           // pageDownload
           // 
           this.pageDownload.Controls.Add(this.label1);
           this.pageDownload.Controls.Add(this.numCacheSize);
           this.pageDownload.Controls.Add(this.chkDeleteFile);
           this.pageDownload.Controls.Add(this.chkPlaySound);
           this.pageDownload.Controls.Add(this.chkOpenFolder);
           this.pageDownload.Controls.Add(this.chkDownAllParts);
           this.pageDownload.Controls.Add(this.chkDownSub);
           this.pageDownload.Location = new System.Drawing.Point(4, 22);
           this.pageDownload.Name = "pageDownload";
           this.pageDownload.Padding = new System.Windows.Forms.Padding(3);
           this.pageDownload.Size = new System.Drawing.Size(398, 318);
           this.pageDownload.TabIndex = 2;
           this.pageDownload.Text = "下载";
           this.pageDownload.UseVisualStyleBackColor = true;
           // 
           // label1
           // 
           this.label1.AutoSize = true;
           this.label1.Location = new System.Drawing.Point(25, 151);
           this.label1.Name = "label1";
           this.label1.Size = new System.Drawing.Size(113, 12);
           this.label1.TabIndex = 16;
           this.label1.Text = "缓存大小：(1-16MB)";
           // 
           // numCacheSize
           // 
           this.numCacheSize.Location = new System.Drawing.Point(144, 149);
           this.numCacheSize.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
           this.numCacheSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
           this.numCacheSize.Name = "numCacheSize";
           this.numCacheSize.Size = new System.Drawing.Size(49, 21);
           this.numCacheSize.TabIndex = 15;
           this.numCacheSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
           // 
           // chkDeleteFile
           // 
           this.chkDeleteFile.AutoSize = true;
           this.chkDeleteFile.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.chkDeleteFile.Location = new System.Drawing.Point(27, 112);
           this.chkDeleteFile.Name = "chkDeleteFile";
           this.chkDeleteFile.Size = new System.Drawing.Size(186, 17);
           this.chkDeleteFile.TabIndex = 14;
           this.chkDeleteFile.Text = "删除任务的同时删除相应文件";
           this.chkDeleteFile.UseVisualStyleBackColor = true;
           // 
           // chkPlaySound
           // 
           this.chkPlaySound.AutoSize = true;
           this.chkPlaySound.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.chkPlaySound.Location = new System.Drawing.Point(27, 89);
           this.chkPlaySound.Name = "chkPlaySound";
           this.chkPlaySound.Size = new System.Drawing.Size(150, 17);
           this.chkPlaySound.TabIndex = 13;
           this.chkPlaySound.Text = "下载完成后播放提示音";
           this.chkPlaySound.UseVisualStyleBackColor = true;
           // 
           // chkOpenFolder
           // 
           this.chkOpenFolder.AutoSize = true;
           this.chkOpenFolder.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.chkOpenFolder.Location = new System.Drawing.Point(27, 67);
           this.chkOpenFolder.Name = "chkOpenFolder";
           this.chkOpenFolder.Size = new System.Drawing.Size(150, 17);
           this.chkOpenFolder.TabIndex = 12;
           this.chkOpenFolder.Text = "下载完成后打开文件夹";
           this.chkOpenFolder.UseVisualStyleBackColor = true;
           // 
           // chkDownAllParts
           // 
           this.chkDownAllParts.AutoSize = true;
           this.chkDownAllParts.Enabled = false;
           this.chkDownAllParts.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.chkDownAllParts.Location = new System.Drawing.Point(27, 45);
           this.chkDownAllParts.Name = "chkDownAllParts";
           this.chkDownAllParts.Size = new System.Drawing.Size(174, 17);
           this.chkDownAllParts.TabIndex = 11;
           this.chkDownAllParts.Text = "自动下载所有相关联的章节";
           this.chkDownAllParts.UseVisualStyleBackColor = true;
           // 
           // chkDownSub
           // 
           this.chkDownSub.AutoSize = true;
           this.chkDownSub.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.chkDownSub.Location = new System.Drawing.Point(27, 22);
           this.chkDownSub.Name = "chkDownSub";
           this.chkDownSub.Size = new System.Drawing.Size(102, 17);
           this.chkDownSub.TabIndex = 10;
           this.chkDownSub.Text = "下载字幕文件";
           this.chkDownSub.UseVisualStyleBackColor = true;
           // 
           // pageUI
           // 
           this.pageUI.Controls.Add(this.txtSearchText);
           this.pageUI.Controls.Add(this.label4);
           this.pageUI.Controls.Add(this.chkShowBigButton);
           this.pageUI.Controls.Add(this.chkEnableWin7);
           this.pageUI.Controls.Add(this.chkCheckUrl);
           this.pageUI.Controls.Add(this.chkWatch);
           this.pageUI.Location = new System.Drawing.Point(4, 22);
           this.pageUI.Name = "pageUI";
           this.pageUI.Padding = new System.Windows.Forms.Padding(3);
           this.pageUI.Size = new System.Drawing.Size(398, 318);
           this.pageUI.TabIndex = 3;
           this.pageUI.Text = "界面";
           this.pageUI.UseVisualStyleBackColor = true;
           // 
           // txtSearchText
           // 
           this.txtSearchText.Location = new System.Drawing.Point(26, 172);
           this.txtSearchText.Name = "txtSearchText";
           this.txtSearchText.Size = new System.Drawing.Size(325, 21);
           this.txtSearchText.TabIndex = 20;
           // 
           // label4
           // 
           this.label4.AutoSize = true;
           this.label4.Location = new System.Drawing.Point(24, 134);
           this.label4.Name = "label4";
           this.label4.Size = new System.Drawing.Size(185, 24);
           this.label4.TabIndex = 19;
           this.label4.Text = "自定义搜索引擎:\r\n(请用%TEST%替换要搜索的字符串)";
           // 
           // chkShowBigButton
           // 
           this.chkShowBigButton.AutoSize = true;
           this.chkShowBigButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.chkShowBigButton.Location = new System.Drawing.Point(26, 50);
           this.chkShowBigButton.Name = "chkShowBigButton";
           this.chkShowBigButton.Size = new System.Drawing.Size(138, 17);
           this.chkShowBigButton.TabIndex = 18;
           this.chkShowBigButton.Text = "显示\"新建任务\"按钮";
           this.chkShowBigButton.UseVisualStyleBackColor = true;
           // 
           // chkEnableWin7
           // 
           this.chkEnableWin7.AutoSize = true;
           this.chkEnableWin7.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.chkEnableWin7.Location = new System.Drawing.Point(26, 27);
           this.chkEnableWin7.Name = "chkEnableWin7";
           this.chkEnableWin7.Size = new System.Drawing.Size(132, 17);
           this.chkEnableWin7.TabIndex = 17;
           this.chkEnableWin7.Text = "启用Windows 7特性";
           this.chkEnableWin7.UseVisualStyleBackColor = true;
           // 
           // chkCheckUrl
           // 
           this.chkCheckUrl.AutoSize = true;
           this.chkCheckUrl.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.chkCheckUrl.Location = new System.Drawing.Point(26, 73);
           this.chkCheckUrl.Name = "chkCheckUrl";
           this.chkCheckUrl.Size = new System.Drawing.Size(108, 17);
           this.chkCheckUrl.TabIndex = 16;
           this.chkCheckUrl.Text = "检查输入的Url";
           this.chkCheckUrl.UseVisualStyleBackColor = true;
           // 
           // chkWatch
           // 
           this.chkWatch.AutoSize = true;
           this.chkWatch.Enabled = false;
           this.chkWatch.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.chkWatch.Location = new System.Drawing.Point(26, 96);
           this.chkWatch.Name = "chkWatch";
           this.chkWatch.Size = new System.Drawing.Size(90, 17);
           this.chkWatch.TabIndex = 15;
           this.chkWatch.Text = "监视剪贴板";
           this.chkWatch.UseVisualStyleBackColor = true;
           // 
           // pagePlugin
           // 
           this.pagePlugin.Location = new System.Drawing.Point(4, 22);
           this.pagePlugin.Name = "pagePlugin";
           this.pagePlugin.Padding = new System.Windows.Forms.Padding(3);
           this.pagePlugin.Size = new System.Drawing.Size(398, 318);
           this.pagePlugin.TabIndex = 4;
           this.pagePlugin.Text = "插件";
           this.pagePlugin.UseVisualStyleBackColor = true;
           // 
           // FormConfig
           // 
           this.AcceptButton = this.btnOK;
           this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
           this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           this.CancelButton = this.btnCancel;
           this.ClientSize = new System.Drawing.Size(406, 387);
           this.Controls.Add(this.tab);
           this.Controls.Add(this.btnOK);
           this.Controls.Add(this.btnCancel);
           this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
           this.MaximizeBox = false;
           this.MinimizeBox = false;
           this.Name = "FormConfig";
           this.ShowIcon = false;
           this.ShowInTaskbar = false;
           this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
           this.Text = "AcDown动漫下载器 设置";
           this.Load += new System.EventHandler(this.FormConfig_Load);
           this.tab.ResumeLayout(false);
           this.pageGeneral.ResumeLayout(false);
           this.pageGeneral.PerformLayout();
           this.pageDownload.ResumeLayout(false);
           this.pageDownload.PerformLayout();
           ((System.ComponentModel.ISupportInitialize)(this.numCacheSize)).EndInit();
           this.pageUI.ResumeLayout(false);
           this.pageUI.PerformLayout();
           this.ResumeLayout(false);

		  }

		  #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage pageGeneral;
        private System.Windows.Forms.TabPage pageDownload;
        private System.Windows.Forms.CheckBox chkDeleteFile;
        private System.Windows.Forms.CheckBox chkPlaySound;
        private System.Windows.Forms.CheckBox chkOpenFolder;
        private System.Windows.Forms.CheckBox chkDownAllParts;
        private System.Windows.Forms.CheckBox chkDownSub;
        private System.Windows.Forms.TabPage pageUI;
        private System.Windows.Forms.TabPage pagePlugin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel lnkSavePath;
        private System.Windows.Forms.LinkLabel lnkLog;
        private System.Windows.Forms.CheckBox chkEnableLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numCacheSize;
        private System.Windows.Forms.TextBox txtSearchText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkShowBigButton;
        private System.Windows.Forms.CheckBox chkEnableWin7;
        private System.Windows.Forms.CheckBox chkCheckUrl;
        private System.Windows.Forms.CheckBox chkWatch;
	 }
}