# Web Programlama Projesi (2024-2025 Güz Dönemi)

> Sakarya Üniversitesi Bilgisayar Mühendisliği – Web Programlama dersi dönem projesi  
> **Geliştirici:** İbrahim Güldemir - Süleyman Samet Kaya
> **Repo:** https://github.com/ibrhmgldmr3/WebProgramlamaProje

---

## İçerik

- [Proje Hakkında](#proje-hakkında)
- [Teknolojiler](#teknolojiler)
- [Kurulum](#kurulum)
- [Çalıştırma](#çalıştırma)
- [Kullanım](#kullanım)
- [Dizin Yapısı](#dizin-yapısı)
- [İletişim](#iletişim)

---

## Proje Hakkında

Bu depo, **full-stack** bir web uygulamasının tüm kaynak kodunu içerir.  
Amaç, modern web geliştirme ilkelerini (MVC mimarisi, istemci–sunucu iletişimi, responsive tasarım vb.) uygulayarak **gerçek hayatta kullanılabilir** bir çözüm üretmektir.  

Başlıca modüller:

| Modül            | Açıklama (örnek)                                   |
| ---------------- | -------------------------------------------------- |
| **Kullanıcı**    | Kayıt, oturum açma, rol bazlı yetkilendirme        |
| **İçerik**       | CRUD işlemleri (yazı, ürün, yorum vs.)             |
| **Yönetim Paneli**| Site ayarları, istatistikler, log takibi          |
| **API**          | RESTful JSON end-point’ler                         |

---

## Teknolojiler

| Katman          | Yığın / Araçlar (örnek)        |
| --------------- | ------------------------------ |
| **Backend**     | ASP.NET Core 8 + Entity Framework Core |
| **Frontend**    | HTML 5, SCSS/CSS, JavaScript (Vanilla) |
| **View Engine** | Razor / Handlebars (dersin gereğine göre) |
| **Veritabanı**  | SQL Server / SQLite            |
| **Diğer**       | Git & GitHub, VS Code / Visual Studio |

---

## Kurulum

### 1. Depoyu klonla

```bash
git clone https://github.com/ibrhmgldmr3/WebProgramlamaProje.git
cd WebProgramlamaProje
```

### 2. Bağımlılıkları yükle

#### .NET tarafı  
.NET SDK 8+ kurulu olmalı:

```bash
dotnet restore
```

#### (Opsiyonel) Front-end derleme adımı  
Eğer ayrı bir **npm** tabanlı varlık yönetimi kullanıyorsan:

```bash
cd WebProgProje/ClientApp
npm install
npm run build   # veya npm run dev
```

### 3. Veritabanını oluştur

```bash
# Varsayılan connection string’i kullanıyorsan:
dotnet ef database update
```

> Başka bir RDBMS kullanıyorsan `appsettings.json` içindeki **ConnectionStrings** değerini güncelle.

---

## Çalıştırma

Yerel geliştirme modunda başlat:

```bash
dotnet run --project WebProgProje
```

Sunucu ayağa kalktıktan sonra tarayıcıdan:

```
http://localhost:5000     # veya launchSettings.json’daki port
```

---

## Kullanım

1. **Kayıt Ol / Giriş Yap:** Sağ üst köşedeki *Register* veya *Login* sayfasına gidin.  
2. **Dashboard:** Oturum açınca yönetim paneline yönlendirilirsiniz.  
3. **İçerik Ekle:** *New Item* butonuyla yeni kayıt oluşturun.  
4. **API:** `/api/v1/...` altında JSON uç noktalarına erişebilirsiniz.

---

## Dizin Yapısı

```text
WebProgramlamaProje/
├── WebProgProje/          # .NET Core (backend + views)
│   ├── Controllers/
│   ├── Models/
│   ├── Views/
│   ├── wwwroot/           # statik dosyalar
│   └── WebProgProje.csproj
├── WebProgProje.Tests/    # xUnit birim testleri
├── scripts/               # yardımcı betikler
└── docs/                  # belgeler, ekran görüntüleri
```

---

---

## Yapılandırma (API anahtarları)

Bu projede kullanılan harici servislerin anahtarları **kaynak koda yazılmaz**; ASP.NET Core
konfigürasyon sisteminden okunur. `appsettings.json` yalnızca anahtarsız varsayılanları içerir.

Yerel geliştirmede **User Secrets** kullanın:

```bash
cd WebProgProje
dotnet user-secrets init
dotnet user-secrets set "HairstyleApi:ApiKey" "SIZIN_RAPIDAPI_ANAHTARINIZ"
dotnet user-secrets set "AILabApi:ApiKey"     "SIZIN_AILAB_ANAHTARINIZ"
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=...;Database=...;Trusted_Connection=True;"
```

Sunucu / CI ortamında ortam değişkeni de kullanılabilir (iki alt çizgi bölüm ayıracıdır):

```bash
export HairstyleApi__ApiKey="..."
export AILabApi__ApiKey="..."
export ApiSettings__BaseUrl="https://sunucu-adresiniz"
```

Kullanılan konfigürasyon anahtarları:

| Anahtar | Açıklama |
| ------- | -------- |
| `ConnectionStrings:DefaultConnection` | MS SQL bağlantı dizesi (boşsa LocalDB'ye düşer) |
| `ApiSettings:BaseUrl` | Uygulamanın kendi REST API'sinin adresi |
| `HairstyleApi:Endpoint` / `:Host` / `:ApiKey` | Saç modeli değiştirme servisi |
| `AILabApi:BaseUrl` / `:ApiKey` | Saç stili düzenleme servisi |

Anahtar tanımlı değilse ilgili uç nokta hata döndürür ve durum log'a yazılır; uygulama çökmez.

---

## Testler

Birim testleri `WebProgProje.Tests` projesindedir (xUnit + EF Core InMemory sağlayıcısı).
İstatistik REST API'sinin LINQ toplulaştırma sorguları ve kullanıcı servisi test edilir.

```bash
dotnet test
```

Testler gerçek bir veritabanına ihtiyaç duymaz; her test kendi izole InMemory veritabanını kurar.

---

## İletişim

| Kanal   | Bilgi                             |
| ------- | --------------------------------- |
| E-posta | ibrahimguldemir123@gmail.com      |
| LinkedIn| <https://www.linkedin.com/in/ibrhmgldmr/> |
| GitHub  | <https://github.com/ibrhmgldmr3>  |

---
