using Kaedei.AcPlay.Diagnostics;
using Kaedei.AcPlay.Redirector;
using System;
using System.IO;
using System.Net;
using System.Threading;

namespace Kaedei.AcPlay.Proxy
{
	public class AcplayHttpProxy
	{
		private IRedirector redirector;
		private string uri;
		public AcplayHttpProxy(string uriPrefix, IRedirector r)
		{
			uri = uriPrefix;
			redirector = r;
		}

		private bool isstop = false;
		/// <summary>
		/// Start HttpServer
		/// </summary>
		public void StartProxy()
		{
			HttpListener listener = new HttpListener();
			listener.Prefixes.Add(uri);
			listener.Start();
			ServerStartStatus.ProxyServerStarted = true;

			while (!isstop)
			{
				Debug2.WriteLine("<<<Proxy Listening at " + AcPlayConfiguration.Config.ProxyServerPort.ToString() + ">>>");
				HttpListenerContext context = listener.GetContext();
				ThreadPool.QueueUserWorkItem(new WaitCallback(AcceptConnect), context);
			}

		}

		private void AcceptConnect(object obj)
		{
			string ID = "[" + new Random().Next(1000, 1999).ToString() + "] ";

			try
			{
				HttpListenerContext context = (HttpListenerContext)obj;

				string requestString = context.Request.RawUrl;
				Debug2.WriteLine(ID + "Got request for " + requestString);

				var result = redirector.Redirect(context);

				if (result.RequestHandled) return;

				//get response
				HttpWebRequest request =null;
				if (result.HandledRequest == null)
				{
					request = (HttpWebRequest)WebRequest.Create(context.Request.RawUrl);
					foreach (string key in context.Request.Headers.Keys)
					{
						try
						{
							if (key.Equals("Host", StringComparison.CurrentCultureIgnoreCase)) continue;
							if (key.Equals("Content-Length", StringComparison.CurrentCultureIgnoreCase)) continue;
							request.Headers.Add(key, context.Request.Headers[key]);
						}
						catch { }
					}
				}
				else
				{
					request = result.HandledRequest;
				}

				request.Proxy = AcPlayConfiguration.Config.WebProxy;
				request.KeepAlive = false;

				Debug2.WriteLine(ID + "Got back response from " + request.RequestUri);


				using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
				{
					foreach (string key in response.Headers)
					{
						try
						{
							context.Response.AppendHeader(key, response.Headers[key]);
						}
						catch { }
					}
					using (Stream receiveStream = response.GetResponseStream())
					{
						HttpListenerResponse responseOut = context.Response;

						// Need to get the length of the response before it can be forwarded on
						//responseOut.ContentLength64 = response.ContentLength;
						int bytesCopied = CopyStream(receiveStream, responseOut.OutputStream);
						responseOut.OutputStream.Close();
						Debug2.WriteLine(ID + "Copied " + bytesCopied.ToString() + " bytes");
					}
				}


			}
			catch (WebException e)
			{
				Debug2.WriteLine(ID + "\nWeb Exception raised!");
				Debug2.WriteLine(ID + "Message: " + e.Message);
				Debug2.WriteLine(ID + "Status: " + e.Status.ToString() + "\r\n");
			}
			catch (Exception e)
			{
				Debug2.WriteLine(ID + "\nMain Exception raised!");
				Debug2.WriteLine(ID + "Source : " + e.Source);
				Debug2.WriteLine(ID + "Message : " + e.Message + "\r\n");
			}



		}
		int limit = 0;
		int t = 0;
		int limitcount = 0;
		int privateTick = 0;
		private int CopyStream2(Stream input, Stream output)
		{
			byte[] buffer = new byte[1024];
			var bs = new BufferedStream(output, 65536);
			int bytesWritten = 0;
			while (true)
			{
				int read = 0;
				if (limit > 0)
				{
					limitcount++;
					read = input.Read(buffer, 0, buffer.Length);
					if (limitcount >= limit)
					{
						t = System.Environment.TickCount - privateTick;
						//检查是否大于一秒
						if (t < 1000)		//如果小于一秒则等待至一秒
							Thread.Sleep(1000 - t);
						//重置count和计时器，继续下载
						limitcount = 0;
						privateTick = System.Environment.TickCount;
					}
				}
				else
				{
					read = input.Read(buffer, 0, buffer.Length);
				}
				if (read <= 0)
					break;
				bs.Write(buffer, 0, read);
				bytesWritten += read;
			}
			bs.Flush();
			bs.Close();
			return bytesWritten;
		}


		private int CopyStream(Stream input, Stream output)
		{
			byte[] buffer = new byte[32768];
			int bytesWritten = 0;
			while (true)
			{
				int read = input.Read(buffer, 0, buffer.Length);
				if (read <= 0)
					break;
				output.Write(buffer, 0, read);
				bytesWritten += read;
			}
			return bytesWritten;
		}

		public void Stop()
		{
			isstop = true;
		}
	}
}
