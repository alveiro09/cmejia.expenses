using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagement.Domain.Infraestructure.Migrations
{
    public partial class changeTypeOfIdentityDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "identitynumber",
                table: "users",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "identitynumber",
                table: "users",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
