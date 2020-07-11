using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseManagement.Domain.Infraestructure.Migrations
{
    public partial class changedatepaidout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DatePaidOut",
                table: "expenses",
                newName: "datepaidout");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "datepaidout",
                table: "expenses",
                newName: "DatePaidOut");
        }
    }
}
