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
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;


namespace Olympic_mintazh_v2
{
    public partial class Form1 : Form
    {
        List<OlympicResult> results = new List<OlympicResult>();
        Excel.Application xlApp;
        Excel.Workbook xlWB;
        Excel.Worksheet xlSheet;

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
            Excel();
                     
        }
        private void Excel()
        {
            try
            {
                xlApp = new Excel.Application();
                xlWB = xlApp.Workbooks.Add(Missing.Value);
                xlSheet = xlWB.ActiveSheet;

                xlApp.Visible = true;
                Export();
                xlApp.UserControl = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
                
            }
        }
        private void Export()
        {
            string[] headers = new string[]
            {
                "Helyezés",
                "Ország",
                "Arany",
                "Ezüst",
                "Bronz"
            };
            for (int i = 0; i < headers.Length; i++)
            {
                xlSheet.Cells[1, i + 1] = headers[i];
                
            }

            var selectedYear = from x in results
                               where x.Year == (int)comboBoxYear.SelectedItem 
                               orderby x.Position 
                               select x;
            var counter = 2;
            foreach (var s in selectedYear)
            {
                xlSheet.Cells[counter, 1] = s.Position;
                xlSheet.Cells[counter, 2] = s.Country;
                xlSheet.Cells[counter, 3] = s.Medals[0];
                xlSheet.Cells[counter, 4] = s.Medals[1];
                xlSheet.Cells[counter, 5] = s.Medals[2];
                counter++;
            }
        }
    }
}
