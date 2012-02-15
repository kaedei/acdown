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
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.btnSendEmail = new System.Windows.Forms.Button();
         this.btnCopyEmail = new System.Windows.Forms.Button();
         this.txtEmail = new System.Windows.Forms.TextBox();
         this.groupBox3 = new System.Windows.Forms.GroupBox();
         this.lnkAdvise = new System.Windows.Forms.LinkLabel();
         this.lnkReportBug = new System.Windows.Forms.LinkLabel();
         this.groupBox4 = new System.Windows.Forms.GroupBox();
         this.lnkFeed = new System.Windows.Forms.LinkLabel();
         this.btnClose = new System.Windows.Forms.Button();
         this.lnkWeibo = new System.Windows.Forms.LinkLabel();
         this.lnkBlog = new System.Windows.Forms.LinkLabel();
         this.lnkProject = new System.Windows.Forms.LinkLabel();
         this.groupBox2.SuspendLayout();
         this.groupBox3.SuspendLayout();
         this.groupBox4.SuspendLayout();
         this.SuspendLayout();
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.label1.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.label1.Location = new System.Drawing.Point(12, 9);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(146, 41);
         this.label1.TabIndex = 0;
         this.label1.Text = "帮助中心";
         // 
         // groupBox2
         // 
         this.groupBox2.Controls.Add(this.btnSendEmail);
         this.groupBox2.Controls.Add(this.btnCopyEmail);
         this.groupBox2.Controls.Add(this.txtEmail);
         this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.groupBox2.Location = new System.Drawing.Point(12, 166);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(304, 130);
         this.groupBox2.TabIndex = 2;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "联系作者(推荐)";
         // 
         // btnSendEmail
         // 
         this.btnSendEmail.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
         this.btnSendEmail.Location = new System.Drawing.Point(201, 68);
         this.btnSendEmail.Name = "btnSendEmail";
         this.btnSendEmail.Size = new System.Drawing.Size(91, 30);
         this.btnSendEmail.TabIndex = 3;
         this.btnSendEmail.Text = "发送邮件";
         this.btnSendEmail.UseVisualStyleBackColor = true;
         this.btnSendEmail.Click += new System.EventHandler(this.btnSendEmail_Click);
         // 
         // btnCopyEmail
         // 
         this.btnCopyEmail.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
         this.btnCopyEmail.Location = new System.Drawing.Point(201, 33);
         this.btnCopyEmail.Name = "btnCopyEmail";
         this.btnCopyEmail.Size = new System.Drawing.Size(91, 29);
         this.btnCopyEmail.TabIndex = 2;
         this.btnCopyEmail.Text = "复制地址";
         this.btnCopyEmail.UseVisualStyleBackColor = true;
         this.btnCopyEmail.Click += new System.EventHandler(this.btnCopyEmail_Click);
         // 
         // txtEmail
         // 
         this.txtEmail.Location = new System.Drawing.Point(7, 51);
         this.txtEmail.Name = "txtEmail";
         this.txtEmail.ReadOnly = true;
         this.txtEmail.Size = new System.Drawing.Size(188, 29);
         this.txtEmail.TabIndex = 1;
         this.txtEmail.Text = "kaedei@foxmail.com";
         // 
         // groupBox3
         // 
         this.groupBox3.Controls.Add(this.lnkAdvise);
         this.groupBox3.Controls.Add(this.lnkReportBug);
         this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.groupBox3.Location = new System.Drawing.Point(12, 53);
         this.groupBox3.Name = "groupBox3";
         this.groupBox3.Size = new System.Drawing.Size(304, 107);
         this.groupBox3.TabIndex = 3;
         this.groupBox3.TabStop = false;
         this.groupBox3.Text = "程序反馈";
         // 
         // lnkAdvise
         // 
         this.lnkAdvise.AutoSize = true;
         this.lnkAdvise.Location = new System.Drawing.Point(30, 67);
         this.lnkAdvise.Name = "lnkAdvise";
         this.lnkAdvise.Size = new System.Drawing.Size(235, 21);
         this.lnkAdvise.TabIndex = 4;
         this.lnkAdvise.TabStop = true;
         this.lnkAdvise.Text = "提交新功能建议 / 解析更多网站";
         this.lnkAdvise.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAdvise_LinkClicked);
         // 
         // lnkReportBug
         // 
         this.lnkReportBug.AutoSize = true;
         this.lnkReportBug.Location = new System.Drawing.Point(89, 34);
         this.lnkReportBug.Name = "lnkReportBug";
         this.lnkReportBug.Size = new System.Drawing.Size(122, 21);
         this.lnkReportBug.TabIndex = 3;
         this.lnkReportBug.TabStop = true;
         this.lnkReportBug.Text = "下载时出现错误";
         this.lnkReportBug.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkReportBug_LinkClicked);
         // 
         // groupBox4
         // 
         this.groupBox4.Controls.Add(this.lnkFeed);
         this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.groupBox4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.groupBox4.Location = new System.Drawing.Point(12, 302);
         this.groupBox4.Name = "groupBox4";
         this.groupBox4.Size = new System.Drawing.Size(304, 105);
         this.groupBox4.TabIndex = 4;
         this.groupBox4.TabStop = false;
         this.groupBox4.Text = "订阅邮件";
         // 
         // lnkFeed
         // 
         this.lnkFeed.Image = global::Kaedei.AcDown.Properties.Resources.UpdateNoti;
         this.lnkFeed.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
         this.lnkFeed.Location = new System.Drawing.Point(27, 22);
         this.lnkFeed.Name = "lnkFeed";
         this.lnkFeed.Size = new System.Drawing.Size(250, 60);
         this.lnkFeed.TabIndex = 6;
         this.lnkFeed.TabStop = true;
         this.lnkFeed.Text = "【新】第一时间获得新版本信息";
         this.lnkFeed.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
         this.lnkFeed.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkFeed_LinkClicked);
         // 
         // btnClose
         // 
         this.btnClose.Cursor = System.Windows.Forms.Cursors.Default;
         this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.btnClose.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.btnClose.Location = new System.Drawing.Point(208, 415);
         this.btnClose.Name = "btnClose";
         this.btnClose.Size = new System.Drawing.Size(108, 37);
         this.btnClose.TabIndex = 10;
         this.btnClose.Text = "关闭";
         this.btnClose.UseVisualStyleBackColor = true;
         this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
         // 
         // lnkWeibo
         // 
         this.lnkWeibo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.lnkWeibo.Image = global::Kaedei.AcDown.Properties.Resources.Weibo;
         this.lnkWeibo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.lnkWeibo.Location = new System.Drawing.Point(92, 410);
         this.lnkWeibo.Name = "lnkWeibo";
         this.lnkWeibo.Size = new System.Drawing.Size(77, 42);
         this.lnkWeibo.TabIndex = 9;
         this.lnkWeibo.TabStop = true;
         this.lnkWeibo.Text = "微博";
         this.lnkWeibo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         this.lnkWeibo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkWeibo_LinkClicked);
         // 
         // lnkBlog
         // 
         this.lnkBlog.AutoSize = true;
         this.lnkBlog.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.lnkBlog.Location = new System.Drawing.Point(12, 431);
         this.lnkBlog.Name = "lnkBlog";
         this.lnkBlog.Size = new System.Drawing.Size(74, 21);
         this.lnkBlog.TabIndex = 8;
         this.lnkBlog.TabStop = true;
         this.lnkBlog.Text = "作者博客";
         this.lnkBlog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkBlog_LinkClicked);
         // 
         // lnkProject
         // 
         this.lnkProject.AutoSize = true;
         this.lnkProject.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.lnkProject.Location = new System.Drawing.Point(12, 410);
         this.lnkProject.Name = "lnkProject";
         this.lnkProject.Size = new System.Drawing.Size(74, 21);
         this.lnkProject.TabIndex = 7;
         this.lnkProject.TabStop = true;
         this.lnkProject.Text = "项目主页";
         this.lnkProject.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkProject_LinkClicked);
         // 
         // FormHelp
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.CancelButton = this.btnClose;
         this.ClientSize = new System.Drawing.Size(330, 467);
         this.Controls.Add(this.lnkWeibo);
         this.Controls.Add(this.lnkBlog);
         this.Controls.Add(this.btnClose);
         this.Controls.Add(this.lnkProject);
         this.Controls.Add(this.groupBox4);
         this.Controls.Add(this.groupBox3);
         this.Controls.Add(this.groupBox2);
         this.Controls.Add(this.label1);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "FormHelp";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Load += new System.EventHandler(this.FormHelp_Load);
         this.groupBox2.ResumeLayout(false);
         this.groupBox2.PerformLayout();
         this.groupBox3.ResumeLayout(false);
         this.groupBox3.PerformLayout();
         this.groupBox4.ResumeLayout(false);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.GroupBox groupBox3;
      private System.Windows.Forms.GroupBox groupBox4;
      private System.Windows.Forms.Button btnClose;
      private System.Windows.Forms.LinkLabel lnkReportBug;
      private System.Windows.Forms.LinkLabel lnkAdvise;
      private System.Windows.Forms.LinkLabel lnkBlog;
      private System.Windows.Forms.LinkLabel lnkProject;
      private System.Windows.Forms.LinkLabel lnkFeed;
      private System.Windows.Forms.LinkLabel lnkWeibo;
      private System.Windows.Forms.Button btnCopyEmail;
      private System.Windows.Forms.TextBox txtEmail;
      private System.Windows.Forms.Button btnSendEmail;
   }
}