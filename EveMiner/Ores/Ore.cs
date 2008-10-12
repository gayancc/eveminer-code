using EveMiner.Ores;

namespace EveMiner.Ores
{
	///<summary>
	/// Руда
	///</summary>
	public enum OreType
	{
		/// <summary>
		/// Велдспар
		/// </summary>
		Veldspar,
		/// <summary>
		/// Скордит
		/// </summary>
		Scordite,
		/// <summary>
		/// Пироксеерз
		/// </summary>
		Pyroxeres,
		/// <summary>
		/// Плагиоклаз
		/// </summary>
		Plagioclase,
		/// <summary>
		/// Омбер
		/// </summary>
		Omber,
		/// <summary>
		/// Кернит
		/// </summary>
		Kernite,
		/// <summary>
		/// Джаспет
		/// </summary>
		Jaspet,
		/// <summary>
		/// Хеморфит
		/// </summary>
		Hemorphite,
		/// <summary>
		/// Хедбергит
		/// </summary>
		Hedbergite,
		/// <summary>
		/// Гнайс
		/// </summary>
		Gneiss,
		/// <summary>
		/// Темная охра
		/// </summary>
		DarkOchre,
		/// <summary>
		/// Сподумайн
		/// </summary>
		Spodumain,
		/// <summary>
		/// Крокит
		/// </summary>
		Crokite,
		/// <summary>
		/// Бистот
		/// </summary>
		Bistot,
		/// <summary>
		/// Арконор
		/// </summary>
		Arkonor,
		/// <summary>
		/// Меркоксит
		/// </summary>
		Mercoxit
	}

	/// <summary>
	/// Тип руды
	/// </summary>
	public class Ore
	{
		/// <summary>
		/// Имя руды
		/// </summary>
		public string Name;

		/// <summary>
		/// Объем единицы руды
		/// </summary>
		public double Volume;
		/// <summary>
		/// Число единиц руды для рефайна
		/// </summary>
		private readonly int unitsToRefine;
		/// <summary>
		/// Выхлоп минеарлов на единицу руды
		/// </summary>
		public MineralsOut MineralsOut;

		///<summary>
		/// 
		///</summary>
		///<param name="name"></param>
		///<param name="volume"></param>
		///<param name="unitsToRefine"></param>
		///<param name="mineralsOut"></param>
		public Ore(string name, double volume, int unitsToRefine, MineralsOut mineralsOut)
		{
			Name = name;
			Volume = volume;
			this.unitsToRefine = unitsToRefine;
			MineralsOut = mineralsOut;
		}
		/// <summary>
		/// Число единиц руды для рефайна
		/// </summary>
		public int UnitsToRefine
		{
			get { return unitsToRefine; }
		}


		public override string ToString()
		{
			return Name;
		}
	}
}