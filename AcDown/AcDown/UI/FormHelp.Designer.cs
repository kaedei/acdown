namespace Kaedei.AcDown.UI
{
   partial class FormHelp
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
			this.label1 = new System.Windows.Forms.Label();
			this.btnSendEmail = new System.Windows.Forms.Button();
			this.btnCopyEmail = new System.Windows.Forms.Button();
			this.txtEmail = new System.Windows.Forms.TextBox();
			this.lnkAdvise = new System.Windows.Forms.LinkLabel();
			this.lnkReportBug = new System.Windows.Forms.LinkLabel();
			this.lnkFeed = new System.Windows.Forms.LinkLabel();
			this.btnClose = new System.Windows.Forms.Button();
			this.lnkWeibo = new System.Windows.Forms.LinkLabel();
			this.lnkBlog = new System.Windows.Forms.LinkLabel();
			this.lnkProjectHome = new System.Windows.Forms.LinkLabel();
			this.lnkProjectDocumentationDeveloper = new System.Windows.Forms.LinkLabel();
			this.lnkQAAcplay = new System.Windows.Forms.LinkLabel();
			this.label2 = new System.Windows.Forms.Label();
			this.lnkQA = new System.Windows.Forms.LinkLabel();
			this.lnkProjectDiscussion = new System.Windows.Forms.LinkLabel();
			this.lnkProjectIssueTracker = new System.Windows.Forms.LinkLabel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.lnkProjectDocumentationUser = new System.Windows.Forms.LinkLabel();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(14, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(114, 41);
			this.label1.TabIndex = 0;
			this.label1.Text = "万事屋";
			// 
			// btnSendEmail
			// 
			this.btnSendEmail.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnSendEmail.Location = new System.Drawing.Point(106, 135);
			this.btnSendEmail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnSendEmail.Name = "btnSendEmail";
			this.btnSendEmail.Size = new System.Drawing.Size(83, 23);
			this.btnSendEmail.TabIndex = 3;
			this.btnSendEmail.Text = "发送邮件";
			this.btnSendEmail.UseVisualStyleBackColor = true;
			this.btnSendEmail.Click += new System.EventHandler(this.btnSendEmail_Click);
			// 
			// btnCopyEmail
			// 
			this.btnCopyEmail.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnCopyEmail.Location = new System.Drawing.Point(17, 135);
			this.btnCopyEmail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnCopyEmail.Name = "btnCopyEmail";
			this.btnCopyEmail.Size = new System.Drawing.Size(83, 23);
			this.btnCopyEmail.TabIndex = 2;
			this.btnCopyEmail.Text = "复制地址";
			this.btnCopyEmail.UseVisualStyleBackColor = true;
			this.btnCopyEmail.Click += new System.EventHandler(this.btnCopyEmail_Click);
			// 
			// txtEmail
			// 
			this.txtEmail.Location = new System.Drawing.Point(14, 104);
			this.txtEmail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txtEmail.Name = "txtEmail";
			this.txtEmail.ReadOnly = true;
			this.txtEmail.Size = new System.Drawing.Size(169, 23);
			this.txtEmail.TabIndex = 1;
			this.txtEmail.Text = "kaedei@foxmail.com";
			// 
			// lnkAdvise
			// 
			this.lnkAdvise.AutoSize = true;
			this.lnkAdvise.Location = new System.Drawing.Point(14, 46);
			this.lnkAdvise.Name = "lnkAdvise";
			this.lnkAdvise.Size = new System.Drawing.Size(92, 17);
			this.lnkAdvise.TabIndex = 4;
			this.lnkAdvise.TabStop = true;
			this.lnkAdvise.Text = "提交新功能建议";
			this.lnkAdvise.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAdvise_LinkClicked);
			// 
			// lnkReportBug
			// 
			this.lnkReportBug.AutoSize = true;
			this.lnkReportBug.Location = new System.Drawing.Point(14, 29);
			this.lnkReportBug.Name = "lnkReportBug";
			this.lnkReportBug.Size = new System.Drawing.Size(92, 17);
			this.lnkReportBug.TabIndex = 3;
			this.lnkReportBug.TabStop = true;
			this.lnkReportBug.Text = "下载时出现错误";
			this.lnkReportBug.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkReportBug_LinkClicked);
			// 
			// lnkFeed
			// 
			this.lnkFeed.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lnkFeed.Image = global::Kaedei.AcDown.Properties.Resources.UpdateNoti;
			this.lnkFeed.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.lnkFeed.Location = new System.Drawing.Point(11, 171);
			this.lnkFeed.Name = "lnkFeed";
			this.lnkFeed.Size = new System.Drawing.Size(172, 57);
			this.lnkFeed.TabIndex = 6;
			this.lnkFeed.TabStop = true;
			this.lnkFeed.Text = "第一时间获得新版本信息";
			this.lnkFeed.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.lnkFeed.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkFeed_LinkClicked);
			this.lnkFeed.Click += new System.EventHandler(this.lnkFeed_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Cursor = System.Windows.Forms.Cursors.Default;
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.Location = new System.Drawing.Point(375, 368);
			this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(91, 35);
			this.btnClose.TabIndex = 10;
			this.btnClose.Text = "关闭";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// lnkWeibo
			// 
			this.lnkWeibo.Image = global::Kaedei.AcDown.Properties.Resources.Weibo;
			this.lnkWeibo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lnkWeibo.Location = new System.Drawing.Point(132, 251);
			this.lnkWeibo.Name = "lnkWeibo";
			this.lnkWeibo.Size = new System.Drawing.Size(69, 34);
			this.lnkWeibo.TabIndex = 9;
			this.lnkWeibo.TabStop = true;
			this.lnkWeibo.Text = "微博";
			this.lnkWeibo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lnkWeibo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkWeibo_LinkClicked);
			// 
			// lnkBlog
			// 
			this.lnkBlog.AutoSize = true;
			this.lnkBlog.Location = new System.Drawing.Point(6, 261);
			this.lnkBlog.Name = "lnkBlog";
			this.lnkBlog.Size = new System.Drawing.Size(108, 17);
			this.lnkBlog.TabIndex = 8;
			this.lnkBlog.TabStop = true;
			this.lnkBlog.Text = "Kaedei的个人博客";
			this.lnkBlog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkBlog_LinkClicked);
			// 
			// lnkProjectHome
			// 
			this.lnkProjectHome.AutoSize = true;
			this.lnkProjectHome.Location = new System.Drawing.Point(6, 105);
			this.lnkProjectHome.Name = "lnkProjectHome";
			this.lnkProjectHome.Size = new System.Drawing.Size(64, 17);
			this.lnkProjectHome.TabIndex = 7;
			this.lnkProjectHome.TabStop = true;
			this.lnkProjectHome.Text = "[官网]主页";
			this.lnkProjectHome.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkProject_LinkClicked);
			// 
			// lnkProjectDocumentationDeveloper
			// 
			this.lnkProjectDocumentationDeveloper.AutoSize = true;
			this.lnkProjectDocumentationDeveloper.Location = new System.Drawing.Point(6, 173);
			this.lnkProjectDocumentationDeveloper.Name = "lnkProjectDocumentationDeveloper";
			this.lnkProjectDocumentationDeveloper.Size = new System.Drawing.Size(100, 17);
			this.lnkProjectDocumentationDeveloper.TabIndex = 11;
			this.lnkProjectDocumentationDeveloper.TabStop = true;
			this.lnkProjectDocumentationDeveloper.Text = "[官网]开发者文档";
			this.lnkProjectDocumentationDeveloper.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkProjectDocumentationDeveloper_LinkClicked);
			// 
			// lnkQAAcplay
			// 
			this.lnkQAAcplay.AutoSize = true;
			this.lnkQAAcplay.Location = new System.Drawing.Point(6, 46);
			this.lnkQAAcplay.Name = "lnkQAAcplay";
			this.lnkQAAcplay.Size = new System.Drawing.Size(141, 17);
			this.lnkQAAcplay.TabIndex = 12;
			this.lnkQAAcplay.TabStop = true;
			this.lnkQAAcplay.Text = "AcPlay常见问题&使用说明";
			this.lnkQAAcplay.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkQAAcplay_LinkClicked);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 83);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(83, 17);
			this.label2.TabIndex = 13;
			this.label2.Text = "给作者发邮件:";
			// 
			// lnkQA
			// 
			this.lnkQA.AutoSize = true;
			this.lnkQA.Location = new System.Drawing.Point(6, 29);
			this.lnkQA.Name = "lnkQA";
			this.lnkQA.Size = new System.Drawing.Size(103, 17);
			this.lnkQA.TabIndex = 14;
			this.lnkQA.TabStop = true;
			this.lnkQA.Text = "AcDown常见问题";
			this.lnkQA.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkQA_LinkClicked);
			// 
			// lnkProjectDiscussion
			// 
			this.lnkProjectDiscussion.AutoSize = true;
			this.lnkProjectDiscussion.Location = new System.Drawing.Point(6, 122);
			this.lnkProjectDiscussion.Name = "lnkProjectDiscussion";
			this.lnkProjectDiscussion.Size = new System.Drawing.Size(76, 17);
			this.lnkProjectDiscussion.TabIndex = 15;
			this.lnkProjectDiscussion.TabStop = true;
			this.lnkProjectDiscussion.Text = "[官网]讨论区";
			this.lnkProjectDiscussion.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkProjectDiscussion_LinkClicked);
			// 
			// lnkProjectIssueTracker
			// 
			this.lnkProjectIssueTracker.AutoSize = true;
			this.lnkProjectIssueTracker.Location = new System.Drawing.Point(6, 139);
			this.lnkProjectIssueTracker.Name = "lnkProjectIssueTracker";
			this.lnkProjectIssueTracker.Size = new System.Drawing.Size(100, 17);
			this.lnkProjectIssueTracker.TabIndex = 16;
			this.lnkProjectIssueTracker.TabStop = true;
			this.lnkProjectIssueTracker.Text = "[官网]问题追踪区";
			this.lnkProjectIssueTracker.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkProjectIssueTracker_LinkClicked);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lnkQA);
			this.groupBox1.Controls.Add(this.lnkQAAcplay);
			this.groupBox1.Location = new System.Drawing.Point(21, 69);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(216, 82);
			this.groupBox1.TabIndex = 17;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "推荐链接";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.lnkProjectDocumentationUser);
			this.groupBox2.Controls.Add(this.lnkProjectDiscussion);
			this.groupBox2.Controls.Add(this.lnkProjectIssueTracker);
			this.groupBox2.Controls.Add(this.lnkProjectHome);
			this.groupBox2.Controls.Add(this.lnkProjectDocumentationDeveloper);
			this.groupBox2.Location = new System.Drawing.Point(21, 157);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(216, 204);
			this.groupBox2.TabIndex = 18;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "官网(需要注册帐号)";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(6, 19);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(204, 73);
			this.label3.TabIndex = 20;
			this.label3.Text = "说明:官网是最高效和快速的反馈方式，你需要拥有一个Live ID或Codeplex帐号才可使用发帖/评论/创建新工作项的功能";
			// 
			// lnkProjectDocumentationUser
			// 
			this.lnkProjectDocumentationUser.AutoSize = true;
			this.lnkProjectDocumentationUser.Location = new System.Drawing.Point(6, 156);
			this.lnkProjectDocumentationUser.Name = "lnkProjectDocumentationUser";
			this.lnkProjectDocumentationUser.Size = new System.Drawing.Size(88, 17);
			this.lnkProjectDocumentationUser.TabIndex = 19;
			this.lnkProjectDocumentationUser.TabStop = true;
			this.lnkProjectDocumentationUser.Text = "[官网]用户文档";
			this.lnkProjectDocumentationUser.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkProjectDocumentationUser_LinkClicked);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.lnkReportBug);
			this.groupBox3.Controls.Add(this.lnkAdvise);
			this.groupBox3.Controls.Add(this.btnSendEmail);
			this.groupBox3.Controls.Add(this.lnkFeed);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.lnkWeibo);
			this.groupBox3.Controls.Add(this.txtEmail);
			this.groupBox3.Controls.Add(this.lnkBlog);
			this.groupBox3.Controls.Add(this.btnCopyEmail);
			this.groupBox3.Location = new System.Drawing.Point(243, 69);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(223, 292);
			this.groupBox3.TabIndex = 19;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "其他反馈方式";
			// 
			// FormHelp
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(478, 412);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormHelp";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.FormHelp_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

      }

      #endregion

		private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Button btnClose;
      private System.Windows.Forms.LinkLabel lnkReportBug;
      private System.Windows.Forms.LinkLabel lnkAdvise;
      private System.Windows.Forms.LinkLabel lnkBlog;
      private System.Windows.Forms.LinkLabel lnkProjectHome;
      private System.Windows.Forms.LinkLabel lnkFeed;
      private System.Windows.Forms.LinkLabel lnkWeibo;
      private System.Windows.Forms.Button btnCopyEmail;
      private System.Windows.Forms.TextBox txtEmail;
      private System.Windows.Forms.Button btnSendEmail;
		private System.Windows.Forms.LinkLabel lnkProjectDocumentationDeveloper;
		private System.Windows.Forms.LinkLabel lnkQAAcplay;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.LinkLabel lnkQA;
		private System.Windows.Forms.LinkLabel lnkProjectDiscussion;
		private System.Windows.Forms.LinkLabel lnkProjectIssueTracker;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.LinkLabel lnkProjectDocumentationUser;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label3;
   }
}