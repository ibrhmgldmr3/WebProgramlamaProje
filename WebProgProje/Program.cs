using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using WebProgramlamaProje.Controllers;
using WebProgramlamaProje.Models;

var builder = WebApplication.CreateBuilder(args);

// **Servislerin eklenmesi**
builder.Services.AddControllersWithViews(); // MVC desteði
builder.Services.AddHttpContextAccessor(); // HttpContext eriþimi
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session zaman aþýmý süresi
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpClient();
builder.Services.AddHttpClient<HairAPIController>(); // HttpClient için servis
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100 MB
});


// **Veritabaný baðlantý ayarlarý**
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? "Server=(localdb)\\mssqllocaldb;Database=WebProgramlamaProje;Trusted_Connection=True;";
builder.Services.AddDbContext<SalonDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// **Custom Servislerin Eklenmesi**
builder.Services.AddScoped<KullaniciService>(); // Kullanýcý servisi

var app = builder.Build();

// **HTTP Request Pipeline Ayarlarý**
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Hata yönlendirme
    app.UseHsts(); // HTTP Strict Transport Security
}

app.UseHttpsRedirection(); // HTTPS yönlendirmesi
app.UseStaticFiles(); // Statik dosyalar (CSS, JS, resimler vb.) için

app.UseRouting();
app.UseSession(); // Session kullanýmýný aktif et
app.UseAuthorization(); // Yetkilendirme middleware'i

// **Rota ayarlarý**
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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=HairAPI}/{action=ChangeHairstyleForm}/{id?}");
});


// **Uygulamanýn çalýþtýrýlmasý**
app.Run();
