using System;
using System.ComponentModel;

namespace EveMiner
{
	/// <summary>
	/// Тип корабля , который бустит
	/// </summary>
	public enum ShipType
	{
		/// <summary>
		/// Неопределенный корабль без бонусов
		/// </summary>
		Common = 0,
		/// <summary>
		/// Orca бонус 3% на каждый уровень
		/// </summary>
		Orca = 1,
		/// <summary>
		/// Rorqual бонус 5%на каждый уровень
		/// </summary>
		Rorqual = 2,
		MiningFrigate = 3,
		Barge = 4,
		Exhumer = 5
	}

	/// <summary>
	/// Настройки приложения
	/// </summary>
	[Serializable]
	public class Settings
	{
		/// <summary>
		/// Скилы 
		/// </summary>
		public Skills Skills = new Skills();

		public const string AsterEndFileName = "AsterEnd.wav";
		public const string CycleEndFileName = "CycleEnd.wav";
		public const int Stand = 0;
		public const int Tax = 1;
		[NonSerialized] public double MiningAmount;
		[NonSerialized] public double Cycle;


		private string _mlu1 = "None";
		private string _mlu2 = "None";
		private string _mlu3 = "None";
		private string _implantS10 = "None";

		private bool _alwaysOnTop;

		public string SelectedTurret = "";
		public string SelectedOre = "";
		public string SelectedShip = "";
		public int SelectedCrystals = 1;

		public bool ImpMichi;
		public bool ImpMindLink;
		public bool IsGang;

		public bool GangAssistModule1;
		public bool GangAssistModule2;
		public bool GangAssistModule3;

		public double Standing;
		public double TaxRate;
		public int StandTaxe;
		public double CargoHold = 27500;

		public double PriceTritanium = 3;
		public double PricePyerite = 5;
		public double PriceMexallon = 35;
		public double PriceIsogen = 65;
		public double PriceNocxium = 110;
		public double PriceZydrine = 2000;
		public double PriceMegacyte = 2500;
		public double PriceMorphite = 8000;

		public ShipType BoosterShip = ShipType.Common;


		[DefaultValue("None")]
		public string Mlu1
		{
			get { return _mlu1; }
			set { _mlu1 = value; }
		}

		[DefaultValue("None")]
		public string Mlu2
		{
			get { return _mlu2; }
			set { _mlu2 = value; }
		}

		[DefaultValue("None")]
		public string Mlu3
		{
			get { return _mlu3; }
			set { _mlu3 = value; }
		}
		[DefaultValue("None")]
		public string ImplantS10
		{
			get { return _implantS10; }
			set { _implantS10 = value; }
		}

		[DefaultValue(false)]
		public bool AlwaysOnTop
		{
			get { return _alwaysOnTop; }
			set { _alwaysOnTop = value; }
		}

		/// <summary>
		/// Возвращает текущий налог
		/// </summary>
		/// <returns></returns>
		public static double GetTaxRate()
		{
			double tax;
			if (Config<Settings>.Instance.StandTaxe == Stand)
				tax = (5 - Config<Settings>.Instance.Standing*5/6.66666)/100;
			else //if(Config<Settings>.Instance.StandTaxe == Tax)
				tax = Config<Settings>.Instance.TaxRate/100;

			if (tax < 0)
				tax = 0;
			return tax;
		}
	}
}