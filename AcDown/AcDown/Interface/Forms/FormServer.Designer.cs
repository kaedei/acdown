﻿namespace Kaedei.AcDown.Interface.Forms
{
   partial class FormServer
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
         this.combo = new System.Windows.Forms.ComboBox();
         this.lblTip = new System.Windows.Forms.Label();
         this.btnOK = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // combo
         // 
         this.combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.combo.DropDownWidth = 365;
         this.combo.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.combo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.combo.FormattingEnabled = true;
         this.combo.Location = new System.Drawing.Point(62, 73);
         this.combo.Name = "combo";
         this.combo.Size = new System.Drawing.Size(296, 29);
         this.combo.TabIndex = 0;
         // 
         // lblTip
         // 
         this.lblTip.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblTip.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.lblTip.Location = new System.Drawing.Point(12, 9);
         this.lblTip.Name = "lblTip";
         this.lblTip.Size = new System.Drawing.Size(397, 61);
         this.lblTip.TabIndex = 1;
         this.lblTip.Text = "下载时优先使用以下服务器:";
         // 
         // btnOK
         // 
         this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnOK.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.btnOK.Location = new System.Drawing.Point(313, 127);
         this.btnOK.Name = "btnOK";
         this.btnOK.Size = new System.Drawing.Size(96, 36);
         this.btnOK.TabIndex = 2;
         this.btnOK.Text = "确定";
         this.btnOK.UseVisualStyleBackColor = true;
         this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
         // 
         // FormServer
         // 
         this.AcceptButton = this.btnOK;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(421, 175);
         this.ControlBox = false;
         this.Controls.Add(this.btnOK);
         this.Controls.Add(this.lblTip);
         this.Controls.Add(this.combo);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
         this.Name = "FormServer";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.TopMost = true;
         this.Load += new System.EventHandler(this.FormServer_Load);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.ComboBox combo;
      private System.Windows.Forms.Label lblTip;
      private System.Windows.Forms.Button btnOK;
   }
}