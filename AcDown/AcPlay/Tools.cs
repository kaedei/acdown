using System;
using System.Text;

namespace Kaedei.AcPlay
{
	public class Tools
	{
		/// <summary>
		/// Url编码(编码为Base64)
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string UrlEncode(string str)
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
		}

		/// <summary>
		/// URL解码(从Base64)
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string UrlDecode(string input)
		{
			return Encoding.UTF8.GetString(Convert.FromBase64String(input));
		}

		/// <summary>
		/// 从适用于URL的Base64编码字符串转换为普通字符串
		/// </summary>
		public static string FromBase64StringForUrl(string base64String)
		{
			string temp = base64String.Replace('.', '=').Replace('*', '+').Replace('-', '/');
			return Encoding.UTF8.GetString(Convert.FromBase64String(temp));
		}

		/// <summary>
		/// 从普通字符串转换为适用于URL的Base64编码字符串
		/// </summary>
		public static string ToBase64StringForUrl(string normalString)
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(normalString)).Replace('+', '*').Replace('/', '-').Replace('=', '.');
		}

	}
}
