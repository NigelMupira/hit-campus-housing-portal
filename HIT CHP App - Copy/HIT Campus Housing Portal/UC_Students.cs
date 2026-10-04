using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIT_Campus_Housing_Portal
{
    public partial class UC_Students : UserControl
    {
        private readonly HITCHPAppEntities _db = new HITCHPAppEntities();
        public UC_Students()
        {
            InitializeComponent();
        }

        private void UC_Students_Load(object sender, EventArgs e)
        {
            /* SQL command:
            * SELECT
            *  StudentID AS ID,
            *  FirstName AS Name,
            *  LastName AS Surname,
            *  DateOfBirth AS DOB,
            *  Gender AS Sex,
            *  NationalID AS NatID,
            *  RegNumber AS RegNum,
            *  School AS Dept,
            *  Course AS Program,
            *  Phone,
            *  Email,
            *  HITmail
            * FROM StudentRecords
           */
            // This is to exclude the unselected columns from the DataGridView
            var students = _db.StudentRecords.Select(q => new
            {
                ID = q.StudentID,
                Name = q.FirstName,
                Surname = q.LastName,
                DOB = q.DateOfBirth,
                Sex = q.Gender,
                NatID = q.NationalID,
                RegNum = q.RegNumber,
                Part = q.Part,
                Dept = q.School,
                Program = q.Course,
                q.Phone,
                q.Email,
                q.HITmail,
                q.Address
            })
                .ToList();
            // Display the selected columns in the DataGridView
            dgvStudentRecords.DataSource = students;

            // Rename the DataGridView columns
            dgvStudentRecords.Columns[0].HeaderText = "#";
            dgvStudentRecords.Columns[1].HeaderText = "FIRST NAME";
            dgvStudentRecords.Columns[2].HeaderText = "LAST NAME";
            dgvStudentRecords.Columns[3].HeaderText = "D.O.B.";
            dgvStudentRecords.Columns[4].HeaderText = "SEX";
            dgvStudentRecords.Columns[5].HeaderText = "NATIONAL ID";
            dgvStudentRecords.Columns[6].HeaderText = "REG NUMBER";
            dgvStudentRecords.Columns[7].HeaderText = "PART";
            dgvStudentRecords.Columns[8].HeaderText = "DEPARTMENT";
            dgvStudentRecords.Columns[9].HeaderText = "PROGRAM";
            dgvStudentRecords.Columns[10].HeaderText = "PHONE";
            dgvStudentRecords.Columns[11].HeaderText = "E-MAIL";
            dgvStudentRecords.Columns[12].HeaderText = "HIT-MAIL";
            dgvStudentRecords.Columns[13].HeaderText = "ADDRESS";

            // Resize the DataGridView columns to fit the content and space available
            dgvStudentRecords.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvStudentRecords.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvStudentRecords.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvStudentRecords.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvStudentRecords.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvStudentRecords.Columns[12].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void btnManage_Click(object sender, EventArgs e)
        {
            MessageBox.Show("UNDER CONSTRUCTION!!!", "Endpoint", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            MessageBox.Show("UNDER CONSTRUCTION!!!", "Endpoint", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
