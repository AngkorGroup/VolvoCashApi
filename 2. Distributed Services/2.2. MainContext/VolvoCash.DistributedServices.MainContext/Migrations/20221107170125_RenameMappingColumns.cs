using Microsoft.EntityFrameworkCore.Migrations;

namespace VolvoCash.DistributedServices.MainContext.Migrations
{
    public partial class RenameMappingColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Filter",
                table: "Mappings");

            migrationBuilder.RenameColumn(
                name: "SenderLogicalId",
                table: "Mappings",
                newName: "SenderLogicalID");

            migrationBuilder.RenameColumn(
                name: "SenderComponentId",
                table: "Mappings",
                newName: "SenderComponentID");

            migrationBuilder.RenameColumn(
                name: "ReceiverLogicalId",
                table: "Mappings",
                newName: "ReceiverLogicalID");

            migrationBuilder.RenameColumn(
                name: "ReceiverComponentId",
                table: "Mappings",
                newName: "ReceiverComponentID");

            migrationBuilder.AddColumn<string>(
                name: "Filler",
                table: "Mappings",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Filler",
                table: "Mappings");

            migrationBuilder.RenameColumn(
                name: "SenderLogicalID",
                table: "Mappings",
                newName: "SenderLogicalId");

            migrationBuilder.RenameColumn(
                name: "SenderComponentID",
                table: "Mappings",
                newName: "SenderComponentId");

            migrationBuilder.RenameColumn(
                name: "ReceiverLogicalID",
                table: "Mappings",
                newName: "ReceiverLogicalId");

            migrationBuilder.RenameColumn(
                name: "ReceiverComponentID",
                table: "Mappings",
                newName: "ReceiverComponentId");

            migrationBuilder.AddColumn<string>(
                name: "Filter",
                table: "Mappings",
                type: "NVARCHAR2(200)",
                maxLength: 200,
                nullable: true);
        }
    }
}
