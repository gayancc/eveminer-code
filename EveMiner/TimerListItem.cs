using System;
using EveMiner.Ores;

namespace EveMiner
{
	/// <summary>
	/// ������� �� ������ ��������
	/// </summary>
	internal class TimerListItem
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
		/// ���� ���� ������� � ������ �� ������ �� ���� ����
		/// </summary>
		private bool isEmptyClose;

		/// <summary>
		/// ����� �� ��������� ��������� � ��������
		/// </summary>
		private double timeToAsterEnd;

		/// <summary>
		/// ������ ���� �� 1 �������
		/// </summary>
		private double oreUnitPerSecond;


		private readonly WorkingTurret turret1;
		private readonly WorkingTurret turret2;
		private readonly WorkingTurret turret3;


		/// <summary>
		/// ����� �� ��������� ��������� � ��������
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
				if (Turret1.IsStarted)
					ret++;
				if (Turret2.IsStarted)
					ret++;
				if (Turret3.IsStarted)
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
			get { return Turret1.IsStarted; }
			set
			{
				if (value)
					Turret1.Start();
				else
					Turret1.Stop();

				isEmptyClose = timeToAsterEnd < cycle * LasersStarted;
			}
		}

		/// <summary>
		/// ��������� �� ������ �����
		/// </summary>
		public bool Laser2Started
		{
			get { return Turret2.IsStarted; }
			set
			{
				
				if (value)
					Turret2.Start();
				else
					Turret2.Stop();

				isEmptyClose = timeToAsterEnd < cycle * LasersStarted;
				
			}
		}

		/// <summary>
		/// ��������� �� ������ �����
		/// </summary>
		public bool Laser3Started
		{
			get { return Turret3.IsStarted; }
			set
			{
				if (value)
					Turret3.Start();
				else
					Turret3.Stop();
				
				isEmptyClose = timeToAsterEnd < cycle*LasersStarted;
			}
		}

		/// <summary>
		/// ���� ���� ������� � ������ �� ������ �� ���� ����
		/// </summary>
		public bool IsEmptyClose
		{
			get { return isEmptyClose; }
		}

		public WorkingTurret Turret1
		{
			get { return turret1; }
		}

		public WorkingTurret Turret2
		{
			get { return turret2; }
		}

		public WorkingTurret Turret3
		{
			get { return turret3; }
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

			turret1 = new WorkingTurret(cycle, 1.0, ProgressChanged, CycleEnded);
			turret2 = new WorkingTurret(cycle, 1.0, ProgressChanged, CycleEnded);
			turret3 = new WorkingTurret(cycle, 1.0, ProgressChanged, CycleEnded);
		}

		/// <summary>
		/// �������� �������� ���� �� ����
		/// </summary>
		/// <param name="miningYield"></param>
		public void SetMiningYield(double miningYield)
		{
			oreUnitPerSecond = miningYield/Cycle/ore.Volume;
			timeToAsterEnd = (int) (currentVolume/oreUnitPerSecond);
			isEmptyClose = timeToAsterEnd < cycle*LasersStarted;
		}


		/// <summary>
		/// ���������� ��������� ��������� ������� ������
		/// </summary>
		/// <param name="obj">����� �� <see cref="WorkingTurret"/></param>
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
					StopTurrets();
				}

			 }
		 }
		 /// <summary>
		 /// ���������� ��������� ����� �������
		 /// </summary>
		 /// <param name="obj">����� �� <see cref="WorkingTurret"/></param>
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
			Turret1.Stop();
			Turret2.Stop();
			Turret3.Stop();
		}
	}
}