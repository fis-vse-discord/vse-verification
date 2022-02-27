using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VseVerification.Migrations
{
    public partial class MemberVerifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "verifications",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    discord_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    azure_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_verifications", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_verifications_azure_id",
                table: "verifications",
                column: "azure_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_verifications_discord_id",
                table: "verifications",
                column: "discord_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "verifications");
        }
    }
}
