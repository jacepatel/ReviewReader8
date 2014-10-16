namespace ReviewReader
{
    partial class frm_Main
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
            this.loadDb = new System.Windows.Forms.Button();
            this.dgv_Reviews = new System.Windows.Forms.DataGridView();
            this.btn_ViewReviews = new System.Windows.Forms.Button();
            this.btn_SelectUsersWithParams = new System.Windows.Forms.Button();
            this.btn_SelectFile = new System.Windows.Forms.Button();
            this.txt_StarsGivenLow = new System.Windows.Forms.TextBox();
            this.lbl_starsbetween = new System.Windows.Forms.Label();
            this.txt_StarsGivenHigh = new System.Windows.Forms.TextBox();
            this.txt_ReviewsMadeHigh = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_ReviewsMadeLow = new System.Windows.Forms.TextBox();
            this.lbl_and1 = new System.Windows.Forms.Label();
            this.btn_ExportToCSV = new System.Windows.Forms.Button();
            this.lbl_reviewbetween = new System.Windows.Forms.Label();
            this.btn_SelectReviewsParams = new System.Windows.Forms.Button();
            this.lbl_and2 = new System.Windows.Forms.Label();
            this.lbl_TotalReviews = new System.Windows.Forms.Label();
            this.lbl_TotalReviewers = new System.Windows.Forms.Label();
            this.lbl_MaxUserReviews = new System.Windows.Forms.Label();
            this.btn_createTable = new System.Windows.Forms.Button();
            this.cmb_TableNames = new System.Windows.Forms.ComboBox();
            this.lbl_DropDown = new System.Windows.Forms.Label();
            this.btn_DeleteTable = new System.Windows.Forms.Button();
            this.btn_SaveToTable = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Reviews)).BeginInit();
            this.SuspendLayout();
            // 
            // loadDb
            // 
            this.loadDb.Location = new System.Drawing.Point(114, 7);
            this.loadDb.Name = "loadDb";
            this.loadDb.Size = new System.Drawing.Size(105, 23);
            this.loadDb.TabIndex = 1;
            this.loadDb.Text = "Load File to Table";
            this.loadDb.UseVisualStyleBackColor = true;
            this.loadDb.Click += new System.EventHandler(this.loadDb_Click);
            // 
            // dgv_Reviews
            // 
            this.dgv_Reviews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Reviews.Location = new System.Drawing.Point(13, 161);
            this.dgv_Reviews.Name = "dgv_Reviews";
            this.dgv_Reviews.Size = new System.Drawing.Size(792, 346);
            this.dgv_Reviews.TabIndex = 2;
            this.dgv_Reviews.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Reviews_CellContentClick);
            // 
            // btn_ViewReviews
            // 
            this.btn_ViewReviews.Location = new System.Drawing.Point(3, 99);
            this.btn_ViewReviews.Name = "btn_ViewReviews";
            this.btn_ViewReviews.Size = new System.Drawing.Size(216, 26);
            this.btn_ViewReviews.TabIndex = 3;
            this.btn_ViewReviews.Text = "View All Reviews in Selected Table";
            this.btn_ViewReviews.UseVisualStyleBackColor = true;
            this.btn_ViewReviews.Click += new System.EventHandler(this.btn_ViewReviews_Click);
            // 
            // btn_SelectUsersWithParams
            // 
            this.btn_SelectUsersWithParams.Location = new System.Drawing.Point(555, 37);
            this.btn_SelectUsersWithParams.Name = "btn_SelectUsersWithParams";
            this.btn_SelectUsersWithParams.Size = new System.Drawing.Size(261, 23);
            this.btn_SelectUsersWithParams.TabIndex = 5;
            this.btn_SelectUsersWithParams.Text = "Select Reviews By Number Given";
            this.btn_SelectUsersWithParams.UseVisualStyleBackColor = true;
            this.btn_SelectUsersWithParams.Click += new System.EventHandler(this.btn_SelectUsersWithParams_Click);
            // 
            // btn_SelectFile
            // 
            this.btn_SelectFile.Location = new System.Drawing.Point(3, 7);
            this.btn_SelectFile.Name = "btn_SelectFile";
            this.btn_SelectFile.Size = new System.Drawing.Size(105, 23);
            this.btn_SelectFile.TabIndex = 6;
            this.btn_SelectFile.Text = "Select File";
            this.btn_SelectFile.UseVisualStyleBackColor = true;
            this.btn_SelectFile.Click += new System.EventHandler(this.btn_SelectFile_Click);
            // 
            // txt_StarsGivenLow
            // 
            this.txt_StarsGivenLow.Location = new System.Drawing.Point(418, 13);
            this.txt_StarsGivenLow.Name = "txt_StarsGivenLow";
            this.txt_StarsGivenLow.Size = new System.Drawing.Size(40, 22);
            this.txt_StarsGivenLow.TabIndex = 7;
            // 
            // lbl_starsbetween
            // 
            this.lbl_starsbetween.AutoSize = true;
            this.lbl_starsbetween.Location = new System.Drawing.Point(305, 19);
            this.lbl_starsbetween.Name = "lbl_starsbetween";
            this.lbl_starsbetween.Size = new System.Drawing.Size(140, 17);
            this.lbl_starsbetween.TabIndex = 8;
            this.lbl_starsbetween.Text = "Stars Given Between";
            // 
            // txt_StarsGivenHigh
            // 
            this.txt_StarsGivenHigh.Location = new System.Drawing.Point(495, 12);
            this.txt_StarsGivenHigh.Name = "txt_StarsGivenHigh";
            this.txt_StarsGivenHigh.Size = new System.Drawing.Size(40, 22);
            this.txt_StarsGivenHigh.TabIndex = 9;
            // 
            // txt_ReviewsMadeHigh
            // 
            this.txt_ReviewsMadeHigh.Location = new System.Drawing.Point(495, 39);
            this.txt_ReviewsMadeHigh.Name = "txt_ReviewsMadeHigh";
            this.txt_ReviewsMadeHigh.Size = new System.Drawing.Size(40, 22);
            this.txt_ReviewsMadeHigh.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(181, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 17);
            this.label2.TabIndex = 11;
            // 
            // txt_ReviewsMadeLow
            // 
            this.txt_ReviewsMadeLow.Location = new System.Drawing.Point(418, 39);
            this.txt_ReviewsMadeLow.Name = "txt_ReviewsMadeLow";
            this.txt_ReviewsMadeLow.Size = new System.Drawing.Size(40, 22);
            this.txt_ReviewsMadeLow.TabIndex = 10;
            // 
            // lbl_and1
            // 
            this.lbl_and1.AutoSize = true;
            this.lbl_and1.Location = new System.Drawing.Point(464, 15);
            this.lbl_and1.Name = "lbl_and1";
            this.lbl_and1.Size = new System.Drawing.Size(32, 17);
            this.lbl_and1.TabIndex = 13;
            this.lbl_and1.Text = "and";
            // 
            // btn_ExportToCSV
            // 
            this.btn_ExportToCSV.Location = new System.Drawing.Point(575, 513);
            this.btn_ExportToCSV.Name = "btn_ExportToCSV";
            this.btn_ExportToCSV.Size = new System.Drawing.Size(141, 27);
            this.btn_ExportToCSV.TabIndex = 14;
            this.btn_ExportToCSV.Text = "Export Data to Text File";
            this.btn_ExportToCSV.UseVisualStyleBackColor = true;
            this.btn_ExportToCSV.Click += new System.EventHandler(this.btn_ExportToCSV_Click);
            // 
            // lbl_reviewbetween
            // 
            this.lbl_reviewbetween.AutoSize = true;
            this.lbl_reviewbetween.Location = new System.Drawing.Point(273, 42);
            this.lbl_reviewbetween.Name = "lbl_reviewbetween";
            this.lbl_reviewbetween.Size = new System.Drawing.Size(183, 17);
            this.lbl_reviewbetween.TabIndex = 15;
            this.lbl_reviewbetween.Text = "Number of Reviews by User";
            this.lbl_reviewbetween.Click += new System.EventHandler(this.lbl_reviewbetween_Click);
            // 
            // btn_SelectReviewsParams
            // 
            this.btn_SelectReviewsParams.Location = new System.Drawing.Point(555, 7);
            this.btn_SelectReviewsParams.Name = "btn_SelectReviewsParams";
            this.btn_SelectReviewsParams.Size = new System.Drawing.Size(261, 23);
            this.btn_SelectReviewsParams.TabIndex = 16;
            this.btn_SelectReviewsParams.Text = "Select Reviews By Stars Given";
            this.btn_SelectReviewsParams.UseVisualStyleBackColor = true;
            this.btn_SelectReviewsParams.Click += new System.EventHandler(this.btn_SelectReviewsWithParams_Click);
            // 
            // lbl_and2
            // 
            this.lbl_and2.AutoSize = true;
            this.lbl_and2.Location = new System.Drawing.Point(464, 42);
            this.lbl_and2.Name = "lbl_and2";
            this.lbl_and2.Size = new System.Drawing.Size(32, 17);
            this.lbl_and2.TabIndex = 17;
            this.lbl_and2.Text = "and";
            // 
            // lbl_TotalReviews
            // 
            this.lbl_TotalReviews.AutoSize = true;
            this.lbl_TotalReviews.Location = new System.Drawing.Point(12, 145);
            this.lbl_TotalReviews.Name = "lbl_TotalReviews";
            this.lbl_TotalReviews.Size = new System.Drawing.Size(112, 17);
            this.lbl_TotalReviews.TabIndex = 18;
            this.lbl_TotalReviews.Text = "Total Reviews: 0";
            // 
            // lbl_TotalReviewers
            // 
            this.lbl_TotalReviewers.AutoSize = true;
            this.lbl_TotalReviewers.Location = new System.Drawing.Point(181, 145);
            this.lbl_TotalReviewers.Name = "lbl_TotalReviewers";
            this.lbl_TotalReviewers.Size = new System.Drawing.Size(125, 17);
            this.lbl_TotalReviewers.TabIndex = 19;
            this.lbl_TotalReviewers.Text = "Total Reviewers: 0";
            this.lbl_TotalReviewers.Click += new System.EventHandler(this.label6_Click);
            // 
            // lbl_MaxUserReviews
            // 
            this.lbl_MaxUserReviews.AutoSize = true;
            this.lbl_MaxUserReviews.Location = new System.Drawing.Point(327, 145);
            this.lbl_MaxUserReviews.Name = "lbl_MaxUserReviews";
            this.lbl_MaxUserReviews.Size = new System.Drawing.Size(170, 17);
            this.lbl_MaxUserReviews.TabIndex = 20;
            this.lbl_MaxUserReviews.Text = "Max Reviews by a User: 0";
            // 
            // btn_createTable
            // 
            this.btn_createTable.Location = new System.Drawing.Point(3, 36);
            this.btn_createTable.Name = "btn_createTable";
            this.btn_createTable.Size = new System.Drawing.Size(105, 23);
            this.btn_createTable.TabIndex = 21;
            this.btn_createTable.Text = "Create Table";
            this.btn_createTable.UseVisualStyleBackColor = true;
            this.btn_createTable.Click += new System.EventHandler(this.btn_createTable_Click);
            // 
            // cmb_TableNames
            // 
            this.cmb_TableNames.FormattingEnabled = true;
            this.cmb_TableNames.Location = new System.Drawing.Point(85, 69);
            this.cmb_TableNames.Name = "cmb_TableNames";
            this.cmb_TableNames.Size = new System.Drawing.Size(134, 24);
            this.cmb_TableNames.TabIndex = 22;
            this.cmb_TableNames.SelectedIndexChanged += new System.EventHandler(this.cmb_TableNames_SelectedIndexChanged);
            // 
            // lbl_DropDown
            // 
            this.lbl_DropDown.AutoSize = true;
            this.lbl_DropDown.Location = new System.Drawing.Point(6, 72);
            this.lbl_DropDown.Name = "lbl_DropDown";
            this.lbl_DropDown.Size = new System.Drawing.Size(87, 17);
            this.lbl_DropDown.TabIndex = 23;
            this.lbl_DropDown.Text = "Select Table";
            // 
            // btn_DeleteTable
            // 
            this.btn_DeleteTable.Location = new System.Drawing.Point(114, 36);
            this.btn_DeleteTable.Name = "btn_DeleteTable";
            this.btn_DeleteTable.Size = new System.Drawing.Size(105, 23);
            this.btn_DeleteTable.TabIndex = 24;
            this.btn_DeleteTable.Text = "Delete Table";
            this.btn_DeleteTable.UseVisualStyleBackColor = true;
            this.btn_DeleteTable.Click += new System.EventHandler(this.btn_DeleteTable_Click);
            // 
            // btn_SaveToTable
            // 
            this.btn_SaveToTable.Location = new System.Drawing.Point(424, 513);
            this.btn_SaveToTable.MaximumSize = new System.Drawing.Size(125, 27);
            this.btn_SaveToTable.MinimumSize = new System.Drawing.Size(125, 27);
            this.btn_SaveToTable.Name = "btn_SaveToTable";
            this.btn_SaveToTable.Size = new System.Drawing.Size(125, 27);
            this.btn_SaveToTable.TabIndex = 25;
            this.btn_SaveToTable.Text = "Save to Table";
            this.btn_SaveToTable.UseVisualStyleBackColor = true;
            this.btn_SaveToTable.Click += new System.EventHandler(this.btn_SaveToTable_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(9, 517);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(217, 23);
            this.progressBar1.TabIndex = 26;
            this.progressBar1.Visible = false;
            // 
            // frm_Main
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(817, 552);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btn_SaveToTable);
            this.Controls.Add(this.btn_DeleteTable);
            this.Controls.Add(this.lbl_DropDown);
            this.Controls.Add(this.cmb_TableNames);
            this.Controls.Add(this.btn_createTable);
            this.Controls.Add(this.lbl_MaxUserReviews);
            this.Controls.Add(this.lbl_TotalReviewers);
            this.Controls.Add(this.lbl_TotalReviews);
            this.Controls.Add(this.lbl_and2);
            this.Controls.Add(this.btn_SelectReviewsParams);
            this.Controls.Add(this.lbl_reviewbetween);
            this.Controls.Add(this.btn_ExportToCSV);
            this.Controls.Add(this.lbl_and1);
            this.Controls.Add(this.txt_ReviewsMadeHigh);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_ReviewsMadeLow);
            this.Controls.Add(this.txt_StarsGivenHigh);
            this.Controls.Add(this.lbl_starsbetween);
            this.Controls.Add(this.txt_StarsGivenLow);
            this.Controls.Add(this.btn_SelectFile);
            this.Controls.Add(this.btn_SelectUsersWithParams);
            this.Controls.Add(this.btn_ViewReviews);
            this.Controls.Add(this.dgv_Reviews);
            this.Controls.Add(this.loadDb);
            this.Name = "frm_Main";
            this.Text = "Review Reader";
            this.Load += new System.EventHandler(this.frm_Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Reviews)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button loadDb;
        private System.Windows.Forms.DataGridView dgv_Reviews;
        private System.Windows.Forms.Button btn_ViewReviews;
        private System.Windows.Forms.Button btn_SelectUsersWithParams;
        private System.Windows.Forms.Button btn_SelectFile;
        private System.Windows.Forms.TextBox txt_StarsGivenLow;
        private System.Windows.Forms.Label lbl_starsbetween;
        private System.Windows.Forms.TextBox txt_StarsGivenHigh;
        private System.Windows.Forms.TextBox txt_ReviewsMadeHigh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_ReviewsMadeLow;
        private System.Windows.Forms.Label lbl_and1;
        private System.Windows.Forms.Button btn_ExportToCSV;
        private System.Windows.Forms.Label lbl_reviewbetween;
        private System.Windows.Forms.Button btn_SelectReviewsParams;
        private System.Windows.Forms.Label lbl_and2;
        private System.Windows.Forms.Label lbl_TotalReviews;
        private System.Windows.Forms.Label lbl_TotalReviewers;
        private System.Windows.Forms.Label lbl_MaxUserReviews;
        private System.Windows.Forms.Button btn_createTable;
        private System.Windows.Forms.ComboBox cmb_TableNames;
        private System.Windows.Forms.Label lbl_DropDown;
        private System.Windows.Forms.Button btn_DeleteTable;
        private System.Windows.Forms.Button btn_SaveToTable;
        private System.Windows.Forms.ProgressBar progressBar1;

        

        //this is handling for the tableselectbox
        public class NameValue
        {

           private string dataName ;
           private string dataValue ;

           public NameValue( string dataName, string dataValue)
          {
              DataName = dataName;
              DataValue = dataValue ;
           }

           public string DataName
           {
             get{ return dataName ;}
             set{ dataName = value ; }
            }

           public string DataValue
           {
               get{ return dataValue ;}
             set{ dataValue = value ; }
           }

           public override string ToString()
           {
             return dataName ;
            }

}

    }
}

