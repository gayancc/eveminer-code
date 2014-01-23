namespace EveMiner.EveDatabase
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
		/// EVE ID
		/// </summary>
		public readonly int Id;

		/// <summary>
		/// Имя руды
		/// </summary>
		public readonly string Name;

		/// <summary>
		/// Объем единицы руды
		/// </summary>
		public readonly double Volume;

		/// <summary>
		/// Число единиц руды для рефайна
		/// </summary>
		private readonly int _unitsToRefine;

		/// <summary>
		/// Выхлоп минеарлов на единицу руды
		/// </summary>
		public readonly MineralsOut MineralsOut;

		/// <summary>
		///  
		/// </summary>
		/// <param name="id"></param>
		/// <param name="name"></param>
		/// <param name="volume"></param>
		/// <param name="unitsToRefine"></param>
		/// <param name="mineralsOut"></param>
		public Ore(int id, string name, double volume, int unitsToRefine, MineralsOut mineralsOut)
		{
			Name = name;
			Volume = volume;
			this._unitsToRefine = unitsToRefine;
			MineralsOut = mineralsOut;
			Id = id;
		}

		/// <summary>
		/// Число единиц руды для рефайна
		/// </summary>
		public int UnitsToRefine
		{
			get { return _unitsToRefine; }
		}


		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </returns>
		public override string ToString()
		{
			return Name;
		}
	}
}