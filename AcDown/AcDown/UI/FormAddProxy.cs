using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaedei.AcDown.UI
{
   public partial class FormAddProxy : Form
   {
      private AcDownProxy p;
      public FormAddProxy(AcDownProxy proxy)
      {
         if (proxy != null)
            p = proxy;
         else
            proxy = new AcDownProxy();
         InitializeComponent();
      }

      private void FormAddProxy_Load(object sender, EventArgs e)
      {
         txtName.Text = p.Name;
         txtAddress.Text = p.Adress;
         txtPort.Text = p.Port.ToString();
         txtUsername.Text = p.Username;
         txtPassword.Text = p.Password;
      }

      private void btnOK_Click(object sender, EventArgs e)
      {
         p.Name = txtName.Text;
         p.Adress = txtAddress.Text;
         p.Port = int.Parse(txtPort.Text);
         p.Username = txtUsername.Text;
         p.Password = txtPassword.Text;
         this.Close();
      }

      private void btnCancel_Click(object sender, EventArgs e)
      {
         this.Close();
      }
   }
}
