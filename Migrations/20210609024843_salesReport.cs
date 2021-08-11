using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KKTraders.Migrations
{
    public partial class salesReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalesReportId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SalesReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    DateOfSale = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NameOfCustomer = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Quantity = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesReport", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "productInvoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    SalesReportId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_productInvoice_SalesReport_SalesReportId",
                        column: x => x.SalesReportId,
                        principalTable: "SalesReport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_SalesReportId",
                table: "Product",
                column: "SalesReportId");

            migrationBuilder.CreateIndex(
                name: "IX_productInvoice_SalesReportId",
                table: "productInvoice",
                column: "SalesReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_SalesReport_SalesReportId",
                table: "Product",
                column: "SalesReportId",
                principalTable: "SalesReport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_SalesReport_SalesReportId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "productInvoice");

            migrationBuilder.DropTable(
                name: "SalesReport");

            migrationBuilder.DropIndex(
                name: "IX_Product_SalesReportId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SalesReportId",
                table: "Product");
        }
    }
}
