using System;

namespace EveMiner
{
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

		public int Mining
		{
			get { return mining; }
			set { mining = CheckRange(value); }
		}

		public int Astrogeology
		{
			get { return astrogeology; }
			set { astrogeology = CheckRange(value); }
		}

		public int MiningBarge
		{
			get { return miningBarge; }
			set { miningBarge = CheckRange(value); }
		}

		public int Exhumers
		{
			get { return exhumers; }
			set { exhumers = CheckRange(value); }
		}

		public int MiningForeman
		{
			get { return miningForeman; }
			set { miningForeman = CheckRange(value); }
		}

		public int MiningDirector
		{
			get { return miningDirector; }
			set { miningDirector = CheckRange(value); }
		}

		public int IceHarvesting
		{
			get { return iceHarvesting; }
			set { iceHarvesting = value; }
		}

		public int WarfareLinkSpec
		{
			get { return warfareLinkSpec; }
			set { warfareLinkSpec = value; }
		}

		public int Frigates
		{
			get { return frigates; }
			set { frigates = value; }
		}

		public int Cruisers
		{
			get { return cruisers; }
			set { cruisers = value; }
		}

		public int VeldsparProcessing
		{
			get { return veldsparProcessing; }
			set { veldsparProcessing = value; }
		}

		public int ScorditeProcessing
		{
			get { return scorditeProcessing; }
			set { scorditeProcessing = value; }
		}

		public int PyroxeresProcessing
		{
			get { return pyroxeresProcessing; }
			set { pyroxeresProcessing = value; }
		}

		public int PlagioclaseProcessing
		{
			get { return plagioclaseProcessing; }
			set { plagioclaseProcessing = value; }
		}

		public int OmberProcessing
		{
			get { return omberProcessing; }
			set { omberProcessing = value; }
		}

		public int KerniteProcessing
		{
			get { return kerniteProcessing; }
			set { kerniteProcessing = value; }
		}

		public int JaspetProcessing
		{
			get { return jaspetProcessing; }
			set { jaspetProcessing = value; }
		}

		public int HemorphiteProcessing
		{
			get { return hemorphiteProcessing; }
			set { hemorphiteProcessing = value; }
		}

		public int HedbergiteProcessing
		{
			get { return hedbergiteProcessing; }
			set { hedbergiteProcessing = value; }
		}

		public int GneissProcessing
		{
			get { return gneissProcessing; }
			set { gneissProcessing = value; }
		}

		public int DarkOchreProcessing
		{
			get { return darkOchreProcessing; }
			set { darkOchreProcessing = value; }
		}

		public int SpodumainProcessing
		{
			get { return spodumainProcessing; }
			set { spodumainProcessing = value; }
		}

		public int CrokiteProcessing
		{
			get { return crokiteProcessing; }
			set { crokiteProcessing = value; }
		}

		public int BistotProcessing
		{
			get { return bistotProcessing; }
			set { bistotProcessing = value; }
		}

		public int ArkonorProcessing
		{
			get { return arkonorProcessing; }
			set { arkonorProcessing = value; }
		}

		public int MercoxitProcessing
		{
			get { return mercoxitProcessing; }
			set { mercoxitProcessing = value; }
		}

		public int Refining
		{
			get { return refining; }
			set { refining = value; }
		}

		public int EfficiencyRefining
		{
			get { return efficiencyRefining; }
			set { efficiencyRefining = value; }
		}

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
