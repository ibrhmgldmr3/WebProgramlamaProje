﻿using System.ComponentModel.DataAnnotations;

namespace WebProgramlamaProje.Models
{
    public class Randevu
    {
        public int RandevuId { get; set; }
        [Display(Name = "Çalışan")]
        public int? CalisanId { get; set; }
        public Calisan? Calisan { get; set; }
        [Display(Name = "Yapılacak İşlem")]
        public int? IslemId { get; set; }
        public Islem? Islem { get; set; }
        [Display(Name = "İşlemin Yapılacağı Salonumuz")]
        public int? SalonId { get; set; }
        public Salon? Salon { get; set; }

        // Kullanıcı ile ilişki
        public int? KullaniciId { get; set; }
        public Kullanici? Kullanici { get; set; }

        public DateOnly Tarih { get; set; }
        public TimeSpan Saat { get; set; }
        public bool OnaylandiMi { get; set; }
    }


}