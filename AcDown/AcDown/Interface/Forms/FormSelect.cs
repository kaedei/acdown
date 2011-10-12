using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;

namespace Kaedei.AcDown.Interface.Forms
{
   public partial class FormSelect : System.Windows.Forms.Form
   {

      BitArray s;
      //初始化数据
      public FormSelect(ref BitArray selected, string[] content)
      {
         InitializeComponent();
         s = selected;
         this.SuspendLayout();
         foreach (var item in content)
         {
            lst.Items.Add(item);
         }
         this.ResumeLayout();
      }

      //选择所有
      private void lnkSelectAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         for (int i = 0; i < lst.Items.Count; i++)
         {
            lst.SetItemChecked(i, true);
         }
      }

      //不选所有
      private void lnkSelectNone_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         for (int i = 0; i < lst.Items.Count; i++)
         {
            lst.SetItemChecked(i, false);
         }
      }

      //反选所有
      private void lnkSelectInvert_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         for (int i = 0; i < lst.Items.Count; i++)
         {
            lst.SetItemChecked(i, !lst.GetItemChecked(i));
         }
      }

      //点击确定
      private void btnOK_Click(object sender, EventArgs e)
      {
         for (int i = 0; i < s.Length; i++)
         {
            s[i] = lst.GetItemChecked(i);
         }
         //关闭窗口
         this.Close();
      }

      private void FormSelect_Load(object sender, EventArgs e)
      {

      }

      private void lst_SelectedIndexChanged(object sender, EventArgs e)
      {

      }

      private void lst_MouseUp(object sender, MouseEventArgs e)
      {
         if (e.Button == System.Windows.Forms.MouseButtons.Right)
         {
            if (lst.SelectedIndex > 0)
            {
               mnu.Show();
            }
         }
      }

      private void mnuSelectUp_Click(object sender, EventArgs e)
      {
         for (int i = 0; i < lst.SelectedIndex +1; i++)
         {
            lst.SetItemChecked(i, true);
         }
      }

      private void mnuDeselectUp_Click(object sender, EventArgs e)
      {
         for (int i = 0; i < lst.SelectedIndex + 1; i++)
         {
            lst.SetItemChecked(i, false);
         }
      }

      private void mnuSelectDown_Click(object sender, EventArgs e)
      {
         for (int i = lst.SelectedIndex; i < lst.Items.Count; i++)
         {
            lst.SetItemChecked(i, true);
         }
      }

      private void mnuDeselectDown_Click(object sender, EventArgs e)
      {
         for (int i = lst.SelectedIndex; i < lst.Items.Count; i++)
         {
            lst.SetItemChecked(i, false);
         }
      }


   }//end class
}
