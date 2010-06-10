using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace EveMiner.Forms
{
	public partial class SlotDevice : UserControl
	{
		private Image _imageNone;
		[Category("SlotDevice")]
		[DisplayName("ImageNone")]
		[DefaultValue(null)]
		public Image ImageNone 
		{ 
			get { return _imageNone; }  
			set
			{
				_imageNone = value;
				pictureBoxDevice.Image = value;
			} 
		}

		public SlotDevice()
		{
			InitializeComponent();
			pictureBoxDevice.Image = ImageNone;
		}

		
	}
}
