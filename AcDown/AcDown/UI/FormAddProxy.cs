using System;
using System.Windows.Forms;
using Kaedei.AcDown.Interface;
using Kaedei.AcDown.Interface.UI;

namespace Kaedei.AcDown.UI
{
   public partial class FormAddProxy : FormBase
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
         txtAddress.Text = p.Address;
         txtPort.Text = p.Port.ToString();
         txtUsername.Text = p.Username;
         txtPassword.Text = p.Password;
      }

      private void btnOK_Click(object sender, EventArgs e)
      {
         if (txtName.Text.Trim() == "")
         {
            MessageBox.Show("代理服务器名称不能为空", "代理服务器", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
         }
         if (txtAddress.Text.Trim() == "")
         {
            MessageBox.Show("代理服务器地址不能为空", "代理服务器", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
         }
         p.Name = txtName.Text;
         p.Address = txtAddress.Text;
         p.Port = int.Parse(txtPort.Text);
         p.Username = txtUsername.Text;
         p.Password = txtPassword.Text;
         this.Close();
      }

      private void btnCancel_Click(object sender, EventArgs e)
      {
         p = null;
         this.Close();
      }


      private void txtPort_KeyPress(object sender, KeyPressEventArgs e)
      {
         if (!"0123456789".Contains(e.KeyChar.ToString()))
         {
            e.Handled = true;
         }
      }
   }
}
