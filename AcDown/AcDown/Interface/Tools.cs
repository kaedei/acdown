﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Kaedei.AcDown.Interface
{
   /// <summary>
   /// 其他工具
   /// </summary>
   public class Tools
   {
      /// <summary>
      /// 无效字符过滤
      /// </summary>
      /// <param name="input">需要过滤的字符串</param>
      /// <param name="replace">替换为的字符串</param>
      /// <returns></returns>
      public static string InvalidCharacterFilter(string input, string replace)
      {
         if (replace == null)
            replace = "";
         foreach (var item in System.IO.Path.GetInvalidFileNameChars())
         {
            input = input.Replace(item.ToString(), replace);
         }
         foreach (var item in System.IO.Path.GetInvalidPathChars())
         {
            input = input.Replace(item.ToString(), replace);
         }
         return input;
      }

      /// <summary>
      /// 取得网络文件的后缀名
      /// </summary>
      /// <param name="url"></param>
      /// <returns></returns>
      public static string GetExtension(string url)
      {
         return new Regex(@"\.(?<ext>\w{3})\?").Match(url).Groups["ext"].ToString();
      }

      /// <summary>
      /// 将Unicode字符转换为String
      /// </summary>
      /// <param name="input"></param>
      /// <returns></returns>
      public static string ReplaceUnicode2Str(string input)
      {
         Regex regex = new Regex("(?i)\\\\u[0-9a-f]{4}");
         MatchEvaluator matchAction = delegate(Match m)
         {
            string str = m.Groups[0].Value;
            byte[] bytes = new byte[2];
            bytes[1] = byte.Parse(int.Parse(str.Substring(2, 2), NumberStyles.HexNumber).ToString());
            bytes[0] = byte.Parse(int.Parse(str.Substring(4, 2), NumberStyles.HexNumber).ToString());
            return Encoding.Unicode.GetString(bytes);
         };
         return regex.Replace(input, matchAction);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="input"></param>
      /// <returns></returns>
      public static string DecodeString(string input)
      {
         string result = "";

         for (int i = 0; i < input.Length; i++)
         {
            if (input.Substring(i, 1) == "%" && input.Substring((i + 3), 1) == "%")
            {
               string bstr1 = "0x" + input.Substring(i + 1, 1) + input.Substring(i + 2, 1);
               string bstr2 = "0x" + input.Substring(i + 4, 1) + input.Substring(i + 5, 1);

               result += encode(Convert.ToByte(bstr1, 16), Convert.ToByte(bstr2, 16));
               i += 5;
            }
            else
            {
               result += input.Substring(i, 1);
            }
         }

         return result;
      }


      private static string encode(byte b1, byte b2)
      {
         System.Text.Encoding ecode = System.Text.Encoding.GetEncoding("GB18030");
         Byte[] codeBytes = { b1, b2 };
         return ecode.GetChars(codeBytes)[0].ToString();
      }

   }
}
