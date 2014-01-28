using System.Collections.Generic;

namespace EveMiner.EveDatabase
{
	/// <summary>
	/// 
	/// </summary>
	public static class MineralList
	{
		/// <summary>
		/// 
		/// </summary>
		public static Dictionary<string, Mineral> Dict = new Dictionary<string, Mineral>();

		/// <summary>
		/// Initializes the <see cref="MineralList"/> class.
		/// </summary>
		static MineralList()
		{
			Mineral m = new Mineral {Name = "Tritanium", Price = Config<Settings>.Instance.PriceTritanium};
			Dict.Add(m.Name, m);

			m = new Mineral {Name = "Pyerite", Price = Config<Settings>.Instance.PricePyerite};
			Dict.Add(m.Name, m);

			m = new Mineral {Name = "Mexallon", Price = Config<Settings>.Instance.PriceMexallon};
			Dict.Add(m.Name, m);

			m = new Mineral {Name = "Isogen", Price = Config<Settings>.Instance.PriceIsogen};
			Dict.Add(m.Name, m);

			m = new Mineral {Name = "Nocxium", Price = Config<Settings>.Instance.PriceNocxium};
			Dict.Add(m.Name, m);

			m = new Mineral {Name = "Zydrine", Price = Config<Settings>.Instance.PriceZydrine};
			Dict.Add(m.Name, m);

			m = new Mineral {Name = "Megacyte", Price = Config<Settings>.Instance.PriceMegacyte};
			Dict.Add(m.Name, m);

			m = new Mineral {Name = "Morphite", Price = Config<Settings>.Instance.PriceMorphite};
			Dict.Add(m.Name, m);
		}

		/// <summary>
		/// Gets the specified name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public static Mineral Get(string name)
		{
			if (Dict.ContainsKey(name))
				return Dict[name];
			return null;
		}
	}
}