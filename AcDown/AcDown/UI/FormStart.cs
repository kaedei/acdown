using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using Kaedei.AcDown.Core;

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
            if (DwmApi.DwmIsCompositionEnabled()) //如果dwm被启用了（有AERO效果）
            {
               this.BackColor = Color.Black;
               this.picIcon.BackColor = Color.Black;
               DwmApi.DwmExtendFrameIntoClientArea(this.Handle, marg);
            }
         }
      }

      //窗体重绘事件
      private void FormStart_Paint(object sender, PaintEventArgs e)
      {
         if (Config.IsWindowsVistaOrHigher())
         {
            //定义重绘的矩形范围
            Rectangle rect = new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height);
            //如果操作系统dwm启用
            if (DwmApi.DwmIsCompositionEnabled()) 
            {
               //使用黑色画刷进行重绘
               using (SolidBrush blackbrush = new SolidBrush(Color.Black))
               {
                  e.Graphics.FillRectangle(blackbrush, rect);//重绘玻璃部分
               }
            }
         }
      }

      //窗体显示时
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
