using LRSimulator.DAL;
using LRSimulator.Models;
using LRSimulator.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LRSimulator.Controllers
{
    public class SetController : Controller
    {
        private ApplicationDbContext db;

        public SetController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Index(string id = "")
        {
            var set = new Set();
            var cards = new List<Card>();

            if (id == "KLD" || id == "kld")
            {
                set = new Set()
                {
                    name = "Kaladesh",
                    code = "KLD",
                    releaseDate = "2016-09-30"
                };

                using (StreamReader r = System.IO.File.OpenText(@"wwwroot/js/KLD.json"))
                {
                    string json = r.ReadToEnd();
                    var cardDTO = JsonConvert.DeserializeObject<CardDTO>(json);
                    cards = cardDTO.Cards;
                }
            }
            else
            {
                set = MTGAPI.GetSetBySetCode(id);
                cards = MTGAPI.GetCardsBySetCode(id);
            }

            var lrSetReviewQuery = db.SetReviews
                .Where(sr => sr.SetCode.Equals(id, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            var lrSetReview = (lrSetReviewQuery == null) ? new List<Grade>() : lrSetReviewQuery.Grades;
            
            var rarityFilter = new Dictionary<string, bool>() {
                { "Common", false },
                { "Uncommon", false },
                { "Rare", false },
                { "Mythic Rare", false },
            };

            var colorFilter = new Dictionary<string, bool>() {
                { "White",  false },
                { "Blue", false },
                { "Black", false },
                { "Red", false },
                { "Green", false },
                { "Colorless", false }
            };

            cards = cards
                .Where(c => !c.type.Contains("Basic Land"))
                .OrderBy(c => c.number, new CardComparer())
                .ToList();

            var userGrades = new CardListViewModel();
            foreach (var card in cards)
            {
                var cvm = new CardViewModel()
                {
                    Card = card,
                    Grade = new Grade()
                    {
                        CardName = card.name,
                        MultiverseID = card.multiverseid ?? -1,
                        Value = GradeEnum.None
                    }
                };

                userGrades.CardViewModels.Add(cvm);
            }

            return View(new SetViewModel()
            {
                RarityFilter = rarityFilter,
                ColorFilter = colorFilter,
                Set = set,
                Cards = cards,
                UserGrades = userGrades
            });
        }

        [HttpPost]
        public IActionResult Index(SetViewModel model)
        {
            try
            {
                var setCode = model.Set.code;
                var cards = new List<Card>();

                if (setCode == "KLD" || setCode == "kld")
                {
                    using (StreamReader r = System.IO.File.OpenText(@"wwwroot/js/KLD.json"))
                    {
                        string json = r.ReadToEnd();
                        var cardDTO = JsonConvert.DeserializeObject<CardDTO>(json);
                        cards = cardDTO.Cards;
                    }
                }
                else
                {
                    cards = MTGAPI.GetCardsBySetCode(setCode);
                }

                var rarityFilter = model.RarityFilter;
                var colorFilter = model.ColorFilter;

                var rarityPredicate = GetRarityFilter(rarityFilter);
                var colorPredicate = GetColorFilter(colorFilter);

                cards = cards
                    .Where(c => !c.type.Contains("Basic Land"))
                    .Where(rarityPredicate)
                    .Where(colorPredicate)
                    .OrderBy(c => c.number, new CardComparer())
                    .ToList();

                var userGrades = new CardListViewModel();
                foreach (var card in cards)
                {
                    var cvm = new CardViewModel()
                    {
                        Card = card,
                        Grade = new Grade()
                        {
                            CardName = card.name,
                            MultiverseID = card.multiverseid ?? -1,
                            Value = GradeEnum.None
                        }
                    };

                    userGrades.CardViewModels.Add(cvm);
                }

                return PartialView("EditorTemplates/CardListViewModel", userGrades);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", model.Set.code);
            }
        }

        [HttpPost]
        public IActionResult Export(List<CardViewModel> model)
        {
            return View();
        }

        private Func<Card, bool> GetColorFilter(Dictionary<string, bool> filter)
        {
            Func<Card, bool> predicate = card => true;

            if (filter.Any(r => r.Value))
                predicate = card => false;

            foreach (var color in filter)
            {
                if (color.Value)
                {
                    var copy = predicate;

                    if (color.Key == "Colorless")
                        predicate = card => copy(card) || (card.colors == null);
                    else
                        predicate = card => copy(card) || (card.colors != null && card.colors.Contains(color.Key));
                }
            }

            return predicate;
        }

        private Func<Card, bool> GetRarityFilter(Dictionary<string, bool> filter)
        {
            Func<Card, bool> predicate = card => true;

            if (filter.Any(r => r.Value))
                predicate = card => false;

            foreach (var rarity in filter)
            {
                if (rarity.Value)
                {
                    var copy = predicate;
                    predicate = card => copy(card) || card.rarity == rarity.Key;
                }
            }

            return predicate;
        }
    }
}
