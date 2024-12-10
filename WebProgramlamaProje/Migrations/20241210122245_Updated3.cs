using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProgramlamaProje.Migrations
{
    /// <inheritdoc />
    public partial class Updated3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calisan_Salon_SalonId",
                table: "Calisan");

            migrationBuilder.DropForeignKey(
                name: "FK_CalisanUygunluk_Calisan_CalisanId",
                table: "CalisanUygunluk");

            migrationBuilder.DropForeignKey(
                name: "FK_Islem_Salon_SalonId",
                table: "Islem");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Calisan_CalisanId",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Islem_IslemId",
                table: "Randevular");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Salon",
                table: "Salon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Islem",
                table: "Islem");

            migrationBuilder.DropIndex(
                name: "IX_Islem_SalonId",
                table: "Islem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CalisanUygunluk",
                table: "CalisanUygunluk");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Calisan",
                table: "Calisan");

            migrationBuilder.DropColumn(
                name: "SalonId",
                table: "Islem");

            migrationBuilder.RenameTable(
                name: "Salon",
                newName: "Salonlar");

            migrationBuilder.RenameTable(
                name: "Islem",
                newName: "Islemler");

            migrationBuilder.RenameTable(
                name: "CalisanUygunluk",
                newName: "CalisanUygunluklar");

            migrationBuilder.RenameTable(
                name: "Calisan",
                newName: "Calisanlar");

            migrationBuilder.RenameColumn(
                name: "ModelName",
                table: "AIResults",
                newName: "SuggestedStyle");

            migrationBuilder.RenameIndex(
                name: "IX_CalisanUygunluk_CalisanId",
                table: "CalisanUygunluklar",
                newName: "IX_CalisanUygunluklar_CalisanId");

            migrationBuilder.RenameIndex(
                name: "IX_Calisan_SalonId",
                table: "Calisanlar",
                newName: "IX_Calisanlar_SalonId");

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

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Kullanicilar",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Kullanicilar",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Kullanicilar",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SuggestedColor",
                table: "AIResults",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Telefon",
                table: "Salonlar",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Isim",
                table: "Salonlar",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<float>(
                name: "Ucret",
                table: "Islemler",
                type: "real(18)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Ad",
                table: "Islemler",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "KullaniciId",
                table: "Calisanlar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalonId1",
                table: "Calisanlar",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Salonlar",
                table: "Salonlar",
                column: "SalonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Islemler",
                table: "Islemler",
                column: "IslemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalisanUygunluklar",
                table: "CalisanUygunluklar",
                column: "CalisanUygunlukId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Calisanlar",
                table: "Calisanlar",
                column: "CalisanId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_CalisanId1",
                table: "Randevular",
                column: "CalisanId1");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_KullaniciId1",
                table: "Randevular",
                column: "KullaniciId1");

            migrationBuilder.CreateIndex(
                name: "IX_Calisanlar_KullaniciId",
                table: "Calisanlar",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Calisanlar_SalonId1",
                table: "Calisanlar",
                column: "SalonId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Calisanlar_Kullanicilar_KullaniciId",
                table: "Calisanlar",
                column: "KullaniciId",
                principalTable: "Kullanicilar",
                principalColumn: "KullaniciId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Calisanlar_Salonlar_SalonId",
                table: "Calisanlar",
                column: "SalonId",
                principalTable: "Salonlar",
                principalColumn: "SalonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Calisanlar_Salonlar_SalonId1",
                table: "Calisanlar",
                column: "SalonId1",
                principalTable: "Salonlar",
                principalColumn: "SalonId");

            migrationBuilder.AddForeignKey(
                name: "FK_CalisanUygunluklar_Calisanlar_CalisanId",
                table: "CalisanUygunluklar",
                column: "CalisanId",
                principalTable: "Calisanlar",
                principalColumn: "CalisanId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Calisanlar_CalisanId",
                table: "Randevular",
                column: "CalisanId",
                principalTable: "Calisanlar",
                principalColumn: "CalisanId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Calisanlar_CalisanId1",
                table: "Randevular",
                column: "CalisanId1",
                principalTable: "Calisanlar",
                principalColumn: "CalisanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Islemler_IslemId",
                table: "Randevular",
                column: "IslemId",
                principalTable: "Islemler",
                principalColumn: "IslemId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Kullanicilar_KullaniciId1",
                table: "Randevular",
                column: "KullaniciId1",
                principalTable: "Kullanicilar",
                principalColumn: "KullaniciId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calisanlar_Kullanicilar_KullaniciId",
                table: "Calisanlar");

            migrationBuilder.DropForeignKey(
                name: "FK_Calisanlar_Salonlar_SalonId",
                table: "Calisanlar");

            migrationBuilder.DropForeignKey(
                name: "FK_Calisanlar_Salonlar_SalonId1",
                table: "Calisanlar");

            migrationBuilder.DropForeignKey(
                name: "FK_CalisanUygunluklar_Calisanlar_CalisanId",
                table: "CalisanUygunluklar");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Calisanlar_CalisanId",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Calisanlar_CalisanId1",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Islemler_IslemId",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Kullanicilar_KullaniciId1",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_CalisanId1",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_KullaniciId1",
                table: "Randevular");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Salonlar",
                table: "Salonlar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Islemler",
                table: "Islemler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CalisanUygunluklar",
                table: "CalisanUygunluklar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Calisanlar",
                table: "Calisanlar");

            migrationBuilder.DropIndex(
                name: "IX_Calisanlar_KullaniciId",
                table: "Calisanlar");

            migrationBuilder.DropIndex(
                name: "IX_Calisanlar_SalonId1",
                table: "Calisanlar");

            migrationBuilder.DropColumn(
                name: "CalisanId1",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "KullaniciId1",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "KullaniciId",
                table: "Calisanlar");

            migrationBuilder.DropColumn(
                name: "SalonId1",
                table: "Calisanlar");

            migrationBuilder.RenameTable(
                name: "Salonlar",
                newName: "Salon");

            migrationBuilder.RenameTable(
                name: "Islemler",
                newName: "Islem");

            migrationBuilder.RenameTable(
                name: "CalisanUygunluklar",
                newName: "CalisanUygunluk");

            migrationBuilder.RenameTable(
                name: "Calisanlar",
                newName: "Calisan");

            migrationBuilder.RenameColumn(
                name: "SuggestedStyle",
                table: "AIResults",
                newName: "ModelName");

            migrationBuilder.RenameIndex(
                name: "IX_CalisanUygunluklar_CalisanId",
                table: "CalisanUygunluk",
                newName: "IX_CalisanUygunluk_CalisanId");

            migrationBuilder.RenameIndex(
                name: "IX_Calisanlar_SalonId",
                table: "Calisan",
                newName: "IX_Calisan_SalonId");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Kullanicilar",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Kullanicilar",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Kullanicilar",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "SuggestedColor",
                table: "AIResults",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Telefon",
                table: "Salon",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Isim",
                table: "Salon",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<decimal>(
                name: "Ucret",
                table: "Islem",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real(18)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Ad",
                table: "Islem",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "SalonId",
                table: "Islem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Salon",
                table: "Salon",
                column: "SalonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Islem",
                table: "Islem",
                column: "IslemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalisanUygunluk",
                table: "CalisanUygunluk",
                column: "CalisanUygunlukId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Calisan",
                table: "Calisan",
                column: "CalisanId");

            migrationBuilder.CreateIndex(
                name: "IX_Islem_SalonId",
                table: "Islem",
                column: "SalonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Calisan_Salon_SalonId",
                table: "Calisan",
                column: "SalonId",
                principalTable: "Salon",
                principalColumn: "SalonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CalisanUygunluk_Calisan_CalisanId",
                table: "CalisanUygunluk",
                column: "CalisanId",
                principalTable: "Calisan",
                principalColumn: "CalisanId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Islem_Salon_SalonId",
                table: "Islem",
                column: "SalonId",
                principalTable: "Salon",
                principalColumn: "SalonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Calisan_CalisanId",
                table: "Randevular",
                column: "CalisanId",
                principalTable: "Calisan",
                principalColumn: "CalisanId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Islem_IslemId",
                table: "Randevular",
                column: "IslemId",
                principalTable: "Islem",
                principalColumn: "IslemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
