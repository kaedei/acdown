using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaedei.AcDown
{
	public partial class FormSubConverter : Form
	{
		public FormSubConverter()
		{
			InitializeComponent();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void lnkDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(@"http://hi.baidu.com/chunqing286/blog");
			this.Close();
		}

		private void FormSubConverter_Load(object sender, EventArgs e)
		{

		}
	}
}
