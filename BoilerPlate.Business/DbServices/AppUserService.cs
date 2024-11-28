using BoilerPlate.Entity.Dto.AppUser;
using BoilerPlate.Entity.Entities.Concrete.Identity;
using BoilerPlate.Entity.Results.Abstract;
using BoilerPlate.Entity.Results.ComplexTypes;
using BoilerPlate.Entity.Results.Concrete;
using BoilerPlate.Entity.ViewModels.AppUserVM;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoilerPlate.Business.DbServices
{
    public class AppUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppRoleService _roleService;

        public AppUserService(UserManager<AppUser> userManager, AppRoleService roleService)
        {
            _userManager = userManager;
            _roleService = roleService;
        }

        // Yeni bir kullanıcı ekleme
        public async Task<IDataResult<AppUser>> AddUserAsync(AddAppUserDto dto)
        {
            try
            {
                var user = dto.Adapt<AppUser>();
                user.UserName = dto.Email;
                user.CreatedDate = DateTime.UtcNow;

                var result = await _userManager.CreateAsync(user, dto.Password);
                if (!result.Succeeded)
                {
                    return new DataResult<AppUser>(ResultStatus.Error, "Kullanıcı eklenirken bir hata oluştu.", null);
                }

                var roleResult = await _roleService.GetRoleByIdAsync(dto.RoleId);
                if (roleResult.ResultStatus != ResultStatus.Success || roleResult.Data == null)
                {
                    return new DataResult<AppUser>(ResultStatus.Error, "Rol bulunamadı.", null);
                }

                var addRoleResult = await _userManager.AddToRoleAsync(user, roleResult.Data.Name);
                if (!addRoleResult.Succeeded)
                {
                    return new DataResult<AppUser>(ResultStatus.Error, "Kullanıcıya rol atanırken bir hata oluştu.", null);
                }

                return new DataResult<AppUser>(ResultStatus.Success, user);
            }
            catch (Exception ex)
            {
                return new DataResult<AppUser>(ResultStatus.Error, ex.Message, null);
            }
        }

        // Kullanıcı silme
        public async Task<IDataResult<bool>> DeleteUserAsync(int id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user == null)
                {
                    return new DataResult<bool>(ResultStatus.Error, "Kullanıcı bulunamadı.", false);
                }

                var roles = await _userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    await _userManager.RemoveFromRoleAsync(user, role);
                }

                var result = await _userManager.DeleteAsync(user);
                return result.Succeeded
                    ? new DataResult<bool>(ResultStatus.Success, true)
                    : new DataResult<bool>(ResultStatus.Error, "Kullanıcı silinirken bir hata oluştu.", false);
            }
            catch (Exception ex)
            {
                return new DataResult<bool>(ResultStatus.Error, ex.Message, false);
            }
        }

        // Kullanıcı güncelleme
        public async Task<IDataResult<bool>> UpdateUserAsync(UpdateAppUserDto dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(dto.Id.ToString());
                if (user == null)
                {
                    return new DataResult<bool>(ResultStatus.Error, "Kullanıcı bulunamadı.", false);
                }

                user.FirstName = dto.FirstName;
                user.LastName = dto.LastName;
                user.Email = dto.Email;
                user.PhoneNumber = dto.PhoneNumber;
                user.UserName = dto.Email;
                user.IsActive = dto.IsActive;

                var roles = await _userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    await _userManager.RemoveFromRoleAsync(user, role);
                }

                var roleResult = await _roleService.GetRoleByIdAsync(dto.RoleId);
                if (roleResult.ResultStatus != ResultStatus.Success || roleResult.Data == null)
                {
                    return new DataResult<bool>(ResultStatus.Error, "Rol bulunamadı.", false);
                }

                var addRoleResult = await _userManager.AddToRoleAsync(user, roleResult.Data.Name);
                if (!addRoleResult.Succeeded)
                {
                    return new DataResult<bool>(ResultStatus.Error, "Kullanıcıya yeni rol atanırken bir hata oluştu.", false);
                }

                var updateResult = await _userManager.UpdateAsync(user);
                return updateResult.Succeeded
                    ? new DataResult<bool>(ResultStatus.Success, true)
                    : new DataResult<bool>(ResultStatus.Error, "Kullanıcı güncellenirken bir hata oluştu.", false);
            }
            catch (Exception ex)
            {
                return new DataResult<bool>(ResultStatus.Error, ex.Message, false);
            }
        }

        // Tüm kullanıcıları alma
        public async Task<IDataResult<IList<ListAppUserViewModel>>> GetAllUsersAsync()
        {
            try
            {
                var users = _userManager.Users.ToList();
                var userViewModels = new List<ListAppUserViewModel>();

                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    userViewModels.Add(new ListAppUserViewModel
                    {
                        AppUser = user,
                        UserRole = string.Join(",", roles)
                    });
                }

                return new DataResult<IList<ListAppUserViewModel>>(ResultStatus.Success, userViewModels);
            }
            catch (Exception ex)
            {
                return new DataResult<IList<ListAppUserViewModel>>(ResultStatus.Error, ex.Message, null);
            }
        }

        // ID ile kullanıcı alma
        public async Task<IDataResult<ListAppUserViewModel>> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user == null)
                {
                    return new DataResult<ListAppUserViewModel>(ResultStatus.Error, "Kullanıcı bulunamadı.", null);
                }

                var roles = await _userManager.GetRolesAsync(user);
                var viewModel = new ListAppUserViewModel
                {
                    AppUser = user,
                    UserRole = string.Join(",", roles)
                };

                return new DataResult<ListAppUserViewModel>(ResultStatus.Success, viewModel);
            }
            catch (Exception ex)
            {
                return new DataResult<ListAppUserViewModel>(ResultStatus.Error, ex.Message, null);
            }
        }

        // Kullanıcı rolünü alma
        public async Task<IDataResult<string>> GetUserRoleAsync(int id)
        {
            try
            {
                var userResult = await GetUserByIdAsync(id);
                if (userResult.ResultStatus != ResultStatus.Success || userResult.Data == null)
                {
                    return new DataResult<string>(ResultStatus.Error, "Kullanıcı bulunamadı.", null);
                }

                var roles = await _userManager.GetRolesAsync(userResult.Data.AppUser);
                return roles.Any()
                    ? new DataResult<string>(ResultStatus.Success, roles.First())
                    : new DataResult<string>(ResultStatus.Error, "Rol atanmadı.", null);
            }
            catch (Exception ex)
            {
                return new DataResult<string>(ResultStatus.Error, ex.Message, null);
            }
        }
    }
}
