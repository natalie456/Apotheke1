using Apotheke1.Data;
using Apotheke1.Interfaces;
using Apotheke1.Services;
using Microsoft.EntityFrameworkCore;
using Apotheke1.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;




var builder = WebApplication.CreateBuilder(args);


string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;


var serverVersion = ServerVersion.AutoDetect(connection);

builder.Services.AddDbContext<ApothekeDbContext>(options =>
    options.UseMySql(connection, serverVersion));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApothekeDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IMedicineService, MedicineService>();



builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    // 1) סעגמנ‏÷למ נמכü Admin ÿךשמ ¿¿ םולא
    if (!await roleManager.RoleExistsAsync("Admin"))
        await roleManager.CreateAsync(new IdentityRole("Admin"));

    // 2) סעגמנ‏÷למ אהל³םא 
    string adminEmail = "admin@apotheke.com";
    string adminPass = "Admin123!";

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
        await userManager.CreateAsync(adminUser, adminPass);
    }

    // 3) המהא÷למ ימדמ ג נמכü Admin
    if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
        await userManager.AddToRoleAsync(adminUser, "Admin");
}
app.UseHttpsRedirection();
app.UseStaticFiles();     
app.UseRouting();

app.UseMiddleware<TestMiddleware>();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Medicine}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapControllers();
app.Run();
