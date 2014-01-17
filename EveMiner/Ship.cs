namespace EveMiner
{
	public class Ship
	{
		public readonly string Name;
		public double LowSlots;
		public double TurretSlots;

		public Ship(string name, int lowSlots, int turretSlots)
		{
			Name = name;
			LowSlots = lowSlots;
			TurretSlots = turretSlots;
		}

		public ShipType Type
		{
			get
			{
				switch (Name)
				{
					case "Venture":
						return ShipType.MiningFrigate;
					case "Procurer":
						return ShipType.Barge;
					case "Retriever":
						return ShipType.Barge;
					case "Covetor":
						return ShipType.Barge;
					case "Skiff":
						return ShipType.Exhumer;
					case "Mackinaw":
						return ShipType.Exhumer;
					case "Hulk":
						return ShipType.Exhumer;
				}
					
				return ShipType.Common;
			}
		}

		public override string ToString()
		{
			return Name;
		}

		public double YieldBonus()
		{
			var bonus = 1.0; 
			Skills skills = Config<Settings>.Instance.Skills;
			switch(Name)
			{
				case "Venture":
					bonus *= (1 + skills.MiningFrigates * 0.05) * 2;
					break;
				case "Procurer":
					bonus *= 3;//(1 + skills.MiningBarge * 0.03);
					break;
				case "Retriver":
					bonus *= 1.5;//(1 + skills.MiningBarge * 0.03);
					break;
				case "Covetor":
					bonus *= (1 + skills.MiningBarge * 0.04);
					break;
				case "Skiff":
					bonus *= (1 + skills.Exhumers * 0.01) * 3;
					break;
				case "Mackinaw":
					bonus *= (1 + skills.Exhumers * 0.01)*1.5;
					break;
				case "Hulk":
					bonus *= (1 + skills.MiningBarge * 0.03) * (1 + skills.Exhumers * 0.03);
					break;
			}
			return bonus;
		}
		public double IceHarvestTimeBonus()
		{
			var bonus = 1.0;
			Skills skills = Config<Settings>.Instance.Skills;
			switch (Name)
			{
				case "Procurer":
					bonus *= (1 - 0.6666);
					break;
				case "Retriver":
					bonus *= (1 - 0.3333);
					break;
				case "Covetor":
					bonus *= (1 - skills.MiningBarge * 0.03);
					break;
				case "Skiff":
					bonus *= (1 - skills.Exhumers * 0.01) * (1 - 0.6666);
					break;
				case "Mackinaw":
					bonus *= (1 - skills.Exhumers * 0.01) * (1 - 0.3333);
					break;
				case "Hulk":
					bonus *= (1 - skills.Exhumers * 0.04);
					break;
			}
			return bonus;
		}

	}
}