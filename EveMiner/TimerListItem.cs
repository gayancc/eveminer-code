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
		/// Стартовали ли таймер
		/// </summary>
		private bool timerStarted;

		/// <summary>
		/// Время до окончаняи астероида в секундах
		/// </summary>
		public double TimeToAsterEnd
		{
			get { return timeToAsterEnd; }
		}

		/// <summary>
		/// Время в секундах до конца текущего цикла
		/// </summary>
		public double TimeToCycleEnd
		{
			get { return timeToCycleEnd; }
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
		/// Стартовали ли таймер
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
		/// Обновить значение руды за цикл
		/// </summary>
		/// <param name="miningYield"></param>
		public void SetMiningYield(double  miningYield)
		{
			oreUnitPerSecond = miningYield / cycle / ore.Volume;
			timeToAsterEnd = (int)(currentVolume / oreUnitPerSecond);
		}
		/// <summary>
		/// Тик
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
		/// сброс таймера в исходное состояние
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
