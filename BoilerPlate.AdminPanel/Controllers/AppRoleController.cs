using BoilerPlate.Business.DbServices;
using BoilerPlate.Entity.Results.ComplexTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BoilerPlate.AdminPanel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AppRoleController : Controller
    {
        private readonly AppRoleService _appRoleService;

        public AppRoleController(AppRoleService appRoleService)
        {
            _appRoleService = appRoleService;
        }

        // Tüm rolleri listeleme
        [HttpGet("approle")]
        public async Task<IActionResult> Index()
        {
            var rolesResult = await _appRoleService.GetAllRolesAsync();

            if (rolesResult.ResultStatus == ResultStatus.Success)
            {
                return View(rolesResult.Data);
            }

            return BadRequest(new { errorMessage = rolesResult.Message });
        }

        // Yeni rol ekleme - GET
        [HttpGet("approle/add")]
        public IActionResult Add()
        {
            return View();
        }

        // Yeni rol ekleme - POST
        [HttpPost("approle/add")]
        public async Task<IActionResult> Add(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest(new { errorMessage = "Rol adı boş olamaz." });
            }

            var result = await _appRoleService.AddRoleAsync(roleName);

            if (result.ResultStatus == ResultStatus.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest(new { errorMessage = result.Message });
        }

        // Rol güncelleme - GET
        [HttpGet("approle/update/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var roleResult = await _appRoleService.GetRoleByIdAsync(id);

            if (roleResult.ResultStatus == ResultStatus.Success && roleResult.Data != null)
            {
                return View(roleResult.Data);
            }

            return RedirectToAction(nameof(Index));
        }

        // Rol güncelleme - POST
        [HttpPost("approle/update")]
        public async Task<IActionResult> Update(int id, string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest(new { errorMessage = "Rol adı boş olamaz." });
            }

            var result = await _appRoleService.UpdateRoleAsync(id, roleName);

            if (result.ResultStatus == ResultStatus.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest(new { errorMessage = result.Message });
        }

        // Rol silme - POST
        [HttpPost("approle/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _appRoleService.DeleteRoleAsync(id);

            if (result.ResultStatus == ResultStatus.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest(new { errorMessage = result.Message });
        }
    }
}
