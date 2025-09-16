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
            var (userResponse, message) = await _userService.LoginAsync(txtEmail.Text, txtPassword.Text);
            if(userResponse is null)
                MessageBox.Show(message, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (userResponse.Role == UserRole.Admin)
            {
                AdminForm adminform = new AdminForm(userResponse);
                this.Hide();
                adminform.Show();
            }
            else
            {
                HomeForm homeform = new HomeForm(userResponse);
                this.Hide();
                homeform.Show();
            }
            
        }
    }
}
