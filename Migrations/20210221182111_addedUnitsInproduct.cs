using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KKTraders.Migrations
{
    public partial class addedUnitsInproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_orderHeader_OrderHeaderId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Product_ServiceId",
                table: "OrderDetails");

            migrationBuilder.DropTable(
                name: "orderHeader");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_ServiceId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Product",
                newName: "Units");

            migrationBuilder.RenameColumn(
                name: "AverageQuantity",
                table: "Product",
                newName: "MinimumUnits");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_OrderDetails_OrderHeaderId",
                table: "OrderDetails",
                column: "OrderHeaderId",
                principalTable: "OrderDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Product_ProductId",
                table: "OrderDetails",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_OrderDetails_OrderHeaderId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Product_ProductId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "Units",
                table: "Product",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "MinimumUnits",
                table: "Product",
                newName: "AverageQuantity");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "OrderDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "orderHeader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderHeader", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ServiceId",
                table: "OrderDetails",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_orderHeader_OrderHeaderId",
                table: "OrderDetails",
                column: "OrderHeaderId",
                principalTable: "orderHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Product_ServiceId",
                table: "OrderDetails",
                column: "ServiceId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
