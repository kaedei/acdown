using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Kaedei.AcPlay.Redirector
{
	public class BilibiliRedirector : IRedirector
	{

		public ActionResult Redirect(HttpListenerContext context)
		{
			HttpListenerRequest request = context.Request;
			HttpListenerResponse response = context.Response;
			string url = request.RawUrl.ToString();
			//crossdomain.xml
			if (url.Contains("crossdomain.xml"))
			{
				if (url.Contains("letv.com"))
					return response.SendStatusCode(403);
				else
					return response.SendCrossDomainXml();
			}
			//local file
			if (request.QueryString["local"] != null && request.QueryString["local"].Equals("true"))
			{
				return response.SendFile(new FileResponse()
				{
					FilePath = Tools.FromBase64StringForUrl(request.QueryString["file"]),
					BufferSize = 5 * 1024 * 1024 //5MB buffer
				});
			}
			//player page
			if (Regex.IsMatch(url, @"video/av\d+"))
			{
				var dict = new Dictionary<string, string>();
				dict.Add("{PLAYER}", AcPlayConfiguration.Config.PlayerUrl);
				dict.Add("{VIDEOID}", "10000");

				return response.SendHtmlFromResource(new ResourceStringResponse()
					{
						ResourceType = typeof(acplayres),
						ResourceName = "bilibilihtml",
						ReplaceDict = dict
					});
			}
			//flash player
			if (url.Contains("play.swf")
				&& File.Exists(Path.Combine(Application.StartupPath, @"Cache\play.swf")))
			{
				return response.SendFile(new FileResponse()
					{
						FilePath = Path.Combine(Application.StartupPath, @"Cache\play.swf")
					});
			}
			//Pad.xml
			if (url.Contains("pad.xml"))
			{
				return response.SendXmlFromResource(new ResourceStringResponse()
					{
						ResourceType = typeof(acplayres),
						ResourceName = "bilibilipad"
					});
			}
			//comment
			if (Regex.IsMatch(url, @"/dm,\d+") || url.EndsWith("/0.xml") ||
				 Regex.IsMatch(url, @"bilibili\.com(|:\d+)/\d+\.xml"))
			{
				return response.SendComment(new CommentResponse()
					{
						PlayerName = "bilibili",
						CommentNumber = 0,
						SubtitleFiles = new List<string>(AcPlayConfiguration.Config.Subtitles),
						StartupPath = AcPlayConfiguration.Config.StartupPath
					});
			}
			//member
			if (url.Contains("member.bilibili.com/player") || url.Contains("player?"))
			{
				return response.SendXmlFromResource(new ResourceStringResponse()
				{
					ResourceType = typeof(acplayres),
					ResourceName = "bilibilimember"
				});
			}
			//video config
			if (url.Contains("v_play.php?") ||
				 url.Contains("playurl?") ||
				 url.Contains("t_play") ||
				 url.Contains("v_cdn_play?"))
			{
				return response.SendConfig(new ConfigResponse()
					{
						Videos = new List<Video>(AcPlayConfiguration.Config.Videos),
						StartupPath = AcPlayConfiguration.Config.StartupPath,
						ServerUrl = "http://localhost:" + AcPlayConfiguration.Config.ProxyServerPort + "/",
						FrameCount = AcPlayConfiguration.Config.ExtraConfig.ContainsKey("framecount") ? AcPlayConfiguration.Config.ExtraConfig["framecount"] : "",
						Src = AcPlayConfiguration.Config.ExtraConfig.ContainsKey("src") ? AcPlayConfiguration.Config.ExtraConfig["src"] : "",
						TimeLength = AcPlayConfiguration.Config.ExtraConfig.ContainsKey("timelength") ? AcPlayConfiguration.Config.ExtraConfig["timelength"] : ""
					});
			}
			//catalogy
			if (url.Contains("catalogy.json"))
			{
				return response.SendXmlFromResource(new ResourceStringResponse()
				{
					ResourceType = typeof(acplayres),
					ResourceName = "bilibilicatalogy"
				});
			}
			//filter
			if (url.Contains("filter/.json") || Regex.IsMatch(url, @"filter/\d+\.json")) //url.Contains("filter/10000.json"))
			{
				return response.SendXmlFromResource(new ResourceStringResponse()
				{
					ResourceType = typeof(acplayres),
					ResourceName = "bilibilifilter"
				});
			}
			//global.json
			if (url.Contains("global.json"))
			{
				return response.SendXmlFromResource(new ResourceStringResponse()
				{
					ResourceType = typeof(acplayres),
					ResourceName = "bilibiliglobal"
				});
			}
			//msg.xml
			if (url.Contains("msg.xml"))
			{
				return response.SendXmlFromResource(new ResourceStringResponse()
				{
					ResourceType = typeof(acplayres),
					ResourceName = "bilibilimsg"
				});
			}
			if (url.Contains("v_buf"))
			{
				return response.SendStatusCode(200);
			}
			if (url.Contains("dmduration"))
			{
				return response.SendStatusCode(404);
			}
			if (url.Contains("advanceComment"))
			{
				return response.SendXmlFromResource(new ResourceStringResponse()
				{
					ResourceType = typeof(acplayres),
					ResourceName = "bilibiliadvancecomment"
				});
			}
			if (url.Contains("playtag"))
			{
				return response.SendXmlFromResource(new ResourceStringResponse()
				{
					ResourceType = typeof(acplayres),
					ResourceName = "bilibiliplaytag"
				});
			}
			if (url.Contains("rolldate"))
			{
				return response.SendXmlFromResource(new ResourceStringResponse()
				{
					ResourceType = typeof(acplayres),
					ResourceName = "bilibilirolldate"
				});
			}
			if (url.Contains("letv.com/"))
			{
				return response.SendStatusCode(403);
			}
			if (url.Contains("/comment"))
			{
				return response.SendHtml(new HtmlResponse()
					{
						Content = "OK"
					});
			}
			if (url.Contains("/blocklist"))
			{
				return response.SendXml(new XmlResponse()
					{
						Content = "<filter></filter>"
					});
			}
			//Post Comment
			if (url.Contains("dmpost"))
			{
				return response.SendStatusCode(200);
			}

			return ActionResult.NotHandled;

		}
		public string GetUrl()
		{
			return "http://www.bilibili.com/video/av10000/";
		}
	}
}
