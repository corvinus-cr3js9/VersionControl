using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserMaintance
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lblFirstName.Text = Resource1.FirstName;
            lblLastName.Text = Resource1.LastName;
            btnAdd.Text = Resource1.Add;
        }
    }
}
