﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.IO.Compression;
using Kaedei.AcFunDowner.Interface;

namespace Kaedei.AcDown.Interface
{
	/// <summary>
	/// 网络相关的静态方法
	/// </summary>
	public static class Network
	{
		/// <summary>
		/// 下载视频文件
		/// </summary>
		public static bool DownloadFile(DownloadParameter para)
		{
			//网络数据包大小 = 1KB
			byte[] buffer = new byte[1024];
			WebRequest Myrq = HttpWebRequest.Create(para.Url);
			WebResponse myrp = Myrq.GetResponse();
			para.TotalLength = myrp.ContentLength; //文件长度
			para.DoneBytes = 0; //完成字节数
			para.LastTick = System.Environment.TickCount; //系统计数
			Stream st, fs; //网络流和文件流
			BufferedStream bs; //缓冲流
			int t, limitcount = 0;
			//确定缓冲长度
			if (GlobalSettings.GetSettings().CacheSizeMb > 256 || GlobalSettings.GetSettings().CacheSizeMb < 1)
				GlobalSettings.GetSettings().CacheSizeMb = 1;
			//获取下载流
			using (st = myrp.GetResponseStream())
			{
				//打开文件流
				using (fs = new FileStream(para.FilePath, FileMode.Create, FileAccess.Write, FileShare.Read, 8))
				{
					//使用缓冲流
					using (bs = new BufferedStream(fs, GlobalSettings.GetSettings().CacheSizeMb * 1024))
					{
						//读取第一块数据
						Int32 osize = st.Read(buffer, 0, buffer.Length);
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
								//删除文件
								if (!GlobalSettings.GetSettings().SaveFileWhenAbort)
									System.IO.File.Delete(para.FilePath);
								return false;
							}
							#endregion

							//增加已完成字节数
							para.DoneBytes += osize;
							
							//写文件(缓存)
							bs.Write(buffer, 0, osize);

							//限速
							if (GlobalSettings.GetSettings().SpeedLimit > 0)
							{

								//下载计数加一count++
								limitcount++;
								//下载1KB
								osize = st.Read(buffer, 0, buffer.Length);
								//累积到limit KB后
								if (limitcount >= GlobalSettings.GetSettings().SpeedLimit)
								{
									t = System.Environment.TickCount - para.LastTick;
									//检查是否大于一秒
									if (t < 1000)		//如果小于一秒则等待至一秒
										Thread.Sleep(1000 - t);
									//重置count和计时器，继续下载
									limitcount = 0;
									para.LastTick = System.Environment.TickCount;
								}
							}
							else //如果不限速
							{
								osize = st.Read(buffer, 0, buffer.Length);
							}

						} //end while
					} //end bufferedstream
				}// end filestream
			} //end netstream

			//一切顺利返回true
			return true;
		}

		/// <summary>
		/// 下载字幕文件
		/// </summary>
		public static bool DownloadSub(DownloadParameter para)
		{
			try
			{
				//网络缓存(100KB)
				byte[] buffer = new byte[102400];
				WebRequest Myrq = HttpWebRequest.Create(para.Url);
				WebResponse myrp = Myrq.GetResponse();

				//获取下载流
				Stream st = myrp.GetResponseStream();
				if (!para.UseDeflate)
				{
					//打开文件流
					using (Stream so = new FileStream(para.FilePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read, 8))
					{
						//读取数据
						Int32 osize = st.Read(buffer, 0, buffer.Length);
						while (osize > 0)
						{
							//写入数据
							so.Write(buffer, 0, osize);
							osize = st.Read(buffer, 0, buffer.Length);
						}
					}
				}
				else
				{
					//deflate解压缩
					var deflate = new DeflateStream(st, CompressionMode.Decompress);
					using (StreamReader reader = new StreamReader(deflate))
					{
						File.WriteAllText(para.FilePath, reader.ReadToEnd());
					}

				}
				//关闭流
				st.Close();
				//一切顺利返回true
				return true;
			}
			catch
			{
				//发生错误返回False
				return false;
			}
		}

		/// <summary>
		/// 取得网页源代码
		/// </summary>
		/// <param name="url"></param>
		/// <param name="encode"></param>
		/// <returns></returns>
		public static string GetHtmlSource(string url, System.Text.Encoding encode)
		{
			string sline = "";
			var req = WebRequest.Create(url);
			var res = req.GetResponse();
			StreamReader strm = new StreamReader(res.GetResponseStream(), encode);
			sline = strm.ReadToEnd();
			strm.Close();
			return sline;
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
		public int LastTick { get; set; }
		/// <summary>
		/// 是否停止下载(可以在下载过程中进行设置，用来控制下载过程的停止)
		/// </summary>
		public bool IsStop { get; set; }
		/// <summary>
		/// 下载时是否使用Deflate解压缩
		/// </summary>
		public bool UseDeflate { get; set; }
	}
}