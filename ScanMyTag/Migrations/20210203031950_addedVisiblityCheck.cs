using Microsoft.EntityFrameworkCore.Migrations;

namespace ScanMyTag.Migrations
{
    public partial class addedVisiblityCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ContactQRModel",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(16)",
                oldMaxLength: 16);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "ContactQRModel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "ContactQr",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "ContactQRModel");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "ContactQr");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ContactQRModel",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
