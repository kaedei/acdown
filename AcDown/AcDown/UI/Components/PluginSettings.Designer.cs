namespace Kaedei.AcDown.UI.Components
{
   partial class PluginSettings
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
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("");
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("");
			System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("");
			this.lsv = new System.Windows.Forms.ListView();
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label9 = new System.Windows.Forms.Label();
			this.btnProperty = new System.Windows.Forms.Button();
			this.btnSupport = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lsv
			// 
			this.lsv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lsv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader2,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader1});
			this.lsv.FullRowSelect = true;
			this.lsv.GridLines = true;
			listViewItem4.Checked = true;
			listViewItem4.StateImageIndex = 1;
			listViewItem5.Checked = true;
			listViewItem5.StateImageIndex = 1;
			listViewItem6.Checked = true;
			listViewItem6.StateImageIndex = 1;
			this.lsv.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem4,
            listViewItem5,
            listViewItem6});
			this.lsv.Location = new System.Drawing.Point(5, 42);
			this.lsv.MultiSelect = false;
			this.lsv.Name = "lsv";
			this.lsv.Size = new System.Drawing.Size(403, 259);
			this.lsv.TabIndex = 7;
			this.lsv.UseCompatibleStateImageBehavior = false;
			this.lsv.View = System.Windows.Forms.View.Details;
			this.lsv.SelectedIndexChanged += new System.EventHandler(this.lsv_SelectedIndexChanged);
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "插件名称";
			this.columnHeader7.Width = 147;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "版本";
			this.columnHeader2.Width = 120;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "作者";
			this.columnHeader8.Width = 77;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "描述";
			this.columnHeader9.Width = 145;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "插件网址";
			this.columnHeader10.Width = 100;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "内部名称";
			this.columnHeader1.Width = 85;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(3, 13);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(278, 26);
			this.label9.TabIndex = 8;
			this.label9.Text = "AcDown插件能够帮助你解析各种各样的网络地址：\r\n(添加或删除插件后，需要重新启动AcDown才能生效)";
			// 
			// btnProperty
			// 
			this.btnProperty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnProperty.Enabled = false;
			this.btnProperty.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnProperty.Location = new System.Drawing.Point(301, 307);
			this.btnProperty.Name = "btnProperty";
			this.btnProperty.Size = new System.Drawing.Size(107, 27);
			this.btnProperty.TabIndex = 10;
			this.btnProperty.Text = "插件属性";
			this.btnProperty.UseVisualStyleBackColor = true;
			this.btnProperty.Click += new System.EventHandler(this.btnProperty_Click);
			// 
			// btnSupport
			// 
			this.btnSupport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSupport.Enabled = false;
			this.btnSupport.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSupport.Location = new System.Drawing.Point(188, 307);
			this.btnSupport.Name = "btnSupport";
			this.btnSupport.Size = new System.Drawing.Size(107, 27);
			this.btnSupport.TabIndex = 11;
			this.btnSupport.Text = "访问插件主页";
			this.btnSupport.UseVisualStyleBackColor = true;
			this.btnSupport.Click += new System.EventHandler(this.btnSupport_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAdd.Location = new System.Drawing.Point(6, 307);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(78, 27);
			this.btnAdd.TabIndex = 12;
			this.btnAdd.Text = "添加";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDelete.Enabled = false;
			this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDelete.Location = new System.Drawing.Point(90, 307);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(78, 27);
			this.btnDelete.TabIndex = 13;
			this.btnDelete.Text = "删除";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// PluginSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnSupport);
			this.Controls.Add(this.btnProperty);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.lsv);
			this.Name = "PluginSettings";
			this.Size = new System.Drawing.Size(411, 346);
			this.Load += new System.EventHandler(this.PluginSettings_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.ListView lsv;
      private System.Windows.Forms.ColumnHeader columnHeader7;
      private System.Windows.Forms.ColumnHeader columnHeader8;
      private System.Windows.Forms.ColumnHeader columnHeader9;
      private System.Windows.Forms.ColumnHeader columnHeader10;
      private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ColumnHeader columnHeader1;
      private System.Windows.Forms.Button btnProperty;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Button btnSupport;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnDelete;
   }
}
