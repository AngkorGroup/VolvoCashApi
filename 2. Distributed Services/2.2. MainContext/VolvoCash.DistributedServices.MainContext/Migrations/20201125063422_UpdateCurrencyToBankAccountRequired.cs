using Microsoft.EntityFrameworkCore.Migrations;

namespace VolvoCash.DistributedServices.MainContext.Migrations
{
    public partial class UpdateCurrencyToBankAccountRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_Currencies_CurrencyId",
                table: "BankAccounts");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "BankAccounts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_Currencies_CurrencyId",
                table: "BankAccounts",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_Currencies_CurrencyId",
                table: "BankAccounts");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "BankAccounts",
                type: "NUMBER(10)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_Currencies_CurrencyId",
                table: "BankAccounts",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
