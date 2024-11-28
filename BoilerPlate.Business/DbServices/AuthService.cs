using BoilerPlate.Entity.Entities.Concrete.Identity;
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

        public async Task<IList<string>?> LoginAsync(string Email, string Password)
        {
            var user = await userManager.FindByEmailAsync(Email);
            if (user == null)
                throw new Exception("E-Posta veya şifre yanlış");
            var result = await signInManager.PasswordSignInAsync(user, Password, true, false);
            if (!result.Succeeded)
                throw new Exception("E-Posta veya şifre yanlış");
            var roles = await userManager.GetRolesAsync(user);
            return roles;
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
