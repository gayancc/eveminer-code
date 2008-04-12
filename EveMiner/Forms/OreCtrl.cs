using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace EveMiner.Forms
{
	public partial class OreCtrl : UserControl
	{
		/// <summary>
		/// Тип руды
		/// </summary>
		private Ore  ore;

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
		[DefaultValue(Ore.Veldspar)]
		public Ore Ore
		{
			get { return ore; }
			set { ore = value; }
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
