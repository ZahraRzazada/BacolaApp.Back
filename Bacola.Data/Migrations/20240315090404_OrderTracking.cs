using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bacola.Data.Migrations
{
    public partial class OrderTracking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderTracking",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderTracking",
                table: "Orders");
        }
    }
}
