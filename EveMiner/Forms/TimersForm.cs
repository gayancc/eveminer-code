using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EveMiner.Ores;

namespace EveMiner.Forms
{
	/// <summary>
	/// 
	/// </summary>
	public partial class TimersForm : Form
	{
		private readonly Timer _timer = new Timer();
		/// <summary>
		/// Initializes a new instance of the <see cref="TimersForm"/> class.
		/// </summary>
		public TimersForm()
		{
			InitializeComponent();
			FillOreList();
			
			checkBoxUseGangBonuses.Checked = Config<Settings>.Instance.isGang;

			_timer.Tick += TimerTick;
			_timer.Interval = 1000;
			_timer.Start();
		}
		/// <summary>
		/// ��� �������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TimerTick(object sender, EventArgs e)
		{
			for (int n = 0; n < dataGridViewTimers.Rows.Count; n++)
			{
				TimerListItem titem = dataGridViewTimers.Rows[n].Tag as TimerListItem;
				if (titem != null && titem.TimerStarted)
				{
					titem.Tick(_timer.Interval / 1000);
					UpdateTimerListItem(dataGridViewTimers.Rows[n]);
				}
			}
		}
		/// <summary>
		/// ��������� ������� �� �������� ���������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DataGridViewTimersCellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (sender == dataGridViewTimers)
			{
				if (e.ColumnIndex == ColumnButtonStart.Index)
				{
					TimerListItem titem = dataGridViewTimers.Rows[e.RowIndex].Tag as TimerListItem;
					if (titem != null)
						titem.TimerStarted = true;
					else
						dataGridViewTimers.Rows.Remove(dataGridViewTimers.Rows[e.RowIndex]);
				}
				if (e.ColumnIndex == ColumnButtonDelete.Index)
				{
					dataGridViewTimers.Rows.Remove(dataGridViewTimers.Rows[e.RowIndex]);
				}
			}
		}
		/// <summary>
		/// Handles the Click event of the buttonAdd control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void buttonAdd_Click(object sender, EventArgs e)
		{
			Ore ore = OreList.Get(comboBoxOre.SelectedItem.ToString());
			try
			{
				double startVolume = Convert.ToDouble(textBoxStartValue.Text);
				if (startVolume == 0)
					return;
				double cycle = Convert.ToDouble(textBoxCycle.Text);
				double miningYield = Convert.ToDouble(textBoxMiningYield.Text);
				TimerListItem timerListItem = new TimerListItem(ore,
																startVolume,
																cycle,
																miningYield);

				DataGridViewRow row = new DataGridViewRow { Tag = timerListItem };
				DataGridViewCell[] cells = new DataGridViewCell[dataGridViewTimers.ColumnCount];
				cells[ColumnOre.Index] = new DataGridViewTextBoxCell { Value = timerListItem.ore.Name };
				cells[ColumnStartQty.Index] = new DataGridViewTextBoxCell { Value = startVolume };
				cells[ColumnCurrentQty.Index] = new DataGridViewTextBoxCell { Value = startVolume };
				cells[ColumnCycle.Index] = new DataGridViewTextBoxCell { Value = cycle.ToString("F0") };
				cells[ColumnTimeToEnd.Index] = new DataGridViewTextBoxCell
				{
					Value =
						string.Format("{0:00}:{1:00}", Math.Floor(timerListItem.TimeToAsterEnd / 60),
									  timerListItem.TimeToAsterEnd % 60)
				};
				cells[ColumnButtonStart.Index] = new DataGridViewButtonCell { Value = ">" };
				cells[ColumnButtonDelete.Index] = new DataGridViewButtonCell { Value = "x" };

				row.Cells.AddRange(cells);


				dataGridViewTimers.Rows.Add(row);

				UpdateTimerListItem(row);
			}
			catch (FormatException)
			{
				textBoxStartValue.Text = "0";
			}
		}
		/// <summary>
		/// �������� ������ � ������ ��������
		/// </summary>
		/// <param name="row"></param>
		private void UpdateTimerListItem(DataGridViewRow row)
		{
			TimerListItem timerListItem = row.Tag as TimerListItem;
			if (timerListItem != null)
			{
				if (timerListItem.TimeToAsterEnd == 0)
				{
					row.DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 200);
					row.Cells[ColumnButtonStart.Index].Value = "x";
				}
				else if (timerListItem.TimerStarted)
					row.DefaultCellStyle.BackColor = Color.FromArgb(200, 255, 200);

				row.Cells[ColumnTimeToEnd.Index].Value =
					string.Format("{0:00}:{1:00}", Math.Floor(timerListItem.TimeToAsterEnd / 60), timerListItem.TimeToAsterEnd % 60);
				row.Cells[ColumnCycle.Index].Value = timerListItem.TimeToCycleEnd.ToString("F0");
				row.Cells[ColumnCurrentQty.Index].Value = string.Format("{0}", timerListItem.CurrentVolume.ToString("F0"));

				if (timerListItem.TimeToAsterEnd == 0)
					row.Tag = null;
			}
		}
		/// <summary>
		/// Handles the Click event of the buttonUpdate control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void buttonUpdate_Click(object sender, EventArgs e)
		{
			double miningYield = Convert.ToDouble(textBoxMiningYield.Text);
			for (int n = 0; n < dataGridViewTimers.Rows.Count; n++)
			{
				TimerListItem titem = dataGridViewTimers.Rows[n].Tag as TimerListItem;
				if (titem != null)
				{
					titem.SetMiningYield(miningYield);
					UpdateTimerListItem(dataGridViewTimers.Rows[n]);
				}
			}
		}


		/// <summary>
		/// Fills the ore list.
		/// </summary>
		private void FillOreList()
		{
			foreach (KeyValuePair<string, Ore> pair in OreList.DictOre)
			{
				comboBoxOre.Items.Add(pair.Value);
			}
			comboBoxOre.SelectedIndex = 0;
			//Select ore from config
			Ore ore = OreList.Get(Config<Settings>.Instance.SelectedOre);
			if (ore != null)
			{
				for (int n = 0; n < comboBoxOre.Items.Count; n++)
				{
					if (comboBoxOre.Items[n] == ore)
						comboBoxOre.SelectedIndex = n;
				}
			}
		}

		/// <summary>
		/// Handles the FormClosing event of the TimersForm control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
		private void TimersForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			Hide();
		}

		/// <summary>
		/// Sets the yield and cycle time
		/// </summary>
		/// <param name="yield">The yield.</param>
		/// <param name="cycleTime">The cycle time.</param>
		/// <param name="turret">The turret.</param>
		public void SetYieldCycle(double yield, double cycleTime, MiningTurret turret)
		{
			textBoxMiningYield.Text = yield.ToString();
			textBoxCycle.Text = cycleTime.ToString();
			Text = string.Format("{0}m3 / {1}sec - {2}", yield.ToString("F2"), cycleTime.ToString("F0"), turret);
		}

		#region Tooltip
		/// <summary>
		/// Called when [mouse enter CTRL].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void OnMouseEnterCtrl(object sender, EventArgs e)
		{
			Control ctrl = sender as Control;
			if (!toolTipInfo.Active && ctrl != null)
			{
				string tooltip = "";
				if(ctrl == textBoxMiningYield)
				{
					toolTipInfo.ToolTipTitle = "Mining Amount";
					tooltip = string.Format("{0} m3", textBoxMiningYield.Text);
				}
				else if(ctrl == textBoxCycle)
				{
					toolTipInfo.ToolTipTitle = "Laser/Harvester duration";
					tooltip = string.Format("{0} seconds", textBoxCycle.Text);
				}

				if(tooltip.Length > 0)
					toolTipInfo.SetToolTip(ctrl, tooltip);
				toolTipInfo.Active = true;
			}
		}

		/// <summary>
		/// Called when [mouse leave CTRL].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void OnMouseLeaveCtrl(object sender, EventArgs e)
		{
			toolTipInfo.Active = false;
		}
		#endregion

	}
}