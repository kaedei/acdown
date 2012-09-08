using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Kaedei.AcDown.Interface.UI;

namespace Kaedei.AcDown.UI
{
   public partial class FormSingle : FormBase
   {
      public FormSingle(string[] args)
      {
         InitializeComponent();
      }

      private void FormSingle_Load(object sender, EventArgs e)
      {
         this.Icon = Properties.Resources.Ac;
      }
   }
}
