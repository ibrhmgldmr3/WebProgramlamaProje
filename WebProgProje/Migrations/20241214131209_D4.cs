using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProgProje.Migrations
{
    /// <inheritdoc />
    public partial class D4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalonId",
                table: "Randevular",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_SalonId",
                table: "Randevular",
                column: "SalonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Salonlar_SalonId",
                table: "Randevular",
                column: "SalonId",
                principalTable: "Salonlar",
                principalColumn: "SalonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Salonlar_SalonId",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_SalonId",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "SalonId",
                table: "Randevular");
        }
    }
}
