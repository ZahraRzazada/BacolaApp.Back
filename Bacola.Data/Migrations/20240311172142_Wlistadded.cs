using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bacola.Data.Migrations
{
    public partial class Wlistadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_AspNetUsers_AppUserId",
                table: "Basket");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketItem_Basket_BasketId",
                table: "BasketItem");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketItem_Products_ProductId",
                table: "BasketItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketItem",
                table: "BasketItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Basket",
                table: "Basket");

            migrationBuilder.RenameTable(
                name: "BasketItem",
                newName: "BasketItems");

            migrationBuilder.RenameTable(
                name: "Basket",
                newName: "Baskets");

            migrationBuilder.RenameIndex(
                name: "IX_BasketItem_ProductId",
                table: "BasketItems",
                newName: "IX_BasketItems_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketItem_BasketId",
                table: "BasketItems",
                newName: "IX_BasketItems_BasketId");

            migrationBuilder.RenameIndex(
                name: "IX_Basket_AppUserId",
                table: "Baskets",
                newName: "IX_Baskets_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketItems",
                table: "BasketItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Baskets",
                table: "Baskets",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Wishlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wishlists_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishlistItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    WishlistId = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishlistItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistItems_Wishlists_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Wishlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_ProductId",
                table: "WishlistItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_WishlistId",
                table: "WishlistItems",
                column: "WishlistId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_AppUserId",
                table: "Wishlists",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Baskets_BasketId",
                table: "BasketItems",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Products_ProductId",
                table: "BasketItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_AspNetUsers_AppUserId",
                table: "Baskets",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Baskets_BasketId",
                table: "BasketItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Products_ProductId",
                table: "BasketItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_AspNetUsers_AppUserId",
                table: "Baskets");

            migrationBuilder.DropTable(
                name: "WishlistItems");

            migrationBuilder.DropTable(
                name: "Wishlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Baskets",
                table: "Baskets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketItems",
                table: "BasketItems");

            migrationBuilder.RenameTable(
                name: "Baskets",
                newName: "Basket");

            migrationBuilder.RenameTable(
                name: "BasketItems",
                newName: "BasketItem");

            migrationBuilder.RenameIndex(
                name: "IX_Baskets_AppUserId",
                table: "Basket",
                newName: "IX_Basket_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketItems_ProductId",
                table: "BasketItem",
                newName: "IX_BasketItem_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketItems_BasketId",
                table: "BasketItem",
                newName: "IX_BasketItem_BasketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Basket",
                table: "Basket",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketItem",
                table: "BasketItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_AspNetUsers_AppUserId",
                table: "Basket",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItem_Basket_BasketId",
                table: "BasketItem",
                column: "BasketId",
                principalTable: "Basket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItem_Products_ProductId",
                table: "BasketItem",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
