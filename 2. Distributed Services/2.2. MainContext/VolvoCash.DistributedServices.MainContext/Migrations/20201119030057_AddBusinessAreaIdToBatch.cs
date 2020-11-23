using Microsoft.EntityFrameworkCore.Migrations;

namespace VolvoCash.DistributedServices.MainContext.Migrations
{
    public partial class AddBusinessAreaIdToBatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessCode",
                table: "Batches");

            migrationBuilder.DropColumn(
                name: "BusinessDescription",
                table: "Batches");

            migrationBuilder.AddColumn<int>(
                name: "BusinessAreaId",
                table: "Batches",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Batches_BusinessAreaId",
                table: "Batches",
                column: "BusinessAreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Batches_BusinessAreas_BusinessAreaId",
                table: "Batches",
                column: "BusinessAreaId",
                principalTable: "BusinessAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batches_BusinessAreas_BusinessAreaId",
                table: "Batches");

            migrationBuilder.DropIndex(
                name: "IX_Batches_BusinessAreaId",
                table: "Batches");

            migrationBuilder.DropColumn(
                name: "BusinessAreaId",
                table: "Batches");

            migrationBuilder.AddColumn<string>(
                name: "BusinessCode",
                table: "Batches",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessDescription",
                table: "Batches",
                type: "NVARCHAR2(2000)",
                nullable: true);
        }
    }
}
