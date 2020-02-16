using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Warehousing.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "productHistoryLines",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifiedAt = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    DeltaQuantity = table.Column<int>(nullable: false),
                    Type = table.Column<string>(maxLength: 20, nullable: false),
                    OccurredOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productHistoryLines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifiedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    ArticleNumber = table.Column<string>(maxLength: 20, nullable: false),
                    Type = table.Column<string>(maxLength: 20, nullable: false),
                    CustomTariffNumber = table.Column<string>(maxLength: 20, nullable: false),
                    Quantity = table.Column<int>(maxLength: 5, nullable: false),
                    Unit = table.Column<string>(maxLength: 20, nullable: false),
                    NetUnitPrice = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    NetValue = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Vat = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    VatSum = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    GrossUnitPrice = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    GrossValue = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Notes = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productHistoryLines",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "products",
                schema: "dbo");
        }
    }
}
