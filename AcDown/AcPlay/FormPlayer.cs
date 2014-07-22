using Kaedei.AcPlay.Proxy;
using Kaedei.AcPlay.Proxy.IEHelper;
using Kaedei.AcPlay.Redirector;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Kaedei.AcPlay
{
	public partial class FormPlayer : Form
	{

		//IE代理
		IEProxy ie;
		//代理服务器
		AcplayHttpProxy proxy = null;
		private IRedirector redirector = null;

		public FormPlayer()
		{
			InitializeComponent();
		}

		private void FormPlayer_Load(object sender, EventArgs e)
		{
			//调整窗口大小
			switch(AcPlayConfiguration.Config.PlayerName.ToUpper())
			{
				case "ACFUN":
					this.ClientSize = new System.Drawing.Size(970, 471);
					redirector = new AcfunRedirector();
					break;
				case "BILIBILI":
					this.ClientSize = new System.Drawing.Size(970, 514);
					redirector = new BilibiliRedirector();
					break;
				case "MUKIOPLAYER":
					redirector = new MukioRedirector();
					break;
			}

			Thread thProxy = new Thread(new ThreadStart(new MethodInvoker(() =>
				{
					proxy = new AcplayHttpProxy("http://127.0.0.1:" + AcPlayConfiguration.Config.ProxyServerPort.ToString() + "/", redirector);
					proxy.StartProxy();
				})));

			Thread wait = new Thread(new ThreadStart(new MethodInvoker(() =>
				{					
					while (!ServerStartStatus.ProxyServerStarted)
					{
						Thread.Sleep(500);
					}

					this.Invoke(new MethodInvoker(() =>
					{
						Application.DoEvents();
						ie = new IEProxy("127.0.0.1:" + AcPlayConfiguration.Config.ProxyServerPort.ToString());
						ie.RefreshIESettings();
						Application.DoEvents();
						Thread.Sleep(1000);
						web.Navigate(redirector.GetUrl());
					}));
				})));
			thProxy.IsBackground = true;
			thProxy.Start();
			wait.Start();
			this.Icon = Properties.Resources.acplay;
		}

		private void web_Navigated(object sender, WebBrowserNavigatedEventArgs e)
		{
			panelLoad.Visible = false;
		}


		private void FormPlayer_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (proxy != null)
				proxy.Stop();
		}

		private void FormPlayer_Resize(object sender, EventArgs e)
		{
#if DEBUG
				this.Text = this.ClientSize.ToString();
#endif

		}


	}
}
