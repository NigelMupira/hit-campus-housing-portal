using System;
using System.Linq;
using System.Windows.Forms;
using AccommodationApp = HIT_Campus_Housing_Portal.Models.Application;
using HIT_Campus_Housing_Portal.Services;

namespace HIT_Campus_Housing_Portal
{
    public partial class UC_Applications : UserControl
    {
        private readonly AdminDash mainForm;
        private readonly AdminService _adminService = new AdminService();

        public UC_Applications(AdminDash mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void UC_Applications_Load(object sender, EventArgs e)
        {
            LoadApplications();
        }

        public void LoadApplications()
        {
            try
            {
                var apps = _adminService.GetApplications().Select(a => new
                {
                    AppID = a.ApplicationId,
                    RegNum = a.RegNumber,
                    Student = a.StudentName,
                    Hostel = a.HostelName,
                    PrefRoom1 = a.PreferredRoom1,
                    PrefRoom2 = a.PreferredRoom2,
                    Status = a.Status,
                    AssignedRoom = a.AssignedRoomNumber,
                    AppliedDate = a.CreatedAt.ToShortDateString()
                }).ToList();

                if (dataGridView1 != null)
                {
                    dataGridView1.DataSource = apps;

                    if (dataGridView1.Columns.Count > 0)
                    {
                        dataGridView1.Columns[0].HeaderText = "APP ID";
                        dataGridView1.Columns[1].HeaderText = "REG NUMBER";
                        dataGridView1.Columns[2].HeaderText = "STUDENT NAME";
                        dataGridView1.Columns[3].HeaderText = "HOSTEL";
                        dataGridView1.Columns[4].HeaderText = "PREF ROOM 1";
                        dataGridView1.Columns[5].HeaderText = "PREF ROOM 2";
                        dataGridView1.Columns[6].HeaderText = "STATUS";
                        dataGridView1.Columns[7].HeaderText = "ASSIGNED ROOM";
                        dataGridView1.Columns[8].HeaderText = "APPLIED DATE";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading applications: {ex.Message}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (dataGridView1 != null && dataGridView1.CurrentRow != null)
            {
                int appId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["AppID"].Value);
                UC_ManageAppli manage = new UC_ManageAppli(mainForm, appId);
                mainForm.addUserControl(manage);
            }
            else
            {
                MessageBox.Show("Please select an application to manage.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
