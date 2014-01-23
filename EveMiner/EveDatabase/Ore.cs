namespace EveMiner.EveDatabase
{
	///<summary>
	/// ����
	///</summary>
	public enum OreType
	{
		/// <summary>
		/// ��������
		/// </summary>
		Veldspar,
		/// <summary>
		/// �������
		/// </summary>
		Scordite,
		/// <summary>
		/// ����������
		/// </summary>
		Pyroxeres,
		/// <summary>
		/// ����������
		/// </summary>
		Plagioclase,
		/// <summary>
		/// �����
		/// </summary>
		Omber,
		/// <summary>
		/// ������
		/// </summary>
		Kernite,
		/// <summary>
		/// �������
		/// </summary>
		Jaspet,
		/// <summary>
		/// ��������
		/// </summary>
		Hemorphite,
		/// <summary>
		/// ���������
		/// </summary>
		Hedbergite,
		/// <summary>
		/// �����
		/// </summary>
		Gneiss,
		/// <summary>
		/// ������ ����
		/// </summary>
		DarkOchre,
		/// <summary>
		/// ���������
		/// </summary>
		Spodumain,
		/// <summary>
		/// ������
		/// </summary>
		Crokite,
		/// <summary>
		/// ������
		/// </summary>
		Bistot,
		/// <summary>
		/// �������
		/// </summary>
		Arkonor,
		/// <summary>
		/// ���������
		/// </summary>
		Mercoxit
	}

	/// <summary>
	/// ��� ����
	/// </summary>
	public class Ore
	{
		/// <summary>
		/// EVE ID
		/// </summary>
		public readonly int Id;

		/// <summary>
		/// ��� ����
		/// </summary>
		public readonly string Name;

		/// <summary>
		/// ����� ������� ����
		/// </summary>
		public readonly double Volume;

		/// <summary>
		/// ����� ������ ���� ��� �������
		/// </summary>
		private readonly int _unitsToRefine;

		/// <summary>
		/// ������ ��������� �� ������� ����
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
		/// ����� ������ ���� ��� �������
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