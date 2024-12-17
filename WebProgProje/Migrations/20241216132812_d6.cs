using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProgProje.Migrations
{
    /// <inheritdoc />
    public partial class d6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CalisanId1",
                table: "CalisanUygunluklar",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CalisanUygunluklar_CalisanId1",
                table: "CalisanUygunluklar",
                column: "CalisanId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CalisanUygunluklar_Calisanlar_CalisanId1",
                table: "CalisanUygunluklar",
                column: "CalisanId1",
                principalTable: "Calisanlar",
                principalColumn: "CalisanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalisanUygunluklar_Calisanlar_CalisanId1",
                table: "CalisanUygunluklar");

            migrationBuilder.DropIndex(
                name: "IX_CalisanUygunluklar_CalisanId1",
                table: "CalisanUygunluklar");

            migrationBuilder.DropColumn(
                name: "CalisanId1",
                table: "CalisanUygunluklar");
        }
    }
}
