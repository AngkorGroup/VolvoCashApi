using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VolvoCash.DistributedServices.MainContext.Migrations
{
    public partial class AddPaymentDateAndVoucherToLiquidation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "Liquidations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Voucher",
                table: "Liquidations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "Liquidations");

            migrationBuilder.DropColumn(
                name: "Voucher",
                table: "Liquidations");
        }
    }
}
