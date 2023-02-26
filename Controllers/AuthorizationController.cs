using BankWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankWeb.Controllers
{
    public class AuthorizationController : Controller
    {
        public IActionResult Index()
        {
            return View(new LP());
        }

        public IActionResult Registration()
        {
            return View("Registration");
        }

        [HttpPost]
        public IActionResult OnAuthorize([FromBody]LP account)
        {

            return RedirectToAction("");
        }
    }
}
