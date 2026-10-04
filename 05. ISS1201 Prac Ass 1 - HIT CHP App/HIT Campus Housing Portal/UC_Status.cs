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
    public partial class UC_Status : UserControl
    {
        private readonly StudentDash mainForm; // Create reference to the main form

        public UC_Status(StudentDash mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm; // Set reference to the dashboard as the main form
            // Hide info of the date applied if user hasn't applied yet
            lbl2.Visible = false;
            lblDateApplied.Visible = false;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            // Show the apply user control in student dashboard
            UC_Apply apply = new UC_Apply(mainForm);
            mainForm.addUserControl(apply);
        }
    }
}
