using EveMiner.Ores;

namespace EveMiner.Ores
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
		/// ��� ����
		/// </summary>
		public string Name;

		/// <summary>
		/// ����� ������� ����
		/// </summary>
		public double Volume;
		/// <summary>
		/// ����� ������ ���� ��� �������
		/// </summary>
		private readonly int unitsToRefine;
		/// <summary>
		/// ������ ��������� �� ������� ����
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
		/// ����� ������ ���� ��� �������
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