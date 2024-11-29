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
        private readonly BlogService blogService;

        public HomeController(BlogService blogService)
        {
            this.blogService = blogService;
        }

        [HttpGet("")]
        public async Task<IActionResult> IndexAsync()
        {
            var blog = new Blog 
            {
                Title = "Home",
                Description = "Selam"
            };

            var res = await blogService.GetAllAsync();
            var data = res.Data;
            return View();
        }
    }
}
