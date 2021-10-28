using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Week06.Entities;
using Week06.MnbServiceReference;


namespace Week06
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
        public Form1()
        {

            var mnbService = new MNBArfolyamServiceSoapClient();
            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };
            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;

            dgwRates.DataSource = Rates;
            
            XML(result);

            InitializeComponent();

        }

        private void XML(string result)
        {
            var xml = new XmlDocument();
            xml.LoadXml(result);
            foreach (XmlElement element in xml.DocumentElement)
            {
                var rate = new RateData();
                Rates.Add(rate);

                //Dátum 

                rate.Date = DateTime.Parse( element.GetAttribute("date"));

                //Valuta

                var ChildElement = (XmlElement)element.ChildNodes[0];
                rate.Currency = ChildElement.GetAttribute("curr");

                //Érték

                var unit = decimal.Parse(ChildElement.GetAttribute("unit"));
                var value = decimal.Parse(ChildElement.InnerText);
                if (unit != 0) rate.Value = value / unit;




            }
        }

    }
}
