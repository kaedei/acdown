namespace Kaedei.AcDown.UI
{
	 partial class FormNew
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
           this.txtInput = new System.Windows.Forms.TextBox();
           this.label1 = new System.Windows.Forms.Label();
           this.btnAdd = new System.Windows.Forms.Button();
           this.picCheck = new System.Windows.Forms.PictureBox();
           this.toolTip = new System.Windows.Forms.ToolTip(this.components);
           this.lnkPaste = new System.Windows.Forms.LinkLabel();
           this.label2 = new System.Windows.Forms.Label();
           this.lblShowConfig = new System.Windows.Forms.LinkLabel();
           this.groupBox1 = new System.Windows.Forms.GroupBox();
           this.lnkSetProxy = new System.Windows.Forms.LinkLabel();
           this.txtPath = new System.Windows.Forms.TextBox();
           this.cboProxy = new System.Windows.Forms.ComboBox();
           this.label3 = new System.Windows.Forms.Label();
           this.groupBox2 = new System.Windows.Forms.GroupBox();
           this.btnExample = new System.Windows.Forms.Button();
           this.txtExample = new System.Windows.Forms.TextBox();
           this.lblFlvcdTip = new System.Windows.Forms.Label();
           ((System.ComponentModel.ISupportInitialize)(this.picCheck)).BeginInit();
           this.groupBox1.SuspendLayout();
           this.groupBox2.SuspendLayout();
           this.SuspendLayout();
           // 
           // txtInput
           // 
           this.txtInput.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
           this.txtInput.Location = new System.Drawing.Point(15, 55);
           this.txtInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
           this.txtInput.Multiline = true;
           this.txtInput.Name = "txtInput";
           this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
           this.txtInput.Size = new System.Drawing.Size(434, 91);
           this.txtInput.TabIndex = 0;
           this.txtInput.Text = "http://";
           this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
           this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
           // 
           // label1
           // 
           this.label1.AutoSize = true;
           this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
           this.label1.Location = new System.Drawing.Point(15, 24);
           this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
           this.label1.Name = "label1";
           this.label1.Size = new System.Drawing.Size(106, 21);
           this.label1.TabIndex = 1;
           this.label1.Text = "请输入网址：";
           // 
           // btnAdd
           // 
           this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
           this.btnAdd.Location = new System.Drawing.Point(325, 151);
           this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
           this.btnAdd.Name = "btnAdd";
           this.btnAdd.Size = new System.Drawing.Size(124, 37);
           this.btnAdd.TabIndex = 0;
           this.btnAdd.Text = "添加";
           this.toolTip.SetToolTip(this.btnAdd, "单击以确认新建此任务");
           this.btnAdd.UseVisualStyleBackColor = true;
           this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
           // 
           // picCheck
           // 
           this.picCheck.Image = global::Kaedei.AcDown.Properties.Resources._1;
           this.picCheck.Location = new System.Drawing.Point(129, 24);
           this.picCheck.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
           this.picCheck.Name = "picCheck";
           this.picCheck.Size = new System.Drawing.Size(19, 19);
           this.picCheck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
           this.picCheck.TabIndex = 6;
           this.picCheck.TabStop = false;
           this.picCheck.Visible = false;
           // 
           // toolTip
           // 
           this.toolTip.AutoPopDelay = 8000;
           this.toolTip.InitialDelay = 500;
           this.toolTip.IsBalloon = true;
           this.toolTip.ReshowDelay = 100;
           this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
           this.toolTip.ToolTipTitle = "提示:";
           // 
           // lnkPaste
           // 
           this.lnkPaste.AutoSize = true;
           this.lnkPaste.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
           this.lnkPaste.Location = new System.Drawing.Point(275, 159);
           this.lnkPaste.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
           this.lnkPaste.Name = "lnkPaste";
           this.lnkPaste.Size = new System.Drawing.Size(42, 21);
           this.lnkPaste.TabIndex = 9;
           this.lnkPaste.TabStop = true;
           this.lnkPaste.Text = "粘贴";
           this.toolTip.SetToolTip(this.lnkPaste, "如果系统剪贴板中有文字,\r\n则将剪贴板中文字粘贴入文本框");
           this.lnkPaste.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkPaste_LinkClicked);
           // 
           // label2
           // 
           this.label2.AutoSize = true;
           this.label2.Location = new System.Drawing.Point(10, 24);
           this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
           this.label2.Name = "label2";
           this.label2.Size = new System.Drawing.Size(121, 20);
           this.label2.TabIndex = 8;
           this.label2.Text = "文件将被保存到：";
           // 
           // lblShowConfig
           // 
           this.lblShowConfig.AutoSize = true;
           this.lblShowConfig.Location = new System.Drawing.Point(355, 24);
           this.lblShowConfig.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
           this.lblShowConfig.Name = "lblShowConfig";
           this.lblShowConfig.Size = new System.Drawing.Size(93, 20);
           this.lblShowConfig.TabIndex = 10;
           this.lblShowConfig.TabStop = true;
           this.lblShowConfig.Text = "更改保存位置";
           this.toolTip.SetToolTip(this.lblShowConfig, "更改默认保存下载文件的位置");
           this.lblShowConfig.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblShowConfig_LinkClicked);
           // 
           // groupBox1
           // 
           this.groupBox1.Controls.Add(this.lnkSetProxy);
           this.groupBox1.Controls.Add(this.txtPath);
           this.groupBox1.Controls.Add(this.cboProxy);
           this.groupBox1.Controls.Add(this.label3);
           this.groupBox1.Controls.Add(this.label2);
           this.groupBox1.Controls.Add(this.lblShowConfig);
           this.groupBox1.Location = new System.Drawing.Point(13, 219);
           this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
           this.groupBox1.Name = "groupBox1";
           this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
           this.groupBox1.Size = new System.Drawing.Size(457, 174);
           this.groupBox1.TabIndex = 11;
           this.groupBox1.TabStop = false;
           this.groupBox1.Text = "设置";
           // 
           // lnkSetProxy
           // 
           this.lnkSetProxy.AutoSize = true;
           this.lnkSetProxy.Location = new System.Drawing.Point(343, 91);
           this.lnkSetProxy.Name = "lnkSetProxy";
           this.lnkSetProxy.Size = new System.Drawing.Size(107, 20);
           this.lnkSetProxy.TabIndex = 14;
           this.lnkSetProxy.TabStop = true;
           this.lnkSetProxy.Text = "设置代理服务器";
           this.toolTip.SetToolTip(this.lnkSetProxy, "编辑代理服务器列表 ");
           this.lnkSetProxy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSetProxy_LinkClicked);
           // 
           // txtPath
           // 
           this.txtPath.Location = new System.Drawing.Point(14, 50);
           this.txtPath.Name = "txtPath";
           this.txtPath.ReadOnly = true;
           this.txtPath.Size = new System.Drawing.Size(434, 26);
           this.txtPath.TabIndex = 13;
           // 
           // cboProxy
           // 
           this.cboProxy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
           this.cboProxy.FormattingEnabled = true;
           this.cboProxy.Location = new System.Drawing.Point(14, 114);
           this.cboProxy.Name = "cboProxy";
           this.cboProxy.Size = new System.Drawing.Size(434, 28);
           this.cboProxy.TabIndex = 12;
           // 
           // label3
           // 
           this.label3.AutoSize = true;
           this.label3.Location = new System.Drawing.Point(11, 91);
           this.label3.Name = "label3";
           this.label3.Size = new System.Drawing.Size(138, 20);
           this.label3.TabIndex = 11;
           this.label3.Text = "使用以下代理服务器:";
           // 
           // groupBox2
           // 
           this.groupBox2.Controls.Add(this.lblFlvcdTip);
           this.groupBox2.Controls.Add(this.lnkPaste);
           this.groupBox2.Controls.Add(this.btnExample);
           this.groupBox2.Controls.Add(this.label1);
           this.groupBox2.Controls.Add(this.txtInput);
           this.groupBox2.Controls.Add(this.btnAdd);
           this.groupBox2.Controls.Add(this.picCheck);
           this.groupBox2.Location = new System.Drawing.Point(12, 14);
           this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
           this.groupBox2.Name = "groupBox2";
           this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
           this.groupBox2.Size = new System.Drawing.Size(458, 195);
           this.groupBox2.TabIndex = 12;
           this.groupBox2.TabStop = false;
           this.groupBox2.Text = "开始";
           // 
           // btnExample
           // 
           this.btnExample.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
           this.btnExample.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
           this.btnExample.Location = new System.Drawing.Point(244, 22);
           this.btnExample.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
           this.btnExample.Name = "btnExample";
           this.btnExample.Size = new System.Drawing.Size(205, 21);
           this.btnExample.TabIndex = 8;
           this.btnExample.Text = "查看当前支持哪些网站 >>";
           this.toolTip.SetToolTip(this.btnExample, "点击查看当前支持解析的网址示例");
           this.btnExample.UseVisualStyleBackColor = true;
           this.btnExample.Click += new System.EventHandler(this.btnExample_Click);
           // 
           // txtExample
           // 
           this.txtExample.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
           this.txtExample.Location = new System.Drawing.Point(488, 14);
           this.txtExample.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
           this.txtExample.Multiline = true;
           this.txtExample.Name = "txtExample";
           this.txtExample.ScrollBars = System.Windows.Forms.ScrollBars.Both;
           this.txtExample.Size = new System.Drawing.Size(336, 378);
           this.txtExample.TabIndex = 13;
           // 
           // lblFlvcdTip
           // 
           this.lblFlvcdTip.AutoSize = true;
           this.lblFlvcdTip.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
           this.lblFlvcdTip.Location = new System.Drawing.Point(15, 160);
           this.lblFlvcdTip.Name = "lblFlvcdTip";
           this.lblFlvcdTip.Size = new System.Drawing.Size(231, 22);
           this.lblFlvcdTip.TabIndex = 10;
           this.lblFlvcdTip.Text = "不支持解析?Url前面加个\"+\"再试试!";
           this.toolTip.SetToolTip(this.lblFlvcdTip, "如果遇到不能解析的视频，请尝试在网址前面添加一个加号，例如：\r\n+http://v.youku.com/v_show/id_XMTc2MTUwNDY4.html\r" +
                   "\n+http://v.163.com/movie/2010/6/7/S/M6GQE36A8_M6HSGF67S.html\r\n程序会调用FLVCD在线解析引擎解析" +
                   "该视频");
           // 
           // FormNew
           // 
           this.AcceptButton = this.btnAdd;
           this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
           this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           this.ClientSize = new System.Drawing.Size(481, 406);
           this.Controls.Add(this.txtExample);
           this.Controls.Add(this.groupBox2);
           this.Controls.Add(this.groupBox1);
           this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
           this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
           this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
           this.MaximizeBox = false;
           this.MinimizeBox = false;
           this.Name = "FormNew";
           this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
           this.Text = "新建下载任务";
           this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormNew_FormClosing);
           this.Load += new System.EventHandler(this.FormNew_Load);
           ((System.ComponentModel.ISupportInitialize)(this.picCheck)).EndInit();
           this.groupBox1.ResumeLayout(false);
           this.groupBox1.PerformLayout();
           this.groupBox2.ResumeLayout(false);
           this.groupBox2.PerformLayout();
           this.ResumeLayout(false);
           this.PerformLayout();

		  }

		  #endregion

		  private System.Windows.Forms.TextBox txtInput;
		  private System.Windows.Forms.Label label1;
		  private System.Windows.Forms.Button btnAdd;
		  private System.Windows.Forms.PictureBox picCheck;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label label2;
		  private System.Windows.Forms.LinkLabel lblShowConfig;
		  private System.Windows.Forms.GroupBox groupBox1;
		  private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnExample;
        private System.Windows.Forms.TextBox txtExample;
        private System.Windows.Forms.LinkLabel lnkPaste;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.ComboBox cboProxy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel lnkSetProxy;
        private System.Windows.Forms.Label lblFlvcdTip;
	 }
}