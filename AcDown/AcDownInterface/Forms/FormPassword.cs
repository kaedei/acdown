using System;
using System.Text;
using Kaedei.AcDown.Interface.UI;

namespace Kaedei.AcDown.Interface.Forms
{
   public partial class FormPassword : FormBase
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
			formtitle = this.Text;
      }

      private void btnOK_Click(object sender, EventArgs e)
      {
         pw.Append(txtPassword.Text);
         this.Close();
      }

      private void FormPassword_Load(object sender, EventArgs e)
      {

      }

		private string formtitle;
		private int countdown = GlobalSettings.GetSettings().ToolFormTimeout;
		private void tmr_Tick(object sender, EventArgs e)
		{
			countdown--;
			this.Text = "[" + countdown.ToString() + "]" + formtitle;
			if (countdown == 0)
			{
				btnOK_Click(sender, EventArgs.Empty);
			}
		}


   }
}
