namespace Kaedei.AcDown.UI
{
	partial class FormPlugins
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
			this.pluginSettings1 = new Kaedei.AcDown.UI.Components.PluginSettings();
			this.SuspendLayout();
			// 
			// pluginSettings1
			// 
			this.pluginSettings1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pluginSettings1.Location = new System.Drawing.Point(0, 0);
			this.pluginSettings1.Name = "pluginSettings1";
			this.pluginSettings1.Size = new System.Drawing.Size(503, 366);
			this.pluginSettings1.TabIndex = 0;
			// 
			// FormPlugins
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(503, 366);
			this.Controls.Add(this.pluginSettings1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPlugins";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AcDown插件";
			this.ResumeLayout(false);

		}

		#endregion

		private Components.PluginSettings pluginSettings1;


	}
}