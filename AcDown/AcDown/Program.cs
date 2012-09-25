using System;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using Kaedei.AcDown.UI;
using Kaedei.AcDown.Core;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;
using Kaedei.AcDown.Interface;
using System.Threading;

namespace Kaedei.AcDown
{
	static class Program
	{
		public static FormMain frmMain;
		public static FormStart frmStart;

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			if (args.Length > 0)
			{
				string firstarg = "";
				//检查第一个参数
				firstarg = args[0];

				if (File.Exists(firstarg))
				{
					Updater upd = new Updater();
					//如果程序以临时文件启动
					if (upd.CheckIfUpdating(Application.ExecutablePath))
					{
						//以自身覆盖目标文件
						upd.CopyTempFileToTargetFile(firstarg);
						//重新执行目标文件
						Process.Start(firstarg, "updated");
						//退出当前程序
						return;
					}
				}
				else if (firstarg.Equals("updated", StringComparison.CurrentCultureIgnoreCase))
				{
					//如果参数为"updated"，删除临时文件
					Updater upd = new Updater();
					upd.DeleteTempFile();
				}
				else if (firstarg.Equals("regasso", StringComparison.CurrentCultureIgnoreCase))
				{
					if (Tools.IsRunningOnMono)
						//MessageBox.Show("此功能暂时不能在非 Windows 上使用。");
						return;
					//注册.acplay关联
					AssociateRegistrar.CreateAssociate(
						 Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
						 "Kaedei" + Path.DirectorySeparatorChar + "AcPlay" + Path.DirectorySeparatorChar + "acplay.exe"),
						 ".acplay", "AcPlayFile", "弹幕播放快捷方式", "");
					//注册.acp关联
					AssociateRegistrar.CreateAssociate(Application.ExecutablePath,
						 ".acp", "AcDownPlugin", "AcDown插件", "");
					return;

				}
				else if (firstarg.Equals("firstrun", StringComparison.CurrentCultureIgnoreCase))
				{
					var fr = new FirstrunHandler();
					fr.RunFirstRun();
					return;
				}
			}
			bool isAcDownNotStarted = false;
			Mutex mLocker = new Mutex(true, "AcDown", out isAcDownNotStarted);

			if (isAcDownNotStarted)
			{
				mLocker.WaitOne();
			}
			else
			{
				if (MessageBox.Show("AcDown正在工作中，您是否希望运行一个新的AcDown实例?\r\n(同时打开多个AcDown容易出现不稳定的状况)", "启动多个AcDown", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
					return;
			}
			Program.frmStart = new FormStart();
			Application.Run(Program.frmStart);
			mLocker.ReleaseMutex();
		}
	}

}
