﻿namespace Kaedei.AcDown.UI
{
   partial class FormInfo
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
         this.txtInfo = new System.Windows.Forms.TextBox();
         this.btnClose = new System.Windows.Forms.Button();
         this.btnCopy = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // txtInfo
         // 
         this.txtInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.txtInfo.Location = new System.Drawing.Point(12, 12);
         this.txtInfo.Multiline = true;
         this.txtInfo.Name = "txtInfo";
         this.txtInfo.ReadOnly = true;
         this.txtInfo.Size = new System.Drawing.Size(393, 380);
         this.txtInfo.TabIndex = 0;
         // 
         // btnClose
         // 
         this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnClose.Location = new System.Drawing.Point(315, 398);
         this.btnClose.Name = "btnClose";
         this.btnClose.Size = new System.Drawing.Size(90, 38);
         this.btnClose.TabIndex = 1;
         this.btnClose.Text = "关闭(&C)";
         this.btnClose.UseVisualStyleBackColor = true;
         this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
         // 
         // btnCopy
         // 
         this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnCopy.Location = new System.Drawing.Point(12, 398);
         this.btnCopy.Name = "btnCopy";
         this.btnCopy.Size = new System.Drawing.Size(113, 38);
         this.btnCopy.TabIndex = 2;
         this.btnCopy.Text = "复制到剪贴板";
         this.btnCopy.UseVisualStyleBackColor = true;
         this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
         // 
         // FormInfo
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(417, 448);
         this.Controls.Add(this.btnCopy);
         this.Controls.Add(this.btnClose);
         this.Controls.Add(this.txtInfo);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "FormInfo";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "任务信息";
         this.Load += new System.EventHandler(this.FormInfo_Load);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox txtInfo;
      private System.Windows.Forms.Button btnClose;
      private System.Windows.Forms.Button btnCopy;
   }
}