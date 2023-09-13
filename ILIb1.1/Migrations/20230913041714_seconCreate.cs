using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ILIb1._1.Migrations
{
    /// <inheritdoc />
    public partial class seconCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AppUserID",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    AppUserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fines = table.Column<int>(type: "int", nullable: false),
                    BorrowedBookCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.AppUserID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AppUserID",
                table: "Books",
                column: "AppUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_AppUserID",
                table: "Books",
                column: "AppUserID",
                principalTable: "Users",
                principalColumn: "AppUserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_AppUserID",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Books_AppUserID",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "AppUserID",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
