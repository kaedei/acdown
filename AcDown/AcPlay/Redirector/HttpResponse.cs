using Kaedei.AcPlay.Combiner;
using Kaedei.AcPlay.Formatter;
using Kaedei.AcPlay.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Kaedei.AcPlay.Redirector
{
	public static class HttpResponse
	{
		public static ActionResult SendFile(this HttpListenerResponse response, FileResponse r)
		{
			response.StatusCode = 200;

			//content-type
			response.ContentType = r.ContentType;

			//Range
			response.Headers["Accept-Ranges"] = "bytes";
			long start = r.Start;
			long end = 0;

			if (!string.IsNullOrEmpty(r.Range))
			{
				Match m = Regex.Match(r.Range, @"(?<start>\d+)\-(?<end>(\d+|))");
				start = long.Parse(m.Groups["start"].Value);
				end = long.Parse(m.Groups["end"].Value);
			}

			//Content-Length
			FileInfo info = new FileInfo(r.FilePath);
			long length = info.Length;
			if (start > length) start = 0;
			response.ContentLength64 = length - start;

			//Content-Range
			response.Headers["Content-Range"] = "bytes " + start.ToString() + "-" +
				(length - 1).ToString() + "/" + response.ContentLength64.ToString();


			int t = 0;
			int limitcount = 0;
			int privateTick = 0;
			byte[] buf = new byte[1024];
			//open output stream
			using (var fs = File.OpenRead(r.FilePath))
			{
				fs.Position = start;
				using (var net = response.OutputStream)
				{
					using (var bs = new BufferedStream(net, r.BufferSize))
					{
						while (true)
						{
							int read = 0;
							if (r.SpeedLimit > 0)
							{
								limitcount++;
								read = fs.Read(buf, 0, buf.Length);
								if (limitcount >= r.SpeedLimit)
								{
									t = System.Environment.TickCount - privateTick;
									if (t < 1000)
										Thread.Sleep(1000 - t);

									limitcount = 0;
									privateTick = System.Environment.TickCount;
								}
							}
							else
							{
								read = fs.Read(buf, 0, buf.Length);
							}


							if (read <= 0)
								break;
							bs.Write(buf, 0, read);
						}
					}
					//bs.Flush();
				}

			}//end write

			return ActionResult.Handled;
		}

		public static ActionResult SendXml(this HttpListenerResponse response, XmlResponse r)
		{
			//buffer 100kb
			byte[] buffer = new byte[r.BufferSize];
			//content type
			response.ContentType = "text/xml";
			//string resource
			string content = r.Content;

			//open output stream
			using (var net = response.OutputStream)
			{
				using (Stream st = new MemoryStream(r.Encode.GetBytes(content)))
				{
					while (true)
					{
						int count = st.Read(buffer, 0, buffer.Length);
						if (count <= 0)
							break;
						net.Write(buffer, 0, count);
					}
				}
			}//end write

			return ActionResult.Handled;
		}

		public static ActionResult SendXmlFromResource(this HttpListenerResponse response, ResourceStringResponse r)
		{
			ResourceManager rm = new ResourceManager(r.ResourceType);
			//get string resource
			r.Content = rm.GetString(r.ResourceName);
			return response.SendXml(r);
		}

		public static ActionResult SendCrossDomainXml(this HttpListenerResponse response)
		{
			var r = new XmlResponse()
			{
				Content = 
@"<?xml version=""1.0""?>
<cross-domain-policy>
  <allow-access-from domain=""*"" />
</cross-domain-policy>"
			};
			return response.SendXml(r);
		}

		public static ActionResult SendHtml(this HttpListenerResponse response, HtmlResponse r)
		{
			//Buffer
			byte[] buffer = new byte[r.BufferSize];
			//Content type
			response.ContentType = "text/html";
			//Get string resource
			string content = r.Content;

			if (r.ReplaceDict != null)
			{
				foreach (string key in r.ReplaceDict.Keys)
				{
					content = content.Replace(key, r.ReplaceDict[key]);
				}
			}
			//open output stream
			using (var net = response.OutputStream)
			{
				using (Stream st = new MemoryStream(r.Encode.GetBytes(content)))
				{
					while (true)
					{
						int count = st.Read(buffer, 0, buffer.Length);
						if (count <= 0)
							break;
						net.Write(buffer, 0, count);
					}
				}
			}//end write

			return ActionResult.Handled;
		}

		public static ActionResult SendHtmlFromResource(this HttpListenerResponse response, ResourceStringResponse r)
		{
			ResourceManager rm = new ResourceManager(r.ResourceType);
			//get string resource
			r.Content = rm.GetString(r.ResourceName);
			return response.SendHtml(r);
		}

		public static ActionResult SendConfig(this HttpListenerResponse response, ConfigResponse r)
		{
			//buffer 100kb
			byte[] buffer = new byte[r.BufferSize];
			//content type
			response.ContentType = "text/xml";

			StringBuilder sb = new StringBuilder();
			sb.AppendLine(@"<?xml version=""1.0"" encoding=""UTF-8""?>");
			sb.AppendLine(@"<video>");
			sb.AppendLine(@"	<result>suee</result>");

			//framecount
			string framecount = "";
			//src
			string src = "400";
			//time length
			string timelength = "";
			//calculate totallength
			int total = 0;
			foreach (Video v in r.Videos)
			{
				total += v.Length;
			}
			timelength = total.ToString();

			//get config from file
			if (!string.IsNullOrEmpty(r.TimeLength))
				timelength = r.TimeLength;
			if (!string.IsNullOrEmpty(r.FrameCount))
				framecount = r.FrameCount;
			if (!string.IsNullOrEmpty(r.Src))
				src = r.Src;



			//generate XML file
			sb.AppendLine(@"	<timelength>" + timelength + "</timelength>");
			sb.AppendLine(@"	<framecount>" + framecount + "</framecount>");
			sb.AppendLine(@"	<vname><![CDATA[]]></vname>");
			sb.AppendLine(@"	<src>" + src + "</src>");

			//append every video
			foreach (Video v in r.Videos)
			{
				sb.AppendLine(@"	<durl>");
				sb.AppendLine(@"		<order>" + v.Order.ToString() + "</order>");
				sb.AppendLine(@"		<length>" + v.Length.ToString() + "</length>");
				//generate filename
				if (v.FileName.StartsWith("http://", StringComparison.CurrentCultureIgnoreCase))
				{
					sb.AppendLine(@"		<url><![CDATA[" + v.FileName + "]]></url>");
				}
				else
				{
					string p = Path.IsPathRooted(v.FileName) ? v.FileName : Path.Combine(r.StartupPath, v.FileName);
					p = r.ServerUrl + (r.ServerUrl.EndsWith("/") ? "" : "/")
						+ "?local=true&file=" + Tools.ToBase64StringForUrl(p);
					sb.AppendLine(@"		<url><![CDATA[" + p + "]]></url>");
				}
				sb.AppendLine(@"	</durl>");
			}


			sb.AppendLine(@"	<vtags><![CDATA[..]]></vtags>");
			sb.AppendLine(@"	<ad><![CDATA[]]></ad>");
			sb.AppendLine(@"	<vstr><![CDATA[]]></vstr>");
			sb.AppendLine(@"	<vip><![CDATA[]]></vip>");
			sb.AppendLine(@"	<vround></vround>");
			sb.AppendLine(@"	<pd></pd>");
			sb.AppendLine(@"</video>");

			string doc = sb.ToString();
			//open output stream
			using (var net = response.OutputStream)
			{
				using (Stream st = new MemoryStream(r.Encode.GetBytes(doc)))
				{
					while (true)
					{
						int count = st.Read(buffer, 0, buffer.Length);
						if (count <= 0)
							break;
						net.Write(buffer, 0, count);
					}
				}
			}//end write

			return ActionResult.Handled;
		}

		public static ActionResult SendComment(this HttpListenerResponse response, CommentResponse r)
		{
			//buffer
			byte[] buffer = new byte[r.BufferSize];

			if (r.PlayerName.Trim().Equals("acfun", StringComparison.CurrentCultureIgnoreCase))
			{
				if (r.CommentNumber == 0)
				{
					byte[] bf = r.Encode.GetBytes("[{}]");
					response.OutputStream.Write(bf, 0, bf.Length);
					response.OutputStream.Close();
					return ActionResult.Handled;
				}
			}

			//解析所有弹幕文件
			List<StandardItem> items = new List<StandardItem>();
			foreach (var file in r.SubtitleFiles)
			{
				string filepath;
				if (Path.IsPathRooted(file))
					filepath = file;
				else
					filepath = Path.Combine(r.StartupPath, file);
				if (!File.Exists(filepath)) continue;

				var identifier = new CommentIdentifier();
				string belongto = identifier.Identify(filepath, Encoding.UTF8);
				IFormatter f = null;
				if (belongto.Equals("acfun"))
				{
					f = new AcPlay.Formatter.AcfunFormatter();
				}
				else if (belongto.Equals("bilibili"))
				{
					f = new AcPlay.Formatter.BilibiliFormatter();
				}
				else if (belongto.Equals("doupao"))
				{
					f = new AcPlay.Formatter.DoupaoFormatter();
				}
				if (f != null)
					items.AddRange(f.Format(File.ReadAllText(filepath)));
			}

			//生成合并后的弹幕文件
			string finalCommentFile;
			ICombiner c = null;
			if (r.PlayerName.Equals("acfun", StringComparison.CurrentCultureIgnoreCase))
			{
				c = new AcPlay.Combiner.AcfunCombiner();
			}
			else if (r.PlayerName.Equals("bilibili", StringComparison.CurrentCultureIgnoreCase))
			{
				c = new AcPlay.Combiner.BilibiliCombiner();
			}
			finalCommentFile = c.Combine(items);

			//Content-Length
			//context.Response.ContentLength64 = finalCommentFile.Length;

			//content-type
			switch (r.PlayerName.ToUpper().Trim())
			{
				case "ACFUN":
					response.ContentType = "application/json";
					break;
				case "BILIBILI":
					response.ContentType = "text/xml";
					break;
				default:
					response.ContentType = "text/xml";
					break;
			}


			//open output stream
			using (var net = response.OutputStream)
			{
				//memory stream
				using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(finalCommentFile)))
				{
					while (true)
					{
						int count = ms.Read(buffer, 0, buffer.Length);
						if (count <= 0)
							break;
						net.Write(buffer, 0, count);
					}
				}//end memory stream
			}//end write

			finalCommentFile = null;
			items.Clear();

			return ActionResult.Handled;
		}

		public static ActionResult SendStatusCode(this HttpListenerResponse response, int code)
		{
			try
			{
				response.StatusCode = code;
			}
			catch (Exception)
			{
				response.StatusCode = 500;
			}
			response.Close();
			return ActionResult.Handled;
		}
	}


	public class ResponseBase
	{
		public int BufferSize = 30 * 1024;
	}
	public class FileResponse : ResponseBase
	{
		public string FilePath = "";
		public string ContentType = "application/octet-stream";
		public long Start = 0;
		public int SpeedLimit = 0;
		public string Range = "";
	}

	public class XmlResponse : ResponseBase
	{
		public string Content = "";
		public Encoding Encode = Encoding.UTF8;
	}

	public class HtmlResponse : XmlResponse
	{
		public Dictionary<string, string> ReplaceDict = new Dictionary<string, string>();
	}

	public class ResourceStringResponse : HtmlResponse
	{
		public string ResourceName;
		public Type ResourceType;
	}

	public class ConfigResponse : ResponseBase
	{
		public List<Video> Videos;
		public string StartupPath = "";
		public string ServerUrl = "http://localhost/";
		public string TimeLength = "";
		public string FrameCount = "";
		public string Src = "";
		public Encoding Encode = Encoding.UTF8;
	}

	public class CommentResponse : ResponseBase
	{
		public string PlayerName;
		public int CommentNumber = 0;
		public Encoding Encode = Encoding.UTF8;
		public List<string> SubtitleFiles = new List<string>();
		public string StartupPath = "";
	}


}
