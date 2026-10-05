using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HIT_Campus_Housing_Portal.Services;

namespace HIT_Campus_Housing_Portal
{
    public partial class UC_Students : UserControl
    {
        private readonly AdminService _adminService = new AdminService();

        public UC_Students()
        {
            InitializeComponent();
        }

        private void UC_Students_Load(object sender, EventArgs e)
        {
            LoadStudentData();
        }

        public void LoadStudentData()
        {
            try
            {
                var students = _adminService.GetAllStudents().Select(s => new
                {
                    ID = s.StudentId,
                    RegNum = s.RegNumber,
                    Name = s.FirstName,
                    Surname = s.LastName,
                    DOB = s.DateOfBirth.ToShortDateString(),
                    Sex = s.Gender,
                    NatID = s.NationalID,
                    Part = s.Part,
                    Dept = s.DeptCode,
                    Program = s.DeptName,
                    Phone = s.Phone,
                    Email = s.Email,
                    HITmail = s.HITMail,
                    Address = s.Address
                }).ToList();

                dgvStudentRecords.DataSource = students;

                if (dgvStudentRecords.Columns.Count > 0)
                {
                    dgvStudentRecords.Columns[0].HeaderText = "#";
                    dgvStudentRecords.Columns[1].HeaderText = "REG NUMBER";
                    dgvStudentRecords.Columns[2].HeaderText = "FIRST NAME";
                    dgvStudentRecords.Columns[3].HeaderText = "LAST NAME";
                    dgvStudentRecords.Columns[4].HeaderText = "D.O.B.";
                    dgvStudentRecords.Columns[5].HeaderText = "SEX";
                    dgvStudentRecords.Columns[6].HeaderText = "NATIONAL ID";
                    dgvStudentRecords.Columns[7].HeaderText = "PART";
                    dgvStudentRecords.Columns[8].HeaderText = "DEPARTMENT";
                    dgvStudentRecords.Columns[9].HeaderText = "PROGRAM";
                    dgvStudentRecords.Columns[10].HeaderText = "PHONE";
                    dgvStudentRecords.Columns[11].HeaderText = "E-MAIL";
                    dgvStudentRecords.Columns[12].HeaderText = "HIT-MAIL";
                    dgvStudentRecords.Columns[13].HeaderText = "ADDRESS";

                    dgvStudentRecords.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgvStudentRecords.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                    dgvStudentRecords.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgvStudentRecords.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load student directory: {ex.Message}", "Database Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnManage_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Student account management active.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (dgvStudentRecords.CurrentRow != null)
            {
                string reg = dgvStudentRecords.CurrentRow.Cells["RegNum"].Value.ToString();
                string name = dgvStudentRecords.CurrentRow.Cells["Name"].Value.ToString();
                string surname = dgvStudentRecords.CurrentRow.Cells["Surname"].Value.ToString();
                MessageBox.Show($"Selected Student:\nName: {name} {surname}\nReg: {reg}", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
