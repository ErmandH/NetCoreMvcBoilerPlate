using BoilerPlate.Entity.Entities.Concrete.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BoilerPlate.AdminPanel
{
    public static class IdentitySeed
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            // Servislerden UserManager ve RoleManager al
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();

            // Rolleri tanımla
            var roles = new[] { "Admin", "User" };

            foreach (var role in roles)
            {
                // Eğer rol yoksa ekle
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var appRole = new AppRole();
                    appRole.Name = role;
                    await roleManager.CreateAsync(appRole);
                }
            }

            // Kullanıcı oluştur
            var adminEmail = "ermandharuni@gmail.com";
            var adminPassword = "dravenligi2002";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true // E-posta doğrulandı olarak işaretle
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    // Admin kullanıcısına Admin rolü ata
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }

}
