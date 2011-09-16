using System;
using System.Windows.Forms;
using AcDown.UI;
using Microsoft.VisualBasic.ApplicationServices;
using Kaedei.AcDown.UI;

namespace AcDown
{
    static class Program
    {
       public static FormMain frmMain;
       private static string[] arguments;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FormMain());
            SingleInstanceManager manager = new SingleInstanceManager();//单实例管理器
            manager.Run(args);
        }
    }

    public class SingleInstanceManager : WindowsFormsApplicationBase
    {

       public SingleInstanceManager()
       {
          this.IsSingleInstance = true;
       }

       protected override bool OnStartup(Microsoft.VisualBasic.ApplicationServices.StartupEventArgs e)
       {
          Application.Run(new FormStart());
          return true;
       }

       protected override void OnStartupNextInstance(StartupNextInstanceEventArgs eventArgs)
       {
          base.OnStartupNextInstance(eventArgs);
          Program.frmMain.ShowFormToFront();
          //List<string> s = new List<string>(eventArgs.CommandLine);

       }
    }

}
