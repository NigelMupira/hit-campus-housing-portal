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
    public partial class AdminDash : Form
    {
        public AdminDash()
        {
            InitializeComponent();
            // Show the admin dashboard user control on form load
            UC_adminDash uc = new UC_adminDash();
            addUserControl(uc);
        }

        public void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill; // Fill the panel with the user control
            panelDisplay.Controls.Clear(); // Clear the panel before adding the user control
            panelDisplay.Controls.Add(userControl); // Add the user control to the panel
            userControl.BringToFront(); // Bring the user control to the front
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            UC_adminDash uc = new UC_adminDash();
            addUserControl(uc);
        }

        private void btnApplications_Click(object sender, EventArgs e)
        {
            UC_Applications uc = new UC_Applications(this);
            addUserControl(uc);
        }

        private void btnStudents_Click(object sender, EventArgs e)
        {
            UC_Students uc = new UC_Students();
            addUserControl(uc);
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            UC_studAcc uc = new UC_studAcc();
            addUserControl(uc);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            UC_studSett uc = new UC_studSett();
            addUserControl(uc);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Prompt user confirmation
            DialogResult dialogResult = MessageBox.Show("You are about to Sign Out of HIT CHP.\n\r"
                + "Any unsaved work with be discarded.\n\r\n\r"
                + "Are you sure you want to Logout?",
                "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            // If user confirms sign out, return to home form
            if (dialogResult == DialogResult.Yes)
            {
                Home home = new Home();
                home.Show(); // Show the home form
                this.Hide(); // Hide the current form
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Prompt user for confirmation
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit the application?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // If user confirms exit, close all forms and exit the application
            if (dialogResult == DialogResult.Yes)
            {
                // Create a temporary list to hold forms to close
                List<Form> formsToClose = new List<Form>();

                foreach (Form form in Application.OpenForms)
                {
                    if (form != this)
                    {
                        formsToClose.Add(form);
                    }
                }

                // Close the forms
                foreach (Form form in formsToClose)
                {
                    form.Close();
                }

                // Exit the application
                Application.Exit();
            }
        }
    }
}
