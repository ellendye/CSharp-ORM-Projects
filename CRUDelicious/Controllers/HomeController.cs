using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers
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
            ViewBag.Alldishes = _context.Dishes.OrderByDescending(l => l.DishId).ToList();
            
            return View();
        }
        [HttpGet("new")]
        public IActionResult New()
        {
            return View();
        }
        [HttpPost("Submit")]
        public IActionResult Submit(Dishes newDish)
        {
            if(ModelState.IsValid)
            {
                _context.Dishes.Add(newDish);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("New");            
        }

        [HttpGet("{id}")]
        public IActionResult ViewDish(int id)
        {
            ViewBag.OneDish = _context.Dishes
                .FirstOrDefault(dish => dish.DishId == id);
            return View();
        }
        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            Dishes delete = _context.Dishes
                .FirstOrDefault(dish => dish.DishId == id);

            _context.Dishes.Remove(delete);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            Dishes editDish = _context.Dishes
                .FirstOrDefault(dish => dish.DishId == id);

            return View(editDish);
        }
        [HttpPost("submitEdit")]
        public IActionResult SubmitEdit(Dishes editDish)
        {
            Dishes edit = _context.Dishes
                .FirstOrDefault(dish => dish.DishId == editDish.DishId);

            edit.Name = editDish.Name;
            edit.Chef = editDish.Chef;
            edit.Calories = editDish.Calories;
            edit.Tastiness = editDish.Tastiness;
            edit.Description = editDish.Description;
            edit.UpdatedAt = DateTime.Now;
            _context.SaveChanges();

            return RedirectToAction("ViewDish", new {id = editDish.DishId});
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
