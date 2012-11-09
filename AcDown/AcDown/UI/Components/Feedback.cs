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

		class WebBrowserProxy
		{
			struct Struct_INTERNET_PROXY_INFO
			{
				public int dwAccessType;
				public IntPtr proxy;
				public IntPtr proxyBypass;
			};

			[DllImport("wininet.dll", SetLastError = true)]
			static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);

			public void SetProxy(string strProxy)
			{
				const int INTERNET_OPTION_PROXY = 38;
				const int INTERNET_OPEN_TYPE_PROXY = 3;

				Struct_INTERNET_PROXY_INFO struct_IPI;

				// Filling in structure 
				struct_IPI.dwAccessType = INTERNET_OPEN_TYPE_PROXY;
				struct_IPI.proxy = Marshal.StringToHGlobalAnsi(strProxy);
				struct_IPI.proxyBypass = Marshal.StringToHGlobalAnsi("local");

				// Allocating memory 
				IntPtr intptrStruct = Marshal.AllocCoTaskMem(Marshal.SizeOf(struct_IPI));

				// Converting structure to IntPtr 
				Marshal.StructureToPtr(struct_IPI, intptrStruct, true);

				bool iReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_PROXY, intptrStruct, Marshal.SizeOf(struct_IPI));
			}
		}

		private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			try
			{
				var a = webBrowser1.Document.GetElementById("lo");
				if(a!=null)
					a.InnerHtml = @"<a href=""http://acplay.loushao.net/"">点击这里刷新页面</a>(￣︶￣)↗";
				var b = webBrowser1.Document.GetElementById("foot");
				if (b != null)
					b.Style = "display:none; height:0px;";
			}
			catch { }
		}
	}
}
