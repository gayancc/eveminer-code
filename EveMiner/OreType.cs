using System;
using System.Collections.Generic;
using System.Text;

namespace EveMiner
{
	public enum Ore
	{
		Veldspar,
		Scordite,
		Pyroxeres,
		Plagioclase,
		Omber,
		Kernite,
		Jaspet,
		Hemorphite,
		Hedbergite,
		Gneiss,
		DarkOchre,
		Spodumain,
		Crokite,
		Bistot,
		Arkonor,
		Mercoxit
	}
	/// <summary>
	/// Тип руды
	/// </summary>
	public class OreType
	{
		/// <summary>
		/// Имя руды
		/// </summary>
		public string Name;

		/// <summary>
		/// Объем единицы руды
		/// </summary>
		public double Volume;

		public int UnitsToRefine;

		public MineralsOut MineralsOut;

		public OreType(string name, double volume, int unitsToRefine, MineralsOut mineralsOut)
		{
			Name = name;
			Volume = volume;
			UnitsToRefine = unitsToRefine;
			MineralsOut = mineralsOut;
		}


		public override string ToString()
		{
			return Name;
		}
	}
}
