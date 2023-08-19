using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aman_gas.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fuelings_Stations_StationId",
                table: "Fuelings");

            migrationBuilder.AddColumn<int>(
                name: "SalesManId",
                table: "Fuelings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Fuelings_SalesManId",
                table: "Fuelings",
                column: "SalesManId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fuelings_SalesMen_SalesManId",
                table: "Fuelings",
                column: "SalesManId",
                principalTable: "SalesMen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fuelings_Stations_StationId",
                table: "Fuelings",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fuelings_SalesMen_SalesManId",
                table: "Fuelings");

            migrationBuilder.DropForeignKey(
                name: "FK_Fuelings_Stations_StationId",
                table: "Fuelings");

            migrationBuilder.DropIndex(
                name: "IX_Fuelings_SalesManId",
                table: "Fuelings");

            migrationBuilder.DropColumn(
                name: "SalesManId",
                table: "Fuelings");

            migrationBuilder.AddForeignKey(
                name: "FK_Fuelings_Stations_StationId",
                table: "Fuelings",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
