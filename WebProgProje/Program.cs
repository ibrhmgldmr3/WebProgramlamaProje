using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using WebProgramlamaProje.Controllers;
using WebProgramlamaProje.Models;

var builder = WebApplication.CreateBuilder(args);

// **Servislerin eklenmesi**
builder.Services.AddControllersWithViews(); // MVC deste�i
builder.Services.AddHttpContextAccessor(); // HttpContext eri�imi
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session zaman a��m� s�resi
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// IHttpClientFactory + varsayılan HttpClient kaydı (controller'lara enjekte edilir)
builder.Services.AddHttpClient();
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100 MB
});


// **Veritaban� ba�lant� ayarlar�**
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString))
{
    connectionString = "Server=(localdb)\\mssqllocaldb;Database=WebProgramlamaProje;Trusted_Connection=True;";
}
builder.Services.AddDbContext<SalonDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// **Custom Servislerin Eklenmesi**
builder.Services.AddScoped<KullaniciService>(); // Kullan�c� servisi

var app = builder.Build();

// **HTTP Request Pipeline Ayarlar�**
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Hata y�nlendirme
    app.UseHsts(); // HTTP Strict Transport Security
}

app.UseHttpsRedirection(); // HTTPS y�nlendirmesi
app.UseStaticFiles(); // Statik dosyalar (CSS, JS, resimler vb.) i�in

app.UseRouting();
app.UseSession(); // Session kullan�m�n� aktif et
app.UseAuthorization(); // Yetkilendirme middleware'i

// **Rota ayarlar�**
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


// **Uygulaman�n �al��t�r�lmas�**
app.Run();
