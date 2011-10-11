namespace Kaedei.AcDown.UI.Components
{
   partial class AcPlay
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
         this.pgb = new System.Windows.Forms.ProgressBar();
         this.lblTip = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // btnStart
         // 
         this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnStart.Location = new System.Drawing.Point(178, 166);
         this.btnStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.btnStart.Name = "btnStart";
         this.btnStart.Size = new System.Drawing.Size(170, 56);
         this.btnStart.TabIndex = 0;
         this.btnStart.Text = "下载AcPlay";
         this.btnStart.UseVisualStyleBackColor = true;
         // 
         // pgb
         // 
         this.pgb.Location = new System.Drawing.Point(42, 74);
         this.pgb.Name = "pgb";
         this.pgb.Size = new System.Drawing.Size(452, 30);
         this.pgb.TabIndex = 1;
         // 
         // lblTip
         // 
         this.lblTip.AutoSize = true;
         this.lblTip.Location = new System.Drawing.Point(59, 37);
         this.lblTip.Name = "lblTip";
         this.lblTip.Size = new System.Drawing.Size(204, 20);
         this.lblTip.TabIndex = 2;
         this.lblTip.Text = "正在下载弹幕播放插件... 请稍候";
         // 
         // AcPlay
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.lblTip);
         this.Controls.Add(this.pgb);
         this.Controls.Add(this.btnStart);
         this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
         this.Name = "AcPlay";
         this.Size = new System.Drawing.Size(531, 303);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button btnStart;
      private System.Windows.Forms.ProgressBar pgb;
      private System.Windows.Forms.Label lblTip;
   }
}
