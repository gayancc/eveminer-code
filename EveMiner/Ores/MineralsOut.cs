namespace EveMiner.Ores
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
	}
}