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
		/// ��� ����
		/// </summary>
		private Ore  ore;

		/// <summary>
		/// ����� �� ���������
		/// </summary>
		private DateTime timeElapsed;
		/// <summary>
		/// ����� ����� � ������
		/// </summary>
		private double volumePerMinute;

		public OreCtrl()
		{
			InitializeComponent();
		}

		/// <summary>
		/// ��� ����
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
		/// ����� ����� � ������
		/// </summary>
		public double VolumePerMinute
		{
			get { return volumePerMinute; }
			set { volumePerMinute = value; }
		}
	}
}
