using System;
using System.Linq;
using System.Windows.Forms;
using AccommodationApp = HIT_Campus_Housing_Portal.Models.Application;
using HIT_Campus_Housing_Portal.Services;

namespace HIT_Campus_Housing_Portal
{
    public partial class UC_ManageAppli : UserControl
    {
        private readonly AdminDash mainForm;
        private readonly int applicationId;
        private readonly AdminService _adminService = new AdminService();
        private AccommodationApp currentApp;

        public UC_ManageAppli()
        {
            InitializeComponent();
        }

        public UC_ManageAppli(AdminDash mainForm, int appId)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.applicationId = appId;
        }

        private void UC_ManageAppli_Load(object sender, EventArgs e)
        {
            LoadApplicationDetails();
        }

        private void LoadApplicationDetails()
        {
            try
            {
                var apps = _adminService.GetApplications();
                currentApp = apps.FirstOrDefault(a => a.ApplicationId == applicationId);

                if (currentApp != null && lblTotal != null)
                {
                    lblTotal.Text = $"App #{currentApp.ApplicationId}: {currentApp.StudentName} ({currentApp.RegNumber})\nHostel: {currentApp.HostelName} - Pref: {currentApp.PreferredRoom1} / {currentApp.PreferredRoom2}\nStatus: {currentApp.Status}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load application details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentApp == null) return;
                string roomNumber = string.IsNullOrEmpty(currentApp.PreferredRoom1) ? "101" : currentApp.PreferredRoom1;

                bool success = _adminService.ApproveApplication(currentApp.ApplicationId, roomNumber, "Approved by Admin");
                if (success)
                {
                    MessageBox.Show("Application approved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UC_Applications apps = new UC_Applications(mainForm);
                    mainForm.addUserControl(apps);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error approving application: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentApp == null) return;
                bool success = _adminService.RejectApplication(currentApp.ApplicationId, "Rejected by Admin");
                if (success)
                {
                    MessageBox.Show("Application rejected.", "Application Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UC_Applications apps = new UC_Applications(mainForm);
                    mainForm.addUserControl(apps);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error rejecting application: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
