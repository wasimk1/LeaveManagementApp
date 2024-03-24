namespace LeaveManagementApp
{
    partial class Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnapplyleave = new System.Windows.Forms.Button();
            this.combxleavetype = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.txtnoofdays = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmBxshiftmode = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtlvid = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnlvreport = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtgetlvidfordel = new System.Windows.Forms.TextBox();
            this.btndel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnapplyleave
            // 
            this.btnapplyleave.Enabled = false;
            this.btnapplyleave.Location = new System.Drawing.Point(145, 210);
            this.btnapplyleave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnapplyleave.Name = "btnapplyleave";
            this.btnapplyleave.Size = new System.Drawing.Size(107, 32);
            this.btnapplyleave.TabIndex = 8;
            this.btnapplyleave.Text = "Apply Leave";
            this.btnapplyleave.UseVisualStyleBackColor = true;
            this.btnapplyleave.Visible = false;
            this.btnapplyleave.Click += new System.EventHandler(this.btnapplyleave_Click);
            // 
            // combxleavetype
            // 
            this.combxleavetype.FormattingEnabled = true;
            this.combxleavetype.Location = new System.Drawing.Point(147, 80);
            this.combxleavetype.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.combxleavetype.Name = "combxleavetype";
            this.combxleavetype.Size = new System.Drawing.Size(200, 24);
            this.combxleavetype.TabIndex = 1;
            this.combxleavetype.Visible = false;
            this.combxleavetype.SelectedIndexChanged += new System.EventHandler(this.combxleavetype_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Leave Type";
            this.label1.Visible = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(145, 111);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker1.TabIndex = 2;
            this.dateTimePicker1.Visible = false;
            this.dateTimePicker1.Validating += new System.ComponentModel.CancelEventHandler(this.dateTimePicker1_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Start Date";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(463, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "End Date";
            this.label3.Visible = false;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(537, 116);
            this.dateTimePicker2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker2.TabIndex = 3;
            this.dateTimePicker2.Visible = false;
            this.dateTimePicker2.Validating += new System.ComponentModel.CancelEventHandler(this.dateTimePicker2_Validating);
            // 
            // txtnoofdays
            // 
            this.txtnoofdays.Location = new System.Drawing.Point(688, 145);
            this.txtnoofdays.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtnoofdays.Name = "txtnoofdays";
            this.txtnoofdays.ReadOnly = true;
            this.txtnoofdays.Size = new System.Drawing.Size(49, 22);
            this.txtnoofdays.TabIndex = 4;
            this.txtnoofdays.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(145, 181);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.MaxLength = 500;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(592, 22);
            this.textBox1.TabIndex = 7;
            this.textBox1.Visible = false;
            this.textBox1.WordWrap = false;
            this.textBox1.Validating += new System.ComponentModel.CancelEventHandler(this.textBox1_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 185);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Leave Comment";
            this.label5.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(145, 153);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(18, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 149);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Day Shift";
            this.label4.Visible = false;
            // 
            // cmBxshiftmode
            // 
            this.cmBxshiftmode.FormattingEnabled = true;
            this.cmBxshiftmode.Location = new System.Drawing.Point(173, 149);
            this.cmBxshiftmode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmBxshiftmode.Name = "cmBxshiftmode";
            this.cmBxshiftmode.Size = new System.Drawing.Size(173, 24);
            this.cmBxshiftmode.TabIndex = 6;
            this.cmBxshiftmode.Visible = false;
            this.cmBxshiftmode.Validating += new System.ComponentModel.CancelEventHandler(this.cmBxshiftmode_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(463, 149);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 16);
            this.label6.TabIndex = 13;
            this.label6.Text = "Total day";
            this.label6.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Cascadia Mono", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(200, -1);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(337, 37);
            this.label7.TabIndex = 12;
            this.label7.Text = "Leave Management App";
            // 
            // txtlvid
            // 
            this.txtlvid.Location = new System.Drawing.Point(145, 248);
            this.txtlvid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtlvid.Name = "txtlvid";
            this.txtlvid.ReadOnly = true;
            this.txtlvid.Size = new System.Drawing.Size(132, 22);
            this.txtlvid.TabIndex = 9;
            this.txtlvid.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 248);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 16);
            this.label8.TabIndex = 8;
            this.label8.Text = "Leave ID";
            this.label8.Visible = false;
            // 
            // btnlvreport
            // 
            this.btnlvreport.Location = new System.Drawing.Point(580, 51);
            this.btnlvreport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnlvreport.Name = "btnlvreport";
            this.btnlvreport.Size = new System.Drawing.Size(157, 30);
            this.btnlvreport.TabIndex = 10;
            this.btnlvreport.Text = "Show Leave Report";
            this.btnlvreport.UseVisualStyleBackColor = true;
            this.btnlvreport.Click += new System.EventHandler(this.btnlvreport_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Apply Leave",
            "Delete Applied Leave"});
            this.comboBox1.Location = new System.Drawing.Point(147, 51);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(200, 24);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.Validating += new System.ComponentModel.CancelEventHandler(this.comboBox1_Validating);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 16);
            this.label9.TabIndex = 17;
            this.label9.Text = "Start";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(353, 51);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 24);
            this.button1.TabIndex = 11;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // txtgetlvidfordel
            // 
            this.txtgetlvidfordel.Location = new System.Drawing.Point(147, 80);
            this.txtgetlvidfordel.Name = "txtgetlvidfordel";
            this.txtgetlvidfordel.Size = new System.Drawing.Size(198, 22);
            this.txtgetlvidfordel.TabIndex = 18;
            this.txtgetlvidfordel.Visible = false;
            // 
            // btndel
            // 
            this.btndel.Location = new System.Drawing.Point(353, 79);
            this.btndel.Name = "btndel";
            this.btndel.Size = new System.Drawing.Size(75, 25);
            this.btndel.TabIndex = 19;
            this.btndel.Text = "Delete";
            this.btndel.UseVisualStyleBackColor = true;
            this.btndel.Visible = false;
            this.btndel.Click += new System.EventHandler(this.btndel_Click);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 281);
            this.Controls.Add(this.btndel);
            this.Controls.Add(this.txtgetlvidfordel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnlvreport);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtlvid);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmBxshiftmode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtnoofdays);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combxleavetype);
            this.Controls.Add(this.btnapplyleave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Leave Management";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Home_FormClosing);
            this.Load += new System.EventHandler(this.Home_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnapplyleave;
        private System.Windows.Forms.ComboBox combxleavetype;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.TextBox txtnoofdays;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmBxshiftmode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtlvid;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnlvreport;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtgetlvidfordel;
        private System.Windows.Forms.Button btndel;
    }
}

