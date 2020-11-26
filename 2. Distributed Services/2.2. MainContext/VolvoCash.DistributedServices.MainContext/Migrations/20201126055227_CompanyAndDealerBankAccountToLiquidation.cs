using Microsoft.EntityFrameworkCore.Migrations;

namespace VolvoCash.DistributedServices.MainContext.Migrations
{
    public partial class CompanyAndDealerBankAccountToLiquidation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyBankAccount",
                table: "Liquidations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DealerBankAccount",
                table: "Liquidations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyBankAccount",
                table: "Liquidations");

            migrationBuilder.DropColumn(
                name: "DealerBankAccount",
                table: "Liquidations");
        }
    }
}
