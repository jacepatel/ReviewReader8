using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity.Validation;
using TextReader.EntityFramework;
using TextReader;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace ReviewReader
{
    public static class Program
    {
        public delegate void progress(object param, EventArgs e);
        public delegate void initprogress(object param, EventArgs e);
        public static event progress _progress;
        public static event initprogress _initprogress;
        private static Properties.Settings settings = new Properties.Settings();
        public class Review
        {
            public int NumOfReviewRatings { get; set; }
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

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frm_Main());
            //string file = "C:\\Users\\jacep_000\\Downloads\\Amazon Review Dataset.txt";
            //ReadFile(file);

        }
        //Creates a table in the dbv
        public static bool createTable(string tableName)
        {

            try
            {
                var connString = settings.ItemReviews;
                MySqlConnection conn = new MySqlConnection(connString);
                MySqlCommand command = conn.CreateCommand();
                conn.Open();
                command.CommandText = "CREATE TABLE `" + tableName + "` (" +
              "`ItemName` varchar(4000) NOT NULL, " +
              "`NumOfReviewRatings` int(11) NOT NULL," +
              "`ReviewersOfReviewFoundHelpful` int(11) NOT NULL," +
              "`StarsGiven` int(11) NOT NULL," +
              "`ShortReview` varchar(500) DEFAULT NULL," +
              "`ReviewerId` varchar(100) DEFAULT NULL," +
              "`ReviewLocation` varchar(100) DEFAULT NULL," +
              "`IsAmazonVerifiedPurchase` bit(1) DEFAULT NULL," +
              "`LongReview` varchar(4000) DEFAULT NULL" +
            ") ENGINE=InnoDB DEFAULT CHARSET=latin1";
                command.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                
                MessageBox.Show(ex.Message);
                return false;
            }
            //add some handling as to whether this create is successful or not
           
        }

        //Retrieves teh tables in db
        public static List<string> getTablesInDatabase()
        {

            var connString = settings.ItemReviews;
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            conn.Open();
            command.CommandText = "select table_name from information_schema.tables" +
            " WHERE TABLE_Schema = '" + settings.selectedDatabase + "'" +
            " and table_name <> 'items'";

            List<string> tableNames = new List<string>();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                tableNames.Add(reader.GetString(0));
            }

            // Call Close when done reading.
            reader.Close();
            conn.Close();
            //add some handling as to whether this create is successful or not
            return tableNames;
        }
                
        
        private static void OnInitprogress(object sender, EventArgs e)
        {
            if (_initprogress != null)
                _initprogress(sender, e);
        }
        private static void OnProgress(object sender, EventArgs e)
        {
            if (_progress != null)
                _progress(sender, e);
        }
    }
}
