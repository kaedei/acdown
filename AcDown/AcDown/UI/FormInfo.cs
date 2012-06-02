using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Kaedei.AcDown.Interface;
using Kaedei.AcDown.Core;
using System.IO;

namespace Kaedei.AcDown.UI
{
	public partial class FormInfo : Form
	{
		TaskInfo _task;
		public FormInfo(TaskInfo task)
		{
			InitializeComponent();
			_task = task;
		}


		private void FormInfo_Load(object sender, EventArgs e)
		{
			this.Text = this.Text + " - " + _task.Title;
			//添加信息
			StringBuilder sb = new StringBuilder();

			sb.AppendLine("任务编号: " + _task.TaskId.ToString());
			sb.AppendLine("Url: " + _task.Url);
			sb.AppendLine("下载状态: " + _task.Status.ToString());
			if (_task.BasePlugin == null)
			{
				sb.AppendLine("插件: 未加载或未启用");
				sb.AppendLine("期望插件: " + _task.PluginName);
			}
			else
			{
				object[] types = _task.BasePlugin.GetType().GetCustomAttributes(typeof(AcDownPluginInformationAttribute), true);
				var attrib = (AcDownPluginInformationAttribute)types[0];
				sb.AppendLine("插件: " + attrib.FriendlyName + " " + attrib.Version.ToString());
			}
			sb.AppendLine("标题: " + _task.Title);
			sb.AppendLine("创建时间: " + _task.CreateTime.ToString());
			if (_task.FinishTime != null)
				sb.AppendLine("完成时间: " + _task.FinishTime.ToString());
			sb.AppendLine("引用页: " + _task.SourceUrl);
			sb.AppendLine("保存目录: " + _task.SaveDirectory.ToString());
			sb.AppendLine("任务散列值: " + _task.Hash);
			sb.AppendLine("字幕/弹幕下载设置: " + _task.DownSub.ToString());
			sb.AppendLine("关联视频解析设置: " + (_task.ParseRelated ? "是" : "否"));
			sb.AppendLine("被其他任务所添加: " + (_task.IsBeAdded ? "是" : "否"));
			sb.Append("代理服务器: ");
			if (_task.Proxy != null && _task.Proxy.Address != null)
			{
				sb.AppendLine(_task.Proxy.Address.ToString());
			}
			else
				sb.AppendLine("未设置");
			sb.AppendLine("注释: ");
			sb.AppendLine(_task.Comment);
			sb.AppendLine();
			sb.AppendLine("文件: ");
			if (_task.FilePath.Count > 0)
			{
				foreach (string item in _task.FilePath)
				{
					if (File.Exists(item))
						sb.AppendLine(item);
					else
						sb.AppendLine("[文件不存在]" + item);
				}
			}
			else
			{
				sb.AppendLine("无");
			}
			sb.AppendLine();
			sb.AppendLine("字幕文件: ");
			if (_task.SubFilePath.Count > 0)
			{
				foreach (string item in _task.SubFilePath)
				{
					if (File.Exists(item))
						sb.AppendLine(item);
					else
						sb.AppendLine("[文件不存在]" + item);
				}
			}
			else
			{
				sb.AppendLine("无");
			}
			sb.AppendLine();
			if (_task.PartialFinished)
			{
				sb.AppendLine("下载成功完成但期间出现错误:");
				sb.AppendLine(_task.PartialFinishedDetail);
			}

			sb.AppendLine();
			sb.AppendLine("插件存储的设置: ");
			if (_task.Settings.Count > 0)
			{
				foreach (string key in _task.Settings.Keys)
				{

					sb.AppendLine(key + " = " + _task.Settings[key]);
				}
			}
			else
			{
				sb.AppendLine("无");
			}
			sb.AppendLine();
			sb.AppendLine("自动应答设置: ");
			if (_task.AutoAnswer != null)
			{
				if (_task.AutoAnswer.Count > 0)
				{
					for (int i = 0; i < _task.AutoAnswer.Count; i++)
					{
						var aa = _task.AutoAnswer[i];
						sb.AppendLine(string.Format("\t{0}: {1}-{2} ({3})", i, aa.Prefix, aa.Identify, aa.Description));
					}
				}
				else
				{
					sb.AppendLine("无");
				}
			}
			sb.AppendLine();
			//sb.AppendLine("索引信息: ");
			//sb.AppendLine(_task.ToString());
			sb.AppendLine("最近一次发生的错误: ");
			if (_task.LastError != null)
			{
				sb.AppendLine("错误信息 = " + _task.LastError.Message);
				if (_task.LastError.TargetSite != null)
					sb.AppendLine("方法名称 = " + _task.LastError.TargetSite);
				if (_task.LastError.Source != null)
					sb.AppendLine("来源 = " + _task.LastError.Source);
				if (_task.LastError.StackTrace != null)
					sb.AppendLine("堆栈 = " + _task.LastError.StackTrace);
				if (!string.IsNullOrEmpty(_task.LastError.HelpLink))
					sb.AppendLine("帮助链接 = " + _task.LastError.HelpLink);
				if (_task.LastError.InnerException != _task.LastError.InnerException)
					sb.AppendLine("内联异常 = " + _task.LastError.InnerException.Message);
			}
			else
				sb.AppendLine("无");
			sb.AppendLine();

			txtInfo.Text = sb.ToString();

		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(txtInfo.Text))
			{
				try
				{
					Clipboard.SetText(txtInfo.Text);
				}
				catch { };
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
