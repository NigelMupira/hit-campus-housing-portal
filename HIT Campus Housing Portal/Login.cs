using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace HIT_Campus_Housing_Portal
{
    public partial class Login : Form
    {
        private bool isVisible = false; // Flag to track password visibility state

        public Login()
        {
            InitializeComponent();
            btnShow.Visible = false; // Hide the show password button
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            // Display the show password button if there is any input in the password section
            if (txtPassword.Text.Length != 0)
            {
                btnShow.Visible = true;
            }
            else
            {
                btnShow.Visible = false;
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            // Decide whether to show or hide password
            if (!isVisible) // If the password is hidden
            {
                txtPassword.UseSystemPasswordChar = false; // Show password
                isVisible = true; // Update flag
                btnShow.BackgroundImage = Properties.Resources.pass_hide; // Change image shown on button
            }
            else // If the password is visible
            {
                txtPassword.UseSystemPasswordChar = true; // Hide password
                isVisible = false; // Update flag
                btnShow.BackgroundImage = Properties.Resources.pass_show; // Change image shown on button
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
                                                
            if (username == "user" || password == "123") // Dummy student credentials
            {
                // Open student dashboard
                StudentDash studDash = new StudentDash();
                studDash.Show();
                this.Hide(); // Hide the login form
            }
            else if (username == "admin" || password == "000") // Dummy admin credentials
            {
                // Open admin dashboard
                AdminDash adminDash = new AdminDash();
                adminDash.Show();
                this.Hide(); // Hide the login form
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            SignUp register = new SignUp();
            register.Show(); // Show the sign up form
            this.Hide(); // Hide the current form
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show(); // Show the home form
            this.Hide(); // Hide the current form
        }
    }
}
