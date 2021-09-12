using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeddingPlanner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace WeddingPlanner.Controllers
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
            int? loggedUserId = HttpContext.Session.GetInt32("userId");
            if(loggedUserId != null) return RedirectToAction("Dashboard");
            
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
                Console.WriteLine("logged in");
                HttpContext.Session.SetInt32("userId", userInDb.UserId);
                return RedirectToAction("Dashboard");
            }
            return View("Index");
        }
        [HttpPost("Register")]
        public IActionResult Register(User form)
        {
            if(ModelState.IsValid)
            {
                if(_context.Users.Any(user => user.Email == form.Email))
                {
                    ModelState.AddModelError("Email", "Email is already in use. Please log in.");

                    return View("Index");
                }

                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                form.Password = Hasher.HashPassword(form, form.Password);
                _context.Users.Add(form);
                _context.SaveChanges();
                Console.WriteLine("logged in");
                HttpContext.Session.SetInt32("userId", form.UserId);
                return RedirectToAction("Dashboard");
            }
            return View("Index");
        }
        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            int? loggedUserId = HttpContext.Session.GetInt32("userId");
            if(loggedUserId == null) return RedirectToAction("Index");

            // List<Wedding> weddings = _context.Weddings
            //     .Where(x => x.Date < DateTime.Now)
            //     .ToList();
            //for loop through weddings, delete each instance that is in the past.

            @ViewBag.LoggedUser = _context.Users.FirstOrDefault(user => user.UserId == loggedUserId);
            @ViewBag.AllWeddings = _context.Weddings
                .Include(w => w.Creator)
                .Include(w => w.Attendees)
                .ThenInclude(w => w.User)
                .ToList();
            @ViewBag.UserNoRSVP = _context.Weddings
                .Include(w => w.Attendees)
                .Where(w => w.Attendees.All(a=>a.UserId != (int)loggedUserId));
            return View();
        }
        [HttpGet("WeddingPlanner")]
        public IActionResult Wedding()
        {
            int? loggedUserId = HttpContext.Session.GetInt32("userId");
            if(loggedUserId == null) return RedirectToAction("Index");

            return View();
        }
        [HttpGet("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Console.WriteLine("logged out");
            return Redirect("/");
        }
        [HttpPost("PlanWedding")]
        public IActionResult PlanWedding(Wedding form)
        {
            int? loggedUserId = HttpContext.Session.GetInt32("userId");
            if(loggedUserId == null) return RedirectToAction("Index");
            if (ModelState.IsValid)
            {
                form.UserId = (int) loggedUserId;
                _context.Add(form);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return View("Wedding");
        }
        [HttpGet("RSVP/{id}")]
        public IActionResult RSVP(int id)
        {
            int? loggedUserId = HttpContext.Session.GetInt32("userId");
            if(loggedUserId == null) return RedirectToAction("Index");
            Guests rsvp = new Guests();
            rsvp.UserId = (int)loggedUserId;
            rsvp.WeddingId = id;
            _context.Guests.Add(rsvp);
            _context.SaveChanges();
            return Redirect("/Dashboard");
        }
        [HttpGet("RemoveRSVP/{id}")]
        public IActionResult RemoveRSVP(int id)
        {
            int? loggedUserId = HttpContext.Session.GetInt32("userId");
            if(loggedUserId == null) return RedirectToAction("Index");
            Guests checkForRSVP = _context.Guests
                .FirstOrDefault(l => l.UserId ==(int)loggedUserId && l.WeddingId == id);
            _context.Guests.Remove(checkForRSVP);
            _context.SaveChanges();
            return Redirect("/Dashboard");
        }
        [HttpGet("Delete/{id}")]
        public IActionResult DeleteWedding(int id)
        {
            int? loggedUserId = HttpContext.Session.GetInt32("userId");
            if(loggedUserId == null) return RedirectToAction("Index");
            Wedding deleteMe = _context.Weddings
                .FirstOrDefault(l => l.WeddingId == id);
            _context.Weddings.Remove(deleteMe);
            _context.SaveChanges();
            return Redirect("/Dashboard");
        }
        [HttpGet("Wedding/{id}")]
        public IActionResult ViewWedding(int id)
        {
            int? loggedUserId = HttpContext.Session.GetInt32("userId");
            if(loggedUserId == null) return RedirectToAction("Index");
            @ViewBag.SingleWedding = _context.Weddings
                .Include(w => w.Creator)
                .Include(w => w.Attendees)
                .ThenInclude(w => w.User)
                .FirstOrDefault(w => w.WeddingId == id);

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
