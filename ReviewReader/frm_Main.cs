﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReviewReader;
using TextReader.EntityFramework;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Windows;

namespace ReviewReader
{
    public partial class frm_Main : Form
    {
        private Properties.Settings settings = new Properties.Settings();
        string filePath = "";
        string currentTableName = "";
        public frm_Main()
        {
            InitializeComponent();




        }

        //Load the txt file that's been selected to the database
        private void loadDb_Click(object sender, EventArgs e)
        {
            if (cmb_TableNames.Text == "")
            {
                MessageBox.Show("Please select a table");
                return;
            }
            if (filePath != "")
            {
                MessageBox.Show("Uploading now.","Uploading");

                //Activare wait cursor, initiate the loading bar, load file to db
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                frm_databaseAction databaseAction = new frm_databaseAction(this);
                readFileIntoDB action = new readFileIntoDB(databaseAction.ReadFile);
                databaseAction.Show(this);
                this.Enabled = false;
                databaseAction.Invoke(action, filePath, cmb_TableNames.Text);
                //Program.ReadFile(filePath, cmb_TableNames.Text);
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;


            }
            else
            {
                MessageBox.Show("Please Select a file first");
            }
        }


        //Executes a command parsed at the db and returns relevant reviews
        public List<review> getReviewsFromDB(string commandText)
        {

            List<review> qryReviews = new List<review>();
            var connString = settings.ItemReviews;
            MySqlConnection conn = new MySqlConnection(connString);
            //Executes the recieved command text on the database stored in settings
            MySqlCommand command = conn.CreateCommand();
            try
            {
                conn.Open();
                command.CommandText = commandText;


                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    review r = new review();
                    try
                    {
                        r.ItemName = reader.GetString(0);
                        r.NumOfReviewRatings = reader.GetInt16(1);
                        r.ReviewersOfReviewFoundHelpful = reader.GetInt16(2);
                        r.StarsGiven = reader.GetInt16(3);
                        r.ShortReview = reader.GetString(4);
                        r.ReviewerId = reader.GetString(5);
                        r.ReviewLocation = reader.GetString(6);
                        r.IsAmazonVerifiedPurchase = reader.GetBoolean(7);
                        r.LongReview = reader.GetString(8);
                        qryReviews.Add(r);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }

                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return qryReviews;
        }

        //Parses the list of reviews and applies the updates to total reviews, total users, max reviews by user
        public void displayReviewsOnScreen(List<review> displayReviews)
        {
            List<review> qryReviews = displayReviews;

            var query = from c in qryReviews select c;
            var users = query.ToList();

            //Query the list for the numbers
            var totReviews = users.Count;
            var maxReviews = (from r in users
                              group r by r.ReviewerId into grp
                              select new { reviewer = grp.Key, reviews = grp.Count() }).OrderByDescending(x => x.reviews).FirstOrDefault();
            ;
            var totUsers = (from r in users
                            group r by r.ReviewerId into grp
                            select new { reviewer = grp.Key }).Distinct().Count();

            var maxReviewsbyUser = "0";
            if (totReviews.ToString() == null)
            {
                totReviews = 0;
                totUsers = 0;
            }
            else
            {
                if (maxReviews != null)
                    maxReviewsbyUser = maxReviews.reviews.ToString();

                else
                    maxReviewsbyUser = "0";
            }

            //Display the labels
            lbl_TotalReviews.Text = "Total Reviews: " + totReviews.ToString();
            //Add some error handling for max reviews when none selected
            lbl_MaxUserReviews.Text = "Most Reviews by a User: " + maxReviewsbyUser;
            lbl_TotalReviewers.Text = "Total Reviewers: " + totUsers.ToString();


        }

        //On Click event to show all reviews in selected table
        private void btn_ViewReviews_Click(object sender, EventArgs e)
        {
            if (cmb_TableNames.Text == null || cmb_TableNames.Text == "")
            {
                MessageBox.Show("Please select a table");
                return;
            }
            else
            {
                //sets teh command text and parses it the getReviewsFromDB Command
                string commandText = "select * from " + settings.selectedDatabase + "." + cmb_TableNames.Text;
                List<review> qryReviews = getReviewsFromDB(commandText);
                displayReviewsOnScreen(qryReviews);
                dgv_Reviews.DataSource = qryReviews;
            }

        }

        //Select the file to upload button on screen
        private void btn_SelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Filter = "txt files (*.txt)|*.txt|csv files (*.csv)|*.csv|All files (*.*)|*.*";
            fDialog.Title = "Open Amazon Reviews Txt File";

            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = fDialog.FileName.ToString();
                MessageBox.Show(fDialog.FileName.ToString(), "File Opened");
            }
        }

