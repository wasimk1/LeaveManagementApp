using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeaveManagementApp
{
    public partial class Home : Form
    {
        SqlConnection con = null;
        DataTable dtleavetype = new DataTable();
        DateTime start;
        DateTime end;
        public Home()
        {
            InitializeComponent();
        }

        

        private void Home_Load(object sender, EventArgs e)
        {
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                
                con = new SqlConnection(strcon);
                con.Open();

                BindLeaveType();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        public void BindLeaveType()
        {
            try
            {
                string cmdstr = "SELECT LEAVE_TYPE FROM LEAVE_TYPE";
                SqlCommand cmd = new SqlCommand(cmdstr, con);
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                sd.Fill(dtleavetype);
                sd.Dispose();

                DataRow row = dtleavetype.NewRow();
                row[0] = "Please Select";
                dtleavetype.Rows.InsertAt(row, 0);

                combxleavetype.DataSource = dtleavetype;
                combxleavetype.DisplayMember = "LEAVE_TYPE";
                combxleavetype.ValueMember = "LEAVE_TYPE";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
            
            
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string startDate = dateTimePicker1.Value.ToString("dd-MM-yyyy");
            // MessageBox.Show(startDate);
             start = DateTime.Parse(startDate);
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            string endDate = dateTimePicker1.Value.ToString("dd-MM-yyyy");
            //MessageBox.Show(endDate);
             end = DateTime.Parse(endDate);
            txtnoofdays.Text = (end-start).TotalDays.ToString();
        }
    }
}
