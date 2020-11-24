using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Oracle.EntityFrameworkCore.Metadata;

namespace VolvoCash.DistributedServices.MainContext.Migrations
{
    public partial class CreateDocumentTypeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "Contacts");

            migrationBuilder.AddColumn<int>(
                name: "DocumentTypeId",
                table: "Contacts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
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
                    Abbreviation = table.Column<string>(nullable: true),
                    SUNATCode = table.Column<string>(nullable: true),
                    ArchiveAt = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_DocumentTypeId",
                table: "Contacts",
                column: "DocumentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_DocumentTypes_DocumentTypeId",
                table: "Contacts",
                column: "DocumentTypeId",
                principalTable: "DocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_DocumentTypes_DocumentTypeId",
                table: "Contacts");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_DocumentTypeId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "DocumentTypeId",
                table: "Contacts");

            migrationBuilder.AddColumn<int>(
                name: "DocumentType",
                table: "Contacts",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);
        }
    }
}
