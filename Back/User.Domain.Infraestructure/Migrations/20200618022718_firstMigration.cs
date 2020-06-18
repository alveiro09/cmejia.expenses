using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagement.Domain.Infraestructure.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    firstname = table.Column<string>(nullable: false),
                    secondname = table.Column<string>(nullable: true),
                    identitynumber = table.Column<string>(nullable: false),
                    identitydocument = table.Column<int>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    secondemail = table.Column<string>(nullable: true),
                    username = table.Column<string>(nullable: false),
                    password = table.Column<string>(nullable: false),
                    age = table.Column<int>(nullable: false),
                    created = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
