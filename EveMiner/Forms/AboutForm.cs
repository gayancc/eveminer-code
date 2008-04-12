using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace EveMiner.Forms
{
	public partial class AboutForm : Form
	{
		public AboutForm()
		{
			InitializeComponent();
			labelVersion.Text = string.Format("Eve Miner Version {0} beta", Assembly.GetExecutingAssembly().GetName().Version);
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("mailto:" + linkLabel1.Text);
		}
	}
}