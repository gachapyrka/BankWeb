using Microsoft.AspNetCore.Mvc;

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
            return PartialView("_AnalyzePartitial");
        }
    }
}
