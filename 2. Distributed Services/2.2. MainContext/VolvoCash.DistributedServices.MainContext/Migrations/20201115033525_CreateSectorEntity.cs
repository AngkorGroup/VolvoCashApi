using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Oracle.EntityFrameworkCore.Metadata;

namespace VolvoCash.DistributedServices.MainContext.Migrations
{
    public partial class CreateSectorEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SectorId",
                table: "Clients",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sector",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sector", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_SectorId",
                table: "Clients",
                column: "SectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Sector_SectorId",
                table: "Clients",
                column: "SectorId",
                principalTable: "Sector",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Sector_SectorId",
                table: "Clients");

            migrationBuilder.DropTable(
                name: "Sector");

            migrationBuilder.DropIndex(
                name: "IX_Clients_SectorId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "SectorId",
                table: "Clients");
        }
    }
}
