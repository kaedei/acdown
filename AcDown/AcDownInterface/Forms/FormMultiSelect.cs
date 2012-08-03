using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace Kaedei.AcDown.Interface.Forms
{
	public partial class FormMultiSelect : System.Windows.Forms.Form
	{

		Collection<string> s;
		//初始化数据
		public FormMultiSelect(ref Collection<string> r, Dictionary<string, string> keyValueContent, List<AutoAnswer> autoAnswers, string autoAnswerPrefix)
		{
			InitializeComponent();
			formtitle = this.Text;
			s = r;

			if (autoAnswers != null)
			{
				foreach (AutoAnswer item in autoAnswers)
				{
					if (item.Prefix.Equals(autoAnswerPrefix, StringComparison.CurrentCultureIgnoreCase))
					{
						foreach (string i in keyValueContent.Keys)
						{
							if (i.Equals(item.Identify, StringComparison.CurrentCultureIgnoreCase))
							{
								s.Add(i);
							}
						}
					}
				}
			}
			if (s.Count > 0)
				return;

			this.SuspendLayout();
			foreach (var item in keyValueContent.Keys)
			{
				var lvi = new ListViewItem(keyValueContent[item]);
				lvi.Tag = item;
				lsv.Items.Add(lvi);
			}
			this.ResumeLayout();
		}

		//选择所有
		private void lnkSelectAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			for (int i = 0; i < lsv.Items.Count; i++)
			{
				lsv.Items[i].Checked = true;
			}
		}

		//不选所有
		private void lnkSelectNone_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			for (int i = 0; i < lsv.Items.Count; i++)
			{
				lsv.Items[i].Checked = false;
			}
		}

		//反选所有
		private void lnkSelectInvert_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			for (int i = 0; i < lsv.Items.Count; i++)
			{
				lsv.Items[i].Checked = !lsv.Items[i].Checked;
			}
		}

		//点击确定
		private void btnOK_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < lsv.Items.Count; i++)
			{
				var lvi = lsv.Items[i];
				if (lvi.Checked)
				{
					string key = lvi.Tag.ToString();
					s.Add(key);
				}
			}
			//关闭窗口
			this.Close();
		}

		//右键显示菜单
		private void lst_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				if (lsv.SelectedIndices.Count > 0)
				{
					mnu.Show(lsv, e.Location);
				}
			}
		}

		private void mnuSelectUp_Click(object sender, EventArgs e)
		{
			if (lsv.SelectedIndices.Count > 0)
			{
				int selected = lsv.SelectedIndices[0];

				for (int i = 0; i < selected + 1; i++)
				{
					lsv.Items[i].Checked = true;
				}
			}
		}

		//取消选中上方所有
		private void mnuDeselectUp_Click(object sender, EventArgs e)
		{
			if (lsv.SelectedIndices.Count > 0)
			{
				int selected = lsv.SelectedIndices[0];

				for (int i = 0; i < selected + 1; i++)
				{
					lsv.Items[i].Checked = false;
				}
			}
		}

		//选中下方
		private void mnuSelectDown_Click(object sender, EventArgs e)
		{
			if (lsv.SelectedIndices.Count > 0)
			{
				int selected = lsv.SelectedIndices[0];

				for (int i = selected; i < lsv.Items.Count; i++)
				{
					lsv.Items[i].Checked = true;
				}
			}
		}

		//取消选中下方所有
		private void mnuDeselectDown_Click(object sender, EventArgs e)
		{
			if (lsv.SelectedIndices.Count > 0)
			{
				int selected = lsv.SelectedIndices[0];

				for (int i = selected; i < lsv.Items.Count; i++)
				{
					lsv.Items[i].Checked = false;
				}
			}
		}

		private string formtitle;
		private int countdown = GlobalSettings.GetSettings().ToolFormTimeout * 2;
		private void tmr_Tick(object sender, EventArgs e)
		{
			countdown--;
			this.Text = "[" + countdown.ToString() + "]" + formtitle;
			if (countdown == 0)
			{
				btnOK_Click(sender, EventArgs.Empty);
			}
		}


	}//end class
}
