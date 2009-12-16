using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace EveMiner.Forms
{
	/// <summary>
	/// Контрол для постронеия гистограмм
	/// </summary>
	public partial class Histogram : Control
	{
		#region Закрытые параметры

		/// <summary>
		/// Верхний цвет фона
		/// </summary>
		private Color _topColor = Color.White;

		private static Color TopDefaultColor
		{
			get { return Color.White; }
		}

		/// <summary>
		/// Нижний цвет фона
		/// </summary>
		private Color _bottomColor = Color.LightSteelBlue;

		private static Color BottomDefaultColor
		{
			get { return Color.LightSteelBlue; }
		}

		/// <summary>
		/// Цвет сетки
		/// </summary>
		private Color _gridColor = Color.Black;

		private static Color GridDefaultColor
		{
			get { return Color.Black; }
		}

		/// <summary>
		/// Расстояние между барами в пикселях
		/// </summary>
		private int _delta = DeltaDefault;

		private const int DeltaDefault = 5;

		/// <summary>
		/// Ширина области границы по осям X и Y
		/// </summary>
		private int _borderX = BorderDefault;

		private int _borderY = BorderDefault;
		private const int BorderDefault = 20;

		/// <summary>
		/// Список баров на гистограмме
		/// </summary>
		private List<Bar> _listBars = new List<Bar>();

		/// <summary>
		/// Показывать имена баров
		/// </summary>
		private bool _showLabels;

		/// <summary>
		/// Показывать значения на гистограмме
		/// </summary>
		private bool _showValues;

		/// <summary>
		/// Шрифт меток значений
		/// </summary>
		private Font _valuesFont;

		/// <summary>
		/// Суффикс для значения
		/// </summary>
		private string _suffix;

		/// <summary>
		/// Мышь в прямоугольнике бара или нет
		/// </summary>
		private Bar _mouseEnteredBar;

		private static Font ValuesDefaultFont
		{
			get { return new Font(FontFamily.GenericSansSerif, 8.25f); }
		}

		#endregion

		#region Параметры контрола

		/// <summary>
		/// Occurs when [bar enter event].
		/// </summary>
		[Category("Histogram")]
		[Description("Mouse enter bar")]
		public event HistogramEnterEventHandler BarEnterEvent;

		/// <summary>
		/// Occurs when [bar leave event].
		/// </summary>
		[Category("Histogram")]
		[Description("Mouse leave bar")]
		public event HistogramEnterEventHandler BarLeaveEvent;

		/// <summary>
		/// Верхний цвет фона
		/// </summary>
		[DisplayName("Color Top")]
		[Category("Histogram")]
		[Description("Top color area of histogram")]
		public Color TopColor
		{
			get
			{
				if (_topColor == Color.Empty)
					_topColor = TopDefaultColor;
				return _topColor;
			}
			set
			{
				_topColor = value;
				Invalidate();
			}
		}

		private void ResetTopColor()
		{
			TopColor = TopDefaultColor;
		}

		private bool ShouldSerializeTopColor()
		{
			return TopColor != TopDefaultColor;
		}

		/// <summary>
		/// Нижний цвет фона
		/// </summary>
		[DisplayName("Color Bottom")]
		[Category("Histogram")]
		[Description("Bottom color area of histogram")]
		public Color BottomColor
		{
			get
			{
				if (_bottomColor == Color.Empty)
					_bottomColor = BottomDefaultColor;
				return _bottomColor;
			}
			set
			{
				_bottomColor = value;
				Invalidate();
			}
		}

		private void ResetBottomColor()
		{
			BottomColor = BottomDefaultColor;
		}

		private bool ShouldSerializeBottomColor()
		{
			return BottomColor != BottomDefaultColor;
		}

		/// <summary>
		/// Цвет сетки
		/// </summary>
		[DisplayName("Grid Color")]
		[Category("Histogram")]
		[Description("Grid Color")]
		public Color GridColor
		{
			get
			{
				if (_gridColor == Color.Empty)
					_gridColor = GridDefaultColor;

				return _gridColor;
			}
			set
			{
				_gridColor = value;
				Invalidate();
			}
		}

		private void ResetGridColor()
		{
			GridColor = GridDefaultColor;
		}

		private bool ShouldSerializeGridColor()
		{
			return GridColor != GridDefaultColor;
		}

		/// <summary>
		/// Расстояние между барами в пикселях
		/// </summary>
		[DisplayName("Delta")]
		[Category("Histogram")]
		[Description("Interval in pixels between bars")]
		[DefaultValue(DeltaDefault)]
		public int Delta
		{
			get { return _delta; }
			set
			{
				_delta = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Ширина области границы по Оси X
		/// </summary>
		[DisplayName("Border X")]
		[Category("Histogram")]
		[Description("Width border left and right diagram")]
		[DefaultValue(BorderDefault)]
		public int BorderX
		{
			get { return _borderX; }
			set
			{
				_borderX = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Ширина области границы по Оси Y
		/// </summary>
		[DisplayName("Border Y")]
		[Category("Histogram")]
		[Description("Width border top and bottom diagram")]
		[DefaultValue(BorderDefault)]
		public int BorderY
		{
			get { return _borderY; }
			set
			{
				_borderY = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Список баров на гистограмме
		/// </summary>
		[DisplayName("Bars")]
		[Category("Histogram")]
		[Description("Bars collection")]
		public List<Bar> ListBars
		{
			get { return _listBars; }
			set
			{
				_listBars = value;
				Invalidate();
			}
		}

		private bool ShouldSerializeListBars()
		{
			Invalidate();
			return _listBars.Count != 0;
		}

		/// <summary>
		/// Показывать или нет имена баров
		/// </summary>
		[DisplayName("Label Show")]
		[Category("Histogram")]
		[Description("Show names of bars on diagram")]
		[DefaultValue(false)]
		public bool ShowLabels
		{
			get { return _showLabels; }
			set
			{
				_showLabels = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Показывать или нет значения
		/// </summary>
		[DisplayName("Values Show")]
		[Category("Histogram")]
		[Description("Show values on diagram")]
		[DefaultValue(false)]
		public bool ShowValues
		{
			get { return _showValues; }
			set
			{
				_showValues = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Шрифт подписей
		/// </summary>
		[DisplayName("Values Font")]
		[Category("Histogram")]
		[Description("Font for labels")]
		public Font ValuesFont
		{
			get
			{
				if (_valuesFont == null)
					_valuesFont = ValuesDefaultFont;
				return _valuesFont;
			}
			set
			{
				_valuesFont = value;
				Invalidate();
			}
		}

		private void ResetValuesFont()
		{
			ValuesFont = ValuesDefaultFont;
		}

		private bool ShouldSerializeValuesFont()
		{
			return !ValuesFont.Equals(ValuesDefaultFont);
		}

		/// <summary>
		/// Суффикс для значения
		/// </summary>
		[DisplayName("Suffix")]
		[Category("Histogram")]
		[Description("Value suffix")]
		public string Suffix
		{
			get { return _suffix; }
			set
			{
				_suffix = value;
				Invalidate();
			}
		}

		#endregion

		///<summary>
		///</summary>
		public Histogram()
		{
			InitializeComponent();
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			Size = new Size(250, 200);
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			// Calling the base class OnPaint
			base.OnPaint(pe);

			pe.Graphics.SmoothingMode = SmoothingMode.HighQuality;
			Rectangle rec = new Rectangle(new Point(0, 0), Size);

			using (Brush br = new LinearGradientBrush(rec, TopColor, BottomColor, LinearGradientMode.ForwardDiagonal))
			{
				pe.Graphics.FillRectangle(br, rec);
			}

			//Если не хватит места для отрисовки баров
			if ((_listBars.Count - 1)*Delta + BorderX*2 >= rec.Width)
				return;
			if (rec.Height <= BorderY*2)
				return;

			//поиск максимальных значений положительных и отрицатеьных
			double maxValTop = 0.0;
			double maxValBottom = 0.0;
			foreach (Bar bar in _listBars)
			{
				if (bar.Value < 0 && maxValBottom < Math.Abs(bar.Value))
					maxValBottom = Math.Abs(bar.Value);
				else if (bar.Value > 0 && maxValTop < bar.Value)
					maxValTop = bar.Value;
			}
			if (!(maxValTop + maxValBottom > 0))
				return;

			rec.Width -= BorderX*2;
			rec.Height -= BorderY*2;
			rec.Location = new Point(BorderX, BorderY);

			//Точка основания диаграммы
			Point baseLineStart = new Point(BorderX,
			                                Convert.ToInt32(rec.Height*maxValTop/(maxValTop + maxValBottom)) + BorderY);
			Point baseLineEnd = new Point(Size.Width - BorderX, baseLineStart.Y);

			Rectangle currentBar = new Rectangle
			                       	{
			                       		Width = (rec.Width - (_listBars.Count - 1)*Delta)/_listBars.Count
			                       	};
			//Ширина одного столбца
			//рисуем стобцы
			for (int n = 0; n < _listBars.Count; n++)
			{
				Bar bar = _listBars[n];
				currentBar.Height = Convert.ToInt32(Math.Abs(bar.Value)*rec.Height/(maxValTop + maxValBottom));
				currentBar.X = n*(Delta + currentBar.Width) + BorderX;

				if (bar.Value < 0)
					currentBar.Y = baseLineStart.Y;
				else
				{
					currentBar.Y = baseLineStart.Y - currentBar.Height;
				}

				if (currentBar.Width > 0 && currentBar.Height > 0)
				{
					using (Brush br = new LinearGradientBrush(currentBar, bar.Color1, bar.Color2, LinearGradientMode.Horizontal))
					{
						pe.Graphics.FillRectangle(br, currentBar);
					}
					pe.Graphics.DrawRectangle(new Pen(GridColor), currentBar);
					bar.Rect = currentBar;
				}
				//если показывать метки
				if (ShowLabels)
				{
					using (StringFormat format = new StringFormat())
					{
						//format.FormatFlags = StringFormatFlags.DirectionVertical;
						format.Alignment = StringAlignment.Center;

						if (bar.Value < 0)
							format.LineAlignment = StringAlignment.Far;
						Point pt = new Point(currentBar.X + currentBar.Width/2,
						                     currentBar.Y + ((bar.Value < 0) ? 0 : currentBar.Height));

						pe.Graphics.DrawString(bar.Name, Font, new SolidBrush(ForeColor), pt, format);
					}
				}
				//Если включен показ значений
				if (ShowValues && bar.Value != 0)
				{
					using (StringFormat format = new StringFormat())
					{
						format.Alignment = StringAlignment.Center;
						if (bar.Value > 0)
							format.LineAlignment = StringAlignment.Far;

						Point pt = new Point(currentBar.X + currentBar.Width/2,
						                     currentBar.Y + ((bar.Value > 0) ? 0 : currentBar.Height - 15));
						pe.Graphics.DrawString(bar.Value.ToString("#,#.##") + Suffix, ValuesFont, new SolidBrush(ForeColor), pt, format);
					}
				}
			}
			pe.Graphics.DrawLine(new Pen(GridColor), baseLineStart, baseLineEnd);
		}


		/// <summary>
		/// Handles the Resize event of the Histogram control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void Histogram_Resize(object sender, EventArgs e)
		{
			Invalidate();
		}

		/// <summary>
		/// Handles the MouseMove event of the Histogram control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
		private void Histogram_MouseMove(object sender, MouseEventArgs e)
		{
			Bar fb = null;
			foreach (Bar bar in ListBars)
			{
				if (e.X > bar.Rect.Left && e.X < bar.Rect.Right &&
				    e.Y < bar.Rect.Bottom && e.Y > bar.Rect.Top)
					fb = bar;
			}
			if (fb != null)
			{
				if (BarEnterEvent != null && _mouseEnteredBar != fb)
					BarEnterEvent(this, new HistogramEnterEventHandlerArgs {Bar = fb});
				_mouseEnteredBar = fb;
			}
			else if (_mouseEnteredBar != null && BarLeaveEvent != null)
			{
				BarLeaveEvent(this, new HistogramEnterEventHandlerArgs {Bar = _mouseEnteredBar});
				_mouseEnteredBar = null;
			}
		}
	}

	/// <summary>
	/// Один прямоугольник гистограммы
	/// </summary>
	[Serializable]
	public class Bar
	{
		private double _value;

		/// <summary>
		/// Имя бара
		/// </summary>
		private string _name;

		/// <summary>
		/// Верхний цвет бара
		/// </summary>
		private Color _color1 = Color.CornflowerBlue;

		private static Color DefaultColor1
		{
			get { return Color.CornflowerBlue; }
		}

		/// <summary>
		/// Нижний цвет бара
		/// </summary>
		private Color _color2 = Color.Black;

		private static Color DefaultColor2
		{
			get { return Color.Black; }
		}

		/// <summary>
		/// обрамляющий прямоугольник бара
		/// </summary>
		[NonSerialized] public Rectangle Rect;

		/// <summary>
		/// Значение
		/// </summary>
		[DisplayName("Value")]
		[DefaultValue(0.0)]
		public double Value
		{
			get { return _value; }
			set { _value = value; }
		}

		/// <summary>
		/// Имя бара
		/// </summary>
		[DisplayName("Name")]
		[DefaultValue(0.0)]
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		/// <summary>
		/// Верхний цвет бара
		/// </summary>
		[DisplayName("Color 1")]
		public Color Color1
		{
			get
			{
				if (_color1 == Color.Empty)
					_color1 = DefaultColor1;
				return _color1;
			}
			set { _color1 = value; }
		}

		private void ResetColor1()
		{
			Color1 = DefaultColor1;
		}

		private bool ShouldSerializeColor1()
		{
			return Color1 != DefaultColor1;
		}

		/// <summary>
		/// Нижний цвет бара
		/// </summary>
		[DisplayName("Color 2")]
		public Color Color2
		{
			get
			{
				if (_color2 == Color.Empty)
					_color2 = DefaultColor2;
				return _color2;
			}
			set { _color2 = value; }
		}


		private void ResetColor2()
		{
			Color2 = DefaultColor2;
		}

		private bool ShouldSerializeColor2()
		{
			return Color2 != DefaultColor2;
		}
	}

	/// <summary>
	/// Параметры события гистограммы
	/// </summary>
	public delegate void HistogramEnterEventHandler(object sender, HistogramEnterEventHandlerArgs args);

	/// <summary>
	/// Праметры для событий гистограммы
	/// </summary>
	public class HistogramEnterEventHandlerArgs : EventArgs
	{
		/// <summary>
		/// Бар гисограммы который сгенерировал событие
		/// </summary>
		public Bar Bar;
	} ;
}