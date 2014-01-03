using System;
using System.Globalization;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace Kaedei.AcDown.UI.Components
{
	public partial class Feedback : UserControl
	{
		public Feedback()
		{
			InitializeComponent();
		}

		public void SetProxy(string host, int port)
		{
			port = port > 0 ? port : 80;
			host = string.IsNullOrEmpty(host) ? "127.0.0.1" : host.Trim();
			Application.DoEvents();
			var ie = new IEProxy("http://" + host + ":" + port.ToString());
			ie.RefreshIESettings();
			Application.DoEvents();
		}

		public void Navigate()
		{
			webBrowser1.Navigate("http://acdown.acplay.net/?ver=" + Application.ProductVersion);
		}

		private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			
		}
	}
}
