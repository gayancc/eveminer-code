using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EveMiner.EveDatabase;
using EveMiner.Properties;

namespace EveMiner.Forms
{
	/// <summary>
	/// 
	/// </summary>
	public partial class MainForm : Form
	{
		private readonly Dictionary<string, MiningTurret> _turretsList = new Dictionary<string, MiningTurret>();
		private readonly Dictionary<string, DeviceBonus> _dictMlu = new Dictionary<string, DeviceBonus>();
		private readonly Dictionary<string, DeviceBonus> _dictImpS10 = new Dictionary<string, DeviceBonus>();
		private readonly Dictionary<string, Ship> _dictShips = new Dictionary<string, Ship>();

		private readonly TimersForm _timersForm = new TimersForm();
		private readonly CalculatorForm _calculatorForm = new CalculatorForm();
		private readonly PricesForm _pricesForm = new PricesForm();

		//Выбранный MLU
		private PictureBox _mluPictureClicked;
		private bool _bExit = true;
		

		/// <summary>
		/// Initializes a new instance of the <see cref="MainForm"/> class.
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
			FillMinigLaserUpgrades();
			FillImplantsMenu();
			FillShips();
			
			FillTurretList();
			
			
			LoadCfg();
		}
		/// <summary>
		/// Fills the implants menu.
		/// </summary>
		private void FillImplantsMenu()
		{
			//Mining implants
			DeviceBonus imp = new DeviceBonus("None", 0.0, 0.0, Resources.slot10);
			_dictImpS10.Add(imp.Name, imp);
			imp = new DeviceBonus("Hardwiring - Inherent Implants 'Highwall' HX-0", 1.0, 0.0, Resources.icon40_16);
			_dictImpS10.Add(imp.Name, imp);
			imp = new DeviceBonus("Hardwiring - Inherent Implants 'Highwall' HX-1", 3.0, 0.0, Resources.icon40_16);
			_dictImpS10.Add(imp.Name, imp);
			imp = new DeviceBonus("Hardwiring - Inherent Implants 'Highwall' HX-2", 5.0, 0.0, Resources.icon40_16);
			_dictImpS10.Add(imp.Name, imp);
			//Ice implants
			imp = new DeviceBonus("Hardwiring - Inherent Implants 'Yeti' BX-0", 0.0, 1.0, Resources.icon40_16);
			_dictImpS10.Add(imp.Name, imp);
			imp = new DeviceBonus("Hardwiring - Inherent Implants 'Yeti' BX-1", 0.0, 3.0, Resources.icon40_16);
			_dictImpS10.Add(imp.Name, imp);
			imp = new DeviceBonus("Hardwiring - Inherent Implants 'Yeti' BX-2", 0.0, 5.0, Resources.icon40_16);
			_dictImpS10.Add(imp.Name, imp);

			//Формируем менюшку для выбора импланта слота 10
			foreach (KeyValuePair<string, DeviceBonus> pair in _dictImpS10)
			{
				DeviceBonus device = pair.Value;
				ToolStripItem item = new ToolStripMenuItem(device.Name, device.Image, MenuSlot10Clicked);
				contextMenuStripImpS10.Items.Add(item);
				contextMenuStripImpS10.Items[contextMenuStripImpS10.Items.Count - 1].Tag = device;
			}


		}

