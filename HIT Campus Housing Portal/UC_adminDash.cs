using System;
using System.Windows.Forms;
using HIT_Campus_Housing_Portal.Services;

namespace HIT_Campus_Housing_Portal
{
    public partial class UC_adminDash : UserControl
    {
        private readonly AdminService _adminService = new AdminService();

        public UC_adminDash()
        {
            InitializeComponent();
        }

        private void UC_adminDash_Load(object sender, EventArgs e)
        {
            LoadAdminDashboard();
        }

        public void LoadAdminDashboard()
        {
            try
            {
                var appStats = _adminService.GetApplicationStats();
                if (lblTotal != null) lblTotal.Text = appStats.TotalRequests.ToString();
                if (lblAccepted != null) lblAccepted.Text = appStats.ApprovedCount.ToString();
                if (lblDenied != null) lblDenied.Text = appStats.RejectedCount.ToString();
                if (lblPending != null) lblPending.Text = appStats.PendingCount.ToString();
            }
            catch
            {
                // Default gracefully if DB is initializing
            }
        }
    }
}
