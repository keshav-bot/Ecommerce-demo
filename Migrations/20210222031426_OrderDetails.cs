using Microsoft.EntityFrameworkCore.Migrations;

namespace KKTraders.Migrations
{
    public partial class OrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_OrderDetails_OrderHeaderId",
                table: "OrderDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_orderHeader_OrderHeaderId",
                table: "OrderDetails",
                column: "OrderHeaderId",
                principalTable: "orderHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_orderHeader_OrderHeaderId",
                table: "OrderDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_OrderDetails_OrderHeaderId",
                table: "OrderDetails",
                column: "OrderHeaderId",
                principalTable: "OrderDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
