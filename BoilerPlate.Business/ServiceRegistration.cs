
using BoilerPlate.Business.DbServices;
using BoilerPlate.Business.StorageServices;
using BoilerPlate.Business.StorageServices.Local;
using Microsoft.Extensions.DependencyInjection;


namespace BoilerPlate.Business
{
    public static class ServiceRegistration
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {


            // tum storage turleri icin ortak storage
            services.AddScoped<IStorageService, StorageService>();
            services.AddStorage<LocalStorage>();
        }

        public static void AddAuthServices(this IServiceCollection services)
        {
            services.AddScoped<AuthService>();
            services.AddScoped<AppUserService>();
            services.AddScoped<AppRoleService>();
        }

        // hangi storage turunun kullanilacagini belirleyen yer burasi
        public static void AddStorage<T>(this IServiceCollection services) where T : class, IStorage
        {
            services.AddScoped<IStorage, T>();
        }
    }
}
