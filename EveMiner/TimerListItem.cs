using System.IO;
using System.Windows.Forms;
using EveMiner.Ores;

namespace EveMiner
{
	/// <summary>
	/// ������� �� ������ ��������
	/// </summary>
	class TimerListItem
	{
		/// <summary>
		/// ����
		/// </summary>
		public Ore ore;
		/// <summary>
		/// ���� � ��������� 
		/// </summary>
		private readonly double startVolume;
		/// <summary>
		/// ������� �������� ���� � ������
		/// </summary>
		private double currentVolume;
		/// <summary>
		/// ���� ������
		/// </summary>
		private readonly double cycle;
		/// <summary>
		/// ����� � �������� �� ����� �������� �����
		/// </summary>
		private double timeToCycleEnd;
		/// <summary>
		/// ����� �� ��������� ��������� � ��������
		/// </summary>
		private double timeToAsterEnd;

		/// <summary>
		/// ������ ���� �� 1 �������
		/// </summary>
		private double oreUnitPerSecond;

		/// <summary>
		/// ��������� �� ������ �����
		/// </summary>
		private bool laser1Started;
		/// <summary>
		/// ��������� �� ������ �����
		/// </summary>
		private bool laser2Started;
		/// <summary>
		/// ��������� �� ������ �����
		/// </summary>
		private bool laser3Started;

		/// <summary>
		/// ����� �� ��������� ��������� � ��������
		/// </summary>
		public double TimeToAsterEnd
		{
			get
			{
				if(LasersStarted > 0)
					return timeToAsterEnd / LasersStarted;

				return timeToAsterEnd;
			}
		}

		/// <summary>
		/// ����� � �������� �� ����� �������� �����
		/// </summary>
		public double TimeToCycleEnd
		{
			get
			{
				return timeToCycleEnd;
			}
		}

		/// <summary>
		/// ������� �������� ���� � ������
		/// </summary>
		public double CurrentVolume
		{
			get { return currentVolume; }
		}

		/// <summary>
		/// ���� � ��������� 
		/// </summary>
		public double StartVolume
		{
			get { return startVolume; }
		}

		/// <summary>
		/// ������� ������� ����������
		/// </summary>
		public int LasersStarted
		{
			get
			{
				int ret = 0;
				if (laser1Started)
					ret++;
				if(laser2Started)
					ret++;
				if (laser3Started)
					ret++;
				return ret;
			}
		}

		/// <summary>
		/// ���� ������
		/// </summary>
		public double Cycle
		{
			get { return cycle; }
		}

		/// <summary>
		/// ��������� �� ������ �����
		/// </summary>
		public bool Laser1Started
		{
			get { return laser1Started; }
			set { laser1Started = value; }
		}

		/// <summary>
		/// ��������� �� ������ �����
		/// </summary>
		public bool Laser2Started
		{
			get { return laser2Started; }
			set { laser2Started = value; }
		}

		/// <summary>
		/// ��������� �� ������ �����
		/// </summary>
		public bool Laser3Started
		{
			get { return laser3Started; }
			set { laser3Started = value; }
		}

		public TimerListItem(Ore ore, double startVolume, double cycle, double miningYield)
		{
			this.ore = ore;
			this.startVolume = startVolume;
			currentVolume = StartVolume;
			this.cycle = cycle;
			timeToCycleEnd = cycle;

			oreUnitPerSecond = miningYield / cycle / ore.Volume;

			timeToAsterEnd = (int)(startVolume / oreUnitPerSecond);
		}
		/// <summary>
		/// �������� �������� ���� �� ����
		/// </summary>
		/// <param name="miningYield"></param>
		public void SetMiningYield(double  miningYield)
		{
			oreUnitPerSecond = miningYield / Cycle / ore.Volume;
			timeToAsterEnd = (int)(currentVolume / oreUnitPerSecond);
		}
		/// <summary>
		/// ���
		/// </summary>
		/// <param name="seconds"></param>
		public void Tick(int seconds)
		{

			if(LasersStarted == 0)
				return;
			currentVolume = CurrentVolume - oreUnitPerSecond*seconds*LasersStarted;
			if (CurrentVolume < 0)
				currentVolume = 0;

			timeToCycleEnd = timeToCycleEnd - seconds;
			if (timeToCycleEnd < 0)
			{
				timeToCycleEnd = timeToCycleEnd + Cycle;
				PlaySound(Settings.CycleEndFileName);
			}

			timeToAsterEnd = timeToAsterEnd - seconds * LasersStarted;
			if (timeToAsterEnd < 0)
			{
				timeToAsterEnd = 0;
			}
		    if(timeToAsterEnd != 0) return;
		    currentVolume = 0;
		    PlaySound(Settings.AsterEndFileName);
		}

		private static void PlaySound(string filename)
		{
			try
			{
				string file = Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar +
				              filename;
				System.Media.SoundPlayer sp = new System.Media.SoundPlayer(file);
				sp.Play();
			}
			catch (FileNotFoundException)
			{
			}
		}

		/// <summary>
		/// ����� ������� � �������� ���������
		/// </summary>
		public void Reset()
		{
			laser1Started = false;
			laser2Started = false;
			laser3Started = false;
			currentVolume = StartVolume;
			timeToCycleEnd = Cycle;
			timeToAsterEnd = (int)(startVolume / oreUnitPerSecond) + 1;
		}


	}
}
