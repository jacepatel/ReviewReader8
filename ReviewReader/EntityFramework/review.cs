namespace TextReader.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inb302.reviews")]
    public partial class review
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [Required]
        [StringLength(4000)]
        public string ItemName { get; set; }

        public int NumOfReviewRatings { get; set; }
        //change name

        public int ReviewersOfReviewFoundHelpful { get; set; }

        public int StarsGiven { get; set; }

        [StringLength(500)]
        public string ShortReview { get; set; }

        [StringLength(100)]
        public string ReviewerId { get; set; }

        [StringLength(100)]
        public string ReviewLocation { get; set; }

        [StringLength(4000)]
        public string LongReview { get; set; }

        [Column(TypeName = "bit")]
        public bool? IsAmazonVerifiedPurchase { get; set; }
    }
}
