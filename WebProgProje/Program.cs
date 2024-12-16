using Microsoft.EntityFrameworkCore;
using WebProgramlamaProje.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

//Veritabani baðlanti adresi
var conn = "Server=(localdb)\\mssqllocaldb;Database=WebProgramlamaProje;Trusted_Connection=True";
builder.Services.AddDbContext<SalonDbContext>(options =>
{
    options.UseSqlServer(conn);
});
builder.Services.AddScoped<KullaniciService>();

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
app.UseSession(); // Session'ý kullanýma alýn

app.UseAuthorization();

app.MapControllerRoute(
    name: "admin",
    pattern: "Home/Admin",
    defaults: new { controller = "Home", action = "Admin" });
app.MapControllerRoute(
    name: "logout",
    pattern: "Logout",
    defaults: new { controller = "Logout", action = "Index" });
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
