namespace EveMiner.EveDatabase
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
		public MineralsOut(int tritanium, int pyerite, int mexallon, int isogen, int nocxium, int zydrine, int megacyte,
		                   int morphite)
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

		/// <summary>
		/// Implements the operator +.
		/// </summary>
		/// <param name="mo1">The mo1.</param>
		/// <param name="mo2">The mo2.</param>
		/// <returns>The result of the operator.</returns>
		public static MineralsOut operator +(MineralsOut mo1, MineralsOut mo2)
		{
			return new MineralsOut(mo1.Tritanium + mo2.Tritanium,
			                       mo1.Pyerite + mo2.Pyerite,
			                       mo1.Mexallon + mo2.Mexallon,
			                       mo1.Isogen + mo2.Isogen,
			                       mo1.Nocxium + mo2.Nocxium,
			                       mo1.Zydrine + mo2.Zydrine,
			                       mo1.Megacyte + mo2.Megacyte,
			                       mo1.Morphite + mo2.Morphite);
		}
	}
}