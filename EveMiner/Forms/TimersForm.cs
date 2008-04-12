using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EveMiner.Forms
{
	public partial class TimersForm : Form
	{
		public TimersForm()
		{
			InitializeComponent();
		}

		private void TimersForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			this.Hide();
		}
	}
}