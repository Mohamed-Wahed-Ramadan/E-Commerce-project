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

        private void btnDone_Click(object sender, EventArgs e)
        {
            // Take information
            // check database if user exists
            // if exists => 
            var user = new User();
            if(user.Role == UserRole.Admin)
            {
                //redirect to Admin Form
            }
            else
            {
                //redirect to Home or cart 
            }

            // check Role 
            // if admin => redirect to admin form 
            // if customer => redirect to cart or home 
        }
    }
}
