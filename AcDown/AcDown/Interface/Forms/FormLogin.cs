using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Kaedei.AcDown.Interface.Forms
{
   public partial class FormLogin : Form
   {
      UserLoginInfo info;
      string url;
      public FormLogin(ref UserLoginInfo userLoginInfo, string regUrl)
      {
         InitializeComponent();
         userLoginInfo = info;
         url = regUrl;
      }

      private void FormLogin_Load(object sender, EventArgs e)
      {
         //隐藏"注册"按钮
         if (string.IsNullOrEmpty(url))
         {
            lnkRegister.Visible = false;
         }
      }

      private void lnkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         Process.Start(url);
      }

      private void btnOK_Click(object sender, EventArgs e)
      {
         info.Username = txtUserName.Text;
         info.Password = txtPassword.Text;
         this.Close();
      }

      private void btnCancel_Click(object sender, EventArgs e)
      {
         this.Close();
      }
   } // end class formlogin

   public class UserLoginInfo
   {
      public string Username = "";
      public string Password = "";
   }
}
