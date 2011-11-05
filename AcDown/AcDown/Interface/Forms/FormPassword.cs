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
      StringBuilder pw;

      public FormPassword(StringBuilder password,bool isPasswordForm,string tipText,string formTitle)
      {
         InitializeComponent();
         pw = password;
         if (isPasswordForm)
            txtPassword.PasswordChar = '●';
         if (!string.IsNullOrEmpty(tipText))
            lblTipText.Text = tipText;
         if (!string.IsNullOrEmpty(formTitle))
            this.Text = formTitle;
         
      }

      private void btnOK_Click(object sender, EventArgs e)
      {
         pw.Append(txtPassword.Text);
         this.Close();
      }

      private void FormPassword_Load(object sender, EventArgs e)
      {

      }


   }
}
