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
        string getleaveid = "";
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            Home openhome=  new Home();
            this.Hide();
            openhome.ShowDialog();
            this.Close();
        }
        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
             getleaveid = textBox1.Text.Trim().ToUpper();
            button1.Enabled = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dtFillLeaveRpt.Clear();
                if (!getleaveid.StartsWith("LEV"))
                {
                    MessageBox.Show("Please enterd the valid LEAVE ID");
                    textBox1.Text = "";
                    textBox1.Focus();
                    button2.Text = "Show Report";
                    return;
                }
                else
                {
                    string cmdstr = "SELECT  LEAVEID, TXT_LEAVE_TYPE [LEAVE TYPE],TXT_SHIFT_TYPE [SHIFT TYPE],HOLIDAY_OR_WORKING_HRS [HOLIDAY / WORKING HOURS],STARTDATE [FROM DATE],ENDDATE [TO DATE] ,LEAVE_COMMENT [LEAVE COMMENT] FROM LEAVE_RECORDS where LEAVEID='" + getleaveid + "'";
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
                        MessageBox.Show("Please enterd the valid LEAVE ID");
                        textBox1.Text = "";
                        textBox1.Focus();
                        return;
                    }
                    
                }
                
                Home.con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dtFillLeaveRpt.Clear();
                string cmdstr = "SELECT  LEAVEID, TXT_LEAVE_TYPE [LEAVE TYPE],TXT_SHIFT_TYPE [SHIFT TYPE],HOLIDAY_OR_WORKING_HRS [HOLIDAY / WORKING HOURS],STARTDATE [FROM DATE],ENDDATE [TO DATE] ,LEAVE_COMMENT [LEAVE COMMENT] FROM LEAVE_RECORDS order by id desc";
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
                    MessageBox.Show("There is no data present to show");
                    return;
                }
                button2.Text = "Referesh";
                Home.con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        
    }
}
