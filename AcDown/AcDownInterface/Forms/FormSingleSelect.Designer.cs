namespace Kaedei.AcDown.Interface.Forms
{
   partial class FormSingleSelect
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
			this.lblTip = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.tmr = new System.Windows.Forms.Timer(this.components);
			this.combo = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// lblTip
			// 
			this.lblTip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTip.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblTip.Location = new System.Drawing.Point(12, 9);
			this.lblTip.Name = "lblTip";
			this.lblTip.Size = new System.Drawing.Size(370, 52);
			this.lblTip.TabIndex = 1;
			this.lblTip.Text = "下载时优先使用以下服务器:";
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(283, 127);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(99, 35);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "确定";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// tmr
			// 
			this.tmr.Enabled = true;
			this.tmr.Interval = 1000;
			this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
			// 
			// combo
			// 
			this.combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.combo.FormattingEnabled = true;
			this.combo.Location = new System.Drawing.Point(50, 64);
			this.combo.Name = "combo";
			this.combo.Size = new System.Drawing.Size(282, 21);
			this.combo.TabIndex = 3;
			// 
			// FormSingleSelect
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(394, 184);
			this.ControlBox = false;
			this.Controls.Add(this.combo);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.lblTip);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FormSingleSelect";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = " 请选择一个选项";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.FormServer_Load);
			this.ResumeLayout(false);

      }

      #endregion

		private System.Windows.Forms.Label lblTip;
      private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Timer tmr;
		private System.Windows.Forms.ComboBox combo;
   }
}