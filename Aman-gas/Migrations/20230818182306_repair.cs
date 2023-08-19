using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aman_gas.Migrations
{
    public partial class repair : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesRequest_Stations_StatioId",
                table: "SalesRequest");

            migrationBuilder.RenameColumn(
                name: "StatioId",
                table: "SalesRequest",
                newName: "StationId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesRequest_StatioId",
                table: "SalesRequest",
                newName: "IX_SalesRequest_StationId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "SalesRequest",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesRequest_Stations_StationId",
                table: "SalesRequest",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesRequest_Stations_StationId",
                table: "SalesRequest");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "SalesRequest");

            migrationBuilder.RenameColumn(
                name: "StationId",
                table: "SalesRequest",
                newName: "StatioId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesRequest_StationId",
                table: "SalesRequest",
                newName: "IX_SalesRequest_StatioId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesRequest_Stations_StatioId",
                table: "SalesRequest",
                column: "StatioId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
