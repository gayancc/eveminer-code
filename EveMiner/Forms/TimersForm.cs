using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using EveMiner.Ores;
using EveMiner.Properties;

namespace EveMiner.Forms
{
	/// <summary>
	/// 
	/// </summary>
	public partial class TimersForm : Form
	{
		private readonly Timer _timer = new Timer();

		/// <summary>
		/// текущая заполненость прогрессбара
		/// </summary>
		private double _currentCargo;


		/// <summary>
		/// Initializes a new instance of the <see cref="TimersForm"/> class.
		/// </summary>
		public TimersForm()
		{
			InitializeComponent();
			FillOreList();
			textBoxCargo.Text = Config<Settings>.Instance.CargoHold.ToString("F0");
			_currentCargo = 0.0;
			progressBar1.Maximum = Convert.ToInt32(Config<Settings>.Instance.CargoHold);
			SetProgressCargo();

			_timer.Tick += TimerTick;
			_timer.Interval = 1000;
			_timer.Start();
		}

		/// <summary>
		/// Sets the progress cargo.
		/// </summary>
		private void SetProgressCargo()
		{
			int val = Convert.ToInt32(_currentCargo);
			if (val > progressBar1.Maximum)
				val = progressBar1.Maximum;
			progressBar1.Value = val;
			progressBar1.Text = _currentCargo.ToString("F2") + Resources.TimersForm_SetProgressCargo__m3;
		}

		/// <summary>
		/// Тик таймера
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TimerTick(object sender, EventArgs e)
		{
			for (int n = 0; n < dataGridViewTimers.Rows.Count; n++)
			{
				TimerListItem titem = dataGridViewTimers.Rows[n].Tag as TimerListItem;
				if (titem != null && titem.IsSatrted)
				{
					UpdateTimerListItem(dataGridViewTimers.Rows[n]);
					_currentCargo += Config<Settings>.Instance.MiningAmount/Config<Settings>.Instance.Cycle*titem.LasersCount;
					SetProgressCargo();
					//SetProgressCycles();
				}
			}
		}

		/// <summary>
		/// Обработка нажатий на кнопочки датагрида
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DataGridViewTimersCellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (sender == dataGridViewTimers)
			{
				if (e.ColumnIndex == ColumnLaser1Start.Index)
				{
					if (e.RowIndex == -1)
						return;
					DataGridViewRow row = dataGridViewTimers.Rows[e.RowIndex];

					if (e.ColumnIndex == ColumnLaser1Start.Index)
						ChangeTurretState(row, e.ColumnIndex);
				}
				if (e.ColumnIndex == ColumnButtonDelete.Index)
				{
					if (e.RowIndex == -1)
						RemoveRows();
					else
						RemoveRow(e.RowIndex);
				}
			}
		}
		/// <summary>
		/// Handles the CellValueChanged event of the dataGridViewTimers control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
		private void DataGridViewTimersCellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (sender == dataGridViewTimers)
			{
				if(e.ColumnIndex == ColumnLasers.Index)
				{
					
				}
			}


		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="row"></param>
		/// <param name="indexColumn"></param>
	    private void ChangeTurretState(DataGridViewRow row, int indexColumn)
        {
			TimerListItem tItem = row.Tag as TimerListItem;
			if(tItem == null)
				return;
	    	bool bState = !tItem.IsEnableTurret();
			int count = Convert.ToInt32(row.Cells[ColumnLasers.Index].Value);
			tItem.EnableTurret(bState, count);
	        if(bState)
	        {
	        	row.Cells[indexColumn].Value = Resources.stop_24;
	        }
	        else
	        {
				row.Cells[indexColumn].Value = Resources.play_24;
	        }
			UpdateTimerListItem(row);
	    }

		/// <summary>
		/// Removes the row.
		/// </summary>
		/// <param name="index">The index.</param>
		void RemoveRow(int index)
		{
			if(index >=0)
			{
				DataGridViewRow row = dataGridViewTimers.Rows[index];
				TimerListItem titem = row.Tag as TimerListItem;
				if (titem != null)
					titem.StopTurrets();
				dataGridViewTimers.Rows.Remove(dataGridViewTimers.Rows[index]);
			}
		}
		/// <summary>
		/// 
		/// </summary>
		void RemoveRows()
		{
			foreach (DataGridViewRow row in dataGridViewTimers.Rows)
			{
				TimerListItem titem = row.Tag as TimerListItem;
				if (titem != null)
					titem.StopTurrets();
			}
			dataGridViewTimers.Rows.Clear();
		}

