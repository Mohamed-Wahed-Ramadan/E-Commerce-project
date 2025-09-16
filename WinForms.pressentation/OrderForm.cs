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
    public partial class OrderForm : Form
    {
        private readonly UserResponse _user;

        public OrderForm(UserResponse user)
        {
            InitializeComponent();
            _user = user;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            HomeForm homeForm = new HomeForm(_user);
            this.Hide();
            homeForm.Show();
        }
    }
}
