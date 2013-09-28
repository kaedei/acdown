namespace Kaedei.AcDown.Interface.Forms
{
   partial class FormLogin
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtUserName = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.lnkRegister = new System.Windows.Forms.LinkLabel();
			this.tmr = new System.Windows.Forms.Timer(this.components);
			this.lblCaptcha = new System.Windows.Forms.Label();
			this.picCaptcha = new System.Windows.Forms.PictureBox();
			this.txtCaptcha = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 5);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(247, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "当前下载需要您输入用户名和密码才能继续：";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(21, 42);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(43, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "用户名";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(21, 82);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(31, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "密码";
			// 
			// txtUserName
			// 
			this.txtUserName.Location = new System.Drawing.Point(80, 39);
			this.txtUserName.Margin = new System.Windows.Forms.Padding(2);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.Size = new System.Drawing.Size(163, 20);
			this.txtUserName.TabIndex = 1;
			this.txtUserName.TextChanged += new System.EventHandler(this.txtUserName_TextChanged);
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(80, 79);
			this.txtPassword.Margin = new System.Windows.Forms.Padding(2);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '●';
			this.txtPassword.Size = new System.Drawing.Size(163, 20);
			this.txtPassword.TabIndex = 2;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(203, 178);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(73, 33);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(118, 178);
			this.btnOK.Margin = new System.Windows.Forms.Padding(2);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(81, 33);
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "登录";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// lnkRegister
			// 
			this.lnkRegister.AutoSize = true;
			this.lnkRegister.Location = new System.Drawing.Point(247, 42);
			this.lnkRegister.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lnkRegister.Name = "lnkRegister";
			this.lnkRegister.Size = new System.Drawing.Size(31, 13);
			this.lnkRegister.TabIndex = 6;
			this.lnkRegister.TabStop = true;
			this.lnkRegister.Text = "注册";
			this.lnkRegister.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRegister_LinkClicked);
			// 
			// tmr
			// 
			this.tmr.Enabled = true;
			this.tmr.Interval = 1000;
			this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
			// 
			// lblCaptcha
			// 
			this.lblCaptcha.AutoSize = true;
			this.lblCaptcha.Location = new System.Drawing.Point(21, 135);
			this.lblCaptcha.Name = "lblCaptcha";
			this.lblCaptcha.Size = new System.Drawing.Size(43, 13);
			this.lblCaptcha.TabIndex = 8;
			this.lblCaptcha.Text = "验证码";
			// 
			// picCaptcha
			// 
			this.picCaptcha.Location = new System.Drawing.Point(176, 118);
			this.picCaptcha.Name = "picCaptcha";
			this.picCaptcha.Size = new System.Drawing.Size(100, 54);
			this.picCaptcha.TabIndex = 9;
			this.picCaptcha.TabStop = false;
			this.picCaptcha.Click += new System.EventHandler(this.picCaptcha_Click);
			// 
			// txtCaptcha
			// 
			this.txtCaptcha.Location = new System.Drawing.Point(80, 132);
			this.txtCaptcha.Name = "txtCaptcha";
			this.txtCaptcha.Size = new System.Drawing.Size(90, 20);
			this.txtCaptcha.TabIndex = 3;
			// 
			// FormLogin
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(291, 223);
			this.Controls.Add(this.txtCaptcha);
			this.Controls.Add(this.picCaptcha);
			this.Controls.Add(this.lblCaptcha);
			this.Controls.Add(this.lnkRegister);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.txtUserName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormLogin";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "用户登录";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.FormLogin_Load);
			((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.TextBox txtUserName;
      private System.Windows.Forms.TextBox txtPassword;
      private System.Windows.Forms.Button btnCancel;
      private System.Windows.Forms.Button btnOK;
      private System.Windows.Forms.LinkLabel lnkRegister;
		private System.Windows.Forms.Timer tmr;
		private System.Windows.Forms.Label lblCaptcha;
		private System.Windows.Forms.PictureBox picCaptcha;
		private System.Windows.Forms.TextBox txtCaptcha;
   }
}