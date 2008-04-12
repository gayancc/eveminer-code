using System;
using System.Collections.Generic;
using System.Text;

namespace EveMiner
{
	public class MiningTurret
	{
		public string Name;

		public int CycleTime;

		public double MiningAmount;

		public bool UseCrystals;


		public MiningTurret(string name, int cycleTime, double miningAmount, bool useCrystals)
		{
			Name = name;
			CycleTime = cycleTime;
			MiningAmount = miningAmount;
			UseCrystals = useCrystals;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
