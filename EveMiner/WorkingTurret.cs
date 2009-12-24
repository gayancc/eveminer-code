using System;
using System.Threading;

namespace EveMiner
{
	/// <summary>
	/// Класс работающей туррели
	/// </summary>
	public	class WorkingTurret
	{
		private double _workingCycle;
		private Timer _cycleTimer;
		private Timer _progressTimer;
		private double _progressInterval;
		/// <summary>
		/// Время в секундах до конца текущего цикла
		/// </summary>
		private double _timeToCycleEnd;


		private readonly TimerCallback _timerCycleEndedCallback;
		/// <summary>
		/// Имя туррели
		/// </summary>
		public readonly string Name;

		/// <summary>
		/// Initializes a new instance of the <see cref="WorkingTurret"/> class.
		/// </summary>
		/// <param name="workingCycle">The working cycle.</param>
		/// <param name="progressInterval">The progress interval.</param>
		/// <param name="timerProgressChanged">The timer progress changed.</param>
		/// <param name="timerCycleEnded">The timer cycle ended.</param>
		public WorkingTurret(double workingCycle, double progressInterval, TimerCallback timerProgressChanged, TimerCallback timerCycleEnded, string name)
		{
			_workingCycle = workingCycle;
			Name = name;
			_progressInterval += progressInterval;
			_timerCycleEndedCallback = CycleEnded;
			TimerProgressChangedCallback = ProgressChanged;
			TimerProgressChangedCallback = TimerProgressChangedCallback + timerProgressChanged;
			_timerCycleEndedCallback += timerCycleEnded;
		}
		/// <summary>
		/// Starts this instance.
		/// </summary>
		public void Start()
		{
			if (IsStarted)
				Stop();
			_timeToCycleEnd = _workingCycle;
			_cycleTimer = new Timer(_timerCycleEndedCallback, this, Convert.ToInt32(WorkingCycle * 1000.0), Convert.ToInt32(WorkingCycle * 1000.0));
			_progressTimer = new Timer(TimerProgressChangedCallback, this, Convert.ToInt32(ProgressInterval * 1000.0), Convert.ToInt32(ProgressInterval * 1000.0));
		}
		/// <summary>
		/// Stops this instance.
		/// </summary>
		public void Stop()
		{
			if(_cycleTimer!= null)
				_cycleTimer.Dispose();
			if(_progressTimer != null)
				_progressTimer.Dispose();
			_cycleTimer = null;
			_progressTimer = null;
		}
		/// <summary>
		/// Gets or sets the working cycle.
		/// </summary>
		/// <value>The working cycle.</value>
		public double WorkingCycle
		{
			get { return _workingCycle; }
			set { _workingCycle = value; }
		}

		/// <summary>
		/// Gets or sets the progress interval.
		/// </summary>
		/// <value>The progress interval.</value>
		public double ProgressInterval
		{
			get { return _progressInterval; }
			set { _progressInterval = value; }
		}
		/// <summary>
		/// Работает ли туррель
		/// </summary>
		public bool IsStarted
		{
			get
			{
				return _cycleTimer != null;
			}
		}

		/// <summary>
		/// Время в секундах до конца текущего цикла
		/// </summary>
		public double TimeToCycleEnd
		{
			get { return _timeToCycleEnd; }
		}

		/// <summary>
		/// Callback для изменения прогресса таймера
		/// </summary>
		public TimerCallback TimerProgressChangedCallback { get; set; }

		/// <summary>
		/// Обработчик прогресса изменения таймера турели
		/// </summary>
		/// <param name="obj">сслка на <see cref="WorkingTurret"/></param>
		public void ProgressChanged(Object obj)
		{
			_timeToCycleEnd -= ProgressInterval;
		}

		/// <summary>
		/// Обработчик окончания цикла туррели
		/// </summary>
		/// <param name="obj">ссылка на <see cref="WorkingTurret"/></param>
		private static void CycleEnded(Object obj)
		{
			Sound.PlaySound(Settings.CycleEndFileName);
		}
	}
}
