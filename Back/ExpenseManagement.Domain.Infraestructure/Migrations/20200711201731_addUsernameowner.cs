using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseManagement.Domain.Infraestructure.Migrations
{
    public partial class addUsernameowner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "usernameowner",
                table: "expenses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "usernameowner",
                table: "expenses");
        }
    }
}
