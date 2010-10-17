using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Kaedei.AcDown;
using AcDownLibrary;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace Kaedei.AcDown
{
	public partial class FormXml : Form
	{
		private AcDowner task;
		//private string strXml;

		public FormXml(AcDowner ac)
		{
			task = ac;
			InitializeComponent();
		}

		private void FormXml_Load(object sender, EventArgs e)
		{
			try
			{
				//添加信息
				this.Text = "任务详细信息——" + task.Info.vname;
				//
				XmlSerializer ser = new XmlSerializer(typeof(SinaVideo));
				StringBuilder sb = new StringBuilder();
				StringWriter sw = new StringWriter(sb);

				ser.Serialize(sw, task.Info);
				txtXml.Text = sb.ToString();
			}
			catch (Exception ex)
			{
				Logging.Add(ex);
				lnkCopy.Visible = false;
			}
		}
		//复制到剪贴板
		private void lnkCopy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Clipboard.SetText(txtXml.Text);
			MessageBox.Show("已复制到剪贴板", "复制XML文档树", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		//关闭窗体
		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
