using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Kaedei.AcDown.UI.Components
{
   public partial class WebsitesControl : UserControl
   {
      public WebsitesControl()
      {
         InitializeComponent();
      }

      private void DownloadPicture()
      {
         //取得APPDATA路径名称
         string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
         path = Path.Combine(path, @"Kaedei\AcDown\Icons\");
         if (!Directory.Exists(path))
         {
            //如果目录不存在则创建
            Directory.CreateDirectory(path);
         }

         //添加字典
         Dictionary<int, string> icons = new Dictionary<int, string>();
         icons.Add(0, @"http://www.flvcd.com/flvcd_logo.jpg"); //flvcd
         icons.Add(1, @"http://www.acfun.tv/templets/images/logo.gif"); //acfun.tv
         icons.Add(2, @"http://static.loli.my/images/mini-logo.gif"); //bilibili.tv
         icons.Add(3, @"http://res.youku.com/1001201011152358511669AA96BACD8772C8CC573A298B8B68.png"); //优酷
         icons.Add(4, @"http://js.imanhua.com/template/default/images/imanhua.gif"); //爱漫画
         icons.Add(5, @""); //土豆
         icons.Add(6, @"http://img.baidu.com/img/post-jg.gif"); //贴吧

         //加载文件
         for (int i = 0; i < icons.Count; i++)
         {
            //图片文件名
            string file = Path.Combine(path, i.ToString() + ".bmp");
            if (File.Exists(file))
            {
               
            }
               

         }

      }
   }
}
