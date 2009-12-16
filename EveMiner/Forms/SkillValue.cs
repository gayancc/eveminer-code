using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using EveMiner.Properties;

namespace EveMiner.Forms
{
	/// <summary>
	/// Контрол для отображения скила
	/// </summary>
	public partial class SkillValue : UserControl
	{
		/// <summary>
		/// Occurs when [value change].
		/// </summary>
		[DisplayName("Value Changed")]
		[Category("SkillValue")]
		public event EventHandler ValueChanged;

		/// <summary>
		/// Initializes a new instance of the <see cref="SkillValue"/> class.
		/// </summary>
		public SkillValue()
		{
			InitializeComponent();
		}

		private int _value;

		/// <summary>
		/// значение скила
		/// </summary>
		/// <value>The value.</value>
		[DisplayName("Skill Value")]
		[Category("SkillValue")]
		[DefaultValue(0)]
		public int Value
		{
			get { return _value; }
			set
			{
				_value = value;
				SetSkillValue(Value);
				if (ValueChanged != null)
					ValueChanged(this, EventArgs.Empty);
			}
		}

		private void numericUpDownValue_ValueChanged(object sender, EventArgs e)
		{
			Value = (int) numericUpDownValue.Value;
		}

		/// <summary>
		/// Ставит нужную картинку для скила
		/// </summary>
		/// <param name="skillValue"></param>
		private void SetSkillValue(int skillValue)
		{
			switch (skillValue)
			{
				case 5:
					pictureBoxValue.Image = Resources.level5;
					break;
				case 4:
					pictureBoxValue.Image = Resources.level4;
					break;
				case 3:
					pictureBoxValue.Image = Resources.level3;
					break;
				case 2:
					pictureBoxValue.Image = Resources.level2;
					break;
				case 1:
					pictureBoxValue.Image = Resources.level1;
					break;
				default:
					pictureBoxValue.Image = Resources.level0;
					break;
			}
			numericUpDownValue.Value = skillValue;
		}
	}
}