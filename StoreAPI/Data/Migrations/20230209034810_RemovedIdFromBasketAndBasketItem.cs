using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedIdFromBasketAndBasketItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasketItemId",
                table: "BasketItem");

            migrationBuilder.DropColumn(
                name: "BasketId",
                table: "Basket");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BasketItemId",
                table: "BasketItem",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BasketId",
                table: "Basket",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");
        }
    }
}
