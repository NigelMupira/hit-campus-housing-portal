using System;
using System.Windows.Forms;
using AccommodationApp = HIT_Campus_Housing_Portal.Models.Application;
using HIT_Campus_Housing_Portal.Services;

namespace HIT_Campus_Housing_Portal
{
    public partial class UC_Status : UserControl
    {
        private readonly StudentDash mainForm;
        private readonly StudentService _studentService = new StudentService();

        public UC_Status(StudentDash mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void UC_Status_Load(object sender, EventArgs e)
        {
            LoadStatusDetails();
        }

        public void LoadStatusDetails()
        {
            try
            {
                var stats = _studentService.GetRoomStats();
                if (lblTotal != null) lblTotal.Text = stats.RoomsLeft.ToString();

                if (UserSession.CurrentStudent != null)
                {
                    AccommodationApp app = _studentService.GetStudentApplication(UserSession.CurrentStudent.StudentId);
                    if (app != null)
                    {
                        if (lblStatus != null) lblStatus.Text = app.Status;
                        if (lblDateApply != null)
                        {
                            lblDateApply.Text = app.CreatedAt.ToShortDateString();
                            lblDateApply.Visible = true;
                        }
                    }
                    else
                    {
                        if (lblStatus != null) lblStatus.Text = "Not Applied";
                    }
                }
            }
            catch
            {
                if (lblStatus != null) lblStatus.Text = "N/A";
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            UC_Apply apply = new UC_Apply(mainForm);
            mainForm.addUserControl(apply);
        }
    }
}
