using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductManagement.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ProductCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    StockQty = table.Column<int>(type: "INTEGER", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Food" },
                    { 2, "Clothing" },
                    { 3, "Electronics" },
                    { 4, "Outdoor" },
                    { 5, "Books" },
                    { 6, "Health" },
                    { 7, "Furniture" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "DateAdded", "Price", "ProductCode", "ProductName", "StockQty" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 11, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 3.00m, "SKU0010", "Bread", 15 },
                    { 2, 1, new DateTime(2024, 11, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 4.00m, "SKU0011", "Ham", 18 },
                    { 3, 1, new DateTime(2024, 11, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 5.00m, "SKU0012", "Cheese", 17 },
                    { 4, 2, new DateTime(2024, 11, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 30.50m, "SKU0013", "Joggers", 12 },
                    { 5, 2, new DateTime(2024, 11, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 20.99m, "SKU0014", "T-Shirt", 4 },
                    { 6, 2, new DateTime(2024, 11, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 46.75m, "SKU0015", "Coat", 11 },
                    { 7, 3, new DateTime(2024, 11, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, "SKU0016", "Iron", 12 },
                    { 8, 3, new DateTime(2024, 11, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 30.15m, "SKU0017", "Microwave", 18 },
                    { 9, 3, new DateTime(2024, 11, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 58.00m, "SKU0018", "Hoover", 14 },
                    { 10, 4, new DateTime(2024, 11, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 5.00m, "SKU0019", "Football", 101 },
                    { 11, 4, new DateTime(2024, 11, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 30.00m, "SKU0020", "Tent", 64 },
                    { 12, 4, new DateTime(2024, 11, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 144.95m, "SKU0021", "Surfboard", 23 },
                    { 13, 5, new DateTime(2024, 11, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, "SKU0022", "Harry Potter", 55 },
                    { 14, 5, new DateTime(2024, 11, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 12.80m, "SKU0023", "Alan Watts", 12 },
                    { 15, 5, new DateTime(2024, 11, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 8.33m, "SKU0024", "Fundamental Physics", 17 },
                    { 16, 6, new DateTime(2024, 11, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 2.00m, "SKU0025", "Vitamin C", 42 },
                    { 17, 6, new DateTime(2024, 11, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 5.00m, "SKU0026", "Ibuprofen", 115 },
                    { 18, 6, new DateTime(2024, 11, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 10.65m, "SKU0027", "Sunscreen SPF 30", 53 },
                    { 19, 7, new DateTime(2024, 11, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 42.00m, "SKU0028", "Chair", 28 },
                    { 20, 7, new DateTime(2024, 11, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 110.80m, "SKU0029", "Bed", 4 },
                    { 21, 7, new DateTime(2024, 11, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 75.00m, "SKU0030", "Shelf", 9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
