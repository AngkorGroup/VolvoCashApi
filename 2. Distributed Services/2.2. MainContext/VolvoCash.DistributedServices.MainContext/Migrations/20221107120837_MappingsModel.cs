using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Oracle.EntityFrameworkCore.Metadata;

namespace VolvoCash.DistributedServices.MainContext.Migrations
{
    public partial class MappingsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mappings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedAt = table.Column<DateTime>(nullable: true),
                    MappingNumber = table.Column<string>(maxLength: 200, nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Type = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    Company = table.Column<string>(maxLength: 200, nullable: true),
                    Feeder = table.Column<string>(maxLength: 200, nullable: true),
                    File = table.Column<string>(maxLength: 200, nullable: true),
                    Username = table.Column<string>(maxLength: 200, nullable: true),
                    Password = table.Column<string>(maxLength: 200, nullable: true),
                    Date = table.Column<string>(maxLength: 200, nullable: true),
                    Filter = table.Column<string>(maxLength: 200, nullable: true),
                    Version = table.Column<string>(maxLength: 200, nullable: true),
                    ReceiverLogicalId = table.Column<string>(maxLength: 200, nullable: true),
                    ReceiverComponentId = table.Column<string>(maxLength: 200, nullable: true),
                    SenderLogicalId = table.Column<string>(maxLength: 200, nullable: true),
                    SenderComponentId = table.Column<string>(maxLength: 200, nullable: true),
                    ArchiveAt = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mappings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MappingHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedAt = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(maxLength: 200, nullable: true),
                    RecordType = table.Column<string>(maxLength: 200, nullable: true),
                    Company = table.Column<string>(maxLength: 200, nullable: true),
                    DocumentNumber = table.Column<string>(maxLength: 200, nullable: true),
                    Reference = table.Column<string>(maxLength: 200, nullable: true),
                    Control = table.Column<string>(maxLength: 200, nullable: true),
                    DocumentType = table.Column<string>(maxLength: 200, nullable: true),
                    DocumentDate = table.Column<string>(maxLength: 200, nullable: true),
                    PostDate = table.Column<string>(maxLength: 200, nullable: true),
                    Currency = table.Column<string>(maxLength: 200, nullable: true),
                    ExchangeRate = table.Column<string>(maxLength: 200, nullable: true),
                    DocumentHeader = table.Column<string>(maxLength: 200, nullable: true),
                    TranslationDate = table.Column<string>(maxLength: 200, nullable: true),
                    IntercompanyNumber = table.Column<string>(maxLength: 200, nullable: true),
                    TradingPartner = table.Column<string>(maxLength: 200, nullable: true),
                    ExchangeRateType = table.Column<string>(maxLength: 200, nullable: true),
                    PostingPeriod = table.Column<string>(maxLength: 200, nullable: true),
                    ExchangeRateToFactor = table.Column<string>(maxLength: 200, nullable: true),
                    ExchangeRateFromFactor = table.Column<string>(maxLength: 200, nullable: true),
                    ReversalReason = table.Column<string>(maxLength: 200, nullable: true),
                    ReversalDate = table.Column<string>(maxLength: 200, nullable: true),
                    MappingId = table.Column<int>(nullable: false),
                    ArchiveAt = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MappingHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MappingHeaders_Mappings_MappingId",
                        column: x => x.MappingId,
                        principalTable: "Mappings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MappingDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedAt = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(maxLength: 200, nullable: true),
                    DocumentType = table.Column<string>(maxLength: 200, nullable: true),
                    Line = table.Column<string>(maxLength: 200, nullable: true),
                    RecordType = table.Column<string>(maxLength: 200, nullable: true),
                    Company = table.Column<string>(maxLength: 200, nullable: true),
                    Reference = table.Column<string>(maxLength: 200, nullable: true),
                    PostKey = table.Column<string>(maxLength: 200, nullable: true),
                    Account = table.Column<string>(maxLength: 200, nullable: true),
                    Sign = table.Column<string>(maxLength: 200, nullable: true),
                    TaxCode = table.Column<string>(maxLength: 200, nullable: true),
                    TaxAmount = table.Column<string>(maxLength: 200, nullable: true),
                    CostCenter = table.Column<string>(maxLength: 200, nullable: true),
                    ProfitCenter = table.Column<string>(maxLength: 200, nullable: true),
                    TradePartner = table.Column<string>(maxLength: 200, nullable: true),
                    DocText = table.Column<string>(maxLength: 200, nullable: true),
                    MoreInfo = table.Column<string>(maxLength: 200, nullable: true),
                    BusinessArea = table.Column<string>(maxLength: 200, nullable: true),
                    Market = table.Column<string>(maxLength: 200, nullable: true),
                    Customer = table.Column<string>(maxLength: 200, nullable: true),
                    ProductModel = table.Column<string>(maxLength: 200, nullable: true),
                    LineType = table.Column<string>(maxLength: 200, nullable: true),
                    Classification = table.Column<string>(maxLength: 200, nullable: true),
                    ArchiveAt = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    MappingHeaderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MappingDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MappingDetails_MappingHeaders_MappingHeaderId",
                        column: x => x.MappingHeaderId,
                        principalTable: "MappingHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MappingDetails_MappingHeaderId",
                table: "MappingDetails",
                column: "MappingHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_MappingHeaders_MappingId",
                table: "MappingHeaders",
                column: "MappingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MappingDetails");

            migrationBuilder.DropTable(
                name: "MappingHeaders");

            migrationBuilder.DropTable(
                name: "Mappings");
        }
    }
}
