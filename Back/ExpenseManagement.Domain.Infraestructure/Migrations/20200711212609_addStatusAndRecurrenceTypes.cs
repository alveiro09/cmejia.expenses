using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseManagement.Domain.Infraestructure.Migrations
{
    public partial class addStatusAndRecurrenceTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_expenses_expensesType_IdExpenseType",
                table: "expenses");

            migrationBuilder.DropIndex(
                name: "IX_expenses_IdExpenseType",
                table: "expenses");

            migrationBuilder.RenameColumn(
                name: "IdExpenseType",
                table: "expenses",
                newName: "idexpensetype");

            migrationBuilder.CreateTable(
                name: "expensesRecurrenceType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expensesRecurrenceType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "expensesStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expensesStatus", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "expensesRecurrenceType");

            migrationBuilder.DropTable(
                name: "expensesStatus");

            migrationBuilder.RenameColumn(
                name: "idexpensetype",
                table: "expenses",
                newName: "IdExpenseType");

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
    }
}
