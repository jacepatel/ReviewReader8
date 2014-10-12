namespace ReviewReader
{
    partial class frm_sqlConnection
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbx_serverName = new System.Windows.Forms.TextBox();
            this.tbx_userName = new System.Windows.Forms.TextBox();
            this.tbx_password = new System.Windows.Forms.TextBox();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmb_databaseNames = new System.Windows.Forms.ComboBox();
            this.brn_CreateNewDatabase = new System.Windows.Forms.Button();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.lbl_Connected = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "User name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password";
            // 
            // tbx_serverName
            // 
            this.tbx_serverName.Location = new System.Drawing.Point(87, 45);
            this.tbx_serverName.Name = "tbx_serverName";
            this.tbx_serverName.Size = new System.Drawing.Size(343, 20);
            this.tbx_serverName.TabIndex = 3;
            // 
            // tbx_userName
            // 
            this.tbx_userName.Location = new System.Drawing.Point(87, 71);
            this.tbx_userName.Name = "tbx_userName";
            this.tbx_userName.Size = new System.Drawing.Size(343, 20);
            this.tbx_userName.TabIndex = 4;
            // 
            // tbx_password
            // 
            this.tbx_password.Location = new System.Drawing.Point(87, 97);
            this.tbx_password.Name = "tbx_password";
            this.tbx_password.Size = new System.Drawing.Size(343, 20);
            this.tbx_password.TabIndex = 5;
            this.tbx_password.UseSystemPasswordChar = true;
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(436, 95);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(75, 23);
            this.btn_Connect.TabIndex = 6;
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmb_databaseNames);
            this.groupBox1.Controls.Add(this.brn_CreateNewDatabase);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(12, 198);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(496, 166);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connect to a Database";
            // 
            // cmb_TableNames
            // 
            this.cmb_databaseNames.FormattingEnabled = true;
            this.cmb_databaseNames.Location = new System.Drawing.Point(6, 19);
            this.cmb_databaseNames.Name = "cmb_TableNames";
            this.cmb_databaseNames.Size = new System.Drawing.Size(484, 21);
            this.cmb_databaseNames.TabIndex = 8;
            // 
            // brn_CreateNewDatabase
            // 
            this.brn_CreateNewDatabase.Location = new System.Drawing.Point(6, 137);
            this.brn_CreateNewDatabase.Name = "brn_CreateNewDatabase";
            this.brn_CreateNewDatabase.Size = new System.Drawing.Size(118, 23);
            this.brn_CreateNewDatabase.TabIndex = 7;
            this.brn_CreateNewDatabase.Text = "Create new Database";
            this.brn_CreateNewDatabase.UseVisualStyleBackColor = true;
            this.brn_CreateNewDatabase.Click += new System.EventHandler(this.brn_CreateNewDatabase_Click);
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(436, 480);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 8;
            this.btn_Ok.Text = "OK";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // lbl_Connected
            // 
            this.lbl_Connected.AutoSize = true;
            this.lbl_Connected.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Connected.ForeColor = System.Drawing.Color.Red;
            this.lbl_Connected.Location = new System.Drawing.Point(368, 140);
            this.lbl_Connected.Name = "lbl_Connected";
            this.lbl_Connected.Size = new System.Drawing.Size(143, 25);
            this.lbl_Connected.TabIndex = 9;
            this.lbl_Connected.Text = "Not Connected";
            // 
            // frm_sqlConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 515);
            this.Controls.Add(this.lbl_Connected);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Connect);
            this.Controls.Add(this.tbx_password);
            this.Controls.Add(this.tbx_userName);
            this.Controls.Add(this.tbx_serverName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frm_sqlConnection";
            this.Text = "Connect to Database";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbx_serverName;
        private System.Windows.Forms.TextBox tbx_userName;
        private System.Windows.Forms.TextBox tbx_password;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmb_databaseNames;
        private System.Windows.Forms.Button brn_CreateNewDatabase;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Label lbl_Connected;
    }
    public class NameValue
    {

        private string dataName;
        private string dataValue;

        public NameValue(string dataName, string dataValue)
        {
            DataName = dataName;
            DataValue = dataValue;
        }

        public string DataName
        {
            get { return dataName; }
            set { dataName = value; }
        }

        public string DataValue
        {
            get { return dataValue; }
            set { dataValue = value; }
        }

        public override string ToString()
        {
            return dataName;
        }

    }
}