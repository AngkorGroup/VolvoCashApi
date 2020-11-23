using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Oracle.EntityFrameworkCore.Metadata;

namespace VolvoCash.DistributedServices.MainContext.Migrations
{
    public partial class BusinessAreasRechargeTypeBanksModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Sector_SectorId",
                table: "Clients");         

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sector",
                table: "Sector");

            migrationBuilder.DropColumn(
                name: "TPContractType",
                table: "Batches");

            migrationBuilder.RenameTable(
                name: "Sector",
                newName: "Sectors");

            migrationBuilder.AddColumn<int>(
                name: "RechargeTypeId",
                table: "Batches",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ArchiveAt",
                table: "Sectors",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Sectors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TPCode",
                table: "Sectors",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sectors",
                table: "Sectors",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Abbreviation = table.Column<string>(nullable: true),
                    TPCode = table.Column<string>(nullable: true),
                    ArchiveAt = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessAreas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    TPCode = table.Column<string>(nullable: true),
                    ArchiveAt = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RechargeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    TPCode = table.Column<string>(nullable: true),
                    ArchiveAt = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RechargeTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Batches_RechargeTypeId",
                table: "Batches",
                column: "RechargeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Batches_RechargeTypes_RechargeTypeId",
                table: "Batches",
                column: "RechargeTypeId",
                principalTable: "RechargeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Sectors_SectorId",
                table: "Clients",
                column: "SectorId",
                principalTable: "Sectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batches_RechargeTypes_RechargeTypeId",
                table: "Batches");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Sectors_SectorId",
                table: "Clients");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "BusinessAreas");

            migrationBuilder.DropTable(
                name: "RechargeTypes");

            migrationBuilder.DropIndex(
                name: "IX_Batches_RechargeTypeId",
                table: "Batches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sectors",
                table: "Sectors");

            migrationBuilder.DropColumn(
                name: "RechargeTypeId",
                table: "Batches");

            migrationBuilder.DropColumn(
                name: "ArchiveAt",
                table: "Sectors");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Sectors");

            migrationBuilder.DropColumn(
                name: "TPCode",
                table: "Sectors");

            migrationBuilder.RenameTable(
                name: "Sectors",
                newName: "Sector");

            migrationBuilder.AddColumn<int>(
                name: "TPContractType",
                table: "Batches",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sector",
                table: "Sector",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Sector_SectorId",
                table: "Clients",
                column: "SectorId",
                principalTable: "Sector",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
