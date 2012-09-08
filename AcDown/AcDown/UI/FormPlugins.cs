using System.Collections.ObjectModel;
using Kaedei.AcDown.Interface.UI;

namespace Kaedei.AcDown.UI
{
	/// <summary>
	/// "插件"窗体
	/// </summary>
	public partial class FormPlugins : FormBase
	{
		public FormPlugins(Collection<Interface.IPlugin> collection)
		{
			InitializeComponent();
			pluginSettings1.LoadPlugins(collection);
		}
	}
}
