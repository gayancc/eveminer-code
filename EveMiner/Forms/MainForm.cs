using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using EveMiner.Forms;
using EveMiner.Ores;
using EveMiner.Properties;

namespace EveMiner.Forms
{
	public partial class MainForm : Form
	{
		private readonly Dictionary<string, MiningTurret> turretsList = new Dictionary<string, MiningTurret>();
		private readonly Dictionary<string, Ore> dictOre = new Dictionary<string, Ore>();
		private readonly Dictionary<string, LaserUpgrade> dictMlu = new Dictionary<string, LaserUpgrade>();
		private readonly Dictionary<string, Ship> dictShips = new Dictionary<string, Ship>();

		readonly TimersForm timersForm = new TimersForm();
		
		//Выбранный MLU
		private PictureBox mluPictureClicked = null;

		private readonly Timer timer = new Timer();

		public MainForm()
		{
			InitializeComponent();
			FillMinigLaserUpgrades();
			FillTurretList();
			FillOreList();
			FillShips();
			LoadCfg();

			notifyIcon.ContextMenuStrip = contextMenuTray;
			
			timer.Tick += timer_Tick;
			timer.Interval = 1000;
			timer.Start();
		}


		/// <summary>
		/// Тик таймера
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer_Tick(object sender, EventArgs e)
		{
			for (int n = 0; n < dataGridViewTimers.Rows.Count; n++)
			{
				TimerListItem titem = dataGridViewTimers.Rows[n].Tag as TimerListItem;
				if (titem != null && titem.TimerStarted)
				{
					titem.Tick(timer.Interval / 1000);
					UpdateTimerListItem(dataGridViewTimers.Rows[n]);
				}
			}
		}

		/// <summary>
		/// Загрузка конфига
		/// </summary>
		private void LoadCfg()
		{
			LaserUpgrade mlu;

			if (dictMlu.ContainsKey(Config<Settings>.Instance.Mlu1))
			{
				mlu = dictMlu[Config<Settings>.Instance.Mlu1];
				pictureBoxMLU1.Image = mlu.image;
				pictureBoxMLU1.Tag = mlu;
			}

			if (dictMlu.ContainsKey(Config<Settings>.Instance.Mlu2))
			{
				mlu = dictMlu[Config<Settings>.Instance.Mlu2];
				pictureBoxMLU2.Image = mlu.image;
				pictureBoxMLU2.Tag = mlu;
			}

			if (dictMlu.ContainsKey(Config<Settings>.Instance.Mlu3))
			{
				mlu = dictMlu[Config<Settings>.Instance.Mlu3];
				pictureBoxMLU3.Image = mlu.image;
				pictureBoxMLU3.Tag = mlu;
			}

			if (turretsList.ContainsKey(Config<Settings>.Instance.SelectedTurret))
			{
				MiningTurret turret = turretsList[Config<Settings>.Instance.SelectedTurret];
				for (int n = 0; n < comboBoxTurret.Items.Count; n++)
				{
					if (comboBoxTurret.Items[n] == turret)
						comboBoxTurret.SelectedIndex = n;
				}
			}
			
			if (dictShips.ContainsKey(Config<Settings>.Instance.SelectedShip))
			{
				Ship ship = dictShips[Config<Settings>.Instance.SelectedShip];
				for (int n = 0; n < comboBoxShip.Items.Count; n++)
				{
					if (comboBoxShip.Items[n] == ship)
						comboBoxShip.SelectedIndex = n;
				}
			}

			if (Config<Settings>.Instance.SelectedCrystals == 2)
				radioButtonT2Crystals.Checked = true;
			else
				radioButtonT1Crystals.Checked = true;

			CheckUseCrystals();

			if (dictOre.ContainsKey(Config<Settings>.Instance.SelectedOre))
			{
				Ore ore = dictOre[Config<Settings>.Instance.SelectedOre];
				for (int n = 0; n < comboBoxOre.Items.Count; n++)
				{
					if (comboBoxOre.Items[n] == ore)
						comboBoxOre.SelectedIndex = n;
				}
			}


			TopMost = Config<Settings>.Instance.AlwaysOnTop;

			numericUpDownMining.Value = Config<Settings>.Instance.skills.Mining;
			numericUpDownAstrogeology.Value = Config<Settings>.Instance.skills.Astrogeology;
			numericUpDownMiningBarge.Value = Config<Settings>.Instance.skills.MiningBarge;
			numericUpDownExhumers.Value = Config<Settings>.Instance.skills.Exhumers;
			numericUpDownMiningForeman.Value = Config<Settings>.Instance.skills.MiningForeman;
			numericUpDownMiningDirector.Value = Config<Settings>.Instance.skills.MiningDirector;
			numericUpDownIceHarvesting.Value = Config<Settings>.Instance.skills.IceHarvesting;
			numericUpDownWarfareLinkSpec.Value = Config<Settings>.Instance.skills.WarfareLinkSpec;
			numericUpDownFrigates.Value = Config<Settings>.Instance.skills.Frigates;
			numericUpDownCruisers.Value = Config<Settings>.Instance.skills.Cruisers;
			numericUpDownVeldspar.Value = Config<Settings>.Instance.skills.VeldsparProcessing;
			numericUpDownScordite.Value = Config<Settings>.Instance.skills.ScorditeProcessing;
			numericUpDownPyroxeres.Value = Config<Settings>.Instance.skills.PyroxeresProcessing;
			numericUpDownPlagioclase.Value = Config<Settings>.Instance.skills.PlagioclaseProcessing;
			numericUpDownOmber.Value = Config<Settings>.Instance.skills.OmberProcessing;
			numericUpDownKernite.Value = Config<Settings>.Instance.skills.KerniteProcessing;
			numericUpDownJaspet.Value = Config<Settings>.Instance.skills.JaspetProcessing;
			numericUpDownHemorphite.Value = Config<Settings>.Instance.skills.HemorphiteProcessing;
			numericUpDownHedbergite.Value = Config<Settings>.Instance.skills.HedbergiteProcessing;
			numericUpDownGneiss.Value = Config<Settings>.Instance.skills.GneissProcessing;
			numericUpDownDarkOchre.Value = Config<Settings>.Instance.skills.DarkOchreProcessing;
			numericUpDownSpodumain.Value = Config<Settings>.Instance.skills.SpodumainProcessing;
			numericUpDownCrokite.Value = Config<Settings>.Instance.skills.CrokiteProcessing;
			numericUpDownBistot.Value = Config<Settings>.Instance.skills.BistotProcessing;
			numericUpDownArkonor.Value = Config<Settings>.Instance.skills.ArkonorProcessing;
			numericUpDownMercoxit.Value = Config<Settings>.Instance.skills.MercoxitProcessing;
			numericUpDownRefining.Value = Config<Settings>.Instance.skills.Refining;
			numericUpDownEfficiencyRefining.Value = Config<Settings>.Instance.skills.EfficiencyRefining;
			numericUpDownIceProcessing.Value = Config<Settings>.Instance.skills.IceProcessing;
			numericUpDownStanding.Value = (decimal) Config<Settings>.Instance.Standing;

			checkBoxHX2Imp.Checked = Config<Settings>.Instance.ImpHX2;
			checkBoxMichiImp.Checked = Config<Settings>.Instance.ImpMichi;
			checkBoxMindLinkImp.Checked = Config<Settings>.Instance.ImpMindLink;
			checkBoxUseGangBonuses.Checked = Config<Settings>.Instance.isGang;

			pictureBoxGang1.Image = (Config<Settings>.Instance.GangAssistModule1) ? Resources.icon53_16 : Resources.highSlot;
			pictureBoxGang2.Image = (Config<Settings>.Instance.GangAssistModule2) ? Resources.icon53_16 : Resources.highSlot;
			pictureBoxGang3.Image = (Config<Settings>.Instance.GangAssistModule3) ? Resources.icon53_16 : Resources.highSlot;

			pictureBoxVeldspar.Tag = dictOre["Veldspar"];
			pictureBoxScordite.Tag = dictOre["Scordite"];
			pictureBoxPyroxeres.Tag = dictOre["Pyroxeres"];
			pictureBoxPlagioclase.Tag = dictOre["Plagioclase"];
			pictureBoxOmber.Tag = dictOre["Omber"];
			pictureBoxKernite.Tag = dictOre["Kernite"];
			pictureBoxJaspet.Tag = dictOre["Jaspet"];
			pictureBoxHemorphite.Tag = dictOre["Hemorphite"];
			pictureBoxHedbergite.Tag = dictOre["Hedbergite"];
			pictureBoxGneiss.Tag = dictOre["Gneiss"];
			pictureBoxDarkOchre.Tag = dictOre["Dark Ochre"];
			pictureBoxSpodumain.Tag = dictOre["Spodumain"];
			pictureBoxCrokite.Tag = dictOre["Crokite"];
			pictureBoxBistot.Tag = dictOre["Bistot"];
			pictureBoxArkonor.Tag = dictOre["Arkonor"];
			pictureBoxMercoxit.Tag = dictOre["Mercoxit"];

			pictureBoxTritanium.Tag = "Tritanium";
			pictureBoxPyerite.Tag = "Pyerite";
			pictureBoxMexallon.Tag = "Mexallon";
			pictureBoxIsogen.Tag = "Isogen";
			pictureBoxNocxium.Tag = "Nocxium";
			pictureBoxZydrine.Tag = "Zydrine";
			pictureBoxMegacyte.Tag = "Megacyte";
			pictureBoxMorphite.Tag = "Morphite";

			textBoxPriceTritanium.Text = Config<Settings>.Instance.PriceTritanium.ToString("F2");
			textBoxPricePyerite.Text = Config<Settings>.Instance.PricePyerite.ToString("F2");
			textBoxPriceMexallon.Text = Config<Settings>.Instance.PriceMexallon.ToString("F2");
			textBoxPriceIsogen.Text = Config<Settings>.Instance.PriceIsogen.ToString("F2");
			textBoxPriceNocxium.Text = Config<Settings>.Instance.PriceNocxium.ToString("F2");
			textBoxPriceZydrine.Text = Config<Settings>.Instance.PriceZydrine.ToString("F2");
			textBoxPriceMegacyte.Text = Config<Settings>.Instance.PriceMegacyte.ToString("F2");
			textBoxPriceMorphite.Text = Config<Settings>.Instance.PriceMorphite.ToString("F2");

			for (int n = 0; n < tabControl1.TabCount; n++)
			{
				if (n == Config<Settings>.Instance.SelectedTabIndex)
					tabControl1.SelectedIndex = n;
			}
		}

