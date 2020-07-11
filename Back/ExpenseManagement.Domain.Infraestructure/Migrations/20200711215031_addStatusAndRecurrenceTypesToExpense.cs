using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseManagement.Domain.Infraestructure.Migrations
{
    public partial class addStatusAndRecurrenceTypesToExpense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idexpenserecurrencetype",
                table: "expenses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idexpensestatus",
                table: "expenses",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "idexpenserecurrencetype",
                table: "expenses");

            migrationBuilder.DropColumn(
                name: "idexpensestatus",
                table: "expenses");
        }
    }
}
