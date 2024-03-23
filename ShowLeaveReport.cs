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
    public partial class ShowLeaveReport : Form
    {
        //SqlConnection con = null;
        //string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        DataTable dtFillLeaveRpt =new DataTable();
        public ShowLeaveReport()
        {
            InitializeComponent();
        }

        private void ShowLeaveReport_Load(object sender, EventArgs e)
        {
            try
            {
                Home.con = new SqlConnection(Home.strcon);
                Home.con.Open();
                string cmdstr = "SELECT  LEAVEID, TXT_LEAVE_TYPE,TXT_SHIFT_TYPE,HOLIDAY_OR_WORKING_HRS,	STARTDATE,	ENDDATE,LEAVE_COMMENT  FROM LEAVE_RECORDS order by id desc";
                SqlCommand cmd = new SqlCommand(cmdstr, Home.con);
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                sd.Fill(dtFillLeaveRpt);
                sd.Dispose();
                if (dtFillLeaveRpt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dtFillLeaveRpt;
                }
                else
                {
                    MessageBox.Show("There is no data Present to show");
                    return;
                }



                Home.con.Close();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            Home openhome=  new Home();
            openhome.ShowDialog();
            this.Close();
        }
    }
}
