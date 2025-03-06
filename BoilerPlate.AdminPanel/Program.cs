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
    options.Password.RequireDigit = false; // Rakam zorunluluðunu kaldýr
    options.Password.RequireLowercase = false; // Küçük harf zorunluluðunu kaldýr
    options.Password.RequireNonAlphanumeric = false; // Alfanümerik olmayan karakter zorunluluðunu kaldýr
    options.Password.RequireUppercase = false; // Büyük harf zorunluluðunu kaldýr
    options.Password.RequiredLength = 0; // Minimum uzunluðu kaldýr
    options.Password.RequiredUniqueChars = 0; // Farklý karakter sayýsýný kaldýr

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

// Seed iþlemini çaðýr
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await IdentitySeed.SeedAsync(services);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Hata oluþtu: {ex.Message}");
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
