using Microsoft.EntityFrameworkCore;
using WebProgramlamaProje.Models;

public class SalonDbContext : DbContext
{
    public SalonDbContext(DbContextOptions<SalonDbContext> options) : base(options)
    {
    }

    // DbSet Tanımları
    public DbSet<Kullanici> Kullanicilar { get; set; }
    public DbSet<Randevu> Randevular { get; set; }
    public DbSet<AIResult> AIResults { get; set; }

    // Model ilişkilerini tanımlamak için OnModelCreating metodu
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Kullanıcı ve Randevular arasında birden çoğa ilişki
        modelBuilder.Entity<Kullanici>()
            .HasMany(k => k.Randevular)
            .WithOne(r => r.Kullanici)
            .HasForeignKey(r => r.KullaniciId);

        // Kullanıcı ve AIResult arasında birden çoğa ilişki
        modelBuilder.Entity<Kullanici>()
            .HasMany(k => k.AIResult)
            .WithOne(ai => ai.Kullanici)
            .HasForeignKey(ai => ai.KullaniciId);
    }
}
