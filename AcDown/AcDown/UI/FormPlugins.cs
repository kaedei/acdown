using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace Kaedei.AcDown.UI
{
	/// <summary>
	/// "插件"窗体
	/// </summary>
	public partial class FormPlugins : Form
	{
		public FormPlugins(Collection<Interface.IPlugin> collection)
		{
			InitializeComponent();
			pluginSettings1.LoadPlugins(collection);
		}
	}
}
