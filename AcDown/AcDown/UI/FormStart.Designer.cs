namespace Kaedei.AcDown.UI
{
   partial class FormStart
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
			this.picIcon = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// picIcon
			// 
			this.picIcon.Image = global::Kaedei.AcDown.Properties.Resources.Logo;
			this.picIcon.Location = new System.Drawing.Point(13, 14);
			this.picIcon.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.picIcon.Name = "picIcon";
			this.picIcon.Size = new System.Drawing.Size(403, 82);
			this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.picIcon.TabIndex = 0;
			this.picIcon.TabStop = false;
			// 
			// FormStart
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(426, 105);
			this.Controls.Add(this.picIcon);
			this.Cursor = System.Windows.Forms.Cursors.AppStarting;
			this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.Name = "FormStart";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AcDown 正在启动...";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.FormStart_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormStart_Paint);
			((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.PictureBox picIcon;
   }
}