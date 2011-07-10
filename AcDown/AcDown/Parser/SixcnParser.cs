using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;
using System.Text.RegularExpressions;
using System.Net;

namespace Kaedei.AcDown.Parser
{
   /// <summary>
   /// 6.cn解析器
   /// </summary>
   class SixcnParser:IParser
   {
      #region IParser 成员
      /// <summary>
      /// 解析6.cn视频
      /// </summary>
      /// <param name="parameters">维度为1、长度为1的字符串数组，内容为待分析的视频ID（evid）</param>
      /// <returns>各备用视频的真实地址数组</returns>
      public string[] Parse(string[] parameters, WebProxy proxy)
      {
         string url = "http://6.cn/v72.php?vid=" + parameters[0];
         string source = Network.GetHtmlSource2(url, Encoding.UTF8, proxy);
         Regex rFlv = new Regex(@"<file>(?<flv>.*)</file>");
         Match mFlv = rFlv.Match(source);
         string flv = mFlv.Groups["flv"].Value;
         //flv.Replace("barcelona", "nantes");

         //生成key
         long loc1 = DateTime.Now.Ticks;
         Random r = new Random();
         loc1 += 123456;
         int key3 = 1000000000 + r.Next(1000000000);
         int key4 = 1000000000 + r.Next(1000000000);
         int key1, key2;
         int loc2 = r.Next(100);
         if (loc2 > 50)
         {
            key1 = Math.Abs((int)Math.Floor((double)loc1 / 3) ^ key3);
            key2 = Math.Abs((int)Math.Floor((double)(loc1 * 2) / 3) ^ key4);
         }
         else
         {
            key1 = Math.Abs((int)Math.Floor((double)loc1 * 2 / 3) ^ key3);
            key2 = Math.Abs((int)Math.Floor((double)loc1 / 3) ^ key4);
         }

         string realurl = flv + string.Format("?key1={0}&key2={1}&key3={2}&key4={3}", key1, key2, key3, key4);
         return new string[] { realurl };
      }

      #endregion
   }
}
