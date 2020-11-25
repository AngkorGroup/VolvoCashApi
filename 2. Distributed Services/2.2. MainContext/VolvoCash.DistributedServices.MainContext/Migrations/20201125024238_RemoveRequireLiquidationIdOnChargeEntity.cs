using Microsoft.EntityFrameworkCore.Migrations;

namespace VolvoCash.DistributedServices.MainContext.Migrations
{
    public partial class RemoveRequireLiquidationIdOnChargeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LiquidationId",
                table: "Charges",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LiquidationId",
                table: "Charges",
                type: "NUMBER(10)",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
