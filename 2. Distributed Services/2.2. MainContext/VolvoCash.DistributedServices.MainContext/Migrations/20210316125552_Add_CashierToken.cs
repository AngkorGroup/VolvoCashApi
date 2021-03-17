using Microsoft.EntityFrameworkCore.Migrations;

namespace VolvoCash.DistributedServices.MainContext.Migrations
{
    public partial class Add_CashierToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResetPasswordTokens_Admins_AdminId",
                table: "ResetPasswordTokens");

            migrationBuilder.AlterColumn<int>(
                name: "AdminId",
                table: "ResetPasswordTokens",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");

            migrationBuilder.AddColumn<int>(
                name: "CashierId",
                table: "ResetPasswordTokens",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResetPasswordTokens_CashierId",
                table: "ResetPasswordTokens",
                column: "CashierId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResetPasswordTokens_Admins_AdminId",
                table: "ResetPasswordTokens",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ResetPasswordTokens_Cashiers_CashierId",
                table: "ResetPasswordTokens",
                column: "CashierId",
                principalTable: "Cashiers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResetPasswordTokens_Admins_AdminId",
                table: "ResetPasswordTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_ResetPasswordTokens_Cashiers_CashierId",
                table: "ResetPasswordTokens");

            migrationBuilder.DropIndex(
                name: "IX_ResetPasswordTokens_CashierId",
                table: "ResetPasswordTokens");

            migrationBuilder.DropColumn(
                name: "CashierId",
                table: "ResetPasswordTokens");

            migrationBuilder.AlterColumn<int>(
                name: "AdminId",
                table: "ResetPasswordTokens",
                type: "NUMBER(10)",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ResetPasswordTokens_Admins_AdminId",
                table: "ResetPasswordTokens",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
