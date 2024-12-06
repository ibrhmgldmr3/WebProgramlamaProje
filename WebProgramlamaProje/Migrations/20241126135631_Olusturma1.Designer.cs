﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace WebProgramlamaProje.Migrations
{
    [DbContext(typeof(SalonDbContext))]
    [Migration("20241126135631_Olusturma1")]
    partial class Olusturma1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("KullaniciId")
                        .HasColumnType("int");

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SuggestedColor")
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

                    b.Property<int>("SalonId")
                        .HasColumnType("int");

                    b.Property<string>("Soyad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Uzmanlik")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CalisanId");

                    b.HasIndex("SalonId");

                    b.ToTable("Calisan");
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

                    b.Property<int>("CalisanId")
                        .HasColumnType("int");

                    b.HasKey("CalisanUygunlukId");

                    b.HasIndex("CalisanId");

                    b.ToTable("CalisanUygunluk");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.Islem", b =>
                {
                    b.Property<int>("IslemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IslemId"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SalonId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Sure")
                        .HasColumnType("time");

                    b.Property<float>("Ucret")
                        .HasColumnType("real");

                    b.HasKey("IslemId");

                    b.HasIndex("SalonId");

                    b.ToTable("Islem");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.Kullanici", b =>
                {
                    b.Property<int>("KullaniciId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KullaniciId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

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

                    b.Property<int>("CalisanId")
                        .HasColumnType("int");

                    b.Property<int>("IslemId")
                        .HasColumnType("int");

                    b.Property<int>("KullaniciId")
                        .HasColumnType("int");

                    b.Property<bool>("OnaylandiMi")
                        .HasColumnType("bit");

                    b.Property<TimeSpan>("Saat")
                        .HasColumnType("time");

                    b.Property<DateTime>("Tarih")
                        .HasColumnType("datetime2");

                    b.HasKey("RandevuId");

                    b.HasIndex("CalisanId");

                    b.HasIndex("IslemId");

                    b.HasIndex("KullaniciId");

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SalonId");

                    b.ToTable("Salon");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.AIResult", b =>
                {
                    b.HasOne("WebProgramlamaProje.Models.Kullanici", "Kullanici")
                        .WithMany("AIResult")
                        .HasForeignKey("KullaniciId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kullanici");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.Calisan", b =>
                {
                    b.HasOne("WebProgramlamaProje.Models.Salon", "Salon")
                        .WithMany("Calisanlar")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.CalisanUygunluk", b =>
                {
                    b.HasOne("WebProgramlamaProje.Models.Calisan", "Calisan")
                        .WithMany("UygunlukSaatleri")
                        .HasForeignKey("CalisanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calisan");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.Islem", b =>
                {
                    b.HasOne("WebProgramlamaProje.Models.Salon", "Salon")
                        .WithMany("Islemler")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.Randevu", b =>
                {
                    b.HasOne("WebProgramlamaProje.Models.Calisan", "Calisan")
                        .WithMany()
                        .HasForeignKey("CalisanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebProgramlamaProje.Models.Islem", "Islem")
                        .WithMany()
                        .HasForeignKey("IslemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebProgramlamaProje.Models.Kullanici", "Kullanici")
                        .WithMany("Randevular")
                        .HasForeignKey("KullaniciId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calisan");

                    b.Navigation("Islem");

                    b.Navigation("Kullanici");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.Calisan", b =>
                {
                    b.Navigation("UygunlukSaatleri");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.Kullanici", b =>
                {
                    b.Navigation("AIResult");

                    b.Navigation("Randevular");
                });

            modelBuilder.Entity("WebProgramlamaProje.Models.Salon", b =>
                {
                    b.Navigation("Calisanlar");

                    b.Navigation("Islemler");
                });
#pragma warning restore 612, 618
        }
    }
}
