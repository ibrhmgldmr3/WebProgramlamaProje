using WebProgramlamaProje.Models;
using System.Linq;
using System;

public class KullaniciService
{
    private readonly SalonDbContext _context;

    public KullaniciService(SalonDbContext context)
    {
        _context = context;
    }

    public Kullanici GetKullaniciByEmail(string email)
    {
        return _context.Kullanicilar.SingleOrDefault(u => u.Email == email);
    }
}
