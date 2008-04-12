using System;
using System.ComponentModel;

namespace EveMiner
{
	[Serializable]
	public class Settings
	{
		/// <summary>
		/// скилы 
		/// </summary>
		public Skills skills = new Skills();

		private string mlu1 = "None";
		private string mlu2 = "None";
		private string mlu3 = "None";

		private bool alwaysOnTop = true;
		
		public string SelectedTurret = "";
		public string SelectedOre = "";
		public string SelectedShip = "";
		public int SelectedCrystals = 1;

		public bool ImpHX2 = false;
		public bool ImpMichi = false;
		public bool ImpMindLink = false;
		public bool isGang = false;
		public int SelectedTabIndex;
		
		public bool GangAssistModule1;
		public bool GangAssistModule2;
		public bool GangAssistModule3;
		
		public double Standing;
		
		public double PriceTritanium = 3;
		public double PricePyerite = 5;
		public double PriceMexallon = 35;
		public double PriceIsogen = 65;
		public double PriceNocxium = 110;
		public double PriceZydrine = 2000;
		public double PriceMegacyte = 2500;
		public double PriceMorphite = 8000;


		[DefaultValue("None")]
		public string Mlu1
		{
			get { return mlu1; }
			set { mlu1 = value; }
		}

		[DefaultValue("None")]
		public string Mlu2
		{
			get { return mlu2; }
			set { mlu2 = value; }
		}

		[DefaultValue("None")]
		public string Mlu3
		{
			get { return mlu3; }
			set { mlu3 = value; }
		}

		[DefaultValue(true)]
		public bool AlwaysOnTop
		{
			get { return alwaysOnTop; }
			set { alwaysOnTop = value; }
		}

		public string AsterEndFileName
		{
			get { return "AsterEnd.wav"; }

		}

		public string CycleEndFileName
		{
			get { return "CycleEnd.wav"; }

		}

	}
}
