using System;

namespace EveMiner
{
	/// <summary>
	/// Skills
	/// </summary>
	[Serializable]
	public class Skills
	{
		private int mining;
		private int astrogeology;
		private int miningBarge;
		private int exhumers;
		private int miningForeman;
		private int miningDirector;
		private int iceHarvesting;
		private int warfareLinkSpec;
		private int frigates;
		private int cruisers;
		private int veldsparProcessing;
		private int scorditeProcessing;
		private int pyroxeresProcessing;
		private int plagioclaseProcessing;
		private int omberProcessing;
		private int kerniteProcessing;
		private int jaspetProcessing;
		private int hemorphiteProcessing;
		private int hedbergiteProcessing;
		private int gneissProcessing;
		private int darkOchreProcessing;
		private int spodumainProcessing;
		private int crokiteProcessing;
		private int bistotProcessing;
		private int arkonorProcessing;
		private int mercoxitProcessing;
		private int refining;
		private int efficiencyRefining;
		private int iceProcessing;

		/// <summary>
		/// Gets or sets the mining.
		/// </summary>
		/// <value>The mining.</value>
		public int Mining
		{
			get { return mining; }
			set { mining = CheckRange(value); }
		}

		/// <summary>
		/// Gets or sets the astrogeology.
		/// </summary>
		/// <value>The astrogeology.</value>
		public int Astrogeology
		{
			get { return astrogeology; }
			set { astrogeology = CheckRange(value); }
		}

		/// <summary>
		/// Gets or sets the mining barge.
		/// </summary>
		/// <value>The mining barge.</value>
		public int MiningBarge
		{
			get { return miningBarge; }
			set { miningBarge = CheckRange(value); }
		}

		/// <summary>
		/// Gets or sets the exhumers.
		/// </summary>
		/// <value>The exhumers.</value>
		public int Exhumers
		{
			get { return exhumers; }
			set { exhumers = CheckRange(value); }
		}

		/// <summary>
		/// Gets or sets the mining foreman.
		/// </summary>
		/// <value>The mining foreman.</value>
		public int MiningForeman
		{
			get { return miningForeman; }
			set { miningForeman = CheckRange(value); }
		}

		/// <summary>
		/// Gets or sets the mining director.
		/// </summary>
		/// <value>The mining director.</value>
		public int MiningDirector
		{
			get { return miningDirector; }
			set { miningDirector = CheckRange(value); }
		}

		/// <summary>
		/// Gets or sets the ice harvesting.
		/// </summary>
		/// <value>The ice harvesting.</value>
		public int IceHarvesting
		{
			get { return iceHarvesting; }
			set { iceHarvesting = value; }
		}

		/// <summary>
		/// Gets or sets the warfare link spec.
		/// </summary>
		/// <value>The warfare link spec.</value>
		public int WarfareLinkSpec
		{
			get { return warfareLinkSpec; }
			set { warfareLinkSpec = value; }
		}

		/// <summary>
		/// Gets or sets the frigates.
		/// </summary>
		/// <value>The frigates.</value>
		public int Frigates
		{
			get { return frigates; }
			set { frigates = value; }
		}

		/// <summary>
		/// Gets or sets the cruisers.
		/// </summary>
		/// <value>The cruisers.</value>
		public int Cruisers
		{
			get { return cruisers; }
			set { cruisers = value; }
		}

		/// <summary>
		/// Gets or sets the veldspar processing.
		/// </summary>
		/// <value>The veldspar processing.</value>
		public int VeldsparProcessing
		{
			get { return veldsparProcessing; }
			set { veldsparProcessing = value; }
		}

		/// <summary>
		/// Gets or sets the scordite processing.
		/// </summary>
		/// <value>The scordite processing.</value>
		public int ScorditeProcessing
		{
			get { return scorditeProcessing; }
			set { scorditeProcessing = value; }
		}

		/// <summary>
		/// Gets or sets the pyroxeres processing.
		/// </summary>
		/// <value>The pyroxeres processing.</value>
		public int PyroxeresProcessing
		{
			get { return pyroxeresProcessing; }
			set { pyroxeresProcessing = value; }
		}

