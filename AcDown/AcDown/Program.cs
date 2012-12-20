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
			if (args.Length > 0)
			{
				string firstarg = "";
				//检查第一个参数
				firstarg = args[0];

				//设置配置文件路径
				CoreManager.StartupPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				 "Kaedei" + Path.DirectorySeparatorChar + "AcDown" + Path.DirectorySeparatorChar);

				if (File.Exists(firstarg))
				{
					string filename = new FileInfo(firstarg).FullName;
					//安装插件
					if (firstarg.EndsWith(".acp", StringComparison.CurrentCultureIgnoreCase))
					{
						try
						{
							var attrib = PluginManager.InstallPlugin(filename);
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
					else
					{
						//如果程序以临时文件启动
						if (Updater.CheckIfUpdating(Application.ExecutablePath))
						{
							//以自身覆盖目标文件
							if (!Updater.CopyTempFileToTargetFile(filename))
								MessageBox.Show("自动更新失败: 旧版本的AcDown可能尚未完全退出", "AcDown自动更新", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							Process.Start(filename, "updated");//重新执行目标文件
						}
					}
					//退出当前程序
					return;
				}
				else if (firstarg.Equals("updated", StringComparison.CurrentCultureIgnoreCase))
				{
					//如果参数为"updated"，删除临时文件
					Updater.DeleteTempFile();
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
				if (MessageBox.Show("AcDown正在工作中，您是否希望运行一个新的AcDown实例?" + Environment.NewLine + "(同时打开多个AcDown容易出现不稳定的状况)", "启动多个AcDown", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
					return;
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Program.frmStart = new FormStart();
			Application.Run(Program.frmStart);
			mLocker.ReleaseMutex();
		}
	}

}
