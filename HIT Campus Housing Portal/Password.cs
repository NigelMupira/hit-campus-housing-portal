using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace HIT_Campus_Housing_Portal
{
    public partial class Password : Form
    {
        public Password(string regNumber)
        {
            InitializeComponent();
            // Restructure the form for new password creation
            groupCurrent.Visible = false;
            lbl1 .Location = new Point(402, 189);
            lblUsername.Location = new Point(608, 189);
            groupCreate.Location = new Point(389, 260);
            lblUsername.Text = regNumber;  // Set the text of the label to the registration number entered in sign up form
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            SignUp register = (SignUp)Application.OpenForms["SignUp"]; // Get the sign up form
            string name = register.name; // Retrieve the name from the sign up form

            string pass = txtPassword.Text;
            string repeat = txtRepeat.Text;

            if (pass.ToString() != repeat.ToString())
            {
                // Show error if password entered and it's confirmation don't match
                MessageBox.Show("Your passwords do not match.", "Password mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Show a message box with the name of the user
                MessageBox.Show($"Welcome {name}!!! \n\r" + "You have successfully created your HIT CHP Account", "Account Created");

                Login login = new Login();
                login.Show(); // Show the login form
                this.Hide(); // Hide the current form
            }
        }

        private void btnForgot_Click(object sender, EventArgs e)
        {
            // Mechanism to reset one's password after forgetting
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
    }
}
