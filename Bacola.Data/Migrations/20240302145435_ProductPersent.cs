using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bacola.Data.Migrations
{
    public partial class ProductPersent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DiscountPercent",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "Products");
        }
    }
}
