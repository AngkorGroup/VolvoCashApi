using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Oracle.EntityFrameworkCore.Metadata;

namespace VolvoCash.DistributedServices.MainContext.Migrations
{
    public partial class CreateRefundEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RefundId",
                table: "Liquidations",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Refunds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedAt = table.Column<DateTime>(nullable: true),
                    Amount_Value = table.Column<double>(nullable: true),
                    Amount_CurrencyId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    RefundStatus = table.Column<int>(nullable: false),
                    LiquidationsCount = table.Column<int>(nullable: false),
                    BankAccountId = table.Column<int>(nullable: false),
                    CompanyBankAccount = table.Column<string>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: true),
                    Voucher = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refunds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Refunds_BankAccounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Refunds_Currencies_Amount_CurrencyId",
                        column: x => x.Amount_CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Liquidations_RefundId",
                table: "Liquidations",
                column: "RefundId");

            migrationBuilder.CreateIndex(
                name: "IX_Refunds_BankAccountId",
                table: "Refunds",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Refunds_Amount_CurrencyId",
                table: "Refunds",
                column: "Amount_CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Liquidations_Refunds_RefundId",
                table: "Liquidations",
                column: "RefundId",
                principalTable: "Refunds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Liquidations_Refunds_RefundId",
                table: "Liquidations");

            migrationBuilder.DropTable(
                name: "Refunds");

            migrationBuilder.DropIndex(
                name: "IX_Liquidations_RefundId",
                table: "Liquidations");

            migrationBuilder.DropColumn(
                name: "RefundId",
                table: "Liquidations");
        }
    }
}
