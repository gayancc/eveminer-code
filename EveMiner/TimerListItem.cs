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
		/// ���������� �� ������
		/// </summary>
		private bool timerStarted;

		/// <summary>
		/// ����� �� ��������� ��������� � ��������
		/// </summary>
		public double TimeToAsterEnd
		{
			get { return timeToAsterEnd; }
		}

		/// <summary>
		/// ����� � �������� �� ����� �������� �����
		/// </summary>
		public double TimeToCycleEnd
		{
			get { return timeToCycleEnd; }
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
		/// ���������� �� ������
		/// </summary>
		public bool TimerStarted
		{
			get { return timerStarted; }
			set { timerStarted = value; }
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
			oreUnitPerSecond = miningYield / cycle / ore.Volume;
			timeToAsterEnd = (int)(currentVolume / oreUnitPerSecond);
		}
		/// <summary>
		/// ���
		/// </summary>
		/// <param name="seconds"></param>
		public void Tick(int seconds)
		{

			if(timerStarted == false)
				return;
			currentVolume = CurrentVolume - oreUnitPerSecond*seconds;
			if (CurrentVolume < 0)
				currentVolume = 0;

			timeToCycleEnd = TimeToCycleEnd - seconds;
			if (TimeToCycleEnd < 0)
			{
				timeToCycleEnd = TimeToCycleEnd + cycle;
				PlaySound(Settings.CycleEndFileName);
			}

			timeToAsterEnd = TimeToAsterEnd - seconds;
			if (TimeToAsterEnd < 0)
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
			timerStarted = false;
			currentVolume = StartVolume;
			timeToCycleEnd = cycle;
			timeToAsterEnd = (int)(startVolume / oreUnitPerSecond) + 1;
		}


	}
}
