using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagement.Domain.Infraestructure.Migrations
{
    public partial class addLastName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "lastname",
                table: "users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lastname",
                table: "users");
        }
    }
}
