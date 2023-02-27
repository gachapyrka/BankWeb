using BankWeb.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            return View("Registration");
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
            // Use Input.Email and Input.Password to authenticate the user
            // with your custom authentication logic.
            //
            // For demonstration purposes, the sample validates the user
            // on the email address maria.rodriguez@contoso.com with 
            // any password that passes model validation.

            if (user == null)
            {
                //TODO:check login/password
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
    }
}
