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
    public partial class UC_Apply : UserControl
    {
        private readonly StudentDash mainForm; // Create reference to the main form

        public UC_Apply(StudentDash mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm; // Set reference to the dashboard as the main form
            panelApplied.Visible = false; // Hide the info panel
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            // Show the application form in dashboard
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            // Show the status user control in dashboard
            UC_Status status = new UC_Status(mainForm);
            mainForm.addUserControl(status);
        }
    }
}
