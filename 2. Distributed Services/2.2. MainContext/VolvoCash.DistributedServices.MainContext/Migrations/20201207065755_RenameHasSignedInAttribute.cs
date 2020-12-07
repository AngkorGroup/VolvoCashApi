using Microsoft.EntityFrameworkCore.Migrations;

namespace VolvoCash.DistributedServices.MainContext.Migrations
{
    public partial class RenameHasSignedInAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasSignIn",
                table: "Contacts");

            migrationBuilder.AddColumn<bool>(
                name: "HasSignedIn",
                table: "Contacts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasSignedIn",
                table: "Contacts");

            migrationBuilder.AddColumn<bool>(
                name: "HasSignIn",
                table: "Contacts",
                type: "NUMBER(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
