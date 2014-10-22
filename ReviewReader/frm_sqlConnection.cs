﻿using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReviewReader.Properties;
using System.Text.RegularExpressions;

namespace ReviewReader
{
    public partial class frm_sqlConnection : Form
    {
        private ReviewReader.Properties.Settings settings = new Settings();
        public static int numOfItems = 0;
        private bool connected = false;
        private bool OK = false;
        public frm_sqlConnection()
        {
            InitializeComponent();
            tbx_serverName.Text = settings.serverName;
            tbx_userName.Text = settings.userId;
            tbx_password.Text = settings.password;

        }

        private void brn_CreateNewDatabase_Click(object sender, EventArgs e)
        {
            var databaseName = Interaction.InputBox("Please Enter a Database Name (No Spaces, text characters only)", "Database Name", "DatabaseName");

            string re1 = "(([_?A-z0-9])+[_]?([A-z_0-9])+)";	// Variable Name 1
            if (databaseName == "")
            {
                return;
            }
            Regex r = new Regex(re1, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = r.Match(databaseName);
            if (m.Success)
            {
                bool databaseSuccess = createDatabase(databaseName);
            }
            else
            {
                MessageBox.Show("Invalid Database name. Enter a name with characters from A to z with no spaces");
            }

            refreshComboBox();
        }

        private bool createDatabase(string databaseName)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(settings.ItemReviewsBase);
                MySqlCommand command = new MySqlCommand("CREATE DATABASE " + databaseName + ";", connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }
        public void refreshComboBox()
        {
            cmb_databaseNames.Items.Clear();
            List<string> databaseNames = getDatabases();
            foreach (string tn in databaseNames)
            {
                NameValue n1 = new NameValue(tn, tn);
                cmb_databaseNames.Items.Add(n1);
            }
            cmb_databaseNames.Text = "";
            cmb_databaseNames.SelectedIndex = 0;
        }

        private List<string> getDatabases()
        {
            var connString = settings.ItemReviewsBase;
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            conn.Open();
            command.CommandText = "show databases";

            List<string> databaseNames = new List<string>();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                databaseNames.Add(reader.GetString(0));
            }

            // Call Close when done reading.
            reader.Close();
            conn.Close();
            //add some handling as to whether this create is successful or not
            return databaseNames;
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            try
            {

                MySqlConnection connection = new MySqlConnection(new ConnectionStringSettings("ItemReviewBase", "server=" + tbx_serverName.Text + ";user id=" + tbx_userName.Text + ";password=" + tbx_password.Text + ";persistsecurityinfo=True", "MySql.Data.MySqlClient").ToString());
                connection.Open();
                connection.Close();
                //MessageBox.Show("Connection was successful");
                lbl_Connected.Text = "Connected";
                lbl_Connected.ForeColor = Color.Green;
                settings.ItemReviewsBase = connection.ConnectionString;
                settings.userId = tbx_userName.Text;
                settings.serverName = tbx_serverName.Text;
                settings.password = tbx_password.Text;
                GB_connToDB.Enabled = true;
                connected = true;
                refreshComboBox();
            }
            catch (MySqlException ex)
            {
                lbl_Connected.Text = "Not Connected";
                lbl_Connected.ForeColor = Color.Red;
                MessageBox.Show(ex.Message);
                connected = false;
                GB_connToDB.Enabled = false;
                cmb_databaseNames.Items.Clear();


            }
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            OK = true;
            Close();
        }

        private void frm_sqlConnection_Load(object sender, EventArgs e)
        {

        }

        private void frm_sqlConnection_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connected && OK)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                settings.selectedDatabase = cmb_databaseNames.Text;
                settings.ItemReviews = new ConnectionStringSettings("ItemReview", "server=" + settings.serverName + ";user id=" + settings.userId + ";password=" + settings.password + ";database=" + cmb_databaseNames.Text + ";persistsecurityinfo=True", "MySql.Data.MySqlClient").ToString();
                settings.Save();
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }
    }
}