		private void FillShips()
		{
			Ship ship = new Ship("Not Miner", 0, 0, false, false);
			dictShips.Add(ship.Name, ship);
			ship = new Ship("Frigate (+20% mining yield per lvl)", 20, 0, false, false);
			dictShips.Add(ship.Name, ship);
			ship = new Ship("Cruiser (+20% mining yield per lvl)", 20, 0, false, false);
			dictShips.Add(ship.Name, ship);
			ship = new Ship("Mining Barge", 3, 0, true, false);
			dictShips.Add(ship.Name, ship);
			ship = new Ship("Mackinaw", 3, 5, true, true);
			dictShips.Add(ship.Name, ship);
			ship = new Ship("Hulk", 3, 3, true, true);
			dictShips.Add(ship.Name, ship);
			ship = new Ship("Skif", 3, 3, true, true);
			dictShips.Add(ship.Name, ship);

			comboBoxShip.BeginUpdate();
			foreach (KeyValuePair<string, Ship> pair in dictShips)
			{
				comboBoxShip.Items.Add(pair.Value);
			}
			comboBoxShip.EndUpdate();

			comboBoxShip.SelectedIndex = 0;
		}

		private void FillMinigLaserUpgrades()
		{
			//MLU
			LaserUpgrade mlu = new LaserUpgrade("None", 0.0, 0.0, Resources.lowSlot);
			dictMlu.Add(mlu.Name, mlu);
			mlu = new LaserUpgrade("Mining Laser Upgrade I", 5.0, 0.0, Resources.icon05_12);
			dictMlu.Add(mlu.Name, mlu);
			mlu = new LaserUpgrade("Erin Mining Upgrade I", 6.0, 0.0, Resources.icon05_12);
			dictMlu.Add(mlu.Name, mlu);
			mlu = new LaserUpgrade("Elara Mining Upgrade I", 7.0, 0.0, Resources.icon05_12);
			dictMlu.Add(mlu.Name, mlu);
			mlu = new LaserUpgrade("Carpo Mining Upgrade I", 8.0, 0.0, Resources.icon05_12);
			dictMlu.Add(mlu.Name, mlu);
			mlu = new LaserUpgrade("Aoede Mining Upgrade I", 9.0, 0.0, Resources.icon05_12);
			dictMlu.Add(mlu.Name, mlu);
			mlu = new LaserUpgrade("Mining Laser Upgrade II", 9.0, 0.0, Resources.icon05_12_t2);
			dictMlu.Add(mlu.Name, mlu);
			//IHU
			mlu = new LaserUpgrade("Ice Harvester Upgrade I", 0.0, 5.0, Resources.icon05_12);
			dictMlu.Add(mlu.Name, mlu);
			mlu = new LaserUpgrade("Crisium Ice Harvester Upgrade I", 0.0, 6.0, Resources.icon05_12);
			dictMlu.Add(mlu.Name, mlu);
			mlu = new LaserUpgrade("Frigoris Ice Harvester Upgrade I", 0.0, 7.0, Resources.icon05_12);
			dictMlu.Add(mlu.Name, mlu);
			mlu = new LaserUpgrade("Anguis Ice Harvester Upgrade I", 0.0, 8.0, Resources.icon05_12);
			dictMlu.Add(mlu.Name, mlu);
			mlu = new LaserUpgrade("Ingenii Ice Harvester Upgrade I", 0.0, 9.0, Resources.icon05_12);
			dictMlu.Add(mlu.Name, mlu);
			mlu = new LaserUpgrade("Ice Harvester Upgrade II", 0.0, 9.0, Resources.icon05_12_t2);
			dictMlu.Add(mlu.Name, mlu);


			//Формируем менюшку для выбора MLU
			foreach (KeyValuePair<string, LaserUpgrade> pair in dictMlu)
			{
				LaserUpgrade device = pair.Value;
				ToolStripItem item = new ToolStripMenuItem(device.Name, device.image, MenuMLU_clicked);
				contextMenuStripMLU.Items.Add(item);
				contextMenuStripMLU.Items[contextMenuStripMLU.Items.Count - 1].Tag = device;
			}
		}

