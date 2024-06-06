using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeaveManagementApp
{
    public partial class AdminReport : Form
    {
        public AdminReport()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string takedate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            MessageBox.Show(takedate);
        }
    }
}
