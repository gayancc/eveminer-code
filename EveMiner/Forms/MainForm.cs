using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EveMiner.Ores;
using EveMiner.Properties;

namespace EveMiner.Forms
{
	/// <summary>
	/// 
	/// </summary>
	public partial class MainForm : Form
	{
		private readonly Dictionary<string, MiningTurret> _turretsList = new Dictionary<string, MiningTurret>();
		private readonly Dictionary<string, LaserUpgrade> _dictMlu = new Dictionary<string, LaserUpgrade>();
		private readonly Dictionary<string, Ship> _dictShips = new Dictionary<string, Ship>();

		private readonly TimersForm _timersForm = new TimersForm();
		private readonly CalculatorForm _calculatorForm = new CalculatorForm();
		
		//Выбранный MLU
		private PictureBox _mluPictureClicked;
		private bool _bExit;

		/// <summary>
		/// Initializes a new instance of the <see cref="MainForm"/> class.
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
			FillMinigLaserUpgrades();
			FillTurretList();
			FillShips();
			LoadCfg();
		}
		/// <summary>
		/// Загрузка конфига
		/// </summary>
		private void LoadCfg()
		{
			LaserUpgrade mlu;

			if (_dictMlu.ContainsKey(Config<Settings>.Instance.Mlu1))
			{
				mlu = _dictMlu[Config<Settings>.Instance.Mlu1];
				pictureBoxMLU1.Image = mlu.Image;
				pictureBoxMLU1.Tag = mlu;
			}

			if (_dictMlu.ContainsKey(Config<Settings>.Instance.Mlu2))
			{
				mlu = _dictMlu[Config<Settings>.Instance.Mlu2];
				pictureBoxMLU2.Image = mlu.Image;
				pictureBoxMLU2.Tag = mlu;
			}

			if (_dictMlu.ContainsKey(Config<Settings>.Instance.Mlu3))
			{
				mlu = _dictMlu[Config<Settings>.Instance.Mlu3];
				pictureBoxMLU3.Image = mlu.Image;
				pictureBoxMLU3.Tag = mlu;
			}

			if (_turretsList.ContainsKey(Config<Settings>.Instance.SelectedTurret))
			{
				MiningTurret turret = _turretsList[Config<Settings>.Instance.SelectedTurret];
				for (int n = 0; n < comboBoxTurret.Items.Count; n++)
				{
					if (comboBoxTurret.Items[n] == turret)
						comboBoxTurret.SelectedIndex = n;
				}
			}
			
			if (_dictShips.ContainsKey(Config<Settings>.Instance.SelectedShip))
			{
				Ship ship = _dictShips[Config<Settings>.Instance.SelectedShip];
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


			TopMost = Config<Settings>.Instance.AlwaysOnTop;
			_timersForm.TopMost = Config<Settings>.Instance.AlwaysOnTop;
			_calculatorForm.TopMost = Config<Settings>.Instance.AlwaysOnTop;

			//Solo
			skillValueMining.Value = Config<Settings>.Instance.Skills.Mining;
			skillValueAstrogeology.Value = Config<Settings>.Instance.Skills.Astrogeology;
			skillValueMiningBarge.Value = Config<Settings>.Instance.Skills.MiningBarge;
			skillValueExhumers.Value = Config<Settings>.Instance.Skills.Exhumers;
			skillValueIceHarvesting.Value = Config<Settings>.Instance.Skills.IceHarvesting;
			skillValueFrigates.Value = Config<Settings>.Instance.Skills.Frigates;
			skillValueCruisers.Value = Config<Settings>.Instance.Skills.Cruisers;
			skillValueRefining.Value = Config<Settings>.Instance.Skills.Refining;
			skillValueEfficiency.Value = Config<Settings>.Instance.Skills.EfficiencyRefining;
			skillValueIceProcessing.Value = Config<Settings>.Instance.Skills.IceProcessing;

			//Gang
			skillValueMiningForeman.Value = Config<Settings>.Instance.Skills.MiningForeman;
			skillValueMiningDirector.Value = Config<Settings>.Instance.Skills.MiningDirector;
			skillValueWarfareLinkSpec.Value = Config<Settings>.Instance.Skills.WarfareLinkSpec;
			skillValueIndustrialCommandShip.Value = Config<Settings>.Instance.Skills.IndustrialCommandShip;

			skillValueVeldsparP.Value = Config<Settings>.Instance.Skills.VeldsparProcessing;
			skillValueScorditeP.Value = Config<Settings>.Instance.Skills.ScorditeProcessing;
			skillValuePyroxeresP.Value = Config<Settings>.Instance.Skills.PyroxeresProcessing;
			skillValuePlagioclaseP.Value = Config<Settings>.Instance.Skills.PlagioclaseProcessing;
			skillValueOmberP.Value = Config<Settings>.Instance.Skills.OmberProcessing;
			skillValueKerniteP.Value = Config<Settings>.Instance.Skills.KerniteProcessing;
			skillValueJaspetP.Value = Config<Settings>.Instance.Skills.JaspetProcessing;
			skillValueHemorphiteP.Value = Config<Settings>.Instance.Skills.HemorphiteProcessing;
			skillValueHedbergiteP.Value = Config<Settings>.Instance.Skills.HedbergiteProcessing;
			skillValueGneissP.Value = Config<Settings>.Instance.Skills.GneissProcessing;
			skillValueDarkOchreP.Value = Config<Settings>.Instance.Skills.DarkOchreProcessing;
			skillValueSpodumainP.Value = Config<Settings>.Instance.Skills.SpodumainProcessing;
			skillValueCrokiteP.Value = Config<Settings>.Instance.Skills.CrokiteProcessing;
			skillValueBistotP.Value = Config<Settings>.Instance.Skills.BistotProcessing;
			skillValueArkonorP.Value = Config<Settings>.Instance.Skills.ArkonorProcessing;
			skillValueMercoxitP.Value = Config<Settings>.Instance.Skills.MercoxitProcessing;
			
			checkBoxHX2Imp.Checked = Config<Settings>.Instance.ImpHx2;
			checkBoxMichiImp.Checked = Config<Settings>.Instance.ImpMichi;
			checkBoxMindLinkImp.Checked = Config<Settings>.Instance.ImpMindLink;
			checkBoxUseGangBonus.Checked = Config<Settings>.Instance.IsGang;
			comboBoxBoosterShip.SelectedIndex = (int)Config<Settings>.Instance.BoosterShip;
			

			pictureBoxGang1.Image = (Config<Settings>.Instance.GangAssistModule1) ? Resources.icon53_16 : Resources.highSlot;
			pictureBoxGang2.Image = (Config<Settings>.Instance.GangAssistModule2) ? Resources.icon53_16 : Resources.highSlot;
			pictureBoxGang3.Image = (Config<Settings>.Instance.GangAssistModule3) ? Resources.icon53_16 : Resources.highSlot;

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

		}

		/// <summary>
		/// Fills the ships.
		/// </summary>
		private void FillShips()
		{
			Ship ship = new Ship("Not Miner", 0, 0, false, false);
			_dictShips.Add(ship.Name, ship);
			ship = new Ship("Frigate (+20% mining yield per lvl)", 20, 0, false, false);
			_dictShips.Add(ship.Name, ship);
			ship = new Ship("Cruiser (+20% mining yield per lvl)", 20, 0, false, false);
			_dictShips.Add(ship.Name, ship);
			ship = new Ship("Mining Barge", 3, 0, true, false);
			_dictShips.Add(ship.Name, ship);
			ship = new Ship("Mackinaw", 3, 5, true, true);
			_dictShips.Add(ship.Name, ship);
			ship = new Ship("Hulk", 3, 3, true, true);
			_dictShips.Add(ship.Name, ship);
			ship = new Ship("Skif", 3, 3, true, true);
			_dictShips.Add(ship.Name, ship);

			comboBoxShip.BeginUpdate();
			foreach (KeyValuePair<string, Ship> pair in _dictShips)
			{
				comboBoxShip.Items.Add(pair.Value);
			}
			comboBoxShip.EndUpdate();

			comboBoxShip.SelectedIndex = 0;
		}

		/// <summary>
		/// Fills the minig laser upgrades.
		/// </summary>
		private void FillMinigLaserUpgrades()
		{
			//MLU
			LaserUpgrade mlu = new LaserUpgrade("None", 0.0, 0.0, Resources.lowSlot);
			_dictMlu.Add(mlu.Name, mlu);
			mlu = new LaserUpgrade("Mining Laser Upgrade I", 5.0, 0.0, Resources.icon05_12);
			_dictMlu.Add(mlu.Name, mlu);
			mlu = new LaserUpgrade("Erin Mining Upgrade I", 6.0, 0.0, Resources.icon05_12);
			_dictMlu.Add(mlu.Name, mlu);
			mlu = new LaserUpgrade("Elara Mining Upgrade I", 7.0, 0.0, Resources.icon05_12);
			_dictMlu.Add(mlu.Name, mlu);
			mlu = new LaserUpgrade("Carpo Mining Upgrade I", 8.0, 0.0, Resources.icon05_12);
			_dictMlu.Add(mlu.Name, mlu);
			mlu = new LaserUpgrade("Aoede Mining Upgrade I", 9.0, 0.0, Resources.icon05_12);
			_dictMlu.Add(mlu.Name, mlu);
			mlu = new LaserUpgrade("Mining Laser Upgrade II", 9.0, 0.0, Resources.icon05_12_t2);
			_dictMlu.Add(mlu.Name, mlu);
			//IHU
			mlu = new LaserUpgrade("Ice Harvester Upgrade I", 0.0, 5.0, Resources.icon05_12);
			_dictMlu.Add(mlu.Name, mlu);
			mlu = new LaserUpgrade("Crisium Ice Harvester Upgrade I", 0.0, 6.0, Resources.icon05_12);
			_dictMlu.Add(mlu.Name, mlu);
			mlu = new LaserUpgrade("Frigoris Ice Harvester Upgrade I", 0.0, 7.0, Resources.icon05_12);
			_dictMlu.Add(mlu.Name, mlu);
			mlu = new LaserUpgrade("Anguis Ice Harvester Upgrade I", 0.0, 8.0, Resources.icon05_12);
			_dictMlu.Add(mlu.Name, mlu);
			mlu = new LaserUpgrade("Ingenii Ice Harvester Upgrade I", 0.0, 9.0, Resources.icon05_12);
			_dictMlu.Add(mlu.Name, mlu);
			mlu = new LaserUpgrade("Ice Harvester Upgrade II", 0.0, 9.0, Resources.icon05_12_t2);
			_dictMlu.Add(mlu.Name, mlu);


			//Формируем менюшку для выбора MLU
			foreach (KeyValuePair<string, LaserUpgrade> pair in _dictMlu)
			{
				LaserUpgrade device = pair.Value;
				ToolStripItem item = new ToolStripMenuItem(device.Name, device.Image, MenuMluClicked);
				contextMenuStripMLU.Items.Add(item);
				contextMenuStripMLU.Items[contextMenuStripMLU.Items.Count - 1].Tag = device;
			}
		}
		/// <summary>
		/// Fills the turret list.
		/// </summary>
		private void FillTurretList()
		{
			//Mining Turrets
			MiningTurret turret = new MiningTurret("Basic Miner", 60, 30.0, false);
			_turretsList.Add(turret.Name, turret);
			turret = new MiningTurret("Cu Vapor Particle Bore Stream I", 60, 49.0, false);
			_turretsList.Add(turret.Name, turret);
			turret = new MiningTurret("Deep Core Mining Laser I", 60, 40.0, false);
			_turretsList.Add(turret.Name, turret);
			turret = new MiningTurret("Dual Diode Mining Laser I", 60, 44.0, false);
			_turretsList.Add(turret.Name, turret);
			turret = new MiningTurret("EP-S Gaussian I Excavation Pulse", 60, 42, false);
			_turretsList.Add(turret.Name, turret);
			turret = new MiningTurret("Miner I", 60, 40, false);
			_turretsList.Add(turret.Name, turret);
			turret = new MiningTurret("Miner II", 60, 60.0, false);
			_turretsList.Add(turret.Name, turret);
			turret = new MiningTurret("Modulated Deep Core Miner II", 180, 120.0, true);
			_turretsList.Add(turret.Name, turret);
			turret = new MiningTurret("XeCl Drilling Beam I", 60, 47.0, false);
			_turretsList.Add(turret.Name, turret);
			//Strips
			turret = new MiningTurret("Strip Miner I", 180, 540.0, false);
			_turretsList.Add(turret.Name, turret);
			turret = new MiningTurret("Modulated Strip Miner II", 180, 360.0, true);
			_turretsList.Add(turret.Name, turret);
			turret = new MiningTurret("Modulated Deep Core Strip Miner II ", 180, 250.0, true);
			_turretsList.Add(turret.Name, turret);
			//Ice Harvesters
			turret = new MiningTurret("Ice Harvester I", 600, 1.0, false);
			_turretsList.Add(turret.Name, turret);
			turret = new MiningTurret("Ice Harvester II", 500, 1.0, false);
			_turretsList.Add(turret.Name, turret);


			comboBoxTurret.BeginUpdate();
			foreach (KeyValuePair<string, MiningTurret> pair in _turretsList)
			{
				comboBoxTurret.Items.Add(pair.Value);
			}
			comboBoxTurret.EndUpdate();

			comboBoxTurret.SelectedIndex = 0;
		}

		/// <summary>
		/// Handles the SelectionChangeCommitted event of the comboBox control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void comboBox_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if (sender == comboBoxTurret)
			{
				CheckUseCrystals();
				if (_turretsList.ContainsKey(comboBoxTurret.SelectedItem.ToString()))
				{
					Config<Settings>.Instance.SelectedTurret = comboBoxTurret.SelectedItem.ToString();
				}
				CalculateMining();
			}
			else if (sender == comboBoxShip)
			{
				if (_dictShips.ContainsKey(comboBoxShip.SelectedItem.ToString()))
					Config<Settings>.Instance.SelectedShip = comboBoxShip.SelectedItem.ToString();

				CalculateMining();
			}
		}

