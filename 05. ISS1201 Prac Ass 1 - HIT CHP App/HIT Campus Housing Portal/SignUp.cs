using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIT_Campus_Housing_Portal
{
    public partial class SignUp : Form
    {
        public string name { get { return txtName.Text; } } // Get the name of the user

        public SignUp()
        {
            InitializeComponent();
        }

        private void SignUp_Load(object sender, EventArgs e)
        {
            // Populate the dropdowns
        }

        private void cbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Change the courses available depending on the department selected
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            /* {
            string firstName = txtName.Text; // Get the first name from the input field
            string lastName = txtSurname.Text; // Get the last name from the input field
            DateTime dateOfBirth = dateDOB.Value.Date; // Get the date of birth from the input field
            // Get the gender - if rbMale is checked then gender = male, else gender = female:
            string gender = radioMale.Checked ? "M" : "F";
            string nationalID = txtNatID.Text; // Get the national ID from the input field
            string part = cbPart.SelectedItem != null ? cbPart.SelectedItem.ToString() : string.Empty;
            // Get the school - if cbDepartment.SelectedItem is not null then school = selected option, else school = empty string:
            string school = cbDepartment.SelectedItem != null ? cbDepartment.SelectedItem.ToString() : string.Empty;
            // Get the course - if cbCourse.SelectedItem is not null then course = selected option, else course = empty string:
            string course = cbCourse.SelectedItem != null ? cbCourse.SelectedItem.ToString() : string.Empty;
            string phone = txtPhone.Text; // Get the phone number from the input field
            string email = txtEmail.Text; // Get the email from the input field
            string hitmail = txtHITmail.Text; // Get the hitmail from the input field
            string address = txtAddress.Text; // Get the address from the input field
            string guardianName = txtNoKName.Text; // Get the guardian's name from the input field
            string guardianSurname = txtNoKSurname.Text; //  Get the guardian's last name from the input field
            // // Get the relationship - if rbParent is checked then relationship = parent, else relationship = guardian:
            string relationship = radioParent.Checked ? "P" : "G";
            string guardianPhone = txtNoKPhone.Text; // Get the guardian's phone number from the input field
            string guardianEmail = txtNoKEmail.Text;// Get the guardian's email from the input field
            } */

            string regNumber = txtRegNum.Text; // Get the registration number from the input field

            Password password = new Password(regNumber);
            password.Show(); // Show the password form
            this.Hide(); // Hide the current form
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Clear out all input areas
            txtName.Clear();
            txtSurname.Clear();
            dateDOB.Value = DateTime.Now;
            radioMale.Checked = false;
            radioFemale.Checked = false;
            txtNatID.Clear();
            txtRegNum.Clear();
            cbPart.SelectedIndex = -1;
            cbDepartment.SelectedIndex = -1;
            cbCourse.SelectedIndex = -1;
            txtPhone.Clear();
            txtEmail.Clear();
            txtHITmail.Clear();
            txtAddress.Clear();
            txtNoKName.Clear();
            txtNoKSurname.Clear();
            radioParent.Checked = false;
            radioMale.Checked = false;
            txtNoKPhone.Clear();
            txtNoKEmail.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show(); // Show the home form
            this.Hide(); // Hide the current form
        }
    }
}
