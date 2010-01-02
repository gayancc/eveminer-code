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


		private readonly WorkingTurret _turret;
		/// <summary>
		/// ������� ������� ����������
		/// </summary>
		private int _lasersCount = 1;


		/// <summary>
		/// ����� �� ��������� ��������� � ��������
		/// </summary>
		public double TimeToAsterEnd
		{
			get
			{
				return timeToAsterEnd/LasersCount;
			}
		}
		/// <summary>
		/// ����� �� ��������� �����
		/// </summary>
		public double TimeToCycleEnd
		{
			get
			{
				return _turret.TimeToCycleEnd;
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
		public int LasersCount
		{
			get
			{
				return _lasersCount;
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
		/// ���� ���� ������� � ������ �� ������ �� ���� ����
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
		/// ��������/��������� �������
		/// </summary>
		/// <param name="bEnable">���/����</param>
		/// <param name="nCount">����� �������� �� �����</param>
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
		/// �������� �� ���������� ��������� �������
		/// </summary>
		/// <returns></returns>
        public bool IsEnableTurret()
        {
        	return _turret.IsStarted;
        }
		/// <summary>
		/// �������� �������� ���� �� ����
		/// </summary>
		/// <param name="miningYield"></param>
		public void SetMiningYield(double miningYield)
		{
			oreUnitPerSecond = miningYield/Cycle/ore.Volume;
			timeToAsterEnd = (int) (currentVolume/oreUnitPerSecond);
			isEmptyClose = timeToAsterEnd < cycle*LasersCount;
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
		 /// ���������� ��������� ����� �������
		 /// </summary>
		 /// <param name="obj">����� �� <see cref="WorkingTurret"/></param>
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