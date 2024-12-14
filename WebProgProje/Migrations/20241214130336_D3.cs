using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProgProje.Migrations
{
    /// <inheritdoc />
    public partial class D3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "Tarih",
                table: "Randevular",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilResmi",
                table: "Kullanicilar",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "CalismaSaatiCikis",
                table: "Calisanlar",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "CalismaSaatiGiris",
                table: "Calisanlar",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilResmi",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "CalismaSaatiCikis",
                table: "Calisanlar");

            migrationBuilder.DropColumn(
                name: "CalismaSaatiGiris",
                table: "Calisanlar");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Tarih",
                table: "Randevular",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