		/// <summary>
		/// Handles the Click event of the buttonAdd control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void ButtonAddClick(object sender, EventArgs e)
		{
			Ore ore = OreList.Get(comboBoxOre.SelectedItem.ToString());
			try
			{
				double startVolume = Convert.ToDouble(textBoxStartValue.Text);
				if (startVolume == 0)
					return;
				double cycle = Config<Settings>.Instance.Cycle;
				double miningYield = Config<Settings>.Instance.MiningAmount;
				TimerListItem timerListItem = new TimerListItem(ore,
				                                                startVolume,
				                                                cycle,
				                                                miningYield);

				DataGridViewRow row = new DataGridViewRow {Tag = timerListItem};
				DataGridViewCell[] cells = new DataGridViewCell[dataGridViewTimers.ColumnCount];
				cells[ColumnOre.Index] = new DataGridViewTextBoxCell {Value = timerListItem.ore.Name};
				cells[ColumnStartQty.Index] = new DataGridViewTextBoxCell {Value = startVolume};
				cells[ColumnCurrentQty.Index] = new DataGridViewTextBoxCell {Value = startVolume};
				cells[ColumnTimeToEnd.Index] = new DataGridViewTextBoxCell
				                               	{
				                               		Value =
				                               			string.Format("{0:00}:{1:00}", Math.Floor(timerListItem.TimeToAsterEnd/60),
				                               			              timerListItem.TimeToAsterEnd%60)
				                               	};
				cells[ColumnLaser1Start.Index] = new DataGridViewImageCell {Value = Resources.play_24};
				cells[ColumnCycle.Index] = new DataGridViewTextBoxCell
				                           	{
				                           		Value =
				                           			string.Format("{0:00}:{1:00}", Math.Floor(timerListItem.TimeToCycleEnd / 60),
				                           			              timerListItem.TimeToCycleEnd % 60)

				                           	};
				DataGridViewComboBoxCell cell = new DataGridViewComboBoxCell();
				cell.Items.AddRange("1", "2", "3", "4", "5", "6", "7", "8");
				cell.Value = "1";
				cells[ColumnLasers.Index] = cell;
				
				
				//cells[ColumnButtonDelete.Index] = new DataGridViewButtonCell {Value = "x"};
				cells[ColumnButtonDelete.Index] = new DataGridViewImageCell {Value = Resources.close_24};

				row.Cells.AddRange(cells);


				dataGridViewTimers.Rows.Add(row);

				UpdateTimerListItem(row);
				textBoxStartValue.SelectionStart = 0;
				textBoxStartValue.SelectionLength = textBoxStartValue.TextLength;
			}
			catch (FormatException)
			{
				textBoxStartValue.Text = Resources.TimersForm_ButtonAddClick__0;
			}
		}

		/// <summary>
		/// Обновить таймер в списке таймеров
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
					timerListItem.StopTurrets();
				}
				else if (timerListItem.LasersCount > 0 && timerListItem.IsSatrted)
				{
					row.DefaultCellStyle.BackColor = timerListItem.IsEmptyClose
					                                 	? Color.FromArgb(255, 255, 200)
					                                 	: Color.FromArgb(200, 255, 200);
				}
				
				


				row.Cells[ColumnTimeToEnd.Index].Value =
					string.Format("{0:00}:{1:00}", Math.Floor(timerListItem.TimeToAsterEnd/60), timerListItem.TimeToAsterEnd%60);
				row.Cells[ColumnCycle.Index].Value =
					string.Format("{0:00}:{1:00}", Math.Floor(timerListItem.TimeToCycleEnd / 60), timerListItem.TimeToCycleEnd % 60);
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
		private void ButtonUpdateClick(object sender, EventArgs e)
		{
			double miningYield = Config<Settings>.Instance.MiningAmount;
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
			else
				comboBoxOre.SelectedIndex = 0;
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
			Text = string.Format("{0}m3 / {1}sec - {2}", yield.ToString("F2"), cycleTime.ToString("F2"), turret);
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
				if (ctrl == textBoxCargo)
				{
					toolTipInfo.ToolTipTitle = "Cargohold";
					tooltip = string.Format("{0:0} m3", Config<Settings>.Instance.CargoHold);
				}
				else if (ctrl == btnReset)
				{
					toolTipInfo.ToolTipTitle = "";
					tooltip = "Reset cargo";
				}
				else if (ctrl == progressBar1)
				{
					toolTipInfo.ToolTipTitle = "";
					tooltip = _currentCargo.ToString("F2") + Resources.TimersForm_SetProgressCargo__m3;
				}

				if (tooltip.Length > 0)
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

		/// <summary>
		/// Handles the SelectedIndexChanged event of the comboBoxOre control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void ComboBoxOreSelectedIndexChanged(object sender, EventArgs e)
		{
			Config<Settings>.Instance.SelectedOre = comboBoxOre.SelectedItem.ToString();
		}

		/// <summary>
		/// Handles the TextChanged event of the textBoxCargo control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void TextBoxCargoTextChanged(object sender, EventArgs e)
		{
			try
			{
				if (textBoxCargo.Text.Length > 0)
					Config<Settings>.Instance.CargoHold = Convert.ToDouble(textBoxCargo.Text);
				else
					Config<Settings>.Instance.CargoHold = 0;
				progressBar1.Maximum = Convert.ToInt32(Config<Settings>.Instance.CargoHold);
			}
			catch (FormatException)
			{
				textBoxCargo.Text = Config<Settings>.Instance.CargoHold.ToString("F0");
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnResetClick(object sender, EventArgs e)
		{
			_currentCargo = 0.0;
			SetProgressCargo();
		}

	}
}