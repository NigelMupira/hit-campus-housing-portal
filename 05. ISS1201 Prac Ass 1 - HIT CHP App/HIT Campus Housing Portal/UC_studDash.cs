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
    public partial class UC_studDash : UserControl
    {
        private readonly StudentDash mainForm; // Create reference to the main form

        public UC_studDash(StudentDash mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm; // Set reference to the dashboard as the main form
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {
            // Show the status user control in the dashboard
            UC_Status status = new UC_Status(mainForm);
            mainForm.addUserControl(status);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            // Show the apply user control in the dashboard
            UC_Apply apply = new UC_Apply(mainForm);
            mainForm.addUserControl(apply);
        }
    }
}
