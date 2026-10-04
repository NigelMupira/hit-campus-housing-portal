using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace HIT_Campus_Housing_Portal
{
    public partial class Login : Form
    {
        private readonly HITCHPAppEntities _db = new HITCHPAppEntities(); // Connect to the database
        private bool isVisible = false; // Flag to track password visibility state

        public Login()
        {
            InitializeComponent();
            btnShow.Visible = false;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            // Display the show button if the password is not empty
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
            if (!isVisible) // If the password is hidden
            {
                txtPassword.UseSystemPasswordChar = false; // Show password
                isVisible = true; // Update flag
                btnShow.BackgroundImage = Properties.Resources.hide;
            }
            else // If the password is visible
            {
                txtPassword.UseSystemPasswordChar = true; // Hide password
                isVisible = false; // Update flag
                btnShow.BackgroundImage = Properties.Resources.appear1;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text;
                string password = txtPassword.Text;

                // 
                var student = _db.LoginDetails.FirstOrDefault(s => s.Username == username && s.Password == password);
                // 
                var admin = _db.AdminLogins.FirstOrDefault(a => a.Username == username && a.Password == password);


                if (username == "" || password == "")
                {
                    // If no credentials are entered, display an error message
                    MessageBox.Show("Please enter login details to proceed.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (student != null) // Check if the credentials match a student record
                {
                    // Open student dashboard
                    StudentDash studDash = new StudentDash();
                    studDash.Show();
                    this.Hide(); // Hide the login form
                    return;
                }
                else if (admin != null) // Check if the credentials match an admin record
                {
                    // Open admin dashboard
                    AdminDash adminDash = new AdminDash();
                    adminDash.Show();
                    this.Hide(); // Hide the login form
                    return;
                }
                else
                {
                    // If credentials don't match any existing records, display an error message
                    MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
