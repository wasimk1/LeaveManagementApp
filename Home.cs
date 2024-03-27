using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeaveManagementApp
{
    public partial class Home : Form
    {
        public static SqlConnection con = null;
        DataTable dtleavetype = new DataTable();
        DataTable dtshiftmode = new DataTable();
        DataTable dtleavegen= new DataTable();  
        public static string startDate, endDate;
        public static int leaveid;
        
        public static string strcon = ConfigurationManager.ConnectionStrings["constr-PAVILION-NB"].ConnectionString;
        
        public Home()
        {
            InitializeComponent();
        }
        private void Home_Load(object sender, EventArgs e)
        {
            try
            {
                if (Environment.MachineName == "PROBOOK") //For office DB connection string
                {
                    strcon = ConfigurationManager.ConnectionStrings["constr-PROBOOK"].ConnectionString;
                }
                
                con = new SqlConnection(strcon);
                con.Open();
                BindLeaveType();
                BindShiftMode();
                this.FormBorderStyle = FormBorderStyle.FixedToolWindow;

            }
            catch (Exception)
            {
                MessageBox.Show("Server is not reachable at the moment, All Servers are buzy");
                return;
            }
            
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

                //DataRow row = dtleavetype.NewRow();
                //row[0] = "Please Select";
                //dtleavetype.Rows.InsertAt(row, 0);

                combxleavetype.DataSource = dtleavetype;
                combxleavetype.DisplayMember = "LEAVE_TYPE";
                combxleavetype.ValueMember = "LEAVE_TYPE";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        public void BindShiftMode()
        {
            string cmdstr = "SELECT MODE,MODE_VALUE FROM DAY_SHIFT";
            SqlCommand cmd = new SqlCommand(cmdstr, con);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.Fill(dtshiftmode);
            sd.Dispose();

            //DataRow row1 = dtshiftmode.NewRow();
            //row1[0] = "Please Select";
            //dtshiftmode.Rows.InsertAt(row1, 0);

            cmBxshiftmode.DataSource = dtshiftmode;
            cmBxshiftmode.DisplayMember = "MODE";
            cmBxshiftmode.ValueMember = "MODE_VALUE";
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (checkBox1.Checked == true)
            //    {
            //        cmBxshiftmode.Enabled = true;
                    
            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message);
            //}
        }

        private void dateTimePicker1_Validating(object sender, CancelEventArgs e)
        {
            startDate = dateTimePicker1.Value.ToString("dd/MM/yyyy");
        }

        private void dateTimePicker2_Validating(object sender, CancelEventArgs e)
        {
            endDate = dateTimePicker2.Value.ToString("dd/MM/yyyy");
            var totaldays = Convert.ToDateTime(endDate) - Convert.ToDateTime(startDate);
            txtnoofdays.Text = ((totaldays.TotalDays) + 1).ToString();
            if (Convert.ToInt32(txtnoofdays.Text) == 1)
            {
                label6.Text = "Total Day";
                checkBox1.Checked = true;
                checkBox1.Enabled = false;
                cmBxshiftmode.Enabled = true;
            }
            else if(Convert.ToInt32(txtnoofdays.Text) > 1)
            {
                label6.Text = "Total Days";
                //cmBxshiftmode.Text = "";
                checkBox1.Checked = false;
                checkBox1.Enabled = false;
                cmBxshiftmode.Enabled = false;
                
                
            }
            
        }

        private void combxleavetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string getval = combxleavetype.SelectedValue.ToString();
            //if (getval == "Please Select")
            //{
            //    MessageBox.Show("Please select Leave type");
            //    combxleavetype.Focus();
            //    return;
               
            //}
        }

        private void btnapplyleave_Click(object sender, EventArgs e)
        {
            try
            {
                string getLeaveType = combxleavetype.SelectedValue.ToString();
                string getShiftType = cmBxshiftmode.SelectedValue.ToString();
                if (checkBox1.Checked == true)
                {
                    if (getShiftType == "4.5")
                    {
                        if (cmBxshiftmode.SelectedText == "FIRST HALF")
                        {
                            getShiftType = "FIRST HALF";
                        }
                        else
                        {
                            getShiftType = "SECOND HALF";
                        }

                    }
                    else
                    {
                        getShiftType = "FULL DAY";
                    }
                    //getShiftType = "Total working hours " + txtnoofdays.Text;

                }
                else
                {
                    //getShiftType = "Full " + txtnoofdays.Text + " Day taken holiday";
                    getShiftType = "FULL DAY"; //+ txtnoofdays.Text + " Day taken holiday";
                }

                //MessageBox.Show(getLeaveType + " " + getShiftType + " "+startDate +" "+ endDate +" "+ txtnoofdays.Text + " "+ textBox1.Text);
                InsertIntoDBLeaveRecord(getLeaveType, getShiftType, txtnoofdays.Text, textBox1.Text, startDate, endDate);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        public void InsertIntoDBLeaveRecord(string lvtype,string sftype,string totdays,string lvcom, string stdt, string endt )
        {
            try
            {
                CreateLeaveID();
                string modleaveid = "LEV" + leaveid;
                DateTime dateTime = DateTime.Now;   
                string cmdstr = "INSERT INTO LEAVE_RECORDS (TXT_LEAVE_TYPE,TXT_SHIFT_TYPE,HOLIDAY_OR_WORKING_HRS,LEAVE_COMMENT,STARTDATE,ENDDATE,SYS_DATE,LEAVEID) VALUES ('" + lvtype + "','" + sftype + "','" + totdays + "','" + lvcom + "','" + stdt + "','" + endt + "','"+dateTime.ToString("yyyy-MM-dd HH:mm:ss")+ "','"+ modleaveid + "')";
                SqlCommand cmd = new SqlCommand(cmdstr, con);
                cmd.ExecuteNonQuery();
                //con.Close();
                btnapplyleave.Enabled = false;
                
                MessageBox.Show("Leave Applied successfully, LeaveID="+modleaveid);
                txtlvid.Text = modleaveid;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);    
                return;
            }
            
            
        }
        private void CreateLeaveID()
        {
            try
            {
                string str = "SELECT NEXT VALUE FOR SEQ_LEAVEID";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                sd.Fill(dtleavegen);
                string getleave = dtleavegen.Rows[0]["Column1"].ToString();
                dtleavegen.Clear();
                leaveid = Convert.ToInt32(getleave);
                
                
            }
            catch (Exception ex )
            {

                MessageBox.Show(ex.Message);
                return;
            }
            
        }
        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to close the Application?", "ExitApplicationApp", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                btnapplyleave.Enabled = true;   
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnlvreport_Click(object sender, EventArgs e)
        {
            ShowLeaveReport openReport = new ShowLeaveReport();
            this.Hide();
            openReport.ShowDialog();
            this.Close();
            
        }

        private void comboBox1_Validating(object sender, CancelEventArgs e)
        {
            string appstart= comboBox1.SelectedText.ToString().Trim();
            if(appstart=="Apply Leave")
            {
                label1.Visible = true;
                combxleavetype.Visible = true;
                label2.Visible = true;
                dateTimePicker1.Visible = true;
                label3.Visible = true;
                dateTimePicker2.Visible     = true;
                label4.Visible = true;
                checkBox1.Visible = true;
                cmBxshiftmode.Visible = true;
                label6.Visible = true;
                txtnoofdays.Visible = true;
                label5.Visible = true;
                textBox1.Visible = true;
                btnapplyleave.Visible = true;
                label8.Visible = true;
                txtlvid.Visible = true;
                button1.Visible = true;
                label1.Text = "Leave Type";
                btndel.Visible = false;
            }
            else if (appstart== "Delete Applied Leave")
            {
                label1.Visible= true;
                label1.Text = "Enter the Leave ID";
                txtgetlvidfordel.Visible = true;
                btndel.Visible  = true;
                button1.Visible= true;

                //label1.Visible = false;
                combxleavetype.Visible = false;
                label2.Visible = false;
                dateTimePicker1.Visible = false;
                label3.Visible = false;
                dateTimePicker2.Visible = false;
                label4.Visible = false;
                checkBox1.Visible = false;
                cmBxshiftmode.Visible = false;
                label6.Visible = false;
                txtnoofdays.Visible = false;
                label5.Visible = false;
                textBox1.Visible = false;
                btnapplyleave.Visible = false;
                label8.Visible = false;
                txtlvid.Visible = false;


            }
            
            else
            {
                label1.Visible = false;
                combxleavetype.Visible = false;
                label2.Visible = false;
                dateTimePicker1.Visible = false;
                label3.Visible = false;
                dateTimePicker2.Visible = false;
                label4.Visible = false;
                checkBox1.Visible = false;
                cmBxshiftmode.Visible = false;
                label6.Visible = false;
                txtnoofdays.Visible = false;
                label5.Visible = false;
                textBox1.Visible = false;
                btnapplyleave.Visible = false;
                label8.Visible = false;
                txtlvid.Visible = false;
            }
        }
        public void DeleteAppliedLeave (string getlvid){
            try
            {
                string passid = "";
                DataTable dtgetleave = new DataTable();
                dtgetleave.Clear();
                string scmd = "select leaveid from leave_records where leaveid='"+ getlvid + "'";
                SqlCommand cmd1=new SqlCommand(scmd,con);
                SqlDataAdapter sd= new SqlDataAdapter(cmd1);
                sd.Fill(dtgetleave);
                sd.Dispose();
                if (dtgetleave.Rows.Count > 0){
                    passid = dtgetleave.Rows[0]["leaveid"].ToString();
                }
                else
                {
                    MessageBox.Show("Please enter the valid Leave ID");
                    txtgetlvidfordel.Text = "";
                    txtgetlvidfordel.Focus();
                    return;
                }
                
                string cmdstr = "delete from LEAVE_RECORDS where LEAVEID='" + passid + "'";
                SqlCommand cmd = new SqlCommand(cmdstr, con);
                SqlDataReader sr = cmd.ExecuteReader();
                MessageBox.Show("Leave successfully deleted");
                txtgetlvidfordel.Text = "";
                txtgetlvidfordel.Focus();
                sr.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                button1.Visible = false;
                comboBox1.Text = "";
                label1.Visible = false;
                combxleavetype.Visible = false;
                label2.Visible = false;
                dateTimePicker1.Visible = false;
                label3.Visible = false;
                dateTimePicker2.Visible = false;
                label4.Visible = false;
                checkBox1.Visible = false;
                cmBxshiftmode.Visible = false;
                label6.Visible = false;
                txtnoofdays.Visible = false;
                label5.Visible = false;
                textBox1.Visible = false;
                btnapplyleave.Visible = false;
                label8.Visible = false;
                txtlvid.Visible = false;
                txtgetlvidfordel.Visible = false;
                btndel.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (string.IsNullOrEmpty(txtgetlvidfordel.Text.ToUpper().Trim()))
                {
                    MessageBox.Show("Please enter the valid Leave ID");
                    return;
                }
                else
                {
                    DeleteAppliedLeave(txtgetlvidfordel.Text.ToUpper().Trim());
                }
            }
            catch (Exception)
            {

                throw;
            }
            
            
        }

        private void cmBxshiftmode_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Double selval = Convert.ToDouble(cmBxshiftmode.SelectedValue);
                if (selval == 4.5)
                {
                    txtnoofdays.Text = selval.ToString();
                    label6.Text = "Total working hours";
                }
                else
                {
                    label6.Text = "Total day";
                    txtnoofdays.Text = "1";
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
            
        }

        
    }
}
