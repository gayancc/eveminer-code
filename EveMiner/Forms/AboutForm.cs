using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace EveMiner.Forms
{
	/// <summary>
	/// About Form
	/// </summary>
	public partial class AboutForm : Form
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AboutForm"/> class.
		/// </summary>
		public AboutForm()
		{
			InitializeComponent();
			labelVersion.Text = string.Format("Eve Miner Version {0} beta", Assembly.GetExecutingAssembly().GetName().Version);
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				Process.Start("mailto:" + linkLabel1.Text);
			}
				//если нет почтового клиента
			catch(Win32Exception)
			{}
		}
	}
}