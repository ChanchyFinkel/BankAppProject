#nullable disable

namespace Transaction.Data.Migrations;
public partial class createTransactionEntities : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Transaction",
            columns: table => new
            {
                ID = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                FromAccount = table.Column<int>(type: "int", nullable: false),
                ToAccount = table.Column<int>(type: "int", nullable: false),
                Ammount = table.Column<int>(type: "int", nullable: false),
                Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                FailureReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Transaction", x => x.ID);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Transaction");
    }
}
