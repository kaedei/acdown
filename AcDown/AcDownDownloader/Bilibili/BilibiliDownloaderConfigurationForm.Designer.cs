namespace Kaedei.AcDown.Downloader.Bilibili
{
	partial class BilibiliDownloaderConfigurationForm
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
			this.txtFormat = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtPreview = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cboVar = new System.Windows.Forms.ComboBox();
			this.btnAdd = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.chkGenerateAcPlay = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtFormat
			// 
			this.txtFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtFormat.Location = new System.Drawing.Point(9, 48);
			this.txtFormat.Name = "txtFormat";
			this.txtFormat.Size = new System.Drawing.Size(421, 24);
			this.txtFormat.TabIndex = 0;
			this.txtFormat.Text = "标题\\子标题(分段).扩展名";
			this.txtFormat.TextChanged += new System.EventHandler(this.txtFormat_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(148, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "自定义视频&&弹幕文件名称:";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.txtPreview);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.cboVar);
			this.groupBox1.Controls.Add(this.btnAdd);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtFormat);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(436, 182);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "自定义文件名称";
			// 
			// txtPreview
			// 
			this.txtPreview.Location = new System.Drawing.Point(46, 143);
			this.txtPreview.Name = "txtPreview";
			this.txtPreview.ReadOnly = true;
			this.txtPreview.Size = new System.Drawing.Size(384, 20);
			this.txtPreview.TabIndex = 9;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 146);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(34, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "预览:";
			// 
			// cboVar
			// 
			this.cboVar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboVar.FormattingEnabled = true;
			this.cboVar.Items.AddRange(new object[] {
            "标题",
            "子标题",
            "(分段)",
            "编号",
            "子编号",
            "扩展名"});
			this.cboVar.Location = new System.Drawing.Point(9, 106);
			this.cboVar.Name = "cboVar";
			this.cboVar.Size = new System.Drawing.Size(155, 21);
			this.cboVar.TabIndex = 7;
			// 
			// btnAdd
			// 
			this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAdd.Location = new System.Drawing.Point(170, 101);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(75, 28);
			this.btnAdd.TabIndex = 6;
			this.btnAdd.Text = "添加";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 90);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(82, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "可选用的变量:";
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.Location = new System.Drawing.Point(373, 307);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 29);
			this.btnSave.TabIndex = 3;
			this.btnSave.Text = "保存";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// chkGenerateAcPlay
			// 
			this.chkGenerateAcPlay.AutoSize = true;
			this.chkGenerateAcPlay.Location = new System.Drawing.Point(19, 33);
			this.chkGenerateAcPlay.Name = "chkGenerateAcPlay";
			this.chkGenerateAcPlay.Size = new System.Drawing.Size(155, 17);
			this.chkGenerateAcPlay.TabIndex = 8;
			this.chkGenerateAcPlay.Text = "生成AcPlay播放快捷方式";
			this.chkGenerateAcPlay.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.chkGenerateAcPlay);
			this.groupBox2.Location = new System.Drawing.Point(254, 200);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(194, 93);
			this.groupBox2.TabIndex = 9;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "弹幕播放";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.txtPassword);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.txtUsername);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Location = new System.Drawing.Point(12, 200);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(236, 93);
			this.groupBox3.TabIndex = 10;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "登录到BiliBili";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(16, 34);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(46, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "用户名:";
			// 
			// txtUsername
			// 
			this.txtUsername.Location = new System.Drawing.Point(70, 31);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(131, 20);
			this.txtUsername.TabIndex = 9;
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(70, 57);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '●';
			this.txtPassword.Size = new System.Drawing.Size(131, 20);
			this.txtPassword.TabIndex = 10;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(28, 60);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(34, 13);
			this.label5.TabIndex = 11;
			this.label5.Text = "密码:";
			// 
			// BilibiliDownloaderConfigurationForm
			// 
			this.AcceptButton = this.btnSave;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(460, 348);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BilibiliDownloaderConfigurationForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Bilibili插件属性";
			this.Load += new System.EventHandler(this.AcfunDownloaderConfigurationForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox txtFormat;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.CheckBox chkGenerateAcPlay;
		private System.Windows.Forms.ComboBox cboVar;
		private System.Windows.Forms.TextBox txtPreview;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.Label label4;
	}
}