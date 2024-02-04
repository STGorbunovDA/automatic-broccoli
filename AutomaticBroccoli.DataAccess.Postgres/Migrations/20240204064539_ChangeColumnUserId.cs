using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomaticBroccoli.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpenLoops_Users_UsertId",
                table: "OpenLoops");

            migrationBuilder.RenameColumn(
                name: "UsertId",
                table: "OpenLoops",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_OpenLoops_UsertId",
                table: "OpenLoops",
                newName: "IX_OpenLoops_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OpenLoops_Users_UserId",
                table: "OpenLoops",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpenLoops_Users_UserId",
                table: "OpenLoops");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "OpenLoops",
                newName: "UsertId");

            migrationBuilder.RenameIndex(
                name: "IX_OpenLoops_UserId",
                table: "OpenLoops",
                newName: "IX_OpenLoops_UsertId");

            migrationBuilder.AddForeignKey(
                name: "FK_OpenLoops_Users_UsertId",
                table: "OpenLoops",
                column: "UsertId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
