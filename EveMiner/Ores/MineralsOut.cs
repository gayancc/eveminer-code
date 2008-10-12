namespace EveMiner.Ores
{
	/// <summary>
	/// Выход минералов для руды
	/// </summary>
	public class MineralsOut
	{
		/// <summary>
		///  Выход трита с одной единицы руды
		/// </summary>
		private readonly int tritanium;
		/// <summary>
		///  Выход пирита с одной единицы руды
		/// </summary>
		private readonly int pyerite;
		/// <summary>
		///  Выход мексаллона с одной единицы руды
		/// </summary>
		private readonly int mexallon;
		/// <summary>
		///  Выход изогена с одной единицы руды
		/// </summary>
		private readonly int isogen;
		/// <summary>
		///  Выход ноксы с одной единицы руды
		/// </summary>
		private readonly int nocxium;
		/// <summary>
		///  Выход зидры с одной единицы руды
		/// </summary>
		private readonly int zydrine;
		/// <summary>
		///  Выход меги с одной единицы руды
		/// </summary>
		private readonly int megacyte;
		/// <summary>
		///  Выход морфита с одной единицы руды
		/// </summary>
		private readonly int morphite;

		/// <summary>
		/// Конструктор по значениям выхода минералов
		/// </summary>
		/// <param name="tritanium">выход трита</param>
		/// <param name="pyerite">выход пирита</param>
		/// <param name="mexallon">выход мексаллона</param>
		/// <param name="isogen">выход изогена</param>
		/// <param name="nocxium">выход ноксы</param>
		/// <param name="zydrine">выход зидры</param>
		/// <param name="megacyte">выход мегацита</param>
		/// <param name="morphite">выход морфита</param>
		public MineralsOut(int tritanium, int pyerite, int mexallon, int isogen, int nocxium, int zydrine, int megacyte, int morphite)
		{
			this.tritanium = tritanium;
			this.pyerite = pyerite;
			this.mexallon = mexallon;
			this.isogen = isogen;
			this.nocxium = nocxium;
			this.zydrine = zydrine;
			this.megacyte = megacyte;
			this.morphite = morphite;
		}

		/// <summary>
		///  Выход трита с одной единицы руды
		/// </summary>
		public int Tritanium
		{
			get { return tritanium; }
		}

		/// <summary>
		///  Выход пирита с одной единицы руды
		/// </summary>
		public int Pyerite
		{
			get { return pyerite; }
		}

		/// <summary>
		///  Выход мексаллона с одной единицы руды
		/// </summary>
		public int Mexallon
		{
			get { return mexallon; }
		}

		/// <summary>
		///  Выход изогена с одной единицы руды
		/// </summary>
		public int Isogen
		{
			get { return isogen; }
		}

		/// <summary>
		///  Выход ноксы с одной единицы руды
		/// </summary>
		public int Nocxium
		{
			get { return nocxium; }
		}

		/// <summary>
		///  Выход зидры с одной единицы руды
		/// </summary>
		public int Zydrine
		{
			get { return zydrine; }
		}

		/// <summary>
		///  Выход меги с одной единицы руды
		/// </summary>
		public int Megacyte
		{
			get { return megacyte; }
		}

		/// <summary>
		///  Выход морфита с одной единицы руды
		/// </summary>
		public int Morphite
		{
			get { return morphite; }
		}
	}
}