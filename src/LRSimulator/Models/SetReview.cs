using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LRSimulator.Models
{
    public class SetReview
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string SetCode { get; set; }

        public virtual List<Grade> Grades { get; set; }
    }
}
