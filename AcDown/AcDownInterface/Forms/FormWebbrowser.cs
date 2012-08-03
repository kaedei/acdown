using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaedei.AcDown.Interface.Forms
{
   public partial class FormWebbrowser : Form
   {
      private string u;
      private string t;
      private bool s;
      public FormWebbrowser(string url,string title,bool sizeable)
      {
         u = url;
         t = title;
         s = sizeable;
         InitializeComponent();
      }

      private void FormWebbrowser_Load(object sender, EventArgs e)
      {
         this.Text = t;
         if (s)
         {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
         }
         else
         {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
         }
         web.Navigate(u);
      }
   }
}
