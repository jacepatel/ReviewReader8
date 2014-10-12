using System;
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

namespace TextReader
{
    public partial class Form1 : Form
    {
        string filePath = "";
        string currentTableName = "";
        public Form1()
        {
            InitializeComponent();

            //initialize the combo box values
            List<string> tableNames = Program.getTablesInDatabase();
            foreach (string tableName in tableNames)
            {
                NameValue n1 = new NameValue(tableName, tableName);
                cmb_TableNames.Items.Add(n1);
            }
            
        }


        private void loadDb_Click(object sender, EventArgs e)
        {
            if (filePath != "")
            {
                MessageBox.Show("This may take up to 30 minutes, will be faster soon");
                Program.ReadFile(filePath, cmb_TableNames.Text);
            }
            else
            {
                MessageBox.Show("Please Select a file first");
            }
        }


        public static List<review> getReviewsFromDB(string commandText)
        {
            var connString = ConfigurationManager.ConnectionStrings["ItemReviews"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            conn.Open();
            command.CommandText = commandText;

            List<review> qryReviews = new List<review>();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                try{
                    review r = new review();
                    r.ItemName = reader.GetString(0);
                    r.ReviewersOfReview = reader.GetInt16(1);
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
                    //MessageBox.Show(ex.Message);
                }

            }

            return qryReviews;
        }

        public void displayReviewsOnScreen(List<review> displayReviews)
        {
            List<review> qryReviews = displayReviews;

            var query = from c in qryReviews select c;
            var users = query.ToList();

            //Hide 0, 2, 10

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
                maxReviewsbyUser = maxReviews.reviews.ToString();
            }

            lbl_TotalReviews.Text = "Total Reviews: " + totReviews.ToString();
            //Add some error handling for max reviews when none selected
            lbl_MaxUserReviews.Text = "Most Reviews by a User: " + maxReviewsbyUser;
            lbl_TotalReviewers.Text = "Total Reviewers: " + totUsers.ToString();


        }

        private void btn_ViewReviews_Click(object sender, EventArgs e)
        {
            if (cmb_TableNames.Text == null || cmb_TableNames.Text == "")
            {
                MessageBox.Show("Please select a table");
                return;
            }
            else
            {
                string commandText = "select * from inb302." + cmb_TableNames.Text;
                List<review> qryReviews = getReviewsFromDB(commandText);
                displayReviewsOnScreen(qryReviews);
                dgv_Reviews.DataSource = qryReviews;
            }

        }


