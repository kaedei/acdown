using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Kaedei.AcDown.Component;
using Kaedei.AcDown.Interface;

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
               object[] types = item.GetType().GetCustomAttributes(typeof(AcDownPluginInformationAttribute), true);
               if (types.Length > 0)
               {
                  var attrib = (AcDownPluginInformationAttribute)types[0];
                  lsv.Items.Add(new ListViewItem(new string[]
                  {
                     attrib.FriendlyName,
                     attrib.Version.ToString(),
                     attrib.Author,
                     attrib.Describe,
                     attrib.SupportUrl,
                     attrib.Name
                  }));
               }
            }
         }
         lsv.ResumeLayout();
      }

   }
}
