using BankWeb.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data.Odbc;
using System.Diagnostics;

namespace BankWeb.Controllers
{
    public class HomeController : Controller
    {
        public static MySqlConnection connection { get; set; }
        public static Account account { get; set; }

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            account = new Account();
            ViewData["Account"] = true;
            _logger = logger;
            string connString = "server = localhost;database = bank_database;user id = root;password = root;";
            string con = "Driver={SQL Server};Server=root@localhost:3306;Database=bank_database;User=root;Password=root;";
            connection = new MySqlConnection(connString);
            connection.Open();
        }

        public IActionResult Index()
        {
            var user = HttpContext.User as Account;
            if (user != null &&user.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Analyze");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}