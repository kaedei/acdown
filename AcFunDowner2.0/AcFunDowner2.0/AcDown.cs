/*AcDown.cs
 * 
 * class AcDowner：
 * 分析/下载AcFun视频/弹幕的类
 * 
 * class DelegateContainer:
 * 包装主程序委托的类
 * 
 * 最后更新：2010-9-28
 * 
 * Copyright 2010 Kaedei Software

	Licensed under the Apache License, Version 2.0 (the "License");
	you may not use this file except in compliance with the License.
	You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	Unless required by applicable law or agreed to in writing, software
	distributed under the License is distributed on an "AS IS" BASIS,
	WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	See the License for the specific language governing permissions and
	limitations under the License.
 * 
 * http://blog.sina.com.cn/kaedei
 * mailto:kaedei@foxmail.com
 * 
 */

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using Kaedei.AcFunDowner;
using System.Text;
using System.Threading;
using System.IO.Compression;
using Kaedei.Lavola;

namespace AcFunDownerLibrary
{
	 public class AcDowner : Kaedei.Lavola.IDownloader
	 {
		  /// <summary>
		  /// 委托
		  /// </summary>
		  public DelegateContainer delegates { get; set; }
		  public string PartName { set; get; }
		  public Guid TaskId { set; get; }
		  public bool Bili; //是否为BiliBili视频

		  public AcDowner(DelegateContainer delegates,string url,string partName,Guid taskId)
		  {
			  delegates = delegates;
			  PartName = partName;
			  TaskId = taskId;
			  Url = url;
			  Status = DownloadStatus.等待开始;
		  }

		  /// <summary>
		  /// 下载地址
		  /// </summary>
		  public string Url { get; set; }
		  /// <summary>
		  /// 视频信息
		  /// </summary>
		  public SinaVideo Info { get; set; }
		 /// <summary>
		 /// 总的分段数量
		 /// </summary>
		  public Int32 PartCount { get { return Info.durl.Length; } }
		 /// <summary>
		 /// 视频文件路径
		 /// </summary>
		  public string FilePathString { get; set; }
		 /// <summary>
		 /// 字幕文件路径
		 /// </summary>
		  public string SubfilePathString { get; set; }
		 /// <summary>
		  /// 下载进度
		 /// </summary>
		  public double DownloadProcess;
		  /// <summary>
		  /// 下载状态
		  /// </summary>
		  public DownloadStatus Status { get; set; }
		 /// <summary>
		 /// 视频标题
		 /// </summary>
		  public string VideoTitle { get { return vTitle; } }
		 /// <summary>
		 /// 
		 /// </summary>
		  public int SpeedLimit = 0;
		  //视频ID
		  private string vId = "";
		  //视频标题
		  private string vTitle = "未知";

