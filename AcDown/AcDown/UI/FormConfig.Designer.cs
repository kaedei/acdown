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
           this.chkDownSub = new System.Windows.Forms.CheckBox();
           this.pageUI = new System.Windows.Forms.TabPage();
           this.lnkCustomSearchExample = new System.Windows.Forms.LinkLabel();
           this.txtSearchText = new System.Windows.Forms.ComboBox();
           this.label4 = new System.Windows.Forms.Label();
           this.chkShowBigButton = new System.Windows.Forms.CheckBox();
           this.chkEnableWin7 = new System.Windows.Forms.CheckBox();
           this.chkCheckUrl = new System.Windows.Forms.CheckBox();
           this.chkWatch = new System.Windows.Forms.CheckBox();
           this.pagePlugin = new System.Windows.Forms.TabPage();
           this.chkPluginImanhua = new System.Windows.Forms.CheckBox();
           this.label3 = new System.Windows.Forms.Label();
           this.chkPluginYouku = new System.Windows.Forms.CheckBox();
           this.chkPluginBilibili = new System.Windows.Forms.CheckBox();
           this.chkPluginTudou = new System.Windows.Forms.CheckBox();
           this.chkPluginAcfun = new System.Windows.Forms.CheckBox();
           this.chkDownAllSection = new System.Windows.Forms.CheckBox();
           this.tab.SuspendLayout();
           this.pageGeneral.SuspendLayout();
           this.pageDownload.SuspendLayout();
           ((System.ComponentModel.ISupportInitialize)(this.numCacheSize)).BeginInit();
           this.pageUI.SuspendLayout();
           this.pagePlugin.SuspendLayout();
           this.SuspendLayout();
           // 
           // btnCancel
           // 
           this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
           this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
           this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.btnCancel.Location = new System.Drawing.Point(327, 352);
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
           this.btnOK.Location = new System.Drawing.Point(246, 352);
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
           this.tab.Location = new System.Drawing.Point(8, 8);
           this.tab.Name = "tab";
           this.tab.SelectedIndex = 0;
           this.tab.ShowToolTips = true;
           this.tab.Size = new System.Drawing.Size(394, 338);
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
           this.pageGeneral.Size = new System.Drawing.Size(386, 312);
           this.pageGeneral.TabIndex = 1;
           this.pageGeneral.Text = "常规";
           this.pageGeneral.UseVisualStyleBackColor = true;
           // 
           // label2
           // 
           this.label2.AutoSize = true;
           this.label2.Location = new System.Drawing.Point(38, 46);
           this.label2.Name = "label2";
           this.label2.Size = new System.Drawing.Size(101, 12);
           this.label2.TabIndex = 8;
           this.label2.Text = "默认保存文件夹：";
           // 
           // lnkSavePath
           // 
           this.lnkSavePath.AutoSize = true;
           this.lnkSavePath.Location = new System.Drawing.Point(38, 70);
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
           this.lnkLog.Location = new System.Drawing.Point(51, 143);
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
           this.chkEnableLog.Location = new System.Drawing.Point(40, 119);
           this.chkEnableLog.Name = "chkEnableLog";
           this.chkEnableLog.Size = new System.Drawing.Size(222, 17);
           this.chkEnableLog.TabIndex = 6;
           this.chkEnableLog.Text = "启用错误日志（重启下载器后生效）";
           this.chkEnableLog.UseVisualStyleBackColor = true;
           // 
           // pageDownload
           // 
           this.pageDownload.Controls.Add(this.chkDownAllSection);
           this.pageDownload.Controls.Add(this.label1);
           this.pageDownload.Controls.Add(this.numCacheSize);
           this.pageDownload.Controls.Add(this.chkDeleteFile);
           this.pageDownload.Controls.Add(this.chkPlaySound);
           this.pageDownload.Controls.Add(this.chkOpenFolder);
           this.pageDownload.Controls.Add(this.chkDownSub);
           this.pageDownload.Location = new System.Drawing.Point(4, 22);
           this.pageDownload.Name = "pageDownload";
           this.pageDownload.Padding = new System.Windows.Forms.Padding(3);
           this.pageDownload.Size = new System.Drawing.Size(386, 312);
           this.pageDownload.TabIndex = 2;
           this.pageDownload.Text = "下载";
           this.pageDownload.UseVisualStyleBackColor = true;
           // 
           // label1
           // 
           this.label1.AutoSize = true;
           this.label1.Location = new System.Drawing.Point(37, 176);
           this.label1.Name = "label1";
           this.label1.Size = new System.Drawing.Size(113, 12);
           this.label1.TabIndex = 16;
           this.label1.Text = "缓存大小：(1-16MB)";
           // 
           // numCacheSize
           // 
           this.numCacheSize.Location = new System.Drawing.Point(156, 174);
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
           this.chkDeleteFile.Location = new System.Drawing.Point(39, 107);
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
           this.chkPlaySound.Location = new System.Drawing.Point(39, 84);
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
           this.chkOpenFolder.Location = new System.Drawing.Point(39, 62);
           this.chkOpenFolder.Name = "chkOpenFolder";
           this.chkOpenFolder.Size = new System.Drawing.Size(150, 17);
           this.chkOpenFolder.TabIndex = 12;
           this.chkOpenFolder.Text = "下载完成后打开文件夹";
           this.chkOpenFolder.UseVisualStyleBackColor = true;
           // 
           // chkDownSub
           // 
           this.chkDownSub.AutoSize = true;
           this.chkDownSub.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.chkDownSub.Location = new System.Drawing.Point(39, 39);
           this.chkDownSub.Name = "chkDownSub";
           this.chkDownSub.Size = new System.Drawing.Size(102, 17);
           this.chkDownSub.TabIndex = 10;
           this.chkDownSub.Text = "下载字幕文件";
           this.chkDownSub.UseVisualStyleBackColor = true;
           // 
           // pageUI
           // 
           this.pageUI.Controls.Add(this.lnkCustomSearchExample);
           this.pageUI.Controls.Add(this.txtSearchText);
           this.pageUI.Controls.Add(this.label4);
           this.pageUI.Controls.Add(this.chkShowBigButton);
           this.pageUI.Controls.Add(this.chkEnableWin7);
           this.pageUI.Controls.Add(this.chkCheckUrl);
           this.pageUI.Controls.Add(this.chkWatch);
           this.pageUI.Location = new System.Drawing.Point(4, 22);
           this.pageUI.Name = "pageUI";
           this.pageUI.Padding = new System.Windows.Forms.Padding(3);
           this.pageUI.Size = new System.Drawing.Size(386, 312);
           this.pageUI.TabIndex = 3;
           this.pageUI.Text = "界面";
           this.pageUI.UseVisualStyleBackColor = true;
           // 
           // lnkCustomSearchExample
           // 
           this.lnkCustomSearchExample.AutoSize = true;
           this.lnkCustomSearchExample.Location = new System.Drawing.Point(279, 145);
           this.lnkCustomSearchExample.Name = "lnkCustomSearchExample";
           this.lnkCustomSearchExample.Size = new System.Drawing.Size(77, 12);
           this.lnkCustomSearchExample.TabIndex = 22;
           this.lnkCustomSearchExample.TabStop = true;
           this.lnkCustomSearchExample.Text = "查看更多示例";
           this.lnkCustomSearchExample.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCustomSearchExample_LinkClicked);
           // 
           // txtSearchText
           // 
           this.txtSearchText.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.txtSearchText.FormattingEnabled = true;
           this.txtSearchText.Items.AddRange(new object[] {
            "Acfun站内搜索 - Google",
            "土豆网",
            "优酷搜索(搜酷)",
            "漫画搜索(爱漫画)"});
           this.txtSearchText.Location = new System.Drawing.Point(29, 173);
           this.txtSearchText.Name = "txtSearchText";
           this.txtSearchText.Size = new System.Drawing.Size(327, 20);
           this.txtSearchText.TabIndex = 21;
           // 
           // label4
           // 
           this.label4.AutoSize = true;
           this.label4.Location = new System.Drawing.Point(27, 133);
           this.label4.Name = "label4";
           this.label4.Size = new System.Drawing.Size(185, 24);
           this.label4.TabIndex = 19;
           this.label4.Text = "自定义搜索引擎:\r\n(请用%TEST%替换要搜索的字符串)";
           // 
           // chkShowBigButton
           // 
           this.chkShowBigButton.AutoSize = true;
           this.chkShowBigButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.chkShowBigButton.Location = new System.Drawing.Point(29, 51);
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
           this.chkEnableWin7.Location = new System.Drawing.Point(29, 28);
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
           this.chkCheckUrl.Location = new System.Drawing.Point(29, 74);
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
           this.chkWatch.Location = new System.Drawing.Point(29, 97);
           this.chkWatch.Name = "chkWatch";
           this.chkWatch.Size = new System.Drawing.Size(90, 17);
           this.chkWatch.TabIndex = 15;
           this.chkWatch.Text = "监视剪贴板";
           this.chkWatch.UseVisualStyleBackColor = true;
           // 
           // pagePlugin
           // 
           this.pagePlugin.Controls.Add(this.chkPluginImanhua);
           this.pagePlugin.Controls.Add(this.label3);
           this.pagePlugin.Controls.Add(this.chkPluginYouku);
           this.pagePlugin.Controls.Add(this.chkPluginBilibili);
           this.pagePlugin.Controls.Add(this.chkPluginTudou);
           this.pagePlugin.Controls.Add(this.chkPluginAcfun);
           this.pagePlugin.Location = new System.Drawing.Point(4, 22);
           this.pagePlugin.Name = "pagePlugin";
           this.pagePlugin.Padding = new System.Windows.Forms.Padding(3);
           this.pagePlugin.Size = new System.Drawing.Size(386, 312);
           this.pagePlugin.TabIndex = 4;
           this.pagePlugin.Text = "插件";
           this.pagePlugin.UseVisualStyleBackColor = true;
           // 
           // chkPluginImanhua
           // 
           this.chkPluginImanhua.AutoSize = true;
           this.chkPluginImanhua.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.chkPluginImanhua.Location = new System.Drawing.Point(41, 143);
           this.chkPluginImanhua.Name = "chkPluginImanhua";
           this.chkPluginImanhua.Size = new System.Drawing.Size(150, 17);
           this.chkPluginImanhua.TabIndex = 5;
           this.chkPluginImanhua.Text = "启用爱漫画网下载插件";
           this.chkPluginImanhua.UseVisualStyleBackColor = true;
           // 
           // label3
           // 
           this.label3.AutoSize = true;
           this.label3.Location = new System.Drawing.Point(25, 27);
           this.label3.Name = "label3";
           this.label3.Size = new System.Drawing.Size(173, 12);
           this.label3.TabIndex = 4;
           this.label3.Text = "设置插件（重启下载器后生效）";
           // 
           // chkPluginYouku
           // 
           this.chkPluginYouku.AutoSize = true;
           this.chkPluginYouku.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.chkPluginYouku.Location = new System.Drawing.Point(41, 121);
           this.chkPluginYouku.Name = "chkPluginYouku";
           this.chkPluginYouku.Size = new System.Drawing.Size(138, 17);
           this.chkPluginYouku.TabIndex = 3;
           this.chkPluginYouku.Text = "启用优酷网下载插件";
           this.chkPluginYouku.UseVisualStyleBackColor = true;
           // 
           // chkPluginBilibili
           // 
           this.chkPluginBilibili.AutoSize = true;
           this.chkPluginBilibili.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.chkPluginBilibili.Location = new System.Drawing.Point(41, 99);
           this.chkPluginBilibili.Name = "chkPluginBilibili";
           this.chkPluginBilibili.Size = new System.Drawing.Size(168, 17);
           this.chkPluginBilibili.TabIndex = 2;
           this.chkPluginBilibili.Text = "启用Bilibili.us下载插件";
           this.chkPluginBilibili.UseVisualStyleBackColor = true;
           // 
           // chkPluginTudou
           // 
           this.chkPluginTudou.AutoSize = true;
           this.chkPluginTudou.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.chkPluginTudou.Location = new System.Drawing.Point(41, 77);
           this.chkPluginTudou.Name = "chkPluginTudou";
           this.chkPluginTudou.Size = new System.Drawing.Size(138, 17);
           this.chkPluginTudou.TabIndex = 1;
           this.chkPluginTudou.Text = "启用土豆网下载插件";
           this.chkPluginTudou.UseVisualStyleBackColor = true;
           // 
           // chkPluginAcfun
           // 
           this.chkPluginAcfun.AutoSize = true;
           this.chkPluginAcfun.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.chkPluginAcfun.Location = new System.Drawing.Point(41, 55);
           this.chkPluginAcfun.Name = "chkPluginAcfun";
           this.chkPluginAcfun.Size = new System.Drawing.Size(150, 17);
           this.chkPluginAcfun.TabIndex = 0;
           this.chkPluginAcfun.Text = "启用Acfun.cn下载插件";
           this.chkPluginAcfun.UseVisualStyleBackColor = true;
           // 
           // chkDownAllSection
           // 
           this.chkDownAllSection.AutoSize = true;
           this.chkDownAllSection.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.chkDownAllSection.Location = new System.Drawing.Point(39, 130);
           this.chkDownAllSection.Name = "chkDownAllSection";
           this.chkDownAllSection.Size = new System.Drawing.Size(150, 17);
           this.chkDownAllSection.TabIndex = 17;
           this.chkDownAllSection.Text = "解析所有关联的下载项";
           this.chkDownAllSection.UseVisualStyleBackColor = true;
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
           this.pagePlugin.ResumeLayout(false);
           this.pagePlugin.PerformLayout();
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
        private System.Windows.Forms.CheckBox chkDownSub;
        private System.Windows.Forms.TabPage pageUI;
        private System.Windows.Forms.TabPage pagePlugin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel lnkSavePath;
        private System.Windows.Forms.LinkLabel lnkLog;
        private System.Windows.Forms.CheckBox chkEnableLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numCacheSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkShowBigButton;
        private System.Windows.Forms.CheckBox chkEnableWin7;
        private System.Windows.Forms.CheckBox chkCheckUrl;
        private System.Windows.Forms.CheckBox chkWatch;
        private System.Windows.Forms.ComboBox txtSearchText;
        private System.Windows.Forms.CheckBox chkPluginYouku;
        private System.Windows.Forms.CheckBox chkPluginBilibili;
        private System.Windows.Forms.CheckBox chkPluginTudou;
        private System.Windows.Forms.CheckBox chkPluginAcfun;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel lnkCustomSearchExample;
        private System.Windows.Forms.CheckBox chkPluginImanhua;
        private System.Windows.Forms.CheckBox chkDownAllSection;
	 }
}