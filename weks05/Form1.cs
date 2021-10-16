using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using weks05.Entities;

namespace weks05
{
    public partial class Form1 : Form
    {
        PortfolioEntities context = new PortfolioEntities();
        List<Tick> Ticks;

        List<PortfolioItem> Portfolio = new List<PortfolioItem>();
        public Form1()
        {
            InitializeComponent();
            CreatePortfolio();

            Ticks = context.Ticks.ToList();
            dataGridView1.DataSource = Ticks;
            
        }

        private void CreatePortfolio()
        {
            Portfolio.Add(new PortfolioItem() { Index = "OTP", Voluem = 10 });
            Portfolio.Add(new PortfolioItem() { Index = "ZWACK", Voluem = 10 });
            Portfolio.Add(new PortfolioItem() { Index = "ELMU", Voluem = 10 });

            dataGridView2.DataSource = Portfolio;
        }
    }
}
