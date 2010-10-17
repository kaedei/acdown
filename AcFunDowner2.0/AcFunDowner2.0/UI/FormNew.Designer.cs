namespace Kaedei.AcDown
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
			  this.pictureBox1 = new System.Windows.Forms.PictureBox();
			  this.chkDownAllSection = new System.Windows.Forms.CheckBox();
			  this.chkDownSub = new System.Windows.Forms.CheckBox();
			  this.picCheck = new System.Windows.Forms.PictureBox();
			  this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			  this.chkImmediate = new System.Windows.Forms.CheckBox();
			  ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			  ((System.ComponentModel.ISupportInitialize)(this.picCheck)).BeginInit();
			  this.SuspendLayout();
			  // 
			  // txtInput
			  // 
			  this.txtInput.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			  this.txtInput.Location = new System.Drawing.Point(12, 91);
			  this.txtInput.Name = "txtInput";
			  this.txtInput.Size = new System.Drawing.Size(445, 33);
			  this.txtInput.TabIndex = 2;
			  this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
			  this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
			  // 
			  // label1
			  // 
			  this.label1.AutoSize = true;
			  this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			  this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			  this.label1.Location = new System.Drawing.Point(12, 69);
			  this.label1.Name = "label1";
			  this.label1.Size = new System.Drawing.Size(123, 19);
			  this.label1.TabIndex = 1;
			  this.label1.Text = "请输入网址：";
			  // 
			  // btnAdd
			  // 
			  this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			  this.btnAdd.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			  this.btnAdd.Location = new System.Drawing.Point(359, 137);
			  this.btnAdd.Name = "btnAdd";
			  this.btnAdd.Size = new System.Drawing.Size(98, 37);
			  this.btnAdd.TabIndex = 0;
			  this.btnAdd.Text = "添加";
			  this.toolTip.SetToolTip(this.btnAdd, "单击以确认新建此任务");
			  this.btnAdd.UseVisualStyleBackColor = true;
			  this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			  // 
			  // pictureBox1
			  // 
			  this.pictureBox1.Image = global::Kaedei.AcDown.Properties.Resources.Logo;
			  this.pictureBox1.Location = new System.Drawing.Point(207, 12);
			  this.pictureBox1.Name = "pictureBox1";
			  this.pictureBox1.Size = new System.Drawing.Size(250, 53);
			  this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			  this.pictureBox1.TabIndex = 3;
			  this.pictureBox1.TabStop = false;
			  // 
			  // chkDownAllSection
			  // 
			  this.chkDownAllSection.AutoSize = true;
			  this.chkDownAllSection.FlatStyle = System.Windows.Forms.FlatStyle.System;
			  this.chkDownAllSection.Location = new System.Drawing.Point(16, 130);
			  this.chkDownAllSection.Name = "chkDownAllSection";
			  this.chkDownAllSection.Size = new System.Drawing.Size(150, 17);
			  this.chkDownAllSection.TabIndex = 4;
			  this.chkDownAllSection.Text = "自动下载所有相关章节";
			  this.toolTip.SetToolTip(this.chkDownAllSection, "选中此复选框后，将自动下载所有与此视频相关联的章节。\r\n在AcFun上，通常表示为视频标题旁的一个下拉列表框。\r\n内容示例：\r\n1、第一集\r\n2、第二集\r\n3、第" +
						 "三集");
			  this.chkDownAllSection.UseVisualStyleBackColor = true;
			  // 
			  // chkDownSub
			  // 
			  this.chkDownSub.AutoSize = true;
			  this.chkDownSub.FlatStyle = System.Windows.Forms.FlatStyle.System;
			  this.chkDownSub.Location = new System.Drawing.Point(16, 151);
			  this.chkDownSub.Name = "chkDownSub";
			  this.chkDownSub.Size = new System.Drawing.Size(102, 17);
			  this.chkDownSub.TabIndex = 5;
			  this.chkDownSub.Text = "下载字幕文件";
			  this.toolTip.SetToolTip(this.chkDownSub, "选中此复选框后，将自动下载与此视频关联的字幕文件");
			  this.chkDownSub.UseVisualStyleBackColor = true;
			  // 
			  // picCheck
			  // 
			  this.picCheck.Image = global::Kaedei.AcDown.Properties.Resources._1;
			  this.picCheck.Location = new System.Drawing.Point(116, 69);
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
			  this.toolTip.ToolTipTitle = "小提示:";
			  // 
			  // chkImmediate
			  // 
			  this.chkImmediate.AutoSize = true;
			  this.chkImmediate.Checked = true;
			  this.chkImmediate.CheckState = System.Windows.Forms.CheckState.Checked;
			  this.chkImmediate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			  this.chkImmediate.Location = new System.Drawing.Point(275, 151);
			  this.chkImmediate.Name = "chkImmediate";
			  this.chkImmediate.Size = new System.Drawing.Size(78, 17);
			  this.chkImmediate.TabIndex = 7;
			  this.chkImmediate.Text = "立即开始";
			  this.toolTip.SetToolTip(this.chkImmediate, "添加任务后是否立即开始任务\r\n(如果未选中则请手动开始下载任务)");
			  this.chkImmediate.UseVisualStyleBackColor = true;
			  // 
			  // FormNew
			  // 
			  this.AcceptButton = this.btnAdd;
			  this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			  this.ClientSize = new System.Drawing.Size(469, 187);
			  this.Controls.Add(this.chkImmediate);
			  this.Controls.Add(this.picCheck);
			  this.Controls.Add(this.chkDownSub);
			  this.Controls.Add(this.chkDownAllSection);
			  this.Controls.Add(this.pictureBox1);
			  this.Controls.Add(this.btnAdd);
			  this.Controls.Add(this.label1);
			  this.Controls.Add(this.txtInput);
			  this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			  this.MaximizeBox = false;
			  this.MinimizeBox = false;
			  this.Name = "FormNew";
			  this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			  this.Text = "新建下载任务";
			  this.Load += new System.EventHandler(this.FormNew_Load);
			  ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			  ((System.ComponentModel.ISupportInitialize)(this.picCheck)).EndInit();
			  this.ResumeLayout(false);
			  this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label label1;
		  private System.Windows.Forms.Button btnAdd;
		  private System.Windows.Forms.PictureBox pictureBox1;
		  private System.Windows.Forms.CheckBox chkDownAllSection;
		  private System.Windows.Forms.CheckBox chkDownSub;
		  private System.Windows.Forms.PictureBox picCheck;
		  private System.Windows.Forms.ToolTip toolTip;
		  private System.Windows.Forms.CheckBox chkImmediate;
    }
}