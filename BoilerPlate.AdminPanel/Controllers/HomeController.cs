using BoilerPlate.Business.DbServices;
using BoilerPlate.Entity.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BoilerPlate.AdminPanel.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public async Task<IActionResult> IndexAsync()
        {
            return View();
        }
    }
}
