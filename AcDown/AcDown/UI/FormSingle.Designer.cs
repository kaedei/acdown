namespace Kaedei.AcDown.UI
{
   partial class FormSingle
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
         this.noti = new System.Windows.Forms.NotifyIcon(this.components);
         this.SuspendLayout();
         // 
         // noti
         // 
         this.noti.Text = "AcDown动漫下载器 - 正在运行";
         this.noti.Visible = true;
         // 
         // FormSingle
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(471, 189);
         this.Name = "FormSingle";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "FormCommandLine";
         this.Load += new System.EventHandler(this.FormSingle_Load);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.NotifyIcon noti;
   }
}