using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using EveMiner.EveDatabase;
using EveMiner.Properties;

namespace EveMiner.Forms
{
    /// <summary>
    /// Calculator Form class
    /// </summary>
    public partial class CalculatorForm : Form
    {
	    private Ship _ship;

	    private class RowComparer : IComparer
        {
            private static int _sortOrderModifier = 1;
            private readonly int _column;

            public RowComparer(SortOrder sortOrder, int column)
            {
                if (sortOrder == SortOrder.Descending)
                {
                    _sortOrderModifier = -1;
                }
                else if (sortOrder == SortOrder.Ascending)
                {
                    _sortOrderModifier = 1;
                }
                _column = column;
            }

            public int Compare(object x, object y)
            {
                DataGridViewRow dataGridViewRow1 = (DataGridViewRow) x;
                DataGridViewRow dataGridViewRow2 = (DataGridViewRow) y;
                double val1 = 0.0;
                double val2 = 0.0;
                try
                {
                    val1 = Convert.ToDouble(dataGridViewRow1.Cells[_column].Value);
                }
                catch (FormatException) { }
                try
                {
                    val2 = Convert.ToDouble(dataGridViewRow2.Cells[_column].Value);
                }
                catch (FormatException) { }

                int compareResult = val1.CompareTo(val2);
                return compareResult*_sortOrderModifier;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculatorForm"/> class.
        /// </summary>
        public CalculatorForm()
        {
            InitializeComponent();
            comboStandTax.SelectedIndex = Config<Settings>.Instance.StandTaxe;


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
        private void BtnEveCentralClick(object sender, EventArgs e)
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
				//string type = httpWResp.ContentType.Substring(0, "text/xml".Length);
                //if (type == "text/xml")
                //{
                    // Скачиваем
                    httpClient.DownloadFile(webAddress, localAddress);
                //}
                httpWResp.Close();
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            try
            {
                System.Globalization.NumberFormatInfo info = new System.Globalization.NumberFormatInfo();
                info.NumberDecimalSeparator = ".";

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
                                        min.Price = Convert.ToDouble(reader.Value, info);
                                        min = null;
                                    }
                                    break;
                                }
                        }
                    }
                }
            }
            catch (XmlException)
            {
            }

            PutMineralPrices();
        }


        /// <summary>
        /// Handles the Click event of the buttonCalculate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ButtonCalculateClick(object sender, EventArgs e)
        {
            double netYield = 0.5;
            int quantity = 0;
            double cargohold = 0.0;
            try
            {
                netYield = Convert.ToDouble(textBoxNetYield.Text)/100;
                quantity = Convert.ToInt32(textBoxQuantity.Text);
            }
            catch (FormatException)
            {
            }
            try
            {
                cargohold = Convert.ToDouble(textBoxCargohold.Text);
				if(sender == btnIskPerHour)
				{
					cargohold = Config<Settings>.Instance.MiningAmount * 3600 / Config<Settings>.Instance.Cycle * _ship.TurretSlots;
				}

            }
            catch (FormatException)
            {
            }


            dataGridViewCalc.Rows.Clear();
            double maxprofit = 0;
            DataGridViewRow maxrowLowOre = null;
            DataGridViewRow maxrowHiOre = null;
            bool bLowOre = true;
            foreach (KeyValuePair<string, Ore> pair in OreList.DictOre)
            {
                Ore ore = pair.Value;
                if (ore.Name == "Gneiss")
                    bLowOre = false;

                if (sender == buttonCalculateCargoHold || sender == btnIskPerHour)
                    quantity = (int) (cargohold/ore.Volume);
	            double prof = InsertProfitLine(ore, netYield, quantity);
                if (prof > maxprofit)
                {
                    maxprofit = prof;
                    if (bLowOre)
                        maxrowLowOre = dataGridViewCalc.Rows[dataGridViewCalc.Rows.Count - 1];
                    else
                        maxrowHiOre = dataGridViewCalc.Rows[dataGridViewCalc.Rows.Count - 1];
                }
            }
            if (maxrowLowOre != null)
            {
                maxrowLowOre.DefaultCellStyle.BackColor = Color.LightGreen;
            }
            if (maxrowHiOre != null)
            {
                maxrowHiOre.DefaultCellStyle.BackColor = Color.LightGreen;
            }
        }

	    /// <summary>
        /// Вычилсить прибыль и добавить строчку в таблицу
        /// </summary>
        /// <param name="ore">тип руды</param>
        /// <param name="netYield">The net yield.</param>
        /// <param name="quantity">количество руды для процессинга</param>
        private double InsertProfitLine(Ore ore, double netYield, int quantity)
        {
			int unitProcess = quantity - quantity % ore.UnitsToRefine;
			int numberOfCycles = quantity / ore.UnitsToRefine;

			MineralsOut minout = ore.GetMineralsOut(netYield, quantity);
            double profit = minout.GetMineralProfit();

            //Добавляем строчку
            DataGridViewRow row = new DataGridViewRow();

            DataGridViewCell[] cells = new DataGridViewCell[dataGridViewCalc.ColumnCount];
            cells[ColumnOreCalc.Index] = new DataGridViewTextBoxCell {Value = ore.Name};
            cells[ColumnVolume.Index] = new DataGridViewTextBoxCell
                                            {
                                                Value = (unitProcess*ore.Volume).ToString("F2") // + " m3"
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
							ore.MineralsOut.Morphite) * numberOfCycles *
							ore.GetEfficiency(netYield) * 0.01).ToString("F2")
            };

            cells[ColumnProfit.Index] = new DataGridViewTextBoxCell {Value = profit.ToString("#,#.##")}; // + " ISK"};
            cells[ColumnDelete2.Index] = new DataGridViewButtonCell {Value = "x"};

            row.Cells.AddRange(cells);
            dataGridViewCalc.Rows.Add(row);
            return profit;
        }

        /// <summary>
        /// Handles the CellClick event of the dataGridViewCalc control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DataGridViewCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender == dataGridViewCalc)
            {
                if (e.ColumnIndex == ColumnDelete2.Index)
                {
                    if (e.RowIndex == -1)
                        dataGridViewCalc.Rows.Clear();
                    else
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
        private void NumericUpDownStandingValueChanged(object sender, EventArgs e)
        {
            if (sender == numericUpDownStanding)
                if (comboStandTax.SelectedIndex == Settings.Stand)
                    Config<Settings>.Instance.Standing = (double) numericUpDownStanding.Value;
                else
                    Config<Settings>.Instance.TaxRate = (double) numericUpDownStanding.Value;
        }

        /// <summary>
        /// Изменили цену миников
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxPriceTextChanged(object sender, EventArgs e)
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
            Mineral m = box.Tag as Mineral;
            if (m != null)
                m.Price = price;

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
            		Ore ore = (Ore) ctrl.Tag;
            		tooltip = ore.Name;
            		try
            		{
            			double netYield = Convert.ToDouble(textBoxNetYield.Text)/100;
            			tooltip += string.Format(Environment.NewLine + "Efficiency: {0}", ore.GetEfficiency(netYield));
            		}
            		catch (FormatException)
            		{}

                }
                else if (ctrl is PictureBox && ctrl.Tag is Mineral)
                {
                    toolTipInfo.ToolTipTitle = "Mineral";
                    Mineral m = ctrl.Tag as Mineral;
                    tooltip = m.Name + Environment.NewLine + "price: " + m.Price.ToString("F3");
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

        /// <summary>
        /// Histogram1_s the bar enter event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        private void Histogram1BarEnterEvent(object sender, HistogramEnterEventHandlerArgs args)
        {
            if (!toolTipInfo.Active)
            {
                toolTipInfo.ToolTipTitle = args.Bar.Name;
                string tooltip = (args.Bar.Value*1000).ToString("#,#.##") + Resources.CalculatorForm_RefreshHistogram__ISK;
                toolTipInfo.SetToolTip((Control) sender, tooltip);

                toolTipInfo.Active = true;
            }
        }

        /// <summary>
        /// Histogram1_s the bar leave event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        private void Histogram1BarLeaveEvent(object sender, HistogramEnterEventHandlerArgs args)
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
            catch (FormatException)
            {
            }
            return quantity;
        }

        /// <summary>
        /// Handles the Click event of the btnCalculateBars control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnCalculateBarsClick(object sender, EventArgs e)
        {
            double netYield = 0.5;
            try
            {
                netYield = Convert.ToDouble(textBoxNetYield.Text)/100;
            }
            catch (FormatException)
            {
            }
            //Veldspar
            int quantity = GetQuantity(textBoxVeldspar0);
            Ore ore = OreList.Get("Veldspar");
            MineralsOut minerals = ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxVeldspar5);
            ore = OreList.Get("Concentrated Veldspar");
            minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxVeldspar10);
            ore = OreList.Get("Dense Veldspar");
			minerals += ore.GetMineralsOut(netYield, quantity);

            //Scordite
            quantity = GetQuantity(textBoxScordite0);
            ore = OreList.Get("Scordite");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxScordite5);
            ore = OreList.Get("Condensed Scordite");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxScordite10);
            ore = OreList.Get("Massive Scordite");
			minerals += ore.GetMineralsOut(netYield, quantity);

            //Pyroxeres
            quantity = GetQuantity(textBoxPyroxeres0);
            ore = OreList.Get("Pyroxeres");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxPyroxeres5);
            ore = OreList.Get("Solid Pyroxeres");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxPyroxeres10);
            ore = OreList.Get("Viscous Pyroxeres");
			minerals += ore.GetMineralsOut(netYield, quantity);

            //Plagioclase
            quantity = GetQuantity(textBoxPlagioclase0);
            ore = OreList.Get("Plagioclase");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxPlagioclase5);
            ore = OreList.Get("Azure Plagioclase");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxPlagioclase10);
            ore = OreList.Get("Rich Plagioclase");
			minerals += ore.GetMineralsOut(netYield, quantity);

            //Omber
            quantity = GetQuantity(textBoxOmber0);
            ore = OreList.Get("Omber");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxOmber5);
            ore = OreList.Get("Silvery Omber");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxOmber10);
            ore = OreList.Get("Golden Omber");
			minerals += ore.GetMineralsOut(netYield, quantity);

            //Kernite
            quantity = GetQuantity(textBoxKernite0);
            ore = OreList.Get("Kernite");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxKernite5);
            ore = OreList.Get("Luminous Kernite");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxKernite10);
            ore = OreList.Get("Fiery Kernite");
			minerals += ore.GetMineralsOut(netYield, quantity);

            //Jaspet
            quantity = GetQuantity(textBoxJaspet0);
            ore = OreList.Get("Jaspet");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxJaspet5);
            ore = OreList.Get("Pure Jaspet");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxJaspet10);
            ore = OreList.Get("Pristine Jaspet");
			minerals += ore.GetMineralsOut(netYield, quantity);

            //Hemorphite
            quantity = GetQuantity(textBoxHemorphite0);
            ore = OreList.Get("Hemorphite");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxHemorphite5);
            ore = OreList.Get("Vivid Hemorphite");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxHemorphite10);
            ore = OreList.Get("Radiant Hemorphite");
			minerals += ore.GetMineralsOut(netYield, quantity);

            //Hedbergite
            quantity = GetQuantity(textBoxHedbergite0);
            ore = OreList.Get("Hedbergite");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxHedbergite5);
            ore = OreList.Get("Vitric Hedbergite");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxHedbergite10);
            ore = OreList.Get("Glazed Hedbergite");
			minerals += ore.GetMineralsOut(netYield, quantity);

            //Gneiss
            quantity = GetQuantity(textBoxGneiss0);
            ore = OreList.Get("Gneiss");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxGneiss5);
            ore = OreList.Get("Iridescent Gneiss");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxGneiss10);
            ore = OreList.Get("Prismatic Gneiss");
			minerals += ore.GetMineralsOut(netYield, quantity);

            //DarkOchre
            quantity = GetQuantity(textBoxDarkOchre0);
            ore = OreList.Get("Dark Ochre");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxDarkOchre5);
            ore = OreList.Get("Onyx Ochre");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxDarkOchre10);
            ore = OreList.Get("Obsidian Ochre");
			minerals += ore.GetMineralsOut(netYield, quantity);

            //Spodumain
            quantity = GetQuantity(textBoxSpodumain0);
            ore = OreList.Get("Spodumain");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxSpodumain5);
            ore = OreList.Get("Bright Spodumain");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxSpodumain10);
            ore = OreList.Get("Gleaming Spodumain");
			minerals += ore.GetMineralsOut(netYield, quantity);

            //Crokite
            quantity = GetQuantity(textBoxCrockite0);
            ore = OreList.Get("Crokite");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxCrockite5);
            ore = OreList.Get("Sharp Crokite");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxCrockite10);
            ore = OreList.Get("Crystalline Crokite");
			minerals += ore.GetMineralsOut(netYield, quantity);

            //Bistot
            quantity = GetQuantity(textBoxBistot0);
            ore = OreList.Get("Bistot");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxBistot5);
            ore = OreList.Get("Triclinic Bistot");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxBistot10);
            ore = OreList.Get("Monoclinic Bistot");
			minerals += ore.GetMineralsOut(netYield, quantity);

            //Arkonor
            quantity = GetQuantity(textBoxArkonor0);
            ore = OreList.Get("Arkonor");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxArkonor5);
            ore = OreList.Get("Crimson Arkonor");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxArkonor10);
            ore = OreList.Get("Prime Arkonor");
			minerals += ore.GetMineralsOut(netYield, quantity);

            //Mercoxit
            quantity = GetQuantity(textBoxMercoxit0);
            ore = OreList.Get("Mercoxit");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxMercoxit5);
            ore = OreList.Get("Magma Mercoxit");
			minerals += ore.GetMineralsOut(netYield, quantity);
            quantity = GetQuantity(textBoxMercoxit10);
            ore = OreList.Get("Vitreous Mercoxit");
			minerals += ore.GetMineralsOut(netYield, quantity);


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
                              Value = minerals.Tritanium*Config<Settings>.Instance.PriceTritanium/1000
                          };
            histogram1.ListBars.Add(bar);

            bar = new Bar
                      {
                          Color1 = Color.FromArgb(0xff, 0xa3, 0x66),
                          Color2 = Color.FromArgb(0x5c, 0x2c, 0x18),
                          Name = "Pyerite",
                          Value = minerals.Pyerite*Config<Settings>.Instance.PricePyerite/1000
                      };
            histogram1.ListBars.Add(bar);

            bar = new Bar
                      {
                          Color1 = Color.FromArgb(0xf2, 0xff, 0xc7),
                          Color2 = Color.FromArgb(0x3c, 0x50, 0x18),
                          Name = "Mexallon",
                          Value = minerals.Mexallon*Config<Settings>.Instance.PriceMexallon/1000
                      };
            histogram1.ListBars.Add(bar);

            bar = new Bar
                      {
                          Color1 = Color.FromArgb(0xe4, 0xf3, 0xf7),
                          Color2 = Color.FromArgb(0x2a, 0x80, 0x9c),
                          Name = "Isogen",
                          Value = minerals.Isogen*Config<Settings>.Instance.PriceIsogen/1000
                      };
            histogram1.ListBars.Add(bar);

            bar = new Bar
                      {
                          Color1 = Color.Silver,
                          Name = "Nocxium",
                          Value = minerals.Nocxium*Config<Settings>.Instance.PriceNocxium/1000
                      };
            histogram1.ListBars.Add(bar);
            bar = new Bar
                      {
                          Color1 = Color.FromArgb(0x54, 0x98, 0x3a),
                          Color2 = Color.FromArgb(0x0b, 0x30, 0x10),
                          Name = "Zydrine",
                          Value = minerals.Zydrine*Config<Settings>.Instance.PriceZydrine/1000
                      };
            histogram1.ListBars.Add(bar);
            bar = new Bar
                      {
                          Color1 = Color.FromArgb(0xc7, 0xbb, 0xa8),
                          Color2 = Color.FromArgb(0x44, 0x37, 0x24),
                          Name = "Megacyte",
                          Value = minerals.Megacyte*Config<Settings>.Instance.PriceMegacyte/1000
                      };
            histogram1.ListBars.Add(bar);
            bar = new Bar
                      {
                          Color1 = Color.FromArgb(0xda, 0x4e, 0x40),
                          Color2 = Color.FromArgb(0x48, 0x04, 0x03),
                          //Color1 = Color.DarkRed,
                          Name = "Morphite",
                          Value = minerals.Morphite*Config<Settings>.Instance.PriceMorphite/1000
                      };
            histogram1.ListBars.Add(bar);


            histogram1.Invalidate();
			textBoxProfit.Text = Resources.CalculatorForm_RefreshHistogram_Total_Profit__ + minerals.GetMineralProfit().ToString("#,#.##") + Resources.CalculatorForm_RefreshHistogram__ISK;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboStandTaxSelectedIndexChanged(object sender, EventArgs e)
        {
            Config<Settings>.Instance.StandTaxe = comboStandTax.SelectedIndex;
            if (comboStandTax.SelectedIndex == Settings.Stand)
                numericUpDownStanding.Value = (decimal) Config<Settings>.Instance.Standing;
            else
                numericUpDownStanding.Value = (decimal) Config<Settings>.Instance.TaxRate;
        }

        private void BtnExportXlsClick(object sender, EventArgs e)
        {
            GridToXmlConverter conv = new GridToXmlConverter();
            conv.ExportIntoXml(dataGridViewCalc);
        }

        private void DataGridViewCalcColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == ColumnOreCalc.Index || e.ColumnIndex == ColumnDelete2.Index)
                return;
            DataGridViewColumn col = dataGridViewCalc.Columns[e.ColumnIndex];
            
            if(col != null)
            {
                SortOrder order = col.HeaderCell.SortGlyphDirection == SortOrder.Ascending
                                             ? SortOrder.Descending
                                             : SortOrder.Ascending;
                col.HeaderCell.SortGlyphDirection = order;
                dataGridViewCalc.Sort(new RowComparer(order, e.ColumnIndex));
                
            }
		}

	    public void SetShip(Ship ship)
	    {
		    _ship = ship;
			Text = string.Format("Calculator - {0} - {1} turrets",
			ship.Name, ship.TurretSlots);
	    }
    }
}