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
        private Toy _nextToy;
        public IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value;
                DisplayNext(); }
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

        private void btnSelectCar_Click(object sender, EventArgs e)
        {
            Factory = new CarFactory();
        }

        private void btnSelectBall_Click(object sender, EventArgs e)
        {
            Factory = new BallFactory()
            {
                BallColor = btnColor.BackColor
            };

        }
        private void DisplayNext()
        {
            if (_nextToy != null)
                Controls.Remove(_nextToy);
            _nextToy = Factory.CreateNew();
            _nextToy.Top = label1.Top + label1.Height + 20;
            _nextToy.Left = label1.Left;
            Controls.Add(_nextToy);
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var colorPicker = new ColorDialog();
            colorPicker.Color = button.BackColor;
            if (colorPicker.ShowDialog() != DialogResult.OK)
                return;
            button.BackColor = colorPicker.Color;
            
        }
    }
}
