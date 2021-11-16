using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Week08.Abstractions;
using Week08.Entities;

namespace Week08
{
    public partial class Form1 : Form
    {
        List<Toy> __toys = new List<Toy>();
        private IToyFactory _factory;

        public IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }

        public Form1()
        {
            InitializeComponent();

            Factory = new CarFactory();
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var toy = Factory.CreateNew();
            __toys.Add(toy);
            toy.Left = -toy.Width;
            mainPanel.Controls.Add(toy);

        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosition = 0;
            foreach (var toy in __toys)
            {
                toy.MoveToy();
                if (toy.Left > maxPosition)
                    maxPosition = toy.Left;
            }

            if (maxPosition > 1000)
            {
                var oldestToy = __toys[0];
                mainPanel.Controls.Remove(oldestToy);
                __toys.Remove(oldestToy);
            }
        
        }
    }
}
