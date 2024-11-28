using Microsoft.AspNetCore.Mvc;
using BoilerPlate.Business.DbServices;
using BoilerPlate.Entity.Dto.AppUser;
using BoilerPlate.Entity.ViewModels.AppUserVM;
using BoilerPlate.Entity.Results.ComplexTypes;
using Microsoft.AspNetCore.Authorization;

namespace BoilerPlate.AdminPanel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AppUserController : Controller
    {
        private readonly AppUserService _appUserService;
        private readonly AppRoleService _appRoleService;

        public AppUserController(AppUserService appUserService, AppRoleService appRoleService)
        {
            _appUserService = appUserService;
            _appRoleService = appRoleService;
        }

        // Tüm kullanıcıları listeleme
        [Route("appuser")]
        public async Task<IActionResult> Index()
        {
            var usersResult = await _appUserService.GetAllUsersAsync();

            if (usersResult.ResultStatus == ResultStatus.Success)
            {
                return View(usersResult.Data);
            }

            return BadRequest(new { errorMessage = usersResult.Message });
        }

        // Yeni kullanıcı ekleme - GET
        [HttpGet("appuser/add")]
        public async Task<IActionResult> Add()
        {
            var rolesResult = await _appRoleService.GetAllRolesAsync();

            if (rolesResult.ResultStatus == ResultStatus.Success)
            {
                return View(rolesResult.Data);
            }

            return BadRequest(new { errorMessage = rolesResult.Message });
        }

        // Yeni kullanıcı ekleme - POST
        [HttpPost("appuser/add")]
        public async Task<IActionResult> Add(AddAppUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { errorMessage = "Lütfen göndermiş olduğunuz bilgilerin doğruluğundan emin olun." });
            }


            var result = await _appUserService.AddUserAsync(dto);

            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(new { errorMessage = result.Message });
        }

        // Kullanıcıyı güncelleme - GET
        [HttpGet("appuser/update/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var userResult = await _appUserService.GetUserByIdAsync(id);

            if (userResult.ResultStatus != ResultStatus.Success || userResult.Data == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var rolesResult = await _appRoleService.GetAllRolesAsync();

            if (rolesResult.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(new { errorMessage = rolesResult.Message });
            }

            var vm = new UpdateAppUserViewModel
            {
                UserViewModel = userResult.Data,
                Roles = rolesResult.Data
            };

            return View(vm);
        }

        // Kullanıcıyı güncelleme - POST
        [HttpPost("appuser/update")]
        public async Task<IActionResult> Update(UpdateAppUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { errorMessage = "Lütfen göndermiş olduğunuz bilgilerin doğruluğundan emin olun." });
            }

            var result = await _appUserService.UpdateUserAsync(dto);

            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok();
            }

            return BadRequest(new { errorMessage = result.Message });
        }

        // Kullanıcıyı silme - POST
        [HttpPost("appuser/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _appUserService.DeleteUserAsync(id);

            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok();
            }

            return BadRequest(new { errorMessage = result.Message });
        }
    }

}
