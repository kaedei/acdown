
using System.Net;
namespace Kaedei.AcPlay.Redirector
{
	public class ActionResult
	{
		public static ActionResult Handled = new ActionResult(true);
		public static ActionResult NotHandled = new ActionResult(false);

		public bool RequestHandled = false;
		public HttpWebRequest HandledRequest = null;

		public ActionResult(bool handled)
		{
			RequestHandled = handled;
		}

		public ActionResult(HttpWebRequest newRequest)
		{
			if (newRequest == null)
				RequestHandled = false;
			else
			{
				RequestHandled = true;
				HandledRequest = newRequest;
			}
		}
	}
}
