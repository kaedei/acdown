using System;
using System.Net;
using System.IO;
using System.Threading;
using System.IO.Compression;
using System.Text;

namespace Kaedei.AcDown.Interface
{
	/// <summary>
	/// 网络相关的静态方法
	/// </summary>
	public class Network
	{
		/// <summary>
		/// 下载文件
		/// </summary>
		/// <param name="para">传递的下载参数</param>
		/// <returns>一个布尔值，指示指定的下载是否已成功完成</returns>
		public static bool DownloadFile(DownloadParameter para)
		{
			return DownloadFile(para, null);
		}

		/// <summary>
		/// 下载文件
		/// </summary>
		/// <param name="para">传递的下载参数</param>
		/// <param name="task">当前下载的任务信息</param>
		/// <returns>一个布尔值，指示指定的下载是否已成功完成</returns>
		public static bool DownloadFile(DownloadParameter para, TaskInfo task)
		{
			//用于限速的Tick
			Int32 privateTick = 0;
			//网络数据包大小 = 1KB
			byte[] buffer = new byte[1024];

			#region 先检查目标网址是否有Location属性

			HttpWebRequest testlocation = (HttpWebRequest)HttpWebRequest.Create(para.Url);
			testlocation.AllowAutoRedirect = false;
			using (WebResponse testresponse = testlocation.GetResponse())
			{
				if (!string.IsNullOrEmpty(testresponse.Headers["Location"]))
					//获得跳转地址
					para.Url = testresponse.Headers["Location"];
			}
			#endregion

			#region 检查文件是否被下载过&是否支持断点续传
			//创建http请求
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(para.Url);
			//设置超时
			request.Timeout = GlobalSettings.GetSettings().NetworkTimeout;
			//设置代理服务器
			if (para.Proxy != null) 
				request.Proxy = para.Proxy;

			//获取服务器回应
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();

				//如果Range超出范围
				//if(((HttpWebResponse)ex.Response).StatusCode != HttpStatusCode.RequestedRangeNotSatisfiable)
				//   throw ex;

			//检查服务器是否支持断点续传
			bool supportrange = false;
			if (response != null)
				supportrange = (response.Headers[HttpResponseHeader.AcceptRanges] == "bytes");

			//设置文件长度和已下载的长度
			//文件长度
			para.TotalLength = response.ContentLength;

			//如果要下载的文件存在
			long filelength=0;
			if (File.Exists(para.FilePath))
			{
				filelength = new FileInfo(para.FilePath).Length;
				//如果文件长度相同
				if (filelength == para.TotalLength)
				{
					//返回下载成功
					return true;
				}
				
				//如果服务器支持断点续传
				if (supportrange)
				{
					//设置"已完成字节数"
					para.DoneBytes = filelength;
					//重新获取服务器回应
					if (response != null)
						response.Close();
					//创建http请求
					request = (HttpWebRequest)HttpWebRequest.Create(para.Url);
					//设置超时
					request.Timeout = GlobalSettings.GetSettings().NetworkTimeout;
					//设置代理服务器
					if (para.Proxy != null)
						request.Proxy = para.Proxy;
					//设置Range
					request.AddRange(int.Parse(filelength.ToString()));
					response = (HttpWebResponse)request.GetResponse();
				}
			}
			else //如果不存在则建立文件夹
			{
				string dir = Directory.GetParent(para.FilePath).ToString();
				if (!Directory.Exists(dir))
					Directory.CreateDirectory(dir);
			}

			#endregion

			
			Stream st, fs; //网络流和文件流
			Stream deflate = null; //Deflate/gzip 解压流
			BufferedStream bs; //缓冲流
			int t, limitcount = 0;
			//确定缓冲长度
			if (para.CacheSize > 256 || para.CacheSize < 1)
				para.CacheSize = 1;
			para.LastTick = System.Environment.TickCount; //系统计数

			//获取下载流
			using (st = response.GetResponseStream())
			{
				//设置gzip/deflate解压缩
				if (response.ContentEncoding == "gzip")
				{
					deflate = new GZipStream(st, CompressionMode.Decompress);
				}
				else if (response.ContentEncoding == "deflate")
				{
					deflate = new DeflateStream(st, CompressionMode.Decompress);
				}
				else
				{
					deflate = st;
				}
				
				//设置FileStream
				if (supportrange && filelength != 0)//若服务器支持断点续传且文件存在
				{
					fs = new FileStream(para.FilePath, FileMode.Open, FileAccess.Write, FileShare.Read, 8);
					fs.Seek(filelength, SeekOrigin.Begin);
				}
				else //服务器不支持断点续传或文件不存在（从头下载）
				{
					fs = new FileStream(para.FilePath, FileMode.Create, FileAccess.Write, FileShare.Read, 8);
				}
				//打开文件流
				using (fs)
				{
					//使用缓冲流
					bs = new BufferedStream(fs, para.CacheSize * 1024);
					
					try
					{
						//读取第一块数据
						Int32 osize = deflate.Read(buffer, 0, buffer.Length);
						//开始循环
						while (osize > 0)
						{
							#region 判断是否取消下载
							//如果用户终止则返回false
							if (para.IsStop)
							{
								//关闭流
								bs.Close();
								st.Close();
								fs.Close();
								return false;
							}
							#endregion

							//增加已完成字节数
							para.DoneBytes += osize;

							//写文件(缓存)
							bs.Write(buffer, 0, osize);


							//设置限速
							int limit = 0;
							if (task != null)
								if (task.SpeedLimit >= 0)
									limit = task.SpeedLimit;

							if (limit > 0)
							{
								//下载计数加一count++
								limitcount++;
								//下载1KB
								osize = deflate.Read(buffer, 0, buffer.Length);
								//累积到limit KB后
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

							else //如果不限速
							{
								osize = deflate.Read(buffer, 0, buffer.Length);
							}
							

						} //end while
					} //end bufferedstream
					catch (Exception ex)
					{
						throw ex;
					}
					finally
					{
						bs.Close();
					}
				}// end filestream
			} //end netstream

			//一切顺利返回true
			return true;
		}

		///// <summary>
		///// 下载字幕文件
		///// </summary>
		//public static bool DownloadSub(DownloadParameter para)
		//{
		//   try
		//   {
		//      //网络缓存(100KB)
		//      byte[] buffer = new byte[102400];
		//      WebRequest Myrq = HttpWebRequest.Create(para.Url);
		//      if (para.Proxy != null)
		//         Myrq.Proxy = para.Proxy;
		//      WebResponse myrp = Myrq.GetResponse();

		//      //获取下载流
		//      Stream st = myrp.GetResponseStream();
		//      if (!para.UseDeflate)
		//      {
		//         //打开文件流
		//         using (Stream so = new FileStream(para.FilePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read, 8))
		//         {
		//            //读取数据
		//            Int32 osize = st.Read(buffer, 0, buffer.Length);
		//            while (osize > 0)
		//            {
		//               //写入数据
		//               so.Write(buffer, 0, osize);
		//               osize = st.Read(buffer, 0, buffer.Length);
		//            }
		//         }
		//      }
		//      else
		//      {
		//         //deflate解压缩
		//         var deflate = new DeflateStream(st, CompressionMode.Decompress);
		//         using (StreamReader reader = new StreamReader(deflate))
		//         {
		//            File.WriteAllText(para.FilePath, reader.ReadToEnd());
		//         }

		//      }
		//      //关闭流
		//      st.Close();
		//      //一切顺利返回true
		//      return true;
		//   }
		//   catch
		//   {
		//      //发生错误返回False
		//      return false;
		//   }
		//}


		/// <summary>
		/// 获取网页源代码(推荐使用的版本)
		/// </summary>
		/// <param name="url"></param>
		/// <param name="encode"></param>
		/// <returns></returns>
		public static string GetHtmlSource(string url,System.Text.Encoding encode)
		{
			return GetHtmlSource(url, encode, null);
		}

		/// <summary>
		/// 获取网页源代码(推荐使用的版本)
		/// </summary>
		/// <param name="url"></param>
		/// <param name="encode"></param>
		/// <returns></returns>
		public static string GetHtmlSource(string url, System.Text.Encoding encode, WebProxy proxy)
		{
			WebClient wc = new WebClient();
			if (proxy != null)
			{
				wc.Proxy = proxy;
			}
			byte[] data = wc.DownloadData(url);
			return encode.GetString(data);
		}

		/// <summary>
		/// 获取网页源代码
		/// </summary>
		/// <param name="request"></param>
		/// <param name="encode"></param>
		/// <returns></returns>
		public static string GetHtmlSource(HttpWebRequest request, System.Text.Encoding encode)
		{
			string sline = "";
			HttpWebResponse res = (HttpWebResponse)request.GetResponse();
			if (res.ContentEncoding == "gzip")
			{
				//Gzip解压缩
				using (GZipStream gzip = new GZipStream(res.GetResponseStream(), CompressionMode.Decompress))
				{
					using (StreamReader reader = new StreamReader(gzip, encode))
					{
						sline = reader.ReadToEnd();
					}
				}
			}
			else if (res.ContentEncoding == "deflate")
			{
				//deflate解压缩
				using (DeflateStream deflate = new DeflateStream(res.GetResponseStream(), CompressionMode.Decompress))
				{
					using (StreamReader reader = new StreamReader(deflate, encode))
					{
						sline = reader.ReadToEnd();
					}
				}
			}
			else
			{
				using (StreamReader reader = new StreamReader(res.GetResponseStream(), encode))
				{
					sline = reader.ReadToEnd();
				}
			}
			return sline;
		}

		/// <summary>
		/// 取得网页源代码
		/// </summary>
		/// <param name="url"></param>
		/// <param name="encode"></param>
		/// <returns></returns>
		public static string GetHtmlSource2(string url, System.Text.Encoding encode, WebProxy proxy)
		{
			string sline = "";
			HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
			if (proxy != null)
				req.Proxy = proxy;
			return GetHtmlSource(req, encode);
		}
	}


