using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Core_CodeFirst.Models;

namespace Core_CodeFirst.Controllers
{
    public class ScoresController : Controller
    {
        private readonly List<Score> st_score;

        public ScoresController()
        {
            st_score = new List<Score>
            {
                new Score{Id=1, Name="朝陽", Chinese=50,English=35,Math=51,Art=50, Sport=90},
                new Score{Id=2, Name="立葵", Chinese=80,English=75,Math=77,Art=80, Sport=85},
                new Score{Id=3, Name="蓮", Chinese=70,English=65,Math=71,Art=100,Sport=80},
                new Score{Id=4, Name="木蓮", Chinese=90,English=85,Math=91,Art=90,Sport=88},
                new Score{Id=12, Name="椿", Chinese=95,English=95,Math=91,Art=89,Sport=95},
                new Score{Id=13, Name="山茶", Chinese=80,English=65,Math=81,Art=70,Sport=80},
                new Score{Id=14, Name="鈴白", Chinese=70,English=75,Math=81,Art=60,Sport=80},
                new Score{Id=15, Name="龍膽", Chinese=87,English=85,Math=91,Art=40,Sport=80},
                new Score{Id=9, Name="紫陽花", Chinese=80,English=75,Math=78,Art=70,Sport=59},
                new Score{Id=5, Name="紅李", Chinese=91,English=92,Math=90,Art=89,Sport=95}
            };
        }

        public IActionResult Index()
        {
            return RedirectToAction("Score");
            //return View("Score");
        }

        public IActionResult Score()
        {
            //calcuate the total score per record 
            st_score.ForEach(s => s.Total
                = s.Chinese + s.English + s.Math + s.Sport + s.Art);

            st_score.OrderByDescending(s => s.Total);

            //find top 1
            var topId = st_score.OrderByDescending(s => s.Total)
                .Select(s => s.Id)
                .FirstOrDefault();

            ViewBag.TopId = topId;

            //return View();
            return View(st_score);
        }

    }
}