namespace Kaedei.AcDown.UI.Components
{
   partial class WebsitesControl
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
         this.components = new System.ComponentModel.Container();
         System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Acfun.tv", 1);
         System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Bilibili.tv", 2);
         System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("优酷网", 3);
         System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("爱漫画", 4);
         System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("土豆网", 5);
         System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("百度贴吧相册", 6);
         System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("更多...", 0);
         this.lsv = new System.Windows.Forms.ListView();
         this.il = new System.Windows.Forms.ImageList(this.components);
         this.SuspendLayout();
         // 
         // lsv
         // 
         this.lsv.Activation = System.Windows.Forms.ItemActivation.OneClick;
         this.lsv.Cursor = System.Windows.Forms.Cursors.Default;
         this.lsv.Dock = System.Windows.Forms.DockStyle.Fill;
         this.lsv.HotTracking = true;
         this.lsv.HoverSelection = true;
         listViewItem1.Tag = "http://www.acfun.tv/";
         listViewItem1.ToolTipText = "Acfun.tv";
         listViewItem2.Tag = "http://www.bilibili.tv/";
         listViewItem2.ToolTipText = "bilibili.tv";
         listViewItem3.Tag = "http://www.youku.com/";
         listViewItem3.ToolTipText = "youku.com";
         listViewItem4.Tag = "http://www.imanhua.com";
         listViewItem5.Tag = "http://www.tudou.com/";
         listViewItem6.Tag = "http://tieba.baidu.com/";
         this.lsv.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7});
         this.lsv.Location = new System.Drawing.Point(0, 0);
         this.lsv.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.lsv.Name = "lsv";
         this.lsv.Size = new System.Drawing.Size(492, 302);
         this.lsv.TabIndex = 0;
         this.lsv.UseCompatibleStateImageBehavior = false;
         this.lsv.View = System.Windows.Forms.View.Tile;
         // 
         // il
         // 
         this.il.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
         this.il.ImageSize = new System.Drawing.Size(16, 16);
         this.il.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // WebsitesControl
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.lsv);
         this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.Name = "WebsitesControl";
         this.Size = new System.Drawing.Size(492, 302);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.ListView lsv;
      private System.Windows.Forms.ImageList il;

   }
}
