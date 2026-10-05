using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HIT_Campus_Housing_Portal.Models;
using HIT_Campus_Housing_Portal.Services;

namespace HIT_Campus_Housing_Portal
{
    public partial class SignUp : Form
    {
        private readonly StudentService _studentService = new StudentService();
        private List<School> _schools = new List<School>();
        private List<Department> _currentDepartments = new List<Department>();

        public SignUp()
        {
            InitializeComponent();
        }

        private void SignUp_Load(object sender, EventArgs e)
        {
            try
            {
                cbPart.Items.Clear();
                cbPart.Items.AddRange(new object[] { "1", "2", "3", "4" });
                cbPart.SelectedIndex = -1;

                _schools = _studentService.GetSchools();
                cbDepartment.DisplayMember = "SchoolCode";
                cbDepartment.ValueMember = "SchoolId";
                cbDepartment.DataSource = _schools;
                cbDepartment.SelectedIndex = -1;
                cbCourse.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load schools list: {ex.Message}", "Database Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDepartment.SelectedItem is School selectedSchool)
            {
                _currentDepartments = _studentService.GetDepartmentsBySchool(selectedSchool.SchoolId);
                cbCourse.DisplayMember = "DeptName";
                cbCourse.ValueMember = "DeptId";
                cbCourse.DataSource = _currentDepartments;
                cbCourse.SelectedIndex = -1;
            }
            else
            {
                cbCourse.DataSource = null;
            }
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
            cbCourse.DataSource = null;
            numPhone.Clear();
            txtMail.Clear();
            txtHITMail.Clear();
            txtAddress.Clear();
            txtGName.Clear();
            txtGSurname.Clear();
            radioParent.Checked = false;
            radioGuardian.Checked = false;
            numGPhone.Clear();
            txtGMail.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                string firstName = txtName.Text.Trim();
                string lastName = txtSurname.Text.Trim();
                DateTime dateOfBirth = dateDOB.Value.Date;
                string gender = radioMale.Checked ? "M" : (radioFemale.Checked ? "F" : string.Empty);
                string nationalID = txtNatID.Text.Trim();
                string regNumber = txtRegNum.Text.Trim();
                string partStr = cbPart.SelectedItem != null ? cbPart.SelectedItem.ToString() : string.Empty;
                Department selectedDept = cbCourse.SelectedItem as Department;
                string phone = numPhone.Text.Trim();
                string email = txtMail.Text.Trim();
                string hitmail = txtHITMail.Text.Trim();
                string address = txtAddress.Text.Trim();
                string guardianName = $"{txtGName.Text.Trim()} {txtGSurname.Text.Trim()}".Trim();
                string relationship = radioParent.Checked ? "P" : (radioGuardian.Checked ? "G" : string.Empty);
                string guardianPhone = numGPhone.Text.Trim();
                string guardianEmail = txtGMail.Text.Trim();

                if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
                    string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(nationalID) ||
                    string.IsNullOrEmpty(regNumber) || string.IsNullOrEmpty(partStr) ||
                    selectedDept == null || string.IsNullOrEmpty(phone) ||
                    string.IsNullOrEmpty(email) || string.IsNullOrEmpty(hitmail) ||
                    string.IsNullOrEmpty(address) || string.IsNullOrEmpty(guardianName) ||
                    string.IsNullOrEmpty(relationship) || string.IsNullOrEmpty(guardianPhone))
                {
                    MessageBox.Show("Please fill in all required fields before proceeding.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dateOfBirth >= DateTime.Now.Date)
                {
                    MessageBox.Show("Invalid Date of Birth. Date must be in the past.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Student newStudent = new Student
                {
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = dateOfBirth,
                    Gender = gender,
                    NationalID = nationalID,
                    RegNumber = regNumber,
                    Part = int.Parse(partStr),
                    DeptId = selectedDept.DeptId,
                    DeptCode = selectedDept.DeptCode,
                    DeptName = selectedDept.DeptName,
                    SchoolCode = selectedDept.SchoolCode,
                    Phone = phone,
                    Email = email,
                    HITMail = hitmail,
                    Address = address,
                    GuardianName = guardianName,
                    GuardianPhone = guardianPhone,
                    GuardianEmail = guardianEmail,
                    GuardianRelationship = relationship
                };

                // Pass student profile to Password creation form
                Password passwordForm = new Password(newStudent);
                passwordForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing sign up: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
