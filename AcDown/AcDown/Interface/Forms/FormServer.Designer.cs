namespace Kaedei.AcDown.Interface.Forms
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
			this.components = new System.ComponentModel.Container();
			this.combo = new System.Windows.Forms.ComboBox();
			this.lblTip = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.tmr = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// combo
			// 
			this.combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.combo.DropDownWidth = 365;
			this.combo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.combo.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.combo.FormattingEnabled = true;
			this.combo.Location = new System.Drawing.Point(62, 79);
			this.combo.Name = "combo";
			this.combo.Size = new System.Drawing.Size(296, 29);
			this.combo.TabIndex = 0;
			// 
			// lblTip
			// 
			this.lblTip.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblTip.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblTip.Location = new System.Drawing.Point(12, 10);
			this.lblTip.Name = "lblTip";
			this.lblTip.Size = new System.Drawing.Size(397, 66);
			this.lblTip.TabIndex = 1;
			this.lblTip.Text = "下载时优先使用以下服务器:";
			// 
			// btnOK
			// 
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnOK.Location = new System.Drawing.Point(292, 138);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(117, 39);
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
			// FormServer
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(421, 190);
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
		private System.Windows.Forms.Timer tmr;
   }
}