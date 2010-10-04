namespace Kaedei.AcFunDowner
{
	partial class FormXml
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
			this.btnClose = new System.Windows.Forms.Button();
			this.lnkCopy = new System.Windows.Forms.LinkLabel();
			this.txtXml = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.Location = new System.Drawing.Point(288, 363);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(83, 36);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "关闭";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// lnkCopy
			// 
			this.lnkCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lnkCopy.AutoSize = true;
			this.lnkCopy.Location = new System.Drawing.Point(10, 375);
			this.lnkCopy.Name = "lnkCopy";
			this.lnkCopy.Size = new System.Drawing.Size(119, 12);
			this.lnkCopy.TabIndex = 2;
			this.lnkCopy.TabStop = true;
			this.lnkCopy.Text = "复制XML文档到剪贴板";
			this.lnkCopy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCopy_LinkClicked);
			// 
			// txtXml
			// 
			this.txtXml.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.txtXml.Location = new System.Drawing.Point(12, 12);
			this.txtXml.Multiline = true;
			this.txtXml.Name = "txtXml";
			this.txtXml.ReadOnly = true;
			this.txtXml.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtXml.Size = new System.Drawing.Size(359, 345);
			this.txtXml.TabIndex = 3;
			this.txtXml.Text = "不能显示此任务的XML文档树\r\n\r\n该任务可能:\r\n1.尚未开始\r\n2.下载期间发生错误\r\n3.并非引用新浪视频\r\n\r\n发生错误的原因可能有：\r\n1.新浪播客系统" +
				 "维护升级\r\n2.视频使用bug封装\r\n建议您重试此下载任务";
			// 
			// FormXml
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(383, 411);
			this.Controls.Add(this.txtXml);
			this.Controls.Add(this.lnkCopy);
			this.Controls.Add(this.btnClose);
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormXml";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "视频详细信息";
			this.Load += new System.EventHandler(this.FormXml_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.LinkLabel lnkCopy;
		private System.Windows.Forms.TextBox txtXml;
	}
}