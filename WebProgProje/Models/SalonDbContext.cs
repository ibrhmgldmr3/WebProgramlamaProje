using Microsoft.EntityFrameworkCore;
using WebProgramlamaProje.Models;

public class SalonDbContext : DbContext
{
    public SalonDbContext(DbContextOptions<SalonDbContext> options) : base(options) { }

    // DbSet Properties
    public DbSet<Kullanici> Kullanicilar { get; set; }
    public DbSet<Salon> Salonlar { get; set; }
    public DbSet<Calisan> Calisanlar { get; set; }
    public DbSet<Islem> Islemler { get; set; }
    public DbSet<CalisanUygunluk> CalisanUygunluklar { get; set; }
    public DbSet<Randevu> Randevular { get; set; }
    public DbSet<AIResult> AIResults { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Kullanici
        modelBuilder.Entity<Kullanici>(entity =>
        {
            entity.HasKey(e => e.KullaniciId);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.Role).IsRequired();
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(15);
        });

        // Salon
        modelBuilder.Entity<Salon>(entity =>
        {
            entity.HasKey(e => e.SalonId);
            entity.Property(e => e.Isim).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Adres).IsRequired();
            entity.Property(e => e.Telefon).IsRequired().HasMaxLength(15);
            entity.Property(e => e.Tip).IsRequired();
            entity.Property(e => e.CalismaBaslangic).IsRequired();
            entity.Property(e => e.CalismaBitis).IsRequired();
        });

        // Calisan
        modelBuilder.Entity<Calisan>(entity =>
        {
            entity.HasKey(e => e.CalisanId);
            entity.HasOne(e => e.Kullanici)
                .WithMany()
                .HasForeignKey(e => e.KullaniciId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Salon)
                .WithMany()
                .HasForeignKey(e => e.SalonId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(e => e.Uzmanlik).IsRequired();
        });

        // Islem
        modelBuilder.Entity<Islem>(entity =>
        {
            entity.HasKey(e => e.IslemId);
            entity.Property(e => e.Ad).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Sure).IsRequired();
            entity.Property(e => e.Ucret).IsRequired().HasPrecision(18, 2);
        });

        // CalisanUygunluk
        modelBuilder.Entity<CalisanUygunluk>(entity =>
        {
            entity.HasKey(e => e.CalisanUygunlukId);
            entity.HasOne(e => e.Calisan)
                .WithMany()
                .HasForeignKey(e => e.CalisanId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.Baslangic).IsRequired();
            entity.Property(e => e.Bitis).IsRequired();
        });

        // Randevu
        modelBuilder.Entity<Randevu>(entity =>
        {
            entity.HasKey(r => r.RandevuId);

            entity.HasOne(r => r.Calisan)
                .WithMany()
                .HasForeignKey(r => r.CalisanId)
                .OnDelete(DeleteBehavior.Restrict); // Cascade kaldırıldı

            entity.HasOne(r => r.Islem)
                .WithMany()
                .HasForeignKey(r => r.IslemId)
                .OnDelete(DeleteBehavior.Restrict); // Cascade kaldırıldı

            entity.HasOne(r => r.Kullanici)
                .WithMany()
                .HasForeignKey(r => r.KullaniciId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(r => r.Tarih).IsRequired();
            entity.Property(r => r.Saat).IsRequired();
            entity.Property(r => r.OnaylandiMi).IsRequired();
        });

        // AIResult
        modelBuilder.Entity<AIResult>(entity =>
        {
            entity.HasKey(e => e.AIResultId);
            entity.HasOne(e => e.Kullanici)
                .WithMany()
                .HasForeignKey(e => e.KullaniciId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.SuggestedColor).IsRequired().HasMaxLength(50);
            entity.Property(e => e.CreatedAt).IsRequired();
        });

    }
}

// Entity sınıfları yukarıdaki kodun aynısıdır.
