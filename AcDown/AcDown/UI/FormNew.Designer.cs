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
			  this.btnSelectDir = new System.Windows.Forms.Button();
			  this.btnSetProxy = new System.Windows.Forms.Button();
			  this.chkParseRelated = new System.Windows.Forms.CheckBox();
			  this.chkAutoAnswer = new System.Windows.Forms.CheckBox();
			  this.chkExtractCache = new System.Windows.Forms.CheckBox();
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
			  this.groupBox6 = new System.Windows.Forms.GroupBox();
			  this.groupBox5 = new System.Windows.Forms.GroupBox();
			  this.groupBox4 = new System.Windows.Forms.GroupBox();
			  this.groupBox3 = new System.Windows.Forms.GroupBox();
			  this.cboDownSub = new System.Windows.Forms.ComboBox();
			  this.tabPage1 = new System.Windows.Forms.TabPage();
			  this.txtComment = new System.Windows.Forms.TextBox();
			  ((System.ComponentModel.ISupportInitialize)(this.picCheck)).BeginInit();
			  this.groupBox1.SuspendLayout();
			  this.groupBox2.SuspendLayout();
			  this.panelSelectPlugin.SuspendLayout();
			  this.tabNew.SuspendLayout();
			  this.tabConfig.SuspendLayout();
			  this.tabSub.SuspendLayout();
			  this.groupBox6.SuspendLayout();
			  this.groupBox5.SuspendLayout();
			  this.groupBox4.SuspendLayout();
			  this.groupBox3.SuspendLayout();
			  this.tabPage1.SuspendLayout();
			  this.SuspendLayout();
			  // 
			  // txtInput
			  // 
			  this.txtInput.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
			  this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
			  this.btnAdd.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
			  // btnSelectDir
			  // 
			  this.btnSelectDir.FlatStyle = System.Windows.Forms.FlatStyle.System;
			  this.btnSelectDir.Location = new System.Drawing.Point(350, 46);
			  this.btnSelectDir.Name = "btnSelectDir";
			  this.btnSelectDir.Size = new System.Drawing.Size(77, 28);
			  this.btnSelectDir.TabIndex = 13;
			  this.btnSelectDir.Text = "选择...";
			  this.toolTip.SetToolTip(this.btnSelectDir, "选择文件将要被下载到的位置");
			  this.btnSelectDir.UseVisualStyleBackColor = true;
			  this.btnSelectDir.Click += new System.EventHandler(this.btnSelectDir_Click);
			  // 
			  // btnSetProxy
			  // 
			  this.btnSetProxy.FlatStyle = System.Windows.Forms.FlatStyle.System;
			  this.btnSetProxy.Location = new System.Drawing.Point(350, 116);
			  this.btnSetProxy.Name = "btnSetProxy";
			  this.btnSetProxy.Size = new System.Drawing.Size(77, 28);
			  this.btnSetProxy.TabIndex = 12;
			  this.btnSetProxy.Text = "编辑...";
			  this.toolTip.SetToolTip(this.btnSetProxy, "点击编辑代理服务器设置");
			  this.btnSetProxy.UseVisualStyleBackColor = true;
			  this.btnSetProxy.Click += new System.EventHandler(this.btnSetProxy_Click);
			  // 
			  // chkParseRelated
			  // 
			  this.chkParseRelated.Location = new System.Drawing.Point(21, 29);
			  this.chkParseRelated.Name = "chkParseRelated";
			  this.chkParseRelated.Size = new System.Drawing.Size(168, 24);
			  this.chkParseRelated.TabIndex = 3;
			  this.chkParseRelated.Text = "解析所有关联的下载项";
			  this.toolTip.SetToolTip(this.chkParseRelated, "添加任务时，解析与此任务相关联的其他任务");
			  this.chkParseRelated.UseVisualStyleBackColor = true;
			  // 
			  // chkAutoAnswer
			  // 
			  this.chkAutoAnswer.Location = new System.Drawing.Point(26, 25);
			  this.chkAutoAnswer.Name = "chkAutoAnswer";
			  this.chkAutoAnswer.Size = new System.Drawing.Size(153, 24);
			  this.chkAutoAnswer.TabIndex = 3;
			  this.chkAutoAnswer.Text = "启用自动应答";
			  this.toolTip.SetToolTip(this.chkAutoAnswer, "启用自动应答后，下载时不再出现询问对话框。\r\n由此任务添加的其他任务也会继承此设置。");
			  this.chkAutoAnswer.UseVisualStyleBackColor = true;
			  // 
			  // chkExtractCache
			  // 
			  this.chkExtractCache.Location = new System.Drawing.Point(21, 25);
			  this.chkExtractCache.Name = "chkExtractCache";
			  this.chkExtractCache.Size = new System.Drawing.Size(168, 24);
			  this.chkExtractCache.TabIndex = 3;
			  this.chkExtractCache.Text = "提取浏览器缓存";
			  this.toolTip.SetToolTip(this.chkExtractCache, "允许自动提取已下载的文件，省却重新下载的过程");
			  this.chkExtractCache.UseVisualStyleBackColor = true;
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
			  this.groupBox1.Controls.Add(this.btnSelectDir);
			  this.groupBox1.Controls.Add(this.btnSetProxy);
			  this.groupBox1.Controls.Add(this.cboProxy);
			  this.groupBox1.Controls.Add(this.txtPath);
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
			  this.cboProxy.Size = new System.Drawing.Size(332, 28);
			  this.cboProxy.TabIndex = 3;
			  // 
			  // txtPath
			  // 
			  this.txtPath.Location = new System.Drawing.Point(12, 47);
			  this.txtPath.Name = "txtPath";
			  this.txtPath.ReadOnly = true;
			  this.txtPath.Size = new System.Drawing.Size(332, 26);
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
			  this.tabSub.Controls.Add(this.groupBox6);
			  this.tabSub.Controls.Add(this.groupBox5);
			  this.tabSub.Controls.Add(this.groupBox4);
			  this.tabSub.Controls.Add(this.groupBox3);
			  this.tabSub.Location = new System.Drawing.Point(4, 29);
			  this.tabSub.Name = "tabSub";
			  this.tabSub.Padding = new System.Windows.Forms.Padding(3);
			  this.tabSub.Size = new System.Drawing.Size(448, 174);
			  this.tabSub.TabIndex = 3;
			  this.tabSub.Text = "高级";
			  this.tabSub.UseVisualStyleBackColor = true;
			  // 
			  // groupBox6
			  // 
			  this.groupBox6.Controls.Add(this.chkExtractCache);
			  this.groupBox6.Location = new System.Drawing.Point(225, 84);
			  this.groupBox6.Name = "groupBox6";
			  this.groupBox6.Size = new System.Drawing.Size(210, 65);
			  this.groupBox6.TabIndex = 7;
			  this.groupBox6.TabStop = false;
			  this.groupBox6.Text = "提取缓存";
			  // 
			  // groupBox5
			  // 
			  this.groupBox5.Controls.Add(this.chkAutoAnswer);
			  this.groupBox5.Location = new System.Drawing.Point(15, 84);
			  this.groupBox5.Name = "groupBox5";
			  this.groupBox5.Size = new System.Drawing.Size(204, 65);
			  this.groupBox5.TabIndex = 6;
			  this.groupBox5.TabStop = false;
			  this.groupBox5.Text = "自动应答";
			  // 
			  // groupBox4
			  // 
			  this.groupBox4.Controls.Add(this.chkParseRelated);
			  this.groupBox4.Location = new System.Drawing.Point(225, 11);
			  this.groupBox4.Name = "groupBox4";
			  this.groupBox4.Size = new System.Drawing.Size(210, 67);
			  this.groupBox4.TabIndex = 5;
			  this.groupBox4.TabStop = false;
			  this.groupBox4.Text = "关联任务";
			  // 
			  // groupBox3
			  // 
			  this.groupBox3.Controls.Add(this.cboDownSub);
			  this.groupBox3.Location = new System.Drawing.Point(15, 11);
			  this.groupBox3.Name = "groupBox3";
			  this.groupBox3.Size = new System.Drawing.Size(204, 67);
			  this.groupBox3.TabIndex = 4;
			  this.groupBox3.TabStop = false;
			  this.groupBox3.Text = "弹幕&&字幕";
			  // 
			  // cboDownSub
			  // 
			  this.cboDownSub.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			  this.cboDownSub.FormattingEnabled = true;
			  this.cboDownSub.Items.AddRange(new object[] {
            "下载视频和弹幕",
            "只下载视频",
            "只下载弹幕"});
			  this.cboDownSub.Location = new System.Drawing.Point(26, 25);
			  this.cboDownSub.Name = "cboDownSub";
			  this.cboDownSub.Size = new System.Drawing.Size(153, 28);
			  this.cboDownSub.TabIndex = 6;
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
			  this.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
			  this.groupBox6.ResumeLayout(false);
			  this.groupBox5.ResumeLayout(false);
			  this.groupBox4.ResumeLayout(false);
			  this.groupBox3.ResumeLayout(false);
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
		  private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.ComboBox cboProxy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabNew;
        private System.Windows.Forms.TabPage tabConfig;
		  private System.Windows.Forms.TabPage tabSub;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Panel panelSelectPlugin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboPlugins;
        private System.Windows.Forms.Button btnSelectDir;
        private System.Windows.Forms.Button btnSetProxy;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkParseRelated;
        private System.Windows.Forms.GroupBox groupBox3;
		  private System.Windows.Forms.GroupBox groupBox5;
		  private System.Windows.Forms.CheckBox chkAutoAnswer;
		  private System.Windows.Forms.ComboBox cboDownSub;
		  private System.Windows.Forms.GroupBox groupBox6;
		  private System.Windows.Forms.CheckBox chkExtractCache;
	 }
}