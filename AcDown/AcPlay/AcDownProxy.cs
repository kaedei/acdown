using System;
using System.Net;

namespace Kaedei.AcPlay
{
	/// <summary>
	/// 代理服务器设置
	/// </summary>
	[Serializable()]
	public class AcDownProxy
	{
		public string Name = "";
		public string Address = "";
		public int Port;
		public string Username = "";
		public string Password = "";
		public WebProxy ToWebProxy()
		{
			WebProxy p = new WebProxy(Address, Port);
			p.Credentials = new NetworkCredential(Username, Password);
			return p;
		}
		public AcDownProxy FromWebProxy(WebProxy proxy)
		{
			if (proxy != null)
			{
				this.Address = proxy.Address == null ? "" : proxy.Address.Host;
				this.Port = proxy.Address.Port;
				this.Username = ((NetworkCredential)proxy.Credentials).UserName;
				this.Password = ((NetworkCredential)proxy.Credentials).Password;
			}
			return this;
		}
	}
}
