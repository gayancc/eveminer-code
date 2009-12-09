using System.IO;
using System.Windows.Forms;
using EveMiner.Ores;

namespace EveMiner
{
	/// <summary>
	/// Элемент из списка таймеров
	/// </summary>
	class TimerListItem
	{
		/// <summary>
		/// Руда
		/// </summary>
		public Ore ore;
		/// <summary>
		/// Руды в астероиде 
		/// </summary>
		private readonly double startVolume;
		/// <summary>
		/// Текущее значение руды в астере
		/// </summary>
		private double currentVolume;
		/// <summary>
		/// Цикл лазера
		/// </summary>
		private readonly double cycle;
		/// <summary>
		/// Время в секундах до конца текущего цикла
		/// </summary>
		private double timeToCycleEnd;
		/// <summary>
		/// Время до окончаняи астероида в секундах
		/// </summary>
		private double timeToAsterEnd;

		/// <summary>
		/// Добыча руды за 1 секунду
		/// </summary>
		private double oreUnitPerSecond;

		/// <summary>
		/// стартовал ли первый лазер
		/// </summary>
		private bool laser1Started;
		/// <summary>
		/// стартовал ли второй лазер
		/// </summary>
		private bool laser2Started;
		/// <summary>
		/// стартовал ли третий лазер
		/// </summary>
		private bool laser3Started;

		/// <summary>
		/// Время до окончаняи астероида в секундах
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
		/// Время в секундах до конца текущего цикла
		/// </summary>
		public double TimeToCycleEnd
		{
			get
			{
				return timeToCycleEnd;
			}
		}

		/// <summary>
		/// Текущее значение руды в астере
		/// </summary>
		public double CurrentVolume
		{
			get { return currentVolume; }
		}

		/// <summary>
		/// Руды в астероиде 
		/// </summary>
		public double StartVolume
		{
			get { return startVolume; }
		}

		/// <summary>
		/// Сколько лазеров стартовало
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
		/// Цикл лазера
		/// </summary>
		public double Cycle
		{
			get { return cycle; }
		}

		/// <summary>
		/// стартовал ли первый лазер
		/// </summary>
		public bool Laser1Started
		{
			get { return laser1Started; }
			set { laser1Started = value; }
		}

		/// <summary>
		/// стартовал ли второй лазер
		/// </summary>
		public bool Laser2Started
		{
			get { return laser2Started; }
			set { laser2Started = value; }
		}

		/// <summary>
		/// стартовал ли третий лазер
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
		/// Обновить значение руды за цикл
		/// </summary>
		/// <param name="miningYield"></param>
		public void SetMiningYield(double  miningYield)
		{
			oreUnitPerSecond = miningYield / Cycle / ore.Volume;
			timeToAsterEnd = (int)(currentVolume / oreUnitPerSecond);
		}
		/// <summary>
		/// Тик
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
		/// сброс таймера в исходное состояние
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
