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
		private readonly int tritanium;
		/// <summary>
		///  ����� ������ � ����� ������� ����
		/// </summary>
		private readonly int pyerite;
		/// <summary>
		///  ����� ���������� � ����� ������� ����
		/// </summary>
		private readonly int mexallon;
		/// <summary>
		///  ����� ������� � ����� ������� ����
		/// </summary>
		private readonly int isogen;
		/// <summary>
		///  ����� ����� � ����� ������� ����
		/// </summary>
		private readonly int nocxium;
		/// <summary>
		///  ����� ����� � ����� ������� ����
		/// </summary>
		private readonly int zydrine;
		/// <summary>
		///  ����� ���� � ����� ������� ����
		/// </summary>
		private readonly int megacyte;
		/// <summary>
		///  ����� ������� � ����� ������� ����
		/// </summary>
		private readonly int morphite;

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
		///  ����� ����� � ����� ������� ����
		/// </summary>
		public int Tritanium
		{
			get { return tritanium; }
		}

		/// <summary>
		///  ����� ������ � ����� ������� ����
		/// </summary>
		public int Pyerite
		{
			get { return pyerite; }
		}

		/// <summary>
		///  ����� ���������� � ����� ������� ����
		/// </summary>
		public int Mexallon
		{
			get { return mexallon; }
		}

		/// <summary>
		///  ����� ������� � ����� ������� ����
		/// </summary>
		public int Isogen
		{
			get { return isogen; }
		}

		/// <summary>
		///  ����� ����� � ����� ������� ����
		/// </summary>
		public int Nocxium
		{
			get { return nocxium; }
		}

		/// <summary>
		///  ����� ����� � ����� ������� ����
		/// </summary>
		public int Zydrine
		{
			get { return zydrine; }
		}

		/// <summary>
		///  ����� ���� � ����� ������� ����
		/// </summary>
		public int Megacyte
		{
			get { return megacyte; }
		}

		/// <summary>
		///  ����� ������� � ����� ������� ����
		/// </summary>
		public int Morphite
		{
			get { return morphite; }
		}
	}
}