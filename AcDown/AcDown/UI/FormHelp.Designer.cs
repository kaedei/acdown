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
         this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
         this.label1 = new System.Windows.Forms.Label();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.lnkCheckUpdate = new System.Windows.Forms.LinkLabel();
         this.lblVersion = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.btnSendEmail = new System.Windows.Forms.Button();
         this.btnCopyEmail = new System.Windows.Forms.Button();
         this.txtEmail = new System.Windows.Forms.TextBox();
         this.groupBox3 = new System.Windows.Forms.GroupBox();
         this.lnkFAQ = new System.Windows.Forms.LinkLabel();
         this.lnkAdvise = new System.Windows.Forms.LinkLabel();
         this.lnkReportBug = new System.Windows.Forms.LinkLabel();
         this.groupBox4 = new System.Windows.Forms.GroupBox();
         this.lnkFeed = new System.Windows.Forms.LinkLabel();
         this.btnClose = new System.Windows.Forms.Button();
         this.panel1 = new System.Windows.Forms.Panel();
         this.lnkWeibo = new System.Windows.Forms.LinkLabel();
         this.lnkBlog = new System.Windows.Forms.LinkLabel();
         this.lnkProject = new System.Windows.Forms.LinkLabel();
         this.tableLayoutPanel1.SuspendLayout();
         this.groupBox1.SuspendLayout();
         this.groupBox2.SuspendLayout();
         this.groupBox3.SuspendLayout();
         this.groupBox4.SuspendLayout();
         this.panel1.SuspendLayout();
         this.SuspendLayout();
         // 
         // tableLayoutPanel1
         // 
         this.tableLayoutPanel1.ColumnCount = 2;
         this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
         this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
         this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
         this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
         this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 1);
         this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 2);
         this.tableLayoutPanel1.Controls.Add(this.groupBox4, 1, 2);
         this.tableLayoutPanel1.Controls.Add(this.btnClose, 1, 3);
         this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 3);
         this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
         this.tableLayoutPanel1.Name = "tableLayoutPanel1";
         this.tableLayoutPanel1.RowCount = 4;
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.778F));
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.778F));
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.444F));
         this.tableLayoutPanel1.Size = new System.Drawing.Size(600, 413);
         this.tableLayoutPanel1.TabIndex = 0;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.label1.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.label1.Location = new System.Drawing.Point(3, 0);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(146, 41);
         this.label1.TabIndex = 0;
         this.label1.Text = "帮助中心";
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.lnkCheckUpdate);
         this.groupBox1.Controls.Add(this.lblVersion);
         this.groupBox1.Controls.Add(this.label2);
         this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.groupBox1.Location = new System.Drawing.Point(3, 44);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(294, 156);
         this.groupBox1.TabIndex = 1;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "检查新版本";
         // 
         // lnkCheckUpdate
         // 
         this.lnkCheckUpdate.AutoSize = true;
         this.lnkCheckUpdate.Location = new System.Drawing.Point(62, 68);
         this.lnkCheckUpdate.Name = "lnkCheckUpdate";
         this.lnkCheckUpdate.Size = new System.Drawing.Size(170, 21);
         this.lnkCheckUpdate.TabIndex = 0;
         this.lnkCheckUpdate.TabStop = true;
         this.lnkCheckUpdate.Text = "检查是否有更新的版本";
         this.lnkCheckUpdate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCheckUpdate_LinkClicked);
         // 
         // lblVersion
         // 
         this.lblVersion.AutoSize = true;
         this.lblVersion.Location = new System.Drawing.Point(105, 36);
         this.lblVersion.Name = "lblVersion";
         this.lblVersion.Size = new System.Drawing.Size(58, 21);
         this.lblVersion.TabIndex = 1;
         this.lblVersion.Text = "3.0.0.0";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(9, 36);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(94, 21);
         this.label2.TabIndex = 0;
         this.label2.Text = "当前版本为:";
         // 
         // groupBox2
         // 
         this.groupBox2.Controls.Add(this.btnSendEmail);
         this.groupBox2.Controls.Add(this.btnCopyEmail);
         this.groupBox2.Controls.Add(this.txtEmail);
         this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
         this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.groupBox2.Location = new System.Drawing.Point(303, 44);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(294, 156);
         this.groupBox2.TabIndex = 2;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "联系作者(推荐)";
         // 
         // btnSendEmail
         // 
         this.btnSendEmail.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
         this.btnSendEmail.Location = new System.Drawing.Point(150, 78);
         this.btnSendEmail.Name = "btnSendEmail";
         this.btnSendEmail.Size = new System.Drawing.Size(91, 36);
         this.btnSendEmail.TabIndex = 3;
         this.btnSendEmail.Text = "发送邮件";
         this.btnSendEmail.UseVisualStyleBackColor = true;
         this.btnSendEmail.Click += new System.EventHandler(this.btnSendEmail_Click);
         // 
         // btnCopyEmail
         // 
         this.btnCopyEmail.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
         this.btnCopyEmail.Location = new System.Drawing.Point(53, 78);
         this.btnCopyEmail.Name = "btnCopyEmail";
         this.btnCopyEmail.Size = new System.Drawing.Size(91, 36);
         this.btnCopyEmail.TabIndex = 2;
         this.btnCopyEmail.Text = "复制地址";
         this.btnCopyEmail.UseVisualStyleBackColor = true;
         this.btnCopyEmail.Click += new System.EventHandler(this.btnCopyEmail_Click);
         // 
         // txtEmail
         // 
         this.txtEmail.Location = new System.Drawing.Point(53, 43);
         this.txtEmail.Name = "txtEmail";
         this.txtEmail.ReadOnly = true;
         this.txtEmail.Size = new System.Drawing.Size(188, 29);
         this.txtEmail.TabIndex = 1;
         this.txtEmail.Text = "kaedei@foxmail.com";
         // 
         // groupBox3
         // 
         this.groupBox3.Controls.Add(this.lnkFAQ);
         this.groupBox3.Controls.Add(this.lnkAdvise);
         this.groupBox3.Controls.Add(this.lnkReportBug);
         this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
         this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.groupBox3.Location = new System.Drawing.Point(3, 206);
         this.groupBox3.Name = "groupBox3";
         this.groupBox3.Size = new System.Drawing.Size(294, 156);
         this.groupBox3.TabIndex = 3;
         this.groupBox3.TabStop = false;
         this.groupBox3.Text = "程序反馈";
         // 
         // lnkFAQ
         // 
         this.lnkFAQ.AutoSize = true;
         this.lnkFAQ.Location = new System.Drawing.Point(110, 102);
         this.lnkFAQ.Name = "lnkFAQ";
         this.lnkFAQ.Size = new System.Drawing.Size(74, 21);
         this.lnkFAQ.TabIndex = 5;
         this.lnkFAQ.TabStop = true;
         this.lnkFAQ.Text = "常见问题";
         this.lnkFAQ.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkFAQ_LinkClicked);
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
         this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
         this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.groupBox4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.groupBox4.Location = new System.Drawing.Point(303, 206);
         this.groupBox4.Name = "groupBox4";
         this.groupBox4.Size = new System.Drawing.Size(294, 156);
         this.groupBox4.TabIndex = 4;
         this.groupBox4.TabStop = false;
         this.groupBox4.Text = "订阅邮件";
         // 
         // lnkFeed
         // 
         this.lnkFeed.Image = global::Kaedei.AcDown.Properties.Resources.UpdateNoti;
         this.lnkFeed.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
         this.lnkFeed.Location = new System.Drawing.Point(22, 47);
         this.lnkFeed.Name = "lnkFeed";
         this.lnkFeed.Size = new System.Drawing.Size(250, 63);
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
         this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
         this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.btnClose.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.btnClose.Location = new System.Drawing.Point(489, 368);
         this.btnClose.Name = "btnClose";
         this.btnClose.Size = new System.Drawing.Size(108, 42);
         this.btnClose.TabIndex = 10;
         this.btnClose.Text = "关闭";
         this.btnClose.UseVisualStyleBackColor = true;
         this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
         // 
         // panel1
         // 
         this.panel1.Controls.Add(this.lnkWeibo);
         this.panel1.Controls.Add(this.lnkBlog);
         this.panel1.Controls.Add(this.lnkProject);
         this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel1.Location = new System.Drawing.Point(3, 368);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(294, 42);
         this.panel1.TabIndex = 6;
         // 
         // lnkWeibo
         // 
         this.lnkWeibo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.lnkWeibo.Image = global::Kaedei.AcDown.Properties.Resources.Weibo;
         this.lnkWeibo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.lnkWeibo.Location = new System.Drawing.Point(192, 0);
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
         this.lnkBlog.Location = new System.Drawing.Point(89, 12);
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
         this.lnkProject.Location = new System.Drawing.Point(9, 12);
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
         this.ClientSize = new System.Drawing.Size(600, 413);
         this.Controls.Add(this.tableLayoutPanel1);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "FormHelp";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Load += new System.EventHandler(this.FormHelp_Load);
         this.tableLayoutPanel1.ResumeLayout(false);
         this.tableLayoutPanel1.PerformLayout();
         this.groupBox1.ResumeLayout(false);
         this.groupBox1.PerformLayout();
         this.groupBox2.ResumeLayout(false);
         this.groupBox2.PerformLayout();
         this.groupBox3.ResumeLayout(false);
         this.groupBox3.PerformLayout();
         this.groupBox4.ResumeLayout(false);
         this.panel1.ResumeLayout(false);
         this.panel1.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.GroupBox groupBox3;
      private System.Windows.Forms.GroupBox groupBox4;
      private System.Windows.Forms.Button btnClose;
      private System.Windows.Forms.LinkLabel lnkCheckUpdate;
      private System.Windows.Forms.Label lblVersion;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.LinkLabel lnkFAQ;
      private System.Windows.Forms.LinkLabel lnkReportBug;
      private System.Windows.Forms.LinkLabel lnkAdvise;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.LinkLabel lnkBlog;
      private System.Windows.Forms.LinkLabel lnkProject;
      private System.Windows.Forms.LinkLabel lnkFeed;
      private System.Windows.Forms.LinkLabel lnkWeibo;
      private System.Windows.Forms.Button btnCopyEmail;
      private System.Windows.Forms.TextBox txtEmail;
      private System.Windows.Forms.Button btnSendEmail;
   }
}