using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProgProje.Migrations
{
    /// <inheritdoc />
    public partial class d8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AIResults_Kullanicilar_KullaniciId1",
                table: "AIResults");

            migrationBuilder.DropForeignKey(
                name: "FK_Calisanlar_Uzmanliklar_UzmanlikId1",
                table: "Calisanlar");

            migrationBuilder.DropForeignKey(
                name: "FK_CalisanUygunluklar_Calisanlar_CalisanId1",
                table: "CalisanUygunluklar");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Calisanlar_CalisanId1",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Kullanicilar_KullaniciId1",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Uzmanliklar_Islemler_IslemId",
                table: "Uzmanliklar");

            migrationBuilder.DropIndex(
                name: "IX_Uzmanliklar_IslemId",
                table: "Uzmanliklar");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_CalisanId1",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_KullaniciId1",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_CalisanUygunluklar_CalisanId1",
                table: "CalisanUygunluklar");

            migrationBuilder.DropIndex(
                name: "IX_Calisanlar_UzmanlikId1",
                table: "Calisanlar");

            migrationBuilder.DropIndex(
                name: "IX_AIResults_KullaniciId1",
                table: "AIResults");

            migrationBuilder.DropColumn(
                name: "IslemId",
                table: "Uzmanliklar");

            migrationBuilder.DropColumn(
                name: "CalisanId1",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "KullaniciId1",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "CalisanId1",
                table: "CalisanUygunluklar");

            migrationBuilder.DropColumn(
                name: "UzmanlikId1",
                table: "Calisanlar");

            migrationBuilder.DropColumn(
                name: "KullaniciId1",
                table: "AIResults");

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

            migrationBuilder.CreateIndex(
                name: "IX_IslemUzmanliklar_UzmanlikId",
                table: "IslemUzmanliklar",
                column: "UzmanlikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IslemUzmanliklar");

            migrationBuilder.AddColumn<int>(
                name: "IslemId",
                table: "Uzmanliklar",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CalisanId1",
                table: "Randevular",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KullaniciId1",
                table: "Randevular",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CalisanId1",
                table: "CalisanUygunluklar",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UzmanlikId1",
                table: "Calisanlar",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KullaniciId1",
                table: "AIResults",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Uzmanliklar_IslemId",
                table: "Uzmanliklar",
                column: "IslemId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_CalisanId1",
                table: "Randevular",
                column: "CalisanId1");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_KullaniciId1",
                table: "Randevular",
                column: "KullaniciId1");

            migrationBuilder.CreateIndex(
                name: "IX_CalisanUygunluklar_CalisanId1",
                table: "CalisanUygunluklar",
                column: "CalisanId1");

            migrationBuilder.CreateIndex(
                name: "IX_Calisanlar_UzmanlikId1",
                table: "Calisanlar",
                column: "UzmanlikId1");

            migrationBuilder.CreateIndex(
                name: "IX_AIResults_KullaniciId1",
                table: "AIResults",
                column: "KullaniciId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AIResults_Kullanicilar_KullaniciId1",
                table: "AIResults",
                column: "KullaniciId1",
                principalTable: "Kullanicilar",
                principalColumn: "KullaniciId");

            migrationBuilder.AddForeignKey(
                name: "FK_Calisanlar_Uzmanliklar_UzmanlikId1",
                table: "Calisanlar",
                column: "UzmanlikId1",
                principalTable: "Uzmanliklar",
                principalColumn: "UzmanlikId");

            migrationBuilder.AddForeignKey(
                name: "FK_CalisanUygunluklar_Calisanlar_CalisanId1",
                table: "CalisanUygunluklar",
                column: "CalisanId1",
                principalTable: "Calisanlar",
                principalColumn: "CalisanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Calisanlar_CalisanId1",
                table: "Randevular",
                column: "CalisanId1",
                principalTable: "Calisanlar",
                principalColumn: "CalisanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Kullanicilar_KullaniciId1",
                table: "Randevular",
                column: "KullaniciId1",
                principalTable: "Kullanicilar",
                principalColumn: "KullaniciId");

            migrationBuilder.AddForeignKey(
                name: "FK_Uzmanliklar_Islemler_IslemId",
                table: "Uzmanliklar",
                column: "IslemId",
                principalTable: "Islemler",
                principalColumn: "IslemId");
        }
    }
}
