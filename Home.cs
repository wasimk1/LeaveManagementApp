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
using System.Web;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace LeaveManagementApp
{
    public partial class Home : Form
    {
        public static SqlConnection con = null;
        DataTable dtleavetype = new DataTable();
        DataTable dtshiftmode = new DataTable();
        DataTable dtleavegen = new DataTable();
        public static string startDate, endDate;
        public static int leaveid;
        public static string username = string.Empty;
        public static string strcon = ConfigurationManager.ConnectionStrings["constr-PAVILION-NB"].ConnectionString;
        Double tot = 0;
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
                    strcon = ConfigurationManager.ConnectionStrings["constr-PROBOOK-NEW"].ConnectionString;
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
            else if (Convert.ToInt32(txtnoofdays.Text) > 1)
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
                    if (getShiftType == "0.5")
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

                DataTable dt = new DataTable();

                // string cmd = "select sum(TOT_CASUAL_LV + TOT_SICK_LV)[TOTAL]  from USERS_RECORDS where TXT_NAME='"+username+"'"; 
                string cmd = "select ALL_BAL_LEAVE from USERS_RECORDS where TXT_NAME='" + username + "'";
                SqlCommand sql = new SqlCommand(cmd, con);
                SqlDataAdapter sd = new SqlDataAdapter(sql);
                sd.Fill(dt);
                sd.Dispose();
                if (dt.Rows.Count > 0)
                {
                    //if(dt.Rows[0]["TotalTakenLeave"].ToString()=="" && dt.Rows[0]["TotalTakenLeave"].ToString() == "")
                    //{
                    //     tot = Convert.ToDouble("0");
                    //}
                    //else
                    //{
                    //    string t = dt.Rows[0]["TotalTakenLeave"].ToString();
                    //    string t1 = dt.Rows[0]["ExtraWork"].ToString();
                    //     tot = Convert.ToDouble(t1) * Convert.ToDouble(t);
                    //}
                    tot = Convert.ToDouble(dt.Rows[0]["ALL_BAL_LEAVE"].ToString());
                    if (Convert.ToDouble(txtnoofdays.Text) > tot)
                    {
                        MessageBox.Show("You are trying to apply a Leave grater than your ramaining Leave, Not possible! You only have " + tot + " Leave remaining");
                        return;
                    }
                }


                //MessageBox.Show(getLeaveType + " " + getShiftType + " "+startDate +" "+ endDate +" "+ txtnoofdays.Text + " "+ textBox1.Text);
                InsertIntoDBLeaveRecord(getLeaveType, getShiftType, txtnoofdays.Text, textBox1.Text, startDate, endDate);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void InsertIntoDBLeaveRecord(string lvtype, string sftype, string totdays, string lvcom, string stdt, string endt)
        {
            try
            {
                //con.Open();
                CreateLeaveID();
                string cmdstr = string.Empty;
                string cmdstr1 = string.Empty;
                string modleaveid = "LEV" + leaveid;
                DateTime dateTime = DateTime.Now;
                if (combxleavetype.SelectedValue.ToString() == "EXTRA WORKING")
                {
                    cmdstr = "INSERT INTO LEAVE_RECORDS (TXT_LEAVE_TYPE,TXT_SHIFT_TYPE,HOLIDAY_OR_WORKING_HRS,LEAVE_COMMENT,STARTDATE,ENDDATE,SYS_DATE,LEAVEID,EXTRA_WORK,TXT_NAME) VALUES ('" + lvtype + "','" + sftype + "',0,'" + lvcom + "','" + stdt + "','" + endt + "','" + dateTime.ToString("yyyy-MM-dd HH:mm:ss") + "','" + modleaveid + "'," + totdays + ",'" + textBox2.Text.ToUpper().Trim() + "')";
                    double findtot = (tot + Convert.ToDouble(totdays));
                    cmdstr1 = "UPDATE USERS_RECORDS SET ALL_BAL_LEAVE=" + findtot + " where TXT_NAME='" + username + "'";

                }
                else
                {
                    cmdstr = "INSERT INTO LEAVE_RECORDS (TXT_LEAVE_TYPE,TXT_SHIFT_TYPE,HOLIDAY_OR_WORKING_HRS,LEAVE_COMMENT,STARTDATE,ENDDATE,SYS_DATE,LEAVEID,EXTRA_WORK,TXT_NAME) VALUES ('" + lvtype + "','" + sftype + "'," + totdays + ",'" + lvcom + "','" + stdt + "','" + endt + "','" + dateTime.ToString("yyyy-MM-dd HH:mm:ss") + "','" + modleaveid + "',0,'" + textBox2.Text.ToUpper().Trim() + "')";


                    double findtot = (tot - Convert.ToDouble(totdays));
                    cmdstr1 = "UPDATE USERS_RECORDS SET ALL_BAL_LEAVE=" + findtot + " where TXT_NAME='" + username + "'";
                }
                //string cmdstr = "INSERT INTO LEAVE_RECORDS (TXT_LEAVE_TYPE,TXT_SHIFT_TYPE,HOLIDAY_OR_WORKING_HRS,LEAVE_COMMENT,STARTDATE,ENDDATE,SYS_DATE,LEAVEID) VALUES ('" + lvtype + "','" + sftype + "','" + totdays + "','" + lvcom + "','" + stdt + "','" + endt + "','" + dateTime.ToString("yyyy-MM-dd HH:mm:ss") + "','" + modleaveid + "')";


                SqlCommand cmd = new SqlCommand(cmdstr, con);
                cmd.ExecuteNonQuery();

                SqlCommand cmd1 = new SqlCommand(cmdstr1, con);
                cmd1.ExecuteNonQuery();
                //con.Close();
                btnapplyleave.Enabled = false;

                MessageBox.Show("Leave Applied successfully, LeaveID=" + modleaveid);
                txtlvid.Text = modleaveid;
                //con.Close();
                cmd.Dispose();
                cmd1.Dispose();
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
                sd.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

        }
        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to close the Application?", "Exit Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
                
            }
            LogDBforOut();
            //else
            //{
            //    e.Cancel = false;
            //}
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
            //this.Hide();
            openReport.ShowDialog();
            //this.Close();

        }

        public void ShowFiledApplyLeave()
        {
            try
            {
                label1.Visible = true;
                combxleavetype.Visible = true;
                label2.Visible = true;
                dateTimePicker1.Visible = true;
                label3.Visible = true;
                dateTimePicker2.Visible = true;
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
                txtgetlvidfordel.Visible = false;
                comboBox1.Enabled = false; // For disabling the combo box after selecting the item 
                if (comboBox1.SelectedItem.ToString() == "Apply Extra Working")
                {
                    btnapplyleave.Text = "Apply Extra Working";
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void showFieldDeleteLeave()
        {
            try
            {
                label1.Visible = true;
                label1.Text = "Enter the Leave ID";
                txtgetlvidfordel.Visible = true;
                btndel.Visible = true;
                button1.Visible = true;
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
                comboBox1.Enabled = false; // For disabling the combo box after selecting the item 
            }
            catch (Exception)
            {

                throw;
            }

        }
        private void comboBox1_Validating(object sender, CancelEventArgs e)
        {
            string appstart = comboBox1.SelectedText.ToString().Trim();
            if (appstart == "Apply Leave")
            {
                ShowFiledApplyLeave();
            }
            else if(appstart == "Apply Extra Working")
            {
                ShowFiledApplyLeave();
            }
            else if (appstart == "Delete Applied Leave")
            {
                showFieldDeleteLeave();
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
        public void DeleteAppliedLeave(string getlvid)
        {
            try
            {
                string passid = "";
                DataTable dtgetleave = new DataTable();
                dtgetleave.Clear();
                string scmd = "SELECT LEAVEID FROM LEAVE_RECORDS WHERE LEAVEID='" + getlvid + "'";
                SqlCommand cmd1 = new SqlCommand(scmd, con);
                SqlDataAdapter sd = new SqlDataAdapter(cmd1);
                sd.Fill(dtgetleave);
                sd.Dispose();
                cmd1.Dispose();
                if (dtgetleave.Rows.Count > 0)
                {
                    passid = dtgetleave.Rows[0]["leaveid"].ToString();
                }
                else
                {
                    MessageBox.Show("Please enter the valid Leave ID");
                    txtgetlvidfordel.Text = "";
                    txtgetlvidfordel.Focus();
                    return;
                }
                //con.Open();
                string cmdstr = "delete from LEAVE_RECORDS where LEAVEID='" + passid + "'and  txt_name='" + textBox2.Text.ToUpper().Trim() + "'";
                SqlCommand cmd = new SqlCommand(cmdstr, con);
                SqlDataReader sr = cmd.ExecuteReader();
                MessageBox.Show("Leave successfully deleted");
                txtgetlvidfordel.Text = "";
                txtgetlvidfordel.Focus();
                sr.Close();
                cmd.Dispose();
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
                comboBox1.Enabled = true; // Modified for cancelling the Leave applying process
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            try
            {
                DateTime d = DateTime.Now;
                string date = d.ToString("dd");
                string time = d.ToString("HH");
                string today = date + time;
                DataTable dt = new DataTable();
                string cmdstr = "SELECT TXT_NAME,TOT_SICK_LV, TOT_CASUAL_LV,USER_MODE  FROM  USERS_RECORDS where TXT_NAME='" + textBox2.Text.ToUpper().Trim() + "'";
                SqlCommand cmd2 = new SqlCommand(cmdstr, Home.con);
                SqlDataAdapter sd = new SqlDataAdapter(cmd2);
                sd.Fill(dt);
                sd.Dispose();

                if (dt.Rows.Count > 0)
                {
                    if (textBox2.Text.ToUpper().Trim() == dt.Rows[0]["TXT_NAME"].ToString() && textBox3.Text == today)
                    {
                        LogDBforIn();
                        username = textBox2.Text.ToUpper().Trim();
                        label9.Visible = true;
                        comboBox1.Visible = true;
                        btnlvreport.Visible = true;
                        textBox2.ReadOnly = true;
                        textBox3.ReadOnly = true;
                        btnSubmit.Enabled = false;
                        btnLogout.Enabled = true;
                        comboBox1.Visible = true;
                        comboBox1.Enabled = true;
                    }
                    else if (textBox2.Text.ToUpper().Trim() == dt.Rows[0]["TXT_NAME"].ToString() && textBox3.Text == today + "@Admin" && Convert.ToInt64(dt.Rows[0]["USER_MODE"].ToString()) == 1)
                    {
                        button2.Visible = true;
                        button2.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("User is invalid");
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox2.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid user");
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox2.Focus();
                    return;
                }
                cmd2.Dispose();
                dt.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void LogDBforIn()
        {
            string format = "yyyy-MM-dd HH:mm:ss tt";
            DateTime time1 = DateTime.Now ;
            DateTime time2 = DateTime.Now;

            string logtime = time1.ToString(format);
            //string logoutime= time2.ToString(format);
            int empid = 1001;
            string username = textBox2.Text.ToUpper().Trim();
            string status = "ACTIVE";

            string cmdstr = "INSERT INTO LOGINANDOUT (EMP_ID,TXT_USERNAME ,LOGIN_TIME,LOGOUT_TIME,EMP_STATUS) VALUES("+empid+",'"+ textBox2.Text.ToUpper().Trim() + "', '"+ logtime + "','"+ logtime + "','"+status+"')";

            SqlCommand cmd = new SqlCommand(cmdstr,con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();

        }
        public void LogDBforOut()
        {
            string format = "yyyy-MM-dd hh:mm:ss tt";
            DateTime time1 = DateTime.Now;
            DateTime time2 = DateTime.Now;

            string logouttime = time1.ToString(format);
            //string logoutime= time2.ToString(format);
            int empid = 1001;
            string username = textBox2.Text.ToUpper().Trim();
            string status = "INACTIVE";

            string cmdstr = "update LOGINANDOUT set LOGOUT_TIME='"+ logouttime + "', EMP_STATUS='"+status+"' where EMP_ID='"+ empid + "' and EMP_STATUS='ACTIVE'";

            SqlCommand cmd = new SqlCommand(cmdstr, con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();

        }
        public void HideonLogout()
        {
            try
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
                label9.Visible = false;
                comboBox1.Visible = false;
                comboBox1.Text = "";
                btnlvreport.Visible = false;
                button1.Visible = false;
                btndel.Visible = false;
                txtgetlvidfordel.Visible = false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            username = "";
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            btnLogout.Enabled = false;
            btnSubmit.Enabled = true;
            textBox2.ReadOnly = false;
            textBox3.ReadOnly = false;
            HideonLogout();
            LogDBforOut();
        }

        private void txtnoofdays_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminReport openAdminReport = new AdminReport();
            //this.Hide();
            openAdminReport.ShowDialog();
            //this.Close();
        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmBxshiftmode_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Double selval = Convert.ToDouble(cmBxshiftmode.SelectedValue);
                if (selval == 0.5)
                {
                    txtnoofdays.Text = selval.ToString();
                    label6.Text = "Half Day";
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
