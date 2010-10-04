namespace Kaedei.AcFunDowner
{
	partial class FormSubConverter
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSubConverter));
			this.label1 = new System.Windows.Forms.Label();
			this.lnkDownload = new System.Windows.Forms.LinkLabel();
			this.btnClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(165, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(339, 221);
			this.label1.TabIndex = 0;
			this.label1.Text = resources.GetString("label1.Text");
			// 
			// lnkDownload
			// 
			this.lnkDownload.AutoSize = true;
			this.lnkDownload.Location = new System.Drawing.Point(12, 239);
			this.lnkDownload.Name = "lnkDownload";
			this.lnkDownload.Size = new System.Drawing.Size(221, 36);
			this.lnkDownload.TabIndex = 3;
			this.lnkDownload.TabStop = true;
			this.lnkDownload.Text = "作者：纯情286\r\n软件下载&&更新日志\r\nhttp://hi.baidu.com/chunqing286/blog";
			this.lnkDownload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDownload_LinkClicked);
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.Location = new System.Drawing.Point(420, 252);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(84, 23);
			this.btnClose.TabIndex = 4;
			this.btnClose.Text = "关闭窗口";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// FormSubConverter
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(516, 287);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.lnkDownload);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSubConverter";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AcFun视频弹幕下载转换器";
			this.Load += new System.EventHandler(this.FormSubConverter_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel lnkDownload;
		private System.Windows.Forms.Button btnClose;
	}
}