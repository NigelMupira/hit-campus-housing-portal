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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace HIT_Campus_Housing_Portal
{
    public partial class SignUp : Form
    {
        private readonly HITCHPAppEntities _db = new HITCHPAppEntities();
        public string name { get { return txtName.Text; } } // Get the name of the user

        public SignUp()
        {
            InitializeComponent();
        }

        private void SignUp_Load(object sender, EventArgs e)
        {
            // Load a list of academic years from the database to a dropdown list
            var academicYear = _db.Departments.ToList(); // Get the list of schools from the database
            cbPart.DisplayMember = "Part"; // Display the part
            cbPart.ValueMember = "Part"; // Store the part
            cbPart.DataSource = academicYear; // Bind the data source to the combobox
            cbPart.SelectedIndex = -1; // Empty the combobox text

            // Load a list of schools from the database to a dropdown list
            var school = _db.Departments.ToList(); // Get the list of schools from the database
            cbDepartment.DisplayMember = "Schools"; // Display the school name
            cbDepartment.ValueMember = "Schools"; // Store the school name
            cbDepartment.DataSource = school; // Bind the data source to the combobox
            cbDepartment.SelectedIndex = -1; // Empty the combobox text
        }

        private void cbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Load a list of courses from the database to a dropdown list
            switch (cbDepartment.SelectedIndex)
            {
                // If the selected school is SIET then load the list of courses from the that department
                case 0:
                    var dptSIET = _db.Departments.ToList();
                    cbCourse.DisplayMember = "SIETdepartment";
                    cbCourse.ValueMember = "SIETdepartment";
                    cbCourse.DataSource = dptSIET;
                    break;

                // If the selected school is SIST then load the list of courses from the that department
                case 1:
                    var dptSIST = _db.Departments.ToList();
                    cbCourse.DisplayMember = "SISTdepartment";
                    cbCourse.ValueMember = "SISTdepartment";
                    cbCourse.DataSource = dptSIST;
                    break;

                // If the selected school is SAHS then load the list of courses from the that department
                case 2:
                    var dptSAHS = _db.Departments.ToList();
                    cbCourse.DisplayMember = "SAHSdepartment";
                    cbCourse.ValueMember = "SAHSdepartment";
                    cbCourse.DataSource = dptSAHS;
                    break;

                // If the selected school is SBMS then load the list of courses from the that department
                case 3:
                    var dptSBMS = _db.Departments.ToList();
                    cbCourse.DisplayMember = "SBMSdepartment";
                    cbCourse.ValueMember = "SBMSdepartment";
                    cbCourse.DataSource = dptSBMS;
                    break;

                // If the selected school is SIIT then load the list of courses from the that department
                case 4:
                    var dptSIIT = _db.Departments.ToList();
                    cbCourse.DisplayMember = "SIITdepartment";
                    cbCourse.ValueMember = "SIITdepartment";
                    cbCourse.DataSource = dptSIIT;
                    break;

                default:
                    // If no school is selected then empty the course dropdown list
                    cbCourse.DataSource = null;
                    cbCourse.Items.Add("Select Department First");
                    break;
            }
            cbCourse.SelectedIndex = -1;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show(); // Show the home form
            this.Hide(); // Hide the current form
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
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
            numPhone.Clear();
            txtMail.Clear();
            txtHITMail.Clear();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                string firstName = txtName.Text; // Get the first name from the input field
                string lastName = txtSurname.Text; // Get the last name from the input field
                DateTime dateOfBirth = dateDOB.Value.Date; // Get the date of birth from the input field
                // If rbMale is checked then gender = male, else gender = female
                string gender = radioMale.Checked ? "M" : "F";
                string nationalID = txtNatID.Text; // Get the national ID from the input field
                string regNumber = txtRegNum.Text; // Get the registration number from the input field
                string part = cbPart.SelectedItem != null ? cbPart.SelectedItem.ToString() : string.Empty;
                // If cbSchool.SelectedItem is not null then school = selected option, else school = empty string
                string school = cbDepartment.SelectedItem != null ? cbDepartment.SelectedItem.ToString() : string.Empty;
                // If cbCourse.SelectedItem is not null then course = selected option, else course = empty string
                string course = cbCourse.SelectedItem != null ? cbCourse.SelectedItem.ToString() : string.Empty;
                string phone = numPhone.Text; // Get the phone number from the input field
                string email = txtMail.Text; // Get the email from the input field
                string hitmail = txtHITMail.Text; // Get the hitmail from the input field
                string address = txtAddress.Text; // Get the address from the input
                string guardianName = txtGName.Text;
                string guardianSurname = txtGSurname.Text;
                string relationship = radioParent.Checked ? "P" : "G";
                string guardianPhone = numGPhone.Text;
                string guardianEmail = txtGMail.Text;

                var isValid = true; // Flag to check if the input data is valid
                // Flag to check if any of the input fields are empty
                var emptyFields = string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(nationalID) || string.IsNullOrWhiteSpace(regNumber) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(hitmail) || string.IsNullOrEmpty(address) || string.IsNullOrWhiteSpace(guardianName) || string.IsNullOrWhiteSpace(guardianSurname) || string.IsNullOrWhiteSpace(guardianPhone) || string.IsNullOrWhiteSpace(guardianEmail);
                // Flag to check if the email, hitmail and next of kin's email are entered
                var emailEntered = !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(hitmail) && !string.IsNullOrEmpty(guardianEmail);
                // Flag to check if the email, hitmail and next of kin's email are valid
                var emailValid = email.Contains("@") && hitmail.Contains("@hit.ac.zw") && guardianEmail.Contains("@");

                var errorMessage = string.Empty; // Error message to display if the input data is invalid

                if (emptyFields == true)
                {
                    // If any of the input fields are empty then
                    isValid = false; // raise the flag to false
                    errorMessage += "EHIT001: Please fill in all fields!\n"; // and display an error message
                }
                else // Else if specific fields are empty then display the corresponding error message
                {
                    if ((radioMale.Checked || radioFemale.Checked) == false)
                    {
                        // If no gender is selected then
                        isValid = false; // raise the flag to false
                        errorMessage += "EHIT002: Please select a gender.\n"; // and display an error message
                    }

                    if ((radioParent.Checked || radioGuardian.Checked) == false)
                    {
                        // If no relationship is selected then
                        isValid = false; // raise the flag to false
                        errorMessage += "EHIT003: Please select a gender.\n"; // and display an error message
                    }

                    if (string.IsNullOrEmpty(school) || string.IsNullOrEmpty(course))
                    {
                        // If no school or course is selected then
                        isValid = false; // raise the flag to false
                        errorMessage += "EHIT005: Please select your school and program.\n"; // and display an error message
                    }
                }

                if (dateOfBirth > DateTime.Now.Date)
                {
                    // If the date of birth is greater than the current date then
                    isValid = false; // raise the flag to false
                    errorMessage += "EHIT004: Invalid date of birth!\n"; // and display an error message
                }

                try // Try to parse the phone number to an integer
                {
                    long.Parse(phone);
                    long.Parse(guardianPhone);
                }
                catch (FormatException) // Catch the exception if the phone number is not an integer
                {
                    isValid = false; // Raise the isValid flag to false
                    errorMessage += "EHIT006: Invalid phone number! Must only contain digits.\n"; // Display an error message
                }

                if (phone.Length != 12 || guardianPhone.Length != 12)
                {
                    // If the phone number is not 12 digits long then
                    isValid = false; // raise the flag to false
                    errorMessage += "EHIT007: Invalid phone number!\n"; // and display an error message
                }

                if (emailEntered == true && emailValid == false)
                {
                    // If email or hitmail is not empty but invalid then
                    isValid = false; // raise the flag to false
                    errorMessage += "EHIT008: Invalid Email or HITmail!\n"; // and display an error message
                }

                // Check if the name and registration number are empty
                if (firstName == "" || regNumber == "")
                {
                    isValid = false; // raise the flag to false
                    MessageBox.Show("Please enter missing data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Show an error message
                }

                if (isValid)
                {
                    // If the the form is filled and no errors are raised then save the input to the database

                    // Create a new student record
                    var studentRecord = new StudentRecord
                    {
                        LastName = lastName, // Record the last name
                        FirstName = firstName, // Record the first name
                        DateOfBirth = dateOfBirth, // Record the date of birth
                        Gender = gender, // Record the date of birth
                        NationalID = nationalID, // Record the national ID
                        RegNumber = regNumber, // Record the registration number
                        Part = (string)cbPart.SelectedValue, // Record the part
                        School = (string)cbDepartment.SelectedValue, // Record the school
                        Course = (string)cbCourse.SelectedValue, // Record the course
                        Phone = phone, // Record the phone number
                        Email = email, // Record the email
                        HITmail = hitmail, // Record the hitmail
                        Address = address // Record the address
                    };
                    _db.StudentRecords.Add(studentRecord); // Add the student record to the database
                    _db.SaveChanges(); // Save the changes to the database

                    /*
                    // Record next of kin details
                    var nextOfKin = new NextOfKin
                    {
                        LastName = guardianSurname,
                        FirstName = guardianName,
                        Relationship = relationship,
                        Phone = guardianPhone,
                        Email = guardianEmail
                    };
                    _db.NextOfKin.Add(nextOfKin);
                    _db.SaveChanges();

                    // Record their student details to the specific school table
                    if (cbDepartment.SelectedIndex == 0)
                    {
                        // If the selected school is SIET then record to the corresponding table
                        var SIETstudent = new SIETdepartment
                        {
                            StudentID = studentRecord.StudentID,
                            LastName = lastName,
                            RegNumber = regNumber,
                            Part = part,
                            Course = (string)cbCourse.SelectedValue,
                            Email = email,
                            HITmail = hitmail
                        };
                        _db.SIETdepartment.Add(SIETstudent);
                    }
                    else if (cbDepartment.SelectedIndex == 1)
                    {
                        // If the selected school is SIST then record to the corresponding table
                        var SISTstudent = new SISTdepartment
                        {
                            StudentID = studentRecord.StudentID,
                            LastName = lastName,
                            RegNumber = regNumber,
                            Part = part,
                            Course = (string)cbCourse.SelectedValue,
                            Email = email,
                            HITmail = hitmail
                        };
                        _db.SISTdepartment.Add(SISTstudent);
                    }
                    else if (cbDepartment.SelectedIndex == 2)
                    {
                        // If the selected school is SAHS then record to the corresponding table
                        var SAHSstudent = new SAHSdepartment
                        {
                            StudentID = studentRecord.StudentID,
                            LastName = lastName,
                            RegNumber = regNumber,
                            Part = part,
                            Course = (string)cbCourse.SelectedValue,
                            Email = email,
                            HITmail = hitmail
                        };
                        _db.SAHSdepartment.Add(SAHSstudent);
                    }
                    else if (cbDepartment.SelectedIndex == 3)
                    {
                        // If the selected school is SBMS then record to the corresponding table
                        var SBMSstudent = new SBMSdepartment
                        {
                            StudentID = studentRecord.StudentID,
                            LastName = lastName,
                            RegNumber = regNumber,
                            Part = part,
                            Course = (string)cbCourse.SelectedValue,
                            Email = email,
                            HITmail = hitmail
                        };
                        _db.SBMSdepartment.Add(SBMSstudent);
                    }
                    else if (cbDepartment.SelectedIndex == 4)
                    {
                        // If the selected school is SIIT then record to the corresponding table
                        var SIITstudent = new SIITdepartment
                        {
                            StudentID = studentRecord.StudentID,
                            LastName = lastName,
                            RegNumber = regNumber,
                            Part = part,
                            Course = (string)cbCourse.SelectedValue,
                            Email = email,
                            HITmail = hitmail
                        };
                        _db.SIITdepartment.Add(SIITstudent);
                    }
                    _db.SaveChanges(); // Save the changes to the database
                    */

                    Password password = new Password(regNumber); // Create a new password form and pass the registration number as an argument
                    password.Show(); // Show the password form
                    this.Hide(); // Hide the current form
                }
                else 
                {
                    // If the input data is invalid then display the corresponding error message
                    MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
