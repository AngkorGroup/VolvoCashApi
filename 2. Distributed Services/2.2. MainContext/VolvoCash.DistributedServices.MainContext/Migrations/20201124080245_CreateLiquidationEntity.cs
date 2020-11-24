using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Oracle.EntityFrameworkCore.Metadata;

namespace VolvoCash.DistributedServices.MainContext.Migrations
{
    public partial class CreateLiquidationEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LiquidationId",
                table: "Charges",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Liquidations",
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
                    DealerId = table.Column<int>(nullable: false),
                    LiquidationStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Liquidations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Liquidations_Dealers_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Liquidations_Currencies_Amount_CurrencyId",
                        column: x => x.Amount_CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Charges_LiquidationId",
                table: "Charges",
                column: "LiquidationId");

            migrationBuilder.CreateIndex(
                name: "IX_Liquidations_DealerId",
                table: "Liquidations",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_Liquidations_Amount_CurrencyId",
                table: "Liquidations",
                column: "Amount_CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Charges_Liquidations_LiquidationId",
                table: "Charges",
                column: "LiquidationId",
                principalTable: "Liquidations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Charges_Liquidations_LiquidationId",
                table: "Charges");

            migrationBuilder.DropTable(
                name: "Liquidations");

            migrationBuilder.DropIndex(
                name: "IX_Charges_LiquidationId",
                table: "Charges");

            migrationBuilder.DropColumn(
                name: "LiquidationId",
                table: "Charges");
        }
    }
}
