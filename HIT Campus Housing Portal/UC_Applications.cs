using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIT_Campus_Housing_Portal
{
    public partial class UC_Applications : UserControl
    {
        private readonly AdminDash mainForm;

        public UC_Applications(AdminDash mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            UC_ManageAppli manage = new UC_ManageAppli();
            mainForm.addUserControl(manage);
        }
    }
}
