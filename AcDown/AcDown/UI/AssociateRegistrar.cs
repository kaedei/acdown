using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace Kaedei.AcDown.UI
{
	public static class AssociateRegistrar
	{
		/// <summary>
		/// 创建文件关联
		/// </summary>
		/// <param name="programFile">应用程序文件的完整路径("C:\abc\def.exe")</param>
		/// <param name="extension">文件扩展名（例如 ".txt"）</param>
		/// <param name="typeName">文件类型名称</param>
		/// <param name="description">文件类型描述</param>
		/// <param name="argument">附加参数（不包括"%1"）</param>
		/// <returns></returns>
		public static bool CreateAssociate(string programFile, string extension, string typeName, string description, string argument)
		{
			argument = argument ?? "";
			try
			{
				RegistryKey key;
				//设置自定义文件的双击打开
				key = Registry.ClassesRoot.OpenSubKey(typeName);
				if (key == null)
					key = Registry.ClassesRoot.CreateSubKey(typeName);
				key.SetValue("", description);
				key = key.CreateSubKey("shell");
				key = key.CreateSubKey("open");
				key = key.CreateSubKey("command");
				key.SetValue("", string.Format(@"""{0}"" ""%1"" {1}", programFile, argument));

				//设置自定义文件的默认图标
				//key = Registry.ClassesRoot.OpenSubKey(typeName, true);
				//key = key.CreateSubKey("DefaultIcon");
				//key.SetValue("", string.Format("{0},0", programFile));

				//新增的扩展名和设置关联
				key = Registry.ClassesRoot.OpenSubKey(extension);
				if (key == null)
					key = Registry.ClassesRoot.CreateSubKey(extension);
				key.SetValue("", typeName);

				//刷新Explorer
				DwmApi.RefreshNotify();

				return true;
			}
			catch
			{
				return false;
			}

		}
	}
}
