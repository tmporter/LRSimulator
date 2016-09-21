using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LRSimulator.Models
{
    public class Grade
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public int SetReviewID { get; set; }
        public string CardName { get; set; }
        public int? MultiverseID { get; set; }
        public GradeEnum Value { get; set; }

        [ForeignKey("SetReviewID")]
        public virtual SetReview SetReview { get; set; }
    }
}
