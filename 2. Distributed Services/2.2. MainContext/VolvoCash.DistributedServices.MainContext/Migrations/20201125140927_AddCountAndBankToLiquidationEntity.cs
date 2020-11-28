using Microsoft.EntityFrameworkCore.Migrations;

namespace VolvoCash.DistributedServices.MainContext.Migrations
{
    public partial class AddCountAndBankToLiquidationEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "Liquidations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChargesCount",
                table: "Liquidations",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "ChargesCount",
                table: "Liquidations");
        }
    }
}
