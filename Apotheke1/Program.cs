using Apotheke1.Data;
using Apotheke1.Interfaces;
using Apotheke1.Services;
using Microsoft.EntityFrameworkCore;
using Apotheke1.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Підключення до бази даних
string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;


var serverVersion = ServerVersion.AutoDetect(connection);

builder.Services.AddDbContext<ApothekeDbContext>(options =>
    options.UseMySql(connection, serverVersion));

// Реєстрація сервісів
builder.Services.AddScoped<IMedicineService, MedicineService>();

// MVC + Views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Мідлвар
app.UseMiddleware<TestMiddleware>();

// Маршрути
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Medicine}/{action=Index}/{id?}");

app.Run();
