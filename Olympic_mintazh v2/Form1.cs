using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Reflection;


namespace Olympic_mintazh_v2
{
    public partial class Form1 : Form
    {
        List<OlympicResult> results = new List<OlympicResult>();

        public Form1()
        {
            InitializeComponent();
            Load("Summer_olympic_Medals.csv");
            Year();
            CalculatePosition();
        }
        public void Load(string Filename)
        {
            using (var sr = new StreamReader(Filename, Encoding.Default))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(',');
                    var or = new OlympicResult()
                    {
                        Year = int.Parse(line[0]),
                        Country = line[3],
                        Medals = new int[]
                        {
                            int.Parse(line[5]),
                            int.Parse(line[6]),
                            int.Parse(line[7])
                        }
                       

                    };
                    results.Add(or);
                }

            }
            
        }
        public void Year()
        {
            var year = (from x in results
                        orderby x.Year descending
                        select x.Year).Distinct();

            YearBindingSource.DataSource = year.ToList();
            comboBoxYear.DataSource = YearBindingSource;
           
        }
        private int CalculateOrder(OlympicResult or)
        {
            int counter = 0;
            var SzurtLista = from x in results
                             where or.Year == x.Year && or.Country != x.Country
                             select x;
            foreach (var f in SzurtLista)
            {
                if (f.Medals[0] > or.Medals[0]) counter++;
                else if (f.Medals[0] == or.Medals[0] && f.Medals[1] > or.Medals[1]) counter++;
                else if (f.Medals[0] == or.Medals[0] && f.Medals[1] == or.Medals[1] && f.Medals[2] == or.Medals[2]) counter++;
            }
            return counter + 1;

        }
        private void CalculatePosition()
        {
            foreach (var p in results)
            {
                p.Position = CalculateOrder(p);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

                     
        }
        private void ExcelExport()
        {
            try
            {
                xlApp = new 
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
