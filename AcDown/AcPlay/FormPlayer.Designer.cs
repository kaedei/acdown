namespace Kaedei.AcPlay
{
   partial class FormPlayer
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
			this.web = new System.Windows.Forms.WebBrowser();
			this.panelLoad = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblStatus = new System.Windows.Forms.Label();
			this.tmr = new System.Windows.Forms.Timer(this.components);
			this.panelLoad.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
			this.web.ScrollBarsEnabled = false;
			this.web.Size = new System.Drawing.Size(784, 475);
			this.web.TabIndex = 0;
			this.web.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.web_Navigated);
			// 
			// panelLoad
			// 
			this.panelLoad.BackColor = System.Drawing.Color.White;
			this.panelLoad.Controls.Add(this.pictureBox1);
			this.panelLoad.Controls.Add(this.lblStatus);
			this.panelLoad.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelLoad.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panelLoad.Location = new System.Drawing.Point(0, 0);
			this.panelLoad.Name = "panelLoad";
			this.panelLoad.Size = new System.Drawing.Size(784, 475);
			this.panelLoad.TabIndex = 1;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::Kaedei.AcPlay.Properties.Resources.loading;
			this.pictureBox1.Location = new System.Drawing.Point(370, 198);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(32, 32);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblStatus.Location = new System.Drawing.Point(331, 245);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(122, 25);
			this.lblStatus.TabIndex = 1;
			this.lblStatus.Text = "正在初始化...";
			// 
			// tmr
			// 
			this.tmr.Enabled = true;
			this.tmr.Interval = 1000;
			// 
			// FormPlayer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 475);
			this.Controls.Add(this.panelLoad);
			this.Controls.Add(this.web);
			this.MinimumSize = new System.Drawing.Size(575, 503);
			this.Name = "FormPlayer";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AcPlay弹幕播放器";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPlayer_FormClosing);
			this.Load += new System.EventHandler(this.FormPlayer_Load);
			this.Resize += new System.EventHandler(this.FormPlayer_Resize);
			this.panelLoad.ResumeLayout(false);
			this.panelLoad.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.WebBrowser web;
      private System.Windows.Forms.Panel panelLoad;
      private System.Windows.Forms.Label lblStatus;
      private System.Windows.Forms.Timer tmr;
      private System.Windows.Forms.PictureBox pictureBox1;
   }
}