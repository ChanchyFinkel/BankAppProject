#nullable disable

namespace Account.Data.Migrations;

public partial class changeTypeBalanceProp : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<int>(
            name: "Balance",
            table: "Account",
            type: "int",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "decimal(18,2)");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            name: "Balance",
            table: "Account",
            type: "decimal(18,2)",
            nullable: false,
            oldClrType: typeof(int),
            oldType: "int");
    }
}
