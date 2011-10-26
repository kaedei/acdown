﻿namespace Kaedei.AcDown.UI
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
           this.components = new System.ComponentModel.Container();
           this.btnCancel = new System.Windows.Forms.Button();
           this.btnOK = new System.Windows.Forms.Button();
           this.tab = new System.Windows.Forms.TabControl();
           this.pageDownload = new System.Windows.Forms.TabPage();
           this.txtCustomSound = new System.Windows.Forms.TextBox();
           this.chkCustomSound = new System.Windows.Forms.CheckBox();
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
           this.chkShowLogo = new System.Windows.Forms.CheckBox();
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
           this.chkPluginSfacg = new System.Windows.Forms.CheckBox();
           this.chkPluginTucao = new System.Windows.Forms.CheckBox();
           this.lnkFlvcdIntro = new System.Windows.Forms.LinkLabel();
           this.chkPluginFlvcd = new System.Windows.Forms.CheckBox();
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
           this.tabUpdate = new System.Windows.Forms.TabPage();
           this.chkEnableCheckUpdate = new System.Windows.Forms.CheckBox();
           this.pageDebug = new System.Windows.Forms.TabPage();
           this.label8 = new System.Windows.Forms.Label();
           this.udNetworkTimeout = new System.Windows.Forms.NumericUpDown();
           this.lnkLog = new System.Windows.Forms.LinkLabel();
           this.chkEnableLog = new System.Windows.Forms.CheckBox();
           this.btnDefault = new System.Windows.Forms.Button();
           this.tip = new System.Windows.Forms.ToolTip(this.components);
           this.groupBox1 = new System.Windows.Forms.GroupBox();
           this.tab.SuspendLayout();
           this.pageDownload.SuspendLayout();
           ((System.ComponentModel.ISupportInitialize)(this.numCacheSize)).BeginInit();
           this.pageUI.SuspendLayout();
           ((System.ComponentModel.ISupportInitialize)(this.udRefreshInfo)).BeginInit();
           this.pagePlugin.SuspendLayout();
           this.pageProxy.SuspendLayout();
           this.tabUpdate.SuspendLayout();
           this.pageDebug.SuspendLayout();
           ((System.ComponentModel.ISupportInitialize)(this.udNetworkTimeout)).BeginInit();
           this.groupBox1.SuspendLayout();
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
           this.btnCancel.TabIndex = 1;
           this.btnCancel.Text = "取消";
           this.tip.SetToolTip(this.btnCancel, "取消更改并关闭此窗口");
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
           this.btnOK.TabIndex = 0;
           this.btnOK.Text = "确认";
           this.tip.SetToolTip(this.btnOK, "保存更改");
           this.btnOK.UseVisualStyleBackColor = true;
           this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
           // 
           // tab
           // 
           this.tab.Controls.Add(this.pageDownload);
           this.tab.Controls.Add(this.pageUI);
           this.tab.Controls.Add(this.pagePlugin);
           this.tab.Controls.Add(this.pageProxy);
           this.tab.Controls.Add(this.tabUpdate);
           this.tab.Controls.Add(this.pageDebug);
           this.tab.Location = new System.Drawing.Point(8, 8);
           this.tab.Name = "tab";
           this.tab.SelectedIndex = 0;
           this.tab.ShowToolTips = true;
           this.tab.Size = new System.Drawing.Size(394, 338);
           this.tab.TabIndex = 3;
           this.tab.TabStop = false;
           // 
           // pageDownload
           // 
           this.pageDownload.Controls.Add(this.txtCustomSound);
           this.pageDownload.Controls.Add(this.chkCustomSound);
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
           // txtCustomSound
           // 
           this.txtCustomSound.Enabled = false;
           this.txtCustomSound.Location = new System.Drawing.Point(164, 158);
           this.txtCustomSound.Name = "txtCustomSound";
           this.txtCustomSound.Size = new System.Drawing.Size(186, 21);
           this.txtCustomSound.TabIndex = 13;
           this.tip.SetToolTip(this.txtCustomSound, "点击修改提示音(.wav格式)");
           this.txtCustomSound.Click += new System.EventHandler(this.txtCustomSound_Click);
           // 
           // chkCustomSound
           // 
           this.chkCustomSound.AutoSize = true;
           this.chkCustomSound.Location = new System.Drawing.Point(56, 160);
           this.chkCustomSound.Name = "chkCustomSound";
           this.chkCustomSound.Size = new System.Drawing.Size(102, 16);
           this.chkCustomSound.TabIndex = 12;
           this.chkCustomSound.Text = "自定义提示音:";
           this.tip.SetToolTip(this.chkCustomSound, "替换系统默认的提示音");
           this.chkCustomSound.UseVisualStyleBackColor = true;
           this.chkCustomSound.CheckedChanged += new System.EventHandler(this.chkCustomSound_CheckedChanged);
           // 
           // txtSavePath
           // 
           this.txtSavePath.Location = new System.Drawing.Point(34, 51);
           this.txtSavePath.Name = "txtSavePath";
           this.txtSavePath.ReadOnly = true;
           this.txtSavePath.Size = new System.Drawing.Size(242, 21);
           this.txtSavePath.TabIndex = 1;
           // 
           // btnSavePath
           // 
           this.btnSavePath.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.btnSavePath.Location = new System.Drawing.Point(282, 51);
           this.btnSavePath.Name = "btnSavePath";
           this.btnSavePath.Size = new System.Drawing.Size(68, 21);
           this.btnSavePath.TabIndex = 2;
           this.btnSavePath.Text = "更改";
           this.tip.SetToolTip(this.btnSavePath, "下载任务的文件将被保存到一个指定文件夹中\r\n点击此按钮进行修改");
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
           this.cboMaxRunningCount.Location = new System.Drawing.Point(173, 256);
           this.cboMaxRunningCount.Name = "cboMaxRunningCount";
           this.cboMaxRunningCount.Size = new System.Drawing.Size(51, 20);
           this.cboMaxRunningCount.TabIndex = 11;
           this.tip.SetToolTip(this.cboMaxRunningCount, "同时可以进行的任务数量的最大值");
           // 
           // label5
           // 
           this.label5.AutoSize = true;
           this.label5.Location = new System.Drawing.Point(33, 259);
           this.label5.Name = "label5";
           this.label5.Size = new System.Drawing.Size(131, 12);
           this.label5.TabIndex = 10;
           this.label5.Text = "同时运行的最大任务数:";
           // 
           // label2
           // 
           this.label2.AutoSize = true;
           this.label2.Location = new System.Drawing.Point(33, 30);
           this.label2.Name = "label2";
           this.label2.Size = new System.Drawing.Size(101, 12);
           this.label2.TabIndex = 0;
           this.label2.Text = "默认保存文件夹：";
           // 
           // chkDownAllSection
           // 
           this.chkDownAllSection.AutoSize = true;
           this.chkDownAllSection.Enabled = false;
           this.chkDownAllSection.Location = new System.Drawing.Point(35, 207);
           this.chkDownAllSection.Name = "chkDownAllSection";
           this.chkDownAllSection.Size = new System.Drawing.Size(144, 16);
           this.chkDownAllSection.TabIndex = 7;
           this.chkDownAllSection.Text = "解析所有关联的下载项";
           this.chkDownAllSection.UseVisualStyleBackColor = true;
           // 
           // label1
           // 
           this.label1.AutoSize = true;
           this.label1.Location = new System.Drawing.Point(33, 231);
           this.label1.Name = "label1";
           this.label1.Size = new System.Drawing.Size(113, 12);
           this.label1.TabIndex = 8;
           this.label1.Text = "缓存大小: (1-16MB)";
           // 
           // numCacheSize
           // 
           this.numCacheSize.Location = new System.Drawing.Point(173, 229);
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
           this.numCacheSize.Size = new System.Drawing.Size(51, 21);
           this.numCacheSize.TabIndex = 9;
           this.tip.SetToolTip(this.numCacheSize, "下载时的缓存大小");
           this.numCacheSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
           // 
           // chkDeleteFile
           // 
           this.chkDeleteFile.AutoSize = true;
           this.chkDeleteFile.Location = new System.Drawing.Point(35, 185);
           this.chkDeleteFile.Name = "chkDeleteFile";
           this.chkDeleteFile.Size = new System.Drawing.Size(180, 16);
           this.chkDeleteFile.TabIndex = 6;
           this.chkDeleteFile.Text = "删除任务的同时删除相应文件";
           this.tip.SetToolTip(this.chkDeleteFile, "删除任务时，是否同时删除与任务有关的所有文件");
           this.chkDeleteFile.UseVisualStyleBackColor = true;
           // 
           // chkPlaySound
           // 
           this.chkPlaySound.AutoSize = true;
           this.chkPlaySound.Location = new System.Drawing.Point(35, 136);
           this.chkPlaySound.Name = "chkPlaySound";
           this.chkPlaySound.Size = new System.Drawing.Size(144, 16);
           this.chkPlaySound.TabIndex = 5;
           this.chkPlaySound.Text = "下载完成后播放提示音";
           this.tip.SetToolTip(this.chkPlaySound, "在每个下载完成后播放一个短暂的提示音");
           this.chkPlaySound.UseVisualStyleBackColor = true;
           this.chkPlaySound.CheckedChanged += new System.EventHandler(this.chkPlaySound_CheckedChanged);
           // 
           // chkOpenFolder
           // 
           this.chkOpenFolder.AutoSize = true;
           this.chkOpenFolder.Location = new System.Drawing.Point(35, 114);
           this.chkOpenFolder.Name = "chkOpenFolder";
           this.chkOpenFolder.Size = new System.Drawing.Size(144, 16);
           this.chkOpenFolder.TabIndex = 4;
           this.chkOpenFolder.Text = "下载完成后打开文件夹";
           this.tip.SetToolTip(this.chkOpenFolder, "下载完成后自动打开“Windows资源管理器”并浏览到下载的文件所在文件夹");
           this.chkOpenFolder.UseVisualStyleBackColor = true;
           // 
           // chkDownSub
           // 
           this.chkDownSub.AutoSize = true;
           this.chkDownSub.Location = new System.Drawing.Point(35, 91);
           this.chkDownSub.Name = "chkDownSub";
           this.chkDownSub.Size = new System.Drawing.Size(96, 16);
           this.chkDownSub.TabIndex = 3;
           this.chkDownSub.Text = "下载字幕文件";
           this.tip.SetToolTip(this.chkDownSub, "针对某些提供字幕/弹幕的网站的下载任务，\r\n启用此选项以允许下载插件下载相应的字幕文件");
           this.chkDownSub.UseVisualStyleBackColor = true;
           // 
           // pageUI
           // 
           this.pageUI.Controls.Add(this.groupBox1);
           this.pageUI.Controls.Add(this.chkShowLogo);
           this.pageUI.Controls.Add(this.udRefreshInfo);
           this.pageUI.Controls.Add(this.label7);
           this.pageUI.Controls.Add(this.chkHideWhenClose);
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
           // chkShowLogo
           // 
           this.chkShowLogo.AutoSize = true;
           this.chkShowLogo.Location = new System.Drawing.Point(29, 73);
           this.chkShowLogo.Name = "chkShowLogo";
           this.chkShowLogo.Size = new System.Drawing.Size(264, 16);
           this.chkShowLogo.TabIndex = 11;
           this.chkShowLogo.Text = "显示主界面的Logo图片（重启下载器后生效）";
           this.tip.SetToolTip(this.chkShowLogo, "显示界面中央的AcDown图片");
           this.chkShowLogo.UseVisualStyleBackColor = true;
           // 
           // udRefreshInfo
           // 
           this.udRefreshInfo.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
           this.udRefreshInfo.Location = new System.Drawing.Point(181, 170);
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
           this.udRefreshInfo.TabIndex = 7;
           this.udRefreshInfo.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
           // 
           // label7
           // 
           this.label7.AutoSize = true;
           this.label7.Location = new System.Drawing.Point(26, 172);
           this.label7.Name = "label7";
           this.label7.Size = new System.Drawing.Size(149, 12);
           this.label7.TabIndex = 6;
           this.label7.Text = "任务列表刷新频率(毫秒)：";
           // 
           // chkHideWhenClose
           // 
           this.chkHideWhenClose.AutoSize = true;
           this.chkHideWhenClose.Location = new System.Drawing.Point(29, 139);
           this.chkHideWhenClose.Name = "chkHideWhenClose";
           this.chkHideWhenClose.Size = new System.Drawing.Size(204, 16);
           this.chkHideWhenClose.TabIndex = 4;
           this.chkHideWhenClose.Text = "点击\"关闭\"时隐藏程序到系统托盘";
           this.tip.SetToolTip(this.chkHideWhenClose, "点击“关闭”按钮时，隐藏程序到系统托盘而不是退出程序");
           this.chkHideWhenClose.UseVisualStyleBackColor = true;
           // 
           // lnkCustomSearchExample
           // 
           this.lnkCustomSearchExample.AutoSize = true;
           this.lnkCustomSearchExample.Location = new System.Drawing.Point(252, 29);
           this.lnkCustomSearchExample.Name = "lnkCustomSearchExample";
           this.lnkCustomSearchExample.Size = new System.Drawing.Size(77, 12);
           this.lnkCustomSearchExample.TabIndex = 10;
           this.lnkCustomSearchExample.TabStop = true;
           this.lnkCustomSearchExample.Text = "查看更多示例";
           this.lnkCustomSearchExample.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCustomSearchExample_LinkClicked);
           // 
           // txtSearchText
           // 
           this.txtSearchText.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.txtSearchText.FormattingEnabled = true;
           this.txtSearchText.Items.AddRange(new object[] {
            "Acfun站内搜索",
            "Bilibili站内搜索",
            "土豆网",
            "优酷搜索(搜酷)",
            "漫画搜索(爱漫画)"});
           this.txtSearchText.Location = new System.Drawing.Point(22, 57);
           this.txtSearchText.Name = "txtSearchText";
           this.txtSearchText.Size = new System.Drawing.Size(290, 20);
           this.txtSearchText.TabIndex = 9;
           this.tip.SetToolTip(this.txtSearchText, "自定义搜索引擎所使用的搜索字符串");
           // 
           // label4
           // 
           this.label4.AutoSize = true;
           this.label4.Location = new System.Drawing.Point(20, 17);
           this.label4.Name = "label4";
           this.label4.Size = new System.Drawing.Size(221, 24);
           this.label4.TabIndex = 8;
           this.label4.Text = "请在下方文本框中填写自定义搜索网址\r\n(网址中的%TEST%用于替换用户的字符串)";
           // 
           // chkShowBigButton
           // 
           this.chkShowBigButton.AutoSize = true;
           this.chkShowBigButton.Location = new System.Drawing.Point(29, 51);
           this.chkShowBigButton.Name = "chkShowBigButton";
           this.chkShowBigButton.Size = new System.Drawing.Size(132, 16);
           this.chkShowBigButton.TabIndex = 1;
           this.chkShowBigButton.Text = "显示\"新建任务\"按钮";
           this.tip.SetToolTip(this.chkShowBigButton, "显示程序启动时界面中央的“新建任务”按钮");
           this.chkShowBigButton.UseVisualStyleBackColor = true;
           // 
           // chkEnableWin7
           // 
           this.chkEnableWin7.AutoSize = true;
           this.chkEnableWin7.Location = new System.Drawing.Point(29, 28);
           this.chkEnableWin7.Name = "chkEnableWin7";
           this.chkEnableWin7.Size = new System.Drawing.Size(126, 16);
           this.chkEnableWin7.TabIndex = 0;
           this.chkEnableWin7.Text = "启用Windows 7特性";
           this.tip.SetToolTip(this.chkEnableWin7, "在Windows 7(及更高版本的)操作系统下，启用超级任务栏特性");
           this.chkEnableWin7.UseVisualStyleBackColor = true;
           // 
           // chkCheckUrl
           // 
           this.chkCheckUrl.AutoSize = true;
           this.chkCheckUrl.Location = new System.Drawing.Point(29, 95);
           this.chkCheckUrl.Name = "chkCheckUrl";
           this.chkCheckUrl.Size = new System.Drawing.Size(102, 16);
           this.chkCheckUrl.TabIndex = 2;
           this.chkCheckUrl.Text = "检查输入的Url";
           this.tip.SetToolTip(this.chkCheckUrl, "在新建任务输入Url时，即时检查输入的Url是否合法");
           this.chkCheckUrl.UseVisualStyleBackColor = true;
           // 
           // chkWatch
           // 
           this.chkWatch.AutoSize = true;
           this.chkWatch.Location = new System.Drawing.Point(29, 117);
           this.chkWatch.Name = "chkWatch";
           this.chkWatch.Size = new System.Drawing.Size(84, 16);
           this.chkWatch.TabIndex = 3;
           this.chkWatch.Text = "监视剪贴板";
           this.tip.SetToolTip(this.chkWatch, "监视剪贴板并自动显示“新建任务”窗口");
           this.chkWatch.UseVisualStyleBackColor = true;
           // 
           // pagePlugin
           // 
           this.pagePlugin.Controls.Add(this.chkPluginSfacg);
           this.pagePlugin.Controls.Add(this.chkPluginTucao);
           this.pagePlugin.Controls.Add(this.lnkFlvcdIntro);
           this.pagePlugin.Controls.Add(this.chkPluginFlvcd);
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
           // chkPluginSfacg
           // 
           this.chkPluginSfacg.AutoSize = true;
           this.chkPluginSfacg.Location = new System.Drawing.Point(41, 245);
           this.chkPluginSfacg.Name = "chkPluginSfacg";
           this.chkPluginSfacg.Size = new System.Drawing.Size(222, 16);
           this.chkPluginSfacg.TabIndex = 10;
           this.chkPluginSfacg.Text = "启用SF互动传媒网（SFAcg）下载插件";
           this.chkPluginSfacg.UseVisualStyleBackColor = true;
           // 
           // chkPluginTucao
           // 
           this.chkPluginTucao.AutoSize = true;
           this.chkPluginTucao.Location = new System.Drawing.Point(41, 188);
           this.chkPluginTucao.Name = "chkPluginTucao";
           this.chkPluginTucao.Size = new System.Drawing.Size(174, 16);
           this.chkPluginTucao.TabIndex = 9;
           this.chkPluginTucao.Text = "启用Tucao(吐槽网)下载插件";
           this.chkPluginTucao.UseVisualStyleBackColor = true;
           // 
           // lnkFlvcdIntro
           // 
           this.lnkFlvcdIntro.AutoSize = true;
           this.lnkFlvcdIntro.Location = new System.Drawing.Point(60, 230);
           this.lnkFlvcdIntro.Name = "lnkFlvcdIntro";
           this.lnkFlvcdIntro.Size = new System.Drawing.Size(83, 12);
           this.lnkFlvcdIntro.TabIndex = 8;
           this.lnkFlvcdIntro.TabStop = true;
           this.lnkFlvcdIntro.Text = "FLVCD插件介绍";
           this.lnkFlvcdIntro.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkFlvcdIntro_LinkClicked);
           // 
           // chkPluginFlvcd
           // 
           this.chkPluginFlvcd.AutoSize = true;
           this.chkPluginFlvcd.Location = new System.Drawing.Point(41, 210);
           this.chkPluginFlvcd.Name = "chkPluginFlvcd";
           this.chkPluginFlvcd.Size = new System.Drawing.Size(132, 16);
           this.chkPluginFlvcd.TabIndex = 7;
           this.chkPluginFlvcd.Text = "启用FLVCD解析插件*";
           this.chkPluginFlvcd.UseVisualStyleBackColor = true;
           // 
           // chkPluginTiebaAlbum
           // 
           this.chkPluginTiebaAlbum.AutoSize = true;
           this.chkPluginTiebaAlbum.Location = new System.Drawing.Point(41, 166);
           this.chkPluginTiebaAlbum.Name = "chkPluginTiebaAlbum";
           this.chkPluginTiebaAlbum.Size = new System.Drawing.Size(168, 16);
           this.chkPluginTiebaAlbum.TabIndex = 6;
           this.chkPluginTiebaAlbum.Text = "启用百度贴吧相册下载插件";
           this.chkPluginTiebaAlbum.UseVisualStyleBackColor = true;
           // 
           // chkPluginImanhua
           // 
           this.chkPluginImanhua.AutoSize = true;
           this.chkPluginImanhua.Location = new System.Drawing.Point(41, 143);
           this.chkPluginImanhua.Name = "chkPluginImanhua";
           this.chkPluginImanhua.Size = new System.Drawing.Size(144, 16);
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
           this.chkPluginYouku.Location = new System.Drawing.Point(41, 121);
           this.chkPluginYouku.Name = "chkPluginYouku";
           this.chkPluginYouku.Size = new System.Drawing.Size(132, 16);
           this.chkPluginYouku.TabIndex = 3;
           this.chkPluginYouku.Text = "启用优酷网下载插件";
           this.chkPluginYouku.UseVisualStyleBackColor = true;
           // 
           // chkPluginBilibili
           // 
           this.chkPluginBilibili.AutoSize = true;
           this.chkPluginBilibili.Location = new System.Drawing.Point(41, 99);
           this.chkPluginBilibili.Name = "chkPluginBilibili";
           this.chkPluginBilibili.Size = new System.Drawing.Size(144, 16);
           this.chkPluginBilibili.TabIndex = 2;
           this.chkPluginBilibili.Text = "启用Bilibili下载插件";
           this.chkPluginBilibili.UseVisualStyleBackColor = true;
           // 
           // chkPluginTudou
           // 
           this.chkPluginTudou.AutoSize = true;
           this.chkPluginTudou.Location = new System.Drawing.Point(41, 77);
           this.chkPluginTudou.Name = "chkPluginTudou";
           this.chkPluginTudou.Size = new System.Drawing.Size(132, 16);
           this.chkPluginTudou.TabIndex = 1;
           this.chkPluginTudou.Text = "启用土豆网下载插件";
           this.chkPluginTudou.UseVisualStyleBackColor = true;
           // 
           // chkPluginAcfun
           // 
           this.chkPluginAcfun.AutoSize = true;
           this.chkPluginAcfun.Location = new System.Drawing.Point(41, 55);
           this.chkPluginAcfun.Name = "chkPluginAcfun";
           this.chkPluginAcfun.Size = new System.Drawing.Size(126, 16);
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
           this.tip.SetToolTip(this.btnProxyDelete, "删除已选中的代理服务器设置");
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
           this.tip.SetToolTip(this.btnProxyModify, "修改代理服务器设置");
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
           this.tip.SetToolTip(this.btnProxyAdd, "添加代理服务器设置");
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
           // tabUpdate
           // 
           this.tabUpdate.Controls.Add(this.chkEnableCheckUpdate);
           this.tabUpdate.Location = new System.Drawing.Point(4, 22);
           this.tabUpdate.Name = "tabUpdate";
           this.tabUpdate.Padding = new System.Windows.Forms.Padding(3);
           this.tabUpdate.Size = new System.Drawing.Size(386, 312);
           this.tabUpdate.TabIndex = 8;
           this.tabUpdate.Text = "更新";
           this.tabUpdate.UseVisualStyleBackColor = true;
           // 
           // chkEnableCheckUpdate
           // 
           this.chkEnableCheckUpdate.AutoSize = true;
           this.chkEnableCheckUpdate.Location = new System.Drawing.Point(32, 32);
           this.chkEnableCheckUpdate.Name = "chkEnableCheckUpdate";
           this.chkEnableCheckUpdate.Size = new System.Drawing.Size(96, 16);
           this.chkEnableCheckUpdate.TabIndex = 0;
           this.chkEnableCheckUpdate.Text = "自动检查更新";
           this.tip.SetToolTip(this.chkEnableCheckUpdate, "启动程序时自动检查是否有更新版本的AcDown");
           this.chkEnableCheckUpdate.UseVisualStyleBackColor = true;
           // 
           // pageDebug
           // 
           this.pageDebug.Controls.Add(this.label8);
           this.pageDebug.Controls.Add(this.udNetworkTimeout);
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
           // label8
           // 
           this.label8.AutoSize = true;
           this.label8.Location = new System.Drawing.Point(35, 95);
           this.label8.Name = "label8";
           this.label8.Size = new System.Drawing.Size(137, 12);
           this.label8.TabIndex = 10;
           this.label8.Text = "网络超时时间（毫秒）：";
           // 
           // udNetworkTimeout
           // 
           this.udNetworkTimeout.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
           this.udNetworkTimeout.Location = new System.Drawing.Point(178, 93);
           this.udNetworkTimeout.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
           this.udNetworkTimeout.Minimum = new decimal(new int[] {
            15000,
            0,
            0,
            0});
           this.udNetworkTimeout.Name = "udNetworkTimeout";
           this.udNetworkTimeout.Size = new System.Drawing.Size(75, 21);
           this.udNetworkTimeout.TabIndex = 9;
           this.udNetworkTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
           this.udNetworkTimeout.ThousandsSeparator = true;
           this.tip.SetToolTip(this.udNetworkTimeout, "设置网络超时时间。\r\n减少数值可以降低网络连接失败时等待的时间\r\n增加数值以适用于网络环境不佳的情况");
           this.udNetworkTimeout.Value = new decimal(new int[] {
            25000,
            0,
            0,
            0});
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
           this.tip.SetToolTip(this.lnkLog, "查看当前已保存的程序日志");
           this.lnkLog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLog_LinkClicked);
           // 
           // chkEnableLog
           // 
           this.chkEnableLog.AutoSize = true;
           this.chkEnableLog.Location = new System.Drawing.Point(37, 33);
           this.chkEnableLog.Name = "chkEnableLog";
           this.chkEnableLog.Size = new System.Drawing.Size(216, 16);
           this.chkEnableLog.TabIndex = 8;
           this.chkEnableLog.Text = "启用错误日志（重启下载器后生效）";
           this.tip.SetToolTip(this.chkEnableLog, "启用下载日志，下载日志可以帮助确认下载时出现的问题");
           this.chkEnableLog.UseVisualStyleBackColor = true;
           // 
           // btnDefault
           // 
           this.btnDefault.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.btnDefault.Location = new System.Drawing.Point(12, 352);
           this.btnDefault.Name = "btnDefault";
           this.btnDefault.Size = new System.Drawing.Size(110, 23);
           this.btnDefault.TabIndex = 2;
           this.btnDefault.Text = "恢复默认设置";
           this.tip.SetToolTip(this.btnDefault, "恢复所有选项至默认值");
           this.btnDefault.UseVisualStyleBackColor = true;
           this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
           // 
           // tip
           // 
           this.tip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
           this.tip.ToolTipTitle = "设置";
           // 
           // groupBox1
           // 
           this.groupBox1.Controls.Add(this.label4);
           this.groupBox1.Controls.Add(this.txtSearchText);
           this.groupBox1.Controls.Add(this.lnkCustomSearchExample);
           this.groupBox1.Location = new System.Drawing.Point(28, 197);
           this.groupBox1.Name = "groupBox1";
           this.groupBox1.Size = new System.Drawing.Size(335, 100);
           this.groupBox1.TabIndex = 12;
           this.groupBox1.TabStop = false;
           this.groupBox1.Text = "自定义搜索引擎";
           // 
           // FormConfig
           // 
           this.AcceptButton = this.btnOK;
           this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
           this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           this.CancelButton = this.btnCancel;
           this.ClientSize = new System.Drawing.Size(406, 387);
           this.Controls.Add(this.btnDefault);
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
           this.tabUpdate.ResumeLayout(false);
           this.tabUpdate.PerformLayout();
           this.pageDebug.ResumeLayout(false);
           this.pageDebug.PerformLayout();
           ((System.ComponentModel.ISupportInitialize)(this.udNetworkTimeout)).EndInit();
           this.groupBox1.ResumeLayout(false);
           this.groupBox1.PerformLayout();
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
		  private System.Windows.Forms.CheckBox chkPluginFlvcd;
		  private System.Windows.Forms.LinkLabel lnkFlvcdIntro;
		  private System.Windows.Forms.Button btnDefault;
		  private System.Windows.Forms.CheckBox chkPluginTucao;
		  private System.Windows.Forms.Label label8;
		  private System.Windows.Forms.NumericUpDown udNetworkTimeout;
		  private System.Windows.Forms.TextBox txtCustomSound;
		  private System.Windows.Forms.CheckBox chkCustomSound;
        private System.Windows.Forms.ToolTip tip;
        private System.Windows.Forms.CheckBox chkPluginSfacg;
        private System.Windows.Forms.CheckBox chkShowLogo;
        private System.Windows.Forms.TabPage tabUpdate;
        private System.Windows.Forms.CheckBox chkEnableCheckUpdate;
        private System.Windows.Forms.GroupBox groupBox1;
	 }
}