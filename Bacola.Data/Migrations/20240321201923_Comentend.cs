using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bacola.Data.Migrations
{
    public partial class Comentend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "Replies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Replies_BlogId",
                table: "Replies",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Blogs_BlogId",
                table: "Replies",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Blogs_BlogId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_BlogId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "Replies");
        }
    }
}
