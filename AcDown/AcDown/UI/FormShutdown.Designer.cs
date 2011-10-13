namespace Kaedei.AcDown.UI
{
	partial class FormShutdown
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
         this.lblDescribe = new System.Windows.Forms.Label();
         this.btnCancel = new System.Windows.Forms.Button();
         this.pgbTime = new System.Windows.Forms.ProgressBar();
         this.lblTime = new System.Windows.Forms.Label();
         this.timer = new System.Windows.Forms.Timer(this.components);
         this.SuspendLayout();
         // 
         // lblDescribe
         // 
         this.lblDescribe.AutoSize = true;
         this.lblDescribe.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblDescribe.Location = new System.Drawing.Point(12, 9);
         this.lblDescribe.Name = "lblDescribe";
         this.lblDescribe.Size = new System.Drawing.Size(113, 12);
         this.lblDescribe.TabIndex = 0;
         this.lblDescribe.Text = "系统将在以下时间内";
         // 
         // btnCancel
         // 
         this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.btnCancel.Location = new System.Drawing.Point(239, 104);
         this.btnCancel.Name = "btnCancel";
         this.btnCancel.Size = new System.Drawing.Size(102, 34);
         this.btnCancel.TabIndex = 1;
         this.btnCancel.Text = "取消";
         this.btnCancel.UseVisualStyleBackColor = true;
         this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
         // 
         // pgbTime
         // 
         this.pgbTime.Location = new System.Drawing.Point(12, 63);
         this.pgbTime.Maximum = 30;
         this.pgbTime.Name = "pgbTime";
         this.pgbTime.Size = new System.Drawing.Size(553, 23);
         this.pgbTime.TabIndex = 2;
         // 
         // lblTime
         // 
         this.lblTime.AutoSize = true;
         this.lblTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.lblTime.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
         this.lblTime.Location = new System.Drawing.Point(275, 32);
         this.lblTime.Name = "lblTime";
         this.lblTime.Size = new System.Drawing.Size(38, 28);
         this.lblTime.TabIndex = 3;
         this.lblTime.Text = "30";
         // 
         // timer
         // 
         this.timer.Enabled = true;
         this.timer.Interval = 1000;
         this.timer.Tick += new System.EventHandler(this.timer_Tick);
         // 
         // FormShutdown
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.CancelButton = this.btnCancel;
         this.ClientSize = new System.Drawing.Size(577, 150);
         this.Controls.Add(this.lblTime);
         this.Controls.Add(this.pgbTime);
         this.Controls.Add(this.btnCancel);
         this.Controls.Add(this.lblDescribe);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "FormShutdown";
         this.Opacity = 0.9D;
         this.ShowIcon = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.TopMost = true;
         this.Load += new System.EventHandler(this.FormShutdown_Load);
         this.ResumeLayout(false);
         this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblDescribe;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ProgressBar pgbTime;
		private System.Windows.Forms.Label lblTime;
		private System.Windows.Forms.Timer timer;
	}
}