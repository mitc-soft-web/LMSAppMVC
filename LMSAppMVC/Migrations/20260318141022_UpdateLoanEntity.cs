using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSAppMVC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLoanEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Librarians",
                keyColumn: "Id",
                keyValue: new Guid("a65c9e02-1f0b-4e57-b3d8-7b77b4a302be"),
                column: "LibrarianRegistrationCode",
                value: "5E097");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_BookId",
                table: "Loans",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Books_BookId",
                table: "Loans",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Books_BookId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_BookId",
                table: "Loans");

            migrationBuilder.UpdateData(
                table: "Librarians",
                keyColumn: "Id",
                keyValue: new Guid("a65c9e02-1f0b-4e57-b3d8-7b77b4a302be"),
                column: "LibrarianRegistrationCode",
                value: "65EBB");
        }
    }
}
