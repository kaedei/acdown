using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using Kaedei.AcDown.Interface.Forms;
using System.Net;

namespace Kaedei.AcDown.Downloader
{
   /// <summary>
   /// 爱漫画下载器
   /// </summary>
   public class SfAcgComicDownloader : IDownloader
   {

      public SfAcgComicDownloader(SfAcgPlugin p)
      {
         _basePlugin = p;
      }
      //插件
      SfAcgPlugin _basePlugin;
      public IAcdownPluginInfo GetBasePlugin() { return _basePlugin; }

      //下载参数
      DownloadParameter currentParameter = new DownloadParameter();

      #region IDownloader 成员

      public Guid TaskId { get; set; }

      public DelegateContainer delegates { get; set; }

      //文件总长度
      public long TotalLength
      {
         get
         {
            if (currentParameter != null)
            {
               return currentParameter.TotalLength;
            }
            else
            {
               return 0;
            }
         }
      }

      //已完成的长度
      public long DoneBytes
      {
         get
         {
            if (currentParameter != null)
            {
               return currentParameter.DoneBytes;
            }
            else
            {
               return 0;
            }
         }
      }

      //最后一次Tick时的值
      public long LastTick
      {
         get
         {
            if (currentParameter != null)
            {
               //将tick值更新为当前值
               long tmp = currentParameter.LastTick;
               currentParameter.LastTick = currentParameter.DoneBytes;
               return tmp;
            }
            else
            {
               return 0;
            }
         }
      }

      //分段数量
      private int _partCount;
      public int PartCount
      {
         get { return _partCount; }
      }

      //当前分段
      private int _currentPart;
      public int CurrentPart
      {
         get { return _currentPart; }
      }

      //下载地址
      public string Url { get; set; }


      //下载状态
      private DownloadStatus _status;
      public DownloadStatus Status
      {
         get
         {
            return _status;
         }
      }

      //视频标题
      private string _title;
      public string Title
      {
         get
         {
            return _title;
         }
      }

      //保存到的文件夹
      public DirectoryInfo SaveDirectory { get; set; }

      //下载文件地址
      private List<string> _filePath = new List<string>();
      public List<string> FilePath
      {
         get
         {
            return _filePath;
         }
      }

      //字幕文件地址
      private List<string> _subFilePath = new List<string>();
      public List<string> SubFilePath
      {
         get
         {
            return _subFilePath;
         }
      }

      //下载信息（显示到UI上）
      public string Info
      {
         get
         {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("TaskId: " + this.TaskId.ToString());
            sb.AppendLine("Url: " + this.Url);
            return sb.ToString();
         }
      }

