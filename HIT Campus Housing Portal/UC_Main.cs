using System;
using System.Data;
using System.Windows.Forms;
using AccommodationApp = HIT_Campus_Housing_Portal.Models.Application;
using HIT_Campus_Housing_Portal.Services;

namespace HIT_Campus_Housing_Portal
{
    public partial class UC_Main : UserControl
    {
        private readonly StudentDash mainForm;
        private readonly StudentService _studentService = new StudentService();

        public UC_Main(StudentDash mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void UC_Main_Load(object sender, EventArgs e)
        {
            LoadHostels();
        }

        private void LoadHostels()
        {
            try
            {
                string gender = UserSession.CurrentStudent?.Gender ?? "M";
                DataTable dtHostels = _studentService.GetHostelsByGender(gender);

                if (cbHostel != null)
                {
                    cbHostel.DisplayMember = "hostel_name";
                    cbHostel.ValueMember = "hostel_id";
                    cbHostel.DataSource = dtHostels;
                    cbHostel.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load hostels list: {ex.Message}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            UC_Apply apply = new UC_Apply(mainForm);
            mainForm.addUserControl(apply);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (cbHostel != null) cbHostel.SelectedIndex = -1;
            if (numRoom1 != null) numRoom1.Clear();
            if (numRoom2 != null) numRoom2.Clear();
            if (txtReason != null) txtReason.Clear();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (UserSession.CurrentStudent == null)
                {
                    MessageBox.Show("Please log in as a student to apply.", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (cbHostel != null && cbHostel.SelectedValue == null)
                {
                    MessageBox.Show("Please select a hostel.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int hostelId = cbHostel != null && cbHostel.SelectedValue != null ? Convert.ToInt32(cbHostel.SelectedValue) : 1;
                string pref1 = numRoom1 != null ? numRoom1.Text.Trim() : string.Empty;
                string pref2 = numRoom2 != null ? numRoom2.Text.Trim() : string.Empty;
                string reason = txtReason != null ? txtReason.Text.Trim() : string.Empty;

                if (string.IsNullOrEmpty(pref1))
                {
                    MessageBox.Show("Please specify at least 1st room preference.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                AccommodationApp app = new AccommodationApp
                {
                    StudentId = UserSession.CurrentStudent.StudentId,
                    HostelId = hostelId,
                    PreferredRoom1 = pref1,
                    PreferredRoom2 = pref2,
                    Reason = reason
                };

                bool success = _studentService.SubmitApplication(app, out string errorMsg);
                if (success)
                {
                    MessageBox.Show("Application Submitted Successfully!", "Application Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UC_studDash uc = new UC_studDash(mainForm);
                    mainForm.addUserControl(uc);
                }
                else
                {
                    MessageBox.Show(errorMsg, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error submitting application: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
