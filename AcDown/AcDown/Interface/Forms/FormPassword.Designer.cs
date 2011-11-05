namespace Kaedei.AcDown.Interface.Forms
{
   partial class FormPassword
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
         this.lblTipText = new System.Windows.Forms.Label();
         this.txtPassword = new System.Windows.Forms.TextBox();
         this.btnOK = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // lblTipText
         // 
         this.lblTipText.AutoSize = true;
         this.lblTipText.Location = new System.Drawing.Point(12, 9);
         this.lblTipText.Name = "lblTipText";
         this.lblTipText.Size = new System.Drawing.Size(250, 21);
         this.lblTipText.TabIndex = 0;
         this.lblTipText.Text = "当前下载需要输入密码才能继续：";
         // 
         // txtPassword
         // 
         this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
         this.txtPassword.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.txtPassword.Location = new System.Drawing.Point(16, 30);
         this.txtPassword.Name = "txtPassword";
         this.txtPassword.Size = new System.Drawing.Size(362, 34);
         this.txtPassword.TabIndex = 1;
         // 
         // btnOK
         // 
         this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Right;
         this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnOK.Location = new System.Drawing.Point(282, 71);
         this.btnOK.Name = "btnOK";
         this.btnOK.Size = new System.Drawing.Size(96, 36);
         this.btnOK.TabIndex = 2;
         this.btnOK.Text = "确定";
         this.btnOK.UseVisualStyleBackColor = true;
         this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
         // 
         // FormPassword
         // 
         this.AcceptButton = this.btnOK;
         this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(390, 125);
         this.ControlBox = false;
         this.Controls.Add(this.btnOK);
         this.Controls.Add(this.txtPassword);
         this.Controls.Add(this.lblTipText);
         this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.Margin = new System.Windows.Forms.Padding(5);
         this.MaximumSize = new System.Drawing.Size(647, 163);
         this.MinimumSize = new System.Drawing.Size(304, 163);
         this.Name = "FormPassword";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "请输入密码";
         this.TopMost = true;
         this.Load += new System.EventHandler(this.FormPassword_Load);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label lblTipText;
      private System.Windows.Forms.TextBox txtPassword;
      private System.Windows.Forms.Button btnOK;
   }
}