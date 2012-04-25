using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;

namespace Kaedei.AcDown.Interface.Forms
{
	public class ToolForm
	{
		/// <summary>
		/// 创建[选择章节]窗体
		/// </summary>
		/// <param name="keyValueContent">一个类型为string-string的字典对象，分别对应“关键字”和“提示语”</param>
		/// <param name="autoAnswers">自动应答设置</param>
		/// <param name="autoAnswerPrefix">自动应答的前缀</param>
		/// <returns>返回string集合，指示每一项用户选择的数据所相对应的值</returns>
		public static Collection<string> CreateMultiSelectForm(Dictionary<string, string> keyValueContent, List<AutoAnswer> autoAnswers, string autoAnswerPrefix)
		{
			Collection<string> c = new Collection<string>();
			FormMultiSelect form = new FormMultiSelect(ref c, keyValueContent, autoAnswers, autoAnswerPrefix);
			if (c.Count <= 0)
				form.ShowDialog();
			return c;
		}


		/// <summary>
		/// 创建[选择服务器]窗体
		/// </summary>
		/// <param name="tip">对话框中显示的文字提示</param>
		/// <param name="tipValueContent">一个类型为string-string的字典对象，分别对应“关键字”和“提示语”</param>
		/// <param name="defaultKey">默认选中的值</param>
		/// <param name="autoAnswers">自动应答设置</param>
		/// <param name="autoAnswerPrefix">自动应答的前缀</param>
		/// <returns>返回string值，指示用户选择项对应的值</returns>
		public static string CreateSingleSelectForm(string tip, Dictionary<string, string> keyValueContent, string defaultKey, List<AutoAnswer> autoAnswers, string autoAnswerPrefix)
		{
			string[] index = new string[1];
			FormSingleSelect frm = new FormSingleSelect(ref index, tip, keyValueContent, defaultKey, autoAnswers, autoAnswerPrefix);
			if (string.IsNullOrEmpty(index[0]))
				frm.ShowDialog();
			return index[0];
		}

		/// <summary>
		/// 创建[输入密码]窗体
		/// </summary>
		/// <returns>返回字符串值，内容为用户输入的密码</returns>
		public static string CreatePasswordForm(bool isPassword, string tipText, string title)
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
		public static void CreateWebpageForm(string url, string formTitle, bool sizable)
		{
			FormWebbrowser frm = new FormWebbrowser(url, formTitle, sizable);
			frm.ShowDialog();
		}
	}
}