		/// <summary>
		/// Checks the use crystals.
		/// </summary>
		private void CheckUseCrystals()
		{
			MiningTurret turret = _turretsList[comboBoxTurret.SelectedItem.ToString()];
			radioButtonT1Crystals.Visible = turret.UseCrystals;
			radioButtonT2Crystals.Visible = turret.UseCrystals;
		}

		/// <summary>
		/// Посчитать объем руды с лазера
		/// </summary>
		private void CalculateMining()
		{
			MiningTurret turret = _turretsList[comboBoxTurret.SelectedItem.ToString()];

			double yield = 1000.0;
			double cycleTime = GetCycleTime(turret);
			if (turret.CycleTime < 200)
				yield = GetYield(turret);
			else if (Config<Settings>.Instance.SelectedShip == "Mackinaw")
				yield = 2000.0;

			_timersForm.SetYieldCycle(yield, cycleTime, turret);
			Config<Settings>.Instance.MiningAmount = yield;
			Config<Settings>.Instance.Cycle = cycleTime;

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

			Skills skills = Config<Settings>.Instance.Skills;
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
			if (Config<Settings>.Instance.IsGang)
			{
				double bonusIndustrial = 0.0;
				switch (Config<Settings>.Instance.BoosterShip)
				{
					case BoosterShipType.Orca:
						bonusIndustrial = 0.03;
						break;
					case BoosterShipType.Rorqual:
						bonusIndustrial = 0.05;
						break;
				}

				if (Config<Settings>.Instance.ImpMindLink)
				{
					cycle *= (1 - 2 * skills.MiningDirector * (1 + skills.WarfareLinkSpec * 0.1) *
							(1 + skills.IndustrialCommandShip * bonusIndustrial) * 1.5 / 100 * gangAssistModule);
				}
				else
				{
					cycle *= (1 - 2 * skills.MiningDirector * (1 + skills.WarfareLinkSpec * 0.1) *
							(1 + skills.IndustrialCommandShip * bonusIndustrial) / 100 * gangAssistModule);
				}
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
			Skills skills = Config<Settings>.Instance.Skills;

			Ship ship = _dictShips[comboBoxShip.SelectedItem.ToString()];

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
			if (Config<Settings>.Instance.ImpHx2)
				yield *= 1.05;
			if (Config<Settings>.Instance.ImpMichi)
				yield *= 1.05;

			//Ганг бонусы
			if (Config<Settings>.Instance.IsGang)
			{
				if (Config<Settings>.Instance.ImpMindLink)
				{
					//Implant Replaces mining foreman skill bonus with fixed 15% mining yield bonus.
					yield *= (1 + 0.15/* skills.MiningForeman * 0.02 * 1.5*/);
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
					_mluPictureClicked = pict;
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

		/// <summary>
		/// Called when [mouse enter].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
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
					toolTip1.ToolTipTitle = "Mineral";
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

				if (tooltip.Length > 0)
					toolTip1.SetToolTip(ctrl, tooltip);
				toolTip1.Active = true;
			}
		}

		/// <summary>
		/// Called when [mouse leave].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void On_MouseLeave(object sender, EventArgs e)
		{
			toolTip1.Active = false;
		}

		#endregion

		/// <summary>
		/// Изменили значения скилов
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SkillValueChanged(object sender, EventArgs e)
		{
			if (sender == skillValueMining)
				Config<Settings>.Instance.Skills.Mining = skillValueMining.Value;
			else if (sender == skillValueAstrogeology)
				Config<Settings>.Instance.Skills.Astrogeology = skillValueAstrogeology.Value;
			else if (sender == skillValueMiningBarge)
				Config<Settings>.Instance.Skills.MiningBarge = skillValueMiningBarge.Value;
			else if (sender == skillValueExhumers)
				Config<Settings>.Instance.Skills.Exhumers = skillValueExhumers.Value;
			else if (sender == skillValueIceHarvesting)
				Config<Settings>.Instance.Skills.IceHarvesting = skillValueIceHarvesting.Value;
			else if (sender == skillValueFrigates)
				Config<Settings>.Instance.Skills.Frigates = skillValueFrigates.Value;
			else if (sender == skillValueCruisers)
				Config<Settings>.Instance.Skills.Cruisers = skillValueCruisers.Value;
			else if (sender == skillValueRefining)
				Config<Settings>.Instance.Skills.Refining = skillValueRefining.Value;
			else if (sender == skillValueEfficiency)
				Config<Settings>.Instance.Skills.EfficiencyRefining = skillValueEfficiency.Value;
			else if (sender == skillValueIceProcessing)
				Config<Settings>.Instance.Skills.IceProcessing = skillValueIceProcessing.Value;
			//GANG
			else if (sender == skillValueWarfareLinkSpec)
				Config<Settings>.Instance.Skills.WarfareLinkSpec = skillValueWarfareLinkSpec.Value;
			else if (sender == skillValueMiningForeman)
				Config<Settings>.Instance.Skills.MiningForeman = skillValueMiningForeman.Value;
			else if (sender == skillValueMiningDirector)
				Config<Settings>.Instance.Skills.MiningDirector = skillValueMiningDirector.Value;
			else if (sender == skillValueIndustrialCommandShip)
				Config<Settings>.Instance.Skills.IndustrialCommandShip = skillValueIndustrialCommandShip.Value;


		//Ore Processing
			#region Ore Processing

			else if (sender == skillValueVeldsparP)
				Config<Settings>.Instance.Skills.VeldsparProcessing = skillValueVeldsparP.Value;
			else if (sender == skillValueScorditeP)
				Config<Settings>.Instance.Skills.ScorditeProcessing = skillValueScorditeP.Value;
			else if (sender == skillValuePyroxeresP)
				Config<Settings>.Instance.Skills.PyroxeresProcessing = skillValuePyroxeresP.Value;
			else if (sender == skillValuePlagioclaseP)
				Config<Settings>.Instance.Skills.PlagioclaseProcessing = skillValuePlagioclaseP.Value;
			else if (sender == skillValueOmberP)
				Config<Settings>.Instance.Skills.OmberProcessing = skillValueOmberP.Value;
			else if (sender == skillValueKerniteP)
				Config<Settings>.Instance.Skills.KerniteProcessing = skillValueKerniteP.Value;
			else if (sender == skillValueJaspetP)
				Config<Settings>.Instance.Skills.JaspetProcessing = skillValueJaspetP.Value;
			else if (sender == skillValueHemorphiteP)
				Config<Settings>.Instance.Skills.HemorphiteProcessing = skillValueHemorphiteP.Value;
			else if (sender == skillValueHedbergiteP)
				Config<Settings>.Instance.Skills.HedbergiteProcessing = skillValueHedbergiteP.Value;
			else if (sender == skillValueGneissP)
				Config<Settings>.Instance.Skills.GneissProcessing = skillValueGneissP.Value;
			else if (sender == skillValueDarkOchreP)
				Config<Settings>.Instance.Skills.DarkOchreProcessing = skillValueDarkOchreP.Value;
			else if (sender == skillValueSpodumainP)
				Config<Settings>.Instance.Skills.SpodumainProcessing = skillValueSpodumainP.Value;
			else if (sender == skillValueCrokiteP)
				Config<Settings>.Instance.Skills.CrokiteProcessing = skillValueCrokiteP.Value;
			else if (sender == skillValueBistotP)
				Config<Settings>.Instance.Skills.BistotProcessing = skillValueBistotP.Value;
			else if (sender == skillValueArkonorP)
				Config<Settings>.Instance.Skills.ArkonorProcessing = skillValueArkonorP.Value;
			else if (sender == skillValueMercoxitP)
				Config<Settings>.Instance.Skills.MercoxitProcessing = skillValueMercoxitP.Value;
			#endregion

			CalculateMining();
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
				Config<Settings>.Instance.ImpHx2 = checkBoxHX2Imp.Checked;
			else if (sender == checkBoxMichiImp)
				Config<Settings>.Instance.ImpMichi = checkBoxMichiImp.Checked;
			else if (sender == checkBoxMindLinkImp)
				Config<Settings>.Instance.ImpMindLink = checkBoxMindLinkImp.Checked;
			else if (sender == checkBoxUseGangBonus)
			{
				Config<Settings>.Instance.IsGang = checkBoxUseGangBonus.Checked;
				groupBoxGangBooster.Enabled = Config<Settings>.Instance.IsGang;

			}

			CalculateMining();
		}


	

		/// <summary>
		/// Handles the FormClosing event of the MainForm control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = !_bExit;
			Hide();
		}

		#region NotifyIcon and Menu
		/// <summary>
		/// Shows the window.
		/// </summary>
		/// <param name="form">The form.</param>
		private static void ShowWindow(Form form)
		{
			if (form.Visible)
			{
				form.Activate();
				if (form.WindowState == FormWindowState.Minimized)
					form.WindowState = FormWindowState.Normal;
			}
			else
			{
				form.Show();
			}
		}
		/// <summary>
		/// Handles the MouseClick event of the notifyIcon control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
		private void notifyIcon_MouseDblClick(object sender, MouseEventArgs e)
		{
			ShowWindow(this);
		}
		/// <summary>
		/// Handles the 1 event of the configurationToolStripMenuItem1_Click control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void configurationToolStripMenuItem1_Click_1(object sender, EventArgs e)
		{
			ShowWindow(this);
		}
		/// <summary>
		/// Handles the Click event of the timersToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void timersToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowWindow(_timersForm);
		}

