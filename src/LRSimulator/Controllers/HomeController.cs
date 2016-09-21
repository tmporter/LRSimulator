using LRSimulator.DAL;
using LRSimulator.Models;
using LRSimulator.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace LRSimulator.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var sets = MTGAPI.GetAllSets();

            sets.Add(new Set()
            {
                name = "Kaladesh",
                code = "KLD",
                releaseDate = "2016-09-30",
                LRReviewID = 1
            });

            return View(new HomeViewModel() {
                Sets = sets.OrderByDescending(s => s.releaseDate).ToList(),
                SearchString = ""
            });
        }
        
        [HttpPost]
        public IActionResult Index(HomeViewModel model)
        {
            var sets = MTGAPI.GetAllSets();

            sets.Add(new Set()
            {
                name = "Kaladesh",
                code = "KLD",
                releaseDate = "2016-09-30"
            });

            if (!string.IsNullOrEmpty(model.SearchString))
                sets = sets.Where(s => s.name.IndexOf(model.SearchString.Trim(), StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            return PartialView("Sets", sets.OrderByDescending(s => s.releaseDate).ToList());
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
