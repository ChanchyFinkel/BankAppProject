using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Account.Data.Migrations
{
    public partial class addPKToEmailVerifictionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailVerification",
                table: "EmailVerification");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "EmailVerification");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailVerification",
                table: "EmailVerification",
                column: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailVerification",
                table: "EmailVerification");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "EmailVerification",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailVerification",
                table: "EmailVerification",
                column: "ID");
        }
    }
}
