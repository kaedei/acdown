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
         this.lblShowConfig = new System.Windows.Forms.LinkLabel();
         this.lnkSetProxy = new System.Windows.Forms.LinkLabel();
         this.rdoDownSubOnly = new System.Windows.Forms.RadioButton();
         this.label2 = new System.Windows.Forms.Label();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.cboProxy = new System.Windows.Forms.ComboBox();
         this.txtPath = new System.Windows.Forms.TextBox();
         this.label3 = new System.Windows.Forms.Label();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.panelSelectPlugin = new System.Windows.Forms.Panel();
         this.label4 = new System.Windows.Forms.Label();
         this.cboPlugins = new System.Windows.Forms.ComboBox();
         this.tabNew = new System.Windows.Forms.TabControl();
         this.tabConfig = new System.Windows.Forms.TabPage();
         this.tabSub = new System.Windows.Forms.TabPage();
         this.rdoDownSub = new System.Windows.Forms.RadioButton();
         this.rdoNotDownSub = new System.Windows.Forms.RadioButton();
         this.tabPage1 = new System.Windows.Forms.TabPage();
         this.txtComment = new System.Windows.Forms.TextBox();
         ((System.ComponentModel.ISupportInitialize)(this.picCheck)).BeginInit();
         this.groupBox1.SuspendLayout();
         this.groupBox2.SuspendLayout();
         this.panelSelectPlugin.SuspendLayout();
         this.tabNew.SuspendLayout();
         this.tabConfig.SuspendLayout();
         this.tabSub.SuspendLayout();
         this.tabPage1.SuspendLayout();
         this.SuspendLayout();
         // 
         // txtInput
         // 
         this.txtInput.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.txtInput.Location = new System.Drawing.Point(19, 55);
         this.txtInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.txtInput.Name = "txtInput";
         this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.txtInput.Size = new System.Drawing.Size(420, 33);
         this.txtInput.TabIndex = 1;
         this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
         this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.label1.Location = new System.Drawing.Point(15, 24);
         this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(106, 21);
         this.label1.TabIndex = 2;
         this.label1.Text = "请输入网址：";
         // 
         // btnAdd
         // 
         this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.btnAdd.Location = new System.Drawing.Point(315, 106);
         this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.btnAdd.Name = "btnAdd";
         this.btnAdd.Size = new System.Drawing.Size(124, 40);
         this.btnAdd.TabIndex = 0;
         this.btnAdd.Text = "粘贴并添加";
         this.toolTip.SetToolTip(this.btnAdd, "单击确认新建任务");
         this.btnAdd.UseVisualStyleBackColor = true;
         this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
         // 
         // picCheck
         // 
         this.picCheck.Image = global::Kaedei.AcDown.Properties.Resources._1;
         this.picCheck.Location = new System.Drawing.Point(129, 26);
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
         this.toolTip.ReshowDelay = 100;
         this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
         this.toolTip.ToolTipTitle = "提示:";
         // 
         // lblShowConfig
         // 
         this.lblShowConfig.AutoSize = true;
         this.lblShowConfig.Location = new System.Drawing.Point(334, 24);
         this.lblShowConfig.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.lblShowConfig.Name = "lblShowConfig";
         this.lblShowConfig.Size = new System.Drawing.Size(93, 20);
         this.lblShowConfig.TabIndex = 0;
         this.lblShowConfig.TabStop = true;
         this.lblShowConfig.Text = "更改保存位置";
         this.toolTip.SetToolTip(this.lblShowConfig, "更改默认保存下载文件的位置");
         this.lblShowConfig.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblShowConfig_LinkClicked);
         // 
         // lnkSetProxy
         // 
         this.lnkSetProxy.AutoSize = true;
         this.lnkSetProxy.Location = new System.Drawing.Point(325, 93);
         this.lnkSetProxy.Name = "lnkSetProxy";
         this.lnkSetProxy.Size = new System.Drawing.Size(107, 20);
         this.lnkSetProxy.TabIndex = 2;
         this.lnkSetProxy.TabStop = true;
         this.lnkSetProxy.Text = "设置代理服务器";
         this.toolTip.SetToolTip(this.lnkSetProxy, "编辑代理服务器列表 ");
         this.lnkSetProxy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSetProxy_LinkClicked);
         // 
         // rdoDownSubOnly
         // 
         this.rdoDownSubOnly.AutoSize = true;
         this.rdoDownSubOnly.Location = new System.Drawing.Point(22, 78);
         this.rdoDownSubOnly.Name = "rdoDownSubOnly";
         this.rdoDownSubOnly.Size = new System.Drawing.Size(131, 24);
         this.rdoDownSubOnly.TabIndex = 2;
         this.rdoDownSubOnly.TabStop = true;
         this.rdoDownSubOnly.Text = "只下载弹幕/字幕";
         this.toolTip.SetToolTip(this.rdoDownSubOnly, "针对弹幕网站的下载任务，下载时仅下载相应的弹幕/字幕文件，而不下载视频本身");
         this.rdoDownSubOnly.UseVisualStyleBackColor = true;
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(8, 24);
         this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(54, 20);
         this.label2.TabIndex = 8;
         this.label2.Text = "存储到:";
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.lnkSetProxy);
         this.groupBox1.Controls.Add(this.cboProxy);
         this.groupBox1.Controls.Add(this.txtPath);
         this.groupBox1.Controls.Add(this.lblShowConfig);
         this.groupBox1.Controls.Add(this.label3);
         this.groupBox1.Controls.Add(this.label2);
         this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.groupBox1.Location = new System.Drawing.Point(3, 3);
         this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.groupBox1.Size = new System.Drawing.Size(442, 168);
         this.groupBox1.TabIndex = 0;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "设置";
         // 
         // cboProxy
         // 
         this.cboProxy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cboProxy.FormattingEnabled = true;
         this.cboProxy.Location = new System.Drawing.Point(12, 116);
         this.cboProxy.Name = "cboProxy";
         this.cboProxy.Size = new System.Drawing.Size(415, 28);
         this.cboProxy.TabIndex = 3;
         // 
         // txtPath
         // 
         this.txtPath.Location = new System.Drawing.Point(12, 47);
         this.txtPath.Name = "txtPath";
         this.txtPath.ReadOnly = true;
         this.txtPath.Size = new System.Drawing.Size(415, 26);
         this.txtPath.TabIndex = 1;
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(8, 93);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(138, 20);
         this.label3.TabIndex = 11;
         this.label3.Text = "使用以下代理服务器:";
         // 
         // groupBox2
         // 
         this.groupBox2.Controls.Add(this.panelSelectPlugin);
         this.groupBox2.Controls.Add(this.txtInput);
         this.groupBox2.Controls.Add(this.picCheck);
         this.groupBox2.Controls.Add(this.btnAdd);
         this.groupBox2.Controls.Add(this.label1);
         this.groupBox2.Location = new System.Drawing.Point(12, 14);
         this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.groupBox2.Size = new System.Drawing.Size(456, 156);
         this.groupBox2.TabIndex = 0;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "下载";
         // 
         // panelSelectPlugin
         // 
         this.panelSelectPlugin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.panelSelectPlugin.Controls.Add(this.label4);
         this.panelSelectPlugin.Controls.Add(this.cboPlugins);
         this.panelSelectPlugin.Location = new System.Drawing.Point(19, 109);
         this.panelSelectPlugin.Name = "panelSelectPlugin";
         this.panelSelectPlugin.Size = new System.Drawing.Size(289, 37);
         this.panelSelectPlugin.TabIndex = 7;
         this.panelSelectPlugin.Visible = false;
         // 
         // label4
         // 
         this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.label4.Location = new System.Drawing.Point(3, 6);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(80, 25);
         this.label4.TabIndex = 1;
         this.label4.Text = "下载插件:";
         // 
         // cboPlugins
         // 
         this.cboPlugins.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.cboPlugins.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cboPlugins.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.cboPlugins.FormattingEnabled = true;
         this.cboPlugins.Location = new System.Drawing.Point(89, 3);
         this.cboPlugins.Name = "cboPlugins";
         this.cboPlugins.Size = new System.Drawing.Size(184, 28);
         this.cboPlugins.TabIndex = 0;
         // 
         // tabNew
         // 
         this.tabNew.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.tabNew.Controls.Add(this.tabConfig);
         this.tabNew.Controls.Add(this.tabSub);
         this.tabNew.Controls.Add(this.tabPage1);
         this.tabNew.Location = new System.Drawing.Point(12, 179);
         this.tabNew.Name = "tabNew";
         this.tabNew.SelectedIndex = 0;
         this.tabNew.Size = new System.Drawing.Size(456, 207);
         this.tabNew.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
         this.tabNew.TabIndex = 1;
         // 
         // tabConfig
         // 
         this.tabConfig.Controls.Add(this.groupBox1);
         this.tabConfig.Location = new System.Drawing.Point(4, 29);
         this.tabConfig.Name = "tabConfig";
         this.tabConfig.Padding = new System.Windows.Forms.Padding(3);
         this.tabConfig.Size = new System.Drawing.Size(448, 174);
         this.tabConfig.TabIndex = 1;
         this.tabConfig.Text = "下载设置";
         this.tabConfig.UseVisualStyleBackColor = true;
         // 
         // tabSub
         // 
         this.tabSub.Controls.Add(this.rdoDownSubOnly);
         this.tabSub.Controls.Add(this.rdoDownSub);
         this.tabSub.Controls.Add(this.rdoNotDownSub);
         this.tabSub.Location = new System.Drawing.Point(4, 29);
         this.tabSub.Name = "tabSub";
         this.tabSub.Padding = new System.Windows.Forms.Padding(3);
         this.tabSub.Size = new System.Drawing.Size(448, 174);
         this.tabSub.TabIndex = 3;
         this.tabSub.Text = "弹幕/字幕";
         this.tabSub.UseVisualStyleBackColor = true;
         // 
         // rdoDownSub
         // 
         this.rdoDownSub.AutoSize = true;
         this.rdoDownSub.Location = new System.Drawing.Point(22, 18);
         this.rdoDownSub.Name = "rdoDownSub";
         this.rdoDownSub.Size = new System.Drawing.Size(117, 24);
         this.rdoDownSub.TabIndex = 0;
         this.rdoDownSub.Text = "下载弹幕/字幕";
         this.rdoDownSub.UseVisualStyleBackColor = true;
         // 
         // rdoNotDownSub
         // 
         this.rdoNotDownSub.AutoSize = true;
         this.rdoNotDownSub.Location = new System.Drawing.Point(22, 48);
         this.rdoNotDownSub.Name = "rdoNotDownSub";
         this.rdoNotDownSub.Size = new System.Drawing.Size(131, 24);
         this.rdoNotDownSub.TabIndex = 1;
         this.rdoNotDownSub.Text = "不下载弹幕/字幕";
         this.rdoNotDownSub.UseVisualStyleBackColor = true;
         // 
         // tabPage1
         // 
         this.tabPage1.Controls.Add(this.txtComment);
         this.tabPage1.Location = new System.Drawing.Point(4, 29);
         this.tabPage1.Name = "tabPage1";
         this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
         this.tabPage1.Size = new System.Drawing.Size(448, 174);
         this.tabPage1.TabIndex = 4;
         this.tabPage1.Text = "注释";
         this.tabPage1.UseVisualStyleBackColor = true;
         // 
         // txtComment
         // 
         this.txtComment.Dock = System.Windows.Forms.DockStyle.Fill;
         this.txtComment.Location = new System.Drawing.Point(3, 3);
         this.txtComment.Multiline = true;
         this.txtComment.Name = "txtComment";
         this.txtComment.Size = new System.Drawing.Size(442, 168);
         this.txtComment.TabIndex = 0;
         // 
         // FormNew
         // 
         this.AcceptButton = this.btnAdd;
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(481, 396);
         this.Controls.Add(this.groupBox2);
         this.Controls.Add(this.tabNew);
         this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "FormNew";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "新建下载任务";
         this.TopMost = true;
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormNew_FormClosing);
         this.Load += new System.EventHandler(this.FormNew_Load);
         ((System.ComponentModel.ISupportInitialize)(this.picCheck)).EndInit();
         this.groupBox1.ResumeLayout(false);
         this.groupBox1.PerformLayout();
         this.groupBox2.ResumeLayout(false);
         this.groupBox2.PerformLayout();
         this.panelSelectPlugin.ResumeLayout(false);
         this.tabNew.ResumeLayout(false);
         this.tabConfig.ResumeLayout(false);
         this.tabSub.ResumeLayout(false);
         this.tabSub.PerformLayout();
         this.tabPage1.ResumeLayout(false);
         this.tabPage1.PerformLayout();
         this.ResumeLayout(false);

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
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.ComboBox cboProxy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel lnkSetProxy;
        private System.Windows.Forms.TabControl tabNew;
        private System.Windows.Forms.TabPage tabConfig;
        private System.Windows.Forms.TabPage tabSub;
        private System.Windows.Forms.RadioButton rdoDownSubOnly;
        private System.Windows.Forms.RadioButton rdoDownSub;
        private System.Windows.Forms.RadioButton rdoNotDownSub;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Panel panelSelectPlugin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboPlugins;
	 }
}