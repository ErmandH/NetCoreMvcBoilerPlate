using BoilerPlate.Business.StorageServices.Local;
using BoilerPlate.Business;
using BoilerPlate.Entity.Entities.Concrete.Identity;
using Microsoft.AspNetCore.Identity;
using BoilerPlate.DAL.Context;
using BoilerPlate.DAL;
using BoilerPlate.AdminPanel.Helpers;
using BoilerPlate.AdminPanel;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddBusinessServices();
builder.Services.AddAuthServices();
builder.Services.AddStorage<LocalStorage>();
builder.Services.AddSingleton<PathHelper>();
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireDigit = false; // Rakam zorunlulu�unu kald�r
    options.Password.RequireLowercase = false; // K���k harf zorunlulu�unu kald�r
    options.Password.RequireNonAlphanumeric = false; // Alfan�merik olmayan karakter zorunlulu�unu kald�r
    options.Password.RequireUppercase = false; // B�y�k harf zorunlulu�unu kald�r
    options.Password.RequiredLength = 0; // Minimum uzunlu�u kald�r
    options.Password.RequiredUniqueChars = 0; // Farkl� karakter say�s�n� kald�r

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.AllowedForNewUsers = true;


    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


// app cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.LogoutPath = "/login";
    options.AccessDeniedPath = "/";
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.SlidingExpiration = true;
    options.Cookie = new CookieBuilder()
    {
        HttpOnly = true,
        Name = ".App.Security.Cookie"
    };
});

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddControllersWithViews();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Seed i�lemini �a��r
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await IdentitySeed.SeedAsync(services);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Hata olu�tu: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapBlazorHub();
app.Run();