        private void btn_SelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "Open Amazon Reviews Txt File";

            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = fDialog.FileName.ToString();
                MessageBox.Show(fDialog.FileName.ToString());
            }
        }

        private void btn_SelectReviewsWithParams_Click(object sender, EventArgs e)
        {

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
            string commandText = "select * from inb302." + cmb_TableNames.Text +
                " WHERE starsGiven >= " + starsGivenLow + " AND starsGiven <= " + starsGivenHigh; 
            List<review> qryReviews = getReviewsFromDB(commandText);
            displayReviewsOnScreen(qryReviews);
            dgv_Reviews.DataSource = qryReviews;

            //Reset the values
            txt_StarsGivenLow.Text = "";
            txt_StarsGivenHigh.Text = "";
            
        }

        private void btn_SelectUsersWithParams_Click(object sender, EventArgs e)
        {
            //Check if a table is selected
            if (cmb_TableNames.Text == null)
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

            string commandText = "select * from inb302." + cmb_TableNames.Text +
                " WHERE ReviewerID IN " + 
                "(SELECT ReviewerID FROM inb302."+cmb_TableNames.Text + " " + 
                "GROUP BY ReviewerID " + 
                "HAVING COUNT(*) >= "+reviewsLow+" AND COUNT(*) <= "+reviewsHigh+")";
            List<review> qryReviews = getReviewsFromDB(commandText);
            displayReviewsOnScreen(qryReviews);
            dgv_Reviews.DataSource = qryReviews; 

            //CLear the boxes
            txt_ReviewsMadeLow.Text = "";
            txt_ReviewsMadeHigh.Text = "";
        }

        private void btn_ExportToCSV_Click(object sender, EventArgs e)
        {
            string openClose = "--";
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

                        string helpfullness = r.ReviewersOfReviewFoundHelpful + "/" + r.ReviewersOfReview;
                        //csv.Append(string.Format("{0}{1}", helpfullness, Environment.NewLine));
                        sw.Write(string.Format("{0}{1}", helpfullness, Environment.NewLine));

                        if (r.ReviewersOfReview != 0)
                        {
                            decimal helpfullnessRating = decimal.Divide(r.ReviewersOfReviewFoundHelpful, r.ReviewersOfReview);
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
                MessageBox.Show(fDialog.FileName.ToString());
            }
           

            //File.WriteAllText(filePath, csv.ToString());

        }

        private void dgv_Reviews_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            //Add some error handling around this, for cell selection, maybe detect diff cells diff actions
            string reviewerId = dgv_Reviews.CurrentCell.Value.ToString();

            string commandText = "select * from inb302." + cmb_TableNames.Text +
                " WHERE ReviewerID = '" + reviewerId + "'";
            List<review> qryReviews = getReviewsFromDB(commandText);
            displayReviewsOnScreen(qryReviews);
            dgv_Reviews.DataSource = qryReviews; 
            

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

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
        }

        private void btn_createTable_Click(object sender, EventArgs e)
        {
            var tableName = Interaction.InputBox("Please Enter a Table Name (No Spaces, text characters only)", "Table Name", "ReviewsTableName");
            
            //Add Some Checks
            bool tableSuccess = Program.createTable(tableName);

            refreshComboBox();

        }

        private void cmb_TableNames_SelectedIndexChanged(object sender, EventArgs e)
        {

            
        }


        private void btn_DeleteTable_Click(object sender, EventArgs e)
        {
            if (cmb_TableNames.Text.ToString() == null)
            {
                MessageBox.Show("Please select a table");
                return;
            }
            var connString = ConfigurationManager.ConnectionStrings["ItemReviews"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            conn.Open();
            command.CommandText = "DROP TABLE " + cmb_TableNames.Text;
            command.ExecuteNonQuery();
            conn.Close();
            refreshComboBox();

        }

        private void btn_SaveToTable_Click(object sender, EventArgs e)
        {
            if (cmb_TableNames.Text == null)
            {
                MessageBox.Show("Please select a table");
                return;
            }

            List<review> printTable = (List<review>)dgv_Reviews.DataSource;

            var connString = ConfigurationManager.ConnectionStrings["ItemReviews"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            conn.Open();

            command.CommandText = "INSERT INTO " + cmb_TableNames.Text + " VALUES(@ItemName, @ReviewersOfReview, @ReviewersOfReviewFoundHelpful, @StarsGiven, @ShortReview, @ReviewerId, @ReviewLocation, @IsAmazonVerifiedPurchase, @LongReview)";
            command.Prepare();


            //Add some error handling if nothing is parsed
            foreach (review r in printTable)
            {
                command.Parameters.Clear();
                //remove the ReviewId and ItemId, fuck em
                command.Parameters.AddWithValue("@ItemName", r.ItemName);
                command.Parameters.AddWithValue("@ReviewersOfReview", r.ReviewersOfReview);
                command.Parameters.AddWithValue("@ReviewersOfReviewFoundHelpful", r.ReviewersOfReviewFoundHelpful);
                command.Parameters.AddWithValue("@StarsGiven", r.StarsGiven);
                command.Parameters.AddWithValue("@ShortReview", r.ShortReview);
                command.Parameters.AddWithValue("@ReviewerId", r.ReviewerId);
                command.Parameters.AddWithValue("@ReviewLocation", r.ReviewLocation);
                command.Parameters.AddWithValue("@IsAmazonVerifiedPurchase", r.IsAmazonVerifiedPurchase);
                command.Parameters.AddWithValue("@LongReview", r.LongReview);
                //add some error handling around this
                command.ExecuteNonQuery();
            }
            currentTableName = "";
            conn.Close();
        }
    }
}
