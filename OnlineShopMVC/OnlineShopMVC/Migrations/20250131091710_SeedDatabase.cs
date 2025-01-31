using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShopMVC.Migrations
{
    /// <inheritdoc />
    public partial class SeedDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Philosophy and academic Books", "Books" },
                    { 2, "Devices and gadgets", "Electronics" },
                    { 3, "Men's & Women's fashion", "Clothing" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Media", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 1, 2, "Powerful laptop for gaming and work", "[\"/images/laptop-1.jpg\"]", "Laptop", 1259.99m, 30 },
                    { 2, 2, "Latest model with integrated AI Assistant", "[\"/images/phone-1.jpg\"]", "Smartphone", 699.99m, 50 },
                    { 3, 1, "Reflect upon different aspects of life with Marcus Aurelius", "[\"/images/meditationsBook.jpg\"]", "Meditations", 19.99m, 25 },
                    { 4, 1, "Learn C# with this in-depth guide", "[\"/images/cSharpBook.jpg\"]", "Programming Book", 29.99m, 75 },
                    { 5, 3, "100% cotton t-shirt for both men and women", "[\"/images/tShirt.jpg\"]", "T-Shirt", 14.99m, 100 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
