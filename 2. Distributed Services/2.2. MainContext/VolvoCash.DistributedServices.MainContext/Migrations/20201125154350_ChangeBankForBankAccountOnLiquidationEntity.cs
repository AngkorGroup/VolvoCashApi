using Microsoft.EntityFrameworkCore.Migrations;

namespace VolvoCash.DistributedServices.MainContext.Migrations
{
    public partial class ChangeBankForBankAccountOnLiquidationEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Liquidations_Banks_BankId",
                table: "Liquidations");

            migrationBuilder.DropIndex(
                name: "IX_Liquidations_BankId",
                table: "Liquidations");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "Liquidations");

            migrationBuilder.AddColumn<int>(
                name: "BankAccountId",
                table: "Liquidations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Liquidations_BankAccountId",
                table: "Liquidations",
                column: "BankAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Liquidations_BankAccounts_BankAccountId",
                table: "Liquidations",
                column: "BankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Liquidations_BankAccounts_BankAccountId",
                table: "Liquidations");

            migrationBuilder.DropIndex(
                name: "IX_Liquidations_BankAccountId",
                table: "Liquidations");

            migrationBuilder.DropColumn(
                name: "BankAccountId",
                table: "Liquidations");

            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "Liquidations",
                type: "NUMBER(10)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Liquidations_BankId",
                table: "Liquidations",
                column: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Liquidations_Banks_BankId",
                table: "Liquidations",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
