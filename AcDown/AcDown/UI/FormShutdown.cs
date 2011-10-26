using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Kaedei.AcDown.UI;

namespace Kaedei.AcDown.UI
{
	public partial class FormShutdown : Form
	{
		private int time = 0;
		private ShutdownType action;
		public FormShutdown(ShutdownType type)
		{
			action = type;
			InitializeComponent();
			switch (action)
			{
				case ShutdownType.Shutdown:
					this.Text = "关机";
					this.lblDescribe.Text = "系统将在以下时间内关机";
					break;
				case ShutdownType.Logoff:
					this.Text = "注销";
					this.lblDescribe.Text = "系统将在以下时间内注销";
					break;
				case ShutdownType.Reboot:
					this.Text = "重新启动";
					this.lblDescribe.Text = "系统将在以下时间内重新启动";
					break;
				case ShutdownType.Hibernate:
					this.Text = "休眠";
					this.lblDescribe.Text = "系统将在以下时间内休眠";
					break;
				case ShutdownType.Suspend:
					this.Text = "待机";
					this.lblDescribe.Text = "系统将在以下时间内待机";
					break;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			timer.Enabled = false;
			this.Dispose();
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			if (time == 30)
			{
				timer.Enabled = false;
				switch (action)
				{
					case ShutdownType.Shutdown:
						Shutdown.ShutDown();
						break;
					case ShutdownType.Logoff:
						Shutdown.LogOff();
						break;
					case ShutdownType.Reboot:
						Shutdown.Reboot();
						break;
					case ShutdownType.Hibernate:
						Shutdown.Hibernate();
						break;
					case ShutdownType.Suspend:
						Shutdown.Hibernate();
						break;
				}
				Program.frmStart.Close();
			}
			else
			{
				time++;
				lblTime.Text = (30-time).ToString();
				pgbTime.Value = time;
			}
		}

		private void FormShutdown_Load(object sender, EventArgs e)
		{
			//防止AcDown阻止系统关机
			Program.frmMain.Hide();
		}
	}

	public enum ShutdownType
	{
		None,
		Shutdown,
		Logoff,
		Reboot,
		Hibernate,
		Suspend,
		ExitProgram
	}
}
