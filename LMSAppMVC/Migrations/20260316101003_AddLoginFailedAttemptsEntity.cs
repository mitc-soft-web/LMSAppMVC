using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSAppMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddLoginFailedAttemptsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FailedLoginAttempts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IpAddress = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AttemptCount = table.Column<int>(type: "int", nullable: false),
                    LastAttemptTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    BlockedUntil = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    RemainingSeconds = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FailedLoginAttempts", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Librarians",
                keyColumn: "Id",
                keyValue: new Guid("a65c9e02-1f0b-4e57-b3d8-7b77b4a302be"),
                column: "LibrarianRegistrationCode",
                value: "6D814");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FailedLoginAttempts");

            migrationBuilder.UpdateData(
                table: "Librarians",
                keyColumn: "Id",
                keyValue: new Guid("a65c9e02-1f0b-4e57-b3d8-7b77b4a302be"),
                column: "LibrarianRegistrationCode",
                value: "CE602");
        }
    }
}
