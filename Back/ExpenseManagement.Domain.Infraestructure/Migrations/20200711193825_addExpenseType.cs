using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseManagement.Domain.Infraestructure.Migrations
{
    public partial class addExpenseType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdExpenseType",
                table: "expenses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "expensesType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expensesType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_expenses_IdExpenseType",
                table: "expenses",
                column: "IdExpenseType");

            migrationBuilder.AddForeignKey(
                name: "FK_expenses_expensesType_IdExpenseType",
                table: "expenses",
                column: "IdExpenseType",
                principalTable: "expensesType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_expenses_expensesType_IdExpenseType",
                table: "expenses");

            migrationBuilder.DropTable(
                name: "expensesType");

            migrationBuilder.DropIndex(
                name: "IX_expenses_IdExpenseType",
                table: "expenses");

            migrationBuilder.DropColumn(
                name: "IdExpenseType",
                table: "expenses");
        }
    }
}
