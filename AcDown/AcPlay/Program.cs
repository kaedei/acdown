using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Kaedei.AcPlay
{

	static class Program
	{
		public static FormDebug DebugWindow;
		public static FormPlayer PlayerWindow;

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			if (args.Length != 1 && args.Length != 2)
			{
				MessageBox.Show("AcPlay启动失败:" + Environment.NewLine +
					"请使用AcDown动漫下载器的“弹幕播放”功能，或双击播放快捷方式(.acplay文件)启动AcPlay" + Environment.NewLine +
					"AcDown动漫下载器可以从 http://acdown.codeplex.com/ 下载到",
					"AcPlay弹幕播放器", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			try
			{
				string arg = args[0];
				XmlSerializer serializer = new XmlSerializer(typeof(AcPlayConfiguration));
				using (FileStream fs = new FileStream(arg, FileMode.Open))
				{
					object tempConfig = serializer.Deserialize(fs);
					AcPlayConfiguration.Config = (AcPlayConfiguration)tempConfig;
					AcPlayConfiguration.Config.StartupPath = Path.GetDirectoryName(arg);
				}
			}
			catch
			{
				MessageBox.Show("配置文件读取失败: " + args[0], "AcPlay弹幕播放器", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return;
			}
			try
			{
				PlayerWindow = new FormPlayer();
				if (args.Length == 2 && args[1].EndsWith("debug", StringComparison.CurrentCultureIgnoreCase))
				{
					PlayerWindow.Show();
					DebugWindow = new FormDebug();
					Application.Run(DebugWindow);
				}
				else
				{
					Application.Run(new FormPlayer());
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("发生未处理的异常: " + ex.Message, "AcPlay弹幕播放器", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return;
			}
		}
	}
}
