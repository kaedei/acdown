using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaedei.AcDown.UI.Components
{
	public partial class AcPlayItem : Form
	{
		Video v;
		public AcPlayItem(Video video)
		{
			v = video;
			InitializeComponent();
		}

		private void AcPlayItem_Load(object sender, EventArgs e)
		{
			txtFile.Text = v.Path;
			udMin.Value = v.Length / 60;
			udSec.Value = v.Length % 60;
		}
	}
}
