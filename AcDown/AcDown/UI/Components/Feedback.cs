using System;
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
			webBrowser1.Navigate("http://acplay.loushao.net/");
		}

		private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			try
			{
				var a = webBrowser1.Document.GetElementById("lo");
				if(a!=null)
					a.InnerHtml = @"<a href=""http://acplay.loushao.net/"">点击这里刷新页面</a>(´・ω・｀)";
				var b = webBrowser1.Document.GetElementById("foot");
				if (b != null)
					b.Style = "display:none; height:0px;";
			}
			catch { }
		}
	}
}
