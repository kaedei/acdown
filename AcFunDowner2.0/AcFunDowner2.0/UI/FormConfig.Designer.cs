namespace Kaedei.AcDown
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
			  this.groupBox1 = new System.Windows.Forms.GroupBox();
			  this.chkEnableLog = new System.Windows.Forms.CheckBox();
			  this.chkDeleteFile = new System.Windows.Forms.CheckBox();
			  this.chkCheckUrl = new System.Windows.Forms.CheckBox();
			  this.chkWatch = new System.Windows.Forms.CheckBox();
			  this.label2 = new System.Windows.Forms.Label();
			  this.lnkSavePath = new System.Windows.Forms.LinkLabel();
			  this.label1 = new System.Windows.Forms.Label();
			  this.numCacheSize = new System.Windows.Forms.NumericUpDown();
			  this.chkPlaySound = new System.Windows.Forms.CheckBox();
			  this.chkOpenFolder = new System.Windows.Forms.CheckBox();
			  this.chkDownAllParts = new System.Windows.Forms.CheckBox();
			  this.chkDownSub = new System.Windows.Forms.CheckBox();
			  this.groupBox2 = new System.Windows.Forms.GroupBox();
			  this.lnkLog = new System.Windows.Forms.LinkLabel();
			  this.txtServerIP = new System.Windows.Forms.TextBox();
			  this.label3 = new System.Windows.Forms.Label();
			  this.btnCancel = new System.Windows.Forms.Button();
			  this.btnOK = new System.Windows.Forms.Button();
			  this.chkShowTrayIcon = new System.Windows.Forms.CheckBox();
			  this.chkEnableWin7 = new System.Windows.Forms.CheckBox();
			  this.chkShowBigButton = new System.Windows.Forms.CheckBox();
			  this.label4 = new System.Windows.Forms.Label();
			  this.txtSearchText = new System.Windows.Forms.TextBox();
			  this.groupBox1.SuspendLayout();
			  ((System.ComponentModel.ISupportInitialize)(this.numCacheSize)).BeginInit();
			  this.groupBox2.SuspendLayout();
			  this.SuspendLayout();
			  // 
			  // groupBox1
			  // 
			  this.groupBox1.Controls.Add(this.txtSearchText);
			  this.groupBox1.Controls.Add(this.label4);
			  this.groupBox1.Controls.Add(this.chkShowBigButton);
			  this.groupBox1.Controls.Add(this.chkEnableWin7);
			  this.groupBox1.Controls.Add(this.chkShowTrayIcon);
			  this.groupBox1.Controls.Add(this.lnkLog);
			  this.groupBox1.Controls.Add(this.chkEnableLog);
			  this.groupBox1.Controls.Add(this.chkDeleteFile);
			  this.groupBox1.Controls.Add(this.chkCheckUrl);
			  this.groupBox1.Controls.Add(this.chkWatch);
			  this.groupBox1.Controls.Add(this.label1);
			  this.groupBox1.Controls.Add(this.numCacheSize);
			  this.groupBox1.Controls.Add(this.chkPlaySound);
			  this.groupBox1.Controls.Add(this.chkOpenFolder);
			  this.groupBox1.Controls.Add(this.chkDownAllParts);
			  this.groupBox1.Controls.Add(this.chkDownSub);
			  this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			  this.groupBox1.Location = new System.Drawing.Point(12, 12);
			  this.groupBox1.Name = "groupBox1";
			  this.groupBox1.Size = new System.Drawing.Size(396, 251);
			  this.groupBox1.TabIndex = 0;
			  this.groupBox1.TabStop = false;
			  this.groupBox1.Text = "常规";
			  // 
			  // chkEnableLog
			  // 
			  this.chkEnableLog.AutoSize = true;
			  this.chkEnableLog.FlatStyle = System.Windows.Forms.FlatStyle.System;
			  this.chkEnableLog.Location = new System.Drawing.Point(220, 20);
			  this.chkEnableLog.Name = "chkEnableLog";
			  this.chkEnableLog.Size = new System.Drawing.Size(102, 17);
			  this.chkEnableLog.TabIndex = 4;
			  this.chkEnableLog.Text = "启用错误日志";
			  this.chkEnableLog.UseVisualStyleBackColor = true;
			  // 
			  // chkDeleteFile
			  // 
			  this.chkDeleteFile.AutoSize = true;
			  this.chkDeleteFile.FlatStyle = System.Windows.Forms.FlatStyle.System;
			  this.chkDeleteFile.Location = new System.Drawing.Point(5, 156);
			  this.chkDeleteFile.Name = "chkDeleteFile";
			  this.chkDeleteFile.Size = new System.Drawing.Size(186, 17);
			  this.chkDeleteFile.TabIndex = 9;
			  this.chkDeleteFile.Text = "删除任务的同时删除相应文件";
			  this.chkDeleteFile.UseVisualStyleBackColor = true;
			  // 
			  // chkCheckUrl
			  // 
			  this.chkCheckUrl.AutoSize = true;
			  this.chkCheckUrl.FlatStyle = System.Windows.Forms.FlatStyle.System;
			  this.chkCheckUrl.Location = new System.Drawing.Point(6, 110);
			  this.chkCheckUrl.Name = "chkCheckUrl";
			  this.chkCheckUrl.Size = new System.Drawing.Size(108, 17);
			  this.chkCheckUrl.TabIndex = 8;
			  this.chkCheckUrl.Text = "检查输入的Url";
			  this.chkCheckUrl.UseVisualStyleBackColor = true;
			  // 
			  // chkWatch
			  // 
			  this.chkWatch.AutoSize = true;
			  this.chkWatch.FlatStyle = System.Windows.Forms.FlatStyle.System;
			  this.chkWatch.Location = new System.Drawing.Point(6, 133);
			  this.chkWatch.Name = "chkWatch";
			  this.chkWatch.Size = new System.Drawing.Size(90, 17);
			  this.chkWatch.TabIndex = 7;
			  this.chkWatch.Text = "监视剪贴板";
			  this.chkWatch.UseVisualStyleBackColor = true;
			  // 
			  // label2
			  // 
			  this.label2.AutoSize = true;
			  this.label2.Location = new System.Drawing.Point(7, 26);
			  this.label2.Name = "label2";
			  this.label2.Size = new System.Drawing.Size(101, 12);
			  this.label2.TabIndex = 6;
			  this.label2.Text = "默认保存文件夹：";
			  // 
			  // lnkSavePath
			  // 
			  this.lnkSavePath.AutoSize = true;
			  this.lnkSavePath.Location = new System.Drawing.Point(111, 26);
			  this.lnkSavePath.Name = "lnkSavePath";
			  this.lnkSavePath.Size = new System.Drawing.Size(95, 12);
			  this.lnkSavePath.TabIndex = 5;
			  this.lnkSavePath.TabStop = true;
			  this.lnkSavePath.Text = "D:\\My Documents";
			  this.lnkSavePath.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSavePath_LinkClicked);
			  // 
			  // label1
			  // 
			  this.label1.AutoSize = true;
			  this.label1.Location = new System.Drawing.Point(209, 157);
			  this.label1.Name = "label1";
			  this.label1.Size = new System.Drawing.Size(113, 12);
			  this.label1.TabIndex = 4;
			  this.label1.Text = "缓存大小：(1-16MB)";
			  // 
			  // numCacheSize
			  // 
			  this.numCacheSize.Location = new System.Drawing.Point(328, 155);
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
			  this.numCacheSize.TabIndex = 2;
			  this.numCacheSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			  // 
			  // chkPlaySound
			  // 
			  this.chkPlaySound.AutoSize = true;
			  this.chkPlaySound.FlatStyle = System.Windows.Forms.FlatStyle.System;
			  this.chkPlaySound.Location = new System.Drawing.Point(6, 87);
			  this.chkPlaySound.Name = "chkPlaySound";
			  this.chkPlaySound.Size = new System.Drawing.Size(150, 17);
			  this.chkPlaySound.TabIndex = 3;
			  this.chkPlaySound.Text = "下载完成后播放提示音";
			  this.chkPlaySound.UseVisualStyleBackColor = true;
			  // 
			  // chkOpenFolder
			  // 
			  this.chkOpenFolder.AutoSize = true;
			  this.chkOpenFolder.FlatStyle = System.Windows.Forms.FlatStyle.System;
			  this.chkOpenFolder.Location = new System.Drawing.Point(6, 65);
			  this.chkOpenFolder.Name = "chkOpenFolder";
			  this.chkOpenFolder.Size = new System.Drawing.Size(150, 17);
			  this.chkOpenFolder.TabIndex = 2;
			  this.chkOpenFolder.Text = "下载完成后打开文件夹";
			  this.chkOpenFolder.UseVisualStyleBackColor = true;
			  // 
			  // chkDownAllParts
			  // 
			  this.chkDownAllParts.AutoSize = true;
			  this.chkDownAllParts.FlatStyle = System.Windows.Forms.FlatStyle.System;
			  this.chkDownAllParts.Location = new System.Drawing.Point(6, 43);
			  this.chkDownAllParts.Name = "chkDownAllParts";
			  this.chkDownAllParts.Size = new System.Drawing.Size(174, 17);
			  this.chkDownAllParts.TabIndex = 1;
			  this.chkDownAllParts.Text = "自动下载所有相关联的章节";
			  this.chkDownAllParts.UseVisualStyleBackColor = true;
			  // 
			  // chkDownSub
			  // 
			  this.chkDownSub.AutoSize = true;
			  this.chkDownSub.FlatStyle = System.Windows.Forms.FlatStyle.System;
			  this.chkDownSub.Location = new System.Drawing.Point(6, 20);
			  this.chkDownSub.Name = "chkDownSub";
			  this.chkDownSub.Size = new System.Drawing.Size(102, 17);
			  this.chkDownSub.TabIndex = 0;
			  this.chkDownSub.Text = "下载字幕文件";
			  this.chkDownSub.UseVisualStyleBackColor = true;
			  // 
			  // groupBox2
			  // 
			  this.groupBox2.Controls.Add(this.txtServerIP);
			  this.groupBox2.Controls.Add(this.label3);
			  this.groupBox2.Controls.Add(this.label2);
			  this.groupBox2.Controls.Add(this.lnkSavePath);
			  this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			  this.groupBox2.Location = new System.Drawing.Point(12, 269);
			  this.groupBox2.Name = "groupBox2";
			  this.groupBox2.Size = new System.Drawing.Size(396, 100);
			  this.groupBox2.TabIndex = 1;
			  this.groupBox2.TabStop = false;
			  this.groupBox2.Text = "全局设置";
			  // 
			  // lnkLog
			  // 
			  this.lnkLog.AutoSize = true;
			  this.lnkLog.Location = new System.Drawing.Point(231, 44);
			  this.lnkLog.Name = "lnkLog";
			  this.lnkLog.Size = new System.Drawing.Size(77, 12);
			  this.lnkLog.TabIndex = 3;
			  this.lnkLog.TabStop = true;
			  this.lnkLog.Text = "查看错误日志";
			  this.lnkLog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLog_LinkClicked);
			  // 
			  // txtServerIP
			  // 
			  this.txtServerIP.Location = new System.Drawing.Point(6, 67);
			  this.txtServerIP.Name = "txtServerIP";
			  this.txtServerIP.Size = new System.Drawing.Size(384, 21);
			  this.txtServerIP.TabIndex = 2;
			  this.txtServerIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtServerIP_KeyPress);
			  // 
			  // label3
			  // 
			  this.label3.AutoSize = true;
			  this.label3.Location = new System.Drawing.Point(6, 52);
			  this.label3.Name = "label3";
			  this.label3.Size = new System.Drawing.Size(131, 12);
			  this.label3.TabIndex = 1;
			  this.label3.Text = "AcFun.cn服务器IP地址:";
			  // 
			  // btnCancel
			  // 
			  this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			  this.btnCancel.Location = new System.Drawing.Point(326, 375);
			  this.btnCancel.Name = "btnCancel";
			  this.btnCancel.Size = new System.Drawing.Size(75, 23);
			  this.btnCancel.TabIndex = 2;
			  this.btnCancel.Text = "取消";
			  this.btnCancel.UseVisualStyleBackColor = true;
			  this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			  // 
			  // btnOK
			  // 
			  this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			  this.btnOK.Location = new System.Drawing.Point(245, 375);
			  this.btnOK.Name = "btnOK";
			  this.btnOK.Size = new System.Drawing.Size(75, 23);
			  this.btnOK.TabIndex = 3;
			  this.btnOK.Text = "确认";
			  this.btnOK.UseVisualStyleBackColor = true;
			  this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			  // 
			  // chkShowTrayIcon
			  // 
			  this.chkShowTrayIcon.AutoSize = true;
			  this.chkShowTrayIcon.FlatStyle = System.Windows.Forms.FlatStyle.System;
			  this.chkShowTrayIcon.Location = new System.Drawing.Point(220, 65);
			  this.chkShowTrayIcon.Name = "chkShowTrayIcon";
			  this.chkShowTrayIcon.Size = new System.Drawing.Size(102, 17);
			  this.chkShowTrayIcon.TabIndex = 10;
			  this.chkShowTrayIcon.Text = "显示托盘图标";
			  this.chkShowTrayIcon.UseVisualStyleBackColor = true;
			  // 
			  // chkEnableWin7
			  // 
			  this.chkEnableWin7.AutoSize = true;
			  this.chkEnableWin7.FlatStyle = System.Windows.Forms.FlatStyle.System;
			  this.chkEnableWin7.Location = new System.Drawing.Point(220, 87);
			  this.chkEnableWin7.Name = "chkEnableWin7";
			  this.chkEnableWin7.Size = new System.Drawing.Size(132, 17);
			  this.chkEnableWin7.TabIndex = 11;
			  this.chkEnableWin7.Text = "启用Windows 7特性";
			  this.chkEnableWin7.UseVisualStyleBackColor = true;
			  // 
			  // chkShowBigButton
			  // 
			  this.chkShowBigButton.AutoSize = true;
			  this.chkShowBigButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			  this.chkShowBigButton.Location = new System.Drawing.Point(220, 110);
			  this.chkShowBigButton.Name = "chkShowBigButton";
			  this.chkShowBigButton.Size = new System.Drawing.Size(138, 17);
			  this.chkShowBigButton.TabIndex = 12;
			  this.chkShowBigButton.Text = "显示\"新建任务\"按钮";
			  this.chkShowBigButton.UseVisualStyleBackColor = true;
			  // 
			  // label4
			  // 
			  this.label4.AutoSize = true;
			  this.label4.Location = new System.Drawing.Point(4, 187);
			  this.label4.Name = "label4";
			  this.label4.Size = new System.Drawing.Size(275, 12);
			  this.label4.TabIndex = 13;
			  this.label4.Text = "自定义搜索引擎:(请用%TEST%替换要搜索的字符串)";
			  // 
			  // txtSearchText
			  // 
			  this.txtSearchText.Location = new System.Drawing.Point(5, 211);
			  this.txtSearchText.Name = "txtSearchText";
			  this.txtSearchText.Size = new System.Drawing.Size(383, 21);
			  this.txtSearchText.TabIndex = 14;
			  // 
			  // FormConfig
			  // 
			  this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			  this.ClientSize = new System.Drawing.Size(420, 415);
			  this.Controls.Add(this.btnOK);
			  this.Controls.Add(this.btnCancel);
			  this.Controls.Add(this.groupBox2);
			  this.Controls.Add(this.groupBox1);
			  this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			  this.MaximizeBox = false;
			  this.Name = "FormConfig";
			  this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			  this.Text = "AcDown动漫下载器 设置";
			  this.Load += new System.EventHandler(this.FormConfig_Load);
			  this.groupBox1.ResumeLayout(false);
			  this.groupBox1.PerformLayout();
			  ((System.ComponentModel.ISupportInitialize)(this.numCacheSize)).EndInit();
			  this.groupBox2.ResumeLayout(false);
			  this.groupBox2.PerformLayout();
			  this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkDownAllParts;
        private System.Windows.Forms.CheckBox chkDownSub;
        private System.Windows.Forms.CheckBox chkOpenFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel lnkSavePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numCacheSize;
        private System.Windows.Forms.CheckBox chkPlaySound;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtServerIP;
		  private System.Windows.Forms.LinkLabel lnkLog;
		  private System.Windows.Forms.CheckBox chkWatch;
		  private System.Windows.Forms.CheckBox chkCheckUrl;
		  private System.Windows.Forms.CheckBox chkDeleteFile;
		  private System.Windows.Forms.CheckBox chkEnableLog;
		  private System.Windows.Forms.CheckBox chkShowBigButton;
		  private System.Windows.Forms.CheckBox chkEnableWin7;
		  private System.Windows.Forms.CheckBox chkShowTrayIcon;
		  private System.Windows.Forms.TextBox txtSearchText;
		  private System.Windows.Forms.Label label4;
    }
}