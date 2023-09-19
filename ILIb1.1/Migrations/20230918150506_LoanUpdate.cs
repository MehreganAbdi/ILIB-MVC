using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ILIb1._1.Migrations
{
    /// <inheritdoc />
    public partial class LoanUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Loans",
                newName: "UserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "LoanDate",
                table: "Loans",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoanId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_LoanId",
                table: "Books",
                column: "LoanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Loans_LoanId",
                table: "Books",
                column: "LoanId",
                principalTable: "Loans",
                principalColumn: "LoanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Loans_LoanId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_LoanId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "LoanDate",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "LoanId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Loans",
                newName: "BookId");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Loans",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
