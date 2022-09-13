#nullable disable

namespace Account.Data.Migrations;
public partial class addOperationsHistoryTable : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "OperationsHistory",
            columns: table => new
            {
                ID = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                AccountID = table.Column<int>(type: "int", nullable: false),
                TransactionID = table.Column<int>(type: "int", nullable: false),
                Debit = table.Column<bool>(type: "bit", nullable: false),
                TransactionAmount = table.Column<int>(type: "int", nullable: false),
                Balance = table.Column<int>(type: "int", nullable: false),
                OperationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OperationsHistory", x => x.ID);
                table.ForeignKey(
                    name: "FK_OperationsHistory_Account_AccountID",
                    column: x => x.AccountID,
                    principalTable: "Account",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_OperationsHistory_AccountID",
            table: "OperationsHistory",
            column: "AccountID");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "OperationsHistory");
    }
}
