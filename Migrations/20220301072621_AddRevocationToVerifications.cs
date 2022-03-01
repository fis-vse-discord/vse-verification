using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VseVerification.Migrations
{
    public partial class AddRevocationToVerifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_revoked",
                table: "verifications",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_revoked",
                table: "verifications");
        }
    }
}
