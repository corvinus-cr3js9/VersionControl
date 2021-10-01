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
using UserMaintance.Entities;

namespace UserMaintance
{

    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();
        
        public Form1()
        {
            InitializeComponent();
            lblLastName.Text = Resource1.LastName;
            btnAdd.Text = Resource1.Add;
            btnWrite.Text = Resource1.WriteFile;
            btnDelete.Text = Resource1.Delete;

            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "Fullname";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
                User u = new User();
                u.Fullname = txtboxLastName.Text;
               // u.FistrName = txtboxFirstName.Text;
                users.Add(u);
            txtboxLastName.Clear();
          
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() != DialogResult.OK) return;

            using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
            {
                foreach (var s in users)
                {
                    sw.Write(s.ID);
                    sw.Write(";");
                    sw.Write(s.Fullname);
                    sw.WriteLine();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var delete = listBox1.SelectedItem;
            if (delete != null)
            {
                users.Remove((User)delete);
            }
        }
    }
}