		 /// <summary>
		 /// 停止下载
		 /// </summary>
		  public void StopDownload()
		  {
			  this.Status = DownloadStatus.已经停止;
		  }
		 /// <summary>
		 /// 开始下载
		 /// </summary>
		  public void Run()
		  {
			  //下载结果
			  bool result = false;
			  try
			  {
				  this.Status = DownloadStatus.正在下载;
				  //开始下载
				  //dele.Start.Invoke(new ParaStart(TaskId));
				  //dele.TipText(new ParaTipText(TaskId,"正在初始化"));

				  if (string.IsNullOrEmpty(Url))
					  throw new Exception("URL设置为空");

				  //源文件
				  string s = "";
				  
				  //判断是否为bilibili
				  Bili = IsBili(Url);

				  //取得网页源代码
				  s = GetSource(Url);

				  //取得视频文件ID
				  if (Bili)
					  vId = GetVideoIdB(s);
				  else
					  vId = GetVideoId(s);
				  

				  //取得视频文件标题
				  vTitle = GetVideoTitle(s);
				  if (!string.IsNullOrEmpty(PartName))
					  vTitle = string.Concat(vTitle, @"-【", PartName, @"】");

				  //判断是否为PlayerF类型            
				  string playerF = CheckPlayerF(s);
				  if (!string.IsNullOrEmpty(playerF))
				  {
					  //如果是则直接下载
					  //肯定只有一个分段
					  delegates.NewPart(new ParaNewPart(TaskId, 1));
					  result = Download(playerF);
				  }
				  else
				  {
					  //取得视频信息
					  Info = GetVideoInfo(vId);
					  //如果只有一个分段
					  if (Info.durl.Length == 1)
					  {
						  delegates.NewPart(new ParaNewPart(TaskId, 1));
						  result = Download(Info.durl[0].url);
					  }
					  //如果有很多分段
					  else
					  {
						  result = DownloadForParts();
					  }
				  }

				  //下载字幕
				  if (Config.setting.DownSub)
				  {
					  //如果是用户手动终止（或程序退出）
					  if (this.Status != DownloadStatus.已经停止)
					  {
						  bool sub = DownloadSub(vId, vTitle);
						  if (!sub)
							  Logging.Add(new Exception("字幕下载失败：" + vTitle));
					  }
				  }

			  }
			  catch(Exception ex) //如果下载过程出错
			  {
				  //显示为错误
				  delegates.Error(new ParaError(TaskId, ex));
				  return;
			  }

				//完成下载
				delegates.Finish(new ParaFinish(TaskId, result ? true : false));
				
		  }

		  /// <summary>
		  /// 开始下载
		  /// </summary>
		  /// <param name="url"></param>
		  private bool  Download(string url)
		  {
				return Download(url, vTitle);
		  }

		 /// <summary>
		 /// 下载多段的视频
		 /// </summary>
		 /// <returns></returns>
		  private bool DownloadForParts()
		  {
				for (int i = 0; i < Info.durl.Length; i++)
				{
					 //通知更换Part
					 delegates.NewPart.Invoke(new ParaNewPart(TaskId, i + 1));
					 bool success; 
					//下载此Part
					 success = Download(Info.durl[i].url, 
											vTitle + " - Part" +	i.ToString() +
											(Info.durl[i].url.EndsWith(".mp4",StringComparison.CurrentCultureIgnoreCase) ? ".mp4" : ".flv")
											);
					 if (!success)
						 return false;
				}
				return true;

		  }

		  public long TotalBytes { get; set; }
		  public long DoneBytes { get; set; }
		  public long LastBytes { get; set; }
		  private int lastTick ;

