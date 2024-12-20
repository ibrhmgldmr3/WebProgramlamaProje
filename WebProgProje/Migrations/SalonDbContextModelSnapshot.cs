﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebProgramlamaProje.Models;

#nullable disable

namespace WebProgramlamaProje.Migrations
{
    [DbContext(typeof(SalonDbContext))]
    partial class SalonDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebProgramlamaProje.Models.AIResult", b =>
                {
                    b.Property<int>("AIResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AIResultId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("KullaniciId")
                        .HasColumnType("int");

                    b.Property<string>("SuggestedColor")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SuggestedStyle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AIResultId");

                    b.HasIndex("KullaniciId");

                    b.ToTable("AIResults");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.Calisan", b =>
                {
                    b.Property<int>("CalisanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CalisanId"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("KullaniciId")
                        .HasColumnType("int");

                    b.Property<int?>("SalonId")
                        .HasColumnType("int");

                    b.Property<int?>("SalonId1")
                        .HasColumnType("int");

                    b.Property<string>("Soyad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UzmanlikId")
                        .HasColumnType("int");

                    b.HasKey("CalisanId");

                    b.HasIndex("KullaniciId");

                    b.HasIndex("SalonId");

                    b.HasIndex("SalonId1");

                    b.HasIndex("UzmanlikId");

                    b.ToTable("Calisanlar");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.CalisanUygunluk", b =>
                {
                    b.Property<int>("CalisanUygunlukId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CalisanUygunlukId"));

                    b.Property<TimeSpan>("Baslangic")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("Bitis")
                        .HasColumnType("time");

                    b.Property<int?>("CalisanId")
                        .HasColumnType("int");

                    b.Property<int>("Gun")
                        .HasColumnType("int");

                    b.HasKey("CalisanUygunlukId");

                    b.HasIndex("CalisanId");

                    b.ToTable("CalisanUygunluklar");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.Islem", b =>
                {
                    b.Property<int>("IslemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IslemId"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<TimeSpan>("Sure")
                        .HasColumnType("time");

                    b.Property<decimal>("Ucret")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IslemId");

                    b.ToTable("Islemler");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.IslemUzmanlik", b =>
                {
                    b.Property<int>("IslemId")
                        .HasColumnType("int");

                    b.Property<int>("UzmanlikId")
                        .HasColumnType("int");

                    b.HasKey("IslemId", "UzmanlikId");

                    b.HasIndex("UzmanlikId");

                    b.ToTable("IslemUzmanliklar");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.Kullanici", b =>
                {
                    b.Property<int>("KullaniciId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KullaniciId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<byte[]>("ProfilResmi")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KullaniciId");

                    b.ToTable("Kullanicilar");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.Randevu", b =>
                {
                    b.Property<int>("RandevuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RandevuId"));

                    b.Property<int?>("CalisanId")
                        .HasColumnType("int");

                    b.Property<int?>("IslemId")
                        .HasColumnType("int");

                    b.Property<int?>("KullaniciId")
                        .HasColumnType("int");

                    b.Property<bool>("OnaylandiMi")
                        .HasColumnType("bit");

                    b.Property<TimeSpan>("Saat")
                        .HasColumnType("time");

                    b.Property<int?>("SalonId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("Tarih")
                        .HasColumnType("date");

                    b.HasKey("RandevuId");

                    b.HasIndex("CalisanId");

                    b.HasIndex("IslemId");

                    b.HasIndex("KullaniciId");

                    b.HasIndex("SalonId");

                    b.ToTable("Randevular");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.Salon", b =>
                {
                    b.Property<int>("SalonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SalonId"));

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("CalismaBaslangic")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("CalismaBitis")
                        .HasColumnType("time");

                    b.Property<string>("Isim")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Telefon")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Tip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SalonId");

                    b.ToTable("Salonlar");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.Uzmanlik", b =>
                {
                    b.Property<int>("UzmanlikId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UzmanlikId"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UzmanlikId");

                    b.ToTable("Uzmanliklar");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.AIResult", b =>
                {
                    b.HasOne("WebProgramlamaProje.Models.Kullanici", "Kullanici")
                        .WithMany("AIResults")
                        .HasForeignKey("KullaniciId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Kullanici");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.Calisan", b =>
                {
                    b.HasOne("WebProgramlamaProje.Models.Kullanici", "Kullanici")
                        .WithMany()
                        .HasForeignKey("KullaniciId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WebProgramlamaProje.Models.Salon", "Salon")
                        .WithMany()
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WebProgramlamaProje.Models.Salon", null)
                        .WithMany("Calisanlar")
                        .HasForeignKey("SalonId1");

                    b.HasOne("WebProgramlamaProje.Models.Uzmanlik", "Uzmanlik")
                        .WithMany("Calisanlar")
                        .HasForeignKey("UzmanlikId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Kullanici");

                    b.Navigation("Salon");

                    b.Navigation("Uzmanlik");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.CalisanUygunluk", b =>
                {
                    b.HasOne("WebProgramlamaProje.Models.Calisan", "Calisan")
                        .WithMany("CalisanUygunluklar")
                        .HasForeignKey("CalisanId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Calisan");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.IslemUzmanlik", b =>
                {
                    b.HasOne("WebProgramlamaProje.Models.Islem", "Islem")
                        .WithMany("IslemUzmanliklar")
                        .HasForeignKey("IslemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebProgramlamaProje.Models.Uzmanlik", "Uzmanlik")
                        .WithMany("IslemUzmanliklar")
                        .HasForeignKey("UzmanlikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Islem");

                    b.Navigation("Uzmanlik");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.Randevu", b =>
                {
                    b.HasOne("WebProgramlamaProje.Models.Calisan", "Calisan")
                        .WithMany("Randevular")
                        .HasForeignKey("CalisanId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WebProgramlamaProje.Models.Islem", "Islem")
                        .WithMany()
                        .HasForeignKey("IslemId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WebProgramlamaProje.Models.Kullanici", "Kullanici")
                        .WithMany("Randevular")
                        .HasForeignKey("KullaniciId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebProgramlamaProje.Models.Salon", "Salon")
                        .WithMany()
                        .HasForeignKey("SalonId");

                    b.Navigation("Calisan");

                    b.Navigation("Islem");

                    b.Navigation("Kullanici");

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.Calisan", b =>
                {
                    b.Navigation("CalisanUygunluklar");

                    b.Navigation("Randevular");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.Islem", b =>
                {
                    b.Navigation("IslemUzmanliklar");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.Kullanici", b =>
                {
                    b.Navigation("AIResults");

                    b.Navigation("Randevular");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.Salon", b =>
                {
                    b.Navigation("Calisanlar");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.Uzmanlik", b =>
                {
                    b.Navigation("Calisanlar");

                    b.Navigation("IslemUzmanliklar");
                });
#pragma warning restore 612, 618
        }
    }
}