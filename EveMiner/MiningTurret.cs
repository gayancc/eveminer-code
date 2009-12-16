namespace EveMiner
{
	/// <summary>
	/// Майнерский лазер
	/// </summary>
	public class MiningTurret
	{
		/// <summary>
		/// Имя туррели
		/// </summary>
		public string Name;

		/// <summary>
		/// Время цикла
		/// </summary>
		public int CycleTime;

		/// <summary>
		/// Получение руды
		/// </summary>
		public double MiningAmount;

		/// <summary>
		/// Использование кристаллов true/false
		/// </summary>
		public bool UseCrystals;


		/// <summary>
		/// Initializes a new instance of the <see cref="MiningTurret"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="cycleTime">The cycle time.</param>
		/// <param name="miningAmount">The mining amount.</param>
		/// <param name="useCrystals">if set to <c>true</c> [use crystals].</param>
		public MiningTurret(string name, int cycleTime, double miningAmount, bool useCrystals)
		{
			Name = name;
			CycleTime = cycleTime;
			MiningAmount = miningAmount;
			UseCrystals = useCrystals;
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </returns>
		public override string ToString()
		{
			return Name;
		}
	}
}