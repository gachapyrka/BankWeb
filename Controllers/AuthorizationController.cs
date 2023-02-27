using BankWeb.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BankWeb.Models.ViewModels;
using System.Data.Odbc;
using MySqlConnector;

namespace BankWeb.Controllers
{
    public class AuthorizationController : Controller
    {
        public IActionResult Index()
        {
            return View(new Account());
        }

        public IActionResult Registration()
        {
            return View("Registration", new RegistrationItem());
        }

        public async Task<IActionResult> SingOut(string returnUrl = null)
        {

            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> OnAuthorize(Account user)
        {
            MySqlConnection connection = HomeController.connection;
            string query = $$"""
            SELECT users.id, bankId, banks.name, banks.hla, banks.iff, banks.do, banks.af30d, banks.cf30d, banks.capital, banks.am1y, banks.cm1y, banks.ib, banks.dl, banks.ld, banks.ra FROM users INNER JOIN banks ON users.bankId = banks.id AND users.login = '{{user.Login}}' AND users.password = '{{user.Password}}'
            """;

            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            if(reader.Read())
            {
                Account account = new Account();
                account.Id = Int32.Parse(reader[0].ToString());
                account.Login = user.Login;
                account.Password = user.Password;
                Bank bank = new Bank(reader[2].ToString(), Double.Parse(reader[3].ToString()),
                                     Double.Parse(reader[4].ToString()), Double.Parse(reader[5].ToString()),
                                     Double.Parse(reader[6].ToString()), Double.Parse(reader[7].ToString()),
                                     Double.Parse(reader[8].ToString()), Double.Parse(reader[9].ToString()),
                                     Double.Parse(reader[10].ToString()), Double.Parse(reader[11].ToString()),
                                     Double.Parse(reader[12].ToString()), Double.Parse(reader[13].ToString()),
                                     Double.Parse(reader[14].ToString()));
                bank.Id = int.Parse(reader[1].ToString());
                account.Bank = bank;
                HomeController.account = account;
                reader.Close();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim("FullName", user.Password),
                new Claim(ClaimTypes.Role, "User"),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "Analyze");
        }

        [HttpPost]
        public async Task<IActionResult> OnRegister(RegistrationItem user)
        {
            // Use Input.Email and Input.Password to authenticate the user
            // with your custom authentication logic.
            //
            // For demonstration purposes, the sample validates the user
            // on the email address maria.rodriguez@contoso.com with 
            // any password that passes model validation.

            if (user.Password != user.RepeatPassword)
            {
                ModelState.AddModelError(string.Empty, "Invalid registration attempt.");
                return View("Registration");
            }

            MySqlConnection connection = HomeController.connection;
            string query = $$"""
            INSERT INTO banks (name, hla, iff, do, af30d, cf30d, capital, am1y, cm1y, ib, dl, ld, ra) VALUES ( '{{user.Bank}}', 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)
            """;

            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();

            query = $$"""
            SELECT id FROM banks WHERE name = '{{user.Bank}}'
            """;
            command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            int id = 0;
            while (reader.Read())
            {
                id = Int32.Parse(reader[0].ToString());
            }
            reader.Close();

            query = $$"""
            INSERT INTO users (login, password, bankId) VALUES ( '{{user.Login}}', '{{user.Password}}', {{id}})
            """;
            command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim("FullName", user.Password),
                new Claim(ClaimTypes.Role, "User"),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "Analyze");
        }
    }
}
