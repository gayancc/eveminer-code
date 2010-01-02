using System;
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


		private readonly WorkingTurret _turret;
		/// <summary>
		/// Сколько лазеров стартовало
		/// </summary>
		private int _lasersCount = 1;


		/// <summary>
		/// Время до окончания астероида в секундах
		/// </summary>
		public double TimeToAsterEnd
		{
			get
			{
				return timeToAsterEnd/LasersCount;
			}
		}
		/// <summary>
		/// Время до окончания цикла
		/// </summary>
		public double TimeToCycleEnd
		{
			get
			{
				return _turret.TimeToCycleEnd;
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
		public int LasersCount
		{
			get
			{
				return _lasersCount;
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
		/// Gets a value indicating whether this instance is satrted.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is satrted; otherwise, <c>false</c>.
		/// </value>
		public bool IsSatrted
		{
			get { return _turret.IsStarted; }
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

			timeToAsterEnd = startVolume/oreUnitPerSecond;
			
			if (timeToAsterEnd < cycle)
				isEmptyClose = true;

			_turret = new WorkingTurret(cycle, 1.0, ProgressChanged, CycleEnded, "1");
            
		}
		/// <summary>
		/// Включить/выключить туррель
		/// </summary>
		/// <param name="bEnable">вкл/выкл</param>
		/// <param name="nCount">Число туррелей на астер</param>
		public void EnableTurret(bool bEnable, int nCount)
        {
            if (nCount < 1 || nCount > 8)
                new ArgumentOutOfRangeException("nCount");
			_lasersCount = nCount;
            if(bEnable)
            {
                _turret.Start();
            }
            else
            {
            	_turret.Stop();
            }
            isEmptyClose = timeToAsterEnd < cycle * LasersCount;
        }
		/// <summary>
		/// проверка на включенное состояние туррели
		/// </summary>
		/// <returns></returns>
        public bool IsEnableTurret()
        {
        	return _turret.IsStarted;
        }
		/// <summary>
		/// Обновить значение руды за цикл
		/// </summary>
		/// <param name="miningYield"></param>
		public void SetMiningYield(double miningYield)
		{
			oreUnitPerSecond = miningYield/Cycle/ore.Volume;
			timeToAsterEnd = (int) (currentVolume/oreUnitPerSecond);
			isEmptyClose = timeToAsterEnd < cycle*LasersCount;
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
				currentVolume -= oreUnitPerSecond * turret.ProgressInterval * LasersCount;
				timeToAsterEnd -= turret.ProgressInterval * LasersCount;
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
				 isEmptyClose = timeToAsterEnd < cycle * LasersCount;
			 }
		 }


		 /// <summary>
		 /// Stops the turrets.
		 /// </summary>
		public void StopTurrets()
		{
		 		_turret.Stop();
		}
	}
}