		/// <summary>
		/// Загрузка конфига
		/// </summary>
		private void LoadCfg()
		{
			DeviceBonus devBonus;

			if (_dictMlu.ContainsKey(Config<Settings>.Instance.Mlu1))
			{
				devBonus = _dictMlu[Config<Settings>.Instance.Mlu1];
				pictureBoxMLU1.Image = devBonus.Image;
				pictureBoxMLU1.Tag = devBonus;
			}

			if (_dictMlu.ContainsKey(Config<Settings>.Instance.Mlu2))
			{
				devBonus = _dictMlu[Config<Settings>.Instance.Mlu2];
				pictureBoxMLU2.Image = devBonus.Image;
				pictureBoxMLU2.Tag = devBonus;
			}

			if (_dictMlu.ContainsKey(Config<Settings>.Instance.Mlu3))
			{
				devBonus = _dictMlu[Config<Settings>.Instance.Mlu3];
				pictureBoxMLU3.Image = devBonus.Image;
				pictureBoxMLU3.Tag = devBonus;
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
				_calculatorForm.SetShip(ship);
				for (int n = 0; n < comboBoxShip.Items.Count; n++)
				{
					if (comboBoxShip.Items[n] == ship)
						comboBoxShip.SelectedIndex = n;
				}
				if (ship.LowSlots > 1)
					pictureBoxMLU2.Show();
				if (ship.LowSlots > 2)
					pictureBoxMLU3.Show();
				if (ship.LowSlots < 3)
					pictureBoxMLU3.Hide();
				if (ship.LowSlots < 2)
					pictureBoxMLU2.Hide();

			}

			if (Config<Settings>.Instance.SelectedCrystals == 2)
				radioButtonT2Crystals.Checked = true;
			else
				radioButtonT1Crystals.Checked = true;

			CheckUseCrystals();

			if (_dictImpS10.ContainsKey(Config<Settings>.Instance.ImplantS10))
			{
				devBonus = _dictImpS10[Config<Settings>.Instance.ImplantS10];
				pictureBoxImpSlot10.Image = devBonus.Image;
				pictureBoxImpSlot10.Tag = devBonus;
			}
			if (Config<Settings>.Instance.ImpMichi)
			{
				pictureBoxImpSlot7.Image = Resources.icon40_16;
			}

			TopMost = Config<Settings>.Instance.AlwaysOnTop;
			_timersForm.TopMost = Config<Settings>.Instance.AlwaysOnTop;
			_calculatorForm.TopMost = Config<Settings>.Instance.AlwaysOnTop;

			//Solo
			skillValueMining.Value = Config<Settings>.Instance.Skills.Mining;
			skillValueAstrogeology.Value = Config<Settings>.Instance.Skills.Astrogeology;
			skillValueMiningBarge.Value = Config<Settings>.Instance.Skills.MiningBarge;
			skillValueExhumers.Value = Config<Settings>.Instance.Skills.Exhumers;
			skillValueIceHarvesting.Value = Config<Settings>.Instance.Skills.IceHarvesting;
			skillValueMinigFrigates.Value = Config<Settings>.Instance.Skills.MiningFrigates;
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

			checkBoxMindLinkImp.Checked = Config<Settings>.Instance.ImpMindLink;
			checkBoxUseGangBonus.Checked = Config<Settings>.Instance.IsGang;
			groupBoxGangBooster.Enabled = Config<Settings>.Instance.IsGang;
			comboBoxBoosterShip.SelectedIndex = (int) Config<Settings>.Instance.BoosterShip;


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
			Ship ship = new Ship("Venture", 1, 2);
			_dictShips.Add(ship.Name, ship);
	
			ship = new Ship("Procurer", 2, 1);
			_dictShips.Add(ship.Name, ship);
			ship = new Ship("Retriever", 3, 2);
			_dictShips.Add(ship.Name, ship);
			ship = new Ship("Covetor", 2, 3);
			_dictShips.Add(ship.Name, ship);

			ship = new Ship("Skiff", 2, 1);
			_dictShips.Add(ship.Name, ship);
			ship = new Ship("Mackinaw", 3, 2);
			_dictShips.Add(ship.Name, ship);
			ship = new Ship("Hulk", 2, 3);
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
			DeviceBonus mlu = new DeviceBonus("None", 0.0, 0.0, Resources.lowSlot);
			_dictMlu.Add(mlu.Name, mlu);
			mlu = new DeviceBonus("Mining Laser Upgrade I", 5.0, 0.0, Resources.icon05_12);
			_dictMlu.Add(mlu.Name, mlu);
			mlu = new DeviceBonus("Erin Mining Upgrade I", 6.0, 0.0, Resources.icon05_12);
			_dictMlu.Add(mlu.Name, mlu);
			mlu = new DeviceBonus("Elara Mining Upgrade I", 7.0, 0.0, Resources.icon05_12);
			_dictMlu.Add(mlu.Name, mlu);
			mlu = new DeviceBonus("Carpo Mining Upgrade I", 8.0, 0.0, Resources.icon05_12);
			_dictMlu.Add(mlu.Name, mlu);
			mlu = new DeviceBonus("Aoede Mining Upgrade I", 9.0, 0.0, Resources.icon05_12);
			_dictMlu.Add(mlu.Name, mlu);
			mlu = new DeviceBonus("Mining Laser Upgrade II", 9.0, 0.0, Resources.icon05_12_t2);
			_dictMlu.Add(mlu.Name, mlu);
			//IHU
			mlu = new DeviceBonus("Ice Harvester Upgrade I", 0.0, 5.0, Resources.icon05_12);
			_dictMlu.Add(mlu.Name, mlu);
			mlu = new DeviceBonus("Crisium Ice Harvester Upgrade I", 0.0, 6.0, Resources.icon05_12);
			_dictMlu.Add(mlu.Name, mlu);
			mlu = new DeviceBonus("Frigoris Ice Harvester Upgrade I", 0.0, 7.0, Resources.icon05_12);
			_dictMlu.Add(mlu.Name, mlu);
			mlu = new DeviceBonus("Anguis Ice Harvester Upgrade I", 0.0, 8.0, Resources.icon05_12);
			_dictMlu.Add(mlu.Name, mlu);
			mlu = new DeviceBonus("Ingenii Ice Harvester Upgrade I", 0.0, 9.0, Resources.icon05_12);
			_dictMlu.Add(mlu.Name, mlu);
			mlu = new DeviceBonus("Ice Harvester Upgrade II", 0.0, 9.0, Resources.icon05_12_t2);
			_dictMlu.Add(mlu.Name, mlu);


			//Формируем менюшку для выбора MLU
			foreach (KeyValuePair<string, DeviceBonus> pair in _dictMlu)
			{
				DeviceBonus device = pair.Value;
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
			if(Config<Settings>.Instance.SelectedShip.Length == 0)
				Config<Settings>.Instance.SelectedShip = "Venture";
			Ship ship = _dictShips[Config<Settings>.Instance.SelectedShip];
			_turretsList.Clear();
			if (ship.Type != ShipType.Barge && ship.Type != ShipType.Exhumer)
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
			}
			else
			{
				//Strips
				MiningTurret turret = new MiningTurret("Strip Miner I", 180, 540.0, false);
				_turretsList.Add(turret.Name, turret);
				turret = new MiningTurret("Modulated Strip Miner II", 180, 360.0, true);
				_turretsList.Add(turret.Name, turret);
				turret = new MiningTurret("Modulated Deep Core Strip Miner II ", 180, 250.0, true);
				_turretsList.Add(turret.Name, turret);
				//Ice Harvesters
				turret = new MiningTurret("Ice Harvester I", 300, 1.0, false);
				_turretsList.Add(turret.Name, turret);
				turret = new MiningTurret("Ice Harvester II", 250, 1.0, false);
				_turretsList.Add(turret.Name, turret);
			}


			comboBoxTurret.BeginUpdate();
			comboBoxTurret.Items.Clear();
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
		private void ComboBoxSelectionChangeCommitted(object sender, EventArgs e)
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
				if(_dictShips.ContainsKey(comboBoxShip.SelectedItem.ToString()))
				{
					Ship currentShip = _dictShips[Config<Settings>.Instance.SelectedShip];
					Ship newShip = _dictShips[comboBoxShip.SelectedItem.ToString()];
					if(newShip.LowSlots > 1)
						pictureBoxMLU2.Show();
					if(newShip.LowSlots > 2)
						pictureBoxMLU3.Show();
					if (newShip.LowSlots < 3)
						pictureBoxMLU3.Hide();
					if (newShip.LowSlots < 2)
						pictureBoxMLU2.Hide();

					Config<Settings>.Instance.SelectedShip = comboBoxShip.SelectedItem.ToString();

					if (currentShip.Type != newShip.Type && 
						((currentShip.Type == ShipType.MiningFrigate) || (newShip.Type == ShipType.MiningFrigate)))
						FillTurretList();
					_calculatorForm.SetShip(newShip);
				}
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
			//else if (Config<Settings>.Instance.SelectedShip == "Mackinaw")
			//	yield = 2000.0;

			_timersForm.SetYieldCycle(yield, cycleTime, turret);
			Config<Settings>.Instance.MiningAmount = yield;
			Config<Settings>.Instance.Cycle = cycleTime;
			Ship ship = _dictShips[comboBoxShip.SelectedItem.ToString()];

			double yieldHour = yield * 3600 / cycleTime * ship.TurretSlots;
			Text = string.Format("Eve Miner - {0}m3/{1}sec(1 turret) - {2}m3/hour({3} turret)",
				yield.ToString("F2"), cycleTime.ToString("F2"), yieldHour.ToString("F2"), ship.TurretSlots);
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
			Ship ship = _dictShips[comboBoxShip.SelectedItem.ToString()];
			//Если турелька лед роет
			if (turret.CycleTime > 200)
			{

				cycle *= (1 - skills.IceHarvesting*0.05);

				DeviceBonus mlu = pictureBoxMLU1.Tag as DeviceBonus;
				if (mlu != null)
					cycle *= (1 - mlu.TimeBonus/100);

				mlu = pictureBoxMLU2.Tag as DeviceBonus;
				if (mlu != null && ship.LowSlots > 1)
					cycle *= (1 - mlu.TimeBonus/100);

				mlu = pictureBoxMLU3.Tag as DeviceBonus;
				if (mlu != null && ship.LowSlots > 2)
					cycle *= (1 - mlu.TimeBonus/100);

				
				cycle *= ship.IceHarvestTimeBonus();
				////Если макинаву юзаем
				//if (Config<Settings>.Instance.SelectedShip == "Mackinaw")
				//{
				//	cycle *= 1.25;
				//	cycle *= (1 - skills.Exhumers*0.05);
				//}
				////Если халк
				//else if (Config<Settings>.Instance.SelectedShip == "Hulk")
				//{
				//	cycle *= (1 - skills.Exhumers*0.03);
				//}
				//учет имплантов но только для Ice харвестеров	
				DeviceBonus db = _dictImpS10[Config<Settings>.Instance.ImplantS10];
				double bonus = 1 - db.TimeBonus / 100;
				cycle *= bonus;
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
					case ShipType.Orca:
						bonusIndustrial = 0.03;
						break;
					case ShipType.Rorqual:
						bonusIndustrial = 0.05;
						break;
				}

				if (Config<Settings>.Instance.ImpMindLink)
				{
					cycle *= (1 - 2*skills.MiningDirector*(1 + skills.WarfareLinkSpec*0.1)*
					              (1 + skills.IndustrialCommandShip*bonusIndustrial)*1.5/100*gangAssistModule);
				}
				else
				{
					cycle *= (1 - 2*skills.MiningDirector*(1 + skills.WarfareLinkSpec*0.1)*
					              (1 + skills.IndustrialCommandShip*bonusIndustrial)/100*gangAssistModule);
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

			yield *= (1 + skills.Mining*0.05)*(1 + skills.Astrogeology*0.05);

			yield *= ship.YieldBonus();
			//if (ship.Barge)
			//	yield *= (1 + skills.MiningBarge*0.03);
			//if (ship.Exhumer && ship.Name != "Hulk")
			//	yield *= (1 + skills.Exhumers*0.03);

			//if (ship.Name.Contains("Frigate"))
			//	yield *= (1 + skills.MiningFrigates*0.2);

			//else if (ship.Name.Contains("Cruiser"))
			//	yield *= (1 + skills.Cruisers*0.2);

			DeviceBonus mlu = pictureBoxMLU1.Tag as DeviceBonus;
			if (mlu != null)
				yield *= (1 + mlu.OreYieldBonus/100);

			mlu = pictureBoxMLU2.Tag as DeviceBonus;
			if (mlu != null && ship.LowSlots > 1)
				yield *= (1 + mlu.OreYieldBonus/100);

			mlu = pictureBoxMLU3.Tag as DeviceBonus;
			if (mlu != null && ship.LowSlots > 2)
				yield *= (1 + mlu.OreYieldBonus/100);

			if (turret.UseCrystals)
			{
				if (Config<Settings>.Instance.SelectedCrystals == 2)
				{
					if (ship.Name == "Skif" && turret.Name.Contains("Deep"))
						yield *= 1.375 *(1 + skills.Exhumers*0.6);
					else
						yield *= 1.75;
				}
				else
				{
					if (ship.Name == "Skif" && turret.Name.Contains("Deep"))
						yield *= (1.25*(1 + skills.Exhumers*0.6));
					else
						yield *= 1.625;
				}
			}

			DeviceBonus db = _dictImpS10[Config<Settings>.Instance.ImplantS10];
			double bonus = 1+ db.OreYieldBonus / 100;
			yield *= bonus;
			if (Config<Settings>.Instance.ImpMichi)
				yield *= 1.05;

			//Ганг бонусы
			if (Config<Settings>.Instance.IsGang)
			{
				if (Config<Settings>.Instance.ImpMindLink)
				{
					//Implant Replaces mining foreman skill bonus with fixed 15% mining yield bonus.
					yield *= (1 + 0.15 /* skills.MiningForeman * 0.02 * 1.5*/);
					//yield *= 1/(1 - 2*skills.MiningDirector*(1 + skills.WarfareLinkSpec*0.1)*1.5/100*gangAssistModule);
				}
				else
					yield *= (1 + skills.MiningForeman*0.02);
			}
			return yield;
		}

		/// <summary>
		/// при клике по MLU показать менюшку
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PictureBoxClick(object sender, EventArgs e)
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
				else if (sender == pictureBoxImpSlot7)
				{
					Config<Settings>.Instance.ImpMichi = !Config<Settings>.Instance.ImpMichi;
					if (Config<Settings>.Instance.ImpMichi)
						pictureBoxImpSlot7.Image = Resources.icon40_16;
					else
						pictureBoxImpSlot7.Image = Resources.slot7;
					CalculateMining();
				}
				else if (sender == pictureBoxImpSlot10)
				{
					contextMenuStripImpS10.Show(pict, 0, pict.Height);
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
				if (ctrl is PictureBox && ctrl.Tag is DeviceBonus)
				{
					if(ctrl == pictureBoxImpSlot10)
						toolTip1.ToolTipTitle = "Implant (slot 10)";
					else
						toolTip1.ToolTipTitle = "Laser Upgrade";
					tooltip = ctrl.Tag.ToString();
				}
				else if (ctrl is PictureBox && ctrl.Tag is Ore)
				{
					toolTip1.ToolTipTitle = "Ore";
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
				else if(ctrl == pictureBoxImpSlot7)
				{
					toolTip1.ToolTipTitle = "Implant (slot 7)";
					if (Config<Settings>.Instance.ImpMichi)
						tooltip = "Michi Excavation" + Environment.NewLine + "Mining amount bonus 5%";
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
			else if (sender == skillValueMinigFrigates)
				Config<Settings>.Instance.Skills.MiningFrigates = skillValueMinigFrigates.Value;
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
		private void RadioButtonCrystalsCheckedChanged(object sender, EventArgs e)
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
		private void CheckBoxCheckedChanged(object sender, EventArgs e)
		{
			if (sender == checkBoxMindLinkImp)
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
		private void NotifyIconMouseDblClick(object sender, MouseEventArgs e)
		{
			ShowWindow(this);
		}

		/// <summary>
		/// Handles the 1 event of the configurationToolStripMenuItem1_Click control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void ConfigurationToolStripMenuItem1Click1(object sender, EventArgs e)
		{
			ShowWindow(this);
		}

		/// <summary>
		/// Handles the Click event of the timersToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void TimersToolStripMenuItemClick(object sender, EventArgs e)
		{
			ShowWindow(_timersForm);
		}

		/// <summary>
		/// Handles the Click event of the calculatorToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void CalculatorToolStripMenuItemClick(object sender, EventArgs e)
		{
			ShowWindow(_calculatorForm);
		}

		/// <summary>
		/// Handles the Click event of the exitToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void ExitToolStripMenuItemClick(object sender, EventArgs e)
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
				DeviceBonus mlu = (DeviceBonus) item.Tag;
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
		/// Menus the slot10 clicked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void MenuSlot10Clicked(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			if (item != null)
			{
				DeviceBonus imp = (DeviceBonus)item.Tag;
				
				Config<Settings>.Instance.ImplantS10 = imp.Name;
				pictureBoxImpSlot10.Image = imp.Image;
				pictureBoxImpSlot10.Tag = item.Tag;
				CalculateMining();
			}
		}

		/// <summary>
		/// Handles the Click event of the aboutToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void AboutToolStripMenuItemClick(object sender, EventArgs e)
		{
			AboutForm dlg = new AboutForm();
			dlg.ShowDialog();
		}

		#endregion

		private void BtnTimersClick(object sender, EventArgs e)
		{
			ShowWindow(_timersForm);
		}

		private void BtnCalculatorClick(object sender, EventArgs e)
		{
			ShowWindow(_calculatorForm);
		}
		private void BtnPricesClick(object sender, EventArgs e)
		{
			ShowWindow(_pricesForm);
		}


		/// <summary>
		/// Handles the Click event of the alwaysOnTopToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void AlwaysOnTopToolStripMenuItemClick(object sender, EventArgs e)
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
		private void ViewToolStripMenuItemDropDownOpening(object sender, EventArgs e)
		{
			alwaysOnTopToolStripMenuItem.Checked = Config<Settings>.Instance.AlwaysOnTop;
		}

		/// <summary>
		/// Handles the SelectedIndexChanged event of the comboBoxBoosterShip control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void ComboBoxBoosterShipSelectedIndexChanged(object sender, EventArgs e)
		{
			ShipType ship = (ShipType) comboBoxBoosterShip.SelectedIndex;
			Config<Settings>.Instance.BoosterShip = ship;
			CalculateMining();
		}

	}
}