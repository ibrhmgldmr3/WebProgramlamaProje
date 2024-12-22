using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProgProje.Migrations
{
    /// <inheritdoc />
    public partial class d7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Islemler",
                columns: table => new
                {
                    IslemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sure = table.Column<TimeSpan>(type: "time", nullable: false),
                    Ucret = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Islemler", x => x.IslemId);
                });

            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    KullaniciId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ProfilResmi = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.KullaniciId);
                });

            migrationBuilder.CreateTable(
                name: "Salonlar",
                columns: table => new
                {
                    SalonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Isim = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalismaBaslangic = table.Column<TimeSpan>(type: "time", nullable: false),
                    CalismaBitis = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salonlar", x => x.SalonId);
                });

            migrationBuilder.CreateTable(
                name: "Uzmanliklar",
                columns: table => new
                {
                    UzmanlikId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzmanliklar", x => x.UzmanlikId);
                });

            migrationBuilder.CreateTable(
                name: "AIResults",
                columns: table => new
                {
                    AIResultId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciId = table.Column<int>(type: "int", nullable: true),
                    SuggestedColor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SuggestedStyle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIResults", x => x.AIResultId);
                    table.ForeignKey(
                        name: "FK_AIResults_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "KullaniciId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Calisanlar",
                columns: table => new
                {
                    CalisanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciId = table.Column<int>(type: "int", nullable: true),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UzmanlikId = table.Column<int>(type: "int", nullable: false),
                    SalonId = table.Column<int>(type: "int", nullable: true),
                    SalonId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calisanlar", x => x.CalisanId);
                    table.ForeignKey(
                        name: "FK_Calisanlar_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "KullaniciId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Calisanlar_Salonlar_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salonlar",
                        principalColumn: "SalonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Calisanlar_Salonlar_SalonId1",
                        column: x => x.SalonId1,
                        principalTable: "Salonlar",
                        principalColumn: "SalonId");
                    table.ForeignKey(
                        name: "FK_Calisanlar_Uzmanliklar_UzmanlikId",
                        column: x => x.UzmanlikId,
                        principalTable: "Uzmanliklar",
                        principalColumn: "UzmanlikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IslemUzmanliklar",
                columns: table => new
                {
                    IslemId = table.Column<int>(type: "int", nullable: false),
                    UzmanlikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IslemUzmanliklar", x => new { x.IslemId, x.UzmanlikId });
                    table.ForeignKey(
                        name: "FK_IslemUzmanliklar_Islemler_IslemId",
                        column: x => x.IslemId,
                        principalTable: "Islemler",
                        principalColumn: "IslemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IslemUzmanliklar_Uzmanliklar_UzmanlikId",
                        column: x => x.UzmanlikId,
                        principalTable: "Uzmanliklar",
                        principalColumn: "UzmanlikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalisanUygunluklar",
                columns: table => new
                {
                    CalisanUygunlukId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalisanId = table.Column<int>(type: "int", nullable: true),
                    Gun = table.Column<int>(type: "int", nullable: false),
                    Baslangic = table.Column<TimeSpan>(type: "time", nullable: false),
                    Bitis = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalisanUygunluklar", x => x.CalisanUygunlukId);
                    table.ForeignKey(
                        name: "FK_CalisanUygunluklar_Calisanlar_CalisanId",
                        column: x => x.CalisanId,
                        principalTable: "Calisanlar",
                        principalColumn: "CalisanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Randevular",
                columns: table => new
                {
                    RandevuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalisanId = table.Column<int>(type: "int", nullable: true),
                    IslemId = table.Column<int>(type: "int", nullable: true),
                    SalonId = table.Column<int>(type: "int", nullable: true),
                    KullaniciId = table.Column<int>(type: "int", nullable: true),
                    Tarih = table.Column<DateOnly>(type: "date", nullable: false),
                    Saat = table.Column<TimeSpan>(type: "time", nullable: false),
                    OnaylandiMi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.RandevuId);
                    table.ForeignKey(
                        name: "FK_Randevular_Calisanlar_CalisanId",
                        column: x => x.CalisanId,
                        principalTable: "Calisanlar",
                        principalColumn: "CalisanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Randevular_Islemler_IslemId",
                        column: x => x.IslemId,
                        principalTable: "Islemler",
                        principalColumn: "IslemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Randevular_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "KullaniciId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Randevular_Salonlar_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salonlar",
                        principalColumn: "SalonId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AIResults_KullaniciId",
                table: "AIResults",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Calisanlar_KullaniciId",
                table: "Calisanlar",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Calisanlar_SalonId",
                table: "Calisanlar",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_Calisanlar_SalonId1",
                table: "Calisanlar",
                column: "SalonId1");

            migrationBuilder.CreateIndex(
                name: "IX_Calisanlar_UzmanlikId",
                table: "Calisanlar",
                column: "UzmanlikId");

            migrationBuilder.CreateIndex(
                name: "IX_CalisanUygunluklar_CalisanId",
                table: "CalisanUygunluklar",
                column: "CalisanId");

            migrationBuilder.CreateIndex(
                name: "IX_IslemUzmanliklar_UzmanlikId",
                table: "IslemUzmanliklar",
                column: "UzmanlikId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_CalisanId",
                table: "Randevular",
                column: "CalisanId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_IslemId",
                table: "Randevular",
                column: "IslemId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_KullaniciId",
                table: "Randevular",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_SalonId",
                table: "Randevular",
                column: "SalonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AIResults");

            migrationBuilder.DropTable(
                name: "CalisanUygunluklar");

            migrationBuilder.DropTable(
                name: "IslemUzmanliklar");

            migrationBuilder.DropTable(
                name: "Randevular");

            migrationBuilder.DropTable(
                name: "Calisanlar");

            migrationBuilder.DropTable(
                name: "Islemler");

            migrationBuilder.DropTable(
                name: "Kullanicilar");

            migrationBuilder.DropTable(
                name: "Salonlar");

            migrationBuilder.DropTable(
                name: "Uzmanliklar");
        }
    }
}