	/// <summary>
	/// 下载参数
	/// </summary>
	public class DownloadParameter
	{
		/// <summary>
		/// 资源的网络位置
		/// </summary>
		public string Url { get; set; }
		/// <summary>
		/// 要创建的本地文件位置
		/// </summary>
		public string FilePath { get; set; }
		/// <summary>
		/// 资源长度
		/// </summary>
		public Int64 TotalLength { get; set; }
		/// <summary>
		/// 已完成的字节数
		/// </summary>
		public Int64 DoneBytes { get; set; }
		/// <summary>
		/// 上次Tick的数值
		/// </summary>
		public Int64 LastTick { get; set; }
		/// <summary>
		/// 是否停止下载(可以在下载过程中进行设置，用来控制下载过程的停止)
		/// </summary>
		public bool IsStop { get; set; }
		/// <summary>
		/// 读取或设置下载所使用的缓存大小，范围为1到255，单位为MByte。默认值为1
		/// </summary>
		public int CacheSize { get; set; }
		/// <summary>
		/// 读取或设置使用的代理服务器设置
		/// </summary>
		public WebProxy Proxy { get; set; }
	}

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
				this.Address = proxy.Address.Host;
				this.Port = proxy.Address.Port;
				this.Username = ((NetworkCredential)proxy.Credentials).UserName;
				this.Password = ((NetworkCredential)proxy.Credentials).Password;
			}
			return this;
		}
	}
}
