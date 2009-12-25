using System;
using System.Threading;
using EveMiner.Ores;

namespace EveMiner
{
	/// <summary>
	/// Элемент из списка таймеров
	/// </summary>
	internal class TimerListItem
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
		/// Если цикл начался а астера не хватит на весь цикл
		/// </summary>
		private bool isEmptyClose;

		/// <summary>
		/// Время до окончаняи астероида в секундах
		/// </summary>
		private double timeToAsterEnd;

		/// <summary>
		/// Добыча руды за 1 секунду
		/// </summary>
		private double oreUnitPerSecond;


		private readonly WorkingTurret[] _turrets = new WorkingTurret[3];


		/// <summary>
		/// Время до окончаняи астероида в секундах
		/// </summary>
		public double TimeToAsterEnd
		{
			get
			{
				if (LasersStarted > 0)
					return timeToAsterEnd/LasersStarted;

				return timeToAsterEnd;
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
				foreach (WorkingTurret turret in _turrets)
				{
					if (turret.IsStarted)
						ret++;
				}
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
		/// Если цикл начался а астера не хватит на весь цикл
		/// </summary>
		public bool IsEmptyClose
		{
			get { return isEmptyClose; }
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="TimerListItem"/> class.
		/// </summary>
		/// <param name="ore">The ore.</param>
		/// <param name="startVolume">The start volume.</param>
		/// <param name="cycle">The cycle.</param>
		/// <param name="miningYield">The mining yield.</param>
		public TimerListItem(Ore ore, double startVolume, double cycle, double miningYield)
		{
			this.ore = ore;
			this.startVolume = startVolume;
			currentVolume = StartVolume;
			this.cycle = cycle;

			oreUnitPerSecond = miningYield/cycle/ore.Volume;

			timeToAsterEnd = (int) (startVolume/oreUnitPerSecond);
			if (timeToAsterEnd < cycle)
				isEmptyClose = true;

			_turrets[0] = new WorkingTurret(cycle, 1.0, ProgressChanged, CycleEnded, "1");
			_turrets[1] = new WorkingTurret(cycle, 1.0, ProgressChanged, CycleEnded, "2");
			_turrets[2] = new WorkingTurret(cycle, 1.0, ProgressChanged, CycleEnded, "3");
            
		}
		/// <summary>
		/// Включить/выключить туррель
		/// </summary>
		/// <param name="nTurret">номер</param>
		/// <param name="bEnable">вкл/выкл</param>
		/// <param name="callbackProgressChanged">коллбэк для функции отображения прогресса цикла</param>
        public void EnableTurret(int nTurret, bool bEnable, TimerCallback callbackProgressChanged)
        {
            if (nTurret < 0 || nTurret > _turrets.Length)
                new ArgumentOutOfRangeException("nTurret");
            if(bEnable)
            {
				_turrets[nTurret].TimerProgressChangedCallback += callbackProgressChanged;
                _turrets[nTurret].Start();
            }
            else
            {
				_turrets[nTurret].TimerProgressChangedCallback -= callbackProgressChanged;
            	_turrets[nTurret].Stop();
            }
            isEmptyClose = timeToAsterEnd < cycle * LasersStarted;
        }
		/// <summary>
		/// проверка на включенное состояние туррели
		/// </summary>
		/// <param name="nTurret"></param>
		/// <returns></returns>
        public bool IsEnableTurret(int nTurret)
        {
        	return _turrets[nTurret].IsStarted;
        }
		/// <summary>
		/// Обновить значение руды за цикл
		/// </summary>
		/// <param name="miningYield"></param>
		public void SetMiningYield(double miningYield)
		{
			oreUnitPerSecond = miningYield/Cycle/ore.Volume;
			timeToAsterEnd = (int) (currentVolume/oreUnitPerSecond);
			isEmptyClose = timeToAsterEnd < cycle*LasersStarted;
		}


		/// <summary>
		/// Обработчик прогресса изменения таймера турели
		/// </summary>
		/// <param name="obj">сслка на <see cref="WorkingTurret"/></param>
		 public void ProgressChanged(Object obj)
		 {
			WorkingTurret turret = obj as WorkingTurret;
			 if(turret != null)
			 {
				currentVolume -= oreUnitPerSecond * turret.ProgressInterval;
				timeToAsterEnd -= turret.ProgressInterval;
				if (timeToAsterEnd <= 0)
				{
					currentVolume = 0;
					timeToAsterEnd = 0;
					Sound.PlaySound(Settings.AsterEndFileName);
				}

			 }
		 }
		 /// <summary>
		 /// Обработчик окончания цикла туррели
		 /// </summary>
		 /// <param name="obj">сслка на <see cref="WorkingTurret"/></param>
		 public void CycleEnded(Object obj)
		 {
			 WorkingTurret turret = obj as WorkingTurret;
			 if (turret != null)
			 {
				 isEmptyClose = timeToAsterEnd < cycle * LasersStarted;
			 }
		 }


		 /// <summary>
		 /// Stops the turrets.
		 /// </summary>
		public void StopTurrets()
		{
		 	foreach (WorkingTurret turret in _turrets)
		 	{
		 		turret.Stop();
		 	}
		}
	}
}