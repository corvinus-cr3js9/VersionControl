using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
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
            InitializeComponent();
            RefreshData();

        }

        private void RefreshData()
        {
            Rates.Clear();
            var mnbService = new MNBArfolyamServiceSoapClient();
            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = (string)comboBox1.SelectedItem,
                startDate = dateTimePicker2.Value.ToString(),
                endDate = dateTimePicker1.ToString()
            };
            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;

            dgwRates.DataSource = Rates;

            XML(result);
            Diagram();
        }

        private void XML(string result)
        {
            var xml = new XmlDocument();
            xml.LoadXml(result);
            
            foreach (XmlElement element in xml.DocumentElement)
            {
                var rate = new RateData();
                

                //Dátum 

                rate.Date = DateTime.Parse( element.GetAttribute("date"));

                //Valuta

                var ChildElement = (XmlElement)element.ChildNodes[0];
                rate.Currency = ChildElement.GetAttribute("curr");

                //Érték

                var unit = decimal.Parse(ChildElement.GetAttribute("unit"));
                var value = decimal.Parse(ChildElement.InnerText);
                if (unit != 0) rate.Value = value / unit;

                Rates.Add(rate);


            }
        }

        private void Diagram()
        {
            chartRateData.DataSource = Rates;
            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;




        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();

        }
    }
}
