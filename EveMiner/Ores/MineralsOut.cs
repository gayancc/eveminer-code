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
		private readonly int _tritanium;
		/// <summary>
		///  Выход пирита с одной единицы руды
		/// </summary>
		private readonly int _pyerite;
		/// <summary>
		///  Выход мексаллона с одной единицы руды
		/// </summary>
		private readonly int _mexallon;
		/// <summary>
		///  Выход изогена с одной единицы руды
		/// </summary>
		private readonly int _isogen;
		/// <summary>
		///  Выход ноксы с одной единицы руды
		/// </summary>
		private readonly int _nocxium;
		/// <summary>
		///  Выход зидры с одной единицы руды
		/// </summary>
		private readonly int _zydrine;
		/// <summary>
		///  Выход меги с одной единицы руды
		/// </summary>
		private readonly int _megacyte;
		/// <summary>
		///  Выход морфита с одной единицы руды
		/// </summary>
		private readonly int _morphite;

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
			_tritanium = tritanium;
			_pyerite = pyerite;
			_mexallon = mexallon;
			_isogen = isogen;
			_nocxium = nocxium;
			_zydrine = zydrine;
			_megacyte = megacyte;
			_morphite = morphite;
		}

		/// <summary>
		///  Выход трита с одной единицы руды
		/// </summary>
		public int Tritanium
		{
			get { return _tritanium; }
		}

		/// <summary>
		///  Выход пирита с одной единицы руды
		/// </summary>
		public int Pyerite
		{
			get { return _pyerite; }
		}

		/// <summary>
		///  Выход мексаллона с одной единицы руды
		/// </summary>
		public int Mexallon
		{
			get { return _mexallon; }
		}

		/// <summary>
		///  Выход изогена с одной единицы руды
		/// </summary>
		public int Isogen
		{
			get { return _isogen; }
		}

		/// <summary>
		///  Выход ноксы с одной единицы руды
		/// </summary>
		public int Nocxium
		{
			get { return _nocxium; }
		}

		/// <summary>
		///  Выход зидры с одной единицы руды
		/// </summary>
		public int Zydrine
		{
			get { return _zydrine; }
		}

		/// <summary>
		///  Выход меги с одной единицы руды
		/// </summary>
		public int Megacyte
		{
			get { return _megacyte; }
		}

		/// <summary>
		///  Выход морфита с одной единицы руды
		/// </summary>
		public int Morphite
		{
			get { return _morphite; }
		}
	}
}