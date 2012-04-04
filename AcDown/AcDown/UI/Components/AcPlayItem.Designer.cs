namespace Kaedei.AcDown.UI.Components
{
	partial class AcPlayItem
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
			this.udMin = new System.Windows.Forms.NumericUpDown();
			this.udSec = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.txtFile = new System.Windows.Forms.TextBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.udMilli = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.udMin)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udSec)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udMilli)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(37, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "文件：";
			// 
			// udMin
			// 
			this.udMin.Location = new System.Drawing.Point(57, 79);
			this.udMin.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
			this.udMin.Name = "udMin";
			this.udMin.Size = new System.Drawing.Size(46, 20);
			this.udMin.TabIndex = 1;
			this.udMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// udSec
			// 
			this.udSec.Location = new System.Drawing.Point(134, 79);
			this.udSec.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
			this.udSec.Name = "udSec";
			this.udSec.Size = new System.Drawing.Size(42, 20);
			this.udSec.TabIndex = 3;
			this.udSec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 81);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(37, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "时间：";
			// 
			// txtFile
			// 
			this.txtFile.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.txtFile.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
			this.txtFile.Location = new System.Drawing.Point(12, 43);
			this.txtFile.Name = "txtFile";
			this.txtFile.Size = new System.Drawing.Size(220, 20);
			this.txtFile.TabIndex = 8;
			// 
			// btnBrowse
			// 
			this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnBrowse.Location = new System.Drawing.Point(238, 41);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(75, 23);
			this.btnBrowse.TabIndex = 9;
			this.btnBrowse.Text = "浏览...";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(109, 81);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(19, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "分";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(182, 81);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(19, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "秒";
			// 
			// btnOK
			// 
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(238, 131);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 10;
			this.btnOK.Text = "确定";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// udMilli
			// 
			this.udMilli.Location = new System.Drawing.Point(207, 79);
			this.udMilli.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
			this.udMilli.Name = "udMilli";
			this.udMilli.Size = new System.Drawing.Size(42, 20);
			this.udMilli.TabIndex = 5;
			this.udMilli.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(255, 81);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(31, 13);
			this.label5.TabIndex = 6;
			this.label5.Text = "毫秒";
			// 
			// AcPlayItem
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(325, 166);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.udMilli);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.txtFile);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.udSec);
			this.Controls.Add(this.udMin);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AcPlayItem";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AcPlay视频文件";
			this.Load += new System.EventHandler(this.AcPlayItem_Load);
			((System.ComponentModel.ISupportInitialize)(this.udMin)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udSec)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udMilli)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown udMin;
		private System.Windows.Forms.NumericUpDown udSec;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtFile;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.NumericUpDown udMilli;
		private System.Windows.Forms.Label label5;
	}
}