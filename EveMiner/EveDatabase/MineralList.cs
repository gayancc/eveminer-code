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
		private static readonly Dictionary<string, Mineral> Dict = new Dictionary<string, Mineral>();
		private static readonly Dictionary<int, Mineral> Dict2 = new Dictionary<int, Mineral>();

		/// <summary>
		/// Initializes the <see cref="MineralList"/> class.
		/// </summary>
		static MineralList()
		{
			Mineral m = new Mineral {Id = 34, Name = "Tritanium", Price = Config<Settings>.Instance.PriceTritanium};
			Dict.Add(m.Name, m);
			Dict2.Add(m.Id, m);

			m = new Mineral {Id = 35, Name = "Pyerite", Price = Config<Settings>.Instance.PricePyerite};
			Dict.Add(m.Name, m);
			Dict2.Add(m.Id, m);

			m = new Mineral {Id = 36, Name = "Mexallon", Price = Config<Settings>.Instance.PriceMexallon};
			Dict.Add(m.Name, m);
			Dict2.Add(m.Id, m);

			m = new Mineral {Id= 37, Name = "Isogen", Price = Config<Settings>.Instance.PriceIsogen};
			Dict.Add(m.Name, m);
			Dict2.Add(m.Id, m);

			m = new Mineral {Id = 38, Name = "Nocxium", Price = Config<Settings>.Instance.PriceNocxium};
			Dict.Add(m.Name, m);
			Dict2.Add(m.Id, m);

			m = new Mineral {Name = "Zydrine", Price = Config<Settings>.Instance.PriceZydrine};
			Dict.Add(m.Name, m);
			Dict2.Add(m.Id, m);

			m = new Mineral {Id= 40, Name = "Megacyte", Price = Config<Settings>.Instance.PriceMegacyte};
			Dict.Add(m.Name, m);
			Dict2.Add(m.Id, m);

			m = new Mineral {Id = 11399, Name = "Morphite", Price = Config<Settings>.Instance.PriceMorphite};
			Dict.Add(m.Name, m);
			Dict2.Add(m.Id, m);
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
		public static Mineral Get(int id)
		{
			if (Dict2.ContainsKey(id))
				return Dict2[id];
			return null;
		}

	}
}