		 /// <summary>
		 /// 下载视频文件
		 /// </summary>
		 /// <param name="url"></param>
		 /// <param name="fileName"></param>
		 /// <returns></returns>
		  private bool Download(string url, string fileName)
		  {
				//网络数据包大小 = 1KB
				byte[] buffer = new byte[1024];
				WebRequest  Myrq = HttpWebRequest.Create(url);
				WebResponse  myrp = Myrq.GetResponse(); 
				TotalBytes = myrp.ContentLength; //文件长度
				DoneBytes = 0; //完成字节数
				lastTick = System.Environment.TickCount; //系统计数
				Stream st, fs; //网络流和文件流
				BufferedStream bs; //缓冲流
				int t, limitcount=0;
				//确定缓冲长度
				if (Config.setting.CacheSize > 16 || Config.setting.CacheSize < 1)
					Config.setting.CacheSize = 1;
				//获取下载流
				using (st = myrp.GetResponseStream())
				{
					//文件保存路径
					FilePathString = Path.Combine(
												 Config.setting.SavePath,
												 fileName + ((fileName.EndsWith(".flv", StringComparison.CurrentCultureIgnoreCase) || fileName.EndsWith(".mp4", StringComparison.CurrentCultureIgnoreCase)) ? "" : ".flv"));

					//打开文件流
					using (fs = new FileStream(FilePathString, FileMode.Create, FileAccess.Write, FileShare.Read, 8))
					{
						//使用缓冲流
						using (bs = new BufferedStream(fs, Config.setting.CacheSize * 1024))
						{
							//读取第一块数据
							Int32 osize = st.Read(buffer, 0, buffer.Length);
							//开始循环
							while (osize > 0)
							{
								#region 判断是否取消下载
								//如果用户终止则返回false
								if (this.Status == DownloadStatus.已经停止)
								{
									//关闭流
									bs.Close();
									st.Close();
									fs.Close();
									//删除文件
									if (!Config.setting.SaveWhenAbort)
										System.IO.File.Delete(FilePathString);
									return false;
								}
								#endregion

								//增加已完成字节数
								DoneBytes += osize;
								//设置下载百分比数（0.1234）
								DownloadProcess = (double)DoneBytes / (double)TotalBytes;
								//调用委托会加大CPU和内存开销
								//dele.TipProcess(new ParaTipProcess(TaskId, (double)doneBytes / (double)totalBytes));
								//写文件(缓存)
								bs.Write(buffer, 0, osize);
	
								//限速
								if (SpeedLimit > 0)
								{
									
									//下载计数加一count++
									limitcount++;
									//下载1KB
									osize = st.Read(buffer, 0, buffer.Length);
									//累积到limit KB后
									if (limitcount >= SpeedLimit)
									{
										t = System.Environment.TickCount - lastTick;
										//检查是否大于一秒
										if (t < 1000)		//如果小于一秒则等待至一秒
											Thread.Sleep(1000 - t);
										//重置count和计时器，继续下载
										limitcount = 0;
										lastTick = System.Environment.TickCount;
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
		 /// <param name="id"></param>
		 /// <param name="title"></param>
		 /// <returns></returns>
		  public bool DownloadSub(string id, string title)
		  {
			  try
			  {
				  string suburl;
				  //字幕url
				  if (Bili) //如果是bb字幕
					  suburl = GetSubUrlB(id);
				  else //acfun
					  suburl = GetSubUrl(id);

				  SubfilePathString = Path.Combine(Config.setting.SavePath, title + ".xml"); //字幕下载的位置
				  
				  //网络缓存(100KB)
				  byte[] buffer = new byte[102400];
				  WebRequest Myrq = HttpWebRequest.Create(suburl);
				  WebResponse myrp = Myrq.GetResponse();

				  //获取下载流
				  Stream st = myrp.GetResponseStream();
					if (!Bili)
				  {
					  //打开文件流
					  using (Stream so = new FileStream(SubfilePathString, FileMode.Create, FileAccess.ReadWrite, FileShare.Read, 8))
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
						  File.WriteAllText(SubfilePathString, reader.ReadToEnd());
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
		  /// 检查URL是否符合要求
		  /// </summary>
		  /// <param name="url"></param>
		  /// <returns></returns>
		  public static bool CheckUrl(string url)
		  {
			  string reg = @"(http://(bilibili.us|acfun.cn|www.acfun.cn|%IpString%)|)(/plus/view\.php\?aid=\d{1,10}|/html/(music|anime|game|ent|zj)/\d{8}/(\d{3,}|\d{3,}_\d{1,2})\.html|/video/\w+/)";
				reg=reg.Replace("%IpString%", Config.setting.ServerIP);
				if (Regex.IsMatch(url, reg))
					 return true;
				else
					 return false;
		  }

		 /// <summary>
		 /// 是否是Bilibili站点视频URL
		 /// </summary>
		 /// <param name="url"></param>
		 /// <returns></returns>
		  public static bool IsBili(string url)
		  {
			  string reg = @"http://bilibili.us/video/\w+";
			  if (Regex.IsMatch(url, reg))
				  return true;
			  else
				  return false;
		  }

		 /// <summary>
		  /// 将精简URL组合为完整URL
		 /// </summary>
		 /// <param name="url"></param>
		 /// <returns></returns>
		  public static string CombineUrl(string url)
		  {
			  string reg = @"^(/plus/view\.php\?aid=\d{1,10}|/html/(music|anime|game|ent|zj)/\d{8}/(\d{3,}|\d{3,}_\d{1,2})\.html)";
			  if (Regex.IsMatch(url, reg))
				  return @"http://acfun.cn" + url;
			  else
				  return url;
		  }

		 /// <summary>
		 /// 从完整URL中提取出精简URL
		 /// </summary>
		 /// <param name="url"></param>
		 /// <returns></returns>
		  public static string DepartUrl(string url)
		  {
			  string reg = @"^(/plus/view\.php\?aid=\d{1,10}|/html/(music|anime|game|ent|zj)/\d{8}/(\d{3,}|\d{3,}_\d{1,2})\.html)";
			  Match m = Regex.Match(url, reg);
			  if (m.Length > 0)
				  return m.Value;
			  else
				  return url;
		  }

		  /// <summary>
		  /// 取得网页源代码
		  /// </summary>
		  /// <param name="url"></param>
		  /// <param name="e"></param>
		  /// <returns></returns>
		  public static string GetSource(string url, System.Text.Encoding encode)
		  {
				string sline = "";
				var req = WebRequest.Create(url);
				var res = req.GetResponse();
				StreamReader strm = new StreamReader(res.GetResponseStream(), encode);
				sline=strm.ReadToEnd();
				strm.Close();
				return sline;
		  }

		  /// <summary>
		  /// 取得网页源代码
		  /// </summary>
		  /// <param name="url"></param>
		  /// <returns></returns>
		  public static string GetSource(string url)
		  {
				return GetSource(url, System.Text.Encoding.GetEncoding("GB2312"));
		  }

		  /// <summary>
		  /// 取得视频所有章节
		  /// </summary>
		  /// <param name="source"></param>
		  /// <returns></returns>
		  public static Dictionary<string,string> GetVideoSections(string url)
		  {
			  try
			  {
				  string strTmp = @"(?<url>http://(acfun.cn|www.acfun.cn|%IpString%)/html/(music|anime|game|ent|zj)/\d{8}/)(\d{3,}|\d{3,}_\d{1,2})\.html";
				  strTmp = strTmp.Replace("%IpString%", Config.setting.ServerIP);
				  //取得URL的前面部分
				  Regex r = new Regex(strTmp);
				  Match resultMatch = r.Match(url);
				  string urlHead = resultMatch.Groups["url"].Value;
				  //取得网页源代码
				  string source = AcDowner.GetSource(url);
				  r = new Regex(@"<option value='(?<id>\d{3,6}|\d{3,6}_\d{1,3}).html'(| selected)>(?<title>|.{1,50})</option>");
				  MatchCollection result = r.Matches(source);
				  //填入字典
				  Dictionary<string, string> tmp = new Dictionary<string, string>();
				  for (int i = 0; i <= result.Count - 1; i++)
				  {
					  string title = result[i].Groups["title"].Value;
					  //过滤非法字符
					  foreach (char j in Path.GetInvalidFileNameChars())
						  title = title.Replace(j.ToString(), "");
					  title = title.Replace(@"\", "");
					  title = title.Replace(@"/", "");
					  //添加到字典中
					  tmp.Add(urlHead + result[i].Groups["id"].Value + @".html",title);
				  }
				  return tmp;
			  }
			  catch(Exception ex) //如果出错
			  {
				  //添加到日志
					Logging.Add(ex);
				  return new Dictionary<string,string>();
			  }
		  }

		 /// <summary>
		 /// 取得视频当前章节的名称
		 /// </summary>
		 /// <param name="url"></param>
		 /// <returns></returns>
		  public static string GetVideoSectionName(string url)
		  {
			  //取得网页源代码
			  string source = AcDowner.GetSource(url);
			  //取得URL后面部分
			  string regexTemp = @"http://(acfun.cn|www.acfun.cn|%IpString%)/html/(music|anime|game|ent|zj)/\d{8}/(?<pageurl>(\d{3,}|\d{3,}_\d{1,2})\.html)";
			  regexTemp = regexTemp.Replace(@"%IpString%", Config.setting.ServerIP);
			  Regex r = new Regex(regexTemp);
			  Match mat = r.Match(url);
			  string subpage = mat.Groups["pageurl"].Value;
			  //取得名称
			  regexTemp = @"<option value='%SubUrl%'(| selected)>(?<content>.{0,50})</option>";
			  regexTemp = regexTemp.Replace(@"%SubUrl%", subpage);
			  r = new Regex(regexTemp);
			  mat = r.Match(source);
			  string content = mat.Groups["content"].Value;
			  //过滤非法字符
			  foreach (char i in Path.GetInvalidFileNameChars())
				  content = content.Replace(i.ToString(), "");
			  content = content.Replace(@"\", "");
			  content = content.Replace(@"/", "");
			  //如果没有匹配则会返回空字符串
			  return content;
		  }

		  /// <summary>
		  /// 检查是否是PlayerF
		  /// </summary>
		  /// <param name="source"></param>
		  /// <returns></returns>
		  public string CheckPlayerF(string source)
		  {
				Regex r = new Regex(@"file=(?<url>http://.{4,300}\.flv|http://.{4,300}\.mp4)");
				var result = r.Match(source);
				if (result.Success)
					 return result.Groups["url"].Value;
				else
					 return "";
		  }

		  /// <summary>
		  /// 取得视频ID
		  /// </summary>
		  /// <param name="source"></param>
		  /// <returns></returns>
		  public string GetVideoId(string source)
		  {
			  Regex r = new Regex(@"(\?( |\r|\n)*?(i|)d=(?<videoId>\w{5,20})|flashvars=""(i|)d=(?<videoId2>\w{5,20}))", RegexOptions.Singleline);
				var result = r.Match(source);
				string s = result.Groups["videoId"].Value;
			  if(!string.IsNullOrEmpty(s))
				  return s;
			  s = result.Groups["videoId2"].Value;
			  if(!string.IsNullOrEmpty(s))
				  return s;
			  return "";
		  }

		 /// <summary>
		 /// 取得Bilibili视频ID
		 /// </summary>
		 /// <param name="source"></param>
		 /// <returns></returns>
		  public string GetVideoIdB(string source)
		  {
			  Regex r = new Regex(@"flashvars=""(\w+|)id=(?<vId>\w+)");
			  var result = r.Match(source);
			  string s = result.Groups["vId"].Value;
			  if(!string.IsNullOrEmpty(s))
				  return s;
			  return "";
		  }

		  /// <summary>
		  /// 取得视频标题
		  /// </summary>
		  /// <param name="source"></param>
		  /// <returns></returns>
		  public string GetVideoTitle(string source)
		  {
				Regex r = new Regex(@"<title>(?<title>.+)( - AcFun.cn|_嗶哩嗶哩)</title>");
				var result = r.Match(source);
				string s= result.Groups["title"].Value;
				foreach (char i in Path.GetInvalidFileNameChars())
					 s = s.Replace(i.ToString(), "");
				s = s.Replace(@"\", "");
				s = s.Replace(@"/", "");
				return s;
		  }


		  /// <summary>
		  /// 取得视频文件信息
		  /// </summary>
		  /// <param name="videoId"></param>
		  /// <returns></returns>
		  public SinaVideo GetVideoInfo(string videoId)
		  {
				var sina = new SinaVideoLoader();
				return sina.LoadVideo(@"http://v.iask.com/v_play.php?vid=" + videoId.ToString(),Encoding.UTF8);
		  }

		  /// <summary>
		  /// 取得字幕文件地址
		  /// </summary>
		  /// <param name="videoId"></param>
		  /// <returns></returns>
		  public static string GetSubUrl(string videoId)
		  {
				string url = @"http://acfun.cn/newflvplayer/xmldata/%VideoId%/comment_on.xml?r=0.5446888564141";
				url = url.Replace("%IpString%", Config.setting.ServerIP);
				url = url.Replace("%VideoId%", videoId.ToString());
				return url;
		  }

		 /// <summary>
		 /// 取得bilibili字幕文件地址
		 /// </summary>
		 /// <param name="videoId"></param>
		 /// <returns></returns>
		  public static string GetSubUrlB(string videoId)
		  {
			  return @"http://d.bilibili.us/" + videoId;
		  }
	 }

	 
}
