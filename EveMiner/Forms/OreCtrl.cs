using System;
using System.ComponentModel;
using System.Windows.Forms;
using EveMiner.Ores;

namespace EveMiner.Forms
{
	public partial class OreCtrl : UserControl
	{
		/// <summary>
		/// Тип руды
		/// </summary>
		private OreType  oreType;

		/// <summary>
		/// Время до выработки
		/// </summary>
		private DateTime timeElapsed;
		/// <summary>
		/// Объем копки в минуту
		/// </summary>
		private double volumePerMinute;

		public OreCtrl()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Тип руды
		/// </summary>
		[DisplayName("Ore type")]
		[Category("Ore Control")]
		[DefaultValue(OreType.Veldspar)]
		public OreType OreType
		{
			get { return oreType; }
			set { oreType = value; }
		}

		[DisplayName("Ore type")]
		[Category("Ore Control")]
		[DefaultValue(1000.0)]
		/// <summary>
		/// Объем копки в минуту
		/// </summary>
		public double VolumePerMinute
		{
			get { return volumePerMinute; }
			set { volumePerMinute = value; }
		}
	}
}
