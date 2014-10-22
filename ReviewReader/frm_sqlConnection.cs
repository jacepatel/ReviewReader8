using Microsoft.VisualBasic;
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

namespace ReviewReader
{
    /// <summary>
    /// Window Form for connecting to database
    /// </summary>
    public partial class frm_sqlConnection : Form
    {
        private ReviewReader.Properties.Settings settings = new Settings();
        public static int numOfItems = 0;
        private bool connected = false;
        public frm_sqlConnection()
        {
            InitializeComponent();
            tbx_serverName.Text = settings.serverName;
            tbx_userName.Text = settings.userId;
            tbx_password.Text = settings.password;
            
        }

        //Button actions for creating new database.
        private void brn_CreateNewDatabase_Click(object sender, EventArgs e)
        {
            var databaseName = Interaction.InputBox("Please Enter a Database Name (No Spaces, text characters only)", "Database Name", "DatabaseName");

            if (databaseName != "")
            {
                bool databaseSuccess = createDatabase(databaseName);
            }

            refreshComboBox();
        }

        /// <summary>
        /// Creating a database on the server.
        /// </summary>
        /// <param name="databaseName">String of the name of database</param>
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
            
            //Exception are thrown when MySQL fails to create database.
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        //Renewing the list of the combo box for displaying different database name.
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

        //Collect database name from DBM and stored into a list of string.
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

            return databaseNames;
        }

        //Button for connecting to database server
        private void btn_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                
                MySqlConnection connection = new MySqlConnection(new ConnectionStringSettings("ItemReviewBase", "server=" + tbx_serverName.Text + ";user id=" + tbx_userName.Text + ";password=" + tbx_password.Text + ";persistsecurityinfo=True", "MySql.Data.MySqlClient").ToString());
                connection.Open();
                connection.Close();
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

            //When database server is not connected
            catch(MySqlException ex)
            {
                lbl_Connected.Text = "Not Connected";
                lbl_Connected.ForeColor = Color.Red;
                MessageBox.Show(ex.Message);
                connected = false;
            }
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
           
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            settings.selectedDatabase = cmb_databaseNames.Text;
            settings.ItemReviews = new ConnectionStringSettings("ItemReview", "server=" + settings.serverName + ";user id=" + settings.userId + ";password=" + settings.password + ";database=" + cmb_databaseNames.Text + ";persistsecurityinfo=True", "MySql.Data.MySqlClient").ToString();
            settings.Save();
            this.Hide();
            frm_Main frmMain = new frm_Main();
            frmMain.ShowDialog();

        }

        private void frm_sqlConnection_Load(object sender, EventArgs e)
        {
            
        }

        //Saving database connection details and proceed.
        private void frm_sqlConnection_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
