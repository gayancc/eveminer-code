using System;
using System.Drawing;

namespace EveMiner
{
	/// <summary>
	/// Laser upgrade
	/// </summary>
	[Serializable]
	public class LaserUpgrade
	{
		/// <summary>
		/// Name
		/// </summary>
		public string Name;

		/// <summary>
		/// Ore Yield bonus
		/// </summary>
		public double OreYieldBonus;

		/// <summary>
		/// Time bonus
		/// </summary>
		public double TimeBonus;

		/// <summary>
		/// Image
		/// </summary>
		public Image Image;

		/// <summary>
		/// Initializes a new instance of the <see cref="LaserUpgrade"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="oreYieldBonus">The ore yield bonus.</param>
		/// <param name="timeBonus">The time bonus.</param>
		/// <param name="image">The Image.</param>
		public LaserUpgrade(string name, double oreYieldBonus, double timeBonus, Image image)
		{
			Image = image;
			Name = name;
			OreYieldBonus = oreYieldBonus;
			TimeBonus = timeBonus;
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </returns>
		public override string ToString()
		{
			if (TimeBonus > 0)
				return string.Format("{0}" + Environment.NewLine + "Time Reduce Bonus {1}%", Name, TimeBonus);
			return string.Format("{0}" + Environment.NewLine + "Minig Amount Bonus {1}%", Name, OreYieldBonus);
		}
	}
}