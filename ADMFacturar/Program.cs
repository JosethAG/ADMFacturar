using ADM.Models;
using ADM.Service;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// A�adir configuraci�n SMTP
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));


// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();

//ApiMonster
builder.Services.AddScoped<IMonsterApiService, MonsterApiService>();
builder.Services.AddScoped<IRestrictionService, RestrictionService>();



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Seguridad/Login";
        option.ExpireTimeSpan = TimeSpan.FromDays(365 * 10); // 10 a�os de duraci�n
        option.SlidingExpiration = false;
        option.AccessDeniedPath = "/Home/AccDenied";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Seguridad}/{action=Login}/{id?}");

app.Run();
