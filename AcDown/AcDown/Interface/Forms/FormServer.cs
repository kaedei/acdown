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
      int[] s;

      public FormServer(string tipText, string[] serverNames, int defaultSelect, ref int[] selected)
      {
         InitializeComponent();
         combo.Items.AddRange(serverNames);
         combo.SelectedIndex = defaultSelect;
         s = selected;
         if (!string.IsNullOrEmpty(tipText))
            lblTip.Text = tipText;
			formtitle = btnOK.Text;
      }

      private void btnOK_Click(object sender, EventArgs e)
      {
         s[0] = combo.SelectedIndex;
         this.Close();
      }

      private void FormServer_Load(object sender, EventArgs e)
      {

      }

		private string formtitle;
		private int countdown = GlobalSettings.GetSettings().ToolFormTimeout;
		private void tmr_Tick(object sender, EventArgs e)
		{
			countdown--;
			btnOK.Text = formtitle + " [" + countdown.ToString() + "]";
			if (countdown == 0)
			{
				btnOK_Click(sender, EventArgs.Empty);
			}
		}
   }//end class
}
