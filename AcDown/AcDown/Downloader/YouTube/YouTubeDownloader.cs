using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using Kaedei.AcDown.Interface.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace Kaedei.AcDown.Downloader
{
   public class YouTubeDownloader: IDownloader
   {
      public TaskInfo Info { get; set; }

      //下载参数
      DownloadParameter currentParameter;

      #region IDownloader 成员

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


      //下载视频
      public void Download()
      {
         //开始下载
         delegates.Start(new ParaStart(this.Info));
         delegates.TipText(new ParaTipText(this.Info, "正在分析视频地址"));
         Info.Status = DownloadStatus.正在下载;
         try
         {
           
            //取得网页源文件
            string src = Network.GetHtmlSource2(Info.Url, Encoding.UTF8, Info.Proxy);

            //转码
            for (int i = 0; i < 10; i++)
            {
               src = src.Replace("%25", "%");
            }
            src = src.Replace("%3A", ":").Replace("%2F", "/")
               .Replace("%3F", "?").Replace("%2C", ",").Replace("%3D", "=")
               .Replace("%26", "&");

            //取得视频标题
            Regex rTitle = new Regex(@"<meta name=""title"" content=""(?<title>.+?)"">");
            Match mTitle = rTitle.Match(src);
            string title = mTitle.Groups["title"].Value;

            Info.Title = title;
            //过滤非法字符
            title = Tools.InvalidCharacterFilter(title, "");

            //视频地址数组
            string[] videos = null;
            //清空地址
            Info.FilePath.Clear();

            //取得Flash参数
            Regex rFlashVars = new Regex(@"flashvars=""(?<content>.+?)""");
            Match mFlashVars = rFlashVars.Match(src);
            string flashvars = mFlashVars.Groups["content"].Value;



            //取得清晰度列表
            Regex rFmtList = new Regex(@"fmt_list=(?<fmt>.+?)&amp");
            Match mFmtList = rFmtList.Match(flashvars);
            string fmtlist = mFmtList.Groups["fmt"].Value;

            Regex rFmts = new Regex(@"(?<fmtid>\d+)/(?<fmtres>\d+x\d+)/\d+/\d+/\d+");
            MatchCollection mFmts = rFmts.Matches(fmtlist);
            List<string> resolutions = new List<string>();
            foreach (Match fmt in mFmts)
            {
               resolutions.Add("FMT=" + fmt.Groups["fmtid"].Value + ",分辨率: " + fmt.Groups["fmtres"].Value);
            }

            //获取下载地址
            string urls = src.Substring(src.IndexOf("url_encoded_fmt_stream_map"));
            Regex rUrls = new Regex(@"url=(?<url>http://.+?)&quality=");
            MatchCollection mUrls = rUrls.Matches(urls);
            List<string> videoUrls = new List<string>();
            foreach (Match item in mUrls)
            {
               videoUrls.Add(item.Groups["url"].Value);
            }
            
            //用户选择清晰度
            int selected = ToolForm.CreateSelectServerForm("您正在下载YouTube视频，请选择视频清晰度:", resolutions.ToArray(), 0);

            videos = new string[1];
            videos[0] = videoUrls[selected];

            //下载视频
            //确定视频共有几个段落
            Info.PartCount = videos.Length;

            //分段落下载
            for (int i = 0; i < Info.PartCount; i++)
            {
               Info.CurrentPart = i + 1;

               //取得文件后缀名
               string ext = Tools.GetExtension(videos[i]);
               //设置当前DownloadParameter
               if (Info.PartCount == 1) //如果只有一段
               {
                  currentParameter = new DownloadParameter()
                  {
                     //文件名 例: c:\123(1).flv
                     FilePath = Path.Combine(Info.SaveDirectory.ToString(),
                                            title + ".flv"),
                     //文件URL
                     Url = videos[i],
                     //代理服务器
                     Proxy = Info.Proxy
                  };
               }
               else //如果分段有多段
               {
                  currentParameter = new DownloadParameter()
                  {
                     //文件名 例: c:\123(1).flv
                     FilePath = Path.Combine(Info.SaveDirectory.ToString(),
                                            title + "(" + (i + 1).ToString() + ")" + ".flv"),
                     //文件URL
                     Url = videos[i],
                     //代理服务器
                     Proxy = Info.Proxy
                  };
               }

               //添加文件路径到List<>中
               Info.FilePath.Add(currentParameter.FilePath);
               //下载文件
               bool success;
               
               //提示更换新Part
               delegates.NewPart(new ParaNewPart(this.Info, i + 1));

               //下载视频文件
               success = Network.DownloadFile(currentParameter, this.Info);

               //未出现错误即用户手动停止
               if (!success)
               {
                  Info.Status = DownloadStatus.已经停止;
                  delegates.Finish(new ParaFinish(this.Info, false));
                  return;
               }
            }
         }
         catch (Exception ex) //出现错误即下载失败
         {
            Info.Status = DownloadStatus.出现错误;
            delegates.Error(new ParaError(this.Info, ex));
            return;
         }
         //下载成功完成
         Info.Status = DownloadStatus.下载完成;
         delegates.Finish(new ParaFinish(this.Info, true));

      }


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
