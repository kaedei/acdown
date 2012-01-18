namespace Kaedei.AcDown.Interface.Forms
{
   partial class FormWebbrowser
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
         this.web = new System.Windows.Forms.WebBrowser();
         this.SuspendLayout();
         // 
         // web
         // 
         this.web.Dock = System.Windows.Forms.DockStyle.Fill;
         this.web.IsWebBrowserContextMenuEnabled = false;
         this.web.Location = new System.Drawing.Point(0, 0);
         this.web.MinimumSize = new System.Drawing.Size(20, 20);
         this.web.Name = "web";
         this.web.ScriptErrorsSuppressed = true;
         this.web.Size = new System.Drawing.Size(790, 448);
         this.web.TabIndex = 0;
         this.web.WebBrowserShortcutsEnabled = false;
         // 
         // FormWebbrowser
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(790, 448);
         this.Controls.Add(this.web);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
         this.Name = "FormWebbrowser";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "FormWebbrowser";
         this.Load += new System.EventHandler(this.FormWebbrowser_Load);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.WebBrowser web;
   }
}