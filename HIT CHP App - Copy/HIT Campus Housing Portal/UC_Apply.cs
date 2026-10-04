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
        private readonly StudentDash mainForm; // Reference to the main form

        public UC_Apply(StudentDash mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm; // Set the reference to the main form
            panel2.Visible = false; // Hide the info panel
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            // Show the application form user control in student dashboard
            UC_Main main = new UC_Main(mainForm);
            mainForm.addUserControl(main);
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            // Show the status user control in student dashboard
            UC_Status status = new UC_Status(mainForm);
            mainForm.addUserControl(status);
        }
    }
}
