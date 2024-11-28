using BoilerPlate.Entity.Entities.Concrete.Identity;
using BoilerPlate.Entity.Results.Abstract;
using BoilerPlate.Entity.Results.ComplexTypes;
using BoilerPlate.Entity.Results.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Business.DbServices
{
    public class AuthService
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IDataResult<IList<string>>> LoginAsync(string email, string password)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return new DataResult<IList<string>>(ResultStatus.Error, "E-Posta veya şifre yanlış.", null);
                }

                var result = await signInManager.PasswordSignInAsync(user, password, true, false);
                if (!result.Succeeded)
                {
                    return new DataResult<IList<string>>(ResultStatus.Error, "E-Posta veya şifre yanlış.", null);
                }

                var roles = await userManager.GetRolesAsync(user);
                return new DataResult<IList<string>>(ResultStatus.Success, roles);
            }
            catch (Exception ex)
            {
                return new DataResult<IList<string>>(ResultStatus.Error, ex.Message, null);
            }
        }


        public async Task<IResult> LogoutAsync()
        {
            try
            {
                await signInManager.SignOutAsync();
                return new Result(ResultStatus.Success);
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.Error, ex.Message, ex);
            }  
        }
    }
}
