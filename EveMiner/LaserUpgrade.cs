using System;
using System.Drawing;

namespace EveMiner
{
	[Serializable]
	public class LaserUpgrade
	{
		public string Name;
		public double OreYieldBonus;
		public double TimeBonus;
		public Image image;
		
		public LaserUpgrade(string name, double oreYieldBonus, double timeBonus, Image image)
		{
			this.image = image;
			Name = name;
			OreYieldBonus = oreYieldBonus;
			TimeBonus = timeBonus;
		}
		
		public override string ToString()
		{
			if(TimeBonus > 0)
				return string.Format("{0}" + Environment.NewLine + "Time Reduce Bonus {1}%", Name, TimeBonus);
			else
				return string.Format("{0}" + Environment.NewLine + "Minig Amount Bonus {1}%", Name, OreYieldBonus);
		}
	}
}
