﻿using System;
using System.Windows.Forms;
using AcDown.UI;
using Microsoft.VisualBasic.ApplicationServices;

namespace AcDown
{
    static class Program
    {
       public static FormMain frmMain;
       //private static string[] args;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FormMain());
            SingleInstanceManager manager = new SingleInstanceManager();//单实例管理器
            manager.Run(new string[] { "" });
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
          Program.frmMain = new FormMain();
          Application.Run(Program.frmMain);
          return false;
       }

       protected override void OnStartupNextInstance(StartupNextInstanceEventArgs eventArgs)
       {
          // Subsequent launches
          base.OnStartupNextInstance(eventArgs);
          Program.frmMain.ShowFormToFront();
          //List<string> s = new List<string>(eventArgs.CommandLine);

       }
    }

}
