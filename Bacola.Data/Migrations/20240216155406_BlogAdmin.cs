using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bacola.Data.Migrations
{
    public partial class BlogAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_Categories_CategoryId",
                table: "Blog");

            migrationBuilder.DropForeignKey(
                name: "FK_TagBlog_Blog_BlogId",
                table: "TagBlog");

            migrationBuilder.DropForeignKey(
                name: "FK_TagBlog_Tag_TagId",
                table: "TagBlog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blog",
                table: "Blog");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "Blog",
                newName: "Blogs");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_CategoryId",
                table: "Blogs",
                newName: "IX_Blogs_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Categories_CategoryId",
                table: "Blogs",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagBlog_Blogs_BlogId",
                table: "TagBlog",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagBlog_Tags_TagId",
                table: "TagBlog",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Categories_CategoryId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_TagBlog_Blogs_BlogId",
                table: "TagBlog");

            migrationBuilder.DropForeignKey(
                name: "FK_TagBlog_Tags_TagId",
                table: "TagBlog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "Blogs",
                newName: "Blog");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_CategoryId",
                table: "Blog",
                newName: "IX_Blog_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blog",
                table: "Blog",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_Categories_CategoryId",
                table: "Blog",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagBlog_Blog_BlogId",
                table: "TagBlog",
                column: "BlogId",
                principalTable: "Blog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagBlog_Tag_TagId",
                table: "TagBlog",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
