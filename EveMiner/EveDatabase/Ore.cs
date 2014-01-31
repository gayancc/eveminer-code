namespace EveMiner.EveDatabase
{
	///<summary>
	/// Руда
	///</summary>
	public enum OreType
	{
		/// <summary>
		/// Велдспар
		/// </summary>
		Veldspar,
		/// <summary>
		/// Скордит
		/// </summary>
		Scordite,
		/// <summary>
		/// Пироксеерз
		/// </summary>
		Pyroxeres,
		/// <summary>
		/// Плагиоклаз
		/// </summary>
		Plagioclase,
		/// <summary>
		/// Омбер
		/// </summary>
		Omber,
		/// <summary>
		/// Кернит
		/// </summary>
		Kernite,
		/// <summary>
		/// Джаспет
		/// </summary>
		Jaspet,
		/// <summary>
		/// Хеморфит
		/// </summary>
		Hemorphite,
		/// <summary>
		/// Хедбергит
		/// </summary>
		Hedbergite,
		/// <summary>
		/// Гнайс
		/// </summary>
		Gneiss,
		/// <summary>
		/// Темная охра
		/// </summary>
		DarkOchre,
		/// <summary>
		/// Сподумайн
		/// </summary>
		Spodumain,
		/// <summary>
		/// Крокит
		/// </summary>
		Crokite,
		/// <summary>
		/// Бистот
		/// </summary>
		Bistot,
		/// <summary>
		/// Арконор
		/// </summary>
		Arkonor,
		/// <summary>
		/// Меркоксит
		/// </summary>
		Mercoxit
	}

	/// <summary>
	/// Тип руды
	/// </summary>
	public class Ore
	{
		/// <summary>
		/// EVE ID
		/// </summary>
		public readonly int Id;

		/// <summary>
		/// Имя руды
		/// </summary>
		public readonly string Name;

		public double Price { get; set; }

		/// <summary>
		/// Объем единицы руды
		/// </summary>
		public readonly double Volume;

		/// <summary>
		/// Число единиц руды для рефайна
		/// </summary>
		private readonly int _unitsToRefine;

		/// <summary>
		/// Выхлоп минеарлов на единицу руды
		/// </summary>
		public readonly MineralsOut MineralsOut;

		/// <summary>
		///  
		/// </summary>
		/// <param name="id"></param>
		/// <param name="name"></param>
		/// <param name="volume"></param>
		/// <param name="unitsToRefine"></param>
		/// <param name="mineralsOut"></param>
		public Ore(int id, string name, double volume, int unitsToRefine, MineralsOut mineralsOut)
		{
			Id = id;
			Name = name;
			Volume = volume;
			_unitsToRefine = unitsToRefine;
			MineralsOut = mineralsOut;
			
		}

		/// <summary>
		/// Число единиц руды для рефайна
		/// </summary>
		public int UnitsToRefine
		{
			get { return _unitsToRefine; }
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
		/// <summary>
		/// получить уровень процессинга для руды
		/// </summary>
		/// <param name="ore"></param>
		/// <returns></returns>
		public int GetProcessingSkill()
		{
			if (Name.Contains("Veldspar"))
				return Config<Settings>.Instance.Skills.VeldsparProcessing;
			if (Name.Contains("Scordite"))
				return Config<Settings>.Instance.Skills.ScorditeProcessing;
			if (Name.Contains("Pyroxeres"))
				return Config<Settings>.Instance.Skills.PyroxeresProcessing;
			if (Name.Contains("Plagioclase"))
				return Config<Settings>.Instance.Skills.PlagioclaseProcessing;
			if (Name.Contains("Omber"))
				return Config<Settings>.Instance.Skills.OmberProcessing;
			if (Name.Contains("Kernite"))
				return Config<Settings>.Instance.Skills.KerniteProcessing;
			if (Name.Contains("Jaspet"))
				return Config<Settings>.Instance.Skills.JaspetProcessing;
			if (Name.Contains("Hemorphite"))
				return Config<Settings>.Instance.Skills.HemorphiteProcessing;
			if (Name.Contains("Hedbergite"))
				return Config<Settings>.Instance.Skills.HedbergiteProcessing;
			if (Name.Contains("Gneiss"))
				return Config<Settings>.Instance.Skills.GneissProcessing;
			if (Name.Contains("Ochre"))
				return Config<Settings>.Instance.Skills.DarkOchreProcessing;
			if (Name.Contains("Bistot"))
				return Config<Settings>.Instance.Skills.BistotProcessing;
			if (Name.Contains("Spodumain"))
				return Config<Settings>.Instance.Skills.SpodumainProcessing;
			if (Name.Contains("Crokite"))
				return Config<Settings>.Instance.Skills.CrokiteProcessing;
			if (Name.Contains("Arkonor"))
				return Config<Settings>.Instance.Skills.ArkonorProcessing;
			if (Name.Contains("Mercoxit"))
				return Config<Settings>.Instance.Skills.MercoxitProcessing;
			return 0;
		}
		/// <summary>
		/// Gets the efficiency.
		/// </summary>
		/// <param name="ore">The ore.</param>
		/// <param name="netYield">The net yield.</param>
		/// <returns></returns>
		public double GetEfficiency(double netYield)
		{
			Skills skills = Config<Settings>.Instance.Skills;
			double eff = netYield +
						 0.375 * (1 + skills.Refining * 0.02) * (1 + skills.EfficiencyRefining * 0.04) *
						 (1 + GetProcessingSkill() * 0.05);
			if (eff > 1.0)
				eff = 1.0;
			return eff;
		}
		/// <summary>
		/// Вычилсить количество минералов которое выйдет с руды
		/// </summary>
		/// <param name="ore">тип руды</param>
		/// <param name="netYield">The net yield.</param>
		/// <param name="p">число циклов рефайна</param>
		/// <returns></returns>
		public MineralsOut GetMineralsOut(double netYield, int quantity)
		{
			int p = quantity/_unitsToRefine;


			double coeff = p * GetEfficiency(netYield) * (1 - Settings.GetTaxRate());
			int tritaniumOut = (int)(MineralsOut.Tritanium * coeff);
			int pyeriteOut = (int)(MineralsOut.Pyerite * coeff);
			int mexallonOut = (int)(MineralsOut.Mexallon * coeff);
			int isogenOut = (int)(MineralsOut.Isogen * coeff);
			int nocxiumOut = (int)(MineralsOut.Nocxium * coeff);
			int zydrineOut = (int)(MineralsOut.Zydrine * coeff);
			int megcyteOut = (int)(MineralsOut.Megacyte * coeff);
			int morphiteOut = (int)(MineralsOut.Morphite * coeff);
			return new MineralsOut(tritaniumOut, pyeriteOut, mexallonOut, isogenOut,
								   nocxiumOut, zydrineOut, megcyteOut, morphiteOut);
		}

	}
}