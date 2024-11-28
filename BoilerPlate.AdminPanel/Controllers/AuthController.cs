using BoilerPlate.Business.DbServices;
using BoilerPlate.Entity.Results.ComplexTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BoilerPlate.AdminPanel.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("login")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Index(string email, string password)
        {
            var loginResult = await _authService.LoginAsync(email, password);

            if (loginResult.ResultStatus == ResultStatus.Success)
            {
                return Ok(new { roles = loginResult.Data });
            }

            return BadRequest(new { errorMessage = loginResult.Message });
        }

        [Authorize]
        [Route("logout")]
        public async Task<IActionResult> LogOut()
        {
            var logoutResult = await _authService.LogoutAsync();

            if (logoutResult.ResultStatus == ResultStatus.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest(new { errorMessage = logoutResult.Message });
        }
    }
}
