using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EveMiner.Ores;

namespace EveMiner.Forms
{
	/// <summary>
	/// Calculator Form class
	/// </summary>
	public partial class CalculatorForm : Form
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CalculatorForm"/> class.
		/// </summary>
		public CalculatorForm()
		{
			InitializeComponent();
			numericUpDownStanding.Value = (decimal)Config<Settings>.Instance.Standing;

			pictureBoxTritanium.Tag = "Tritanium";
			pictureBoxPyerite.Tag = "Pyerite";
			pictureBoxMexallon.Tag = "Mexallon";
			pictureBoxIsogen.Tag = "Isogen";
			pictureBoxNocxium.Tag = "Nocxium";
			pictureBoxZydrine.Tag = "Zydrine";
			pictureBoxMegacyte.Tag = "Megacyte";
			pictureBoxMorphite.Tag = "Morphite";

			pictureBoxVeldspar.Tag = OreList.Get("Veldspar");
			pictureBoxScordite.Tag = OreList.Get("Scordite");
			pictureBoxPyroxeres.Tag = OreList.Get("Pyroxeres");
			pictureBoxPlagioclase.Tag = OreList.Get("Plagioclase");
			pictureBoxOmber.Tag = OreList.Get("Omber");
			pictureBoxKernite.Tag = OreList.Get("Kernite");
			pictureBoxJaspet.Tag = OreList.Get("Jaspet");
			pictureBoxHemorphite.Tag = OreList.Get("Hemorphite");
			pictureBoxHedbergite.Tag = OreList.Get("Hedbergite");
			pictureBoxGneiss.Tag = OreList.Get("Gneiss");
			pictureBoxDarkOchre.Tag = OreList.Get("Dark Ochre");
			pictureBoxSpodumain.Tag = OreList.Get("Spodumain");
			pictureBoxCrokite.Tag = OreList.Get("Crokite");
			pictureBoxBistot.Tag = OreList.Get("Bistot");
			pictureBoxArkonor.Tag = OreList.Get("Arkonor");
			pictureBoxMercoxit.Tag = OreList.Get("Mercoxit");

			textBoxPriceTritanium.Text = Config<Settings>.Instance.PriceTritanium.ToString("F2");
			textBoxPricePyerite.Text = Config<Settings>.Instance.PricePyerite.ToString("F2");
			textBoxPriceMexallon.Text = Config<Settings>.Instance.PriceMexallon.ToString("F2");
			textBoxPriceIsogen.Text = Config<Settings>.Instance.PriceIsogen.ToString("F2");
			textBoxPriceNocxium.Text = Config<Settings>.Instance.PriceNocxium.ToString("F2");
			textBoxPriceZydrine.Text = Config<Settings>.Instance.PriceZydrine.ToString("F2");
			textBoxPriceMegacyte.Text = Config<Settings>.Instance.PriceMegacyte.ToString("F2");
			textBoxPriceMorphite.Text = Config<Settings>.Instance.PriceMorphite.ToString("F2");


		}
		/// <summary>
		/// Handles the Click event of the btnEveCentral control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void btnEveCentral_Click(object sender, EventArgs e)
		{
			//TODO http://eve-central.com/api/evemon
		}