		/// <summary>
		/// Handles the Click event of the calculatorToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void calculatorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowWindow(_calculatorForm);
		}

		/// <summary>
		/// Handles the Click event of the exitToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_bExit = true;
			Close();
		}
		/// <summary>
		/// Handles the clicked event of the MenuMLU control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void MenuMluClicked(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			if (item != null)
			{
				LaserUpgrade mlu = (LaserUpgrade)item.Tag;
				_mluPictureClicked.Image = mlu.Image;
				_mluPictureClicked.Tag = item.Tag;
				if (_mluPictureClicked == pictureBoxMLU1)
				{
					Config<Settings>.Instance.Mlu1 = mlu.Name;
				}
				else if (_mluPictureClicked == pictureBoxMLU2)
				{
					Config<Settings>.Instance.Mlu2 = mlu.Name;
				}
				else if (_mluPictureClicked == pictureBoxMLU3)
				{
					Config<Settings>.Instance.Mlu3 = mlu.Name;
				}
				CalculateMining();
			}
		}

		/// <summary>
		/// Handles the Click event of the aboutToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutForm dlg = new AboutForm();
			dlg.ShowDialog();
		}

		#endregion

		private void btnTimers_Click(object sender, EventArgs e)
		{
			ShowWindow(_timersForm);
		}

		private void btnCalculator_Click(object sender, EventArgs e)
		{
			ShowWindow(_calculatorForm);
		}

		/// <summary>
		/// Handles the Click event of the alwaysOnTopToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Config<Settings>.Instance.AlwaysOnTop = !alwaysOnTopToolStripMenuItem.Checked;
			TopMost = Config<Settings>.Instance.AlwaysOnTop;
			_timersForm.TopMost = Config<Settings>.Instance.AlwaysOnTop;
			_calculatorForm.TopMost = Config<Settings>.Instance.AlwaysOnTop;
		}

		/// <summary>
		/// Handles the DropDownOpening event of the viewToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void viewToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
		{
			alwaysOnTopToolStripMenuItem.Checked = Config<Settings>.Instance.AlwaysOnTop;
		}

		/// <summary>
		/// Handles the SelectedIndexChanged event of the comboBoxBoosterShip control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void comboBoxBoosterShip_SelectedIndexChanged(object sender, EventArgs e)
		{
			BoosterShipType ship = (BoosterShipType)comboBoxBoosterShip.SelectedIndex;
			Config<Settings>.Instance.BoosterShip = ship;
			CalculateMining();
		}

	}
}