		/// <summary>
		/// Gets or sets the plagioclase processing.
		/// </summary>
		/// <value>The plagioclase processing.</value>
		public int PlagioclaseProcessing
		{
			get { return plagioclaseProcessing; }
			set { plagioclaseProcessing = value; }
		}

		/// <summary>
		/// Gets or sets the omber processing.
		/// </summary>
		/// <value>The omber processing.</value>
		public int OmberProcessing
		{
			get { return omberProcessing; }
			set { omberProcessing = value; }
		}

		/// <summary>
		/// Gets or sets the kernite processing.
		/// </summary>
		/// <value>The kernite processing.</value>
		public int KerniteProcessing
		{
			get { return kerniteProcessing; }
			set { kerniteProcessing = value; }
		}

		/// <summary>
		/// Gets or sets the jaspet processing.
		/// </summary>
		/// <value>The jaspet processing.</value>
		public int JaspetProcessing
		{
			get { return jaspetProcessing; }
			set { jaspetProcessing = value; }
		}

		/// <summary>
		/// Gets or sets the hemorphite processing.
		/// </summary>
		/// <value>The hemorphite processing.</value>
		public int HemorphiteProcessing
		{
			get { return hemorphiteProcessing; }
			set { hemorphiteProcessing = value; }
		}

		/// <summary>
		/// Gets or sets the hedbergite processing.
		/// </summary>
		/// <value>The hedbergite processing.</value>
		public int HedbergiteProcessing
		{
			get { return hedbergiteProcessing; }
			set { hedbergiteProcessing = value; }
		}

		/// <summary>
		/// Gets or sets the gneiss processing.
		/// </summary>
		/// <value>The gneiss processing.</value>
		public int GneissProcessing
		{
			get { return gneissProcessing; }
			set { gneissProcessing = value; }
		}

		/// <summary>
		/// Gets or sets the dark ochre processing.
		/// </summary>
		/// <value>The dark ochre processing.</value>
		public int DarkOchreProcessing
		{
			get { return darkOchreProcessing; }
			set { darkOchreProcessing = value; }
		}

		/// <summary>
		/// Gets or sets the spodumain processing.
		/// </summary>
		/// <value>The spodumain processing.</value>
		public int SpodumainProcessing
		{
			get { return spodumainProcessing; }
			set { spodumainProcessing = value; }
		}

		/// <summary>
		/// Gets or sets the crokite processing.
		/// </summary>
		/// <value>The crokite processing.</value>
		public int CrokiteProcessing
		{
			get { return crokiteProcessing; }
			set { crokiteProcessing = value; }
		}

		/// <summary>
		/// Gets or sets the bistot processing.
		/// </summary>
		/// <value>The bistot processing.</value>
		public int BistotProcessing
		{
			get { return bistotProcessing; }
			set { bistotProcessing = value; }
		}

		/// <summary>
		/// Gets or sets the arkonor processing.
		/// </summary>
		/// <value>The arkonor processing.</value>
		public int ArkonorProcessing
		{
			get { return arkonorProcessing; }
			set { arkonorProcessing = value; }
		}

		/// <summary>
		/// Gets or sets the mercoxit processing.
		/// </summary>
		/// <value>The mercoxit processing.</value>
		public int MercoxitProcessing
		{
			get { return mercoxitProcessing; }
			set { mercoxitProcessing = value; }
		}

		/// <summary>
		/// Gets or sets the refining.
		/// </summary>
		/// <value>The refining.</value>
		public int Refining
		{
			get { return refining; }
			set { refining = value; }
		}

		/// <summary>
		/// Gets or sets the efficiency refining.
		/// </summary>
		/// <value>The efficiency refining.</value>
		public int EfficiencyRefining
		{
			get { return efficiencyRefining; }
			set { efficiencyRefining = value; }
		}

		/// <summary>
		/// Gets or sets the ice processing.
		/// </summary>
		/// <value>The ice processing.</value>
		public int IceProcessing
		{
			get { return iceProcessing; }
			set { iceProcessing = value; }
		}

		/// <summary>
		/// привести к допустимому значению
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		private static int CheckRange(int value)
		{
			if (value < 0)
				return 0;
			if (value > 5)
				return 5;
			return value;
		}
	}
}
