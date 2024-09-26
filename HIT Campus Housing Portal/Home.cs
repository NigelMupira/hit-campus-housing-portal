using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIT_Campus_Housing_Portal
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show(); // Show the login form
            this.Hide();  // Hide the current form
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            SignUp create = new SignUp();
            create.Show(); // Show the sign up form
            this.Hide(); // Hide the current form
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Exit the application
        }
    }
}
