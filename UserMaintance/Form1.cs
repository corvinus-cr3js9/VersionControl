using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMaintance.Entities;

namespace UserMaintance
{

    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();

        public Form1()
        {
            InitializeComponent();
            lblFirstName.Text = Resource1.FirstName;
            lblLastName.Text = Resource1.LastName;
            btnAdd.Text = Resource1.Add;

            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "Fullname";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
                User u = new User();
                u.Lastname = txtboxLastName.Text;
                u.FistrName = txtboxFirstName.Text;
                users.Add(u);
          
        }
    }
}
