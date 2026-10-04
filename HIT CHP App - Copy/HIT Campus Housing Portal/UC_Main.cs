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
    public partial class UC_Main : UserControl
    {
        private readonly StudentDash mainForm; // Reference to the main form

        public UC_Main(StudentDash mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm; // Set the reference to the main form
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Show the apply user control in student dashboard
            UC_Apply apply = new UC_Apply(mainForm);
            mainForm.addUserControl(apply);

        }

        private void btnReset_Click(object sender, EventArgs e)
        {

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            // Inform the user they have applied for a place successfully
            MessageBox.Show("Application Submitted Successfully", "Application Status", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Show the student dashboard user control
            UC_studDash uc = new UC_studDash(mainForm);
            mainForm.addUserControl(uc);
        }
    }
}
