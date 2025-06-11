# Web Programlama Projesi (2024-2025 Güz Dönemi)

> Sakarya Üniversitesi Bilgisayar Mühendisliği – Web Programlama dersi dönem projesi  
> **Geliştirici:** İbrahim Güldemir  
> **Repo:** https://github.com/ibrhmgldmr3/WebProgramlamaProje

---

## İçerik

- [Proje Hakkında](#proje-hakkında)
- [Teknolojiler](#teknolojiler)
- [Ekran Görüntüleri](#ekran-görüntüleri)
- [Kurulum](#kurulum)
- [Çalıştırma](#çalıştırma)
- [Kullanım](#kullanım)
- [Dizin Yapısı](#dizin-yapısı)
- [Katkıda Bulunma](#katkıda-bulunma)
- [Lisans](#lisans)
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
├── scripts/               # yardımcı betikler
└── docs/                  # belgeler, ekran görüntüleri
```

---


## İletişim

| Kanal   | Bilgi                             |
| ------- | --------------------------------- |
| E-posta | ibrahimguldemir123@gmail.com      |
| LinkedIn| <https://www.linkedin.com/in/ibrhmgldmr/> |
| GitHub  | <https://github.com/ibrhmgldmr3>  |

---
