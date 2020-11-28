using Microsoft.EntityFrameworkCore.Migrations;

namespace VolvoCash.DistributedServices.MainContext.Migrations
{
    public partial class UpdateBankAccountTypeToBankAccountRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_BankAccountTypes_BankAccountTypeId",
                table: "BankAccounts");

            migrationBuilder.AlterColumn<int>(
                name: "BankAccountTypeId",
                table: "BankAccounts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_BankAccountTypes_BankAccountTypeId",
                table: "BankAccounts",
                column: "BankAccountTypeId",
                principalTable: "BankAccountTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_BankAccountTypes_BankAccountTypeId",
                table: "BankAccounts");

            migrationBuilder.AlterColumn<int>(
                name: "BankAccountTypeId",
                table: "BankAccounts",
                type: "NUMBER(10)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_BankAccountTypes_BankAccountTypeId",
                table: "BankAccounts",
                column: "BankAccountTypeId",
                principalTable: "BankAccountTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
