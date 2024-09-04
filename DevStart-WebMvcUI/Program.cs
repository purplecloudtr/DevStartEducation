using DevStart_DataAccsess.Contexts;
using DevStart_Service.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// AppSettings.json da yer alan ayarlarý DBContext e ekleme
builder.Services.AddDbContext<DevStartDbContext>(
        options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DevStartConnection"))
    );

builder.Services.AddDistributedMemoryCache(); // Add memory cache services.
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session süresi
    options.Cookie.HttpOnly = true; // Cookie eriþim ayarlarý
    options.Cookie.IsEssential = true; // Cookie'nin gerekli olduðu belirtir
});

builder.Services.AddExtensions(); //DependencyExtensions için.

builder.Services.AddSession();

var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
