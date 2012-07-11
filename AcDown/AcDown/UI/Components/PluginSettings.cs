using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Kaedei.AcDown.Core;
using Kaedei.AcDown.Interface;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace Kaedei.AcDown.UI.Components
{
	public partial class PluginSettings : UserControl
	{

		public PluginSettings()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 读取并显示插件
		/// </summary>
		/// <param name="plugins"></param>
		public void LoadPlugins(Collection<IPlugin> plugins)
		{
			lsv.SuspendLayout();
			lsv.Items.Clear();
			foreach (var item in plugins)
			{
				object[] types = item.GetType().GetCustomAttributes(typeof(AcDownPluginInformationAttribute), true);
				if (types.Length > 0)
				{
					var attrib = (AcDownPluginInformationAttribute)types[0];
					lsv.Items.Add(new ListViewItem(new string[]
						{
							attrib.FriendlyName,
							attrib.Version.ToString(),
							attrib.Author,
							attrib.Describe,
							attrib.SupportUrl,
							attrib.Name
						}));
				}
			}
			lsv.ResumeLayout();
		}

        private void PluginSettings_Load(object sender, EventArgs e)
        {
            DwmApi.SetListViewVisualEffect(this.lsv);
        }

	}
}
