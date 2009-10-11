using System;
using System.Security.Permissions;
using System.Windows.Forms;
using EveMiner.Forms;

namespace EveMiner
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{

			using (SingleProgramInstance spi = new SingleProgramInstance("x5k6yz"))
			{
				if(spi.IsSingleInstance)
				{
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					MainForm mainForm = new MainForm();
					Application.AddMessageFilter(new MyMessageFilter(mainForm));
					Application.Run(mainForm);
					//Application.Run(new OreForm());
					Config<Settings>.Save();
				}
				else
				{
					spi.RaiseOtherProcess();
				}
			}
		}
	}
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class MyMessageFilter : IMessageFilter
	{
		readonly Form _mMainForm;
		public MyMessageFilter(Form form)
		{
			_mMainForm = form;
		}

		public bool PreFilterMessage(ref Message m)
		{
			if (m.Msg == SingleProgramInstance.WakeupMessage)
			{
				_mMainForm.WindowState = FormWindowState.Normal;
				_mMainForm.Visible = true;
				return true;
			}
			return false;
		}
	}
}