﻿using System;
using System.Collections.Generic;
using System.Text;
using Kaedei.AcDown.Interface;

namespace Kaedei.AcDown.Interface
{
	/// <summary>
	/// AcDown插件
	/// </summary>
	public interface IPlugin
	{
		/// <summary>
		/// 创建新的下载器实例
		/// </summary>
		/// <returns></returns>
		IDownloader CreateDownloader();
		/// <summary>
		/// 检查指定的Url是否符合能够被当前插件解析
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		bool CheckUrl(string url);
		/// <summary>
		/// 取得指定Url的Hash值
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		string GetHash(string url);
		/// <summary>
		/// 插件支持的UI特性
		/// </summary>
		Dictionary<String, Object> Feature { get; } //AutoAnswer(List<AutoAnswer>) ExampleUrl(String[]) ConfigurationForm(MethodInvoker)
		/// <summary>
		/// 插件独立存储
		/// </summary>
		SerializableDictionary<String, String> Configuration { get; set; }
	}

	/// <summary>
	/// AcDown插件信息
	/// </summary>
	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
	public class AcDownPluginInformationAttribute : Attribute
	{
		/// <summary>
		/// 内部名称
		/// </summary>
		public string Name { get; private set; }
		/// <summary>
		/// 用户友好的名称
		/// </summary>
		public string FriendlyName { get; private set; }
		/// <summary>
		/// 作者
		/// </summary>
		public string Author { get; private set; }
		/// <summary>
		/// 版本（0.0.0.0）
		/// </summary>
		public Version Version { get; private set; }
		/// <summary>
		/// 详细描述
		/// </summary>
		public string Describe { get; private set; }
		/// <summary>
		/// 客户支持网址
		/// </summary>
		public string SupportUrl { get; private set; }

		/// <summary>
		/// 创建一个新的AcDownPluginInformationAttribute对象
		/// </summary>
		/// <param name="name">内部名称</param>
		/// <param name="friendlyName">友好名称</param>
		/// <param name="author">插件作者</param>
		/// <param name="version">版本号</param>
		/// <param name="describe">插件描述</param>
		/// <param name="supportUrl">插件作者网站</param>
		public AcDownPluginInformationAttribute(string name, string friendlyName, string author, string version, string describe, string supportUrl)
		{
			Name = name;
			FriendlyName = friendlyName;
			Author = author;
			Version = new Version(version);
			Describe = describe;
			SupportUrl = supportUrl;
		}
	}
}
