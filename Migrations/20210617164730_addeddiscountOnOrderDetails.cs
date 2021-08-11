using Microsoft.EntityFrameworkCore.Migrations;

namespace KKTraders.Migrations
{
    public partial class addeddiscountOnOrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "discount",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "discount",
                table: "OrderDetails");
        }
    }
}
