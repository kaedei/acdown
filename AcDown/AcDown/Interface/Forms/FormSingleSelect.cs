using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaedei.AcDown.Interface.Forms
{
	public partial class FormSingleSelect : System.Windows.Forms.Form
	{
		string[] s = new string[1];

		public FormSingleSelect(ref string[] r, string tip, Dictionary<string, string> keyValueContent, string defaultKey, List<AutoAnswer> autoAnswers, string autoAnswerPrefix)
		{
			s = r;
			InitializeComponent();

			List<KeyValueItem> contents = new List<KeyValueItem>();
			foreach (string item in keyValueContent.Keys)
			{
				contents.Add(new KeyValueItem() { Key = item, Value = keyValueContent[item] });
			}
			foreach (var item in contents)
			{
				combo.Items.Add(item);
			}

			if (autoAnswers != null)
			{
				foreach (AutoAnswer item in autoAnswers)
				{
					if (item.Prefix.Equals(autoAnswerPrefix, StringComparison.CurrentCultureIgnoreCase))
					{
						foreach (KeyValueItem i in combo.Items)
						{
							if (i.Key.Equals(item.Identify, StringComparison.CurrentCultureIgnoreCase))
							{
								s[0] = i.Key;
								//this.Close();
								return;
							}
						}
					}
				}
			}

			//find defaultitem
			foreach (var item in contents)
			{
				if (item.Key.Equals(defaultKey, StringComparison.CurrentCultureIgnoreCase))
				{
					combo.SelectedItem = item;
					break;
				}
			}

			if (combo.SelectedIndex < 0)
				combo.SelectedIndex = 0;

			if (!string.IsNullOrEmpty(tip))
				lblTip.Text = tip;
			formtitle = this.Text;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			s[0] = ((KeyValueItem)combo.SelectedItem).Key;
			this.Close();
		}

		private void FormServer_Load(object sender, EventArgs e)
		{

		}

		private string formtitle;
		private int countdown = GlobalSettings.GetSettings().ToolFormTimeout;
		private void tmr_Tick(object sender, EventArgs e)
		{
			countdown--;
			this.Text = " [" + countdown.ToString() + "]" + formtitle;
			if (countdown == 0)
			{
				btnOK_Click(sender, EventArgs.Empty);
			}
		}
	}//end class

	public class KeyValueItem
	{
		public string Key;
		public string Value;
		public override string ToString()
		{
			return Value;
		}
	}
}
