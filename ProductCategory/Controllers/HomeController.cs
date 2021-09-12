using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductCategory.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ProductCategory.Controllers
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
            @ViewBag.Products = _context.Products
                .ToList();

            return View();
        }
        [HttpPost("AddProduct")]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return Redirect("/");
            }
            @ViewBag.Products = _context.Products
                .ToList();
            return View("Index");
        }
        [HttpGet("/categories")]
        public IActionResult Categories()
        {
            @ViewBag.Categories = _context.Categories
                .ToList();
            return View();
        }
        [HttpPost("AddCategory")]
        public IActionResult AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return Redirect("/categories");
            }
            @ViewBag.Categories = _context.Categories
                .ToList();
            return View("Categories");
        }
        [HttpGet("product/{id}")]
        public IActionResult ViewProduct(int id)
        {
            ViewBag.SingleProduct = _context.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(p=> p.Category)
                .FirstOrDefault(i => i.ProductID == id);

            ViewBag.CatNotIn = _context.Categories
                .ToList();

            return View();
        }
        [HttpPost("AddCategoryToItem")]
        public IActionResult AddCategoryToItem(Association form)
        {
            if (ModelState.IsValid)
            {
                _context.Associations.Add(form);
                _context.SaveChanges();
                return RedirectToAction("ViewProduct", new {id=form.ProductID});
            }
            ViewBag.SingleProduct = _context.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(p=> p.Category)
                .FirstOrDefault(i => i.ProductID == form.ProductID);

            ViewBag.CatNotIn = _context.Categories
                .ToList();


            return View("ViewProduct");
        }
        [HttpGet("category/{id}")]
        public IActionResult ViewCategory(int id)
        {
            ViewBag.SingleCategory = _context.Categories
                .Include(p => p.ProductsInCategory)
                .ThenInclude(p=> p.Product)
                .FirstOrDefault(i => i.CategoryID == id);

            ViewBag.ProdNotIn = _context.Products
                .ToList();

            return View();
        }
        [HttpPost("AddProductTCat")]
        public IActionResult AddProductToCat(Association form)
        {
            if (ModelState.IsValid)
            {
                _context.Associations.Add(form);
                _context.SaveChanges();
                return RedirectToAction("ViewCategory", new {id=form.CategoryID});
            }

            ViewBag.SingleCategory = _context.Categories
                .Include(p => p.ProductsInCategory)
                .ThenInclude(p=> p.Product)
                .FirstOrDefault(i => i.CategoryID == form.CategoryID);

            ViewBag.ProdNotIn = _context.Products
                .ToList();


            return RedirectToAction("ViewCategory",new {id=form.CategoryID});
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
