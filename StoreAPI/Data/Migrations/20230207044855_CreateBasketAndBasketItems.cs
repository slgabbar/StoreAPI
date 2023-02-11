using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateBasketAndBasketItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Basket",
                columns: table => new
                {
                    BasketKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasketId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basket", x => x.BasketKey);
                });

            migrationBuilder.CreateTable(
                name: "BasketItem",
                columns: table => new
                {
                    BasketItemKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasketItemId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    BasketKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItem", x => x.BasketItemKey);
                    table.ForeignKey(
                        name: "FK_BasketItem_Basket_BasketKey",
                        column: x => x.BasketKey,
                        principalTable: "Basket",
                        principalColumn: "BasketKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketItem_Product_ProductKey",
                        column: x => x.ProductKey,
                        principalTable: "Product",
                        principalColumn: "ProductKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketItem_BasketKey",
                table: "BasketItem",
                column: "BasketKey");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItem_ProductKey",
                table: "BasketItem",
                column: "ProductKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketItem");

            migrationBuilder.DropTable(
                name: "Basket");
        }
    }
}
