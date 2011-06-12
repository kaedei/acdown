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
           this.chkImmediate = new System.Windows.Forms.CheckBox();
           this.label2 = new System.Windows.Forms.Label();
           this.lblPath = new System.Windows.Forms.Label();
           this.lblShowConfig = new System.Windows.Forms.LinkLabel();
           this.groupBox1 = new System.Windows.Forms.GroupBox();
           this.groupBox2 = new System.Windows.Forms.GroupBox();
           ((System.ComponentModel.ISupportInitialize)(this.picCheck)).BeginInit();
           this.groupBox1.SuspendLayout();
           this.groupBox2.SuspendLayout();
           this.SuspendLayout();
           // 
           // txtInput
           // 
           this.txtInput.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.txtInput.Location = new System.Drawing.Point(8, 47);
           this.txtInput.Multiline = true;
           this.txtInput.Name = "txtInput";
           this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
           this.txtInput.Size = new System.Drawing.Size(431, 101);
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
           this.label1.Location = new System.Drawing.Point(11, 22);
           this.label1.Name = "label1";
           this.label1.Size = new System.Drawing.Size(106, 21);
           this.label1.TabIndex = 1;
           this.label1.Text = "请输入网址：";
           // 
           // btnAdd
           // 
           this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
           this.btnAdd.Location = new System.Drawing.Point(341, 154);
           this.btnAdd.Name = "btnAdd";
           this.btnAdd.Size = new System.Drawing.Size(98, 37);
           this.btnAdd.TabIndex = 0;
           this.btnAdd.Text = "添加";
           this.toolTip.SetToolTip(this.btnAdd, "单击以确认新建此任务");
           this.btnAdd.UseVisualStyleBackColor = true;
           this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
           // 
           // picCheck
           // 
           this.picCheck.Image = global::Kaedei.AcDown.Properties.Resources._1;
           this.picCheck.Location = new System.Drawing.Point(123, 24);
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
           // chkImmediate
           // 
           this.chkImmediate.AutoSize = true;
           this.chkImmediate.Checked = true;
           this.chkImmediate.CheckState = System.Windows.Forms.CheckState.Checked;
           this.chkImmediate.Enabled = false;
           this.chkImmediate.FlatStyle = System.Windows.Forms.FlatStyle.System;
           this.chkImmediate.Location = new System.Drawing.Point(257, 168);
           this.chkImmediate.Name = "chkImmediate";
           this.chkImmediate.Size = new System.Drawing.Size(78, 17);
           this.chkImmediate.TabIndex = 7;
           this.chkImmediate.Text = "立即开始";
           this.toolTip.SetToolTip(this.chkImmediate, "添加任务后是否立即开始任务\r\n(如果未选中则请手动开始下载任务)");
           this.chkImmediate.UseVisualStyleBackColor = true;
           this.chkImmediate.Visible = false;
           // 
           // label2
           // 
           this.label2.AutoSize = true;
           this.label2.Location = new System.Drawing.Point(16, 28);
           this.label2.Name = "label2";
           this.label2.Size = new System.Drawing.Size(101, 12);
           this.label2.TabIndex = 8;
           this.label2.Text = "文件将被保存到：";
           // 
           // lblPath
           // 
           this.lblPath.AutoSize = true;
           this.lblPath.Location = new System.Drawing.Point(16, 52);
           this.lblPath.Name = "lblPath";
           this.lblPath.Size = new System.Drawing.Size(23, 12);
           this.lblPath.TabIndex = 9;
           this.lblPath.Text = "C:\\";
           // 
           // lblShowConfig
           // 
           this.lblShowConfig.AutoSize = true;
           this.lblShowConfig.Location = new System.Drawing.Point(362, 28);
           this.lblShowConfig.Name = "lblShowConfig";
           this.lblShowConfig.Size = new System.Drawing.Size(77, 12);
           this.lblShowConfig.TabIndex = 10;
           this.lblShowConfig.TabStop = true;
           this.lblShowConfig.Text = "更改保存位置";
           this.lblShowConfig.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblShowConfig_LinkClicked);
           // 
           // groupBox1
           // 
           this.groupBox1.Controls.Add(this.label2);
           this.groupBox1.Controls.Add(this.lblShowConfig);
           this.groupBox1.Controls.Add(this.lblPath);
           this.groupBox1.Location = new System.Drawing.Point(12, 222);
           this.groupBox1.Name = "groupBox1";
           this.groupBox1.Size = new System.Drawing.Size(447, 85);
           this.groupBox1.TabIndex = 11;
           this.groupBox1.TabStop = false;
           this.groupBox1.Text = "设置";
           // 
           // groupBox2
           // 
           this.groupBox2.Controls.Add(this.label1);
           this.groupBox2.Controls.Add(this.txtInput);
           this.groupBox2.Controls.Add(this.btnAdd);
           this.groupBox2.Controls.Add(this.picCheck);
           this.groupBox2.Controls.Add(this.chkImmediate);
           this.groupBox2.Location = new System.Drawing.Point(12, 12);
           this.groupBox2.Name = "groupBox2";
           this.groupBox2.Size = new System.Drawing.Size(447, 204);
           this.groupBox2.TabIndex = 12;
           this.groupBox2.TabStop = false;
           this.groupBox2.Text = "开始";
           // 
           // FormNew
           // 
           this.AcceptButton = this.btnAdd;
           this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
           this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           this.ClientSize = new System.Drawing.Size(471, 324);
           this.Controls.Add(this.groupBox2);
           this.Controls.Add(this.groupBox1);
           this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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

		  }

		  #endregion

		  private System.Windows.Forms.TextBox txtInput;
		  private System.Windows.Forms.Label label1;
		  private System.Windows.Forms.Button btnAdd;
		  private System.Windows.Forms.PictureBox picCheck;
		  private System.Windows.Forms.ToolTip toolTip;
		  private System.Windows.Forms.CheckBox chkImmediate;
		  private System.Windows.Forms.Label label2;
		  private System.Windows.Forms.Label lblPath;
		  private System.Windows.Forms.LinkLabel lblShowConfig;
		  private System.Windows.Forms.GroupBox groupBox1;
		  private System.Windows.Forms.GroupBox groupBox2;
	 }
}