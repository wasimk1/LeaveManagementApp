﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LeaveManagementApp
{
    public partial class ShowLeaveReport : Form
    {
        //SqlConnection con = null;
        //string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        DataTable dtFillLeaveRpt = new DataTable();
        string getleaveid = "";
        string filepath = @"C:\Leave_Management_Report\Leave_Management_Report_" + Home.username + ".csv";

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
            Home openhome = new Home();
            //this.Hide();
            //closeapp();
            openhome.ShowDialog();
            //this.close();


        }
        public void closeapp()
        {
            Application.Exit();
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
                    //string cmdstr = "SELECT  LEAVEID, TXT_LEAVE_TYPE [LEAVE TYPE],TXT_SHIFT_TYPE [SHIFT TYPE],HOLIDAY_OR_WORKING_HRS [HOLIDAY / HALF_DAY], EXTRA_WORK [EXTRA WORK], STARTDATE [FROM DATE],ENDDATE [TO DATE] ,LEAVE_COMMENT [LEAVE COMMENT] FROM LEAVE_RECORDS where LEAVEID='" + getleaveid + "'";
                    string cmdstr = "SELECT  LR.LEAVEID, LR.TXT_LEAVE_TYPE [LEAVE TYPE],LR.TXT_SHIFT_TYPE [SHIFT TYPE],LR.HOLIDAY_OR_WORKING_HRS [HOLIDAY / HALF_DAY], LR.EXTRA_WORK [EXTRA WORK],LR.STARTDATE [FROM DATE],LR.ENDDATE [TO DATE] ,LR.LEAVE_COMMENT [LEAVE COMMENT] FROM LEAVE_RECORDS LR INNER JOIN USERS_RECORDS UR ON LR.TXT_NAME = UR.TXT_NAME WHERE UR.TXT_NAME='" + Home.username + "' and LEAVEID='" + getleaveid + "' ";
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

                //Home.con.Close();
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
                textBox1.Text = "";
                showTotCasualandSickLV();
                showTotLeaveTaken();

                //string cmdstr = "SELECT  LEAVEID, TXT_LEAVE_TYPE [LEAVE TYPE],TXT_SHIFT_TYPE [SHIFT TYPE],HOLIDAY_OR_WORKING_HRS [HOLIDAY / HALF_DAY],EXTRA_WORK [EXTRA WORK], STARTDATE [FROM DATE],ENDDATE [TO DATE] ,LEAVE_COMMENT [LEAVE COMMENT] FROM LEAVE_RECORDS order by id desc";
                string cmdstr = "SELECT  LR.LEAVEID, LR.TXT_LEAVE_TYPE [LEAVE TYPE],LR.TXT_SHIFT_TYPE [SHIFT TYPE],LR.HOLIDAY_OR_WORKING_HRS [HOLIDAY / HALF_DAY],LR.EXTRA_WORK [EXTRA WORK], LR.STARTDATE [FROM DATE],LR.ENDDATE [TO DATE] ,LR.LEAVE_COMMENT [LEAVE COMMENT] FROM LEAVE_RECORDS LR INNER JOIN USERS_RECORDS UR ON LR.TXT_NAME = UR.TXT_NAME WHERE UR.TXT_NAME='" + Home.username + "' ORDER BY LR.ID DESC";
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
                    MessageBox.Show("You have not taken any Leave yet");
                    return;
                }
                button2.Text = "Referesh";
                //Home.con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public void showTotLeaveTaken()
        {
            try
            {
                DataTable dt = new DataTable();
                //string cmdstr = "SELECT SUM(HOLIDAY_OR_WORKING_HRS)[TotalTakenLeave],sum(EXTRA_WORK)[ExtraWork] FROM LEAVE_RECORDS";
                string cmdstr = "SELECT SUM(LR.HOLIDAY_OR_WORKING_HRS)[TotalTakenLeave],sum(LR.EXTRA_WORK)[ExtraWork] FROM LEAVE_RECORDS LR INNER JOIN USERS_RECORDS UR ON LR.TXT_NAME = UR.TXT_NAME WHERE UR.TXT_NAME='" + Home.username + "'";
                SqlCommand cmd2 = new SqlCommand(cmdstr, Home.con);
                SqlDataAdapter sd = new SqlDataAdapter(cmd2);
                sd.Fill(dt);
                sd.Dispose();

                if (dt.Rows.Count > 0)
                {
                    textBox2.Text = dt.Rows[0]["TotalTakenLeave"].ToString();
                    textBox3.Text = dt.Rows[0]["ExtraWork"].ToString();
                    if (textBox2.Text == "" && textBox3.Text == "")
                    {
                        textBox2.Text = "0";
                        textBox3.Text = "0";
                    }
                    //decimal tatal=Math.
                    textBox4.Text = ((Convert.ToDouble(textBox7.Text) - Convert.ToDouble(textBox2.Text)) + Convert.ToDouble(textBox3.Text)).ToString();
                }
                cmd2.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void showTotCasualandSickLV()
        {
            try
            {
                DataTable dt = new DataTable();
                string cmdstr = "SELECT TOT_SICK_LV,TOT_CASUAL_LV FROM USERS_RECORDS WHERE TXT_NAME='" + Home.username + "'";
                SqlCommand cmd2 = new SqlCommand(cmdstr, Home.con);
                SqlDataAdapter sd = new SqlDataAdapter(cmd2);
                sd.Fill(dt);
                sd.Dispose();

                if (dt.Rows.Count > 0)
                {
                    textBox5.Text = dt.Rows[0]["TOT_CASUAL_LV"].ToString();
                    textBox6.Text = dt.Rows[0]["TOT_SICK_LV"].ToString();
                    textBox7.Text = Convert.ToString(Convert.ToDouble(textBox5.Text) + Convert.ToDouble(textBox6.Text));
                    //textBox4.Text = Convert.ToString(Convert.ToInt32(textBox5.Text) + Convert.ToInt32(textBox6.Text));
                }
                cmd2.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btndwnldrpt_Click(object sender, EventArgs e)
        {
            try
            {
                //string filepath = @"C:\Leave_Management_Report\Leave_Management_Report.csv";
                if (!Directory.Exists(@"C:\Leave_Management_Report"))
                {

                    Directory.CreateDirectory(@"C:\Leave_Management_Report");
                    if (!File.Exists(filepath))
                    {
                        File.Create(filepath);
                    }
                    else
                    {
                        File.Delete(filepath);
                    }
                }
                dtFillLeaveRpt.Clear();
                //string cmdstr = "SELECT  LEAVEID, TXT_LEAVE_TYPE [LEAVE TYPE],TXT_SHIFT_TYPE [SHIFT TYPE],HOLIDAY_OR_WORKING_HRS [HOLIDAY / HALF_DAY],EXTRA_WORK [EXTRA WORK], STARTDATE [FROM DATE],ENDDATE [TO DATE] ,LEAVE_COMMENT [LEAVE COMMENT] FROM LEAVE_RECORDS order by id desc";
                string cmdstr = "SELECT  LR.LEAVEID, LR.TXT_LEAVE_TYPE [LEAVE TYPE],LR.TXT_SHIFT_TYPE [SHIFT TYPE],LR.HOLIDAY_OR_WORKING_HRS [HOLIDAY / HALF_DAY],LR.EXTRA_WORK [EXTRA WORK], LR.STARTDATE [FROM DATE],LR.ENDDATE [TO DATE] ,LR.LEAVE_COMMENT [LEAVE COMMENT] FROM LEAVE_RECORDS LR INNER JOIN USERS_RECORDS UR ON LR.TXT_NAME = UR.TXT_NAME WHERE UR.TXT_NAME='" + Home.username + "' ORDER BY LR.ID DESC";
                SqlCommand cmd = new SqlCommand(cmdstr, Home.con);
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                sd.Fill(dtFillLeaveRpt);
                sd.Dispose();
                if (dtFillLeaveRpt.Rows.Count > 0)
                {
                    CreateCSVFile(ref dtFillLeaveRpt, filepath);
                }
                else
                {
                    MessageBox.Show("There is no Leave you have taken, so report downloading is not possible");
                    return;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show("Error creating Excel File - Please close the existing excel opened file first - " + ex.Message);
            }

        }
        public void CreateCSVFile(ref DataTable dt, string strFilePath)
        {
            try
            {
                // Create the CSV file to which grid data will be exported.
                StreamWriter sw = new StreamWriter(strFilePath, false);
                // First we will write the headers.
                //DataTable dt = m_dsProducts.Tables[0];
                int iColCount = dt.Columns.Count;
                for (int i = 0; i < iColCount; i++)
                {
                    sw.Write(dt.Columns[i]);
                    if (i < iColCount - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);

                // Now write all the rows.

                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < iColCount; i++)
                    {
                        if (!Convert.IsDBNull(dr[i]))
                        {
                            sw.Write(dr[i].ToString());
                        }
                        if (i < iColCount - 1)
                        {
                            sw.Write(",");
                        }
                    }

                    sw.Write(sw.NewLine);
                }
                sw.Close();
                MessageBox.Show("Report successfully downloaded at the path= " + filepath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ShowLeaveReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
            //Home.con.Close();
        }
    }
}
