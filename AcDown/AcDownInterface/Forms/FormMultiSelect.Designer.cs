namespace Kaedei.AcDown.Interface.Forms
{
   partial class FormMultiSelect
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
		  this.mnu = new System.Windows.Forms.ContextMenuStrip(this.components);
		  this.请选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.mnuSelectUp = new System.Windows.Forms.ToolStripMenuItem();
		  this.mnuDeselectUp = new System.Windows.Forms.ToolStripMenuItem();
		  this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
		  this.mnuSelectDown = new System.Windows.Forms.ToolStripMenuItem();
		  this.mnuDeselectDown = new System.Windows.Forms.ToolStripMenuItem();
		  this.btnOK = new System.Windows.Forms.Button();
		  this.lnkSelectAll = new System.Windows.Forms.LinkLabel();
		  this.lnkSelectNone = new System.Windows.Forms.LinkLabel();
		  this.lnkSelectInvert = new System.Windows.Forms.LinkLabel();
		  this.label1 = new System.Windows.Forms.Label();
		  this.tmr = new System.Windows.Forms.Timer(this.components);
		  this.lsv = new System.Windows.Forms.ListView();
		  this.mnu.SuspendLayout();
		  this.SuspendLayout();
		  // 
		  // mnu
		  // 
		  this.mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.请选择ToolStripMenuItem,
            this.mnuSelectUp,
            this.mnuDeselectUp,
            this.toolStripMenuItem1,
            this.mnuSelectDown,
            this.mnuDeselectDown});
		  this.mnu.Name = "mnu";
		  this.mnu.Size = new System.Drawing.Size(195, 120);
		  // 
		  // 请选择ToolStripMenuItem
		  // 
		  this.请选择ToolStripMenuItem.Enabled = false;
		  this.请选择ToolStripMenuItem.Name = "请选择ToolStripMenuItem";
		  this.请选择ToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
		  this.请选择ToolStripMenuItem.Text = "请选择:";
		  // 
		  // mnuSelectUp
		  // 
		  this.mnuSelectUp.Name = "mnuSelectUp";
		  this.mnuSelectUp.Size = new System.Drawing.Size(194, 22);
		  this.mnuSelectUp.Text = "选中之前所有项目";
		  this.mnuSelectUp.Click += new System.EventHandler(this.mnuSelectUp_Click);
		  // 
		  // mnuDeselectUp
		  // 
		  this.mnuDeselectUp.Name = "mnuDeselectUp";
		  this.mnuDeselectUp.Size = new System.Drawing.Size(194, 22);
		  this.mnuDeselectUp.Text = "取消选中之前所有项目";
		  this.mnuDeselectUp.Click += new System.EventHandler(this.mnuDeselectUp_Click);
		  // 
		  // toolStripMenuItem1
		  // 
		  this.toolStripMenuItem1.Name = "toolStripMenuItem1";
		  this.toolStripMenuItem1.Size = new System.Drawing.Size(179, 6);
		  // 
		  // mnuSelectDown
		  // 
		  this.mnuSelectDown.Name = "mnuSelectDown";
		  this.mnuSelectDown.Size = new System.Drawing.Size(194, 22);
		  this.mnuSelectDown.Text = "选中之后所有项目";
		  this.mnuSelectDown.Click += new System.EventHandler(this.mnuSelectDown_Click);
		  // 
		  // mnuDeselectDown
		  // 
		  this.mnuDeselectDown.Name = "mnuDeselectDown";
		  this.mnuDeselectDown.Size = new System.Drawing.Size(194, 22);
		  this.mnuDeselectDown.Text = "取消选中之后所有项目";
		  this.mnuDeselectDown.Click += new System.EventHandler(this.mnuDeselectDown_Click);
		  // 
		  // btnOK
		  // 
		  this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		  this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
		  this.btnOK.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
		  this.btnOK.Location = new System.Drawing.Point(285, 391);
		  this.btnOK.Margin = new System.Windows.Forms.Padding(5);
		  this.btnOK.Name = "btnOK";
		  this.btnOK.Size = new System.Drawing.Size(96, 36);
		  this.btnOK.TabIndex = 2;
		  this.btnOK.Text = "确定";
		  this.btnOK.UseVisualStyleBackColor = true;
		  this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
		  // 
		  // lnkSelectAll
		  // 
		  this.lnkSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		  this.lnkSelectAll.AutoSize = true;
		  this.lnkSelectAll.Location = new System.Drawing.Point(10, 399);
		  this.lnkSelectAll.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
		  this.lnkSelectAll.Name = "lnkSelectAll";
		  this.lnkSelectAll.Size = new System.Drawing.Size(42, 21);
		  this.lnkSelectAll.TabIndex = 3;
		  this.lnkSelectAll.TabStop = true;
		  this.lnkSelectAll.Text = "全选";
		  this.lnkSelectAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSelectAll_LinkClicked);
		  // 
		  // lnkSelectNone
		  // 
		  this.lnkSelectNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		  this.lnkSelectNone.AutoSize = true;
		  this.lnkSelectNone.Location = new System.Drawing.Point(62, 399);
		  this.lnkSelectNone.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
		  this.lnkSelectNone.Name = "lnkSelectNone";
		  this.lnkSelectNone.Size = new System.Drawing.Size(58, 21);
		  this.lnkSelectNone.TabIndex = 4;
		  this.lnkSelectNone.TabStop = true;
		  this.lnkSelectNone.Text = "全不选";
		  this.lnkSelectNone.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSelectNone_LinkClicked);
		  // 
		  // lnkSelectInvert
		  // 
		  this.lnkSelectInvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		  this.lnkSelectInvert.AutoSize = true;
		  this.lnkSelectInvert.Location = new System.Drawing.Point(130, 399);
		  this.lnkSelectInvert.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
		  this.lnkSelectInvert.Name = "lnkSelectInvert";
		  this.lnkSelectInvert.Size = new System.Drawing.Size(74, 21);
		  this.lnkSelectInvert.TabIndex = 5;
		  this.lnkSelectInvert.TabStop = true;
		  this.lnkSelectInvert.Text = "反向选择";
		  this.lnkSelectInvert.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSelectInvert_LinkClicked);
		  // 
		  // label1
		  // 
		  this.label1.Location = new System.Drawing.Point(14, 9);
		  this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
		  this.label1.Name = "label1";
		  this.label1.Size = new System.Drawing.Size(316, 45);
		  this.label1.TabIndex = 6;
		  this.label1.Text = "请确认要下载的章节：\r\n(鼠标右键单击显示更多选项)";
		  // 
		  // tmr
		  // 
		  this.tmr.Enabled = true;
		  this.tmr.Interval = 1000;
		  this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
		  // 
		  // lsv
		  // 
		  this.lsv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
					  | System.Windows.Forms.AnchorStyles.Left)
					  | System.Windows.Forms.AnchorStyles.Right)));
		  this.lsv.CheckBoxes = true;
		  this.lsv.FullRowSelect = true;
		  this.lsv.Location = new System.Drawing.Point(18, 57);
		  this.lsv.MultiSelect = false;
		  this.lsv.Name = "lsv";
		  this.lsv.Size = new System.Drawing.Size(363, 326);
		  this.lsv.TabIndex = 7;
		  this.lsv.UseCompatibleStateImageBehavior = false;
		  this.lsv.View = System.Windows.Forms.View.List;
		  this.lsv.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lst_MouseUp);
		  // 
		  // FormMultiSelect
		  // 
		  this.AcceptButton = this.btnOK;
		  this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
		  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		  this.ClientSize = new System.Drawing.Size(395, 433);
		  this.ControlBox = false;
		  this.Controls.Add(this.lsv);
		  this.Controls.Add(this.label1);
		  this.Controls.Add(this.lnkSelectInvert);
		  this.Controls.Add(this.lnkSelectNone);
		  this.Controls.Add(this.lnkSelectAll);
		  this.Controls.Add(this.btnOK);
		  this.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
		  this.Margin = new System.Windows.Forms.Padding(5);
		  this.MinimumSize = new System.Drawing.Size(340, 390);
		  this.Name = "FormMultiSelect";
		  this.ShowInTaskbar = false;
		  this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		  this.Text = "请选择一个或多个选项";
		  this.TopMost = true;
		  this.mnu.ResumeLayout(false);
		  this.ResumeLayout(false);
		  this.PerformLayout();

      }

      #endregion

		private System.Windows.Forms.Button btnOK;
      private System.Windows.Forms.LinkLabel lnkSelectAll;
      private System.Windows.Forms.LinkLabel lnkSelectNone;
      private System.Windows.Forms.LinkLabel lnkSelectInvert;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.ContextMenuStrip mnu;
      private System.Windows.Forms.ToolStripMenuItem 请选择ToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem mnuSelectUp;
      private System.Windows.Forms.ToolStripMenuItem mnuDeselectUp;
      private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
      private System.Windows.Forms.ToolStripMenuItem mnuSelectDown;
      private System.Windows.Forms.ToolStripMenuItem mnuDeselectDown;
		private System.Windows.Forms.Timer tmr;
		private System.Windows.Forms.ListView lsv;
   }
}