namespace EveMiner
{
	public class Ship
	{
		public string Name;
		public double MiningBonusPerLevel;
		public double TimeBonusPerLevel;
		public bool Barge;
		public bool Exhumer;


		public Ship(string name, double miningBonusPerLevel, double timeBonusPerLevel, bool barge, bool exhumer)
		{
			Name = name;
			MiningBonusPerLevel = miningBonusPerLevel;
			TimeBonusPerLevel = timeBonusPerLevel;
			Barge = barge;
			Exhumer = exhumer;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
