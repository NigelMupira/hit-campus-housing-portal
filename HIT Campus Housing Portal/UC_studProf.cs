using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace HIT_Campus_Housing_Portal
{
    public partial class UC_studProf : UserControl
    {
        private readonly HITCHPAppEntities _db = new HITCHPAppEntities(); // Create an instance of the database context
        private StudentRecord _userProfile; // Store the user's profile information
        private StudentRecord _nextOfKin; // Store the user's next of kin information

        public UC_studProf()
        {
            InitializeComponent();
            //LoadUserProfile(username);
        }
        /*
        // Method to load the user's profile
        public void LoadUserProfile( string username)
        {
            try
            {
                // Retrieve the user's profile based on the username
                _userProfile = _db.StudentRecords.FirstOrDefault(u => u.RegNumber == username); // RegNumber is the unique identifier for a user
                _nextOfKin = _db.NextOfKin.FirstOrDefault(n => n.StudentID == _userProfile.StudentID); // StudentID is the unique identifier for a user in the NextOfKin table

                if (_userProfile != null)
                {
                    // Populate the labels with the user's information
                    lblName.Text = _userProfile.FirstName + " " + _userProfile.LastName;
                    lblDOB.Text = _userProfile.DateOfBirth.ToShortDateString();
                    lblGender.Text = _userProfile.Gender;
                    lblNatID.Text = _userProfile.NationalID;
                    lblRegNum.Text = _userProfile.RegNumber;
                    lblSchool.Text = _userProfile.School;
                    lblCourse.Text = _userProfile.Course;
                    lblPhone.Text = _userProfile.Phone;
                    lblEmail.Text = _userProfile.Email;
                    lblHITmail.Text = _userProfile.HITmail;

                    // If you have labels for next of kin information, you can populate them here
                    if (_nextOfKin != null)
                    {
                        lblNoKName.Text = _nextOfKin.FirstName + " " + _nextOfKin.LastName;
                        lblRelationship.Text = _nextOfKin.Relationship;
                        lblNoKPhone.Text = _nextOfKin.Phone;
                        lblNoKEmail.Text = _nextOfKin.Email;
                    }
                }
                else
                {
                    // If the user is not found display an error message
                    MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user profile: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        */

    }
}
