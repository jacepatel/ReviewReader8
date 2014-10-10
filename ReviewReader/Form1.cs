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
                Program.ReadFile(filePath, cmb_TableNames.Text.ToString());
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
            lbl_TotalReviews.Text = "Total Reviews: " + totReviews.ToString();
            //Add some error handling for max reviews when none selected
            lbl_MaxUserReviews.Text = "Most Reviews by a User: " + maxReviews.reviews.ToString();
            lbl_TotalReviewers.Text = "Total Reviewers: " + totUsers.ToString();


        }

        private void btn_ViewReviews_Click(object sender, EventArgs e)
        {

            string commandText = "select * from inb302." + cmb_TableNames.Text.ToString();
            List<review> qryReviews = getReviewsFromDB(commandText);
            displayReviewsOnScreen(qryReviews);
            dgv_Reviews.DataSource = qryReviews;

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
            var starsGivenLow = Convert.ToInt16(txt_StarsGivenLow.Text);
            var starsGivenHigh = Convert.ToInt16(txt_StarsGivenHigh.Text);
            string commandText = "select * from inb302." + cmb_TableNames.Text.ToString() +
                " WHERE starsGiven >= " + starsGivenLow + " AND starsGiven <= " + starsGivenHigh; 
            List<review> qryReviews = getReviewsFromDB(commandText);
            displayReviewsOnScreen(qryReviews);
            dgv_Reviews.DataSource = qryReviews;
            
        }

        private void btn_SelectUsersWithParams_Click(object sender, EventArgs e)
        {
            var reviewsLow = Convert.ToInt16(txt_ReviewsMadeLow.Text);
            var reviewsHigh = Convert.ToInt16(txt_ReviewsMadeHigh.Text);

            string commandText = "select * from inb302." + cmb_TableNames.Text.ToString() +
                " WHERE ReviewerID IN " + 
                "(SELECT ReviewerID FROM inb302.reviews " + 
                "GROUP BY ReviewerID " + 
                "HAVING COUNT(*) >= "+reviewsLow+" AND COUNT(*) <= "+reviewsHigh+")";
            List<review> qryReviews = getReviewsFromDB(commandText);
            displayReviewsOnScreen(qryReviews);
            dgv_Reviews.DataSource = qryReviews; 
        }

        private void btn_ExportToCSV_Click(object sender, EventArgs e)
        {
            string openClose = "--";
            List<review> printTable = (List<review>)dgv_Reviews.DataSource;
            var csv = new StringBuilder();
            foreach (review r in printTable)
            {
                csv.Append(string.Format("{0}{1}", openClose, Environment.NewLine));

                string helpfullness = r.ReviewersOfReviewFoundHelpful + "/" +  r.ReviewersOfReview;
                csv.Append(string.Format("{0}{1}", helpfullness, Environment.NewLine));

                if (r.ReviewersOfReview != 0)
                {
                    decimal helpfullnessRating = decimal.Divide(r.ReviewersOfReviewFoundHelpful, r.ReviewersOfReview);
                    csv.Append(string.Format("{0}{1}", helpfullnessRating.ToString("#.##"), Environment.NewLine));
                }
                else
                {
                    csv.Append(string.Format("{0}{1}", "No Helpfullness Rating", Environment.NewLine));
                }
                csv.Append(string.Format("{0}{1}", r.StarsGiven.ToString(), Environment.NewLine));

                csv.Append(string.Format("{0}{1}", r.ItemName, Environment.NewLine));

                string[] sentences = Regex.Split(r.LongReview, @"(?<=[\.!\?])\s+");
                foreach (string sentence in sentences)
                {
                    csv.Append(string.Format("{0}{1}", sentence, Environment.NewLine));
                }

                csv.Append(string.Format("{0}{1}", openClose, Environment.NewLine));
            }

            SaveFileDialog fDialog = new SaveFileDialog();
            fDialog.Title = "Select File to Save To";
            fDialog.Filter = "Text File|*.txt";

            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = fDialog.FileName.ToString();
                MessageBox.Show(fDialog.FileName.ToString());
            }

            File.Create(filePath).Dispose();

            File.WriteAllText(filePath, csv.ToString());

        }

        private void dgv_Reviews_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            //Add some error handling around this, for cell selection, maybe detect diff cells diff actions
            var context = new ItemReviews();
            string reviewerId = dgv_Reviews.CurrentCell.Value.ToString();
            var query = from c in context.reviews
                        where c.ReviewerId == reviewerId
                        select c;

            //

            List<review> users = query.ToList();

            dgv_Reviews.DataSource = users;

            var totReviews = users.Count;
            var maxReviews = (from r in users
                            group r by r.ReviewerId into grp
                            select new { reviewer = grp.Key, reviews = grp.Count() }).OrderByDescending(x => x.reviews).FirstOrDefault();
                           var totUsers = (from r in users
                                           group r by r.ReviewerId into grp
                                           select new { reviewer = grp.Key }).Distinct().Count();
            lbl_TotalReviews.Text = "Total Reviews: " + totReviews.ToString();
            lbl_MaxUserReviews.Text = "Most Reviews by a User: " + maxReviews.reviews.ToString();
            lbl_TotalReviewers.Text = "Total Reviewers: " + totUsers.ToString();

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
            currentTableName = "";
            
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
            command.CommandText = "DROP TABLE " + cmb_TableNames.Text.ToString();
            command.ExecuteNonQuery();
            conn.Close();
            refreshComboBox();

        }

        private void btn_SaveToTable_Click(object sender, EventArgs e)
        {

        }
    }
}
