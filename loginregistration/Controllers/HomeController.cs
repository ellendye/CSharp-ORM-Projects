using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using loginregistration.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace loginregistration.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;

        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginUser form)
        {
            if(ModelState.IsValid)
            {
                User userInDb = _context.Users.FirstOrDefault(u => u.Email == form.LoginEmail);
            // If no user exists with provided email
                if(userInDb == null)
                {
                // Add an error to ModelState and return to View!
                    ModelState.AddModelError("LoginEmail", "Invalid Email");
                    return View("Index");
                }
            
            // Initialize hasher object
                var hasher = new PasswordHasher<LoginUser>();
            
            // verify provided password against hash stored in db
                var result = hasher.VerifyHashedPassword(form, userInDb.Password, form.LoginPassword);
            
            // result can be compared to 0 for failure
                if(result == 0)
                {
                    ModelState.AddModelError("LoginPassword", "Invalid Password");
                    return View("Index");
                }
                return RedirectToAction("Success");
            }
            return View("Index");
        }
        [HttpPost("Register")]
        public IActionResult Register(User form)
        {
            if(ModelState.IsValid)
            {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                form.Password = Hasher.HashPassword(form, form.Password);
                _context.Users.Add(form);
                _context.SaveChanges();
                
                return RedirectToAction("Success");
            }
            return View("Index");
        }

        [HttpGet("Success")]
        public IActionResult Success()
        {
            return View();
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
