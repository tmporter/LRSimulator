using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRSimulator.Models
{
    public class Set
    {
        public string name { get; set; }
        public string code { get; set; }
        public string releaseDate { get; set; }

        public int? LRReviewID { get; set; }
    }
}
