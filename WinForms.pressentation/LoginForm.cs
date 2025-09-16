using Autofac;
using E_Commerce.application.Interfaces;
using E_Commerce.application.Services;
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
        private readonly IUserServices _userService;

        public LoginForm()
        {
            InitializeComponent();
            var builder = Autofac.Inject();
            _userService = builder.Resolve<IUserServices>();
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            this.Hide();
            form.Show();
        }

        private async void btnDone_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == "admin" && txtPassword.Text == "admin")
            {
                AdminForm adminform = new AdminForm();
                this.Hide();
                adminform.Show();
            }
            else
            {
                if (!string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(txtPassword.Text))
                {
                    var (userResponse, message) = await _userService.LoginAsync(txtEmail.Text, txtPassword.Text);

                    if (message == "Success!" && userResponse != null)
                    {
                        HomeForm homeform = new HomeForm();
                        this.Hide();
                        homeform.Show();
                    }
                    else
                    {
                        MessageBox.Show(message, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
