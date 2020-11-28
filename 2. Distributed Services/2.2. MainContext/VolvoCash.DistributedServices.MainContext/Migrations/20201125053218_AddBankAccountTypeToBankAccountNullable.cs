using Microsoft.EntityFrameworkCore.Migrations;

namespace VolvoCash.DistributedServices.MainContext.Migrations
{
    public partial class AddBankAccountTypeToBankAccountNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BankAccountTypeId",
                table: "BankAccounts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_BankAccountTypeId",
                table: "BankAccounts",
                column: "BankAccountTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_BankAccountTypes_BankAccountTypeId",
                table: "BankAccounts",
                column: "BankAccountTypeId",
                principalTable: "BankAccountTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_BankAccountTypes_BankAccountTypeId",
                table: "BankAccounts");

            migrationBuilder.DropIndex(
                name: "IX_BankAccounts_BankAccountTypeId",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "BankAccountTypeId",
                table: "BankAccounts");
        }
    }
}
