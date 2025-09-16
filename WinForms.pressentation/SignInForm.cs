using Autofac;
using E_commerce.infratructure;
using E_Commerce.application.Interfaces;
using E_Commerce.application.Services;
using E_Commerce.DTOs.User;
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
    public partial class SignInForm : Form
    {
        private readonly UserService _userService;

        public SignInForm()
        {
            InitializeComponent();
            var builder = Autofac.Inject();
            _userService = (UserService?)builder.Resolve<IUserServices>();
           
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            this.Hide();
            form.Show();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var newUser = new CreateUserDTO
            {
                UserName = txtName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Password = txtPassword.Text.Trim()
            };

            // 2) استدعاء RegisterAsync
            var (success, message) = await _userService.RegisterAsync(newUser);

            // 3) التعامل مع النتيجة
            if (success)
            {
                MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // بعد التسجيل يروح على الـ Login
                LoginForm loginForm = new LoginForm();
                this.Hide();
                loginForm.Show();
            }
            else
            {
                MessageBox.Show(message, "Register Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
