using BoilerPlate.Entity.Entities.Concrete.Identity;
using BoilerPlate.Entity.Results.Abstract;
using BoilerPlate.Entity.Results.ComplexTypes;
using BoilerPlate.Entity.Results.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Business.DbServices
{
    public class AppRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public AppRoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // Yeni rol ekleme
        public async Task< IDataResult<IdentityResult> > AddRoleAsync(string roleName)
        {
            try
            {
                var role = new AppRole { Name = roleName };
                var idresult = await _roleManager.CreateAsync(role);
                return new DataResult<IdentityResult>(ResultStatus.Success, idresult);

            }
            catch (Exception ex)
            {
                return new DataResult<IdentityResult>(ResultStatus.Error, ex.Message, null );
            }
            
        }

        // Rol silme
        public async Task<IDataResult<IdentityResult>> DeleteRoleAsync(int roleId)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId.ToString());
                if (role == null)
                    return new DataResult<IdentityResult>(ResultStatus.Error, "Rol bulunamadı.", null);

                var idresult = await _roleManager.DeleteAsync(role);
                return new DataResult<IdentityResult>(ResultStatus.Success, idresult);

            }
            catch (Exception ex)
            {
                return new DataResult<IdentityResult>(ResultStatus.Error, ex.Message, null);
            }

            
        }

        // Rol güncelleme
        public async Task<IDataResult<IdentityResult>> UpdateRoleAsync(int roleId, string newRoleName)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId.ToString());
                if (role == null)
                {
                    return new DataResult<IdentityResult>(ResultStatus.Error, "Rol bulunamadı.", null);
                }

                role.Name = newRoleName;
                var idresult = await _roleManager.UpdateAsync(role);

                return new DataResult<IdentityResult>(ResultStatus.Success, idresult);
            }
            catch (Exception ex)
            {
                return new DataResult<IdentityResult>(ResultStatus.Error, ex.Message, null);
            }
        }

        // Tüm rolleri listeleme
        public async Task< IDataResult<IList<AppRole>> > GetAllRolesAsync()
        {
            try
            {
                var list = new List<AppRole>(await _roleManager.Roles.ToListAsync());
                return new DataResult<List<AppRole>>(ResultStatus.Success, list);
            }
            catch (Exception ex)
            {
                return new DataResult<List<AppRole>> (ResultStatus.Error, ex.Message, null);
            }

        }

        // Rolü ID ile getirme
        public async Task<IDataResult<AppRole?>> GetRoleByIdAsync(int roleId)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId.ToString());
                return new DataResult<AppRole?>(ResultStatus.Success, role);
            }
            catch (Exception ex)
            {
                return new DataResult<AppRole?>(ResultStatus.Error, ex.Message, null);
            }

        }

        // Rolü isme göre getirme
        public async Task<IDataResult<AppRole?>> GetRoleByNameAsync(string roleName)
        {
            try
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                return new DataResult<AppRole?>(ResultStatus.Success, role);
            }
            catch (Exception ex)
            {
                return new DataResult<AppRole?>(ResultStatus.Error, ex.Message, null);
            }

        }
    }
}
