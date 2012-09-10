using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using Kaedei.AcDown.Interface;

namespace Kaedei.AcDown.Interface.UI
{
	public class FormBase : System.Windows.Forms.Form
	{
		public FormBase()
		{
			// 如果我设置了这个，遇到的问题是控件大小会根据字体来缩放，然而这不是我想要的结果。
			/*
			if (Tools.IsRunningOnMono)
				this.Font = FormBase.MonoFont;
			*/
		}

		private static System.Drawing.Font monoFont;

		public static System.Drawing.Font MonoFont
		{
			get
			{
				if (monoFont != null)
					return monoFont;

				// 一些常见的可能出现在 linux 中的字体，据说 droid sans fallback & wenquanyi 的被大部分发行版接受用于中文显示
				var fallback = new string[] { "微软雅黑", "Droid Sans Fallback", "WenQuanYi Zen Hei", "WenQuanYi Micro Hei", "宋体", "SimSun" };

				Dictionary<string, FontFamily> familes = new Dictionary<string, FontFamily>();
				foreach (FontFamily item in System.Drawing.FontFamily.Families)
				{
					try
					{
						familes.Add(item.GetName(0), item);
					}
					catch
					{
					}
				}
				FontFamily family;
				foreach (string name in fallback)
				{
					if (familes.TryGetValue(name, out family))
					{
						monoFont = new System.Drawing.Font(family, 9);
						return monoFont;
					}
				}

				monoFont = new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 9);
				return monoFont;
			}
		}

		protected override void OnControlAdded(ControlEventArgs e)
		{
			base.OnControlAdded(e);
			if (Tools.IsRunningOnMono)
			{
				ApplyMonoStyle(e.Control);
			}
		}

		protected void ApplyMonoStyle(System.Windows.Forms.Control control)
		{
			control.Font = new Font(FormBase.MonoFont.FontFamily, control.Font.Size);
			if (control.ContextMenuStrip != null)
			{
				foreach (ToolStripItem item in control.ContextMenuStrip.Items)
				{
					item.Font = new Font(FormBase.MonoFont.FontFamily, item.Font.Size);
				}
			}
			if (control is ToolStrip)
			{
				foreach (ToolStripItem item in (control as ToolStrip).Items)
				{
					item.Font = new Font(FormBase.MonoFont.FontFamily, item.Font.Size);
				}
			}

			foreach (System.Windows.Forms.Control item in control.Controls)
			{
				ApplyMonoStyle(item);
			}
		}
	}
}

