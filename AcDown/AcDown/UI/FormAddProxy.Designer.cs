namespace Kaedei.AcDown.UI
{
   partial class FormAddProxy
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
         this.txtName = new System.Windows.Forms.TextBox();
         this.label2 = new System.Windows.Forms.Label();
         this.txtAddress = new System.Windows.Forms.TextBox();
         this.label3 = new System.Windows.Forms.Label();
         this.txtUsername = new System.Windows.Forms.TextBox();
         this.txtPassword = new System.Windows.Forms.TextBox();
         this.label4 = new System.Windows.Forms.Label();
         this.label5 = new System.Windows.Forms.Label();
         this.btnCancel = new System.Windows.Forms.Button();
         this.btnOK = new System.Windows.Forms.Button();
         this.txtPort = new System.Windows.Forms.TextBox();
         this.SuspendLayout();
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(12, 23);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(65, 20);
         this.label1.TabIndex = 0;
         this.label1.Text = "代理名称";
         // 
         // txtName
         // 
         this.txtName.Location = new System.Drawing.Point(83, 20);
         this.txtName.Name = "txtName";
         this.txtName.Size = new System.Drawing.Size(233, 26);
         this.txtName.TabIndex = 0;
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(26, 55);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(51, 20);
         this.label2.TabIndex = 2;
         this.label2.Text = "服务器";
         // 
         // txtAddress
         // 
         this.txtAddress.Location = new System.Drawing.Point(83, 52);
         this.txtAddress.Name = "txtAddress";
         this.txtAddress.Size = new System.Drawing.Size(136, 26);
         this.txtAddress.TabIndex = 1;
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(225, 55);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(37, 20);
         this.label3.TabIndex = 5;
         this.label3.Text = "端口";
         // 
         // txtUsername
         // 
         this.txtUsername.Location = new System.Drawing.Point(83, 96);
         this.txtUsername.Name = "txtUsername";
         this.txtUsername.Size = new System.Drawing.Size(136, 26);
         this.txtUsername.TabIndex = 3;
         // 
         // txtPassword
         // 
         this.txtPassword.Location = new System.Drawing.Point(83, 128);
         this.txtPassword.Name = "txtPassword";
         this.txtPassword.Size = new System.Drawing.Size(136, 26);
         this.txtPassword.TabIndex = 4;
         // 
         // label4
         // 
         this.label4.AutoSize = true;
         this.label4.Location = new System.Drawing.Point(26, 99);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(51, 20);
         this.label4.TabIndex = 8;
         this.label4.Text = "用户名";
         // 
         // label5
         // 
         this.label5.AutoSize = true;
         this.label5.Location = new System.Drawing.Point(40, 131);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(37, 20);
         this.label5.TabIndex = 9;
         this.label5.Text = "密码";
         // 
         // btnCancel
         // 
         this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnCancel.Location = new System.Drawing.Point(244, 169);
         this.btnCancel.Name = "btnCancel";
         this.btnCancel.Size = new System.Drawing.Size(75, 31);
         this.btnCancel.TabIndex = 6;
         this.btnCancel.Text = "取消";
         this.btnCancel.UseVisualStyleBackColor = true;
         this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
         // 
         // btnOK
         // 
         this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnOK.Location = new System.Drawing.Point(163, 169);
         this.btnOK.Name = "btnOK";
         this.btnOK.Size = new System.Drawing.Size(75, 31);
         this.btnOK.TabIndex = 5;
         this.btnOK.Text = "确定";
         this.btnOK.UseVisualStyleBackColor = true;
         this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
         // 
         // txtPort
         // 
         this.txtPort.Location = new System.Drawing.Point(270, 52);
         this.txtPort.MaxLength = 5;
         this.txtPort.Name = "txtPort";
         this.txtPort.Size = new System.Drawing.Size(46, 26);
         this.txtPort.TabIndex = 2;
         this.txtPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPort_KeyPress);
         // 
         // FormAddProxy
         // 
         this.AcceptButton = this.btnOK;
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.CancelButton = this.btnCancel;
         this.ClientSize = new System.Drawing.Size(331, 212);
         this.Controls.Add(this.txtPort);
         this.Controls.Add(this.btnOK);
         this.Controls.Add(this.btnCancel);
         this.Controls.Add(this.label5);
         this.Controls.Add(this.label4);
         this.Controls.Add(this.txtPassword);
         this.Controls.Add(this.txtUsername);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.txtAddress);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.txtName);
         this.Controls.Add(this.label1);
         this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "FormAddProxy";
         this.ShowIcon = false;
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "编辑代理服务器";
         this.Load += new System.EventHandler(this.FormAddProxy_Load);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.TextBox txtName;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.TextBox txtAddress;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.TextBox txtUsername;
      private System.Windows.Forms.TextBox txtPassword;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Button btnCancel;
      private System.Windows.Forms.Button btnOK;
      private System.Windows.Forms.TextBox txtPort;
   }
}