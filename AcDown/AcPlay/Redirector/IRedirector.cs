using System.Net;

namespace Kaedei.AcPlay.Redirector
{
	public interface IRedirector
	{
		ActionResult Redirect(HttpListenerContext context);
		string GetUrl();
	}
}