using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BankAccounts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BankAccounts.Controllers
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
            return View();
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
                
                return RedirectToAction("Account", new {id = form.UserId});
            }
            return View("Index");
        }
        [HttpGet("/login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost("LoginUser")]
        public IActionResult LoginUser(LoginUser form)
        {
            if(ModelState.IsValid)
            {
                User userInDb = _context.Users.FirstOrDefault(u => u.Email == form.LoginEmail);
            // If no user exists with provided email
                if(userInDb == null)
                {
                // Add an error to ModelState and return to View!
                    ModelState.AddModelError("LoginEmail", "Invalid Email");
                    return View("Login");
                }
            
            // Initialize hasher object
                var hasher = new PasswordHasher<LoginUser>();
            
            // verify provided password against hash stored in db
                var result = hasher.VerifyHashedPassword(form, userInDb.Password, form.LoginPassword);
            
            // result can be compared to 0 for failure
                if(result == 0)
                {
                    ModelState.AddModelError("LoginPassword", "Invalid Password");
                    return View("Login");
                }
                return RedirectToAction("Account", new {id=userInDb.UserId});
            }
            return View("Login");
        }
        [HttpGet("Account/{id}")]
        public IActionResult Account(int id)
        {
            ViewBag.SingleUser = _context.Transactions
                .Include(transaction => transaction.User)
                .OrderByDescending(amt => amt.CreatedAt)
                .Where(us => us.UserID == id)
                .ToList();

            ViewBag.UserAccountBalance = _context.Transactions
                .Where(us => us.UserID == id)
                .Sum(amt => amt.Amount);

            return View();
        }
        [HttpPost("EditAmount")]
        public IActionResult EditAmount(Transaction Amt)
        {
            var Balance = _context.Transactions
                .Where(us => us.UserID == Amt.UserID)
                .Sum(amt => amt.Amount);
            if(Balance + Amt.Amount < 0)
            {
                ModelState.AddModelError("Amount", "Account can't go into the negatives");
                return RedirectToAction("Account", new {id=Amt.UserID});
            }
            _context.Add(Amt);
            _context.SaveChanges();
            return RedirectToAction("Account", new {id=Amt.UserID});
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
