using System.Collections.Generic;

namespace EveMiner.EveDatabase
{
	/// <summary>
	/// Ore List
	/// </summary>
	public static class OreList
	{
		/// <summary>
		/// 
		/// </summary>
		public static readonly Dictionary<string, Ore> DictOre = new Dictionary<string, Ore>();

		static OreList()
		{
			//Veldspar
			MineralsOut mOut = new MineralsOut(1000, 0, 0, 0, 0, 0, 0, 0);
			Ore ore = new Ore(1230, "Veldspar", 0.1, 333, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(1050, 0, 0, 0, 0, 0, 0, 0);
			ore = new Ore(17470, "Concentrated Veldspar", 0.1, 333, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(1100, 0, 0, 0, 0, 0, 0, 0);
			ore = new Ore(17471, "Dense Veldspar", 0.1, 333, mOut);
			DictOre.Add(ore.Name, ore);

			//Scordite
			mOut = new MineralsOut(833, 416, 0, 0, 0, 0, 0, 0);
			ore = new Ore(1228, "Scordite", 0.15, 333, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(875, 437, 0, 0, 0, 0, 0, 0);
			ore = new Ore(17463, "Condensed Scordite", 0.15, 333, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(916, 458, 0, 0, 0, 0, 0, 0);
			ore = new Ore(17464, "Massive Scordite", 0.15, 333, mOut);
			DictOre.Add(ore.Name, ore);

			//Pyroxeres
			mOut = new MineralsOut(844, 59, 120, 0, 11, 0, 0, 0);
			ore = new Ore(1224, "Pyroxeres", 0.3, 333, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(886, 62, 126, 0, 12, 0, 0, 0);
			ore = new Ore(17459, "Solid Pyroxeres", 0.3, 333, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(928, 65, 132, 0, 12, 0, 0, 0);
			ore = new Ore(17460, "Viscous Pyroxeres", 0.3, 333, mOut);
			DictOre.Add(ore.Name, ore);

			//Plagioclase
			mOut = new MineralsOut(256, 512, 256, 0, 0, 0, 0, 0);
			ore = new Ore(18, "Plagioclase", 0.35, 333, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(269, 538, 269, 0, 0, 0, 0, 0);
			ore = new Ore(17455, "Azure Plagioclase", 0.35, 333, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(282, 563, 282, 0, 0, 0, 0, 0);
			ore = new Ore(17456, "Rich Plagioclase", 0.35, 333, mOut);
			DictOre.Add(ore.Name, ore);

			//Omber
			mOut = new MineralsOut(307, 123, 0, 307, 0, 0, 0, 0);
			ore = new Ore(1227, "Omber", 0.6, 500, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(322, 129, 0, 322, 0, 0, 0, 0);
			ore = new Ore(17867, "Silvery Omber", 0.6, 500, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(338, 135, 0, 338, 0, 0, 0, 0);
			ore = new Ore(17868, "Golden Omber", 0.6, 500, mOut);
			DictOre.Add(ore.Name, ore);

			//Kernite
			mOut = new MineralsOut(386, 0, 773, 386, 0, 0, 0, 0);
			ore = new Ore(20, "Kernite", 1.2, 400, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(405, 0, 812, 405, 0, 0, 0, 0);
			ore = new Ore(17452, "Luminous Kernite", 1.2, 400, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(425, 0, 850, 425, 0, 0, 0, 0);
			ore = new Ore(17453, "Fiery Kernite", 1.2, 400, mOut);
			DictOre.Add(ore.Name, ore);

			//Jaspet
			mOut = new MineralsOut(259, 437, 518, 0, 259, 8, 0, 0);
			ore = new Ore(1226, "Jaspet", 2.0, 500, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(272, 459, 544, 0, 272, 8, 0, 0);
			ore = new Ore(17448, "Pure Jaspet", 2.0, 500, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(285, 481, 570, 0, 285, 9, 0, 0);
			ore = new Ore(17449, "Pristine Jaspet", 2.0, 500, mOut);
			DictOre.Add(ore.Name, ore);

			//Hemorphite
			mOut = new MineralsOut(650, 260, 60, 212, 424, 28, 0, 0);
			ore = new Ore(1231, "Hemorphite", 3.0, 500, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(683, 273, 63, 223, 445, 29, 0, 0);
			ore = new Ore(17444, "Vivid Hemorphite", 3.0, 500, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(715, 286, 66, 233, 466, 31, 0, 0);
			ore = new Ore(17445, "Radiant Hemorphite", 3.0, 500, mOut);
			DictOre.Add(ore.Name, ore);

			//Hedbergite
			mOut = new MineralsOut(0, 290, 0, 708, 354, 32, 0, 0);
			ore = new Ore(21, "Hedbergite", 3.0, 500, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(0, 305, 0, 743, 372, 34, 0, 0);
			ore = new Ore(17440, "Vitric Hedbergite", 3.0, 500, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(0, 319, 0, 779, 389, 35, 0, 0);
			ore = new Ore(17441, "Glazed Hedbergite", 3.0, 500, mOut);
			DictOre.Add(ore.Name, ore);

			//Gneiss
			mOut = new MineralsOut(3700, 0, 3700, 700, 0, 171, 0, 0);
			ore = new Ore(1229, "Gneiss", 5.0, 400, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(3885, 0, 3885, 735, 0, 180, 0, 0);
			ore = new Ore(17865, "Iridescent Gneiss", 5.0, 400, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(4070, 0, 4070, 770, 0, 188, 0, 0);
			ore = new Ore(17866, "Prismatic Gneiss", 5.0, 400, mOut);
			DictOre.Add(ore.Name, ore);

			//Dark Ochre
			mOut = new MineralsOut(25500, 0, 0, 0, 500, 250, 0, 0);
			ore = new Ore(1232, "Dark Ochre", 8.0, 400, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(26775, 0, 0, 0, 525, 263, 0, 0);
			ore = new Ore(17436, "Onyx Ochre", 8.0, 400, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(28050, 0, 0, 0, 550, 275, 0, 0);
			ore = new Ore(17437, "Obsidian Ochre", 8.0, 400, mOut);
			DictOre.Add(ore.Name, ore);

			//Spodumain
			mOut = new MineralsOut(71000, 9000, 0, 0, 0, 0, 140, 0);
			ore = new Ore(19, "Spodumain", 16.0, 250, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(74550, 9450, 0, 0, 0, 0, 147, 0);
			ore = new Ore(17466, "Bright Spodumain", 16.0, 250, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(78100, 9900, 0, 0, 0, 0, 154, 0);
			ore = new Ore(17467, "Gleaming Spodumain", 16.0, 250, mOut);
			DictOre.Add(ore.Name, ore);

			//Crokite
			mOut = new MineralsOut(38000, 0, 0, 0, 331, 663, 0, 0);
			ore = new Ore(1225, "Crokite", 16.0, 250, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(39900, 0, 0, 0, 348, 696, 0, 0);
			ore = new Ore(17432, "Sharp Crokite", 16.0, 250, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(41800, 0, 0, 0, 364, 729, 0, 0);
			ore = new Ore(17433, "Crystalline Crokite", 16.0, 250, mOut);
			DictOre.Add(ore.Name, ore);

			//Bistot
			mOut = new MineralsOut(0, 12000, 0, 0, 0, 341, 170, 0);
			ore = new Ore(1223, "Bistot", 16.0, 200, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(0, 12600, 0, 0, 0, 358, 179, 0);
			ore = new Ore(17428, "Triclinic Bistot", 16.0, 200, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(0, 13200, 0, 0, 0, 375, 187, 0);
			ore = new Ore(17429, "Monoclinic Bistot", 16.0, 200, mOut);
			DictOre.Add(ore.Name, ore);

			//Arkonor
			mOut = new MineralsOut(10000, 0, 0, 0, 0, 166, 333, 0);
			ore = new Ore(22, "Arkonor", 16.0, 200, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(10500, 0, 0, 0, 0, 174, 350, 0);
			ore = new Ore(17425, "Crimson Arkonor", 16.0, 200, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(11000, 0, 0, 0, 0, 183, 366, 0);
			ore = new Ore(17426, "Prime Arkonor", 16.0, 200, mOut);
			DictOre.Add(ore.Name, ore);

			//Mercoxit
			mOut = new MineralsOut(0, 0, 0, 0, 0, 0, 0, 530);
			ore = new Ore(11396, "Mercoxit", 40.0, 250, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(0, 0, 0, 0, 0, 0, 0, 557);
			ore = new Ore(17869, "Magma Mercoxit", 40.0, 250, mOut);
			DictOre.Add(ore.Name, ore);
			mOut = new MineralsOut(0, 0, 0, 0, 0, 0, 0, 583);
			ore = new Ore(17870, "Vitreous Mercoxit", 40.0, 250, mOut);
			DictOre.Add(ore.Name, ore);


			//ICE
//			mOut = new MineralsOut(0, 0, 0, 0, 0, 0, 0, 0);
//			ore = new Ore("Ice", 1000.0, 1, mOut);
//			DictOre.Add(ore.Name, ore);
			//GAS
//			mOut = new MineralsOut(0, 0, 0, 0, 0, 0, 0, 0);
//			ore = new Ore("Gas", 10.0, 1, mOut);
//			DictOre.Add(ore.Name, ore);


		}

		/// <summary>
		/// Gets the specified ore name.
		/// </summary>
		/// <param name="oreName">Name of the ore.</param>
		/// <returns></returns>
		public static Ore Get(string oreName)
		{
			if (DictOre.ContainsKey(oreName))
				return DictOre[oreName];
			return null;
		}

		/// <summary>
		/// получить уровень процессинга для руды
		/// </summary>
		/// <param name="ore"></param>
		/// <returns></returns>
		public static int GetProcessingSkill(Ore ore)
		{
			if (ore.Name.Contains("Veldspar"))
				return Config<Settings>.Instance.Skills.VeldsparProcessing;
			if (ore.Name.Contains("Scordite"))
				return Config<Settings>.Instance.Skills.ScorditeProcessing;
			if (ore.Name.Contains("Pyroxeres"))
				return Config<Settings>.Instance.Skills.PyroxeresProcessing;
			if (ore.Name.Contains("Plagioclase"))
				return Config<Settings>.Instance.Skills.PlagioclaseProcessing;
			if (ore.Name.Contains("Omber"))
				return Config<Settings>.Instance.Skills.OmberProcessing;
			if (ore.Name.Contains("Kernite"))
				return Config<Settings>.Instance.Skills.KerniteProcessing;
			if (ore.Name.Contains("Jaspet"))
				return Config<Settings>.Instance.Skills.JaspetProcessing;
			if (ore.Name.Contains("Hemorphite"))
				return Config<Settings>.Instance.Skills.HemorphiteProcessing;
			if (ore.Name.Contains("Hedbergite"))
				return Config<Settings>.Instance.Skills.HedbergiteProcessing;
			if (ore.Name.Contains("Gneiss"))
				return Config<Settings>.Instance.Skills.GneissProcessing;
			if (ore.Name.Contains("Ochre"))
				return Config<Settings>.Instance.Skills.DarkOchreProcessing;
			if (ore.Name.Contains("Bistot"))
				return Config<Settings>.Instance.Skills.BistotProcessing;
			if (ore.Name.Contains("Spodumain"))
				return Config<Settings>.Instance.Skills.SpodumainProcessing;
			if (ore.Name.Contains("Crokite"))
				return Config<Settings>.Instance.Skills.CrokiteProcessing;
			if (ore.Name.Contains("Arkonor"))
				return Config<Settings>.Instance.Skills.ArkonorProcessing;
			if (ore.Name.Contains("Mercoxit"))
				return Config<Settings>.Instance.Skills.MercoxitProcessing;

			return 0;
		}

		/// <summary>
		/// Gets the efficiency.
		/// </summary>
		/// <param name="ore">The ore.</param>
		/// <param name="netYield">The net yield.</param>
		/// <returns></returns>
		public static double GetEfficiency(Ore ore, double netYield)
		{
			Skills skills = Config<Settings>.Instance.Skills;
			double eff = netYield +
			             0.375*(1 + skills.Refining*0.02)*(1 + skills.EfficiencyRefining*0.04)*
			             (1 + GetProcessingSkill(ore)*0.05);
			if (eff > 1.0)
				eff = 1.0;
			return eff;
		}
	}
}