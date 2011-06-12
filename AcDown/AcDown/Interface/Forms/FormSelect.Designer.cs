namespace Kaedei.AcDown.Interface.Forms
{
   partial class FormSelect
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
         this.lst = new System.Windows.Forms.CheckedListBox();
         this.btnOK = new System.Windows.Forms.Button();
         this.lnkSelectAll = new System.Windows.Forms.LinkLabel();
         this.lnkSelectNone = new System.Windows.Forms.LinkLabel();
         this.lnkSelectInvert = new System.Windows.Forms.LinkLabel();
         this.label1 = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // lst
         // 
         this.lst.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.lst.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.lst.CheckOnClick = true;
         this.lst.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.lst.FormattingEnabled = true;
         this.lst.Location = new System.Drawing.Point(14, 42);
         this.lst.Margin = new System.Windows.Forms.Padding(5);
         this.lst.Name = "lst";
         this.lst.Size = new System.Drawing.Size(324, 314);
         this.lst.TabIndex = 0;
         // 
         // btnOK
         // 
         this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnOK.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.btnOK.Location = new System.Drawing.Point(242, 379);
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
         this.lnkSelectAll.Location = new System.Drawing.Point(10, 387);
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
         this.lnkSelectNone.Location = new System.Drawing.Point(62, 387);
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
         this.lnkSelectInvert.Location = new System.Drawing.Point(130, 387);
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
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(14, 9);
         this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(170, 21);
         this.label1.TabIndex = 6;
         this.label1.Text = "请确认要下载的章节：";
         // 
         // FormSelect
         // 
         this.AcceptButton = this.btnOK;
         this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(352, 429);
         this.ControlBox = false;
         this.Controls.Add(this.label1);
         this.Controls.Add(this.lnkSelectInvert);
         this.Controls.Add(this.lnkSelectNone);
         this.Controls.Add(this.lnkSelectAll);
         this.Controls.Add(this.btnOK);
         this.Controls.Add(this.lst);
         this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.Margin = new System.Windows.Forms.Padding(5);
         this.MinimumSize = new System.Drawing.Size(340, 390);
         this.Name = "FormSelect";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "请选择要下载的章节";
         this.TopMost = true;
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.CheckedListBox lst;
      private System.Windows.Forms.Button btnOK;
      private System.Windows.Forms.LinkLabel lnkSelectAll;
      private System.Windows.Forms.LinkLabel lnkSelectNone;
      private System.Windows.Forms.LinkLabel lnkSelectInvert;
      private System.Windows.Forms.Label label1;
   }
}