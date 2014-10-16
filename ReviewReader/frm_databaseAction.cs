using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReviewReader
{

    public delegate void readFileIntoDB(string file, string writeTableName);
    public partial class frm_databaseAction : Form
    {
        private static Properties.Settings settings = new Properties.Settings();


        public frm_databaseAction()
        {
            InitializeComponent();
        }
        //public void readFileIntoDB(readFileIntoDB readFile)
        //{
        //    ReadFile(
        //}
        public void ReadFile(string file, string writeTableName)
        {

            Task.Run(() =>
            {

                //Look i added some comments
                int counter = 0;
                string line;
                bool isItemHead = false;
                bool nextLineIsItemName = false;
                bool nextLineIsTotalReview = false;
                bool nextLineIsFiveStar = false;
                bool isReviews = false;
                bool nextLineIsShortReview = false;
                bool nextLineIsReviewer = false;
                bool nextLineIsLongReview = false;
                int reviewLine = 0;
                List<Item> allItems = new List<Item>();

                Item reviewItem = new Item();
                List<Review> reviewsForItem = new List<Review>();
                Review itemReview = new Review();
                try
                {
                    // Read the file and display it line by line.
                    System.IO.StreamReader fileRead =
                       new System.IO.StreamReader(file);

                    //Open and read file while line is not ended
                    while ((line = fileRead.ReadLine()) != null)
                    {
                        line = line.Trim();
                        //Check if its a blank line
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            counter++;
                            continue;
                            //Skips WhiteSpace
                        }

                        //item detected initiate read
                        if (isItemHead)
                        {
                            //if this is the end of the set of reviews reset the item head
                            if (line.Trim() == "© 1996-2012, Amazon.com, Inc. or its affiliates")
                            {

                                isItemHead = false;
                                //add the reviewItem
                                allItems.Add(reviewItem);
                                //reset the list of reviews
                                reviewsForItem = new List<Review>();
                                //reset the reviewItem
                                reviewItem = new Item();
                                //set is reviews to false
                                isReviews = false;
                                counter++;
                                continue;
                            }

                            //if the review section is initiated
                            if (isReviews)
                            {
                                try
                                {
                                    //if the next line is the long review
                                    if (nextLineIsLongReview)
                                    {
                                        //checks for the end of the long review
                                        if (line == "Help other customers find the most helpful reviews")
                                        {
                                            nextLineIsLongReview = false;
                                            counter++;
                                            continue;
                                        }
                                        //add to the long review line by line
                                        itemReview.LongReview = itemReview.LongReview + " " + line;
                                        counter++;
                                        continue;
                                    }

                                    //check for long review
                                    if (line.IndexOf("This review is from: ") > -1)
                                    {
                                        itemReview.ReviewItem = line.Trim().Substring(21, line.Trim().Length - 21);
                                        counter++;
                                        nextLineIsLongReview = true;
                                        continue;
                                    }

                                    //if the next line is the reviewer
                                    if (nextLineIsReviewer)
                                    {
                                        line = line.Trim();
                                        //detects that there is a location
                                        if (line.IndexOf("(") > 0 && line.IndexOf(")") > 0)
                                        {
                                            //trims the reviewer around parameters
                                            int start = line.IndexOf("(") + 1;
                                            int end = line.IndexOf(")", start);
                                            itemReview.ReviewLocation = line.Substring(start, end - start);
                                            itemReview.Reviewer = line.Substring(3, start - 5);
                                            counter++;
                                            //resets theNextLineisReviewer
                                            nextLineIsReviewer = false;
                                            continue;
                                        }

                                        //if theree's no location
                                        if (line.IndexOf("-") > -1)
                                        {
                                            itemReview.Reviewer = line.Substring(3, line.IndexOf("-"));
                                        }
                                        else
                                        {
                                            itemReview.Reviewer = line.Trim();
                                        }
                                        itemReview.ReviewLocation = "Unknown";
                                        counter++;
                                        //next line is not reviewer
                                        nextLineIsReviewer = false;
                                        continue;

                                    }
                                    if (nextLineIsShortReview)
                                    {
                                        line = line.Trim();
                                        int ix1 = line.LastIndexOf(',');
                                        int ix2 = ix1 > 0 ? line.LastIndexOf(',', ix1 - 1) : -1;
                                        itemReview.ReviewDate = line.Substring(ix2 + 2, line.Length - (ix2 + 2));
                                        itemReview.ShortReview = line.Substring(0, ix2);
                                        nextLineIsReviewer = true;
                                        nextLineIsShortReview = false;
                                        counter++;
                                        continue;
                                    }
                                    if (line.Trim().IndexOf("found the following review helpful:") > -1)
                                    {
                                        int space1 = line.Trim().IndexOf(" ");
                                        int space2 = line.Trim().IndexOf(" ", space1 + 1);
                                        int space3 = line.Trim().IndexOf(" ", space2 + 1);
                                        itemReview.ReviewersOfReviewFoundHelpful = Convert.ToInt16(line.Trim().Substring(0, space1).Replace(",", ""));
                                        itemReview.ReviewersOfReview = Convert.ToInt16(line.Trim().Substring(space2 + 1, space3 - space2).Replace(",", ""));
                                        counter++;
                                        continue;
                                    }
                                    if (line.Trim().IndexOf("out of 5 stars") > -1 && line.Trim().IndexOf("customer reviews") == -1)
                                    {

                                        itemReview.StarsGiven = Convert.ToDecimal(line.Trim().Substring(0, 3));
                                        nextLineIsShortReview = true;
                                        counter++;
                                        continue;
                                    }

                                    if (line.IndexOf("PermalinkComment") > 0)
                                    {
                                        //END OF REVIEW
                                        reviewsForItem.Add(itemReview);
                                        reviewItem.Reviews = reviewsForItem;
                                        itemReview = new Review();
                                        counter++;
                                        continue;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    //Add in some error handling about what we missed here
                                    //reset all values to look for the new header
                                    isItemHead = false;
                                    nextLineIsItemName = false;
                                    nextLineIsTotalReview = false;
                                    nextLineIsFiveStar = false;
                                    isReviews = false;
                                    nextLineIsShortReview = false;
                                    nextLineIsReviewer = false;
                                    nextLineIsLongReview = false;
                                    counter++;
                                    continue;

                                }
                            }

                            //detects that we initiate a review
                            if (line == "Most Helpful First | Newest First")
                            {
                                isReviews = true;
                            }

                            if (nextLineIsFiveStar)
                            {
                                if (line.Trim().Substring(0, 1) == "(")
                                {

                                    int start = line.IndexOf("(") + 1;
                                    int end = line.IndexOf(")", start);
                                    if (reviewLine == 0)
                                        reviewItem.FiveStar = Convert.ToInt16(line.Substring(start, end - start));
                                    if (reviewLine == 1)
                                        reviewItem.FourStar = Convert.ToInt16(line.Substring(start, end - start));
                                    if (reviewLine == 2)
                                        reviewItem.ThreeStar = Convert.ToInt16(line.Substring(start, end - start));
                                    if (reviewLine == 3)
                                        reviewItem.TwoStar = Convert.ToInt16(line.Substring(start, end - start));
                                    if (reviewLine == 4)
                                    {
                                        reviewItem.OneStar = Convert.ToInt16(line.Substring(start, end - start));
                                        nextLineIsFiveStar = false;
                                        reviewLine = 0;
                                        counter++;
                                        continue;
                                    }
                                    reviewLine++;
                                    counter++;
                                    continue;
                                }
                                counter++;
                                continue;
                            }
                            if (nextLineIsTotalReview)
                            {
                                //SET TOTAL REVIEWS
                                //line.Trim()
                                reviewItem.NoOfReviews = Convert.ToInt16(line.Trim().Substring(0, line.Trim().IndexOf(" ")));
                                nextLineIsTotalReview = false;
                                nextLineIsFiveStar = true;
                                counter++;
                                continue;
                            }
                            if (nextLineIsItemName)
                            {
                                //SET ITEM NAME
                                reviewItem.ItemName = line;
                                nextLineIsItemName = false;
                                nextLineIsTotalReview = true;
                                counter++;
                                continue;
                            }
                            if (line.Trim() == "Customer Reviews")
                            {
                                nextLineIsItemName = true;
                                counter++;
                                continue;
                            }
                        }
                        if (line.IndexOf("Amazon.com: Customer Reviews:") > -1)
                        {
                            isItemHead = true;
                            counter++;
                            continue;
                            //THIS IS THE BEGINING OF ITEM
                        }
                        counter++;
                    }

                    //Write allItems to the db here
                    fileRead.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                //Writing the files to the db
                //This needs handling for a lot of shit
                try
                {
                    var connString = settings.ItemReviews;
                    MySqlConnection conn = new MySqlConnection(connString);
                    MySqlCommand command = conn.CreateCommand();
                    conn.Open();


                    command.CommandText = "INSERT INTO " + writeTableName + " VALUES(@ItemName, @ReviewersOfReview, @ReviewersOfReviewFoundHelpful, @StarsGiven, @ShortReview, @ReviewerId, @ReviewLocation, @IsAmazonVerifiedPurchase, @LongReview)";
                    command.Prepare();

                    if (allItems != null)
                    {
                        progressBar1.Step = allItems.Count / 100;
                        foreach (Item i in allItems)
                        {
                            //this.Invoke(new delegate void updateGUI(() => { progressBar1.PerformStep(); }));
                            //OnProgress(new object(), EventArgs.Empty);
                            if (i.Reviews != null)
                            {
                                foreach (Review r in i.Reviews)
                                {
                                    var trimLongReview = r.LongReview;

                                    if (r.LongReview != null)
                                    {
                                        if (r.LongReview.Length > 3999)
                                        {
                                            trimLongReview = r.LongReview.Substring(1, 3999);
                                        }
                                    }

                                    command.Parameters.Clear();
                                    //remove the ReviewId and ItemId, fuck em
                                    if (r.LongReview != null || r.LongReview != "")
                                    {
                                        Debug.WriteLine(r.ReviewItem);
                                        command.Parameters.AddWithValue("@ItemName", r.ReviewItem);
                                        command.Parameters.AddWithValue("@ReviewersOfReview", r.ReviewersOfReview);
                                        command.Parameters.AddWithValue("@ReviewersOfReviewFoundHelpful", r.ReviewersOfReviewFoundHelpful);
                                        command.Parameters.AddWithValue("@StarsGiven", r.StarsGiven);
                                        command.Parameters.AddWithValue("@ShortReview", r.ShortReview);
                                        command.Parameters.AddWithValue("@ReviewerId", r.Reviewer);
                                        command.Parameters.AddWithValue("@ReviewLocation", r.ReviewLocation);
                                        command.Parameters.AddWithValue("@IsAmazonVerifiedPurchase", r.IsAmazonVerifiedPurchase);
                                        command.Parameters.AddWithValue("@LongReview", trimLongReview);

                                        //add some error handling around this
                                        try
                                        {
                                            command.ExecuteNonQuery();
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    conn.Close();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                MessageBox.Show("Finished uploading");
            });
            // Suspend the screen.
            //Console.ReadLine();


        }

    }
    public class Review
    {
        public int ReviewersOfReview { get; set; }
        public int ReviewersOfReviewFoundHelpful { get; set; }
        public decimal StarsGiven { get; set; }
        public string ShortReview { get; set; }
        public string ReviewDate { get; set; }
        public string Reviewer { get; set; }
        public string ReviewLocation { get; set; }
        public bool IsAmazonVerifiedPurchase { get; set; }
        public string ReviewItem { get; set; }
        public string LongReview { get; set; }
    }
    //[Table(Name = "Items")]
    public class Item
    {
        public string ItemName { get; set; }
        public int NoOfReviews { get; set; }
        public int FiveStar { get; set; }
        public int FourStar { get; set; }
        public int ThreeStar { get; set; }
        public int TwoStar { get; set; }
        public int OneStar { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
