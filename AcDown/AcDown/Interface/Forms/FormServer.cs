using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaedei.AcDown.Interface.Forms
{
   public partial class FormServer : System.Windows.Forms.Form
   {
      int s;
      public FormServer(string[] serverNames,int defaultSelect,ref int selected)
      {
         InitializeComponent();
         combo.Items.AddRange(serverNames);
         combo.SelectedIndex = defaultSelect;
         selected = s;
      }

      private void btnOK_Click(object sender, EventArgs e)
      {
         s = combo.SelectedIndex;
         this.Close();
      }

      private void FormServer_Load(object sender, EventArgs e)
      {

      }
   }//end class
}
