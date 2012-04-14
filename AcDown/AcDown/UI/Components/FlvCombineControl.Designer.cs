namespace Kaedei.AcDown.UI.Components
{
	partial class FlvCombineControl
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
			this.panelCombine = new System.Windows.Forms.Panel();
			this.btnCombineClear = new System.Windows.Forms.Button();
			this.lstCombine = new System.Windows.Forms.ListBox();
			this.btnCombineDelete = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.txtCombineOutput = new System.Windows.Forms.TextBox();
			this.btnCombineStart = new System.Windows.Forms.Button();
			this.btnCombineChooseOutput = new System.Windows.Forms.Button();
			this.btnCombineAdd = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.panelCombine.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelCombine
			// 
			this.panelCombine.Controls.Add(this.btnCombineClear);
			this.panelCombine.Controls.Add(this.lstCombine);
			this.panelCombine.Controls.Add(this.btnCombineDelete);
			this.panelCombine.Controls.Add(this.label4);
			this.panelCombine.Controls.Add(this.txtCombineOutput);
			this.panelCombine.Controls.Add(this.btnCombineStart);
			this.panelCombine.Controls.Add(this.btnCombineChooseOutput);
			this.panelCombine.Controls.Add(this.btnCombineAdd);
			this.panelCombine.Controls.Add(this.label3);
			this.panelCombine.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelCombine.Location = new System.Drawing.Point(0, 0);
			this.panelCombine.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.panelCombine.Name = "panelCombine";
			this.panelCombine.Size = new System.Drawing.Size(551, 303);
			this.panelCombine.TabIndex = 12;
			// 
			// btnCombineClear
			// 
			this.btnCombineClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCombineClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCombineClear.Location = new System.Drawing.Point(457, 123);
			this.btnCombineClear.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnCombineClear.Name = "btnCombineClear";
			this.btnCombineClear.Size = new System.Drawing.Size(87, 31);
			this.btnCombineClear.TabIndex = 10;
			this.btnCombineClear.Text = "清空";
			this.btnCombineClear.UseVisualStyleBackColor = true;
			this.btnCombineClear.Click += new System.EventHandler(this.btnCombineClear_Click);
			// 
			// lstCombine
			// 
			this.lstCombine.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstCombine.FormattingEnabled = true;
			this.lstCombine.ItemHeight = 20;
			this.lstCombine.Location = new System.Drawing.Point(11, 41);
			this.lstCombine.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.lstCombine.Name = "lstCombine";
			this.lstCombine.ScrollAlwaysVisible = true;
			this.lstCombine.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
			this.lstCombine.Size = new System.Drawing.Size(438, 124);
			this.lstCombine.TabIndex = 2;
			// 
			// btnCombineDelete
			// 
			this.btnCombineDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCombineDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCombineDelete.Location = new System.Drawing.Point(457, 82);
			this.btnCombineDelete.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnCombineDelete.Name = "btnCombineDelete";
			this.btnCombineDelete.Size = new System.Drawing.Size(87, 31);
			this.btnCombineDelete.TabIndex = 9;
			this.btnCombineDelete.Text = "删除";
			this.btnCombineDelete.UseVisualStyleBackColor = true;
			this.btnCombineDelete.Click += new System.EventHandler(this.btnCombineDelete_Click);
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(4, 176);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(65, 20);
			this.label4.TabIndex = 8;
			this.label4.Text = "合并为：";
			// 
			// txtCombineOutput
			// 
			this.txtCombineOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtCombineOutput.Location = new System.Drawing.Point(11, 201);
			this.txtCombineOutput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtCombineOutput.Name = "txtCombineOutput";
			this.txtCombineOutput.ReadOnly = true;
			this.txtCombineOutput.Size = new System.Drawing.Size(416, 26);
			this.txtCombineOutput.TabIndex = 3;
			// 
			// btnCombineStart
			// 
			this.btnCombineStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCombineStart.Enabled = false;
			this.btnCombineStart.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCombineStart.Location = new System.Drawing.Point(192, 237);
			this.btnCombineStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnCombineStart.Name = "btnCombineStart";
			this.btnCombineStart.Size = new System.Drawing.Size(167, 50);
			this.btnCombineStart.TabIndex = 7;
			this.btnCombineStart.Text = "开始合并";
			this.btnCombineStart.UseVisualStyleBackColor = true;
			this.btnCombineStart.Click += new System.EventHandler(this.btnCombineStart_Click);
			// 
			// btnCombineChooseOutput
			// 
			this.btnCombineChooseOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCombineChooseOutput.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCombineChooseOutput.Location = new System.Drawing.Point(435, 200);
			this.btnCombineChooseOutput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnCombineChooseOutput.Name = "btnCombineChooseOutput";
			this.btnCombineChooseOutput.Size = new System.Drawing.Size(109, 29);
			this.btnCombineChooseOutput.TabIndex = 4;
			this.btnCombineChooseOutput.Text = "选择...";
			this.btnCombineChooseOutput.UseVisualStyleBackColor = true;
			this.btnCombineChooseOutput.Click += new System.EventHandler(this.btnCombineChooseOutput_Click);
			// 
			// btnCombineAdd
			// 
			this.btnCombineAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCombineAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCombineAdd.Location = new System.Drawing.Point(457, 41);
			this.btnCombineAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnCombineAdd.Name = "btnCombineAdd";
			this.btnCombineAdd.Size = new System.Drawing.Size(87, 31);
			this.btnCombineAdd.TabIndex = 6;
			this.btnCombineAdd.Text = "添加";
			this.btnCombineAdd.UseVisualStyleBackColor = true;
			this.btnCombineAdd.Click += new System.EventHandler(this.btnCombineAdd_Click_1);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(7, 15);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(224, 20);
			this.label3.TabIndex = 5;
			this.label3.Text = "选取要合并的视频文件(FLV格式)：";
			// 
			// FlvCombineControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.Controls.Add(this.panelCombine);
			this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "FlvCombineControl";
			this.Size = new System.Drawing.Size(551, 303);
			this.panelCombine.ResumeLayout(false);
			this.panelCombine.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelCombine;
		private System.Windows.Forms.ListBox lstCombine;
		private System.Windows.Forms.Button btnCombineDelete;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtCombineOutput;
		private System.Windows.Forms.Button btnCombineStart;
		private System.Windows.Forms.Button btnCombineChooseOutput;
		private System.Windows.Forms.Button btnCombineAdd;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnCombineClear;

	}
}
