using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Kaedei.AcDown.Component;

namespace Kaedei.AcDown.UI.Components
{
   public partial class PluginSettings : UserControl
   {
      public PluginSettings()
      {
         InitializeComponent();
      }

      private PluginManager _mgr;
      public void SetPluginManager(PluginManager pluginManager)
      {
         _mgr = pluginManager;
         lsv.SuspendLayout();
         lsv.Items.Clear();
         if (_mgr != null)
         {
            foreach (var item in _mgr.Plugins)
            {
               lsv.Items.Add(new ListViewItem(new string[] 
            {
               item.Describe,
               item.Version.ToString(),
               item.Author,
               item.Describe,
               item.SupportUrl,
               item.Name
            }));
            }
         }
         lsv.ResumeLayout();
      }

   }
}
