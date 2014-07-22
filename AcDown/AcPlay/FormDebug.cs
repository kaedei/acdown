using System;
using System.Windows.Forms;

namespace Kaedei.AcPlay
{
	public partial class FormDebug : Form
	{
		public FormDebug()
		{
			InitializeComponent();
		}

		public void WriteLine(string text)
		{
			this.Invoke(new MethodInvoker(() => textBox1.AppendText(text + Environment.NewLine)));
		}
		private void FormDebug_Load(object sender, EventArgs e)
		{

		}
	}
}
