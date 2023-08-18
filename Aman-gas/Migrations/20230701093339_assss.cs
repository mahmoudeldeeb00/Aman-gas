using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aman_gas.Migrations
{
    public partial class assss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SalesRequest_StatioId",
                table: "SalesRequest",
                column: "StatioId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesRequest_Stations_StatioId",
                table: "SalesRequest",
                column: "StatioId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesRequest_Stations_StatioId",
                table: "SalesRequest");

            migrationBuilder.DropIndex(
                name: "IX_SalesRequest_StatioId",
                table: "SalesRequest");
        }
    }
}