        //Select reviews with stars given
        private void btn_SelectReviewsWithParams_Click(object sender, EventArgs e)
        {
            if (cmb_TableNames.Text == null || cmb_TableNames.Text == "")
            {
                MessageBox.Show("Please select a table");
                return;
            }
            //Check if inputs are numbers
            double Num;
            bool isNum = double.TryParse(txt_StarsGivenHigh.Text, out Num);
            if (!isNum)
            {
                MessageBox.Show("Invalid number in inputs");
                txt_StarsGivenLow.Text = "";
                txt_StarsGivenHigh.Text = "";
                return;
            }

            isNum = double.TryParse(txt_StarsGivenLow.Text, out Num);
            if (!isNum)
            {
                MessageBox.Show("Invalid number in inputs");
                txt_StarsGivenLow.Text = "";
                txt_StarsGivenHigh.Text = "";
                return;
            }

            var starsGivenLow = Convert.ToInt16(txt_StarsGivenLow.Text);
            var starsGivenHigh = Convert.ToInt16(txt_StarsGivenHigh.Text);
            //Pases the command to the getReviewsFromDB command
            string commandText = "select * from " + settings.selectedDatabase + "." + cmb_TableNames.Text +
                " WHERE starsGiven >= " + starsGivenLow + " AND starsGiven <= " + starsGivenHigh;
            List<review> qryReviews = getReviewsFromDB(commandText);
            displayReviewsOnScreen(qryReviews);
            dgv_Reviews.DataSource = qryReviews;

            //Reset the values
            txt_StarsGivenLow.Text = "";
            txt_StarsGivenHigh.Text = "";


        }

        //Selects the reviews by users with set number of reviews
        private void btn_SelectUsersWithParams_Click(object sender, EventArgs e)
        {
            //Check if a table is selected
            if (cmb_TableNames.Text == "")
            {
                MessageBox.Show("Please select a table");
                return;
            }

            //Check if inputs are numbers
            double Num;
            bool isNum = double.TryParse(txt_ReviewsMadeLow.Text, out Num);
            if (!isNum)
            {
                MessageBox.Show("Invalid number in inputs");
                return;
            }

            isNum = double.TryParse(txt_ReviewsMadeHigh.Text, out Num);
            if (!isNum)
            {
                MessageBox.Show("Invalid number in inputs");
                return;
            }

            var reviewsLow = Convert.ToInt16(txt_ReviewsMadeLow.Text);
            var reviewsHigh = Convert.ToInt16(txt_ReviewsMadeHigh.Text);

            //Sets the command text with parameters and parses to the db
            string commandText = "select * from " + settings.selectedDatabase + "." + cmb_TableNames.Text +
                " WHERE ReviewerID IN " +
                "(SELECT ReviewerID FROM " + settings.selectedDatabase + "." + cmb_TableNames.Text + " " +
                "GROUP BY ReviewerID " +
                "HAVING COUNT(*) >= " + reviewsLow + " AND COUNT(*) <= " + reviewsHigh + ")";
            List<review> qryReviews = getReviewsFromDB(commandText);
            displayReviewsOnScreen(qryReviews);
            dgv_Reviews.DataSource = qryReviews;

            //CLear the boxes
            txt_ReviewsMadeLow.Text = "";
            txt_ReviewsMadeHigh.Text = "";
        }

