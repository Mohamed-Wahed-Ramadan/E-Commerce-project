using E_Commerce_project.models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms.pressentation
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            this.Hide();
            form.Show();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if(txtEmail.Text == "admin" && txtPassword.Text == "admin")
            {
                AdminForm adminform = new AdminForm();
                this.Hide();
                adminform.Show();
            }
            else
            {
                //add order
                //delete cart info
                HomeForm homeform = new HomeForm();
                this.Hide();
                homeform.Show();
            }
               
        }
    }
}
