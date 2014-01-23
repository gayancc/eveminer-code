namespace EveMiner.EveDatabase
{
	/// <summary>
	/// ����� ��������� ��� ����
	/// </summary>
	public class MineralsOut
	{
		/// <summary>
		///  ����� ����� � ����� ������� ����
		/// </summary>
		private readonly int _tritanium;

		/// <summary>
		///  ����� ������ � ����� ������� ����
		/// </summary>
		private readonly int _pyerite;

		/// <summary>
		///  ����� ���������� � ����� ������� ����
		/// </summary>
		private readonly int _mexallon;

		/// <summary>
		///  ����� ������� � ����� ������� ����
		/// </summary>
		private readonly int _isogen;

		/// <summary>
		///  ����� ����� � ����� ������� ����
		/// </summary>
		private readonly int _nocxium;

		/// <summary>
		///  ����� ����� � ����� ������� ����
		/// </summary>
		private readonly int _zydrine;

		/// <summary>
		///  ����� ���� � ����� ������� ����
		/// </summary>
		private readonly int _megacyte;

		/// <summary>
		///  ����� ������� � ����� ������� ����
		/// </summary>
		private readonly int _morphite;

		/// <summary>
		/// ����������� �� ��������� ������ ���������
		/// </summary>
		/// <param name="tritanium">����� �����</param>
		/// <param name="pyerite">����� ������</param>
		/// <param name="mexallon">����� ����������</param>
		/// <param name="isogen">����� �������</param>
		/// <param name="nocxium">����� �����</param>
		/// <param name="zydrine">����� �����</param>
		/// <param name="megacyte">����� ��������</param>
		/// <param name="morphite">����� �������</param>
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
		///  ����� ����� � ����� ������� ����
		/// </summary>
		public int Tritanium
		{
			get { return _tritanium; }
		}

		/// <summary>
		///  ����� ������ � ����� ������� ����
		/// </summary>
		public int Pyerite
		{
			get { return _pyerite; }
		}

		/// <summary>
		///  ����� ���������� � ����� ������� ����
		/// </summary>
		public int Mexallon
		{
			get { return _mexallon; }
		}

		/// <summary>
		///  ����� ������� � ����� ������� ����
		/// </summary>
		public int Isogen
		{
			get { return _isogen; }
		}

		/// <summary>
		///  ����� ����� � ����� ������� ����
		/// </summary>
		public int Nocxium
		{
			get { return _nocxium; }
		}

		/// <summary>
		///  ����� ����� � ����� ������� ����
		/// </summary>
		public int Zydrine
		{
			get { return _zydrine; }
		}

		/// <summary>
		///  ����� ���� � ����� ������� ����
		/// </summary>
		public int Megacyte
		{
			get { return _megacyte; }
		}

		/// <summary>
		///  ����� ������� � ����� ������� ����
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