		private void FillOreList()
		{
			//Veldspar
			MineralsOut mOut = new MineralsOut(1000, 0, 0, 0, 0, 0, 0, 0);
			Ore ore = new Ore("Veldspar", 0.1, 333, mOut);
			dictOre.Add(ore.Name, ore);

			mOut = new MineralsOut(1050, 0, 0, 0, 0, 0, 0, 0);
			ore = new Ore("Concentrated Veldspar", 0.1, 333, mOut);
			dictOre.Add(ore.Name, ore);

			mOut = new MineralsOut(1100, 0, 0, 0, 0, 0, 0, 0);
			ore = new Ore("Dense Veldspar", 0.1, 333, mOut);
			dictOre.Add(ore.Name, ore);

			//Scordite
			mOut = new MineralsOut(833, 416, 0, 0, 0, 0, 0, 0);
			ore = new Ore("Scordite", 0.15, 333, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(875, 437, 0, 0, 0, 0, 0, 0);
			ore = new Ore("Condensed Scordite", 0.15, 333, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(916, 458, 0, 0, 0, 0, 0, 0);
			ore = new Ore("Massive Scordite", 0.15, 333, mOut);
			dictOre.Add(ore.Name, ore);

			//Pyroxeres
			mOut = new MineralsOut(844, 59, 120, 0, 11, 0, 0, 0);
			ore = new Ore("Pyroxeres", 0.3, 333, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(886, 62, 126, 0, 12, 0, 0, 0);
			ore = new Ore("Solid Pyroxeres", 0.3, 333, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(928, 65, 132, 0, 12, 0, 0, 0);
			ore = new Ore("Viscous Pyroxeres", 0.3, 333, mOut);
			dictOre.Add(ore.Name, ore);

			//Plagioclase
			mOut = new MineralsOut(256, 512, 256, 0, 0, 0, 0, 0);
			ore = new Ore("Plagioclase", 0.35, 333, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(269, 538, 269, 0, 0, 0, 0, 0);
			ore = new Ore("Azure Plagioclase", 0.35, 333, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(282, 563, 282, 0, 0, 0, 0, 0);
			ore = new Ore("Rich Plagioclase", 0.35, 333, mOut);
			dictOre.Add(ore.Name, ore);

			//Omber
			mOut = new MineralsOut(307, 123, 0, 307, 0, 0, 0, 0);
			ore = new Ore("Omber", 0.6, 500, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(322, 129, 0, 322, 0, 0, 0, 0);
			ore = new Ore("Silvery Omber", 0.6, 500, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(338, 135, 0, 338, 0, 0, 0, 0);
			ore = new Ore("Golden Omber", 0.6, 500, mOut);
			dictOre.Add(ore.Name, ore);

			//Kernite
			mOut = new MineralsOut(386, 0, 773, 386, 0, 0, 0, 0);
			ore = new Ore("Kernite", 1.2, 400, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(405, 0, 812, 405, 0, 0, 0, 0);
			ore = new Ore("Luminous Kernite", 1.2, 400, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(425, 0, 850, 425, 0, 0, 0, 0);
			ore = new Ore("Fiery Kernite", 1.2, 400, mOut);
			dictOre.Add(ore.Name, ore);

			//Jaspet
			mOut = new MineralsOut(259, 259, 518, 0, 259, 8, 0, 0);
			ore = new Ore("Jaspet", 2.0, 500, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(272, 272, 544, 0, 272, 8, 0, 0);
			ore = new Ore("Pure Jaspet", 2.0, 500, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(285, 285, 570, 0, 285, 9, 0, 0);
			ore = new Ore("Pristine Jaspet", 2.0, 500, mOut);
			dictOre.Add(ore.Name, ore);

			//Hemorphite
			mOut = new MineralsOut(212, 0, 0, 212, 424, 28, 0, 0);
			ore = new Ore("Hemorphite", 3.0, 500, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(223, 0, 0, 223, 445, 29, 0, 0);
			ore = new Ore("Vivid Hemorphite", 3.0, 500, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(233, 0, 0, 233, 466, 31, 0, 0);
			ore = new Ore("Radiant Hemorphite", 3.0, 500, mOut);
			dictOre.Add(ore.Name, ore);

			//Hedbergite
			mOut = new MineralsOut(0, 0, 0, 708, 354, 32, 0, 0);
			ore = new Ore("Hedbergite", 3.0, 500, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(0, 0, 0, 743, 372, 34, 0, 0);
			ore = new Ore("Vitric Hedbergite", 3.0, 500, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(0, 0, 0, 779, 389, 35, 0, 0);
			ore = new Ore("Glazed Hedbergite", 3.0, 500, mOut);
			dictOre.Add(ore.Name, ore);

			//Gneiss
			mOut = new MineralsOut(171, 0, 171, 343, 0, 171, 0, 0);
			ore = new Ore("Gneiss", 5.0, 400, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(180, 0, 180, 360, 0, 180, 0, 0);
			ore = new Ore("Iridescent Gneiss", 5.0, 400, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(188, 0, 188, 371, 0, 188, 0, 0);
			ore = new Ore("Prismatic Gneiss", 5.0, 400, mOut);
			dictOre.Add(ore.Name, ore);

			//Dark Ochre
			mOut = new MineralsOut(250, 0, 0, 0, 500, 250, 0, 0);
			ore = new Ore("Dark Ochre", 8.0, 400, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(263, 0, 0, 0, 525, 263, 0, 0);
			ore = new Ore("Onyx Ochre", 8.0, 400, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(275, 0, 0, 0, 550, 275, 0, 0);
			ore = new Ore("Obsidian Ochre", 8.0, 400, mOut);
			dictOre.Add(ore.Name, ore);

			//Spodumain
			mOut = new MineralsOut(700, 140, 0, 0, 0, 0, 140, 0);
			ore = new Ore("Spodumain", 16.0, 250, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(735, 147, 0, 0, 0, 0, 147, 0);
			ore = new Ore("Bright Spodumain", 16.0, 250, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(770, 154, 0, 0, 0, 0, 154, 0);
			ore = new Ore("Gleaming Spodumain", 16.0, 250, mOut);
			dictOre.Add(ore.Name, ore);

			//Crokite
			mOut = new MineralsOut(331, 0, 0, 0, 331, 663, 0, 0);
			ore = new Ore("Crokite", 16.0, 250, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(348, 0, 0, 0, 348, 696, 0, 0);
			ore = new Ore("Sharp Crokite", 16.0, 250, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(364, 0, 0, 0, 364, 729, 0, 0);
			ore = new Ore("Crystalline Crokite", 16.0, 250, mOut);
			dictOre.Add(ore.Name, ore);

			//Bistot
			mOut = new MineralsOut(0, 170, 0, 0, 0, 341, 170, 0);
			ore = new Ore("Bistot", 16.0, 200, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(0, 179, 0, 0, 0, 358, 179, 0);
			ore = new Ore("Triclinic Bistot", 16.0, 200, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(0, 187, 0, 0, 0, 375, 187, 0);
			ore = new Ore("Monoclinic Bistot", 16.0, 200, mOut);
			dictOre.Add(ore.Name, ore);

			//Arkonor
			mOut = new MineralsOut(300, 0, 0, 0, 0, 166, 333, 0);
			ore = new Ore("Arkonor", 16.0, 200, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(315, 0, 0, 0, 0, 174, 350, 0);
			ore = new Ore("Crimson Arkonor", 16.0, 200, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(330, 0, 0, 0, 0, 183, 366, 0);
			ore = new Ore("Prime Arkonor", 16.0, 200, mOut);
			dictOre.Add(ore.Name, ore);

			//Mercoxit
			mOut = new MineralsOut(0, 0, 0, 0, 0, 0, 0, 509);
			ore = new Ore("Mercoxit", 40.0, 250, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(0, 0, 0, 0, 0, 0, 0, 535);
			ore = new Ore("Magma Mercoxit", 40.0, 250, mOut);
			dictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(0, 0, 0, 0, 0, 0, 0, 560);
			ore = new Ore("Vitreous Mercoxit", 40.0, 250, mOut);
			dictOre.Add(ore.Name, ore);


			///ICE
			mOut = new MineralsOut(0, 0, 0, 0, 0, 0, 0, 0);
			ore = new Ore("Ice", 1000.0, 1, mOut);
			dictOre.Add(ore.Name, ore);


			foreach (KeyValuePair<string, Ore> pair in dictOre)
			{
				comboBoxOre.Items.Add(pair.Value);
			}
			comboBoxOre.SelectedIndex = 0;
		}

		private void FillTurretList()
		{
			//Mining Turrets
			MiningTurret turret = new MiningTurret("Basic Miner", 60, 30.0, false);
			turretsList.Add(turret.Name, turret);
			turret = new MiningTurret("Cu Vapor Particle Bore Stream I", 60, 49.0, false);
			turretsList.Add(turret.Name, turret);
			turret = new MiningTurret("Deep Core Mining Laser I", 60, 40.0, false);
			turretsList.Add(turret.Name, turret);
			turret = new MiningTurret("Dual Diode Mining Laser I", 60, 44.0, false);
			turretsList.Add(turret.Name, turret);
			turret = new MiningTurret("EP-S Gaussian I Excavation Pulse", 60, 42, false);
			turretsList.Add(turret.Name, turret);
			turret = new MiningTurret("Miner I", 60, 40, false);
			turretsList.Add(turret.Name, turret);
			turret = new MiningTurret("Miner II", 60, 60.0, false);
			turretsList.Add(turret.Name, turret);
			turret = new MiningTurret("Modulated Deep Core Miner II", 180, 120.0, true);
			turretsList.Add(turret.Name, turret);
			turret = new MiningTurret("XeCl Drilling Beam I", 60, 47.0, false);
			turretsList.Add(turret.Name, turret);
			//Strips
			turret = new MiningTurret("Strip Miner I", 180, 540.0, false);
			turretsList.Add(turret.Name, turret);
			turret = new MiningTurret("Modulated Strip Miner II", 180, 360.0, true);
			turretsList.Add(turret.Name, turret);
			turret = new MiningTurret("Modulated Deep Core Strip Miner II ", 180, 250.0, true);
			turretsList.Add(turret.Name, turret);
			//Ice Harvesters
			turret = new MiningTurret("Ice Harvester I", 600, 1.0, false);
			turretsList.Add(turret.Name, turret);
			turret = new MiningTurret("Ice Harvester II", 500, 1.0, false);
			turretsList.Add(turret.Name, turret);


			comboBoxTurret.BeginUpdate();
			foreach (KeyValuePair<string, MiningTurret> pair in turretsList)
			{
				comboBoxTurret.Items.Add(pair.Value);
			}
			comboBoxTurret.EndUpdate();

			comboBoxTurret.SelectedIndex = 0;
		}

		#region Menu

		private void MenuMLU_clicked(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			if (item != null)
			{
				LaserUpgrade mlu = (LaserUpgrade) item.Tag;
				mluPictureClicked.Image = mlu.image;
				mluPictureClicked.Tag = item.Tag;
				if (mluPictureClicked == pictureBoxMLU1)
				{
					Config<Settings>.Instance.Mlu1 = mlu.Name;
				}
				else if (mluPictureClicked == pictureBoxMLU2)
				{
					Config<Settings>.Instance.Mlu2 = mlu.Name;
				}
				else if (mluPictureClicked == pictureBoxMLU3)
				{
					Config<Settings>.Instance.Mlu3 = mlu.Name;
				}
				CalculateMining();
			}
		}

		/// <summary>
		/// Удалить все элементы из списка
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void removeAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (tabControl1.SelectedTab == tabPageTimers)
				dataGridViewTimers.Rows.Clear();
			else if (tabControl1.SelectedTab == tabPageCalculator)
				dataGridViewCalc.Rows.Clear();
		}

		private void alwaysOnTopToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			alwaysOnTopToolStripMenuItem.Checked = TopMost;
		}

		private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TopMost = !TopMost;
			Config<Settings>.Instance.AlwaysOnTop = TopMost;
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutForm dlg = new AboutForm();
			dlg.ShowDialog();
		}

		#endregion

		private void comboBox_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if (sender == comboBoxTurret)
			{
				CheckUseCrystals();
				if (turretsList.ContainsKey(comboBoxTurret.SelectedItem.ToString()))
				{
					Config<Settings>.Instance.SelectedTurret = comboBoxTurret.SelectedItem.ToString();
				}
				CalculateMining();
			}
			else if (sender == comboBoxOre)
			{
				if (dictOre.ContainsKey(comboBoxOre.SelectedItem.ToString()))
					Config<Settings>.Instance.SelectedOre = comboBoxOre.SelectedItem.ToString();
			}
			else if (sender == comboBoxShip)
			{
				if (dictShips.ContainsKey(comboBoxShip.SelectedItem.ToString()))
					Config<Settings>.Instance.SelectedShip = comboBoxShip.SelectedItem.ToString();

				CalculateMining();
			}
		}

		private void CheckUseCrystals()
		{
			MiningTurret turret = turretsList[comboBoxTurret.SelectedItem.ToString()];
			radioButtonT1Crystals.Visible = turret.UseCrystals;
			radioButtonT2Crystals.Visible = turret.UseCrystals;
		}

		/// <summary>
		/// Посчитать объем руды с лазера
		/// </summary>
		private void CalculateMining()
		{
			MiningTurret turret = turretsList[comboBoxTurret.SelectedItem.ToString()];

			double yield = 1000.0;
			double cycleTime = GetCycleTime(turret);
			if (turret.CycleTime < 200)
				yield = GetYield(turret);
			else if (Config<Settings>.Instance.SelectedShip == "Mackinaw")
				yield = 2000.0;

			textBoxMiningYield.Text = yield.ToString();
			textBoxCycle.Text = cycleTime.ToString();

			Text = string.Format("Eve Miner - {0}m3 / {1}sec - {2}", yield.ToString("F2"), cycleTime.ToString("F0"), turret);
		}

		/// <summary>
		/// Посчитать цикл
		/// </summary>
		/// <param name="turret"></param>
		/// <returns></returns>
		private double GetCycleTime(MiningTurret turret)
		{
			double cycle = turret.CycleTime;

			Skills skills = Config<Settings>.Instance.skills;
			//Если турелька лед роет
			if (turret.CycleTime > 200)
			{
				cycle *= (1 - skills.IceHarvesting * 0.05);

				LaserUpgrade mlu = pictureBoxMLU1.Tag as LaserUpgrade;
				if (mlu != null)
					cycle *= (1 - mlu.TimeBonus / 100);

				mlu = pictureBoxMLU2.Tag as LaserUpgrade;
				if (mlu != null)
					cycle *= (1 - mlu.TimeBonus / 100);

				mlu = pictureBoxMLU3.Tag as LaserUpgrade;
				if (mlu != null)
					cycle *= (1 - mlu.TimeBonus / 100);

				//Если макинаву юзаем
				if (Config<Settings>.Instance.SelectedShip == "Mackinaw")
				{
					cycle *= 1.25;
					cycle *= (1 - skills.Exhumers * 0.05);
				}
					//Если халк
				else if (Config<Settings>.Instance.SelectedShip == "Hulk")
				{
					cycle *= (1 - skills.Exhumers * 0.03);
				}
			}
			int gangAssistModule = 0;
			if (Config<Settings>.Instance.GangAssistModule1)
				gangAssistModule++;
			if (Config<Settings>.Instance.GangAssistModule2)
				gangAssistModule++;
			if (Config<Settings>.Instance.GangAssistModule3)
				gangAssistModule++;

			//Ганг бонусы
			if (Config<Settings>.Instance.isGang)
			{
				if (Config<Settings>.Instance.ImpMindLink)
					cycle *= (1 - 2 * skills.MiningDirector * (1 + skills.WarfareLinkSpec * 0.1) * 1.5 / 100 * gangAssistModule);
				else
					cycle *= (1 - 2 * skills.MiningDirector * (1 + skills.WarfareLinkSpec * 0.1) / 100 * gangAssistModule);
			}

			return cycle;
		}

		/// <summary>
		///Подсчет объема руды за цикл
		/// </summary>
		/// <param name="turret"></param>
		/// <returns></returns>
		private double GetYield(MiningTurret turret)
		{
			double yield = turret.MiningAmount;
			Skills skills = Config<Settings>.Instance.skills;

			Ship ship = dictShips[comboBoxShip.SelectedItem.ToString()];

			yield *= (1 + skills.Mining * 0.05) * (1 + skills.Astrogeology * 0.05);

			if (ship.Barge)
				yield *= (1 + skills.MiningBarge * 0.03);
			if (ship.Exhumer)
				yield *= (1 + skills.Exhumers * 0.03);

			if (ship.Name.Contains("Frigate"))
				yield *= (1 + skills.Frigates * 0.2);

			else if (ship.Name.Contains("Cruiser"))
				yield *= (1 + skills.Cruisers * 0.2);

			LaserUpgrade mlu = pictureBoxMLU1.Tag as LaserUpgrade;
			if (mlu != null)
				yield *= (1 + mlu.OreYieldBonus / 100);

			mlu = pictureBoxMLU2.Tag as LaserUpgrade;
			if (mlu != null)
				yield *= (1 + mlu.OreYieldBonus / 100);

			mlu = pictureBoxMLU3.Tag as LaserUpgrade;
			if (mlu != null)
				yield *= (1 + mlu.OreYieldBonus / 100);

			if (turret.UseCrystals)
			{
				if (Config<Settings>.Instance.SelectedCrystals == 2)
				{
					if (ship.Name == "Skif" && turret.Name.Contains("Deep"))
						yield *= (1.375 * (1 + skills.Exhumers * 0.6));
					else
						yield *= 1.75;
				}
				else
				{
					if (ship.Name == "Skif" && turret.Name.Contains("Deep"))
						yield *= (1.25 * (1 + skills.Exhumers * 0.6));
					else
						yield *= 1.625;
				}
			}
			if (Config<Settings>.Instance.ImpHX2)
				yield *= 1.05;
			if (Config<Settings>.Instance.ImpMichi)
				yield *= 1.05;

			//Ганг бонусы
			if (Config<Settings>.Instance.isGang)
			{
				if (Config<Settings>.Instance.ImpMindLink)
				{
					yield *= (1 + skills.MiningForeman * 0.02 * 1.5);
					//yield *= 1/(1 - 2*skills.MiningDirector*(1 + skills.WarfareLinkSpec*0.1)*1.5/100*gangAssistModule);
				}
				else
					yield *= (1 + skills.MiningForeman * 0.02);
			}
			return yield;
		}

		/// <summary>
		/// при клике по MLU показать менюшку
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox_Click(object sender, EventArgs e)
		{
			PictureBox pict = sender as PictureBox;
			if (pict != null)
			{
				if (sender == pictureBoxMLU1 ||
				    sender == pictureBoxMLU2 ||
				    sender == pictureBoxMLU3)
				{
					contextMenuStripMLU.Show(pict, 0, pict.Height);
					mluPictureClicked = pict;
				}
				else if (sender == pictureBoxGang1)
				{
					Config<Settings>.Instance.GangAssistModule1 = !Config<Settings>.Instance.GangAssistModule1;
					if (Config<Settings>.Instance.GangAssistModule1)
						pictureBoxGang1.Image = Resources.icon53_16;
					else
						pictureBoxGang1.Image = Resources.highSlot;
					CalculateMining();
				}
				else if (sender == pictureBoxGang2)
				{
					Config<Settings>.Instance.GangAssistModule2 = !Config<Settings>.Instance.GangAssistModule2;
					if (Config<Settings>.Instance.GangAssistModule2)
						pictureBoxGang2.Image = Resources.icon53_16;
					else
						pictureBoxGang2.Image = Resources.highSlot;
					CalculateMining();
				}
				else if (sender == pictureBoxGang3)
				{
					Config<Settings>.Instance.GangAssistModule3 = !Config<Settings>.Instance.GangAssistModule3;
					if (Config<Settings>.Instance.GangAssistModule3)
						pictureBoxGang3.Image = Resources.icon53_16;
					else
						pictureBoxGang3.Image = Resources.highSlot;
					CalculateMining();
				}
			}
		}

		#region Tooltips

		private void On_MouseEnter(object sender, EventArgs e)
		{
			Control ctrl = sender as Control;
			if (!toolTip1.Active && ctrl != null)
			{
				string tooltip = "";
				toolTip1.ToolTipTitle = "";
				if (ctrl is PictureBox && ctrl.Tag is LaserUpgrade)
				{
					toolTip1.ToolTipTitle = "Laser Upgrade";
					tooltip = ctrl.Tag.ToString();
				}
				else if (ctrl is PictureBox && ctrl.Tag is Ore)
				{
					toolTip1.ToolTipTitle = "Ore";
					if (ctrl.Tag != null)
						tooltip = ((Ore) ctrl.Tag).Name;
				}
				else if (ctrl is PictureBox && ctrl.Tag is string)
				{
					tooltip = (string) ctrl.Tag;
				}

				else if (ctrl == pictureBoxGang1)
				{
					toolTip1.ToolTipTitle = "Gang Assist Module";
					if (Config<Settings>.Instance.GangAssistModule1)
						tooltip = "Mining Foreman Link - Laser Optimization";
					else
						tooltip = "None";
				}
				else if (ctrl == pictureBoxGang2)
				{
					toolTip1.ToolTipTitle = "Gang Assist Module";
					if (Config<Settings>.Instance.GangAssistModule2)
						tooltip = "Mining Foreman Link - Laser Optimization";
					else
						tooltip = "None";
				}
				else if (ctrl == pictureBoxGang3)
				{
					toolTip1.ToolTipTitle = "Gang Assist Module";
					if (Config<Settings>.Instance.GangAssistModule3)
						tooltip = "Mining Foreman Link - Laser Optimization";
					else
						tooltip = "None";
				}

				else if (ctrl == textBoxMiningYield)
				{
					toolTip1.ToolTipTitle = "Mining Amount";
					tooltip = string.Format("{0} m3", textBoxMiningYield.Text);
				}
				else if (ctrl == textBoxCycle)
				{
					toolTip1.ToolTipTitle = "Laser/Harvester duration";
					tooltip = string.Format("{0} seconds", textBoxCycle.Text);
				}

				if (tooltip.Length > 0)
					toolTip1.SetToolTip(ctrl, tooltip);
				toolTip1.Active = true;
			}
		}

		private void On_MouseLeave(object sender, EventArgs e)
		{
			toolTip1.Active = false;
		}

		#endregion

		/// <summary>
		/// Добавить таймер к списку таймеров
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAdd_Click(object sender, EventArgs e)
		{
			Ore ore = dictOre[comboBoxOre.SelectedItem.ToString()];
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

				DataGridViewRow row = new DataGridViewRow();
				row.Tag = timerListItem;
				DataGridViewCell[] cells = new DataGridViewCell[dataGridViewTimers.ColumnCount];
				cells[ColumnOre.Index] = new DataGridViewTextBoxCell();
				cells[ColumnOre.Index].Value = timerListItem.ore.Name;
				cells[ColumnStartQty.Index] = new DataGridViewTextBoxCell();
				cells[ColumnStartQty.Index].Value = startVolume;
				cells[ColumnCurrentQty.Index] = new DataGridViewTextBoxCell();
				cells[ColumnCurrentQty.Index].Value = startVolume;
				cells[ColumnCycle.Index] = new DataGridViewTextBoxCell();
				cells[ColumnCycle.Index].Value = cycle.ToString("F0");
				cells[ColumnTimeToEnd.Index] = new DataGridViewTextBoxCell();
				cells[ColumnTimeToEnd.Index].Value =
					string.Format("{0:F0}:{1:F0}", timerListItem.TimeToAsterEnd / 60, timerListItem.TimeToAsterEnd % 60);
				cells[ColumnButtonStart.Index] = new DataGridViewButtonCell();
				cells[ColumnButtonStart.Index].Value = ">";
				cells[ColumnButtonDelete.Index] = new DataGridViewButtonCell();
				cells[ColumnButtonDelete.Index].Value = "x";

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
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
					row.Cells[ColumnButtonStart.Index].Value = "x";
				}
				else if (timerListItem.TimerStarted)
					row.DefaultCellStyle.BackColor = Color.FromArgb(200, 255, 200);


//				DateTime time = new DateTime();
//				time.AddSeconds(timerListItem.TimeToAsterEnd);

				row.Cells[ColumnTimeToEnd.Index].Value =
					string.Format("{0:00}:{1:00}", timerListItem.TimeToAsterEnd / 60, timerListItem.TimeToAsterEnd % 60);
				row.Cells[ColumnCycle.Index].Value = timerListItem.TimeToCycleEnd.ToString("F0");
				row.Cells[ColumnCurrentQty.Index].Value = string.Format("{0}", timerListItem.CurrentVolume.ToString("F0"));

				if (timerListItem.TimeToAsterEnd == 0)
					row.Tag = null;
			}
		}

		/// <summary>
		/// Изменили значения скилов
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownSkills_ValueChanged(object sender, EventArgs e)
		{
			if (sender == numericUpDownMining)
			{
				Config<Settings>.Instance.skills.Mining = (int) numericUpDownMining.Value;
				SetSkillPicture(pictureBoxSMining, Config<Settings>.Instance.skills.Mining);
			}
			else if (sender == numericUpDownAstrogeology)
			{
				Config<Settings>.Instance.skills.Astrogeology = (int) numericUpDownAstrogeology.Value;
				SetSkillPicture(pictureBoxSAstrogeology, Config<Settings>.Instance.skills.Astrogeology);
			}

			else if (sender == numericUpDownMiningBarge)
			{
				Config<Settings>.Instance.skills.MiningBarge = (int) numericUpDownMiningBarge.Value;
				SetSkillPicture(pictureBoxSMiningBarge, Config<Settings>.Instance.skills.MiningBarge);
			}

			else if (sender == numericUpDownExhumers)
			{
				Config<Settings>.Instance.skills.Exhumers = (int) numericUpDownExhumers.Value;
				SetSkillPicture(pictureBoxSExhumers, Config<Settings>.Instance.skills.Exhumers);
			}
			else if (sender == numericUpDownMiningForeman)
			{
				Config<Settings>.Instance.skills.MiningForeman = (int) numericUpDownMiningForeman.Value;
				SetSkillPicture(pictureBoxSMiningForeman, Config<Settings>.Instance.skills.MiningForeman);
			}
			else if (sender == numericUpDownMiningDirector)
			{
				Config<Settings>.Instance.skills.MiningDirector = (int) numericUpDownMiningDirector.Value;
				SetSkillPicture(pictureBoxSMiningDirector, Config<Settings>.Instance.skills.MiningDirector);
			}
			else if (sender == numericUpDownIceHarvesting)
			{
				Config<Settings>.Instance.skills.IceHarvesting = (int) numericUpDownIceHarvesting.Value;
				SetSkillPicture(pictureBoxSIceHarvesting, Config<Settings>.Instance.skills.IceHarvesting);
			}
			else if (sender == numericUpDownWarfareLinkSpec)
			{
				Config<Settings>.Instance.skills.WarfareLinkSpec = (int) numericUpDownWarfareLinkSpec.Value;
				SetSkillPicture(pictureBoxSWarfareLinkSpec, Config<Settings>.Instance.skills.WarfareLinkSpec);
			}
			else if (sender == numericUpDownFrigates)
			{
				Config<Settings>.Instance.skills.Frigates = (int) numericUpDownFrigates.Value;
				SetSkillPicture(pictureBoxSFrigates, Config<Settings>.Instance.skills.Frigates);
			}
			else if (sender == numericUpDownCruisers)
			{
				Config<Settings>.Instance.skills.Cruisers = (int) numericUpDownCruisers.Value;
				SetSkillPicture(pictureBoxSCruisers, Config<Settings>.Instance.skills.Cruisers);
			}

				//Ore Processing
				#region Ore Processing

			else if (sender == numericUpDownVeldspar)
			{
				Config<Settings>.Instance.skills.VeldsparProcessing = (int) numericUpDownVeldspar.Value;
				SetSkillPicture(pictureBoxSVeldsparP, Config<Settings>.Instance.skills.VeldsparProcessing);
			}
			else if (sender == numericUpDownScordite)
			{
				Config<Settings>.Instance.skills.ScorditeProcessing = (int) numericUpDownScordite.Value;
				SetSkillPicture(pictureBoxSScorditeP, Config<Settings>.Instance.skills.ScorditeProcessing);
			}
			else if (sender == numericUpDownPyroxeres)
			{
				Config<Settings>.Instance.skills.PyroxeresProcessing = (int) numericUpDownPyroxeres.Value;
				SetSkillPicture(pictureBoxSPyroxeresP, Config<Settings>.Instance.skills.PyroxeresProcessing);
			}
			else if (sender == numericUpDownPlagioclase)
			{
				Config<Settings>.Instance.skills.PlagioclaseProcessing = (int) numericUpDownPlagioclase.Value;
				SetSkillPicture(pictureBoxSPlagioclaseP, Config<Settings>.Instance.skills.PlagioclaseProcessing);
			}
			else if (sender == numericUpDownOmber)
			{
				Config<Settings>.Instance.skills.OmberProcessing = (int) numericUpDownOmber.Value;
				SetSkillPicture(pictureBoxSOmberP, Config<Settings>.Instance.skills.OmberProcessing);
			}
			else if (sender == numericUpDownKernite)
			{
				Config<Settings>.Instance.skills.KerniteProcessing = (int) numericUpDownKernite.Value;
				SetSkillPicture(pictureBoxSKerniteP, Config<Settings>.Instance.skills.KerniteProcessing);
			}
			else if (sender == numericUpDownJaspet)
			{
				Config<Settings>.Instance.skills.JaspetProcessing = (int) numericUpDownJaspet.Value;
				SetSkillPicture(pictureBoxSJaspetP, Config<Settings>.Instance.skills.JaspetProcessing);
			}
			else if (sender == numericUpDownHemorphite)
			{
				Config<Settings>.Instance.skills.HemorphiteProcessing = (int) numericUpDownHemorphite.Value;
				SetSkillPicture(pictureBoxSHemorphiteP, Config<Settings>.Instance.skills.HemorphiteProcessing);
			}
			else if (sender == numericUpDownHedbergite)
			{
				Config<Settings>.Instance.skills.HedbergiteProcessing = (int) numericUpDownHedbergite.Value;
				SetSkillPicture(pictureBoxSHedbergiteP, Config<Settings>.Instance.skills.HedbergiteProcessing);
			}
			else if (sender == numericUpDownGneiss)
			{
				Config<Settings>.Instance.skills.GneissProcessing = (int) numericUpDownGneiss.Value;
				SetSkillPicture(pictureBoxSGneissP, Config<Settings>.Instance.skills.GneissProcessing);
			}
			else if (sender == numericUpDownDarkOchre)
			{
				Config<Settings>.Instance.skills.DarkOchreProcessing = (int) numericUpDownDarkOchre.Value;
				SetSkillPicture(pictureBoxSDarkOchreP, Config<Settings>.Instance.skills.DarkOchreProcessing);
			}
			else if (sender == numericUpDownSpodumain)
			{
				Config<Settings>.Instance.skills.SpodumainProcessing = (int) numericUpDownSpodumain.Value;
				SetSkillPicture(pictureBoxSSpodumainP, Config<Settings>.Instance.skills.SpodumainProcessing);
			}
			else if (sender == numericUpDownCrokite)
			{
				Config<Settings>.Instance.skills.CrokiteProcessing = (int) numericUpDownCrokite.Value;
				SetSkillPicture(pictureBoxSCrokiteP, Config<Settings>.Instance.skills.CrokiteProcessing);
			}
			else if (sender == numericUpDownBistot)
			{
				Config<Settings>.Instance.skills.BistotProcessing = (int) numericUpDownBistot.Value;
				SetSkillPicture(pictureBoxSBistotP, Config<Settings>.Instance.skills.BistotProcessing);
			}
			else if (sender == numericUpDownArkonor)
			{
				Config<Settings>.Instance.skills.ArkonorProcessing = (int) numericUpDownArkonor.Value;
				SetSkillPicture(pictureBoxSArkonorP, Config<Settings>.Instance.skills.ArkonorProcessing);
			}
			else if (sender == numericUpDownMercoxit)
			{
				Config<Settings>.Instance.skills.MercoxitProcessing = (int) numericUpDownMercoxit.Value;
				SetSkillPicture(pictureBoxSMercoxitP, Config<Settings>.Instance.skills.MercoxitProcessing);
			}
				#endregion

			else if (sender == numericUpDownRefining)
			{
				Config<Settings>.Instance.skills.Refining = (int) numericUpDownRefining.Value;
				SetSkillPicture(pictureBoxSRefining, Config<Settings>.Instance.skills.Refining);
			}
			else if (sender == numericUpDownEfficiencyRefining)
			{
				Config<Settings>.Instance.skills.EfficiencyRefining = (int) numericUpDownEfficiencyRefining.Value;
				SetSkillPicture(pictureBoxSEfficiencyRefining, Config<Settings>.Instance.skills.EfficiencyRefining);
			}
			else if (sender == numericUpDownIceProcessing)
			{
				Config<Settings>.Instance.skills.IceProcessing = (int) numericUpDownIceProcessing.Value;
				SetSkillPicture(pictureBoxSIceProcessing, Config<Settings>.Instance.skills.IceProcessing);
			}
			else if (sender == numericUpDownStanding)
			{
				Config<Settings>.Instance.Standing = (double) numericUpDownStanding.Value;
			}


			CalculateMining();
		}

		/// <summary>
		/// Ставит нужную картинку для скила
		/// </summary>
		/// <param name="pict"></param>
		/// <param name="skillValue"></param>
		private static void SetSkillPicture(PictureBox pict, int skillValue)
		{
			switch (skillValue)
			{
				case 5:
					pict.Image = Resources.level5;
					break;
				case 4:
					pict.Image = Resources.level4;
					break;
				case 3:
					pict.Image = Resources.level3;
					break;
				case 2:
					pict.Image = Resources.level2;
					break;
				case 1:
					pict.Image = Resources.level1;
					break;
				default:
					pict.Image = Resources.level0;
					break;
			}
		}

		/// <summary>
		/// Кликнули по смене кристалов
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonCrystals_CheckedChanged(object sender, EventArgs e)
		{
			if (sender == radioButtonT1Crystals)
				Config<Settings>.Instance.SelectedCrystals = 1;
			else if (sender == radioButtonT2Crystals)
				Config<Settings>.Instance.SelectedCrystals = 2;
			CalculateMining();
		}

		/// <summary>
		/// Обработка флажков
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkBox_CheckedChanged(object sender, EventArgs e)
		{
			if (sender == checkBoxHX2Imp)
				Config<Settings>.Instance.ImpHX2 = checkBoxHX2Imp.Checked;
			else if (sender == checkBoxMichiImp)
				Config<Settings>.Instance.ImpMichi = checkBoxMichiImp.Checked;
			else if (sender == checkBoxMindLinkImp)
				Config<Settings>.Instance.ImpMindLink = checkBoxMindLinkImp.Checked;
			else if (sender == checkBoxUseGangBonuses)
			{
				Config<Settings>.Instance.isGang = checkBoxUseGangBonuses.Checked;
				checkBoxUseGangBonus2.Checked = checkBoxUseGangBonuses.Checked;
			}
			else if (sender == checkBoxUseGangBonus2)
			{
				Config<Settings>.Instance.isGang = checkBoxUseGangBonus2.Checked;
				checkBoxUseGangBonuses.Checked = checkBoxUseGangBonus2.Checked;
			}

			CalculateMining();
		}

		/// <summary>
		/// Обработка нажатий на кнопочки датагрида
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGridViewTimers_CellClick(object sender, DataGridViewCellEventArgs e)
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
			else if (sender == dataGridViewCalc)
			{
				if (e.ColumnIndex == ColumnDelete2.Index)
				{
					dataGridViewCalc.Rows.Remove(dataGridViewCalc.Rows[e.RowIndex]);
				}
			}
		}

		/// <summary>
		/// Выбрали таб 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tabControl1_Selected(object sender, TabControlEventArgs e)
		{
			Config<Settings>.Instance.SelectedTabIndex = e.TabPageIndex;
			if (e.TabPage == tabPageTimers)
			{
				textBoxStartValue.Focus();
				textBoxStartValue.Select(0, textBoxStartValue.Text.Length);
			}
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

		/// <summary>
		/// Подсчитать прибыль
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCalculate_Click(object sender, EventArgs e)
		{
			double NetYield = 0.5;
			int quantity = 0;
			double cargohold = 0.0;
			try
			{
				NetYield = Convert.ToDouble(textBoxNetYield.Text) / 100;
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
			Skills skills = Config<Settings>.Instance.skills;

			dataGridViewCalc.Rows.Clear();
			foreach (KeyValuePair<string, Ore> pair in dictOre)
			{
				Ore ore = pair.Value;
				double eff = NetYield +
				             0.375 * (1 + skills.Refining * 0.02) * (1 + skills.EfficiencyRefining * 0.04) *
				             (1 + GetProcessing(ore) * 0.05);
				if (eff > 1.0)
					eff = 1.0;

				double nal = (5 - Config<Settings>.Instance.Standing * 5 / 6.66666) / 100;
				if (nal < 0)
					nal = 0;

				if (sender == buttonCalculateCargoHold)
					quantity = (int) (cargohold / ore.Volume);

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
			int mineralsOut = (int) (ore.MineralsOut.Tritanium * p * eff * (1 - nal));
			double profit = mineralsOut * Config<Settings>.Instance.PriceTritanium;
			mineralsOut = (int) (ore.MineralsOut.Pyerite * p * eff * (1 - nal));
			profit += mineralsOut * Config<Settings>.Instance.PricePyerite;
			mineralsOut = (int) (ore.MineralsOut.Mexallon * p * eff * (1 - nal));
			profit += mineralsOut * Config<Settings>.Instance.PriceMexallon;
			mineralsOut = (int) (ore.MineralsOut.Isogen * p * eff * (1 - nal));
			profit += mineralsOut * Config<Settings>.Instance.PriceIsogen;
			mineralsOut = (int) (ore.MineralsOut.Nocxium * p * eff * (1 - nal));
			profit += mineralsOut * Config<Settings>.Instance.PriceNocxium;
			mineralsOut = (int) (ore.MineralsOut.Zydrine * p * eff * (1 - nal));
			profit += mineralsOut * Config<Settings>.Instance.PriceZydrine;
			mineralsOut = (int) (ore.MineralsOut.Megacyte * p * eff * (1 - nal));
			profit += mineralsOut * Config<Settings>.Instance.PriceMegacyte;
			mineralsOut = (int) (ore.MineralsOut.Morphite * p * eff * (1 - nal));
			profit += mineralsOut * Config<Settings>.Instance.PriceMorphite;


			///Добавляем строчку
			DataGridViewRow row = new DataGridViewRow();

			DataGridViewCell[] cells = new DataGridViewCell[dataGridViewCalc.ColumnCount];
			cells[ColumnOreCalc.Index] = new DataGridViewTextBoxCell();
			cells[ColumnOreCalc.Index].Value = ore.Name;
			cells[ColumnVolume.Index] = new DataGridViewTextBoxCell();
			cells[ColumnVolume.Index].Value = (unitProcess * ore.Volume).ToString("F2") + " m3";
			cells[ColumnRefVolume.Index] = new DataGridViewTextBoxCell();
			cells[ColumnRefVolume.Index].Value = ((ore.MineralsOut.Tritanium +
			                                       ore.MineralsOut.Pyerite +
			                                       ore.MineralsOut.Mexallon +
			                                       ore.MineralsOut.Isogen +
			                                       ore.MineralsOut.Nocxium +
			                                       ore.MineralsOut.Zydrine +
			                                       ore.MineralsOut.Megacyte +
			                                       ore.MineralsOut.Morphite) * p * eff * 0.01).ToString("F2") + " m3";

			cells[ColumnProfit.Index] = new DataGridViewTextBoxCell();
			cells[ColumnProfit.Index].Value = profit.ToString("#,#.##") + " ISK";
			cells[ColumnDelete2.Index] = new DataGridViewButtonCell();
			cells[ColumnDelete2.Index].Value = "x";

			row.Cells.AddRange(cells);
			dataGridViewCalc.Rows.Add(row);
		}

		/// <summary>
		/// получить уровень процессинга для руды
		/// </summary>
		/// <param name="ore"></param>
		/// <returns></returns>
		private static int GetProcessing(Ore ore)
		{
			if (ore.Name.Contains("Veldspar"))
				return Config<Settings>.Instance.skills.VeldsparProcessing;
			if (ore.Name.Contains("Scordite"))
				return Config<Settings>.Instance.skills.ScorditeProcessing;
			if (ore.Name.Contains("Pyroxeres"))
				return Config<Settings>.Instance.skills.PyroxeresProcessing;
			if (ore.Name.Contains("Plagioclase"))
				return Config<Settings>.Instance.skills.PlagioclaseProcessing;
			if (ore.Name.Contains("Omber"))
				return Config<Settings>.Instance.skills.OmberProcessing;
			if (ore.Name.Contains("Kernite"))
				return Config<Settings>.Instance.skills.KerniteProcessing;
			if (ore.Name.Contains("Jaspet"))
				return Config<Settings>.Instance.skills.JaspetProcessing;
			if (ore.Name.Contains("Hemorphite"))
				return Config<Settings>.Instance.skills.HemorphiteProcessing;
			if (ore.Name.Contains("Hedbergite"))
				return Config<Settings>.Instance.skills.HedbergiteProcessing;
			if (ore.Name.Contains("Gneiss"))
				return Config<Settings>.Instance.skills.GneissProcessing;
			if (ore.Name.Contains("Ochre"))
				return Config<Settings>.Instance.skills.DarkOchreProcessing;
			if (ore.Name.Contains("Bistot"))
				return Config<Settings>.Instance.skills.BistotProcessing;
			if (ore.Name.Contains("Spodumain"))
				return Config<Settings>.Instance.skills.SpodumainProcessing;
			if (ore.Name.Contains("Crokite"))
				return Config<Settings>.Instance.skills.CrokiteProcessing;
			if (ore.Name.Contains("Arkonor"))
				return Config<Settings>.Instance.skills.ArkonorProcessing;
			if (ore.Name.Contains("Mercoxit"))
				return Config<Settings>.Instance.skills.MercoxitProcessing;

			return 0;
		}

		#region NotifyIcon
		private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
		{
			
		}

		private void timersToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if(timersForm.Visible)
			{
				timersForm.Activate();
				if (timersForm.WindowState == FormWindowState.Minimized)
					timersForm.WindowState = FormWindowState.Normal;
			}
			else
			{
				timersForm.Show();
			}

		}
		#endregion
	}
}