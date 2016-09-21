using LRSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRSimulator.ViewModels
{
    public class SetViewModel
    {
        public Set Set { get; set; }
        public List<Card> Cards { get; set; }
        public Dictionary<string, bool> RarityFilter { get; set; }
        public Dictionary<string, bool> ColorFilter { get; set; }
        public CardListViewModel UserGrades { get; set; }
    }
}
