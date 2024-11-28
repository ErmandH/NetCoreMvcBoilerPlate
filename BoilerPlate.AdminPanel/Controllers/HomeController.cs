using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BoilerPlate.AdminPanel.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
