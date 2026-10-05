using System;
using System.Windows.Forms;
using HIT_Campus_Housing_Portal.Models;
using HIT_Campus_Housing_Portal.Services;

namespace HIT_Campus_Housing_Portal
{
    public partial class UC_studProf : UserControl
    {
        public UC_studProf()
        {
            InitializeComponent();
        }

        private void UC_studProf_Load(object sender, EventArgs e)
        {
            LoadUserProfile();
        }

        public void LoadUserProfile()
        {
            try
            {
                Student student = UserSession.CurrentStudent;
                if (student != null)
                {
                    lblName.Text = student.FullName;
                    lblDOB.Text = student.DateOfBirth.ToShortDateString();
                    lblGender.Text = student.Gender == "M" ? "Male" : "Female";
                    lblNatID.Text = student.NationalID;
                    lblRegNum.Text = student.RegNumber;
                    lblSchool.Text = student.SchoolCode;
                    lblCourse.Text = student.DeptName;
                    lblPhone.Text = student.Phone;
                    lblEmail.Text = student.Email;
                    lblHITmail.Text = student.HITMail;

                    lblNoKName.Text = student.GuardianName;
                    lblRelationship.Text = student.GuardianRelationship == "P" ? "Parent" : "Guardian";
                    lblNoKPhone.Text = student.GuardianPhone;
                    lblNoKEmail.Text = student.GuardianEmail;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user profile: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
