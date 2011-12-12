namespace Kaedei.AcDown.UI.Components
{
   partial class AcPlayControl
   {
      /// <summary> 
      /// 必需的设计器变量。
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary> 
      /// 清理所有正在使用的资源。
      /// </summary>
      /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region 组件设计器生成的代码

      /// <summary> 
      /// 设计器支持所需的方法 - 不要
      /// 使用代码编辑器修改此方法的内容。
      /// </summary>
      private void InitializeComponent()
      {
         this.btnStart = new System.Windows.Forms.Button();
         this.panelPlayer = new System.Windows.Forms.Panel();
         this.lnkQA = new System.Windows.Forms.LinkLabel();
         this.btnLaunchAcPlay = new System.Windows.Forms.Button();
         this.groupLength = new System.Windows.Forms.GroupBox();
         this.label6 = new System.Windows.Forms.Label();
         this.udSec = new System.Windows.Forms.NumericUpDown();
         this.label5 = new System.Windows.Forms.Label();
         this.udMin = new System.Windows.Forms.NumericUpDown();
         this.label4 = new System.Windows.Forms.Label();
         this.groupBox3 = new System.Windows.Forms.GroupBox();
         this.btnXml2 = new System.Windows.Forms.Button();
         this.txtXml2 = new System.Windows.Forms.TextBox();
         this.lblXml2 = new System.Windows.Forms.Label();
         this.btnXml = new System.Windows.Forms.Button();
         this.txtXml = new System.Windows.Forms.TextBox();
         this.lblXml = new System.Windows.Forms.Label();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.btnItemClear = new System.Windows.Forms.Button();
         this.btnItemDown = new System.Windows.Forms.Button();
         this.btnItemUp = new System.Windows.Forms.Button();
         this.btnItemDelete = new System.Windows.Forms.Button();
         this.btnItemAdd = new System.Windows.Forms.Button();
         this.lstVideo = new System.Windows.Forms.ListBox();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.lnkUpdatePlayer = new System.Windows.Forms.LinkLabel();
         this.label1 = new System.Windows.Forms.Label();
         this.cboPlayer = new System.Windows.Forms.ComboBox();
         this.panelPlayer.SuspendLayout();
         this.groupLength.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.udSec)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.udMin)).BeginInit();
         this.groupBox3.SuspendLayout();
         this.groupBox2.SuspendLayout();
         this.groupBox1.SuspendLayout();
         this.SuspendLayout();
         // 
         // btnStart
         // 
         this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnStart.Location = new System.Drawing.Point(17, 36);
         this.btnStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.btnStart.Name = "btnStart";
         this.btnStart.Size = new System.Drawing.Size(193, 64);
         this.btnStart.TabIndex = 0;
         this.btnStart.Text = "下载AcPlay弹幕播放插件";
         this.btnStart.UseVisualStyleBackColor = true;
         this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
         // 
         // panelPlayer
         // 
         this.panelPlayer.AutoScroll = true;
         this.panelPlayer.Controls.Add(this.lnkQA);
         this.panelPlayer.Controls.Add(this.btnLaunchAcPlay);
         this.panelPlayer.Controls.Add(this.groupLength);
         this.panelPlayer.Controls.Add(this.groupBox3);
         this.panelPlayer.Controls.Add(this.groupBox2);
         this.panelPlayer.Controls.Add(this.groupBox1);
         this.panelPlayer.Dock = System.Windows.Forms.DockStyle.Top;
         this.panelPlayer.Location = new System.Drawing.Point(0, 0);
         this.panelPlayer.Name = "panelPlayer";
         this.panelPlayer.Size = new System.Drawing.Size(506, 553);
         this.panelPlayer.TabIndex = 4;
         this.panelPlayer.Visible = false;
         // 
         // lnkQA
         // 
         this.lnkQA.AutoSize = true;
         this.lnkQA.Location = new System.Drawing.Point(9, 12);
         this.lnkQA.Name = "lnkQA";
         this.lnkQA.Size = new System.Drawing.Size(180, 20);
         this.lnkQA.TabIndex = 13;
         this.lnkQA.TabStop = true;
         this.lnkQA.Text = "AcPlay使用方法及常见问题";
         this.lnkQA.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkQA_LinkClicked);
         // 
         // btnLaunchAcPlay
         // 
         this.btnLaunchAcPlay.Enabled = false;
         this.btnLaunchAcPlay.Location = new System.Drawing.Point(340, 495);
         this.btnLaunchAcPlay.Name = "btnLaunchAcPlay";
         this.btnLaunchAcPlay.Size = new System.Drawing.Size(116, 36);
         this.btnLaunchAcPlay.TabIndex = 12;
         this.btnLaunchAcPlay.Text = "启动AcPlay";
         this.btnLaunchAcPlay.UseVisualStyleBackColor = true;
         this.btnLaunchAcPlay.Click += new System.EventHandler(this.btnLaunchAcPlay_Click);
         // 
         // groupLength
         // 
         this.groupLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.groupLength.Controls.Add(this.label6);
         this.groupLength.Controls.Add(this.udSec);
         this.groupLength.Controls.Add(this.label5);
         this.groupLength.Controls.Add(this.udMin);
         this.groupLength.Controls.Add(this.label4);
         this.groupLength.Location = new System.Drawing.Point(3, 476);
         this.groupLength.Name = "groupLength";
         this.groupLength.Size = new System.Drawing.Size(317, 65);
         this.groupLength.TabIndex = 11;
         this.groupLength.TabStop = false;
         this.groupLength.Text = "播放时间";
         // 
         // label6
         // 
         this.label6.AutoSize = true;
         this.label6.Location = new System.Drawing.Point(245, 31);
         this.label6.Name = "label6";
         this.label6.Size = new System.Drawing.Size(23, 20);
         this.label6.TabIndex = 4;
         this.label6.Text = "秒";
         // 
         // udSec
         // 
         this.udSec.Location = new System.Drawing.Point(200, 29);
         this.udSec.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
         this.udSec.Name = "udSec";
         this.udSec.Size = new System.Drawing.Size(39, 26);
         this.udSec.TabIndex = 3;
         this.udSec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // label5
         // 
         this.label5.AutoSize = true;
         this.label5.Location = new System.Drawing.Point(171, 31);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(23, 20);
         this.label5.TabIndex = 2;
         this.label5.Text = "分";
         // 
         // udMin
         // 
         this.udMin.Location = new System.Drawing.Point(101, 29);
         this.udMin.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
         this.udMin.Name = "udMin";
         this.udMin.Size = new System.Drawing.Size(64, 26);
         this.udMin.TabIndex = 1;
         this.udMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // label4
         // 
         this.label4.AutoSize = true;
         this.label4.Location = new System.Drawing.Point(16, 31);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(79, 20);
         this.label4.TabIndex = 0;
         this.label4.Text = "视频总时长";
         // 
         // groupBox3
         // 
         this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.groupBox3.Controls.Add(this.btnXml2);
         this.groupBox3.Controls.Add(this.txtXml2);
         this.groupBox3.Controls.Add(this.lblXml2);
         this.groupBox3.Controls.Add(this.btnXml);
         this.groupBox3.Controls.Add(this.txtXml);
         this.groupBox3.Controls.Add(this.lblXml);
         this.groupBox3.Location = new System.Drawing.Point(3, 356);
         this.groupBox3.Name = "groupBox3";
         this.groupBox3.Size = new System.Drawing.Size(486, 114);
         this.groupBox3.TabIndex = 10;
         this.groupBox3.TabStop = false;
         this.groupBox3.Text = "弹幕文件";
         // 
         // btnXml2
         // 
         this.btnXml2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.btnXml2.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnXml2.Location = new System.Drawing.Point(387, 69);
         this.btnXml2.Name = "btnXml2";
         this.btnXml2.Size = new System.Drawing.Size(93, 31);
         this.btnXml2.TabIndex = 8;
         this.btnXml2.Text = "选择";
         this.btnXml2.UseVisualStyleBackColor = true;
         this.btnXml2.Click += new System.EventHandler(this.btnXml2_Click);
         // 
         // txtXml2
         // 
         this.txtXml2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.txtXml2.Location = new System.Drawing.Point(141, 71);
         this.txtXml2.Name = "txtXml2";
         this.txtXml2.ReadOnly = true;
         this.txtXml2.Size = new System.Drawing.Size(240, 26);
         this.txtXml2.TabIndex = 7;
         // 
         // lblXml2
         // 
         this.lblXml2.AutoSize = true;
         this.lblXml2.Location = new System.Drawing.Point(16, 74);
         this.lblXml2.Name = "lblXml2";
         this.lblXml2.Size = new System.Drawing.Size(117, 20);
         this.lblXml2.TabIndex = 6;
         this.lblXml2.Text = "弹幕XML文件2：";
         // 
         // btnXml
         // 
         this.btnXml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.btnXml.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnXml.Location = new System.Drawing.Point(387, 28);
         this.btnXml.Name = "btnXml";
         this.btnXml.Size = new System.Drawing.Size(93, 31);
         this.btnXml.TabIndex = 5;
         this.btnXml.Text = "选择";
         this.btnXml.UseVisualStyleBackColor = true;
         this.btnXml.Click += new System.EventHandler(this.btnXml_Click);
         // 
         // txtXml
         // 
         this.txtXml.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.txtXml.Location = new System.Drawing.Point(141, 30);
         this.txtXml.Name = "txtXml";
         this.txtXml.ReadOnly = true;
         this.txtXml.Size = new System.Drawing.Size(240, 26);
         this.txtXml.TabIndex = 1;
         // 
         // lblXml
         // 
         this.lblXml.AutoSize = true;
         this.lblXml.Location = new System.Drawing.Point(16, 33);
         this.lblXml.Name = "lblXml";
         this.lblXml.Size = new System.Drawing.Size(109, 20);
         this.lblXml.TabIndex = 0;
         this.lblXml.Text = "弹幕XML文件：";
         // 
         // groupBox2
         // 
         this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.groupBox2.Controls.Add(this.btnItemClear);
         this.groupBox2.Controls.Add(this.btnItemDown);
         this.groupBox2.Controls.Add(this.btnItemUp);
         this.groupBox2.Controls.Add(this.btnItemDelete);
         this.groupBox2.Controls.Add(this.btnItemAdd);
         this.groupBox2.Controls.Add(this.lstVideo);
         this.groupBox2.Location = new System.Drawing.Point(3, 114);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(486, 236);
         this.groupBox2.TabIndex = 9;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "视频文件";
         // 
         // btnItemClear
         // 
         this.btnItemClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.btnItemClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnItemClear.Location = new System.Drawing.Point(387, 189);
         this.btnItemClear.Name = "btnItemClear";
         this.btnItemClear.Size = new System.Drawing.Size(93, 35);
         this.btnItemClear.TabIndex = 5;
         this.btnItemClear.Text = "清空";
         this.btnItemClear.UseVisualStyleBackColor = true;
         this.btnItemClear.Click += new System.EventHandler(this.btnItemClear_Click);
         // 
         // btnItemDown
         // 
         this.btnItemDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.btnItemDown.Enabled = false;
         this.btnItemDown.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnItemDown.Location = new System.Drawing.Point(387, 148);
         this.btnItemDown.Name = "btnItemDown";
         this.btnItemDown.Size = new System.Drawing.Size(93, 35);
         this.btnItemDown.TabIndex = 4;
         this.btnItemDown.Text = "下移";
         this.btnItemDown.UseVisualStyleBackColor = true;
         this.btnItemDown.Click += new System.EventHandler(this.btnItemDown_Click);
         // 
         // btnItemUp
         // 
         this.btnItemUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.btnItemUp.Enabled = false;
         this.btnItemUp.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnItemUp.Location = new System.Drawing.Point(387, 107);
         this.btnItemUp.Name = "btnItemUp";
         this.btnItemUp.Size = new System.Drawing.Size(93, 35);
         this.btnItemUp.TabIndex = 3;
         this.btnItemUp.Text = "上移";
         this.btnItemUp.UseVisualStyleBackColor = true;
         this.btnItemUp.Click += new System.EventHandler(this.btnItemUp_Click);
         // 
         // btnItemDelete
         // 
         this.btnItemDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.btnItemDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnItemDelete.Location = new System.Drawing.Point(387, 66);
         this.btnItemDelete.Name = "btnItemDelete";
         this.btnItemDelete.Size = new System.Drawing.Size(93, 35);
         this.btnItemDelete.TabIndex = 2;
         this.btnItemDelete.Text = "删除";
         this.btnItemDelete.UseVisualStyleBackColor = true;
         this.btnItemDelete.Click += new System.EventHandler(this.btnItemDelete_Click);
         // 
         // btnItemAdd
         // 
         this.btnItemAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.btnItemAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnItemAdd.Location = new System.Drawing.Point(387, 25);
         this.btnItemAdd.Name = "btnItemAdd";
         this.btnItemAdd.Size = new System.Drawing.Size(93, 35);
         this.btnItemAdd.TabIndex = 1;
         this.btnItemAdd.Text = "添加";
         this.btnItemAdd.UseVisualStyleBackColor = true;
         this.btnItemAdd.Click += new System.EventHandler(this.btnItemAdd_Click);
         // 
         // lstVideo
         // 
         this.lstVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.lstVideo.FormattingEnabled = true;
         this.lstVideo.ItemHeight = 20;
         this.lstVideo.Location = new System.Drawing.Point(10, 25);
         this.lstVideo.Name = "lstVideo";
         this.lstVideo.ScrollAlwaysVisible = true;
         this.lstVideo.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
         this.lstVideo.Size = new System.Drawing.Size(371, 204);
         this.lstVideo.TabIndex = 0;
         this.lstVideo.SelectedIndexChanged += new System.EventHandler(this.lstVideo_SelectedIndexChanged);
         // 
         // groupBox1
         // 
         this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.groupBox1.Controls.Add(this.lnkUpdatePlayer);
         this.groupBox1.Controls.Add(this.label1);
         this.groupBox1.Controls.Add(this.cboPlayer);
         this.groupBox1.Location = new System.Drawing.Point(3, 50);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(486, 58);
         this.groupBox1.TabIndex = 8;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "播放器";
         // 
         // lnkUpdatePlayer
         // 
         this.lnkUpdatePlayer.AutoSize = true;
         this.lnkUpdatePlayer.Location = new System.Drawing.Point(336, 22);
         this.lnkUpdatePlayer.Name = "lnkUpdatePlayer";
         this.lnkUpdatePlayer.Size = new System.Drawing.Size(107, 20);
         this.lnkUpdatePlayer.TabIndex = 3;
         this.lnkUpdatePlayer.TabStop = true;
         this.lnkUpdatePlayer.Text = "更新播放器缓存";
         this.lnkUpdatePlayer.Visible = false;
         this.lnkUpdatePlayer.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUpdatePlayer_LinkClicked);
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(6, 22);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(107, 20);
         this.label1.TabIndex = 2;
         this.label1.Text = "请选择播放器：";
         // 
         // cboPlayer
         // 
         this.cboPlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cboPlayer.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.cboPlayer.FormattingEnabled = true;
         this.cboPlayer.Items.AddRange(new object[] {
            "BiliBili.tv播放器",
            "Acfun.tv播放器"});
         this.cboPlayer.Location = new System.Drawing.Point(141, 19);
         this.cboPlayer.Name = "cboPlayer";
         this.cboPlayer.Size = new System.Drawing.Size(176, 28);
         this.cboPlayer.TabIndex = 1;
         this.cboPlayer.SelectedIndexChanged += new System.EventHandler(this.cboPlayer_SelectedIndexChanged);
         // 
         // AcPlayControl
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.AutoScroll = true;
         this.Controls.Add(this.btnStart);
         this.Controls.Add(this.panelPlayer);
         this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.Name = "AcPlayControl";
         this.Size = new System.Drawing.Size(506, 435);
         this.Load += new System.EventHandler(this.AcPlay_Load);
         this.panelPlayer.ResumeLayout(false);
         this.panelPlayer.PerformLayout();
         this.groupLength.ResumeLayout(false);
         this.groupLength.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.udSec)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.udMin)).EndInit();
         this.groupBox3.ResumeLayout(false);
         this.groupBox3.PerformLayout();
         this.groupBox2.ResumeLayout(false);
         this.groupBox1.ResumeLayout(false);
         this.groupBox1.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Button btnStart;
      private System.Windows.Forms.Panel panelPlayer;
      private System.Windows.Forms.GroupBox groupLength;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.NumericUpDown udSec;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.NumericUpDown udMin;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.GroupBox groupBox3;
      private System.Windows.Forms.Button btnXml2;
      private System.Windows.Forms.TextBox txtXml2;
      private System.Windows.Forms.Label lblXml2;
      private System.Windows.Forms.Button btnXml;
      private System.Windows.Forms.TextBox txtXml;
      private System.Windows.Forms.Label lblXml;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.Button btnItemDown;
      private System.Windows.Forms.Button btnItemUp;
      private System.Windows.Forms.Button btnItemDelete;
      private System.Windows.Forms.Button btnItemAdd;
      private System.Windows.Forms.ListBox lstVideo;
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.LinkLabel lnkUpdatePlayer;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.ComboBox cboPlayer;
      private System.Windows.Forms.Button btnLaunchAcPlay;
      private System.Windows.Forms.LinkLabel lnkQA;
      private System.Windows.Forms.Button btnItemClear;
   }
}
