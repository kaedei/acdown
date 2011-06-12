using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaedei.AcDown.Interface.Forms
{
   public partial class FormPassword : Form
   {
      string pw;
      public FormPassword(ref string password)
      {
         InitializeComponent();
         password = pw;
      }

      private void btnOK_Click(object sender, EventArgs e)
      {
         pw = txtPassword.Text;
         this.Close();
      }


   }
}
