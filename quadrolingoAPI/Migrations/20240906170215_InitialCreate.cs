using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quadrolingoAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    LANG_CODE = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LANG_ENG_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.LANG_CODE);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WORD_LANG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WORD_BASE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WORD_TRANSLATION = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CHAT_ID = table.Column<long>(type: "bigint", nullable: false),
                    TELEGRAM_ID = table.Column<long>(type: "bigint", nullable: false),
                    USERNAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FIRST_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LAST_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BASE_LANGLANG_CODE = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    STUDY_LANGLANG_CODE = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Languages_BASE_LANGLANG_CODE",
                        column: x => x.BASE_LANGLANG_CODE,
                        principalTable: "Languages",
                        principalColumn: "LANG_CODE");
                    table.ForeignKey(
                        name: "FK_Users_Languages_STUDY_LANGLANG_CODE",
                        column: x => x.STUDY_LANGLANG_CODE,
                        principalTable: "Languages",
                        principalColumn: "LANG_CODE");
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TIMESTAMP = table.Column<DateTime>(type: "datetime2", nullable: false),
                    USER_IDId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_Users_USER_IDId",
                        column: x => x.USER_IDId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TIMESTAMP = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WORD_IDId = table.Column<int>(type: "int", nullable: false),
                    USER_IDId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWords_Users_USER_IDId",
                        column: x => x.USER_IDId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWords_Words_WORD_IDId",
                        column: x => x.WORD_IDId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WordExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WORD_IDId = table.Column<int>(type: "int", nullable: false),
                    EXERCISE_IDId = table.Column<int>(type: "int", nullable: false),
                    Guessed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordExercises_Exercises_EXERCISE_IDId",
                        column: x => x.EXERCISE_IDId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WordExercises_Words_WORD_IDId",
                        column: x => x.WORD_IDId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_USER_IDId",
                table: "Exercises",
                column: "USER_IDId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWords_USER_IDId",
                table: "UserWords",
                column: "USER_IDId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWords_WORD_IDId",
                table: "UserWords",
                column: "WORD_IDId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BASE_LANGLANG_CODE",
                table: "Users",
                column: "BASE_LANGLANG_CODE");

            migrationBuilder.CreateIndex(
                name: "IX_Users_STUDY_LANGLANG_CODE",
                table: "Users",
                column: "STUDY_LANGLANG_CODE");

            migrationBuilder.CreateIndex(
                name: "IX_WordExercises_EXERCISE_IDId",
                table: "WordExercises",
                column: "EXERCISE_IDId");

            migrationBuilder.CreateIndex(
                name: "IX_WordExercises_WORD_IDId",
                table: "WordExercises",
                column: "WORD_IDId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWords");

            migrationBuilder.DropTable(
                name: "WordExercises");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}
