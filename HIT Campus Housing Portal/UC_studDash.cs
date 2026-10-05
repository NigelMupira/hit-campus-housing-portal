using System;
using System.Windows.Forms;
using AccommodationApp = HIT_Campus_Housing_Portal.Models.Application;
using HIT_Campus_Housing_Portal.Services;

namespace HIT_Campus_Housing_Portal
{
    public partial class UC_studDash : UserControl
    {
        private readonly StudentDash mainForm;
        private readonly StudentService _studentService = new StudentService();

        public UC_studDash(StudentDash mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void UC_studDash_Load(object sender, EventArgs e)
        {
            LoadStudentDashboard();
        }

        public void LoadStudentDashboard()
        {
            try
            {
                var stats = _studentService.GetRoomStats();
                if (lblTotal != null) lblTotal.Text = stats.RoomsLeft.ToString();
                if (lblBoys != null) lblBoys.Text = stats.MaleRoomsLeft.ToString();
                if (lblGirls != null) lblGirls.Text = stats.FemaleRoomsLeft.ToString();

                if (UserSession.CurrentStudent != null)
                {
                    AccommodationApp app = _studentService.GetStudentApplication(UserSession.CurrentStudent.StudentId);
                    if (lblStatus != null) lblStatus.Text = app != null ? app.Status : "Not Applied";
                }
            }
            catch
            {
                if (lblStatus != null) lblStatus.Text = "Not Applied";
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            UC_Apply apply = new UC_Apply(mainForm);
            mainForm.addUserControl(apply);
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {
            UC_Status status = new UC_Status(mainForm);
            mainForm.addUserControl(status);
        }
    }
}
