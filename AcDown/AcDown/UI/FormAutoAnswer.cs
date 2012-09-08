using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Kaedei.AcDown.Interface;
using Kaedei.AcDown.Interface.UI;

namespace Kaedei.AcDown.UI
{
	public partial class FormAutoAnswer : FormBase
	{
		MethodInvoker refresh;
		private List<AutoAnswer> aa;

		public FormAutoAnswer(List<AutoAnswer> autoAnswers)
		{
			InitializeComponent();
			aa = autoAnswers;
			formtitle = this.Text;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		}

		private void FormAutoAnswer_Load(object sender, EventArgs e)
		{
			refresh = () =>
			{
				lsv.Items.Clear();
				foreach (var item in aa)
				{
					lsv.Items.Add(new ListViewItem(new string[]{
						item.Description,
						item.Prefix,
						item.Identify
					}));
				}
			};
			refresh.Invoke();
		}

		private void lsv_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lsv.SelectedIndices.Count > 0)
			{
				int selected = lsv.SelectedIndices[0];
				if (selected != 0)
					btnUp.Enabled = true;
				if (selected != lsv.Items.Count - 1)
					btnDown.Enabled = true;
				btnDelete.Enabled = true;
			}
			else
			{
				btnUp.Enabled = false;
				btnDown.Enabled = false;
				btnDelete.Enabled = false;
			}
		}

		//向上移动
		private void btnUp_Click(object sender, EventArgs e)
		{
			int selected = lsv.SelectedIndices[0];
			if (selected == 0) return;
			AutoAnswer temp = aa[selected];
			aa[selected] = aa[selected - 1];
			aa[selected - 1] = temp;
			refresh.Invoke();
			lsv.Items[selected - 1].Selected = true;
			lsv.Select();
		}

		//向下移动
		private void btnDown_Click(object sender, EventArgs e)
		{
			int selected = lsv.SelectedIndices[0];
			if (selected == lsv.Items.Count - 1) return;
			AutoAnswer temp = aa[selected];
			aa[selected] = aa[selected + 1];
			aa[selected + 1] = temp;
			refresh.Invoke();
			lsv.Items[selected + 1].Selected = true;
			lsv.Select();
		}

		//删除
		private void btnDelete_Click(object sender, EventArgs e)
		{
			int selected = lsv.SelectedIndices[0];
			if (selected < 0) return;
			aa.RemoveAt(selected);
			refresh.Invoke();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		private int countdown = GlobalSettings.GetSettings().ToolFormTimeout;
		private string formtitle;
		private void tmr_Tick(object sender, EventArgs e)
		{
			countdown--;
			this.Text = "[" + countdown.ToString() + "]" + formtitle;
			if (countdown == 0)
			{
				btnOK_Click(sender, EventArgs.Empty);
			}
		}


	}
}
