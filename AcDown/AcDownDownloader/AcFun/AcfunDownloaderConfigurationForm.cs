﻿using System;
using System.Windows.Forms;
using Kaedei.AcDown.Interface;
using Kaedei.AcDown.Interface.UI;

namespace Kaedei.AcDown.Downloader.AcFun
{
	public partial class AcfunDownloaderConfigurationForm : FormBase
	{
		private SerializableDictionary<string, string> Configuration;

		public AcfunDownloaderConfigurationForm(SerializableDictionary<string, string> Configuration)
		{
			//初始化界面
			InitializeComponent();
			//读取插件设置
			this.Configuration = Configuration;
			//生成AcPlay设置
			string genAcPlay = Configuration.ContainsKey("GenerateAcPlay") ? Configuration["GenerateAcPlay"] : "true";
			chkGenerateAcPlay.Checked = (genAcPlay == "true");
			//自定义命名
			txtFormat.Text = Configuration.ContainsKey("CustomFileName") ?
				Configuration["CustomFileName"] : AcFunPlugin.DefaultFileNameFormat;
			//预览
			txtFormat_TextChanged(this, EventArgs.Empty);

		}

		private void txtFormat_TextChanged(object sender, EventArgs e)
		{
			var helper = new CustomFileNameHelper();
			txtPreview.Text = helper.CombineFileName(txtFormat.Text, "银魂", "第99话", "5", "flv", "ac54321", "2", "", "", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString());
		}

		//添加变量文字到文本框
		private void btnAdd_Click(object sender, EventArgs e)
		{
			if (cboVar.SelectedIndex >= 0)
			{
				txtFormat.AppendText(cboVar.Text);
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			//检查格式文本框中是否有(分段)和扩展名
			if (!txtFormat.Text.Contains(CustomFileNameHelper.PART))
			{
				MessageBox.Show("您所定义的文件名称中不含有分段编号，请重新添加", "文件名缺少必要部分");
				return;
			}
			if (!txtFormat.Text.Contains(CustomFileNameHelper.EXT))
			{
				MessageBox.Show("您所定义的文件名称中不含有扩展名，请重新添加", "文件名缺少必要部分");
				return;
			}

			//保存设置
			Configuration["CustomFileName"] = txtFormat.Text;
			Configuration["GenerateAcPlay"] = chkGenerateAcPlay.Checked ? "true" : "false";

			//关闭窗体
			this.Close();
		}

		private void AcfunDownloaderConfigurationForm_Load(object sender, EventArgs e)
		{

		}

	}
}
