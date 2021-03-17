using Microsoft.EntityFrameworkCore.Migrations;

namespace VolvoCash.DistributedServices.MainContext.Migrations
{
    public partial class AddCashierIdToAdminDTO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CashierId",
                table: "Admins",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admins_CashierId",
                table: "Admins",
                column: "CashierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Cashiers_CashierId",
                table: "Admins",
                column: "CashierId",
                principalTable: "Cashiers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Cashiers_CashierId",
                table: "Admins");

            migrationBuilder.DropIndex(
                name: "IX_Admins_CashierId",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "CashierId",
                table: "Admins");
        }
    }
}
