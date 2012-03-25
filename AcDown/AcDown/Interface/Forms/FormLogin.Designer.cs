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
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(330, 21);
			this.label1.TabIndex = 0;
			this.label1.Text = "当前下载需要您输入用户名和密码才能继续：";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(28, 45);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 21);
			this.label2.TabIndex = 1;
			this.label2.Text = "用户名";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(28, 87);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(42, 21);
			this.label3.TabIndex = 2;
			this.label3.Text = "密码";
			// 
			// txtUserName
			// 
			this.txtUserName.Location = new System.Drawing.Point(126, 42);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.Size = new System.Drawing.Size(224, 29);
			this.txtUserName.TabIndex = 3;
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(126, 84);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '●';
			this.txtPassword.Size = new System.Drawing.Size(224, 29);
			this.txtPassword.TabIndex = 4;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(313, 127);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(96, 36);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(211, 127);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(96, 36);
			this.btnOK.TabIndex = 6;
			this.btnOK.Text = "登录";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// lnkRegister
			// 
			this.lnkRegister.AutoSize = true;
			this.lnkRegister.Location = new System.Drawing.Point(356, 45);
			this.lnkRegister.Name = "lnkRegister";
			this.lnkRegister.Size = new System.Drawing.Size(42, 21);
			this.lnkRegister.TabIndex = 7;
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
			// FormLogin
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(421, 175);
			this.ControlBox = false;
			this.Controls.Add(this.lnkRegister);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.txtUserName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(5);
			this.Name = "FormLogin";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "用户登录";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.FormLogin_Load);
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
   }
}