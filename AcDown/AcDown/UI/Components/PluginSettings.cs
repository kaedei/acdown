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
using System.Diagnostics;
using System.IO;

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
				//如果此插件将要被卸载则不加载到UI上
				if (CoreManager.PluginManager.IsPluginWillBeUninstalled(item))
					continue;
				object[] types = item.GetType().GetCustomAttributes(typeof(AcDownPluginInformationAttribute), true);
				if (types.Length > 0)
				{
					var attrib = (AcDownPluginInformationAttribute)types[0];
					var lvi = new ListViewItem(new string[]
						{
							attrib.FriendlyName,
							attrib.Version.ToString(),
							attrib.Author,
							attrib.Describe,
							attrib.SupportUrl,
							attrib.Name
						});
					lvi.Tag = item;
					lsv.Items.Add(lvi);
				}
			}
			lsv.ResumeLayout();
		}

		private void PluginSettings_Load(object sender, EventArgs e)
		{
			DwmApi.SetListViewVisualEffect(this.lsv);
		}

		//检查插件是否支持删除和访问网址
		private void lsv_SelectedIndexChanged(object sender, EventArgs e)
		{
			btnDelete.Enabled = false;
			btnSupport.Enabled = false;
			btnProperty.Enabled = false;
			if (lsv.SelectedItems.Count == 1)
			{
				var lvi = lsv.SelectedItems[0];
				var plugin = (IPlugin)lvi.Tag;

				if (plugin.Feature != null && plugin.Feature.ContainsKey("ConfigForm"))
				{
					btnProperty.Enabled = true;
				}


				try
				{
					var attrib = CoreManager.PluginManager.GetAttr(plugin);
					if (!string.IsNullOrEmpty(attrib.SupportUrl))
					{
						if (attrib.SupportUrl.StartsWith("http://", StringComparison.CurrentCultureIgnoreCase) ||
							attrib.SupportUrl.StartsWith("https://", StringComparison.CurrentCultureIgnoreCase))
						{
							btnSupport.Enabled = true;
						}
					}

					if (!CoreManager.PluginManager.IsInnerPlugin(plugin))
					{
						btnDelete.Enabled = true;
					}
				}
				catch { }
			}
		}

		//属性
		private void btnProperty_Click(object sender, EventArgs e)
		{
			var lvi = lsv.SelectedItems[0];
			var plugin = (IPlugin)lvi.Tag;
			var method = (Delegate)plugin.Feature["ConfigForm"];
			this.Invoke(method);
			//保存设置
			CoreManager.PluginManager.SaveConfiguration(plugin);
		}

		private void btnSupport_Click(object sender, EventArgs e)
		{
			var lvi = lsv.SelectedItems[0];
			var plugin = (IPlugin)lvi.Tag;
			try
			{
				var attrib = CoreManager.PluginManager.GetAttr(plugin);
				Process.Start(attrib.SupportUrl);
			}
			catch { }
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Multiselect = false;
			ofd.DefaultExt = ".acp";
			ofd.Filter = "AcDown插件(*.acp)|*.acp";
			ofd.Title = "向AcDown添加插件";
			if (ofd.ShowDialog() == DialogResult.Cancel)
				return;
			try
			{
				var attrib = PluginManager.InstallPlugin(ofd.FileName);
				MessageBox.Show("插件添加成功！" + Environment.NewLine + Environment.NewLine +
						"名称: " + attrib.FriendlyName + Environment.NewLine +
						"版本: " + attrib.Version.ToString() + Environment.NewLine +
						"作者: " + attrib.Author + Environment.NewLine +
						"来自: " + attrib.SupportUrl + Environment.NewLine + Environment.NewLine +
						"这个插件会在您下一次启动AcDown之后被加载", "添加插件成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (PluginFileNotSupportedException)
			{
				MessageBox.Show("未能成功加载此插件文件" + Environment.NewLine + "这个文件可能不是正确的AcDown插件", "插件加载失败", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
			catch (Exception)
			{
				MessageBox.Show("文件读取失败" + Environment.NewLine + "如果您想重新安装一个已有的插件，请退出所有正在运行中的AcDown后再安装", "插件加载失败", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			var lvi = lsv.SelectedItems[0];
			var plugin = (IPlugin)lvi.Tag;
			try
			{
				var attrib = CoreManager.PluginManager.GetAttr(plugin);
				if (MessageBox.Show("真的要删除这个插件吗？" + Environment.NewLine + Environment.NewLine +
						"名称: " + attrib.FriendlyName + Environment.NewLine +
						"版本: " + attrib.Version.ToString() + Environment.NewLine +
						"作者: " + attrib.Author + Environment.NewLine +
						"来自: " + attrib.SupportUrl, "删除插件",
						MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
						== DialogResult.Yes)
				{
					//删除插件
					CoreManager.PluginManager.UninstallPlugin(plugin);
					//移除ListViewItem
					lsv.Items.Remove(lvi);
					MessageBox.Show("删除成功！" + Environment.NewLine + "这个插件会在您下一次启动AcDown之后被彻底删除", "删除插件", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch { }
		}

	}
}
