﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Kaedei.AcDown.Interface;

namespace Kaedei.AcDown.Parser
{
	public interface IParser
	{
		ParseResult Parse(ParseRequest request);
	}

	/// <summary>
	/// 请求解析的相关信息
	/// </summary>
	public class ParseRequest
	{
		/// <summary>
		/// 需要解析的ID
		/// </summary>
		public string Id = "";
		/// <summary>
		/// 用户名
		/// </summary>
		public string Username = "";
		/// <summary>
		/// 密码
		/// </summary>
		public string Password = "";
		/// <summary>
		/// 代理服务器
		/// </summary>
		public WebProxy Proxy = new WebProxy();
		/// <summary>
		/// 自动应答设置
		/// </summary>
		public List<AutoAnswer> AutoAnswers = new List<AutoAnswer>();
		/// <summary>
		/// 指定特殊配置
		/// </summary>
		public List<string> SpecificConfiguration = new List<string>();
	}

	/// <summary>
	/// 解析器的解析结果
	/// </summary>
	public class ParseResult
	{
		public List<ParseResultItem> Items = new List<ParseResultItem>();
		public String[] ToArray()
		{
			string[] r = new string[Items.Count];
			for (int i = 0; i < Items.Count; i++)
			{
				r[i] = Items[i].RealAddress.ToString();
			}
			return r;
		}
	}

	/// <summary>
	/// 每个解析结果的相关信息
	/// </summary>
	public class ParseResultItem
	{
		public ParseResultItem() { }
		public ParseResultItem(Uri address)
		{
			RealAddress = address;
		}
		/// <summary>
		/// 真实地址
		/// </summary>
		public Uri RealAddress = null;
		/// <summary>
		/// 相关信息字典
		/// </summary>
		public Dictionary<string, string> Information = new Dictionary<string, string>();
	}

}
