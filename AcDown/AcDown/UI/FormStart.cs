using System;
using System.Drawing;
using System.Windows.Forms;
using AcDown;
using AcDown.UI;

namespace Kaedei.AcDown.UI
{
   public partial class FormStart : Form
   {
      DwmApi.MARGINS marg; 

      public FormStart()
      {
         InitializeComponent();
      }

      
      private void FormStart_Load(object sender, EventArgs e)
      {
         //设置AERO效果
         marg = new DwmApi.MARGINS(this.Width, this.Height, this.Width, this.Height);
         if (Config.IsWindowsVistaOrHigher()) //如果是vista以上
         {
            if (DwmApi.DwmIsCompositionEnabled())
            {
               DwmApi.DwmExtendFrameIntoClientArea(this.Handle, marg);
            }
            else //如果AERO被禁用
            {
               this.BackColor = DefaultBackColor;
               this.picIcon.BackColor = DefaultBackColor;
            }
         }
      }

      private void FormStart_Paint(object sender, PaintEventArgs e)
      {
         if (Config.IsWindowsVistaOrHigher())
         {
            Rectangle rect = new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height);
            if (DwmApi.DwmIsCompositionEnabled()) 
            {
               using (SolidBrush blackbrush = new SolidBrush(Color.Black))
               {
                  e.Graphics.FillRectangle(blackbrush, rect);//重绘玻璃部分
               }
            }
         }
      }

      private void FormStart_Shown(object sender, EventArgs e)
      {
         //加载FormMain窗体
         Application.DoEvents();
         Program.frmMain = new FormMain();
         Program.frmMain.Show();
         this.Hide();
      }
   }
}
