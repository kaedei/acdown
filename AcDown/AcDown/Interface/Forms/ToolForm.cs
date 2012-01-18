using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Kaedei.AcDown.Interface.Forms
{
   public class ToolForm
   {
      /// <summary>
      /// 创建[选择章节]窗体
      /// </summary>
      /// <param name="itemContent">一个字符串数组,包括列表中每一项的描述</param>
      /// <returns>返回Boolean数组，指示每一项数据所相对应的值</returns>
      public static BitArray CreateSelctionForm(string[] itemContent)
      {
         BitArray ba = new BitArray(itemContent.Length);
         return CreateSelctionForm(itemContent, ba);
      }

      /// <summary>
      /// 创建[选择章节]窗体
      /// </summary>
      /// <param name="itemContent">一个字符串数组,包括列表中每一项的描述</param>
      /// <param name="selectedItem">一个Boolean数组，指示默认已选中的项</param>
      /// <returns>返回Boolean数组，指示每一项数据所相对应的值</returns>
      public static BitArray CreateSelctionForm(string[] itemContent, BitArray selectedItem)
      {
         FormSelect frm = new FormSelect(ref selectedItem, itemContent);
         frm.ShowDialog();
         return selectedItem;
      }

      /// <summary>
      /// 创建[选择服务器]窗体
      /// </summary>
      /// <param name="serverNames">一个字符串数组，包括对每一个服务器的描述</param>
      /// <param name="defaultIndex">默认选中的列表项</param>
      /// <returns>返回Int32值，指示用户选择的服务器在数组中的索引</returns>
      public static int CreateSelectServerForm(string tip, string[] serverNames, int defaultIndex)
      {
         int[] index = new int[1];
         FormServer frm = new FormServer(tip, serverNames, defaultIndex, ref index);
         frm.ShowDialog();
         return index[0];
      }

      /// <summary>
      /// 创建[输入密码]窗体
      /// </summary>
      /// <returns>返回字符串值，内容为用户输入的密码</returns>
      public static string CreatePasswordForm(bool isPassword,string tipText,string title)
      {
         StringBuilder pw = new StringBuilder();
         FormPassword frm = new FormPassword(pw, isPassword, tipText, title);
         frm.ShowDialog();
         return pw.ToString();
      }

      /// <summary>
      /// 创建[登录]窗体
      /// </summary>
      /// <param name="registerUrl">注册新用户的链接，传递空字符串可以隐藏界面上的[]注册]按钮</param>
      /// <returns>返回一个包含用户登录数据的UserLoginInfo对象</returns>
      public static UserLoginInfo CreateLoginForm(string registerUrl)
      {
         UserLoginInfo info = new UserLoginInfo();
         FormLogin frm = new FormLogin(info, registerUrl);
         frm.ShowDialog();
         return info;
      }

      /// <summary>
      /// 创建[显示网页]窗体
      /// </summary>
      /// <param name="url">需要加载的页面链接</param>
      public static void CreateWebpageForm(string url,string formTitle,bool sizable)
      {
         FormWebbrowser frm = new FormWebbrowser(url, formTitle, sizable);
         frm.ShowDialog();
      }
   }
}
