using System;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using EveMiner.EveDatabase;

namespace EveMiner.Forms
{
	public partial class PricesForm : Form
	{
		public PricesForm()
		{
			InitializeComponent();
			PutMineralPrices();
		}

		private void PricesForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			Hide();
		}

		private void btnEveCentral_Click(object sender, EventArgs e)
		{
			GetMineralsPricesFromEveCentral();
			PutMineralPrices();
		}

		private void GetMineralsPricesFromEveCentral()
		{
			// Строки: URI и имя локального файла
			const string webAddress = "http://eve-central.com/api/evemon";
			const string localAddress = "EveCentral.xml";

			try
			{
				// Два объекта для получения информации о предполагаемом скачиваемом xml
				HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(webAddress);
				WebClient httpClient = new WebClient();
				HttpWebResponse httpWResp = (HttpWebResponse)httpWReq.GetResponse();
				// Проверяем,  действительно ли по данному адресу находится xml
				//string type = httpWResp.ContentType.Substring(0, "text/xml".Length);
				//if (type == "text/xml")
				//{
				// Скачиваем
				httpClient.DownloadFile(webAddress, localAddress);
				//}
				httpWResp.Close();
			}
			catch (WebException ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}

			try
			{
				System.Globalization.NumberFormatInfo info = new System.Globalization.NumberFormatInfo();
				info.NumberDecimalSeparator = ".";

				using (XmlTextReader reader = new XmlTextReader(localAddress))
				{
					Mineral min = null;
					while (reader.Read())
					{
						switch (reader.NodeType)
						{
							case XmlNodeType.Text:
								{
									if (min == null)
										min = MineralList.Get(reader.Value);
									else
									{
										min.Price = Convert.ToDouble(reader.Value, info);
										min = null;
									}
									break;
								}
						}
					}
				}
			}
			catch (XmlException)
			{
			}			
		}

		private void PutMineralPrices()
		{
			textBoxPriceTritanium.Text = MineralList.Get("Tritanium").Price.ToString("F2");
			textBoxPricePyerite.Text = MineralList.Get("Pyerite").Price.ToString("F2");
			textBoxPriceMexallon.Text = MineralList.Get("Mexallon").Price.ToString("F2");
			textBoxPriceIsogen.Text = MineralList.Get("Isogen").Price.ToString("F2");
			textBoxPriceNocxium.Text = MineralList.Get("Nocxium").Price.ToString("F2");
			textBoxPriceZydrine.Text = MineralList.Get("Zydrine").Price.ToString("F2");
			textBoxPriceMegacyte.Text = MineralList.Get("Megacyte").Price.ToString("F2");
			textBoxPriceMorphite.Text = MineralList.Get("Morphite").Price.ToString("F2");
		}

		private void textBoxPrice_TextChanged(object sender, EventArgs e)
		{
			TextBox box = sender as TextBox;
			double price = 0.0;
			if (box == null)
				return;
			try
			{
				price = Convert.ToDouble(box.Text);
			}
			catch (FormatException)
			{
			}
			Mineral m = box.Tag as Mineral;
			if (m != null)
				m.Price = price;

			if (sender == textBoxPriceTritanium)
				Config<Settings>.Instance.PriceTritanium = price;
			else if (sender == textBoxPricePyerite)
				Config<Settings>.Instance.PricePyerite = price;
			else if (sender == textBoxPriceMexallon)
				Config<Settings>.Instance.PriceMexallon = price;
			else if (sender == textBoxPriceIsogen)
				Config<Settings>.Instance.PriceIsogen = price;
			else if (sender == textBoxPriceNocxium)
				Config<Settings>.Instance.PriceNocxium = price;
			else if (sender == textBoxPriceZydrine)
				Config<Settings>.Instance.PriceZydrine = price;
			else if (sender == textBoxPriceMegacyte)
				Config<Settings>.Instance.PriceMegacyte = price;
			else if (sender == textBoxPriceMorphite)
				Config<Settings>.Instance.PriceMorphite = price;

		}

	}
}
