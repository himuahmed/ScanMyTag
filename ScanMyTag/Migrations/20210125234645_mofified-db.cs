using Microsoft.EntityFrameworkCore.Migrations;

namespace ScanMyTag.Migrations
{
    public partial class mofifieddb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactQr_AspNetUsers_UserId1",
                table: "ContactQr");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactQRModel_AspNetUsers_UserId1",
                table: "ContactQRModel");

            migrationBuilder.DropIndex(
                name: "IX_ContactQRModel_UserId1",
                table: "ContactQRModel");

            migrationBuilder.DropIndex(
                name: "IX_ContactQr_UserId1",
                table: "ContactQr");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ContactQRModel");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ContactQr");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ContactQRModel",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ContactQr",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ContactQRModel_UserId",
                table: "ContactQRModel",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactQr_UserId",
                table: "ContactQr",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactQr_AspNetUsers_UserId",
                table: "ContactQr",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactQRModel_AspNetUsers_UserId",
                table: "ContactQRModel",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactQr_AspNetUsers_UserId",
                table: "ContactQr");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactQRModel_AspNetUsers_UserId",
                table: "ContactQRModel");

            migrationBuilder.DropIndex(
                name: "IX_ContactQRModel_UserId",
                table: "ContactQRModel");

            migrationBuilder.DropIndex(
                name: "IX_ContactQr_UserId",
                table: "ContactQr");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ContactQRModel",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "ContactQRModel",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ContactQr",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "ContactQr",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactQRModel_UserId1",
                table: "ContactQRModel",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_ContactQr_UserId1",
                table: "ContactQr",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactQr_AspNetUsers_UserId1",
                table: "ContactQr",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactQRModel_AspNetUsers_UserId1",
                table: "ContactQRModel",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
