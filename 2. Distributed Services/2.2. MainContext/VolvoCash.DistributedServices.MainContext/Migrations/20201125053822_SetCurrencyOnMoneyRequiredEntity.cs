using Microsoft.EntityFrameworkCore.Migrations;

namespace VolvoCash.DistributedServices.MainContext.Migrations
{
    public partial class SetCurrencyOnMoneyRequiredEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batches_Currencies_Amount_CurrencyId",
                table: "Batches");

            migrationBuilder.DropForeignKey(
                name: "FK_Batches_Currencies_Balance_CurrencyId",
                table: "Batches");

            migrationBuilder.DropForeignKey(
                name: "FK_BatchMovements_Currencies_Amount_CurrencyId",
                table: "BatchMovements");

            migrationBuilder.DropForeignKey(
                name: "FK_CardBatches_Currencies_Balance_CurrencyId",
                table: "CardBatches");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Currencies_Balance_CurrencyId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Charges_Currencies_Amount_CurrencyId",
                table: "Charges");

            migrationBuilder.DropForeignKey(
                name: "FK_Liquidations_Currencies_Amount_CurrencyId",
                table: "Liquidations");

            migrationBuilder.DropForeignKey(
                name: "FK_Movements_Currencies_Amount_CurrencyId",
                table: "Movements");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Currencies_Amount_CurrencyId",
                table: "Transfers");

            migrationBuilder.AddForeignKey(
                name: "FK_Batches_Currencies_Amount_CurrencyId",
                table: "Batches",
                column: "Amount_CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Batches_Currencies_Balance_CurrencyId",
                table: "Batches",
                column: "Balance_CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BatchMovements_Currencies_Amount_CurrencyId",
                table: "BatchMovements",
                column: "Amount_CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CardBatches_Currencies_Balance_CurrencyId",
                table: "CardBatches",
                column: "Balance_CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Currencies_Balance_CurrencyId",
                table: "Cards",
                column: "Balance_CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Charges_Currencies_Amount_CurrencyId",
                table: "Charges",
                column: "Amount_CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Liquidations_Currencies_Amount_CurrencyId",
                table: "Liquidations",
                column: "Amount_CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_Currencies_Amount_CurrencyId",
                table: "Movements",
                column: "Amount_CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Currencies_Amount_CurrencyId",
                table: "Transfers",
                column: "Amount_CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batches_Currencies_Amount_CurrencyId",
                table: "Batches");

            migrationBuilder.DropForeignKey(
                name: "FK_Batches_Currencies_Balance_CurrencyId",
                table: "Batches");

            migrationBuilder.DropForeignKey(
                name: "FK_BatchMovements_Currencies_Amount_CurrencyId",
                table: "BatchMovements");

            migrationBuilder.DropForeignKey(
                name: "FK_CardBatches_Currencies_Balance_CurrencyId",
                table: "CardBatches");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Currencies_Balance_CurrencyId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Charges_Currencies_Amount_CurrencyId",
                table: "Charges");

            migrationBuilder.DropForeignKey(
                name: "FK_Liquidations_Currencies_Amount_CurrencyId",
                table: "Liquidations");

            migrationBuilder.DropForeignKey(
                name: "FK_Movements_Currencies_Amount_CurrencyId",
                table: "Movements");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Currencies_Amount_CurrencyId",
                table: "Transfers");

            migrationBuilder.AddForeignKey(
                name: "FK_Batches_Currencies_Amount_CurrencyId",
                table: "Batches",
                column: "Amount_CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Batches_Currencies_Balance_CurrencyId",
                table: "Batches",
                column: "Balance_CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BatchMovements_Currencies_Amount_CurrencyId",
                table: "BatchMovements",
                column: "Amount_CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CardBatches_Currencies_Balance_CurrencyId",
                table: "CardBatches",
                column: "Balance_CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Currencies_Balance_CurrencyId",
                table: "Cards",
                column: "Balance_CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Charges_Currencies_Amount_CurrencyId",
                table: "Charges",
                column: "Amount_CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Liquidations_Currencies_Amount_CurrencyId",
                table: "Liquidations",
                column: "Amount_CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_Currencies_Amount_CurrencyId",
                table: "Movements",
                column: "Amount_CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Currencies_Amount_CurrencyId",
                table: "Transfers",
                column: "Amount_CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