      //下载视频
      public void Download()
      {
         //开始下载
         delegates.Start(new ParaStart(this.TaskId));
         delegates.TipText(new ParaTipText(this.TaskId, "正在分析漫画地址"));
         _status = DownloadStatus.正在下载;
         try
         {
            //取得Url源文件
            string src = Network.GetHtmlSource(Url, Encoding.UTF8, delegates.Proxy);

            //要下载的Url列表（页面）
            List<string> subUrls = new List<string>();

            //分析漫画id
            Regex r = new Regex(@"http://\w+\.sfacg\.com/AllComic/(?<id>.+)");
            Match m = r.Match(Url);
            string id = m.Groups["id"].Value;

            //是否为漫画的介绍页面
            bool isIntroPage = string.IsNullOrEmpty(id);

            #region 确定是整部漫画还是单独一话

            //如果为整部漫画
            if (isIntroPage)
            {
               //取得所有漫画的列表
               Regex rAllComics = new Regex(@"<li><a href=""(?<page>http://\w+\.sfacg\.com/AllComic/.+?)"" target=""_blank"">(?<title>.+?)</a>");
               MatchCollection mcAllComics = rAllComics.Matches(src);

               //新建数组
               BitArray selected = new BitArray(mcAllComics.Count);

               //suburl数组
               List<string> urls = new List<string>();
               foreach (Match item in mcAllComics)
               {
                  urls.Add(item.Groups["page"].Value);
               }

               //各话标题数组
               List<string> titles = new List<string>();
               foreach (Match item in mcAllComics)
               {
                  titles.Add(item.Groups["title"].Value);
               }

               //选择下载哪部漫画
               selected = ToolForm.CreateSelctionForm(titles.ToArray());

               //将地址填充到下载列表中
               for (int i = 0; i < mcAllComics.Count; i++)
               {
                  if (selected[i])
                  {
                     subUrls.Add(urls[i]);
                  }
               }

               //如果用户没有选择任何章节
               if (subUrls.Count == 0)
               {
                  _status = DownloadStatus.已经停止;
                  delegates.Finish(new ParaFinish(this.TaskId, false));
                  return;
               }

               //取得漫画标题
               Regex rTitle = new Regex(@"<title>(?<title>.+?),(\<title>)漫画在线观看_SF在线漫画</title>");
               Match mTitle = rTitle.Match(src);
               string title = mTitle.Groups["title"].Value;
               //过滤标题中的非法字符
               title = Tools.InvalidCharacterFilter(title, "");
               _title = title;
            }
            else //如果不是整部漫画则添加此单话url
            {
               subUrls.Add(Url);
               //取得漫画标题
               //取得源代码并分析
               string pSrc = Network.GetHtmlSource(Url, Encoding.UTF8, delegates.Proxy);
               //取得漫画标题
               Regex rTitle = new Regex(@"&gt;&gt; <a href="".+?"">(?<title>.+?)</a> &gt;&gt;");
               Match mTitle = rTitle.Match(pSrc);
               string title = mTitle.Groups["title"].Value;
               //过滤标题中的非法字符
               title = Tools.InvalidCharacterFilter(title, "");
               _title = title;
            } //end if

            #endregion

            #region 选择服务器

            //string serverName;
            ////取得配置文件
            //string serverjs = Network.GetHtmlSource(@"http://www.SfAcgComic.com/v2/config/config.js", Encoding.GetEncoding("GB2312"), delegates.Proxy);
            //Regex rServer = new Regex("\"(?<sname>.+?)\" , \"(?<surl>.+?)\"");
            //MatchCollection mServers = rServer.Matches(serverjs);

            ////添加到数组中
            //List<string> servers = new List<string>();
            //foreach (Match item in mServers)
            //{
            //   if (servers.Count < 5)
            //      servers.Add(item.Groups["sname"].Value);
            //}

            ////选择服务器
            //int svr = ToolForm.CreateSelectServerForm("", servers.ToArray(), 0);
            //serverName = mServers[svr].Groups["surl"].Value;


            #endregion

            #region 下载漫画

            //建立文件夹
            string mainDir = SaveDirectory + (SaveDirectory.ToString().EndsWith(@"\") ? "" : @"\") + _title;
            //确定漫画共有几个段落
            _partCount = subUrls.Count;

            //分段落下载
            for (int i = 0; i < _partCount; i++)
            {
               
               _currentPart = i + 1;
               //提示更换新Part
               delegates.NewPart(new ParaNewPart(this.TaskId, i + 1));

               //地址数组
               Dictionary<string, string> files = new Dictionary<string, string>();

               //分析源代码,取得下载地址
               WebClient wc = new WebClient();
               if (delegates.Proxy != null)
                  wc.Proxy = delegates.Proxy;

               //取得源代码
               byte[] buff = wc.DownloadData(subUrls[i]);
               string cookie = wc.ResponseHeaders.Get("Set-Cookie");
               string source = Encoding.UTF8.GetString(buff);
               //取得标题
               Regex rTitle = new Regex(@"&gt;&gt;.+?&gt;&gt; (?<title>.+?)</li>");
               Match mTitle = rTitle.Match(source);
               string subTitle = mTitle.Groups["title"].Value;
               //过滤子标题中的非法字符
               subTitle = Tools.InvalidCharacterFilter(subTitle, "");
               //合并本地路径(文件夹)
               string subDir = mainDir + @"\" + subTitle;
               //创建文件夹
               Directory.CreateDirectory(subDir);


               #region 取得js文件

               //取得js文件路径
               string urljs = subUrls[i];
               //分析漫画id
               Regex rjs = new Regex(@"(?<server>http://\w+\.sfacg\.com/)AllComic/(?<id>.+)");
               Match mjs = rjs.Match(urljs);
               string jsserver = mjs.Groups["server"].Value;
               string jsid = mjs.Groups["id"].Value.TrimEnd('/');
               urljs = jsserver + "Utility/" + jsid + ".js";

               #endregion


               //取得JS文件内容
               string jscontent = Network.GetHtmlSource(urljs, Encoding.UTF8);

               //添加到url数组
               Regex rFiles = new Regex(@"picAy\[(?<no>\d+)\] = ""(?<file>.+?(?<ext>\w{3}))""");
               MatchCollection mFiles = rFiles.Matches(jscontent);
               //添加url到数组
               foreach (Match item in mFiles)
               {
                  //将 url - 本地文件 添加到字典中
                  files.Add(item.Groups["file"].Value, Path.Combine(subDir, item.Groups["no"].Value + "." + item.Groups["ext"].Value));
               }

               //设置下载长度
               currentParameter.TotalLength = files.Count;
               currentParameter.DoneBytes = 0;

               //下载文件
               foreach (string key in files.Keys)
               {
                  if (currentParameter.IsStop)
                  {
                     _status = DownloadStatus.已经停止;
                     delegates.Finish(new ParaFinish(this.TaskId, false));
                     return;
                  }
                  try
                  {
                     wc.Headers.Add("Referer", subUrls[i]);
                     wc.Headers.Add("Cookie", cookie);
                     byte[] content = wc.DownloadData(key);
                     File.WriteAllBytes(files[key], content);
                  }
                  catch (Exception ex) { } //end try
                  currentParameter.DoneBytes += 1;
               } // end foreach

            }//end for
         }//end try
         catch (Exception ex) //出现错误即下载失败
         {
            _status = DownloadStatus.出现错误;
            delegates.Error(new ParaError(this.TaskId, ex));
            return;
         }//end try
         //下载成功完成
         _status = DownloadStatus.下载完成;
         delegates.Finish(new ParaFinish(this.TaskId, true));

            #endregion

      }//end DownloadVideo


      //停止下载
      public void StopDownload()
      {
         if (currentParameter != null)
         {
            //将停止flag设置为true
            currentParameter.IsStop = true;
         }
      }

      #endregion
   }
}
