using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using chefsndishes.Models;
using Microsoft.EntityFrameworkCore;


namespace chefsndishes.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;
        public HomeController(MyContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Chefs = _context.Chefs
                .Include(dish => dish.CreatedDishes)
                .ToList();

            return View();
        }
        [HttpGet("dishes")]
        public IActionResult Dishes()
        {
            ViewBag.Dishes = _context.Dishes
                .Include(chef => chef.Chef)
                .ToList();

            return View();
        }
        [HttpGet("new")]
        public IActionResult AddChef()
        {
            return View();
        }
        [HttpPost("Submit")]
        public IActionResult Submit(Chef newChef)
        {
            if(ModelState.IsValid)
            {
                _context.Chefs.Add(newChef);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("AddChef");
        }
        [HttpGet("dishes/new")]
        public IActionResult AddDish()
        {
            ViewBag.AllChefs = _context.Chefs
                .ToList();
            
            return View();
        }
        [HttpPost("SubmitDish")]
        public IActionResult SubmitDish(Dish newDish)
        {
            if(ModelState.IsValid)
            {
                _context.Dishes.Add(newDish);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AllChefs = _context.Chefs
                .ToList();
            return View("AddDish");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
