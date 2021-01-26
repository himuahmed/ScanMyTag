using Microsoft.EntityFrameworkCore.Migrations;

namespace ScanMyTag.Migrations
{
    public partial class qruserrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ContactQr",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "ContactQr",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContactQRModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QrCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactQRModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactQRModel_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactQr_UserId1",
                table: "ContactQr",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_ContactQRModel_UserId1",
                table: "ContactQRModel",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactQr_AspNetUsers_UserId1",
                table: "ContactQr",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactQr_AspNetUsers_UserId1",
                table: "ContactQr");

            migrationBuilder.DropTable(
                name: "ContactQRModel");

            migrationBuilder.DropIndex(
                name: "IX_ContactQr_UserId1",
                table: "ContactQr");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ContactQr");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ContactQr");
        }
    }
}
