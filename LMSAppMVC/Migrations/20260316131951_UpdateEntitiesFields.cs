using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSAppMVC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntitiesFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "DateModified",
                table: "Loans",
                newName: "ApprovedDate");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Authors",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Authors",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Librarians",
                keyColumn: "Id",
                keyValue: new Guid("a65c9e02-1f0b-4e57-b3d8-7b77b4a302be"),
                column: "LibrarianRegistrationCode",
                value: "7C312");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "ApprovedDate",
                table: "Loans",
                newName: "DateModified");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Loans",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Librarians",
                keyColumn: "Id",
                keyValue: new Guid("a65c9e02-1f0b-4e57-b3d8-7b77b4a302be"),
                column: "LibrarianRegistrationCode",
                value: "6D814");
        }
    }
}
