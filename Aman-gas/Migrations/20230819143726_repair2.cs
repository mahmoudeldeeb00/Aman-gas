using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aman_gas.Migrations
{
    public partial class repair2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FueltTpeId",
                table: "PointsRatios",
                newName: "FueltTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FueltTypeId",
                table: "PointsRatios",
                newName: "FueltTpeId");
        }
    }
}
