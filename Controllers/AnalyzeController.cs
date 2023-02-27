using BankWeb.Models.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BankWeb.Models;
using System.Data.Odbc;
using System.Diagnostics;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Xml.Linq;
using MySqlConnector;

namespace BankWeb.Controllers
{
    public class AnalyzeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AnalyzePartitial()
        {
            Account account = HomeController.account;
            var bank = new BankViewModel(account.Bank);
            return PartialView("_AnalyzePartitial", bank);
        }

        public IActionResult InputPartitial()
        {
            return PartialView("_InputPartitial", new Bank("", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
        }

        public IActionResult AddPartitial()
        {
            return PartialView("_AddPartitial", new RegistrationItem());
        }

        public IActionResult HelpPartitial()
        {
            return PartialView("_HelpPartitial");
        }

        [HttpPost]
        public async Task<IActionResult> OnRegister(RegistrationItem user)
        {
            if (user.Password != user.RepeatPassword)
            {
                ModelState.AddModelError(string.Empty, "Invalid registration attempt.");
                return View("Registration");
            }

            Account account = HomeController.account;

            MySqlConnection connection = HomeController.connection;
            string query = $$"""
            INSERT INTO users (login, password, bankId) VALUES ( '{{user.Login}}', '{{user.Password}}', {{account.Bank.Id}})
            """;
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();

            return RedirectToAction("Index", "Analyze");
        }

        [HttpPost]
        public async Task<IActionResult> OnInput(Bank bank)
        {
            if (bank.HighlyLiquidAssets <= 0 ||
               bank.InvolvedFunds <= 0 ||
               bank.DemandObligations <= 0 ||
               bank.AssetsFor30Days <= 0 ||
               bank.CommitmentsFor30Days <= 0 ||
               bank.Capital <= 0 ||
               bank.AssetsMore1Year <= 0 ||
               bank.CommitmentsMore1Year <= 0 ||
               bank.IssuedBills <= 0 ||
               bank.DelinquentLoans <= 0 ||
               bank.LoanDebt <= 0 ||
               bank.RiskAssets <= 0)
                return RedirectToAction("Index", "Analyze");
            Account account = HomeController.account;

            MySqlConnection connection = HomeController.connection;
            string query = $$"""
            UPDATE banks SET hla = {{bank.HighlyLiquidAssets}}, iff = {{bank.InvolvedFunds}}, do = {{bank.DemandObligations}}, af30d = {{bank.AssetsFor30Days}}, cf30d = {{bank.CommitmentsFor30Days}}, capital = {{bank.Capital}}, am1y = {{bank.AssetsMore1Year}}, cm1y = {{bank.CommitmentsMore1Year}}, ib = {{bank.IssuedBills}}, dl = {{bank.DelinquentLoans}}, ld = {{bank.LoanDebt}}, ra = {{bank.RiskAssets}} WHERE id = {{account.Bank.Id}}
            """;
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();

            connection = HomeController.connection;
            query = $$"""
            SELECT users.id, bankId, banks.name, banks.hla, banks.iff, banks.do, banks.af30d, banks.cf30d, banks.capital, banks.am1y, banks.cm1y, banks.ib, banks.dl, banks.ld, banks.ra FROM users INNER JOIN banks ON users.bankId = banks.id AND users.login = '{{account.Login}}' AND users.password = '{{account.Password}}'
            """;

            command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Bank newBank = new Bank(reader[2].ToString(), Double.Parse(reader[3].ToString()),
                                     Double.Parse(reader[4].ToString()), Double.Parse(reader[5].ToString()),
                                     Double.Parse(reader[6].ToString()), Double.Parse(reader[7].ToString()),
                                     Double.Parse(reader[8].ToString()), Double.Parse(reader[9].ToString()),
                                     Double.Parse(reader[10].ToString()), Double.Parse(reader[11].ToString()),
                                     Double.Parse(reader[12].ToString()), Double.Parse(reader[13].ToString()),
                                     Double.Parse(reader[14].ToString()));
                bank.Id = int.Parse(reader[1].ToString());
                account.Bank = newBank;
                ViewData.Add("Account", account);
                reader.Close();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }

            return RedirectToAction("Index", "Analyze");
        }
    }
}
