using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Account.Data.Migrations
{
    public partial class addSaltToCustomerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Customer",
                type: "nvarchar(25)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Customer");
        }
    }
}
