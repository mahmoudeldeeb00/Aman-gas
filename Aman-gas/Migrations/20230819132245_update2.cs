using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aman_gas.Migrations
{
    public partial class update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesMen_Stations_StationId",
                table: "SalesMen");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesMen_Stations_StationId",
                table: "SalesMen",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesMen_Stations_StationId",
                table: "SalesMen");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesMen_Stations_StationId",
                table: "SalesMen",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
