using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VolvoCash.DistributedServices.MainContext.Migrations
{
    public partial class AddCashierFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ArchiveAt",
                table: "Cashiers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Imei",
                table: "Cashiers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Cashiers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArchiveAt",
                table: "Cashiers");

            migrationBuilder.DropColumn(
                name: "Imei",
                table: "Cashiers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Cashiers");
        }
    }
}
