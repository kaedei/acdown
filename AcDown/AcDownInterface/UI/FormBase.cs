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
					catch { }
				}
				FontFamily family;
				foreach (string name in fallback)
				{
					if (familes.TryGetValue(name, out family))
					{
						monoFont = new System.Drawing.Font(family, 11);
						return monoFont;
					}
				}

				monoFont = new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 11);
				return monoFont;
			}
		}

		protected override void OnControlAdded(ControlEventArgs e)
		{
			base.OnControlAdded(e);

			if (Tools.IsRunningOnMono)
			{
				e.Control.Font = FormBase.MonoFont;
				if (e.Control.ContextMenuStrip != null)
				{
					foreach (ToolStripItem item in e.Control.ContextMenuStrip.Items)
					{
						item.Font = FormBase.MonoFont;
					}
				}
				if (e.Control is ToolStrip)
				{
					foreach (ToolStripItem item in (e.Control as ToolStrip).Items)
					{
						item.Font = FormBase.MonoFont;
					}
				}
			}
		}
	}
}

