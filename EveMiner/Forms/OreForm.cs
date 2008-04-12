using System.Windows.Forms;

namespace EveMiner.Forms
{
	public partial class OreForm : Form
	{
		private bool isMouseDown;
		private int x;
		private int y;

		public OreForm()
		{
			InitializeComponent();
		}

		private void OreForm_MouseDown(object sender, MouseEventArgs e)
		{
			x = e.X;

			y = e.Y;

//			left = this.Left;
//
//			top = this.Top;

			isMouseDown = true;

			Cursor = Cursors.SizeAll;
		}

		private void OreForm_MouseMove(object sender, MouseEventArgs e)
		{
			if (isMouseDown)
			{
				Left = Left + (e.X - x);

				Top = Top + (e.Y - y);

				//Refresh the parent to ensure that whatever is behind the control gets painted 
				//before we need to do our own graphics output.
				if(Parent != null)
					Parent.Refresh();
			}
		}

		private void OreForm_MouseUp(object sender, MouseEventArgs e)
		{
			isMouseDown = false;

			Cursor = Cursors.Default;

			Refresh();
		}
	}
}