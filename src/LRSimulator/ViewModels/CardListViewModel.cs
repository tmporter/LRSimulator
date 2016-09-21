using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRSimulator.ViewModels
{
    public class CardListViewModel
    {
        public CardListViewModel()
        {
            CardViewModels = new List<CardViewModel>();
        }

        public List<CardViewModel> CardViewModels { get; set; }
    }
}
