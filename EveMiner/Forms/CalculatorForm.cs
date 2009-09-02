using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using System.Xml;
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
			numericUpDownStanding.Value = (decimal) Config<Settings>.Instance.Standing;

			pictureBoxTritanium.Tag = MineralList.Get("Tritanium");
			textBoxPriceTritanium.Tag = MineralList.Get("Tritanium");
			pictureBoxPyerite.Tag = MineralList.Get("Pyerite");
			textBoxPricePyerite.Tag = MineralList.Get("Pyerite");
			pictureBoxMexallon.Tag = MineralList.Get("Mexallon");
			textBoxPriceMexallon.Tag = MineralList.Get("Mexallon");
			pictureBoxIsogen.Tag = MineralList.Get("Isogen");
			textBoxPriceIsogen.Tag = MineralList.Get("Isogen");
			pictureBoxNocxium.Tag = MineralList.Get("Nocxium");
			textBoxPriceNocxium.Tag = MineralList.Get("Nocxium");
			pictureBoxZydrine.Tag = MineralList.Get("Zydrine");
			textBoxPriceZydrine.Tag = MineralList.Get("Zydrine");
			pictureBoxMegacyte.Tag = MineralList.Get("Megacyte");
			textBoxPriceMegacyte.Tag = MineralList.Get("Megacyte");
			pictureBoxMorphite.Tag = MineralList.Get("Morphite");
			textBoxPriceMorphite.Tag = MineralList.Get("Morphite");

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

			PutMineralPrices();

			histogram1.ShowLabels = true;
			histogram1.ShowValues = true;
		}

		/// <summary>
		/// Puts the mineral prices.
		/// </summary>
		private void PutMineralPrices()
		{
			textBoxPriceTritanium.Text = MineralList.Get("Tritanium").Price.ToString("F2");
			textBoxPricePyerite.Text = MineralList.Get("Pyerite").Price.ToString("F2"); 
			textBoxPriceMexallon.Text = MineralList.Get("Mexallon").Price.ToString("F2"); 
			textBoxPriceIsogen.Text = MineralList.Get("Isogen").Price.ToString("F2"); 
			textBoxPriceNocxium.Text = MineralList.Get("Nocxium").Price.ToString("F2"); 
			textBoxPriceZydrine.Text = MineralList.Get("Zydrine").Price.ToString("F2"); 
			textBoxPriceMegacyte.Text = MineralList.Get("Megacyte").Price.ToString("F2"); 
			textBoxPriceMorphite.Text = MineralList.Get("Morphite").Price.ToString("F2");
		}

		/// <summary>
		/// Handles the Click event of the btnEveCentral control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void btnEveCentral_Click(object sender, EventArgs e)
		{
			// Строки: URI и имя локального файла
			const string webAddress = "http://eve-central.com/api/evemon";
			const string localAddress = "EveCentral.xml";

			try
			{

				// Два объекта для получения информации о предполагаемом скачиваемом xml
				HttpWebRequest httpWReq = (HttpWebRequest) WebRequest.Create(webAddress);
				WebClient httpClient = new WebClient();
				HttpWebResponse httpWResp = (HttpWebResponse) httpWReq.GetResponse();
				// Проверяем,  действительно ли по данному адресу находится xml
				if(httpWResp.ContentType == "text/xml")
				{
					// Скачиваем
					httpClient.DownloadFile(webAddress, localAddress);

				}
				httpWResp.Close();
			}
			catch (WebException ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}

			try
			{
				using (XmlTextReader reader = new XmlTextReader(localAddress))
				{
					Mineral min = null;
					while (reader.Read())
					{
						switch (reader.NodeType)
						{
							case XmlNodeType.Text:
								{
									if (min == null)
										min = MineralList.Get(reader.Value);
									else
									{
										min.Price = Convert.ToDouble(reader.Value);
										min = null;
									}
									break;
								}
						}
					}
				}
			}
			catch(XmlException)
			{}

			PutMineralPrices();


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
			catch(FormatException)
			{}
			try
			{
				cargohold = Convert.ToDouble(textBoxCargohold.Text);
			}
			catch(FormatException)
			{}


			dataGridViewCalc.Rows.Clear();
			foreach(KeyValuePair<string, Ore> pair in OreList.DictOre)
			{
				Ore ore = pair.Value;

				if(sender == buttonCalculateCargoHold)
					quantity = (int) (cargohold / ore.Volume);

				int unitProcess = quantity - quantity % ore.UnitsToRefine;
				int p = quantity / ore.UnitsToRefine;

				InsertProfitLine(ore, netYield, p, unitProcess);
			}
		}

		/// <summary>
		/// Возвращает текущий налог
		/// </summary>
		/// <returns></returns>
		private static double GetNal()
		{
			double nal = (5 - Config<Settings>.Instance.Standing * 5 / 6.66666) / 100;
			if(nal < 0)
				nal = 0;
			return nal;
		}

		/// <summary>
		/// Вычилсить прибыль и добавить строчку в таблицу
		/// </summary>
		/// <param name="ore">тип руды</param>
		/// <param name="netYield">The net yield.</param>
		/// <param name="p">число циклов рефайна</param>
		/// <param name="unitProcess">количество руды для процессинга</param>
		private void InsertProfitLine(Ore ore, double netYield, int p, int unitProcess)
		{
			MineralsOut minout = GetMineralsOut(ore, netYield, p);
			double profit = GetMineralProfit(minout);

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
			                               		          ore.MineralsOut.Morphite) * p *
			                               		         OreList.GetEfficiency(ore, netYield) * 0.01).ToString("F2") + " m3"
			                               	};

			cells[ColumnProfit.Index] = new DataGridViewTextBoxCell {Value = profit.ToString("#,#.##") + " ISK"};
			cells[ColumnDelete2.Index] = new DataGridViewButtonCell {Value = "x"};

			row.Cells.AddRange(cells);
			dataGridViewCalc.Rows.Add(row);
		}

		/// <summary>
		/// Вычилсить количество минералов которое выйдет с руды
		/// </summary>
		/// <param name="ore">тип руды</param>
		/// <param name="netYield">The net yield.</param>
		/// <param name="p">число циклов рефайна</param>
		/// <returns></returns>
		private static MineralsOut GetMineralsOut(Ore ore, double netYield, int p)
		{
			double coeff = p * OreList.GetEfficiency(ore, netYield) * (1 - GetNal());
			int tritaniumOut = (int) (ore.MineralsOut.Tritanium * coeff);
			int pyeriteOut = (int) (ore.MineralsOut.Pyerite * coeff);
			int mexallonOut = (int) (ore.MineralsOut.Mexallon * coeff);
			int isogenOut = (int) (ore.MineralsOut.Isogen * coeff);
			int nocxiumOut = (int) (ore.MineralsOut.Nocxium * coeff);
			int zydrineOut = (int) (ore.MineralsOut.Zydrine * coeff);
			int megcyteOut = (int) (ore.MineralsOut.Megacyte * coeff);
			int morphiteOut = (int) (ore.MineralsOut.Morphite * coeff);
			return new MineralsOut(tritaniumOut, pyeriteOut, mexallonOut, isogenOut,
			                       nocxiumOut, zydrineOut, megcyteOut, morphiteOut);
		}

		/// <summary>
		/// Gets the mineral profit.
		/// </summary>
		/// <param name="minerals">The minerals.</param>
		/// <returns></returns>
		private static double GetMineralProfit(MineralsOut minerals)
		{
			double profit = minerals.Tritanium * Config<Settings>.Instance.PriceTritanium;
			profit += minerals.Pyerite * Config<Settings>.Instance.PricePyerite;
			profit += minerals.Mexallon * Config<Settings>.Instance.PriceMexallon;
			profit += minerals.Isogen * Config<Settings>.Instance.PriceIsogen;
			profit += minerals.Nocxium * Config<Settings>.Instance.PriceNocxium;
			profit += minerals.Zydrine * Config<Settings>.Instance.PriceZydrine;
			profit += minerals.Megacyte * Config<Settings>.Instance.PriceMegacyte;
			profit += minerals.Morphite * Config<Settings>.Instance.PriceMorphite;
			return profit;
		}

		/// <summary>
		/// Handles the CellClick event of the dataGridViewCalc control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
		private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if(sender == dataGridViewCalc)
			{
				if(e.ColumnIndex == ColumnDelete2.Index)
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
			if(sender == numericUpDownStanding)
				Config<Settings>.Instance.Standing = (double) numericUpDownStanding.Value;
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
			if(box == null)
				return;
			try
			{
				price = Convert.ToDouble(box.Text);
			}
			catch(FormatException)
			{}
			Mineral m = box.Tag as Mineral;
			if(m != null)
				m.Price = price;

			if(sender == textBoxPriceTritanium)
				Config<Settings>.Instance.PriceTritanium = price;
			else if(sender == textBoxPricePyerite)
				Config<Settings>.Instance.PricePyerite = price;
			else if(sender == textBoxPriceMexallon)
				Config<Settings>.Instance.PriceMexallon = price;
			else if(sender == textBoxPriceIsogen)
				Config<Settings>.Instance.PriceIsogen = price;
			else if(sender == textBoxPriceNocxium)
				Config<Settings>.Instance.PriceNocxium = price;
			else if(sender == textBoxPriceZydrine)
				Config<Settings>.Instance.PriceZydrine = price;
			else if(sender == textBoxPriceMegacyte)
				Config<Settings>.Instance.PriceMegacyte = price;
			else if(sender == textBoxPriceMorphite)
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
			if(!toolTipInfo.Active && ctrl != null)
			{
				string tooltip = "";
				if(ctrl is PictureBox && ctrl.Tag is Ore)
				{
					toolTipInfo.ToolTipTitle = "Ore";
					if(ctrl.Tag != null)
						tooltip = ((Ore) ctrl.Tag).Name;
				}
				else if(ctrl is PictureBox && ctrl.Tag is Mineral)
				{
					toolTipInfo.ToolTipTitle = "Mineral";
					Mineral m = ctrl.Tag as Mineral;
					tooltip = m.Name + Environment.NewLine + "price: " + m.Price.ToString("F3");
				}

				if(tooltip.Length > 0)
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
		/// <summary>
		/// Histogram1_s the bar enter event.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="args">The args.</param>
		private void histogram1_BarEnterEvent(object sender, HistogramEnterEventHandlerArgs args)
		{
			if (!toolTipInfo.Active)
			{
				toolTipInfo.ToolTipTitle = args.Bar.Name;
				string tooltip = (args.Bar.Value * 1000).ToString("#,#.##") + " ISK";
				toolTipInfo.SetToolTip((Control)sender, tooltip);

				toolTipInfo.Active = true;
			}

		}

		/// <summary>
		/// Histogram1_s the bar leave event.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="args">The args.</param>
		private void histogram1_BarLeaveEvent(object sender, HistogramEnterEventHandlerArgs args)
		{
			toolTipInfo.Active = false;
		}

		#endregion

		/// <summary>
		/// Gets the quantity.
		/// </summary>
		/// <param name="control">The control.</param>
		/// <returns></returns>
		private static int GetQuantity(Control control)
		{
			int quantity = 0;
			try
			{
				quantity = Convert.ToInt32(control.Text);
			}
			catch(FormatException)
			{}
			return quantity;
		}

		/// <summary>
		/// Handles the Click event of the btnCalculateBars control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void btnCalculateBars_Click(object sender, EventArgs e)
		{
			double netYield = 0.5;
			try
			{
				netYield = Convert.ToDouble(textBoxNetYield.Text) / 100;
			}
			catch(FormatException)
			{}
			//Veldspar
			int quantity = GetQuantity(textBoxVeldspar0);
			Ore ore = OreList.Get("Veldspar");
			MineralsOut minerals = GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxVeldspar5);
			ore = OreList.Get("Concentrated Veldspar");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxVeldspar10);
			ore = OreList.Get("Dense Veldspar");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			
			//Scordite
			quantity = GetQuantity(textBoxScordite0);
			ore = OreList.Get("Scordite");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxScordite5);
			ore = OreList.Get("Condensed Scordite");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxScordite10);
			ore = OreList.Get("Massive Scordite");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);

			//Pyroxeres
			quantity = GetQuantity(textBoxPyroxeres0);
			ore = OreList.Get("Pyroxeres");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxPyroxeres5);
			ore = OreList.Get("Solid Pyroxeres");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxPyroxeres10);
			ore = OreList.Get("Viscous Pyroxeres");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);

			//Plagioclase
			quantity = GetQuantity(textBoxPlagioclase0);
			ore = OreList.Get("Plagioclase");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxPlagioclase5);
			ore = OreList.Get("Azure Plagioclase");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxPlagioclase10);
			ore = OreList.Get("Rich Plagioclase");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);

			//Omber
			quantity = GetQuantity(textBoxOmber0);
			ore = OreList.Get("Omber");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxOmber5);
			ore = OreList.Get("Silvery Omber");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxOmber10);
			ore = OreList.Get("Golden Omber");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);

			//Kernite
			quantity = GetQuantity(textBoxKernite0);
			ore = OreList.Get("Kernite");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxKernite5);
			ore = OreList.Get("Luminous Kernite");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxKernite10);
			ore = OreList.Get("Fiery Kernite");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);

			//Jaspet
			quantity = GetQuantity(textBoxJaspet0);
			ore = OreList.Get("Jaspet");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxJaspet5);
			ore = OreList.Get("Pure Jaspet");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxJaspet10);
			ore = OreList.Get("Pristine Jaspet");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);

			//Hemorphite
			quantity = GetQuantity(textBoxHemorphite0);
			ore = OreList.Get("Hemorphite");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxHemorphite5);
			ore = OreList.Get("Vivid Hemorphite");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxHemorphite10);
			ore = OreList.Get("Radiant Hemorphite");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);

			//Hedbergite
			quantity = GetQuantity(textBoxHedbergite0);
			ore = OreList.Get("Hedbergite");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxHedbergite5);
			ore = OreList.Get("Vitric Hedbergite");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxHedbergite10);
			ore = OreList.Get("Glazed Hedbergite");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);

			//Gneiss
			quantity = GetQuantity(textBoxGneiss0);
			ore = OreList.Get("Gneiss");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxGneiss5);
			ore = OreList.Get("Iridescent Gneiss");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxGneiss10);
			ore = OreList.Get("Prismatic Gneiss");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);

			//DarkOchre
			quantity = GetQuantity(textBoxDarkOchre0);
			ore = OreList.Get("Dark Ochre");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxDarkOchre5);
			ore = OreList.Get("Onyx Ochre");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxDarkOchre10);
			ore = OreList.Get("Obsidian Ochre");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);

			//Spodumain
			quantity = GetQuantity(textBoxSpodumain0);
			ore = OreList.Get("Spodumain");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxSpodumain5);
			ore = OreList.Get("Bright Spodumain");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxSpodumain10);
			ore = OreList.Get("Gleaming Spodumain");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);

			//Crokite
			quantity = GetQuantity(textBoxCrockite0);
			ore = OreList.Get("Crokite");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxCrockite5);
			ore = OreList.Get("Sharp Crokite");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxCrockite10);
			ore = OreList.Get("Crystalline Crokite");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);

			//Bistot
			quantity = GetQuantity(textBoxBistot0);
			ore = OreList.Get("Bistot");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxBistot5);
			ore = OreList.Get("Triclinic Bistot");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxBistot10);
			ore = OreList.Get("Monoclinic Bistot");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);

			//Arkonor
			quantity = GetQuantity(textBoxArkonor0);
			ore = OreList.Get("Arkonor");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxArkonor5);
			ore = OreList.Get("Crimson Arkonor");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxArkonor10);
			ore = OreList.Get("Prime Arkonor");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);

			//Mercoxit
			quantity = GetQuantity(textBoxMercoxit0);
			ore = OreList.Get("Mercoxit");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxMercoxit5);
			ore = OreList.Get("Magma Mercoxit");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);
			quantity = GetQuantity(textBoxMercoxit10);
			ore = OreList.Get("Vitreous Mercoxit");
			minerals += GetMineralsOut(ore, netYield, quantity / ore.UnitsToRefine);


			RefreshHistogram(minerals);
		}

		/// <summary>
		/// Refreshes the histogram.
		/// </summary>
		/// <param name="minerals">The minerals.</param>
		private void RefreshHistogram(MineralsOut minerals)
		{
			histogram1.Suffix = "k Isk";
			histogram1.ListBars.Clear();
			Bar bar = new Bar
			                    	{
			                    		Color1 = Color.FromArgb(255, 228, 175),
			                    		Color2 = Color.FromArgb(88, 61, 31),
			                    		Name = "Tritanium",
			                    		Value = minerals.Tritanium * Config<Settings>.Instance.PriceTritanium / 1000
			                    	};
			histogram1.ListBars.Add(bar);

			bar = new Bar
			      	{
						Color1 = Color.FromArgb(0xff, 0xa3, 0x66),
						Color2 = Color.FromArgb(0x5c, 0x2c, 0x18),
			      		Name = "Pyerite",
			      		Value = minerals.Pyerite * Config<Settings>.Instance.PricePyerite / 1000
			      	};
			histogram1.ListBars.Add(bar);

			bar = new Bar
			      	{
						Color1 = Color.FromArgb(0xf2, 0xff ,0xc7),
						Color2 = Color.FromArgb(0x3c, 0x50, 0x18),
			      		Name = "Mexallon",
			      		Value = minerals.Mexallon * Config<Settings>.Instance.PriceMexallon / 1000
			      	};
			histogram1.ListBars.Add(bar);

			bar = new Bar
			      	{
						Color1 = Color.FromArgb(0xe4, 0xf3, 0xf7),
						Color2 = Color.FromArgb(0x2a, 0x80, 0x9c),
			      		Name = "Isogen",
			      		Value = minerals.Isogen * Config<Settings>.Instance.PriceIsogen / 1000
			      	};
			histogram1.ListBars.Add(bar);

			bar = new Bar
			      	{
						Color1 = Color.Silver,
			      		Name = "Nocxium",
			      		Value = minerals.Nocxium * Config<Settings>.Instance.PriceNocxium / 1000
			      	};
			histogram1.ListBars.Add(bar);
			bar = new Bar
			      	{
						Color1 = Color.FromArgb(0x54, 0x98, 0x3a),
						Color2 = Color.FromArgb(0x0b, 0x30, 0x10),
			      		Name = "Zydrine",
			      		Value = minerals.Zydrine * Config<Settings>.Instance.PriceZydrine / 1000
			      	};
			histogram1.ListBars.Add(bar);
			bar = new Bar
			      	{
						Color1 = Color.FromArgb(0xc7, 0xbb, 0xa8),
						Color2 = Color.FromArgb(0x44, 0x37, 0x24),
			      		Name = "Megacyte",
			      		Value = minerals.Megacyte * Config<Settings>.Instance.PriceMegacyte / 1000
			      	};
			histogram1.ListBars.Add(bar);
			bar = new Bar
			      	{
						Color1 = Color.FromArgb(0xda, 0x4e, 0x40),
						Color2 = Color.FromArgb(0x48, 0x04, 0x03),
						//Color1 = Color.DarkRed,
			      		Name = "Morphite",
			      		Value = minerals.Morphite * Config<Settings>.Instance.PriceMorphite / 1000
			      	};
			histogram1.ListBars.Add(bar);


			histogram1.Invalidate();
			textBoxProfit.Text = "Total Profit:" + GetMineralProfit(minerals).ToString("#,#.##") + " ISK";
		}

	}
}