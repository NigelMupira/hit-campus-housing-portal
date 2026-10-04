using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HIT_Campus_Housing_Portal
{
    public partial class Password : Form
    {
        private readonly HITCHPAppEntities _db = new HITCHPAppEntities();

        public Password(string regNumber)
        {
            InitializeComponent();
            groupBox1.Visible = false; // Hide the group box
            label1.Location = new Point(402, 189); // Set the location of the label
            lblUser.Location = new Point(608, 189); // Set the location of the label
            groupBox2.Location = new Point(389, 260); // Set the location of the group box
            lblUser.Text = regNumber;  // Set the text of the label to the registration number
        }

        private void btnForgot_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
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

        private void btnCreate_Click(object sender, EventArgs e)
        {
            SignUp register = (SignUp)Application.OpenForms["SignUp"]; // Get the sign up form
            string name = register.name; // Get the name from the sign up form

            string pass = txtPassword.Text;
            string repeat = txtPassword.Text;

            if (pass.ToString() != repeat.ToString())
            {
                MessageBox.Show("Your passwords do not match.", "Password mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Show a message box with the name of the user
                MessageBox.Show($"Welcome {name}!!! \n\r" + "You have successfully created your HIT CHP Account", "Account Created");

                var loginCredentials = new LoginDetail
                {
                    Username = lblUser.Text,
                    Password = pass.ToString()
                };
                _db.LoginDetails.Add(loginCredentials);
                _db.SaveChanges();

                Login login = new Login();
                login.Show(); // Show the login form
                this.Hide(); // Hide the current form
            }
        }
    }
}
