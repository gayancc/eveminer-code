using System;
using System.Text.RegularExpressions;
using System.Xml;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace EveMiner
{
	public class GridToXmlConverter
	{
		/// <summary>
		/// Стиль ячейки XML "отсутствует значение"
		/// </summary>
		private const string StyleXmlMissval = "missval";

		/// <summary>
		/// Стиль ячейки XML "обычный"
		/// </summary>
		private const string StyleXmlGeneral = "general";

		/// <summary>
		/// Стиль ячейки XML "время"
		/// </summary>
		private const string StyleXmlTime = "time";

		/// <summary>
		/// Стиль ячейки XML "заголовок"
		/// </summary>
		private const string StyleXmlHeader = "header";


		/// <summary>
		/// Экспорт данных в Excel
		/// </summary>
		public void ExportIntoXml(DataGridView grid)
		{
			XmlWriterSettings settings = new XmlWriterSettings {Indent = true, IndentChars = ("    ")};
			const string filename = "ExportData.xls";
			//Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + Application.ProductName, "ExportData.xls");
			try
			{
				using (XmlWriter writer = XmlWriter.Create(filename, settings))
				{
					WriteHeader(writer);
					WriteStyles(writer);
					WriteTable(writer, grid);
					if (writer != null) writer.Flush();
				}
			}
			catch (IOException)
			{
				MessageBox.Show("File is busy." +
				                Environment.NewLine + "Close Excel and try it again.",
				                Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			Process.Start(filename);
		}

		/// <summary>
		/// Запсиь заголовка XML
		/// </summary>
		/// <param name="writer">писатель в XML</param>
		private static void WriteHeader(XmlWriter writer)
		{
			writer.WriteProcessingInstruction("mso-application", "progid=\"Excel.Sheet\"");
			writer.WriteStartElement("Workbook", "urn:schemas-microsoft-com:office:spreadsheet");
			writer.WriteAttributeString("xmlns", "o", null, "urn:schemas-microsoft-com:office:office");
			writer.WriteAttributeString("xmlns", "x", null, "urn:schemas-microsoft-com:office:excel");
			writer.WriteAttributeString("xmlns", "ss", null, "urn:schemas-microsoft-com:office:spreadsheet");
			writer.WriteAttributeString("xmlns", "html", null, "http://www.w3.org/TR/REC-html40");
		}

		/// <summary>
		/// Записать стили в xml файл
		/// </summary>
		/// <param name="writer">писатель в XML</param>
		private static void WriteStyles(XmlWriter writer)
		{
			writer.WriteStartElement("Styles");

			WriteStyle(writer, StyleXmlHeader, true, Color.FromArgb(0xC0, 0xC0, 0xC0));
			WriteStyle(writer, StyleXmlGeneral, false, Color.FromArgb(0xFF, 0xFF, 0xFF));
			WriteStyle(writer, StyleXmlMissval, false, Color.FromArgb(0xFF, 0x00, 0x00));
			WriteStyle(writer, StyleXmlTime, false, Color.FromArgb(0xFF, 0xCC, 0x99));

			writer.WriteEndElement();
		}

		/// <summary>
		/// Записать 1 ститль в XML
		/// </summary>
		/// <param name="writer">писатель в XML</param>
		/// <param name="name">имя стиля</param>
		/// <param name="bold">полужирный ли шрифт</param>
		/// <param name="color">цвет фона ячейки таблицы</param>
		private static void WriteStyle(XmlWriter writer, string name, bool bold, Color color)
		{
			#region Style

			writer.WriteStartElement("Style");
			writer.WriteAttributeString("ss", "ID", null, name);

			#region Alignment

			writer.WriteStartElement("Alignment");
			writer.WriteAttributeString("ss", "Horizontal", null, "Center");
			writer.WriteAttributeString("ss", "Vertical", null, "Center");
			writer.WriteAttributeString("ss", "WrapText", null, "1");
			writer.WriteEndElement();

			#endregion

			#region Borders

			writer.WriteStartElement("Borders");

			writer.WriteStartElement("Border");
			writer.WriteAttributeString("ss", "Position", null, "Bottom");
			writer.WriteAttributeString("ss", "LineStyle", null, "Continuous");
			writer.WriteEndElement();

			writer.WriteStartElement("Border");
			writer.WriteAttributeString("ss", "Position", null, "Top");
			writer.WriteAttributeString("ss", "LineStyle", null, "Continuous");
			writer.WriteEndElement();

			writer.WriteStartElement("Border");
			writer.WriteAttributeString("ss", "Position", null, "Left");
			writer.WriteAttributeString("ss", "LineStyle", null, "Continuous");
			writer.WriteEndElement();

			writer.WriteStartElement("Border");
			writer.WriteAttributeString("ss", "Position", null, "Right");
			writer.WriteAttributeString("ss", "LineStyle", null, "Continuous");
			writer.WriteEndElement();

			writer.WriteEndElement();

			#endregion

			#region Font

			writer.WriteStartElement("Font");
			writer.WriteAttributeString("ss", "FontName", null, "Arial Cyr");
			writer.WriteAttributeString("x", "CharSet", null, "204");
			writer.WriteAttributeString("ss", "Bold", null, Convert.ToInt32(bold).ToString());
			writer.WriteEndElement();

			#endregion

			#region Interior

			writer.WriteStartElement("Interior");
			writer.WriteAttributeString("ss", "Color", null,
			                            string.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B));
			writer.WriteAttributeString("ss", "Pattern", null, "Solid");
			writer.WriteEndElement();

			#endregion

			writer.WriteEndElement();

			#endregion
		}

		/// <summary>
		/// Запись таблицы с данными
		/// </summary>
		/// <param name="writer">писатель в XML</param>
		private void WriteTable(XmlWriter writer, DataGridView grid)
		{
			writer.WriteStartElement("Worksheet");
			writer.WriteAttributeString("ss", "Name", null, "Данные");

			#region Table

			writer.WriteStartElement("Table");

			foreach (DataGridViewColumn col in grid.Columns)
			{
				writer.WriteStartElement("Column");
				writer.WriteAttributeString("ss", "Width", null, col.Width.ToString());
				writer.WriteEndElement();
			}

			#region Header

			writer.WriteStartElement("Row");
			foreach (DataGridViewColumn col in grid.Columns)
			{
				if (col.Visible)
				{
					WriteCellText(writer, StyleXmlHeader, col.HeaderText);
				}
			}

			writer.WriteEndElement();

			#endregion

			#region RowData

			foreach (DataGridViewRow row in grid.Rows)
			{
				writer.WriteStartElement("Row");
				int count = row.Cells.Count;
				if (count > 1) // && !row.Cells[1].ReadOnly)
				{
					WriteCellText(writer, StyleXmlTime, row.Cells[0].Value.ToString());
					for (int n = 1; n < count - 1; n++)
					{
						DataGridViewCell cell = row.Cells[n];
						WriteCellNumber(writer, StyleXmlGeneral, cell.Value.ToString());
					}
				}
				writer.WriteEndElement();
			}

			#endregion

			writer.WriteEndElement();

			#endregion

			writer.WriteEndElement();
		}

		/// <summary>
		/// Записать ячейку таблицы в XML
		/// </summary>
		/// <param name="writer">писатель в XML</param>
		/// <param name="type">тип стиля ячейки</param>
		/// <param name="value">значение ячейки</param>
		private static void WriteCellText(XmlWriter writer, string type, string value)
		{
			writer.WriteStartElement("Cell");
			writer.WriteAttributeString("ss", "StyleID", null, type);
			writer.WriteStartElement("Data");
			writer.WriteAttributeString("ss", "Type", null, "String");
			writer.WriteString(value);
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		/// <summary>
		/// Записать ячейку таблицы в XML
		/// </summary>
		/// <param name="writer">писатель в XML</param>
		/// <param name="type">тип стиля ячейки</param>
		/// <param name="value">значение ячейки</param>
		private static void WriteCellNumber(XmlWriter writer, string type, string value)
		{
			string val = Regex.Replace(value, @"\s", "");
			val = val.Replace(",", ".");

			writer.WriteStartElement("Cell");
			writer.WriteAttributeString("ss", "StyleID", null, type);
			if (value != "")
			{
				writer.WriteStartElement("Data");
				writer.WriteAttributeString("ss", "Type", null, "Number");
				writer.WriteString(val);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}
	}
}