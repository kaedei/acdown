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
           this.pageDownload = new System.Windows.Forms.TabPage();
           this.txtSavePath = new System.Windows.Forms.TextBox();
           this.btnSavePath = new System.Windows.Forms.Button();
           this.cboMaxRunningCount = new System.Windows.Forms.ComboBox();
           this.label5 = new System.Windows.Forms.Label();
           this.label2 = new System.Windows.Forms.Label();
           this.chkDownAllSection = new System.Windows.Forms.CheckBox();
           this.label1 = new System.Windows.Forms.Label();
           this.numCacheSize = new System.Windows.Forms.NumericUpDown();
           this.chkDeleteFile = new System.Windows.Forms.CheckBox();
           this.chkPlaySound = new System.Windows.Forms.CheckBox();
           this.chkOpenFolder = new System.Windows.Forms.CheckBox();
           this.chkDownSub = new System.Windows.Forms.CheckBox();
           this.pageUI = new System.Windows.Forms.TabPage();
           this.udRefreshInfo = new System.Windows.Forms.NumericUpDown();
           this.label7 = new System.Windows.Forms.Label();
           this.chkHideWhenClose = new System.Windows.Forms.CheckBox();
           this.lnkCustomSearchExample = new System.Windows.Forms.LinkLabel();
           this.txtSearchText = new System.Windows.Forms.ComboBox();
           this.label4 = new System.Windows.Forms.Label();
           this.chkShowBigButton = new System.Windows.Forms.CheckBox();
           this.chkEnableWin7 = new System.Windows.Forms.CheckBox();
           this.chkCheckUrl = new System.Windows.Forms.CheckBox();
           this.chkWatch = new System.Windows.Forms.CheckBox();
           this.pagePlugin = new System.Windows.Forms.TabPage();
           this.chkPluginTiebaAlbum = new System.Windows.Forms.CheckBox();
           this.chkPluginImanhua = new System.Windows.Forms.CheckBox();
           this.label3 = new System.Windows.Forms.Label();
           this.chkPluginYouku = new System.Windows.Forms.CheckBox();
           this.chkPluginBilibili = new System.Windows.Forms.CheckBox();
           this.chkPluginTudou = new System.Windows.Forms.CheckBox();
           this.chkPluginAcfun = new System.Windows.Forms.CheckBox();
           this.pageProxy = new System.Windows.Forms.TabPage();
           this.btnProxyDelete = new System.Windows.Forms.Button();
           this.btnProxyModify = new System.Windows.Forms.Button();
           this.btnProxyAdd = new System.Windows.Forms.Button();
           this.lsvProxy = new System.Windows.Forms.ListView();
           this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
           this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
           this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
           this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
           this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
           this.label6 = new System.Windows.Forms.Label();
           this.pageDebug = new System.Windows.Forms.TabPage();
           this.lnkLog = new System.Windows.Forms.LinkLabel();
           this.chkEnableLog = new System.Windows.Forms.CheckBox();
           this.chkFlvcd = new System.Windows.Forms.CheckBox();
           this.tab.SuspendLayout();
           this.pageDownload.SuspendLayout();
           ((System.ComponentModel.ISupportInitialize)(this.numCacheSize)).BeginInit();
           this.pageUI.SuspendLayout();
           ((System.ComponentModel.ISupportInitialize)(this.udRefreshInfo)).BeginInit();
           this.pagePlugin.SuspendLayout();
           this.pageProxy.SuspendLayout();
           this.pageDebug.SuspendLayout();
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
           this.tab.Controls.Add(this.pageDownload);
           this.tab.Controls.Add(this.pageUI);
           this.tab.Controls.Add(this.pagePlugin);
           this.tab.Controls.Add(this.pageProxy);
           this.tab.Controls.Add(this.pageDebug);
           this.tab.Location = new System.Drawing.Point(8, 8);
           this.tab.Name = "tab";
           this.tab.SelectedIndex = 0;
           this.tab.ShowToolTips = true;
           this.tab.Size = new System.Drawing.Size(394, 338);
           this.tab.TabIndex = 4;
           this.tab.TabStop = false;
           // 
           // pageDownload
           // 
           this.pageDownload.Controls.Add(this.txtSavePath);
           this.pageDownload.Controls.Add(this.btnSavePath);
           this.pageDownload.Controls.Add(this.cboMaxRunningCount);
           this.pageDownload.Controls.Add(this.label5);
           this.pageDownload.Controls.Add(this.label2);
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
           // txtSavePath
           // 
           this.txtSavePath.Location = new System.Drawing.Point(34, 51);
           this.txtSavePath.Name = "txtSavePath";
           this.txtSavePath.ReadOnly = true;
           this.txtSavePath.Size = new System.Drawing.Size(242, 21);
           this.txtSavePath.TabIndex = 23;
           // 
           // btnSavePath
           // 
           this.btnSavePath.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.btnSavePath.Location = new System.Drawing.Point(282, 51);
           this.btnSavePath.Name = "btnSavePath";
           this.btnSavePath.Size = new System.Drawing.Size(68, 21);
           this.btnSavePath.TabIndex = 22;
           this.btnSavePath.Text = "更改";
           this.btnSavePath.UseVisualStyleBackColor = true;
           this.btnSavePath.Click += new System.EventHandler(this.btnSavePath_Click);
           // 
           // cboMaxRunningCount
           // 
           this.cboMaxRunningCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
           this.cboMaxRunningCount.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.cboMaxRunningCount.FormattingEnabled = true;
           this.cboMaxRunningCount.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
           this.cboMaxRunningCount.Location = new System.Drawing.Point(170, 247);
           this.cboMaxRunningCount.Name = "cboMaxRunningCount";
           this.cboMaxRunningCount.Size = new System.Drawing.Size(51, 20);
           this.cboMaxRunningCount.TabIndex = 21;
           // 
           // label5
           // 
           this.label5.AutoSize = true;
           this.label5.Location = new System.Drawing.Point(33, 250);
           this.label5.Name = "label5";
           this.label5.Size = new System.Drawing.Size(131, 12);
           this.label5.TabIndex = 20;
           this.label5.Text = "同时运行的最大任务数:";
           // 
           // label2
           // 
           this.label2.AutoSize = true;
           this.label2.Location = new System.Drawing.Point(33, 30);
           this.label2.Name = "label2";
           this.label2.Size = new System.Drawing.Size(101, 12);
           this.label2.TabIndex = 19;
           this.label2.Text = "默认保存文件夹：";
           // 
           // chkDownAllSection
           // 
           this.chkDownAllSection.AutoSize = true;
           this.chkDownAllSection.Enabled = false;
           this.chkDownAllSection.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.chkDownAllSection.Location = new System.Drawing.Point(35, 182);
           this.chkDownAllSection.Name = "chkDownAllSection";
           this.chkDownAllSection.Size = new System.Drawing.Size(150, 17);
           this.chkDownAllSection.TabIndex = 17;
           this.chkDownAllSection.Text = "解析所有关联的下载项";
           this.chkDownAllSection.UseVisualStyleBackColor = true;
           // 
           // label1
           // 
           this.label1.AutoSize = true;
           this.label1.Location = new System.Drawing.Point(33, 214);
           this.label1.Name = "label1";
           this.label1.Size = new System.Drawing.Size(113, 12);
           this.label1.TabIndex = 16;
           this.label1.Text = "缓存大小：(1-16MB)";
           // 
           // numCacheSize
           // 
           this.numCacheSize.Location = new System.Drawing.Point(152, 212);
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
           this.chkDeleteFile.Location = new System.Drawing.Point(35, 159);
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
           this.chkPlaySound.Location = new System.Drawing.Point(35, 136);
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
           this.chkOpenFolder.Location = new System.Drawing.Point(35, 114);
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
           this.chkDownSub.Location = new System.Drawing.Point(35, 91);
           this.chkDownSub.Name = "chkDownSub";
           this.chkDownSub.Size = new System.Drawing.Size(102, 17);
           this.chkDownSub.TabIndex = 10;
           this.chkDownSub.Text = "下载字幕文件";
           this.chkDownSub.UseVisualStyleBackColor = true;
           // 
           // pageUI
           // 
           this.pageUI.Controls.Add(this.udRefreshInfo);
           this.pageUI.Controls.Add(this.label7);
           this.pageUI.Controls.Add(this.chkHideWhenClose);
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
           // udRefreshInfo
           // 
           this.udRefreshInfo.Location = new System.Drawing.Point(181, 147);
           this.udRefreshInfo.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
           this.udRefreshInfo.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
           this.udRefreshInfo.Name = "udRefreshInfo";
           this.udRefreshInfo.Size = new System.Drawing.Size(57, 21);
           this.udRefreshInfo.TabIndex = 25;
           this.udRefreshInfo.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
           // 
           // label7
           // 
           this.label7.AutoSize = true;
           this.label7.Location = new System.Drawing.Point(26, 149);
           this.label7.Name = "label7";
           this.label7.Size = new System.Drawing.Size(149, 12);
           this.label7.TabIndex = 24;
           this.label7.Text = "下载信息刷新频率(毫秒)：";
           // 
           // chkHideWhenClose
           // 
           this.chkHideWhenClose.AutoSize = true;
           this.chkHideWhenClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.chkHideWhenClose.Location = new System.Drawing.Point(28, 120);
           this.chkHideWhenClose.Name = "chkHideWhenClose";
           this.chkHideWhenClose.Size = new System.Drawing.Size(210, 17);
           this.chkHideWhenClose.TabIndex = 23;
           this.chkHideWhenClose.Text = "点击\"关闭\"时隐藏程序到系统托盘";
           this.chkHideWhenClose.UseVisualStyleBackColor = true;
           // 
           // lnkCustomSearchExample
           // 
           this.lnkCustomSearchExample.AutoSize = true;
           this.lnkCustomSearchExample.Location = new System.Drawing.Point(278, 195);
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
           this.txtSearchText.Location = new System.Drawing.Point(28, 223);
           this.txtSearchText.Name = "txtSearchText";
           this.txtSearchText.Size = new System.Drawing.Size(327, 20);
           this.txtSearchText.TabIndex = 21;
           // 
           // label4
           // 
           this.label4.AutoSize = true;
           this.label4.Location = new System.Drawing.Point(26, 183);
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
           this.pagePlugin.Controls.Add(this.chkFlvcd);
           this.pagePlugin.Controls.Add(this.chkPluginTiebaAlbum);
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
           // chkPluginTiebaAlbum
           // 
           this.chkPluginTiebaAlbum.AutoSize = true;
           this.chkPluginTiebaAlbum.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.chkPluginTiebaAlbum.Location = new System.Drawing.Point(41, 166);
           this.chkPluginTiebaAlbum.Name = "chkPluginTiebaAlbum";
           this.chkPluginTiebaAlbum.Size = new System.Drawing.Size(174, 17);
           this.chkPluginTiebaAlbum.TabIndex = 6;
           this.chkPluginTiebaAlbum.Text = "启用百度贴吧相册下载插件";
           this.chkPluginTiebaAlbum.UseVisualStyleBackColor = true;
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
           this.chkPluginBilibili.Size = new System.Drawing.Size(150, 17);
           this.chkPluginBilibili.TabIndex = 2;
           this.chkPluginBilibili.Text = "启用Bilibili下载插件";
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
           this.chkPluginAcfun.Size = new System.Drawing.Size(132, 17);
           this.chkPluginAcfun.TabIndex = 0;
           this.chkPluginAcfun.Text = "启用Acfun下载插件";
           this.chkPluginAcfun.UseVisualStyleBackColor = true;
           // 
           // pageProxy
           // 
           this.pageProxy.Controls.Add(this.btnProxyDelete);
           this.pageProxy.Controls.Add(this.btnProxyModify);
           this.pageProxy.Controls.Add(this.btnProxyAdd);
           this.pageProxy.Controls.Add(this.lsvProxy);
           this.pageProxy.Controls.Add(this.label6);
           this.pageProxy.Location = new System.Drawing.Point(4, 22);
           this.pageProxy.Name = "pageProxy";
           this.pageProxy.Padding = new System.Windows.Forms.Padding(3);
           this.pageProxy.Size = new System.Drawing.Size(386, 312);
           this.pageProxy.TabIndex = 6;
           this.pageProxy.Text = "代理服务器";
           this.pageProxy.UseVisualStyleBackColor = true;
           // 
           // btnProxyDelete
           // 
           this.btnProxyDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.btnProxyDelete.Location = new System.Drawing.Point(183, 257);
           this.btnProxyDelete.Name = "btnProxyDelete";
           this.btnProxyDelete.Size = new System.Drawing.Size(75, 23);
           this.btnProxyDelete.TabIndex = 4;
           this.btnProxyDelete.Text = "删除";
           this.btnProxyDelete.UseVisualStyleBackColor = true;
           this.btnProxyDelete.Click += new System.EventHandler(this.btnProxyDelete_Click);
           // 
           // btnProxyModify
           // 
           this.btnProxyModify.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.btnProxyModify.Location = new System.Drawing.Point(102, 257);
           this.btnProxyModify.Name = "btnProxyModify";
           this.btnProxyModify.Size = new System.Drawing.Size(75, 23);
           this.btnProxyModify.TabIndex = 3;
           this.btnProxyModify.Text = "修改";
           this.btnProxyModify.UseVisualStyleBackColor = true;
           this.btnProxyModify.Click += new System.EventHandler(this.btnProxyModify_Click);
           // 
           // btnProxyAdd
           // 
           this.btnProxyAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.btnProxyAdd.Location = new System.Drawing.Point(21, 257);
           this.btnProxyAdd.Name = "btnProxyAdd";
           this.btnProxyAdd.Size = new System.Drawing.Size(75, 23);
           this.btnProxyAdd.TabIndex = 2;
           this.btnProxyAdd.Text = "添加";
           this.btnProxyAdd.UseVisualStyleBackColor = true;
           this.btnProxyAdd.Click += new System.EventHandler(this.btnProxyAdd_Click);
           // 
           // lsvProxy
           // 
           this.lsvProxy.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
           this.lsvProxy.FullRowSelect = true;
           this.lsvProxy.GridLines = true;
           this.lsvProxy.Location = new System.Drawing.Point(21, 43);
           this.lsvProxy.MultiSelect = false;
           this.lsvProxy.Name = "lsvProxy";
           this.lsvProxy.Size = new System.Drawing.Size(342, 208);
           this.lsvProxy.TabIndex = 1;
           this.lsvProxy.UseCompatibleStateImageBehavior = false;
           this.lsvProxy.View = System.Windows.Forms.View.Details;
           // 
           // columnHeader1
           // 
           this.columnHeader1.Text = "名称";
           this.columnHeader1.Width = 68;
           // 
           // columnHeader2
           // 
           this.columnHeader2.Text = "服务器";
           this.columnHeader2.Width = 93;
           // 
           // columnHeader3
           // 
           this.columnHeader3.Text = "端口";
           // 
           // columnHeader4
           // 
           this.columnHeader4.Text = "用户名";
           this.columnHeader4.Width = 75;
           // 
           // columnHeader5
           // 
           this.columnHeader5.Text = "密码";
           this.columnHeader5.Width = 75;
           // 
           // label6
           // 
           this.label6.AutoSize = true;
           this.label6.Location = new System.Drawing.Point(6, 15);
           this.label6.Name = "label6";
           this.label6.Size = new System.Drawing.Size(125, 12);
           this.label6.TabIndex = 0;
           this.label6.Text = "编辑代理服务器列表：";
           // 
           // pageDebug
           // 
           this.pageDebug.Controls.Add(this.lnkLog);
           this.pageDebug.Controls.Add(this.chkEnableLog);
           this.pageDebug.Location = new System.Drawing.Point(4, 22);
           this.pageDebug.Name = "pageDebug";
           this.pageDebug.Padding = new System.Windows.Forms.Padding(3);
           this.pageDebug.Size = new System.Drawing.Size(386, 312);
           this.pageDebug.TabIndex = 5;
           this.pageDebug.Text = "调试";
           this.pageDebug.UseVisualStyleBackColor = true;
           // 
           // lnkLog
           // 
           this.lnkLog.AutoSize = true;
           this.lnkLog.Location = new System.Drawing.Point(48, 57);
           this.lnkLog.Name = "lnkLog";
           this.lnkLog.Size = new System.Drawing.Size(77, 12);
           this.lnkLog.TabIndex = 7;
           this.lnkLog.TabStop = true;
           this.lnkLog.Text = "查看错误日志";
           this.lnkLog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLog_LinkClicked);
           // 
           // chkEnableLog
           // 
           this.chkEnableLog.AutoSize = true;
           this.chkEnableLog.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.chkEnableLog.Location = new System.Drawing.Point(37, 33);
           this.chkEnableLog.Name = "chkEnableLog";
           this.chkEnableLog.Size = new System.Drawing.Size(222, 17);
           this.chkEnableLog.TabIndex = 8;
           this.chkEnableLog.Text = "启用错误日志（重启下载器后生效）";
           this.chkEnableLog.UseVisualStyleBackColor = true;
           // 
           // chkFlvcd
           // 
           this.chkFlvcd.AutoSize = true;
           this.chkFlvcd.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.chkFlvcd.Location = new System.Drawing.Point(41, 189);
           this.chkFlvcd.Name = "chkFlvcd";
           this.chkFlvcd.Size = new System.Drawing.Size(138, 17);
           this.chkFlvcd.TabIndex = 7;
           this.chkFlvcd.Text = "启用FLVCD解析插件*";
           this.chkFlvcd.UseVisualStyleBackColor = true;
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
           this.pageDownload.ResumeLayout(false);
           this.pageDownload.PerformLayout();
           ((System.ComponentModel.ISupportInitialize)(this.numCacheSize)).EndInit();
           this.pageUI.ResumeLayout(false);
           this.pageUI.PerformLayout();
           ((System.ComponentModel.ISupportInitialize)(this.udRefreshInfo)).EndInit();
           this.pagePlugin.ResumeLayout(false);
           this.pagePlugin.PerformLayout();
           this.pageProxy.ResumeLayout(false);
           this.pageProxy.PerformLayout();
           this.pageDebug.ResumeLayout(false);
           this.pageDebug.PerformLayout();
           this.ResumeLayout(false);

		  }

		  #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage pageDownload;
        private System.Windows.Forms.CheckBox chkDeleteFile;
        private System.Windows.Forms.CheckBox chkPlaySound;
        private System.Windows.Forms.CheckBox chkOpenFolder;
        private System.Windows.Forms.CheckBox chkDownSub;
        private System.Windows.Forms.TabPage pageUI;
        private System.Windows.Forms.TabPage pagePlugin;
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
        private System.Windows.Forms.CheckBox chkPluginTiebaAlbum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkHideWhenClose;
        private System.Windows.Forms.TabPage pageDebug;
        private System.Windows.Forms.LinkLabel lnkLog;
        private System.Windows.Forms.CheckBox chkEnableLog;
        private System.Windows.Forms.ComboBox cboMaxRunningCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage pageProxy;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnProxyDelete;
        private System.Windows.Forms.Button btnProxyModify;
        private System.Windows.Forms.Button btnProxyAdd;
        private System.Windows.Forms.ListView lsvProxy;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.TextBox txtSavePath;
        private System.Windows.Forms.Button btnSavePath;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown udRefreshInfo;
        private System.Windows.Forms.CheckBox chkFlvcd;
	 }
}