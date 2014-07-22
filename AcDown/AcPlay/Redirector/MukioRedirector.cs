using Kaedei.AcPlay.Diagnostics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Kaedei.AcPlay.Redirector
{
	public class MukioRedirector : IRedirector
	{
		public ActionResult Redirect(HttpListenerContext context)
		{
			HttpListenerRequest request = context.Request;
			HttpListenerResponse response = context.Response;
			string url = request.RawUrl.ToString();

			string httpsvr = @"http://localhost:" + AcPlayConfiguration.Config.HttpServerPort.ToString() + "/";
			//crossdomain.xml
			if (url.Contains("crossdomain.xml"))
			{
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
			if (url.Equals(this.GetUrl()))
			{
				var dict = new Dictionary<string, string>();
				dict.Add("{PLAYER}", AcPlayConfiguration.Config.PlayerUrl);
				dict.Add("{VIDEOID}", "10000");

				return response.SendHtmlFromResource(new ResourceStringResponse()
				{
					ResourceType = typeof(MukioRes),
					ResourceName = "Mukiohtml",
					ReplaceDict = dict
				});
			}
			//flash player
			if (url.Contains("mukioplayer.swf"))
			{
				string playerfilename = Path.GetFileNameWithoutExtension(url);
				//check the cache file
				string cache = Path.Combine(Application.StartupPath, @"Cache" + Path.DirectorySeparatorChar + "mukioplayerplus.swf");
				if (File.Exists(cache))
				{
					return response.SendFile(new FileResponse()
					{
						FilePath = cache
					});
				}
				else
				{
					return ActionResult.NotHandled;
				}
			}
			//Flash player skin
			if (url.Contains("skin/") && url.EndsWith(".zip", StringComparison.CurrentCultureIgnoreCase))
			{
				string zipfilename = Path.GetFileName(url);
				string cache = Path.Combine(Application.StartupPath, @"Cache\" + zipfilename);
				if (File.Exists(cache))
				{
					return response.SendFile(new FileResponse()
					{
						FilePath = cache
					});
				}
				else
				{
					return ActionResult.NotHandled;
				}
			}
			//Get Video by id
			if (url.Contains("getVideoByID.aspx?vid="))
			{
				return response.SendXmlFromResource(new ResourceStringResponse()
				{
					ResourceType = typeof(MukioRes),
					ResourceName = "Mukiogetvideobyid"
				});
			}
			//ban list
			if (url.Contains("ban.json"))
			{
				return response.SendXmlFromResource(new ResourceStringResponse()
				{
					ResourceType = typeof(MukioRes),
					ResourceName = "Mukioban"
				});
			}
			if (url.Contains("newad.xml"))
			{
				return response.SendXmlFromResource(new ResourceStringResponse()
				{
					ResourceType = typeof(MukioRes),
					ResourceName = "Mukionewad"
				});
			}
			if (url.Contains("v_play.php?vid=") || url.Contains("t_play") || url.Contains("permanent"))
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
			if (Regex.IsMatch(url, @"comment_on\.xml") || Regex.IsMatch(url, @"/\d+\.xml"))
			{
				return response.SendComment(new CommentResponse()
				{
					PlayerName = "bilibili",
					CommentNumber = 0,
					SubtitleFiles = new List<string>(AcPlayConfiguration.Config.Subtitles),
					StartupPath = AcPlayConfiguration.Config.StartupPath
				});
			}
			if (Regex.IsMatch(url, @"\d+_lock\.json"))//url.Contains("10000_lock.json"))
			{
				return response.SendComment(new CommentResponse()
				{
					PlayerName = "Mukio",
					CommentNumber = 1,
					SubtitleFiles = new List<string>(AcPlayConfiguration.Config.Subtitles),
					StartupPath = AcPlayConfiguration.Config.StartupPath
				});
			}
			if (url.Contains("u.json"))
			{
				return response.SendXmlFromResource(new ResourceStringResponse()
				{
					ResourceType = typeof(MukioRes),
					ResourceName = "Mukiou"
				});
			}
			if (url.Contains("updateVideoByContentID.aspx"))
			{
				return response.SendXmlFromResource(new ResourceStringResponse()
				{
					ResourceType = typeof(MukioRes),
					ResourceName = "Mukioupdatevideobycontentid"
				});
			}
			if (url.Contains("conf.xml"))
			{
				return response.SendXmlFromResource(new ResourceStringResponse()
					{
						ResourceType = typeof(MukioRes),
						ResourceName = "mukioconf"
					});
			}
			if (url.Contains("cnmd.aspx") || url.Contains("/post"))
			{
				var content = new StreamReader(request.InputStream, Encoding.ASCII).ReadToEnd();
				Debug2.WriteLine("REQUEST DATA:" + Environment.NewLine + content);
				response.StatusCode = 200;
				response.Close();
				return ActionResult.Handled;
			}

			return ActionResult.NotHandled;
		}

		public string GetUrl()
		{
			return "http://mukioplayer/";
		}


	}

}
