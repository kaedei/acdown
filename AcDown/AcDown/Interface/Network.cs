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
			//网络流
			Stream st;
			//文件流
			Stream fs;
			//Deflate/gzip 解压流
			Stream decompressStream = null;
			//缓冲流
			BufferedStream bs;
			//服务器是否支持range
			bool supportrange = false;
			//是否启用断点续传
			bool enableResume = false;
			//提取缓存
			bool extractcache = false;
			if (task != null)
				extractcache = para.ExtractCache;
			//修正代理服务器
			if (para.Proxy == null)
				para.Proxy = new WebProxy();

			//初始化重试管理器
			bool needRedownload = false; //需要重试下载
			int remainRetryTime = GlobalSettings.GetSettings().RetryTimes; //剩余的重试次数

			//允许重试时才进行循环
			do
			{
				//Http请求
				HttpWebRequest request;
				//服务器回应
				HttpWebResponse response;

				#region 获取多次跳转后的真实地址

				bool needRedirect = false; //是否需要继续获取Location值(重定向)
				do
				{
					//创建http请求
					request = (HttpWebRequest)HttpWebRequest.Create(para.Url);
					//设置超时
					request.Timeout = GlobalSettings.GetSettings().NetworkTimeout;
					//设置代理服务器
					if (para.Proxy != null)
						request.Proxy = para.Proxy;
					request.AllowAutoRedirect = false;
					//获取服务器回应
					response = (HttpWebResponse)request.GetResponse();
					if (!string.IsNullOrEmpty(response.Headers["Location"]))
					{
						para.Url = response.Headers["Location"];
						needRedirect = true;
					}
					else
					{
						needRedirect = false;
					}
				} while (needRedirect);  //重新获取服务器回应

				#endregion

				#region 检查文件是否被下载过&是否支持断点续传

				//检查服务器是否支持断点续传
				if (response != null)
					supportrange = (response.Headers[HttpResponseHeader.AcceptRanges] == "bytes");

				//设置文件长度和已下载的长度
				//文件长度
				para.TotalLength = response.ContentLength;

				#region 检查系统缓存
				try
				{
					if (extractcache && para.TotalLength > 0) //如果允许提取缓存且文件长度大于0时
					{
						//获取temp文件夹
						string tempfolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Temp\");
						//获取internet cache文件夹
						string internettemp = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
						//查找到的文件名称
						string filename = null;
						//查找文件
						//internet cache文件夹
						string[] files = Directory.GetFiles(internettemp, para.ExtractCachePattern, SearchOption.AllDirectories);
						foreach (var file in files)
						{
							FileInfo fi = new FileInfo(file);
							if (fi.Length == para.TotalLength)
							{
								filename = file;
								break;
							}
						}
						if (String.IsNullOrEmpty(filename)) //系统temp文件夹
						{
							files = Directory.GetFiles(tempfolder, para.ExtractCachePattern, SearchOption.AllDirectories);
							foreach (var file in files)
							{
								FileInfo fi = new FileInfo(file);
								if (fi.Length == para.TotalLength)
								{
									filename = file;
									break;
								}
							}
						}
						//释放空间
						files = null;

						//如果找到文件则直接复制
						if (!String.IsNullOrEmpty(filename))
						{
							para.DoneBytes = para.TotalLength;
							File.Copy(filename, para.FilePath);
							//不需要继续下载
							needRedownload = false;
							//返回下载成功
							return true;
						}
					}
				}
				catch
				{
					//如果复制过程中出错则继续下载
					para.DoneBytes = 0;
					needRedownload = true;
				}
				#endregion

				//建立文件夹
				string dir = Directory.GetParent(para.FilePath).ToString();
				if (!Directory.Exists(dir))
					Directory.CreateDirectory(dir);

				//如果要下载的文件存在
				long filelength = 0;
				if (File.Exists(para.FilePath))
				{
					filelength = new FileInfo(para.FilePath).Length;
					if (filelength > 0)
					{
						//如果文件长度相同
						if (filelength == para.TotalLength)
						{
							//返回下载成功
							return true;
						}
						//如果【已有文件长度小于网络文件总长度】且【服务器支持断点续传】才启用断点续传功能
						enableResume = (filelength < para.TotalLength) && supportrange;

						//如果服务器支持断点续传
						if (enableResume)
						{
							//重新获取服务器回应
							if (response != null)
								response.Close();
							//创建http请求
							var newrequest = (HttpWebRequest)HttpWebRequest.Create(para.Url);
							//设置超时
							newrequest.Timeout = GlobalSettings.GetSettings().NetworkTimeout;
							//设置代理服务器
							if (para.Proxy != null)
								newrequest.Proxy = para.Proxy;
							//设置Range
							newrequest.AddRange(int.Parse(filelength.ToString()));
							var newresponse = (HttpWebResponse)newrequest.GetResponse();
							//检测服务器是否存在欺诈（宣称支持断点续传且返回200 OK，但是内容为报错信息。经常出现在新浪视频服务器的返回信息中）
							//判定为欺诈的条件为：返回的长度小于剩余(未下载的)文件长度的90%
							if (newresponse.ContentLength < (para.TotalLength - filelength) * 9 / 10)
							{
								//重新获取文件
								response = (HttpWebResponse)request.GetResponse();
								//服务器不支持断点续传
								enableResume = false;
								//设置"已完成字节数"
								para.DoneBytes = 0;
							}
							else
							{
								//服务器支持断点续传
								para.DoneBytes = filelength;
								//设置新连接
								response = newresponse;
							}
						}
					}
				}

				#endregion


				int t, limitcount = 0;
				//系统计数
				para.LastTick = System.Environment.TickCount;

				//获取下载流
				using (st = response.GetResponseStream())
				{
					//设置gzip/deflate解压缩
					if (response.ContentEncoding == "gzip")
					{
						decompressStream = new GZipStream(st, CompressionMode.Decompress);
					}
					else if (response.ContentEncoding == "deflate")
					{
						decompressStream = new DeflateStream(st, CompressionMode.Decompress);
					}
					else
					{
						decompressStream = st;
					}

					//设置FileStream
					if (enableResume)//若允许断点续传
					{
						fs = new FileStream(para.FilePath, FileMode.Open, FileAccess.Write, FileShare.Read, 8);
						fs.Seek(filelength, SeekOrigin.Begin);
					}
					else //不允许断点续传
					{
						fs = new FileStream(para.FilePath, FileMode.Create, FileAccess.Write, FileShare.Read, 8);
					}
					//打开文件流
					using (fs)
					{
						//使用缓冲流
						bs = new BufferedStream(fs, GlobalSettings.GetSettings().CacheSize * 1024 * 1024);

						try
						{
							//读取第一块数据
							Int32 osize = decompressStream.Read(buffer, 0, buffer.Length);
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
									osize = decompressStream.Read(buffer, 0, buffer.Length);
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
									osize = decompressStream.Read(buffer, 0, buffer.Length);
								}

							} //end while

							//如果下载完成的数据没有到达服务器宣称的长度的90%就报告错误
							if (para.TotalLength > 0)
								if (para.DoneBytes < (para.TotalLength * 9 / 10))
									throw new Exception("Data downloaded is less than the server announced.");

							//下载成功完成，不需要重新下载
							needRedownload = false;
						} //end bufferedstream
						catch (Exception ex)
						{
							//可重试次数减1
							remainRetryTime--;
							//不再重试直接抛出异常的规则：
							//1.没有可重试次数
							//2.服务器不支持断点续传
							if (remainRetryTime < 0 || (!enableResume))
							{
								needRedownload = false;
								throw ex;
							}
							else //否则继续重试
							{
								needRedownload = true;
								//重试前等待的时间
								Thread.Sleep(GlobalSettings.GetSettings().RetryWaitingTime);
							}
						}
						finally
						{
							bs.Close();
						}
					}// end filestream
				} //end netstream
			} while (needRedownload); //end while(needReDownload)
			//一切顺利返回true
			return true;

		}

		/// <summary>
		/// 获取网页源代码
		/// </summary>
		/// <param name="url"></param>
		/// <param name="encode"></param>
		/// <returns></returns>
		public static string GetHtmlSource(string url, System.Text.Encoding encode)
		{
			return GetHtmlSource(url, encode, new WebProxy());
		}

		///// <summary>
		///// 获取网页源代码(推荐使用的版本)
		///// </summary>
		///// <param name="url"></param>
		///// <param name="encode"></param>
		///// <returns></returns>
		//public static string GetHtmlSource(string url, System.Text.Encoding encode, WebProxy proxy)
		//{
		//   WebClient wc = new WebClient();
		//   if (proxy != null)
		//   {
		//      wc.Proxy = proxy;
		//   }
		//   byte[] data = wc.DownloadData(url);
		//   return encode.GetString(data);
		//}

		/// <summary>
		/// 获取网页源代码
		/// </summary>
		/// <param name="request"></param>
		/// <param name="encode"></param>
		/// <returns></returns>
		public static string GetHtmlSource(HttpWebRequest request, System.Text.Encoding encode)
		{
			string sline = "";
			bool needRedownload = false;
			int remainTimes = GlobalSettings.GetSettings().RetryTimes;
			//当需要重试下载时
			do
			{
				try
				{
					//获取服务器回应
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
				}
				catch (Exception ex) //发生错误
				{
					//重试次数-1
					remainTimes--;
					//如果重试次数小于0，抛出错误
					if (remainTimes < 0)
					{
						needRedownload = false;
						throw ex;
					}
					else
					{
						//等待时间
						Thread.Sleep(GlobalSettings.GetSettings().RetryWaitingTime);
						needRedownload = true;
					}
				}
			} while (needRedownload);
			return sline;
		}

		/// <summary>
		/// 取得网页源代码
		/// </summary>
		/// <param name="url"></param>
		/// <param name="encode"></param>
		/// <returns></returns>
		public static string GetHtmlSource(string url, System.Text.Encoding encode, WebProxy proxy)
		{
			HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
			if (proxy == null)
				req.Proxy = new WebProxy();
			else
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
		/// <summary>
		/// 读取或设置一个值，指示下载时是否直接提取系统缓存
		/// </summary>
		public bool ExtractCache { get; set; }
		/// <summary>
		/// 读取或设置一个值，指示提取缓存时所使用的文件名过滤器
		/// </summary>
		public string ExtractCachePattern { get; set; }
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