        //Button to export the reviews to a text file
        private void btn_ExportToCSV_Click(object sender, EventArgs e)
        {
            //Checks for seleted table
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            if (cmb_TableNames.Text == "")
            {
                MessageBox.Show("Please select a table");
                return;
            }

            
            string openClose = "--";
            //Gets the data from the datasource
            List<review> printTable = (List<review>)dgv_Reviews.DataSource;
            var csv = new StringBuilder();
            SaveFileDialog fDialog = new SaveFileDialog();
            fDialog.Title = "Select File to Save To";
            fDialog.Filter = "Text File|*.txt";

            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = fDialog.FileName.ToString();



                File.Create(filePath).Dispose();

                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    foreach (review r in printTable)
                    {
                        sw.Write(string.Format("{0}{1}", openClose, Environment.NewLine));
                        //csv.Append(string.Format("{0}{1}", openClose, Environment.NewLine));

                        string helpfullness = r.ReviewersOfReviewFoundHelpful + "/" + r.NumOfReviewRatings;
                        //csv.Append(string.Format("{0}{1}", helpfullness, Environment.NewLine));
                        sw.Write(string.Format("{0}{1}", helpfullness, Environment.NewLine));

                        if (r.NumOfReviewRatings != 0)
                        {
                            decimal helpfullnessRating = decimal.Divide(r.ReviewersOfReviewFoundHelpful, r.NumOfReviewRatings);
                            //csv.Append(string.Format("{0}{1}", helpfullnessRating.ToString("#.##"), Environment.NewLine));
                            sw.Write(string.Format("{0}{1}", helpfullnessRating.ToString("#.##"), Environment.NewLine));
                        }
                        else
                        {
                            //csv.Append(string.Format("{0}{1}", "No Helpfullness Rating", Environment.NewLine));
                            sw.Write(string.Format("{0}{1}", "No Helpfullness Rating", Environment.NewLine));
                        }
                        //csv.Append(string.Format("{0}{1}", r.StarsGiven.ToString(), Environment.NewLine));
                        sw.Write(string.Format("{0}{1}", r.StarsGiven.ToString(), Environment.NewLine));
                        //csv.Append(string.Format("{0}{1}", r.ItemName, Environment.NewLine));
                        sw.Write(string.Format("{0}{1}", r.ItemName, Environment.NewLine));

                        string[] sentences = Regex.Split(r.LongReview, @"(?<=[\.!\?])\s+");
                        foreach (string sentence in sentences)
                        {
                            //csv.Append(string.Format("{0}{1}", sentence, Environment.NewLine));
                            sw.Write(string.Format("{0}{1}", sentence, Environment.NewLine));
                        }

                        //csv.Append(string.Format("{0}{1}", openClose, Environment.NewLine));
                        sw.Write(string.Format("{0}{1}", openClose, Environment.NewLine));
                    }
                }
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                MessageBox.Show(fDialog.FileName.ToString());
            }


            //File.WriteAllText(filePath, csv.ToString());

        }

        //On click of reviewer cell it displays all the reviews by the user
        private void dgv_Reviews_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (cmb_TableNames.Text == null || cmb_TableNames.Text == "")
            {
                MessageBox.Show("Please select a table");
                return;
            }
            //Checks which cell was clicked, 5 is reviewer ID
            if (dgv_Reviews.CurrentCell.ColumnIndex == 5)
            {
                string reviewerId = dgv_Reviews.CurrentCell.Value.ToString();

                //Selects the current cell adn 
                string commandText = "select * from " + settings.selectedDatabase + "." + cmb_TableNames.Text +
                    " WHERE ReviewerID = '" + reviewerId + "'";
                List<review> qryReviews = getReviewsFromDB(commandText);
                displayReviewsOnScreen(qryReviews);
                dgv_Reviews.DataSource = qryReviews;
                dgv_Reviews.ClearSelection();
            }
            else if (dgv_Reviews.CurrentCell.ColumnIndex == 0)
            {
                string ItemName = dgv_Reviews.CurrentCell.Value.ToString();

                string commandText = "select * from " + settings.selectedDatabase + "." + cmb_TableNames.Text +
                    " WHERE ItemName = '" + ItemName + "'";
                List<review> qryReviews = getReviewsFromDB(commandText);
                displayReviewsOnScreen(qryReviews);
                dgv_Reviews.DataSource = qryReviews;
                dgv_Reviews.ClearSelection();
            }
            else if (dgv_Reviews.CurrentCell.ColumnIndex == 7)
            {
                string review = dgv_Reviews.CurrentCell.Value.ToString();
                string[] sentences = Regex.Split(review, @"(?<=[\.!\?])\s+");
                review = "";
                foreach (string sentence in sentences)
                {
                    //csv.Append(string.Format("{0}{1}", sentence, Environment.NewLine));
                    review += string.Format("{0}{1}", sentence, Environment.NewLine);
                }
                    MessageBox.Show(review, "Long Review");
                //format

            }

        }
        //Unnecessary should be deleted
        private void label6_Click(object sender, EventArgs e)
        {
        }

        //Refreshes the combo box with up to date list of tables
        public void refreshComboBox()
        {
            cmb_TableNames.Items.Clear();
            List<string> tableNames = Program.getTablesInDatabase();
            foreach (string tn in tableNames)
            {
                NameValue n1 = new NameValue(tn, tn);
                cmb_TableNames.Items.Add(n1);
            }
            cmb_TableNames.Text = "";
            if (cmb_TableNames.Items.Count > 0)
                cmb_TableNames.SelectedIndex = 0;
        }

        //Create Table in Database button
        private void btn_createTable_Click(object sender, EventArgs e)
        {
            var tableName = Interaction.InputBox("Please Enter a Table Name (No Spaces, text characters only)", "Table Name", "ReviewsTableName");

            string re1 = "(([_?A-z0-9])+[_]?([A-z_0-9])+)";	// Variable Name 1
            if (tableName == "")
            {
                return;
            }
            Regex r = new Regex(re1, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = r.Match(tableName);
            if (m.Success)
            {

                bool tableSuccess = Program.createTable(tableName);


                refreshComboBox();
            }

            //Add Some Checks

        }


        //unnecessary
        private void cmb_TableNames_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        //Button to delete table
        private void btn_DeleteTable_Click(object sender, EventArgs e)
        {
            if (cmb_TableNames.Text.ToString() == null || cmb_TableNames.Text == "")
            {
                MessageBox.Show("Please select a table");
                return;
            }
            try
            {
                var connString = settings.ItemReviews;
                MySqlConnection conn = new MySqlConnection(connString);
                MySqlCommand command = conn.CreateCommand();
                conn.Open();
                command.CommandText = "DROP TABLE " + cmb_TableNames.Text;
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            refreshComboBox();

        }

        //Button to save current data to a new table
        private void btn_SaveToTable_Click(object sender, EventArgs e)
        {

            frm_databaseAction dbAction = new frm_databaseAction(this);
            saveTabletoDB saveDb = new saveTabletoDB(dbAction.saveTable);
            dbAction.Show();
            dbAction.Invoke(saveDb, dgv_Reviews);
            this.Enabled = false;           

        }


        //unecessary
        private void lbl_reviewbetween_Click(object sender, EventArgs e)
        {

        }


        //On the form main load
        private void frm_Main_Load(object sender, EventArgs e)
        {
            frm_sqlConnection sqlFrm = new frm_sqlConnection();
            if (sqlFrm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //males the combo box select only
                cmb_TableNames.DropDownStyle = ComboBoxStyle.DropDownList;

                //initialize the combo box values
                List<string> tableNames = Program.getTablesInDatabase();
                foreach (string tableName in tableNames)
                {
                    NameValue n1 = new NameValue(tableName, tableName);
                    cmb_TableNames.Items.Add(n1);
                }
                if (cmb_TableNames.Items.Count > 0)
                    cmb_TableNames.SelectedIndex = 0;
            }
            else
                Close();
        }


    }
}
