using System;
using System.Drawing;
using System.Windows.Forms;
using HIT_Campus_Housing_Portal.Models;
using HIT_Campus_Housing_Portal.Services;

namespace HIT_Campus_Housing_Portal
{
    public partial class Password : Form
    {
        private readonly AuthService _authService = new AuthService();
        private readonly Student _studentModel;

        public Password(Student student)
        {
            InitializeComponent();
            _studentModel = student;

            if (groupBox1 != null) groupBox1.Visible = false;
            label1.Location = new Point(402, 189);
            lblUser.Location = new Point(608, 189);
            groupBox2.Location = new Point(389, 260);

            lblUser.Text = student?.RegNumber ?? string.Empty;
        }

        public Password(string regNumber)
        {
            InitializeComponent();
            if (groupBox1 != null) groupBox1.Visible = false;
            lblUser.Text = regNumber;
        }

        private void btnForgot_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please contact system administration to reset your password.", "Password Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            SignUp register = new SignUp();
            register.Show();
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                string pass = txtPassword.Text;
                string repeat = txtRepeat.Text;

                if (string.IsNullOrEmpty(pass))
                {
                    MessageBox.Show("Please enter a password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (pass != repeat)
                {
                    MessageBox.Show("Your passwords do not match.", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (_studentModel != null)
                {
                    bool success = _authService.RegisterStudent(_studentModel, pass, out string errorMsg);
                    if (success)
                    {
                        MessageBox.Show($"Welcome {_studentModel.FirstName}!\n\rYou have successfully created your HIT CHP Account.", "Account Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Login login = new Login();
                        login.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show(errorMsg, "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Student profile information missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating account: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
