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
        private readonly StudentDash mainForm; // Reference to the main form

        public UC_Status(StudentDash mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm; // Set the reference to the main form
            label9.Visible = false; // Hide the label
            lblDateApply.Visible = false; // Hide the label
        }

        public static implicit operator UC_Status(UC_studDash v)
        {
            throw new NotImplementedException();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            // Show the apply user control in student dashboard
            UC_Apply apply = new UC_Apply(mainForm);
            mainForm.addUserControl(apply);
        }
    }
}
