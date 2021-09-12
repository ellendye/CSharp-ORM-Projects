using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsORM.Models;


namespace SportsORM.Controllers
{
    public class HomeController : Controller
    {

        private static Context _context;

        public HomeController(Context DBContext)
        {
            _context = DBContext;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.BaseballLeagues = _context.Leagues
                .Where(l => l.Sport.Contains("Baseball"))
                .ToList();
            return View();
        }

        [HttpGet("level_1")]
        public IActionResult Level1()
        {
            //1.
            ViewBag.WomensLeagues = _context.Leagues
                .Where(l => l.Name.Contains("Women"))
                .ToList();

            //2.
            ViewBag.HockeyLeagues = _context.Leagues
                .Where(l => l.Sport.Contains("Hockey"))
                .ToList();

            //3
            ViewBag.NotFootballLeagues = _context.Leagues
                .Where(l => !l.Sport.Contains("Football"))
                .ToList();
            
            //4
            ViewBag.ConferenceLeagues = _context.Leagues
                .Where(l => l.Name.Contains("Conference"))
                .ToList();
            
            //5
            ViewBag.AtlanticLeagues = _context.Leagues
                .Where(l => l.Name.Contains("Atlantic"))
                .ToList();

            //6
            ViewBag.DallasTeams = _context.Teams
                .Where(l => l.Location =="Dallas")
                .ToList();
            
            //7
            ViewBag.Raptors = _context.Teams
                .Where(l => l.TeamName =="Raptors")
                .ToList();

            //8
            ViewBag.CityTeam = _context.Teams
                .Where(l => l.Location.Contains("City"))
                .ToList();

            //9
            ViewBag.TTeam = _context.Teams
                .Where(l => l.TeamName.StartsWith("T"))
                .ToList();
            
            //10
            ViewBag.TeamByName = _context.Teams
                .OrderBy(l => l.TeamName)
                .ToList();          
            
            //11
            ViewBag.TeamByNameReversed = _context.Teams
                .OrderByDescending(l => l.TeamName)
                .ToList();

            //12
            ViewBag.Cooper = _context.Players
                .Where(l => l.LastName == "Cooper")
                .ToList();

            //13
            ViewBag.Joshua = _context.Players
                .Where(l => l.FirstName == "Joshua")
                .ToList();

            //14
            ViewBag.NotJoshCooper = _context.Players
                .Where(l => l.LastName == "Cooper")
                .Where(l => l.FirstName != "Joshua")
                .ToList();
            
            //15
            ViewBag.NotJoshCooper = _context.Players
                .Where(l => l.LastName == "Cooper" && l.FirstName != "Joshua")
                // .Where(l => l.FirstName != "Joshua")
                .ToList();

            //16
            ViewBag.AlexOrWyatt = _context.Players
                .Where(l => l.FirstName == "Wyatt" || l.FirstName == "Alexander")
                .ToList();
            return View();
        }

        [HttpGet("level_2")]
        public IActionResult Level2()
        {
            //1.
            ViewBag.AllAtlanticTeams = _context.Teams
                .Include(team => team.CurrLeague)
                .Where(l => l.CurrLeague.Name == "Atlantic Soccer Conference")
                .ToList();

            //2.
            ViewBag.BostonPenguins = _context.Players
                .Include(team => team.CurrentTeam)
                .Where(l => l.CurrentTeam.Location == "Boston" && l.CurrentTeam.TeamName == "Penguins")
                .ToList();

            //3.
            ViewBag.ICBCPlayers = _context.Players
                .Include(team => team.CurrentTeam)
                .Include(leage => leage.CurrentTeam.CurrLeague)
                .Where(l => l.CurrentTeam.CurrLeague.Name == "International Collegiate Baseball Conference")
                .ToList();

            //4.
            ViewBag.ACAFLopez = _context.Players
                .Include(team => team.CurrentTeam)
                .Include(league => league.CurrentTeam.CurrLeague)
                .Where(l => l.CurrentTeam.CurrLeague.Name == "American Conference of Amateur Football" && l.LastName =="Lopez")
                .ToList();

            //5.
            ViewBag.FootballPlayers = _context.Players
                .Include(team => team.CurrentTeam)
                .Include(league => league.CurrentTeam.CurrLeague)
                .Where(l => l.CurrentTeam.CurrLeague.Sport == "Football")
                .ToList();

            //6.
            ViewBag.TeamsWSophia = _context.Players
                .Include(team => team.CurrentTeam)
                .ThenInclude(league => league.CurrLeague)
                .Where(player => player.FirstName == "Sophia")
                .ToList();

            //7.
            ViewBag.LeaguesWSophia = _context.Players
                .Include(team => team.CurrentTeam)
                .ThenInclude(league => league.CurrLeague)
                .Where(player => player.FirstName == "Sophia")
                .ToList();

            //8.
            ViewBag.FloresNotWashington = _context.Players
                .Include(team => team.CurrentTeam)
                .Where(player => player.LastName == "Flores" && player.CurrentTeam.TeamName != "Roughriders")
                .ToList();
            return View();
        }

        [HttpGet("level_3")]
        public IActionResult Level3()
        {

            //1.
            ViewBag.SamEvans = _context.Players
                .Include(team => team.CurrentTeam)
                .Include(team => team.AllTeams)
                .ThenInclude(team => team.TeamOfPlayer)
                .FirstOrDefault(player => player.FirstName == "Samuel" && player.LastName == "Evans");
            
            //2.
            ViewBag.Manitoba = _context.Teams
                .Include(player => player.CurrentPlayers)
                .Include(player => player.AllPlayers)
                .ThenInclude(player => player.PlayerOnTeam)
                .FirstOrDefault(team => team.Location == "Manitoba" && team.TeamName == "Tiger-Cats");

            //3.
            ViewBag.Vikings = _context.Teams
                .Include(player => player.AllPlayers)
                .ThenInclude(player => player.PlayerOnTeam)
                .ThenInclude(team => team.CurrentTeam)
                .FirstOrDefault(team => team.Location =="Wichita" && team.TeamName =="Vikings");

            //4.
            ViewBag.JacobGray = _context.Players
                .Include(team => team.CurrentTeam)
                .Include(team => team.AllTeams)
                .ThenInclude(team => team.TeamOfPlayer)
                .FirstOrDefault(player => player.FirstName =="Jacob" && player.LastName =="Gray");

            //5.
            ViewBag.JoshuaAmBas = _context.Players
                .Include(team => team.CurrentTeam)
                .Include(team => team.AllTeams)
                .ThenInclude(team => team.TeamOfPlayer)
                .Where(player => player.FirstName =="Joshua")
                .ToList();
            
            //6
            ViewBag.MoreThanTwelve = _context.Teams
                .Include(team => team.CurrentPlayers)
                .Include(team => team.AllPlayers)
                .ThenInclude(team => team.PlayerOnTeam)
                .ToList();


            return View();
        }

    }
}