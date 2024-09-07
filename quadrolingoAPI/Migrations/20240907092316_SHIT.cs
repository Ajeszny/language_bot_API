using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quadrolingoAPI.Migrations
{
    /// <inheritdoc />
    public partial class SHIT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Languages_BASE_LANGLANG_CODE",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Languages_STUDY_LANGLANG_CODE",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_BASE_LANGLANG_CODE",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_STUDY_LANGLANG_CODE",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BASE_LANGLANG_CODE",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "STUDY_LANGLANG_CODE",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "BASE_LANG",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "STUDY_LANG",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BASE_LANG",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "STUDY_LANG",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "BASE_LANGLANG_CODE",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STUDY_LANGLANG_CODE",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_BASE_LANGLANG_CODE",
                table: "Users",
                column: "BASE_LANGLANG_CODE");

            migrationBuilder.CreateIndex(
                name: "IX_Users_STUDY_LANGLANG_CODE",
                table: "Users",
                column: "STUDY_LANGLANG_CODE");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Languages_BASE_LANGLANG_CODE",
                table: "Users",
                column: "BASE_LANGLANG_CODE",
                principalTable: "Languages",
                principalColumn: "LANG_CODE");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Languages_STUDY_LANGLANG_CODE",
                table: "Users",
                column: "STUDY_LANGLANG_CODE",
                principalTable: "Languages",
                principalColumn: "LANG_CODE");
        }
    }
}
