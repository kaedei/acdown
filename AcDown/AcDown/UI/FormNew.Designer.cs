namespace Kaedei.AcDown.UI
{
	 partial class FormNew
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
			this.txtInput = new System.Windows.Forms.TextBox();
			this.btnAdd = new System.Windows.Forms.Button();
			this.picCheck = new System.Windows.Forms.PictureBox();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.btnSetProxy = new System.Windows.Forms.Button();
			this.chkParseRelated = new System.Windows.Forms.CheckBox();
			this.chkAutoAnswer = new System.Windows.Forms.CheckBox();
			this.chkExtractCache = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cboProxy = new System.Windows.Forms.ComboBox();
			this.cboPlugins = new System.Windows.Forms.ComboBox();
			this.txtComment = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.lnkPaste = new System.Windows.Forms.LinkLabel();
			this.lnkPluginConfig = new System.Windows.Forms.LinkLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.lnkBrowseDir = new System.Windows.Forms.LinkLabel();
			this.chkAdvance = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.lstDownloadType = new System.Windows.Forms.CheckedListBox();
			this.cboPath = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.picCheck)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtInput
			// 
			this.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtInput.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtInput.Location = new System.Drawing.Point(66, 27);
			this.txtInput.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txtInput.Name = "txtInput";
			this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtInput.Size = new System.Drawing.Size(287, 27);
			this.txtInput.TabIndex = 2;
			this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
			this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnAdd.Location = new System.Drawing.Point(326, 158);
			this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(96, 30);
			this.btnAdd.TabIndex = 0;
			this.btnAdd.Text = "下载";
			this.toolTip.SetToolTip(this.btnAdd, "单击确认新建任务");
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// picCheck
			// 
			this.picCheck.Image = global::Kaedei.AcDown.Properties.Resources._1;
			this.picCheck.Location = new System.Drawing.Point(398, 30);
			this.picCheck.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.picCheck.Name = "picCheck";
			this.picCheck.Size = new System.Drawing.Size(19, 19);
			this.picCheck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.picCheck.TabIndex = 6;
			this.picCheck.TabStop = false;
			this.picCheck.Visible = false;
			// 
			// toolTip
			// 
			this.toolTip.AutoPopDelay = 8000;
			this.toolTip.InitialDelay = 500;
			this.toolTip.ReshowDelay = 100;
			this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.toolTip.ToolTipTitle = "提示:";
			// 
			// btnSetProxy
			// 
			this.btnSetProxy.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSetProxy.Location = new System.Drawing.Point(127, 76);
			this.btnSetProxy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnSetProxy.Name = "btnSetProxy";
			this.btnSetProxy.Size = new System.Drawing.Size(67, 24);
			this.btnSetProxy.TabIndex = 1;
			this.btnSetProxy.Text = "编辑...";
			this.toolTip.SetToolTip(this.btnSetProxy, "点击编辑代理服务器设置");
			this.btnSetProxy.UseVisualStyleBackColor = true;
			this.btnSetProxy.Click += new System.EventHandler(this.btnSetProxy_Click);
			// 
			// chkParseRelated
			// 
			this.chkParseRelated.Location = new System.Drawing.Point(12, 23);
			this.chkParseRelated.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.chkParseRelated.Name = "chkParseRelated";
			this.chkParseRelated.Size = new System.Drawing.Size(147, 21);
			this.chkParseRelated.TabIndex = 0;
			this.chkParseRelated.Text = "解析所有关联的下载项";
			this.toolTip.SetToolTip(this.chkParseRelated, "添加任务时，解析与此任务相关联的其他任务");
			this.chkParseRelated.UseVisualStyleBackColor = true;
			// 
			// chkAutoAnswer
			// 
			this.chkAutoAnswer.Location = new System.Drawing.Point(11, 81);
			this.chkAutoAnswer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.chkAutoAnswer.Name = "chkAutoAnswer";
			this.chkAutoAnswer.Size = new System.Drawing.Size(134, 21);
			this.chkAutoAnswer.TabIndex = 2;
			this.chkAutoAnswer.Text = "挂机下载模式";
			this.toolTip.SetToolTip(this.chkAutoAnswer, "启用自动应答后，下载时不再出现询问对话框。\r\n由此任务添加的其他任务也会继承此设置。");
			this.chkAutoAnswer.UseVisualStyleBackColor = true;
			// 
			// chkExtractCache
			// 
			this.chkExtractCache.Location = new System.Drawing.Point(11, 52);
			this.chkExtractCache.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.chkExtractCache.Name = "chkExtractCache";
			this.chkExtractCache.Size = new System.Drawing.Size(147, 21);
			this.chkExtractCache.TabIndex = 1;
			this.chkExtractCache.Text = "提取浏览器缓存";
			this.toolTip.SetToolTip(this.chkExtractCache, "允许自动提取已下载的文件，省却重新下载的过程");
			this.chkExtractCache.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 120);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 17);
			this.label2.TabIndex = 7;
			this.label2.Text = "保存到:";
			// 
			// cboProxy
			// 
			this.cboProxy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboProxy.FormattingEnabled = true;
			this.cboProxy.Location = new System.Drawing.Point(12, 43);
			this.cboProxy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.cboProxy.Name = "cboProxy";
			this.cboProxy.Size = new System.Drawing.Size(182, 25);
			this.cboProxy.TabIndex = 0;
			// 
			// cboPlugins
			// 
			this.cboPlugins.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPlugins.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cboPlugins.FormattingEnabled = true;
			this.cboPlugins.Location = new System.Drawing.Point(66, 73);
			this.cboPlugins.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.cboPlugins.Name = "cboPlugins";
			this.cboPlugins.Size = new System.Drawing.Size(245, 25);
			this.cboPlugins.TabIndex = 5;
			this.cboPlugins.SelectedIndexChanged += new System.EventHandler(this.cboPlugins_SelectedIndexChanged);
			// 
			// txtComment
			// 
			this.txtComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtComment.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtComment.Location = new System.Drawing.Point(3, 19);
			this.txtComment.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txtComment.Multiline = true;
			this.txtComment.Name = "txtComment";
			this.txtComment.Size = new System.Drawing.Size(194, 88);
			this.txtComment.TabIndex = 0;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(25, 32);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(35, 17);
			this.label5.TabIndex = 1;
			this.label5.Text = "网址:";
			// 
			// lnkPaste
			// 
			this.lnkPaste.AutoSize = true;
			this.lnkPaste.Location = new System.Drawing.Point(359, 32);
			this.lnkPaste.Name = "lnkPaste";
			this.lnkPaste.Size = new System.Drawing.Size(32, 17);
			this.lnkPaste.TabIndex = 3;
			this.lnkPaste.TabStop = true;
			this.lnkPaste.Text = "粘贴";
			this.lnkPaste.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkPaste_LinkClicked);
			// 
			// lnkPluginConfig
			// 
			this.lnkPluginConfig.AutoSize = true;
			this.lnkPluginConfig.Location = new System.Drawing.Point(320, 76);
			this.lnkPluginConfig.Name = "lnkPluginConfig";
			this.lnkPluginConfig.Size = new System.Drawing.Size(80, 17);
			this.lnkPluginConfig.TabIndex = 6;
			this.lnkPluginConfig.TabStop = true;
			this.lnkPluginConfig.Text = "调整插件设置";
			this.lnkPluginConfig.Visible = false;
			this.lnkPluginConfig.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkPluginConfig_LinkClicked);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(25, 76);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 17);
			this.label1.TabIndex = 4;
			this.label1.Text = "插件:";
			// 
			// lnkBrowseDir
			// 
			this.lnkBrowseDir.AutoSize = true;
			this.lnkBrowseDir.Location = new System.Drawing.Point(359, 121);
			this.lnkBrowseDir.Name = "lnkBrowseDir";
			this.lnkBrowseDir.Size = new System.Drawing.Size(41, 17);
			this.lnkBrowseDir.TabIndex = 9;
			this.lnkBrowseDir.TabStop = true;
			this.lnkBrowseDir.Text = "选择...";
			this.lnkBrowseDir.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkBrowseDir_LinkClicked);
			// 
			// chkAdvance
			// 
			this.chkAdvance.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkAdvance.BackColor = System.Drawing.Color.Transparent;
			this.chkAdvance.FlatAppearance.BorderSize = 0;
			this.chkAdvance.Location = new System.Drawing.Point(19, 158);
			this.chkAdvance.Name = "chkAdvance";
			this.chkAdvance.Size = new System.Drawing.Size(106, 30);
			this.chkAdvance.TabIndex = 10;
			this.chkAdvance.Text = "更多选项 >>";
			this.chkAdvance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkAdvance.UseVisualStyleBackColor = false;
			this.chkAdvance.CheckedChanged += new System.EventHandler(this.chkAdvance_CheckedChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtComment);
			this.groupBox1.Location = new System.Drawing.Point(222, 329);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 110);
			this.groupBox1.TabIndex = 14;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "注释";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.chkParseRelated);
			this.groupBox2.Controls.Add(this.chkAutoAnswer);
			this.groupBox2.Controls.Add(this.chkExtractCache);
			this.groupBox2.Location = new System.Drawing.Point(222, 211);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(200, 112);
			this.groupBox2.TabIndex = 12;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "高级";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.cboProxy);
			this.groupBox3.Controls.Add(this.btnSetProxy);
			this.groupBox3.Location = new System.Drawing.Point(16, 213);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(200, 110);
			this.groupBox3.TabIndex = 11;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "代理服务器";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.lstDownloadType);
			this.groupBox4.Location = new System.Drawing.Point(16, 329);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(200, 110);
			this.groupBox4.TabIndex = 13;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "允许下载以下类型的文件";
			// 
			// lstDownloadType
			// 
			this.lstDownloadType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstDownloadType.CheckOnClick = true;
			this.lstDownloadType.ColumnWidth = 80;
			this.lstDownloadType.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lstDownloadType.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstDownloadType.FormattingEnabled = true;
			this.lstDownloadType.Items.AddRange(new object[] {
            "视频",
            "音频",
            "图片",
            "文字",
            "字幕/弹幕",
            "评论"});
			this.lstDownloadType.Location = new System.Drawing.Point(3, 19);
			this.lstDownloadType.MultiColumn = true;
			this.lstDownloadType.Name = "lstDownloadType";
			this.lstDownloadType.Size = new System.Drawing.Size(194, 88);
			this.lstDownloadType.TabIndex = 0;
			// 
			// cboPath
			// 
			this.cboPath.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPath.FormattingEnabled = true;
			this.cboPath.Location = new System.Drawing.Point(66, 117);
			this.cboPath.Name = "cboPath";
			this.cboPath.Size = new System.Drawing.Size(287, 25);
			this.cboPath.TabIndex = 15;
			this.cboPath.SelectedIndexChanged += new System.EventHandler(this.cboPath_SelectedIndexChanged);
			// 
			// FormNew
			// 
			this.AcceptButton = this.btnAdd;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(434, 458);
			this.Controls.Add(this.cboPath);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.chkAdvance);
			this.Controls.Add(this.lnkBrowseDir);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lnkPluginConfig);
			this.Controls.Add(this.cboPlugins);
			this.Controls.Add(this.lnkPaste);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtInput);
			this.Controls.Add(this.picCheck);
			this.Controls.Add(this.btnAdd);
			this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormNew";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "新建下载任务";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormNew_FormClosing);
			this.Load += new System.EventHandler(this.FormNew_Load);
			((System.ComponentModel.ISupportInitialize)(this.picCheck)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		  }

		  #endregion

		  private System.Windows.Forms.TextBox txtInput;
		  private System.Windows.Forms.Button btnAdd;
		  private System.Windows.Forms.PictureBox picCheck;
        private System.Windows.Forms.ToolTip toolTip;
		  private System.Windows.Forms.Label label2;
		  private System.Windows.Forms.ComboBox cboProxy;
		  private System.Windows.Forms.TextBox txtComment;
		  private System.Windows.Forms.ComboBox cboPlugins;
		  private System.Windows.Forms.Button btnSetProxy;
		  private System.Windows.Forms.CheckBox chkParseRelated;
		  private System.Windows.Forms.CheckBox chkAutoAnswer;
		  private System.Windows.Forms.CheckBox chkExtractCache;
		  private System.Windows.Forms.Label label5;
		  private System.Windows.Forms.LinkLabel lnkPaste;
		  private System.Windows.Forms.LinkLabel lnkPluginConfig;
		  private System.Windows.Forms.Label label1;
		  private System.Windows.Forms.LinkLabel lnkBrowseDir;
		  private System.Windows.Forms.CheckBox chkAdvance;
		  private System.Windows.Forms.GroupBox groupBox1;
		  private System.Windows.Forms.GroupBox groupBox2;
		  private System.Windows.Forms.GroupBox groupBox3;
		  private System.Windows.Forms.GroupBox groupBox4;
		  private System.Windows.Forms.CheckedListBox lstDownloadType;
		  private System.Windows.Forms.ComboBox cboPath;
	 }
}