namespace Kaedei.AcDown.UI
{
    partial class FormAbout
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
         this.txtAbout = new System.Windows.Forms.TextBox();
         this.pictureBox1 = new System.Windows.Forms.PictureBox();
         this.lblVersion = new System.Windows.Forms.Label();
         this.btnOK = new System.Windows.Forms.Button();
         this.lnkSupport = new System.Windows.Forms.LinkLabel();
         this.lnkProject = new System.Windows.Forms.LinkLabel();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
         this.SuspendLayout();
         // 
         // txtAbout
         // 
         this.txtAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.txtAbout.Location = new System.Drawing.Point(12, 136);
         this.txtAbout.Multiline = true;
         this.txtAbout.Name = "txtAbout";
         this.txtAbout.ReadOnly = true;
         this.txtAbout.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.txtAbout.Size = new System.Drawing.Size(309, 223);
         this.txtAbout.TabIndex = 6;
         this.txtAbout.Text = resources.GetString("txtAbout.Text");
         // 
         // pictureBox1
         // 
         this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.pictureBox1.Image = global::Kaedei.AcDown.Properties.Resources.Logo;
         this.pictureBox1.Location = new System.Drawing.Point(14, 11);
         this.pictureBox1.Name = "pictureBox1";
         this.pictureBox1.Size = new System.Drawing.Size(300, 60);
         this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
         this.pictureBox1.TabIndex = 7;
         this.pictureBox1.TabStop = false;
         // 
         // lblVersion
         // 
         this.lblVersion.AutoSize = true;
         this.lblVersion.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblVersion.Location = new System.Drawing.Point(12, 74);
         this.lblVersion.Name = "lblVersion";
         this.lblVersion.Size = new System.Drawing.Size(41, 12);
         this.lblVersion.TabIndex = 8;
         this.lblVersion.Text = "版本：";
         this.lblVersion.DoubleClick += new System.EventHandler(this.lblVersion_DoubleClick);
         // 
         // btnOK
         // 
         this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnOK.Location = new System.Drawing.Point(235, 365);
         this.btnOK.Name = "btnOK";
         this.btnOK.Size = new System.Drawing.Size(86, 27);
         this.btnOK.TabIndex = 1;
         this.btnOK.Text = "确定";
         this.btnOK.UseVisualStyleBackColor = true;
         // 
         // lnkSupport
         // 
         this.lnkSupport.AutoSize = true;
         this.lnkSupport.LinkArea = new System.Windows.Forms.LinkArea(3, 6);
         this.lnkSupport.Location = new System.Drawing.Point(12, 95);
         this.lnkSupport.Name = "lnkSupport";
         this.lnkSupport.Size = new System.Drawing.Size(79, 19);
         this.lnkSupport.TabIndex = 10;
         this.lnkSupport.TabStop = true;
         this.lnkSupport.Text = "制作：Kaedei";
         this.lnkSupport.UseCompatibleTextRendering = true;
         this.lnkSupport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSupport_LinkClicked);
         // 
         // lnkProject
         // 
         this.lnkProject.AutoSize = true;
         this.lnkProject.LinkArea = new System.Windows.Forms.LinkArea(5, 32);
         this.lnkProject.Location = new System.Drawing.Point(12, 114);
         this.lnkProject.Name = "lnkProject";
         this.lnkProject.Size = new System.Drawing.Size(233, 19);
         this.lnkProject.TabIndex = 11;
         this.lnkProject.TabStop = true;
         this.lnkProject.Text = "项目主页：http://acdown.codeplex.com/";
         this.lnkProject.UseCompatibleTextRendering = true;
         this.lnkProject.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkProject_LinkClicked);
         // 
         // FormAbout
         // 
         this.AcceptButton = this.btnOK;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.CancelButton = this.btnOK;
         this.ClientSize = new System.Drawing.Size(333, 403);
         this.Controls.Add(this.pictureBox1);
         this.Controls.Add(this.lnkProject);
         this.Controls.Add(this.btnOK);
         this.Controls.Add(this.lblVersion);
         this.Controls.Add(this.txtAbout);
         this.Controls.Add(this.lnkSupport);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "FormAbout";
         this.Padding = new System.Windows.Forms.Padding(9, 8, 9, 8);
         this.ShowIcon = false;
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "关于AcDown动漫下载器";
         this.Load += new System.EventHandler(this.FormAbout_Load);
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtAbout;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.LinkLabel lnkSupport;
        private System.Windows.Forms.LinkLabel lnkProject;

    }
}
