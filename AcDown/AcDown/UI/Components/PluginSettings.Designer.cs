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
         System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
         System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("");
         System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("");
         this.lsv = new System.Windows.Forms.ListView();
         this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.label9 = new System.Windows.Forms.Label();
         this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.SuspendLayout();
         // 
         // lsv
         // 
         this.lsv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.lsv.CheckBoxes = true;
         this.lsv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader1});
         this.lsv.FullRowSelect = true;
         this.lsv.GridLines = true;
         listViewItem1.Checked = true;
         listViewItem1.StateImageIndex = 1;
         listViewItem2.Checked = true;
         listViewItem2.StateImageIndex = 1;
         listViewItem3.Checked = true;
         listViewItem3.StateImageIndex = 1;
         this.lsv.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
         this.lsv.Location = new System.Drawing.Point(6, 39);
         this.lsv.MultiSelect = false;
         this.lsv.Name = "lsv";
         this.lsv.Size = new System.Drawing.Size(395, 269);
         this.lsv.TabIndex = 7;
         this.lsv.UseCompatibleStateImageBehavior = false;
         this.lsv.View = System.Windows.Forms.View.Details;
         // 
         // columnHeader6
         // 
         this.columnHeader6.Text = "启用";
         this.columnHeader6.Width = 80;
         // 
         // columnHeader7
         // 
         this.columnHeader7.Text = "插件名称";
         this.columnHeader7.Width = 93;
         // 
         // columnHeader8
         // 
         this.columnHeader8.Text = "作者";
         // 
         // columnHeader9
         // 
         this.columnHeader9.Text = "描述";
         this.columnHeader9.Width = 100;
         // 
         // columnHeader10
         // 
         this.columnHeader10.Text = "支持信息";
         this.columnHeader10.Width = 75;
         // 
         // label9
         // 
         this.label9.AutoSize = true;
         this.label9.Location = new System.Drawing.Point(3, 0);
         this.label9.Name = "label9";
         this.label9.Size = new System.Drawing.Size(269, 24);
         this.label9.TabIndex = 8;
         this.label9.Text = "AcDown插件能够帮助你解析各种各样的网络地址：\r\n（设置插件启用/禁用后，重启下载器即可生效）";
         // 
         // columnHeader1
         // 
         this.columnHeader1.Text = "内部名称";
         // 
         // PluginSettings
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.label9);
         this.Controls.Add(this.lsv);
         this.Name = "PluginSettings";
         this.Size = new System.Drawing.Size(404, 311);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.ListView lsv;
      private System.Windows.Forms.ColumnHeader columnHeader6;
      private System.Windows.Forms.ColumnHeader columnHeader7;
      private System.Windows.Forms.ColumnHeader columnHeader8;
      private System.Windows.Forms.ColumnHeader columnHeader9;
      private System.Windows.Forms.ColumnHeader columnHeader10;
      private System.Windows.Forms.Label label9;
      private System.Windows.Forms.ColumnHeader columnHeader1;
   }
}