		/// <summary>
		/// Handles the Click event of the buttonCalculate control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void buttonCalculate_Click(object sender, EventArgs e)
		{
			double netYield = 0.5;
			int quantity = 0;
			double cargohold = 0.0;
			try
			{
				netYield = Convert.ToDouble(textBoxNetYield.Text) / 100;
				quantity = Convert.ToInt32(textBoxQuantity.Text);
			}
			catch (FormatException)
			{
			}
			try
			{
				cargohold = Convert.ToDouble(textBoxCargohold.Text);
			}
			catch (FormatException)
			{
			}
			Skills skills = Config<Settings>.Instance.Skills;

			dataGridViewCalc.Rows.Clear();
			foreach (KeyValuePair<string, Ore> pair in OreList.DictOre)
			{
				Ore ore = pair.Value;
				double eff = netYield +
							 0.375 * (1 + skills.Refining * 0.02) * (1 + skills.EfficiencyRefining * 0.04) *
							 (1 + OreList.GetProcessingSkill(ore) * 0.05);
				if (eff > 1.0)
					eff = 1.0;

				double nal = (5 - Config<Settings>.Instance.Standing * 5 / 6.66666) / 100;
				if (nal < 0)
					nal = 0;

				if (sender == buttonCalculateCargoHold)
					quantity = (int)(cargohold / ore.Volume);

				int unitProcess = quantity - quantity % ore.UnitsToRefine;
				int p = quantity / ore.UnitsToRefine;

				CalculateProfit(ore, eff, nal, p, unitProcess);
			}

		}
		/// <summary>
		/// Вычилсить прибыль и добавить строчку в таблицу
		/// </summary>
		/// <param name="ore"></param>
		/// <param name="eff"></param>
		/// <param name="nal"></param>
		/// <param name="p"></param>
		/// <param name="unitProcess"></param>
		private void CalculateProfit(Ore ore, double eff, double nal, int p, int unitProcess)
		{
			int mineralsOut = (int)(ore.MineralsOut.Tritanium * p * eff * (1 - nal));
			double profit = mineralsOut * Config<Settings>.Instance.PriceTritanium;
			mineralsOut = (int)(ore.MineralsOut.Pyerite * p * eff * (1 - nal));
			profit += mineralsOut * Config<Settings>.Instance.PricePyerite;
			mineralsOut = (int)(ore.MineralsOut.Mexallon * p * eff * (1 - nal));
			profit += mineralsOut * Config<Settings>.Instance.PriceMexallon;
			mineralsOut = (int)(ore.MineralsOut.Isogen * p * eff * (1 - nal));
			profit += mineralsOut * Config<Settings>.Instance.PriceIsogen;
			mineralsOut = (int)(ore.MineralsOut.Nocxium * p * eff * (1 - nal));
			profit += mineralsOut * Config<Settings>.Instance.PriceNocxium;
			mineralsOut = (int)(ore.MineralsOut.Zydrine * p * eff * (1 - nal));
			profit += mineralsOut * Config<Settings>.Instance.PriceZydrine;
			mineralsOut = (int)(ore.MineralsOut.Megacyte * p * eff * (1 - nal));
			profit += mineralsOut * Config<Settings>.Instance.PriceMegacyte;
			mineralsOut = (int)(ore.MineralsOut.Morphite * p * eff * (1 - nal));
			profit += mineralsOut * Config<Settings>.Instance.PriceMorphite;


			///Добавляем строчку
			DataGridViewRow row = new DataGridViewRow();

			DataGridViewCell[] cells = new DataGridViewCell[dataGridViewCalc.ColumnCount];
			cells[ColumnOreCalc.Index] = new DataGridViewTextBoxCell {Value = ore.Name};
			cells[ColumnVolume.Index] = new DataGridViewTextBoxCell
			                            	{
			                            		Value = (unitProcess * ore.Volume).ToString("F2") + " m3"
			                            	};
			cells[ColumnRefVolume.Index] = new DataGridViewTextBoxCell
			                               	{
			                               		Value = ((ore.MineralsOut.Tritanium +
			                               		          ore.MineralsOut.Pyerite +
			                               		          ore.MineralsOut.Mexallon +
			                               		          ore.MineralsOut.Isogen +
			                               		          ore.MineralsOut.Nocxium +
			                               		          ore.MineralsOut.Zydrine +
			                               		          ore.MineralsOut.Megacyte +
			                               		          ore.MineralsOut.Morphite) * p * eff * 0.01).ToString("F2") + " m3"
			                               	};

			cells[ColumnProfit.Index] = new DataGridViewTextBoxCell {Value = profit.ToString("#,#.##") + " ISK"};
			cells[ColumnDelete2.Index] = new DataGridViewButtonCell {Value = "x"};

			row.Cells.AddRange(cells);
			dataGridViewCalc.Rows.Add(row);
		}

		/// <summary>
		/// Handles the CellClick event of the dataGridViewCalc control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
		private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (sender == dataGridViewCalc)
			{
				if (e.ColumnIndex == ColumnDelete2.Index)
				{
					dataGridViewCalc.Rows.Remove(dataGridViewCalc.Rows[e.RowIndex]);
				}
			}
		}

		/// <summary>
		/// Handles the FormClosing event of the CalculatorForm control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
		private void CalculatorForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			Hide();
		}

		/// <summary>
		/// Handles the ValueChanged event of the numericUpDownStanding control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void numericUpDownStanding_ValueChanged(object sender, EventArgs e)
		{
			if (sender == numericUpDownStanding)
				Config<Settings>.Instance.Standing = (double)numericUpDownStanding.Value;
		}
		/// <summary>
		/// Изменили цену миников
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxPrice_TextChanged(object sender, EventArgs e)
		{
			TextBox box = sender as TextBox;
			double price = 0.0;
			if (box == null)
				return;
			try
			{
				price = Convert.ToDouble(box.Text);
			}
			catch (FormatException)
			{
			}

			if (sender == textBoxPriceTritanium)
				Config<Settings>.Instance.PriceTritanium = price;
			else if (sender == textBoxPricePyerite)
				Config<Settings>.Instance.PricePyerite = price;
			else if (sender == textBoxPriceMexallon)
				Config<Settings>.Instance.PriceMexallon = price;
			else if (sender == textBoxPriceIsogen)
				Config<Settings>.Instance.PriceIsogen = price;
			else if (sender == textBoxPriceNocxium)
				Config<Settings>.Instance.PriceNocxium = price;
			else if (sender == textBoxPriceZydrine)
				Config<Settings>.Instance.PriceZydrine = price;
			else if (sender == textBoxPriceMegacyte)
				Config<Settings>.Instance.PriceMegacyte = price;
			else if (sender == textBoxPriceMorphite)
				Config<Settings>.Instance.PriceMorphite = price;
		}


		#region Tooltips
		/// <summary>
		/// Called when [mouse enter].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void OnMouseEnter(object sender, EventArgs e)
		{
			Control ctrl = sender as Control;
			if (!toolTipInfo.Active && ctrl != null)
			{
				string tooltip = "";
				if (ctrl is PictureBox && ctrl.Tag is Ore)
				{
					toolTipInfo.ToolTipTitle = "Ore";
					if (ctrl.Tag != null)
						tooltip = ((Ore)ctrl.Tag).Name;
				}
				else if (ctrl is PictureBox && ctrl.Tag is string)
				{
					toolTipInfo.ToolTipTitle = "Mineral";
					tooltip = (string)ctrl.Tag;
				}

				if (tooltip.Length > 0)
					toolTipInfo.SetToolTip(ctrl, tooltip);
				toolTipInfo.Active = true;
			}


		}

		/// <summary>
		/// Called when [mouse leave].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void OnMouseLeave(object sender, EventArgs e)
		{
			toolTipInfo.Active = false;
		}
		#endregion
	}
}