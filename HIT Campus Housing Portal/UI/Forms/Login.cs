using System;
using System.Windows.Forms;
using HIT_Campus_Housing_Portal.Models;
using HIT_Campus_Housing_Portal.Services;

namespace HIT_Campus_Housing_Portal
{
    public partial class Login : Form
    {
        private readonly AuthService _authService = new AuthService();
        private bool isVisible = false;

        public Login()
        {
            InitializeComponent();
            btnShow.Visible = false;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            btnShow.Visible = txtPassword.Text.Length > 0;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (!isVisible)
            {
                txtPassword.UseSystemPasswordChar = false;
                isVisible = true;
                btnShow.BackgroundImage = Properties.Resources.hide;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
                isVisible = false;
                btnShow.BackgroundImage = Properties.Resources.appear1;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter login details to proceed.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                User user = _authService.Login(username, password, out Student student);

                if (user != null)
                {
                    UserSession.CurrentUser = user;
                    UserSession.CurrentStudent = student;

                    if (user.Role == "Student")
                    {
                        StudentDash studDash = new StudentDash();
                        studDash.Show();
                        this.Hide();
                    }
                    else if (user.Role == "Admin")
                    {
                        AdminDash adminDash = new AdminDash();
                        adminDash.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database or system error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
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
    }
}
