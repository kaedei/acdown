﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using _30edu.Common;
using Kaedei.AcDown.Interface.AcPlay;
using Kaedei.AcDown.Interface.UI;

namespace Kaedei.AcDown.UI.Components
{
	public partial class AcPlayItem : FormBase
	{
		Video v;
		public AcPlayItem(Video video)
		{
			v = video;
			InitializeComponent();
		}

		private void AcPlayItem_Load(object sender, EventArgs e)
		{
			txtFile.Text = v.FileName;
			//TimeSpan ts = new TimeSpan(0, 0, 0, 0, v.Length > 1 ? v.Length : 1);
			TimeSpan ts;
			//获取视频时长
			try
			{
				ts = FlvInfoHelper.GetVideoDuration(v.FileName,
					Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
					"Kaedei" + Path.DirectorySeparatorChar + "FlvCombine" + Path.DirectorySeparatorChar + "ffmpeg.exe"));

			}
			catch
			{
				ts = new TimeSpan(0, 0, 0, 0, 1);
			}

			udMin.Value = ts.Hours * 60 + ts.Minutes;
			udSec.Value = ts.Seconds;
			udMilli.Value = ts.Milliseconds;

		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "视频文件(*.flv;*.mp4;*.hlv;*.f4v)|*.flv;*.mp4;*.hlv;*.f4v";
			ofd.Title = "请选择视频文件";
			ofd.Multiselect = false;
			ofd.InitialDirectory = Path.GetDirectoryName(txtFile.Text);
			if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
			{
				txtFile.Text = ofd.FileName;
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			v.FileName = txtFile.Text;
			v.Length = (int)udMin.Value * 60000 + (int)udSec.Value * 1000 + (int)udMilli.Value;
			this.Close();
		}
	}
}
